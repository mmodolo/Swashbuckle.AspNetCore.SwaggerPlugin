using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerPlugin.SampleAPI.Filters;

namespace Swashbuckle.AspNetCore.SwaggerPlugin.SampleAPI.v2
{
    
    [ApiVersion("2")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiAccessTokenAuth]
    public class FooController : ControllerBase
    {
        // GET api/foo
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1-v2", "value2-v2" };
        }

        // GET api/foo/5
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value-v2";
        }

        // POST api/foo
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/foo/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/foo/5
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
