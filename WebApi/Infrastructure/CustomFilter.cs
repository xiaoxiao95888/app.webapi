using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Newtonsoft.Json;
using WebApi.Models;
using WebApi.Models.jwt;

namespace WebApi.Infrastructure
{
    public class CustomFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            var error = true;
            const string key = "Authorization";
            var token = filterContext.Request.Headers.GetValues(key).First();
            var segments = token?.Split('.');
            if (segments?.Length == 3)
            {
                var headerSeg = segments[0];
                var payloadSeg = segments[1];
                var signatureSeg = segments[2];
                if (headerSeg != null && payloadSeg != null && signatureSeg != null)
                {
                    payloadSeg = System.Text.Encoding.Default.GetString(Convert.FromBase64String(payloadSeg));
                    headerSeg = System.Text.Encoding.Default.GetString(Convert.FromBase64String(headerSeg));
                    var secret = ConfigurationManager.AppSettings["Secret"];
                    var payload = JsonConvert.DeserializeObject<Payload>(payloadSeg);
                    if (payload.ExpirationTime >= DateTime.UtcNow)
                    {
                        var signature = Helper.SHA1_Hash(headerSeg + payloadSeg + secret);
                        if (signature.ToUpper() == signatureSeg)
                        {
                            error = false;
                        }
                    }
                }
            }
            if (error)
            {
                var errorResponse = filterContext.Request.CreateResponse(HttpStatusCode.Unauthorized);//InternalServerError
                filterContext.Response = errorResponse;
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}