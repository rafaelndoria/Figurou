using Figurou.Business.Configuration;
using Figurou.Business.Enums;
using Figurou.Business.Services.Interfaces;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Figurou.Business.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly AppSettings _settings;

        public AuthService(IOptions<AppSettings> options)
        {
            _settings = options.Value;
        }

        public string CriptografarSenha(string senha)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    builder.Append(hashedBytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public string GerarJwtToken(string email, string nomeUsuario, EUsuarioRole papel, Guid usuarioId, Guid? albumId)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Secret));
            var issuer = _settings.Emissor;
            var audience = _settings.ValidoEm;

            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, nomeUsuario),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, papel.ToString()),
                new Claim("UsuarioId", usuarioId.ToString())
            };

            if (albumId != null)
            {
                claims.Add(new Claim("AlbumId", albumId.ToString()));
            }

            var tokenOptions = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddHours(4),
                signingCredentials: signinCredentials);

            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return token;
        }
    }
}
