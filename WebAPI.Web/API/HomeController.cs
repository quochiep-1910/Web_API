using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Service;
using WebAPI.Web.Infrastructure.Core;

namespace WebAPI.Web.API
{
    [RoutePrefix("api/home")]
    [Authorize]
    public class HomeController : ApiControllerBase
    {
        private IErrorService _errorService;

        public HomeController(IErrorService errorService) : base(errorService)
        {
            this._errorService = errorService;
        }

        [HttpGet]
        [Route("TestMethod")]
        public string TestMethod()
        {
            return "Hello, TEDU Member. ";
        }
    }
}