using JWT.Auth.Helpers;
using JWT.Auth.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace JWT.Auth.Services
{
    public class TokenService
    {
        private readonly AuthSettings _authSettings;
        public TokenService(AuthSettings authSettings)
        {
            _authSettings = authSettings;
        }

        public AuthToken GenerateAccessToken(string userName, string roleName) 
        {
            var claims = new List<Claim>
            {
                new (ClaimTypes.Name, userName),
                new (ClaimTypes.Role, roleName)
            };

            var secretKey = KeyHelper.GetSymmetricSecurityKey(_authSettings.Key);
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var utcNow = DateTime.UtcNow;

            var expireDate = utcNow.AddMinutes(_authSettings.AccessTokenLifeTime);

            var tokenOptions = new JwtSecurityToken(
                issuer: _authSettings.Issuer,
                audience: _authSettings.Audience,
                notBefore: utcNow,
                claims: claims,
                expires: expireDate,
                signingCredentials: signinCredentials
            );

            return new AuthToken
            {
                Value = new JwtSecurityTokenHandler().WriteToken(tokenOptions),
                ExpiryDate = expireDate
            };
        }

        public AuthToken GenerateRefreshToken()
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                var randomNumber = new byte[32];
                rng.GetBytes(randomNumber);
                return new AuthToken
                {
                    Value = Convert.ToBase64String(randomNumber),
                    ExpiryDate = DateTime.UtcNow.AddDays(_authSettings.RefreshTokenLifeTime)
                };
            }
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            if (token is null)
                return null;

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _authSettings.Issuer,
                ValidateAudience = true,
                ValidAudience = _authSettings.Audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = KeyHelper.GetSymmetricSecurityKey(_authSettings.Key),
                ValidateLifetime = true
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
            if (principal is null)
                return null;

            if (securityToken is not JwtSecurityToken jwtSecurityToken
                || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }
    }
}
