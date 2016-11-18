using Library.Models.Interfaces;
using System;

namespace Library.Models
{
    public class User: IDtStamped
    {
        public Guid Id { get; set; }
        public string OpenId { get; set; }
        public string NickName { get; set; }
        public string Sex { get; set; }
        public string Language { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string Headimgurl { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
