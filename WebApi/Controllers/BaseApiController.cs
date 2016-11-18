using System;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using Newtonsoft.Json;
using WebApi.Infrastructure;
using WebApi.Models.jwt;

namespace WebApi.Controllers
{
    [CustomFilter]
    public class BaseApiController : ApiController
    {
        public Payload CurrentPayload()
        {
            Payload payload = null;
            const string key = "Authorization";
            var token = Request.Headers.GetValues(key).First();
            var segments = token?.Split('.');
            if (segments?.Length == 3)
            {
                var payloadSeg = segments[1];
                payloadSeg = System.Text.Encoding.Default.GetString(Convert.FromBase64String(payloadSeg));
                payload = JsonConvert.DeserializeObject<Payload>(payloadSeg);

            }
            return payload;
        }
    }
}
