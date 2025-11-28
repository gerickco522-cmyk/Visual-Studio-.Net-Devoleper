using BE.DTO;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BL.Estructura
{
    public class TokenService
    {
        private static string? _secretKey;
        private static string? _issuer;
        private static string? _audience;
        private static double _expireMinutes;

        public static void Initialize(IConfiguration configuration)
        {
            _secretKey = configuration["Jwt:Secret"];
            _issuer = configuration["Jwt:Issuer"];
            _audience = configuration["Jwt:Audience"];
            var exp = configuration["Jwt:ExpireTime"];

            if (string.IsNullOrWhiteSpace(_secretKey))
                throw new Exception("Jwt:Secret is missing in appsettings.json");

            if (string.IsNullOrWhiteSpace(_issuer))
                throw new Exception("Jwt:Issuer is missing in appsettings.json");

            if (string.IsNullOrWhiteSpace(_audience))
                throw new Exception("Jwt:Audience is missing in appsettings.json");

            if (string.IsNullOrWhiteSpace(exp) || !double.TryParse(exp, out _expireMinutes))
                throw new Exception("Jwt:ExpireTime must be numeric in appsettings.json");
        }


        public string GenerateJWT(Usuarios user)
        {
            // Claims del usuario
            Claim[] claims = new[] {
            new Claim("Id", user.id.ToString()),
            new Claim("Username", user.username),
            new Claim("Email", user.email),
            new Claim("CreatedAt", user.createdAt.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            // Key & firma
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_expireMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
