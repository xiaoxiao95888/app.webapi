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
            var token = string.Empty;
            try
            {
                token = filterContext.Request.Headers.GetValues(key).FirstOrDefault();
            }
            catch (Exception)
            {
                // ignored
            }
            var segments = token?.Split('.');
            if (segments?.Length == 3)
            {
                var headerSeg = segments[0];
                var payloadSeg = segments[1];
                var signatureSeg = segments[2];
                if (headerSeg != null && payloadSeg != null && signatureSeg != null)
                {
                    var secret = ConfigurationManager.AppSettings["Secret"];
                    var payload = JsonConvert.DeserializeObject<Payload>(System.Text.Encoding.Default.GetString(Convert.FromBase64String(payloadSeg)));
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