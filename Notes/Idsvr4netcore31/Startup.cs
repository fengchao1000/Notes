using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.DbContexts;
using Idsvr4;
using Idsvr4.IdentityServerExtensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Idsvr4netcore31
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            #region 4.0数据库迁移
            //https://identityserver4.readthedocs.io/en/latest/quickstarts/5_entityframework.html
            ////测试代码连接到本地LocalDb
            //const string connectionString = @"Data Source=114.67.236.81;Initial Catalog=Identityserver4;Integrated Security=False;Persist Security Info=False;User ID=sa;Password=feng@123";
            //var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;


            //// configure identity server with in-memory stores, keys, clients and scopes
            //services.AddIdentityServer()
            //   .AddDeveloperSigningCredential()
            //    //.AddTestUsers(Config.GetUsers())
            //    // this adds the config data from DB (clients, resources)
            //    .AddConfigurationStore(options =>
            //    {
            //        options.ConfigureDbContext = builder =>
            //            builder.UseSqlServer(connectionString,
            //                sql => sql.MigrationsAssembly(migrationsAssembly));
            //    })
            //    // this adds the operational data from DB (codes, tokens, consents)
            //    .AddOperationalStore(options =>
            //    {
            //        options.ConfigureDbContext = builder =>
            //            builder.UseSqlServer(connectionString,
            //                sql => sql.MigrationsAssembly(migrationsAssembly));

            //        // this enables automatic token cleanup. this is optional.
            //        options.EnableTokenCleanup = true;
            //        options.TokenCleanupInterval = 30;
            //    });

            //InitConfigDataToDB(services);

            #endregion


            #region Memory

            services.AddIdentityServer(options =>
            {
                options.Authentication.CookieSlidingExpiration = true;
                options.Authentication.CookieLifetime = TimeSpan.FromMinutes(5);
            })
                   //配置证书
                   .AddDeveloperSigningCredential()
                   // 配置API scope资源
                   .AddInMemoryApiScopes(Config.GetApiScopes())
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

            #endregion

            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //  .AddCookie(options =>
            //  {
            //      options.ExpireTimeSpan = System.TimeSpan.FromMinutes(5);
            //      options.SlidingExpiration = true;
            //  });

        }


        public static void InitConfigDataToDB(IServiceCollection services)
        {
            var provider = services.BuildServiceProvider();

            using (var scope = provider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetService<PersistedGrantDbContext>().Database.Migrate();

                scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>().Database.Migrate();

                //如果数据库表不存在，则会自动创建数据库表 
            }
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();//IdentityServer4中间件

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
            });
        }
    }
}
