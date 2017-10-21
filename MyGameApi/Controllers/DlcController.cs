using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;

namespace MyGameApi.Controllers
{
    using System.IO;
    using System.Net.Http.Headers;
    using System.Text;

    using CloudAndDlc.Models;

    public class DlcController : ApiController
    {
        // GET api/values
        public HttpResponseMessage Get(string name, string resource)
        {
            var path = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath + "\\App_Data\\";
            var filename = name + "_" + resource;

            var result = Request.CreateResponse(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(File.ReadAllBytes(path + filename));
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = filename
            };

            return result;

        }
    }
}
