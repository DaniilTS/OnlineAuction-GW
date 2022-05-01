using System;

namespace JWT.Auth.Models
{
    public class TokenModel
    {
        public string Value { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
