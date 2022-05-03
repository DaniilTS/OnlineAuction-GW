using OnlineAuction.JWT.Auth.Models;

namespace OnlineAuction.API.Models
{
    public class LoginResponse
    {
        public AuthToken AccessToken { get; set; }
        public AuthToken RefreshToken { get; set; }
    }
}
