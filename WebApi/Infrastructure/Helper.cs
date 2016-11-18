using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using Newtonsoft.Json;

namespace WebApi.Infrastructure
{
    public static class Helper
    {
        public static string SHA1_Hash(string strSha1In)
        {
            var sha1 = new SHA1CryptoServiceProvider();
            var bytesSha1In = System.Text.Encoding.Default.GetBytes(strSha1In);
            var bytesSha1Out = sha1.ComputeHash(bytesSha1In);
            var strSha1Out = BitConverter.ToString(bytesSha1Out);
            strSha1Out = strSha1Out.Replace("-", "").ToUpper();
            return strSha1Out;
        }

        public static string ToBase64String(string json)
        {
            var encode = System.Text.Encoding.Default;
            var bytedata = encode.GetBytes(json);
            return Convert.ToBase64String(bytedata, 0, bytedata.Length);
        }
    }
}