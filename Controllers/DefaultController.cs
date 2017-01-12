using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace WebApi.Controllers
{
    [RoutePrefix("api/main")]
    public class DefaultController : BaseController
    {
        [HttpGet]
        [Route("getok")]
        public Task<HttpResponseMessage> Main()
        {
            return CreateResponse(System.Net.HttpStatusCode.OK, "ok");
        }

    }
}