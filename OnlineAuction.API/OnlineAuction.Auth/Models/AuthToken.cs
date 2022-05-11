using System;

namespace OnlineAuction.Auth.Models
{
    public class AuthToken
    {
        public string Value { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
