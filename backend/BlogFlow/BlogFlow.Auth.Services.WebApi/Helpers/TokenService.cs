using Azure.Core;
using Azure;
using BlogFlow.Core.Application.DTO;
using BlogFlow.Core.Domain.Entities;
using BlogFlow.Core.Transversal.Common;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlogFlow.Auth.Services.WebApi.Helpers
{
    public class TokenService
    {
        private readonly AppSettings _appSettings;

        public TokenService(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public RefreshTokenDTO CreateNewToken(int? userId)
        {
            var (accessToken, refreshToken) = BuildToken(userId);

            return new RefreshTokenDTO
            {
                Token = refreshToken,
                AccessToken = accessToken,
                JwtId = Guid.NewGuid().ToString(),
                IsRevoked = false,
                IsUsed = false,
                ExpiryDate = DateTime.UtcNow.AddDays(Convert.ToDouble(_appSettings.RefreshTokenExpiration)),
                UserId = userId
            };
        }

        private (string AccessToken, string RefreshToken) BuildToken(int? userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_appSettings.AccessTokenExpiration)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.Audience
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var refreshToken = Guid.NewGuid().ToString();

            return (tokenHandler.WriteToken(token), refreshToken);
        }
    }
}
