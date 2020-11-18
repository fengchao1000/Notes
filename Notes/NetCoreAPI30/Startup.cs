using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;

namespace NetCoreAPI30
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
            services.AddControllers(options =>
            {
                //options.Filters.Add(typeof(AuthorizeAttribute));
                //options.Filters.Add(new RequireHttpsAttribute());

                //var policy = new AuthorizationPolicyBuilder()
                // .RequireAuthenticatedUser()
                // .RequireRole("Admin", "SuperUser")
                // .Build();
                // options.Filters.Add(new AuthorizeFilter(policy)); 
            });

            //IdentityServer
            services.AddAuthentication("Bearer")
               .AddIdentityServerAuthentication(options =>
               {
                   options.RequireHttpsMetadata = false;
                   options.Authority = "http://localhost:63238";
                   options.ApiName = "FrameworkAPI"; 
               });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            #region 跨域
            app.UseCors(policy =>
            {
                policy.WithOrigins("http://localhost:5001");
                policy.AllowAnyHeader();
                policy.AllowCredentials();
                policy.AllowAnyMethod();
                policy.WithExposedHeaders(HeaderNames.ContentDisposition, HeaderNames.WWWAuthenticate);
            });
            #endregion

            app.UseAuthentication();

            app.UseAuthorization();
             
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
