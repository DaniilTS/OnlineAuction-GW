using OnlineAuction.JWT.Auth.Models;

namespace OnlineAuction.API.Models.Shared
{
    public class AuthBase
    {
        public AuthToken AccessToken { get; set; }
        public AuthToken RefreshToken { get; set; }
    }
}
