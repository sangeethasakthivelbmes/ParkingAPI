using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Parking.WebAPI.CoreHelper.Helpers.Interfaces;
using Parking.WebAPI.CoreHelper.Models;

namespace Parking.WebAPI.CoreHelper.Helpers
{
    public class JWTHelper : IJWTHelper
    {
        private const string _secret = "SUxxMFF0TnZMdXNoNGh2TWNHdWgyUjlKQU14bnV3b0JDY2hHVHZrRW1tL2R6WGI5ejEzb1BIV2xQN0dIVGJHTA==";
        public string GenerateToken(AuthUser user)
        {
            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Phone),
                    new Claim("UserId", user.UserId.ToString()),
                    new Claim("Name", user.Name),
                    new Claim("Email", user.Email),
                    new Claim(ClaimTypes.Role, user.RoleId.ToString())
                }),
                Expires = DateTime.MaxValue,
                SigningCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(securityToken);
            return tokenString;
        }

        public AuthUser ValidateToken(string token)
        {
            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
            var tokenHandler = new JwtSecurityTokenHandler();
            tokenHandler.ValidateToken(
                token,
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = symmetricKey,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = false,
                    ValidateLifetime = false,
                    ClockSkew = TimeSpan.Zero
                },
                out var validatedToken
            );

            var jwtToken = (JwtSecurityToken) validatedToken;

            return new AuthUser
            {
                UserId = Convert.ToInt64(jwtToken.Claims.First(c => c.Type == "UserId" ).Value),
                RoleId = Convert.ToInt64(jwtToken.Claims.First(c => c.Type == "role" ).Value),
                Email = jwtToken.Claims.First(c => c.Type == "Email").Value,
                Name = jwtToken.Claims.First(c => c.Type == "Name").Value,
                Phone = jwtToken.Claims.First(c => c.Type == "nameid").Value
            };
        }
    }
}

