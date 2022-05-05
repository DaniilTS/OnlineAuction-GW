using OnlineAuction.API.Models.Shared;

namespace OnlineAuction.API.Models.Requests
{
    public class RefreshTokenRequest
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
