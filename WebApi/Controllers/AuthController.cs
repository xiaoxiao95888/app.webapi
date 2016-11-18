using System;
using System.Configuration;
using System.Web.Http;
using Newtonsoft.Json;
using WebApi.Infrastructure;
using WebApi.Models.jwt;

namespace WebApi.Controllers
{
    [AllowAnonymous]
    public class AuthController : BaseApiController
    {
        public object Get()
        {
            var secret = ConfigurationManager.AppSettings["Secret"];
            var payload = new Payload
            {
                ExpirationTime = DateTime.MaxValue,
                UserId = Guid.NewGuid()
            };
            var header = new
            {
                Type = "JWT",
                Alg = "SHA1"
            };
            var base64Header = Helper.ToBase64String(JsonConvert.SerializeObject(header));
            var base64Payload = Helper.ToBase64String(JsonConvert.SerializeObject(payload));
            var signature = Helper.SHA1_Hash(base64Header + base64Payload + secret);
            var token = $"{base64Header}.{base64Payload}.{signature}";
            return token;
        }
    }
}
