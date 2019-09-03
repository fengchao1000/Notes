using System;
using System.Threading.Tasks;
using IdentityServer3.AccessTokenValidation;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(FrameworkAPI.Startup))]

namespace FrameworkAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        { 
            app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = "http://localhost:62527/",
                RequiredScopes = new[] { "FrameworkAPI" },//对应Client中配置的AllowedScopes和ApiResource

                DelayLoadMetadata = true
            });

        }
    }
}
