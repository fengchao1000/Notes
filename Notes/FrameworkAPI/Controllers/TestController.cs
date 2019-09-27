using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace FrameworkAPI.Controllers
{
    [Route("test")]
    //[PermitFilter]
    //[Authorize]
    public class TestController : BaseController
    {
        public IHttpActionResult Get()
        {

            string userIDTest = UserID;

            var user = User as ClaimsPrincipal;
            
            var claims = from c in user.Claims
                         select new
                         {
                             type = c.Type,
                             value = c.Value
                         };

            return Json(claims);
        }


    }
}