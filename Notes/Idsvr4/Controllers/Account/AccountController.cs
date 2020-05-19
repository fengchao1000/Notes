using IdentityModel;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Idsvr4.Attributes;
using Idsvr4.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Idsvr4.Controllers.Account
{
    //[SecurityHeaders]
    public class AccountController : Controller
    { 
        private readonly IIdentityServerInteractionService interaction;
        private readonly IClientStore clientStore;
        private readonly IAuthenticationSchemeProvider schemeProvider;
        private readonly IEventService events;
        private readonly IConfiguration config;
        private readonly IUserSession userSession;
        private readonly IHostingEnvironment environment;
        private readonly ILogger logger;

        public AccountController(
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            IAuthenticationSchemeProvider schemeProvider,
            IEventService events,
            IConfiguration config,
            IUserSession userSession,
            IHostingEnvironment environment,
            ILogger<AccountController> logger)
        {
           
            this.interaction = interaction;
            this.clientStore = clientStore;
            this.schemeProvider = schemeProvider;
            this.events = events;
            this.config = config;
            this.userSession = userSession;
            this.environment = environment;
            this.logger = logger;
        }

        #region 登录
        /// <summary>
        /// 显示登陆页面
        /// </summary>
        /// <param name="returnUrl">跳转地址</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {



            //如果returnUrl为空，则跳转，如果不是开发环境，returnUrl为空时先跳到Secure地址，因为需要拿到回调地址
            //if (!environment.IsDevelopment())
            //{
            //    if (string.IsNullOrEmpty(returnUrl))
            //    {
            //        return Redirect(config.GetValue<string>(ConstanceHelper.AppSettings.PASystemPage));
            //    }
            //}
            //根据returnUrl构建LoginViewModel
            var vm = await BuildLoginViewModelAsync(returnUrl);

            return View(vm);
        }

        /// <summary>
        /// 根据returnUrl构建LoginViewModel
        /// </summary>
        /// <param name="returnUrl">跳转地址</param>
        /// <returns></returns>
        private async Task<LoginViewModel> BuildLoginViewModelAsync(string returnUrl)
        {
            return new LoginViewModel
            {
                AllowRememberLogin = AccountOptions.AllowRememberLogin,
                EnableLocalLogin = AccountOptions.AllowLocalLogin,
                ReturnUrl = returnUrl
            };
        }

        /// <summary>
        /// 登录
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginInputModel model)
        { 
            if (string.IsNullOrEmpty(model.UserID))
            {
                ModelState.AddModelError("", "请输入用户名");
                return View(await BuildLoginViewModelAsync(model));
            }
            
            if (string.IsNullOrEmpty(model.Password))
            {
                ModelState.AddModelError("", "请输入密码");
                return View(await BuildLoginViewModelAsync(model));
            }
            //验证用户名密码是否正确
            if (model.UserID !=  "test" || model.Password != "test")
            {
                ModelState.AddModelError("", "用户名或者密码错误");
                return View(await BuildLoginViewModelAsync(model));
            }

            return await LoginSuccessful(model);
        } 
         
        /// <summary>
        /// 登录成功处理
        /// </summary>
        /// <param name="model">LoginInputModel</param>
        public async Task<IActionResult> LoginSuccessful(LoginInputModel model)
        { 
            await events.RaiseAsync(new UserLoginSuccessEvent("test", "test", "test"));
            //设置登录过期时间
            AuthenticationProperties props = new AuthenticationProperties
            {
                IsPersistent = false,//true表示关闭浏览器再打开系统不需要输入账号密码
                ExpiresUtc = DateTimeOffset.UtcNow.Add(AccountOptions.DefaultLoginDuration)
            }; 
             
            //颁发身份认证Cookie
            Claim[] claimArray = new Claim[] {
                                 new Claim("userID","1"),
                                 new Claim("userName", "test")
                                            };

            await HttpContext.SignInAsync("userID", "userName",  claimArray);
            string currentSessionId = await userSession.GetSessionIdAsync();
            
            if (interaction.IsValidReturnUrl(model.ReturnUrl) || Url.IsLocalUrl(model.ReturnUrl))
            {
                return Redirect(model.ReturnUrl);
            }
            else
            {
                return Redirect("");
            } 
        }
         
        /// <summary>
        /// 登录失败，构建LoginViewModel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private async Task<LoginViewModel> BuildLoginViewModelAsync(LoginInputModel model)
        {
            var vm = await BuildLoginViewModelAsync(model.ReturnUrl);
            vm.UserID = model.UserID; 
            vm.RememberLogin = model.RememberLogin; 
            return vm;
        }
         
        #endregion

        #region 登出
        /// <summary>
        /// 显示登出页面
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            var vm = await BuildLoggedOutViewModelAsync(logoutId);

            if (User?.Identity.IsAuthenticated == true)
            {
                await LocalLogout();
            }
            if (string.IsNullOrEmpty(vm.PostLogoutRedirectUri))
            {
                if (environment.IsDevelopment() || environment.IsStaging())
                {
                    var refererUrl = Request.Headers["Referer"].ToString();
                    if (refererUrl.IndexOf('/') > 0)
                    {
                        string[] url = refererUrl.Split('/');
                        return Redirect(url[0] + "//" + url[2]);
                    }
                    return Redirect(refererUrl);
                }
                else
                {
                    return Redirect("");
                }
            }
            else
            {
                return Redirect(vm.PostLogoutRedirectUri);
            }
        }

        /// <summary>
        /// 清除本地登录信息
        /// </summary>
        /// <returns></returns>
        private async Task LocalLogout()
        {
            if (User?.Identity.IsAuthenticated == true)
            {
                await HttpContext.SignOutAsync();//删除认证Cookie 
                await events.RaiseAsync(new UserLogoutSuccessEvent(User.GetSubjectId(), User.GetDisplayName()));//启动注销事件
            }
        }

        /// <summary>
        /// 构建LoggedOutViewModel模型
        /// </summary>
        /// <param name="logoutId"></param>
        /// <returns></returns>
        private async Task<LoggedOutViewModel> BuildLoggedOutViewModelAsync(string logoutId)
        {
            // 获取上下文信息，包括退出客户端、跳转地址 (client name, post logout redirect URI and iframe for federated signout)
            var logout = await interaction.GetLogoutContextAsync(logoutId);

            var vm = new LoggedOutViewModel
            {
                AutomaticRedirectAfterSignOut = AccountOptions.AutomaticRedirectAfterSignOut,
                PostLogoutRedirectUri = logout?.PostLogoutRedirectUri,
                ClientName = string.IsNullOrEmpty(logout?.ClientId) ? logout?.ClientId : logout?.ClientId,
                SignOutIframeUrl = logout?.SignOutIFrameUrl,
                LogoutId = logoutId
            };

            if (User?.Identity.IsAuthenticated == true)
            {
                var idp = User.FindFirst(JwtClaimTypes.IdentityProvider)?.Value;
                if (idp != null && idp != IdentityServer4.IdentityServerConstants.LocalIdentityProvider)
                {
                    var providerSupportsSignout = await HttpContext.GetSchemeSupportsSignOutAsync(idp);
                    if (providerSupportsSignout)
                    {
                        if (vm.LogoutId == null)
                        {
                            // if there's no current logout context, we need to create one
                            // this captures necessary info from the current logged in user
                            // before we signout and redirect away to the external IdP for signout
                            vm.LogoutId = await interaction.CreateLogoutContextAsync();
                        }

                        vm.ExternalAuthenticationScheme = idp;
                    }
                }
            }

            return vm;
        }
        #endregion

        

    }

    public class AccountOptions
    {
        public static bool AllowLocalLogin = true;
        public static bool AllowRememberLogin = true;
        public static TimeSpan RememberMeLoginDuration = TimeSpan.FromDays(30);
        public static TimeSpan DefaultLoginDuration = TimeSpan.FromMinutes(5); //默认登录过期时间

        public static bool ShowLogoutPrompt = true;
        public static bool AutomaticRedirectAfterSignOut = false;

        // specify the Windows authentication scheme being used
        public static readonly string WindowsAuthenticationSchemeName = Microsoft.AspNetCore.Server.IISIntegration.IISDefaults.AuthenticationScheme;
        // if user uses windows auth, should we load the groups from windows
        public static bool IncludeWindowsGroups = false;
        
    }

}
