using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreAPICSRFClient.Helper;
using Newtonsoft.Json;

namespace NetCoreAPICSRFClient.Controllers
{
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
            RequestAPIHelper requestAPIHelper = new RequestAPIHelper();
            string url = "http://localhost:50188/api/values";
            string response = requestAPIHelper.RequestAPI(url, JsonConvert.SerializeObject(user), ConstanceHelper.requestAPIMethod_Post, out string errorMsg);

            if (errorMsg != string.Empty)
            {
                return BadRequest($"{response}|{errorMsg}");
            }

            return Ok(response);
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