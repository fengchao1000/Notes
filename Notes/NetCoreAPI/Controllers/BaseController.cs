using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace NetCoreAPI.Controllers
{
    public class BaseController : ControllerBase
    {  
        /// <summary>
        /// UserID,用户标识
        /// </summary>
        public string UserID
        {
            get
            {
                Claim userIDClaim = CurrentUser.Claims.Where(q => q.Type == "userID").FirstOrDefault();
                if (userIDClaim != null)
                {
                    return userIDClaim.Value;
                }
                else
                {
                    throw new ArgumentNullException(nameof(UserID));
                }
            }
        }
        /// <summary>
        /// CurrentUser
        /// </summary>
        public ClaimsPrincipal CurrentUser
        {
            get
            {
                ClaimsPrincipal currentUser = User as ClaimsPrincipal;
                if (currentUser != null)
                {
                    return currentUser;
                }
                else
                {
                    throw new ArgumentNullException(nameof(CurrentUser));
                }
            }
        }

    }
}