using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace NetCoreAPICSRFClient
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            ////使服务器能够XSRF-TOKEN从Angular应用程序中检测出防伪令牌的值，现在，您的ASP.NET Core应用程序知道与请求一起作为标头发送的令牌。
            //services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");
            ////options => options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute())
            //services.AddMvc(options => options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()))
            //       .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
