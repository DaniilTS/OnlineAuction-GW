using OnlineAuction.JWT.Auth.Models;

namespace OnlineAuction.API.Models
{
    public class SignUpResponse
    {
        public AuthToken AccessToken { get; set; }
        public AuthToken RefreshToken { get; set; }
    }
}
