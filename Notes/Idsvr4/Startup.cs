using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Idsvr4.IdentityServerExtensions;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Idsvr4
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
             
            services.AddIdentityServer(options =>
            {
                options.Authentication.CookieSlidingExpiration = true;
                options.Authentication.CookieLifetime = TimeSpan.FromMinutes(5);
            })
                   //配置证书
                   .AddDeveloperSigningCredential()
                   //配置API资源
                   .AddInMemoryApiResources(Config.GetApis())
                   //配置身份资源
                   .AddInMemoryIdentityResources(Config.GetIdentityResources())
                   //预置Client
                   .AddInMemoryClients(Config.GetClients())
                   //自定义登录返回信息
                   .AddProfileService<ProfileService>()
                   //添加Password模式下用于自定义登录验证 
                   .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
                  //添加自定义授权模式
                  .AddExtensionGrantValidator<SMSGrantValidator>()
                  //添加操作数据存储
                  //.AddOperationalStore(options =>
                  //{
                  //    options.RedisConnectionString = Configuration.GetConnectionString("Redis");
                  //    options.Db = 1;
                  //})
                  //IdentityServer配置缓存，缓存Client、CorsPolicy、Resource等信息，CachingClientStore，CachingCorsPolicyService，CachingResourceStore，https://github.com/AliBazzi/IdentityServer4.Contrib.RedisStore
                  //.AddRedisCaching(options =>
                  //{
                  //    options.RedisConnectionString = Configuration.GetConnectionString("Redis");
                  //    options.Db = 1;
                  //})
                  ;

            //services.AddAuthentication()
            //.AddCookie(options =>
            //{
            //    options.ExpireTimeSpan = System.TimeSpan.FromMinutes(30);
            //    options.SlidingExpiration = true;
            //});


            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
              .AddCookie(options =>
              {
                  options.ExpireTimeSpan = System.TimeSpan.FromMinutes(5);
                  options.SlidingExpiration = true;
              });


            //options =>
            //{
            //    options.Authentication.CookieSlidingExpiration = true;
            //    options.Authentication.CookieLifetime = TimeSpan.FromHours(1);
            //}


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();//IdentityServer4中间件

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("IdentityServer4");
            //});

            app.UseStaticFiles();

            //app.UseMvcWithDefaultRoute();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Account", action = "Login" }
                );
            });
        }
    }
}
