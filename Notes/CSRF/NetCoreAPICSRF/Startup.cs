using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace NetCoreAPICSRF
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //使服务器能够XSRF-TOKEN从Angular应用程序中检测出防伪令牌的值，现在，您的ASP.NET Core应用程序知道与请求一起作为标头发送的令牌。
            services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");
            //options => options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute())
            services.AddMvc(options => options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()))
                   .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IAntiforgery antiforgery)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.Use(next => context =>
            //{
            //    string path = context.Request.Path.Value;

            //    //if (
            //    //    string.Equals(path, "/api/values", StringComparison.OrdinalIgnoreCase))
            //    //{
            //    //    var tokens = antiforgery.GetAndStoreTokens(context);
            //    //    context.Response.Cookies.Append("XSRF-TOKEN", tokens.RequestToken,
            //    //        new CookieOptions() { HttpOnly = false });
            //    //}

            //    var tokens = antiforgery.GetAndStoreTokens(context);
            //    context.Response.Cookies.Append("XSRF-TOKEN", tokens.RequestToken,
            //        new CookieOptions() { HttpOnly = false });

            //    return next(context);
            //});

            app.UseMvc();
        }
    }
}
