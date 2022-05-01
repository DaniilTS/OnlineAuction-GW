namespace JWT.Auth.Models
{
    public class AuthSettings
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }
        public int AccessTokenLifeTime { get; set; }
        public int RefreshTokenLifeTime { get; set; }
    }
}
