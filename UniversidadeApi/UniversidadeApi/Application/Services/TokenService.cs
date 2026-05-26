using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UniversidadeApi.Models;

namespace UniversidadeApi.Application.Services
{
    public static class TokenService
    {
        public static string Gerar(Usuario usuario)
        {
            var handler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes("}bCIx{:,oR1p%(aHq4M$jRu6RC[^b{U?BG{9nA0YF^p");
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = GerarClaims(usuario),
                SigningCredentials = credentials,
                Issuer = "Api",
                Audience = "Cliente",
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
