using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using Idsvr4.Helper;
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
                new ApiResource("FrameworkAPI",new List<string>(){JwtClaimTypes.Subject}),
                new ApiResource("NetCoreAPI",new List<string>(){JwtClaimTypes.Subject})
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
                //ResourceOwnerPassword模式Client配置，适用于App、Winform
                new Client
                {
                    ClientId = "Wpf",
                    ClientName = "App",
                    ClientSecrets = { new Secret("123456".Sha256()) },
                    AccessTokenLifetime = 60*60,//单位s
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AbsoluteRefreshTokenLifetime = 2592000,//RefreshToken的最长生命周期,默认30天
                    RefreshTokenExpiration = TokenExpiration.Sliding,//刷新令牌时，将刷新RefreshToken的生命周期。RefreshToken的总生命周期不会超过AbsoluteRefreshTokenLifetime。
                    SlidingRefreshTokenLifetime = 3600*24,//以秒为单位滑动刷新令牌的生命周期。 
                    AllowOfflineAccess = true,
                    AllowedScopes = new List<string>
                    {
                        "FrameworkAPI",//对应webapi里面的scope配置
                        "NetCoreAPI",//对应webapi里面的scope配置
                        StandardScopes.OfflineAccess,
                        StandardScopes.OpenId,
                        StandardScopes.Profile
                    }
                },
                //自定义短信验证模式
                new Client
                {
                    ClientId = "sms",
                    ClientName = "sms",
                    ClientSecrets = { new Secret("123456".Sha256()) },
                    AccessTokenLifetime = 60*60,//单位s
                    AllowedGrantTypes = new[] {ExtensionGrantTypes.SMSGrantType}, //一个 Client 可以配置多个 GrantType
                    AbsoluteRefreshTokenLifetime = 2592000,//RefreshToken的最长生命周期,默认30天
                    RefreshTokenExpiration = TokenExpiration.Sliding,//刷新令牌时，将刷新RefreshToken的生命周期。RefreshToken的总生命周期不会超过AbsoluteRefreshTokenLifetime。
                    SlidingRefreshTokenLifetime = 3600*24,//以秒为单位滑动刷新令牌的生命周期。
                    AllowOfflineAccess = true,
                    AllowedScopes = new List<string>
                    {
                        "FrameworkAPI",//对应webapi里面的scope配置
                        "NetCoreAPI",//对应webapi里面的scope配置
                        StandardScopes.OfflineAccess,
                        StandardScopes.OpenId,
                        StandardScopes.Profile
                    }
                },
                new Client
                {
                    ClientId = "SwaggerClientId",
                    ClientName = "Swagger UI",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false,
                    RedirectUris =
                    {
                        "http://localhost:5001/swagger/oauth2-redirect.html"//swagger回调地址
                    },
                    AllowedScopes = new List<string>
                    {
                        "FrameworkAPI",//对应webapi里面的scope配置
                        "NetCoreAPI",//对应webapi里面的scope配置
                        StandardScopes.OfflineAccess,
                        StandardScopes.OpenId,
                        StandardScopes.Profile
                    }
                }

            };
        }  
    }
}
