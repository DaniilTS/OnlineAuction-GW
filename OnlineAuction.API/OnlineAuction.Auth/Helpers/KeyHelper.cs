using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace OnlineAuction.Auth.Helpers
{
    public static class KeyHelper
    {
        public static SecurityKey GetSymmetricSecurityKey(string key)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        }
    }
}
