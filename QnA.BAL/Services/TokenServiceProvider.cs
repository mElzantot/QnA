using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QnA.BAL.DTO;
using QnA.DbModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QnA.BAL.Services
{
    public class TokenServiceProvider : ITokenServiceProvider
    {
        private readonly IConfiguration _configuration;
        public TokenServiceProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public AuthResponseDTO GenerateAccessToken(AppUser appUser)
        {
            var expirationTime = DateTime.Now.AddMinutes(int.Parse(_configuration["JWT:LifeTimeInMinutes"]));
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                claims: GenerateUserClaim(appUser),
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
                expires: expirationTime,
                notBefore: DateTime.Now
                 );
            return new AuthResponseDTO
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = GenerateRefreshToken(),
                ExpiryDate = expirationTime
            };
        }

        private List<Claim> GenerateUserClaim(AppUser appUser)
        {
            return new List<Claim>
                {
                    new Claim(ClaimTypes.Name, appUser.Name),
                    new Claim(ClaimTypes.NameIdentifier, appUser.Id.ToString()),
                };
        }

        private string GenerateRefreshToken()
        {
            byte[] randomNumber = new byte[32];
            using RandomNumberGenerator rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

    }
}
