using System;

namespace JWT.Auth.Models
{
    public class AuthToken
    {
        public string Value { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
