using System;

namespace WebApi.Models.jwt
{
    public class Payload
    {
        public Guid UserId { get; set; }
        public DateTime ExpirationTime { get; set; }
    }
}