using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerPlugin.SampleAPI.Filters;

namespace Swashbuckle.AspNetCore.SwaggerPlugin.SampleAPI.v1
{
    [ApiVersion("0.9", Deprecated = true)]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiAccessTokenAuth]
    public class FooController : ControllerBase
    {
        /// <summary>
        /// Get Values test from the API
        /// </summary>
        /// <returns></returns>
        // GET api/foo
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/foo/5
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/foo
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        /// <summary>
        /// Updates existing value
        /// </summary>
        /// <param name="id">value id</param>
        /// <param name="value">new value</param>
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
