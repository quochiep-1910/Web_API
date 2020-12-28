using System.Collections.Generic;
using System.Web.Http;
using WebAPI.Service;
using WebAPI.Web.Infrastructure.Core;

namespace WebAPI.Web.API
{
    public class PostCategoryController : ApiControllerBase
    {
        public PostCategoryController(IErrorService errorService) : base(errorService)
        {
        }

        // GET: api/PostCategory
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/PostCategory/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/PostCategory
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/PostCategory/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/PostCategory/5
        public void Delete(int id)
        {
        }
    }
}