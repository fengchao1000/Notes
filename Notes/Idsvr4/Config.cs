using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace Idsvr4
{
    public class Config
    {
        // scopes define the resources in your system
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource("FrameworkAPI",new List<string>(){JwtClaimTypes.Subject})
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            { 
                
                //Implicit模式Client配置，适用于SPA
                new Client
                {
                    ClientId = "Test",
                    ClientName = "Test",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    AccessTokenLifetime = 60*60,
                    AccessTokenType = AccessTokenType.Jwt,
                    RedirectUris =
                    {
                        "http://localhost:5003/callback.html",
                        "http://localhost:5003/silent.html"
                    },
                    PostLogoutRedirectUris = { "http://localhost:5003" },
                    AllowedCorsOrigins = { "http://localhost:5003" },
                    RequireConsent = false,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "FrameworkAPI"//对应webapi里面的scope配置
                    }
                },
                //ResourceOwnerPassword模式Client配置，适用于App、webform
                new Client
                {
                    ClientId = "App",
                    ClientName = "App",
                    ClientSecrets = { new Secret("123456".Sha256()) },
                    AccessTokenLifetime = 60*60,//单位s
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    SlidingRefreshTokenLifetime =  2592000,
                    AllowOfflineAccess = true,
                    AllowedScopes = new List<string>
                    {
                        "FrameworkAPI",//对应webapi里面的scope配置
                        StandardScopes.OfflineAccess,
                        StandardScopes.OpenId,
                        StandardScopes.Profile
                    }
                }

            };
        }  
    }
}
