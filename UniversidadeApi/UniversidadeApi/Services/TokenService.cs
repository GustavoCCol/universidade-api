using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UniversidadeApi.Models;

namespace UniversidadeApi.Services
{
    public static class TokenService
    {
        public static string Gerar(Usuario usuario)
        {
            var handler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(Configuration.PrivateKey);
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = GerarClaims(usuario),
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddHours(2)
            };

            var token = handler.CreateToken(tokenDescriptor);

            return handler.WriteToken(token);
        }

        private static ClaimsIdentity GerarClaims(Usuario usuario)
        {
            var ci = new ClaimsIdentity();
            ci.AddClaim(new Claim(ClaimTypes.Name, usuario.ID.ToString()));
            ci.AddClaim(new Claim("Nome", usuario.NOME));
            return ci;
        }
    }
}
