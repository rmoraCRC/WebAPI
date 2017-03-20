using System;
using SecurityAppBusiness.Interface;

namespace SecurityAppBusiness.Entity
{
    public class TokenEntity : ITokenEntity
    {
        public int TokenId { get; set; }
        public int UserId { get; set; }
        public string AuthToken { get; set; }
        public DateTime IssuedOn { get; set; }
        public DateTime ExpiresOn { get; set; }
        public bool Status { get; set; }
    }
}