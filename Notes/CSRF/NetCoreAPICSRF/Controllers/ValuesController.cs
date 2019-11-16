using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NetCoreAPICSRF.Controllers
{

    [Route("api/[controller]")] 
    [ApiController] public class SecurityController : Controller
    { 
        
        private IAntiforgery antiforgery; 

        public SecurityController(IAntiforgery antiforgery)
        {
            this.antiforgery = antiforgery; 
        }

        //[HttpGet("xsrf-token")] 
        [HttpGet]
        public ActionResult GetXsrfToken() 
        { 
            var tokens = antiforgery.GetAndStoreTokens(HttpContext); 
            Response.Cookies.Append("XSRF-TOKEN", tokens.RequestToken, new CookieOptions 
            { 
                HttpOnly = false, Path = "/", IsEssential = true, SameSite = SameSiteMode.Lax 
            });
            return Ok(tokens.RequestToken);
        }
    }




    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public ActionResult<string> Post([FromBody]User user)
        {
            return "add user success";
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


    }

    [Serializable]
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
