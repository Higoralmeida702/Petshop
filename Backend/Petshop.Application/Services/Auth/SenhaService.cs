using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Petshop.Application.Interfaces.Auth;
using Petshop.Domain.Model;

namespace Petshop.Application.Services.Auth
{
    public class SenhaService : ISenhaService
    {
        private readonly IConfiguration _configuration;

        public SenhaService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void CriarSenhaHash(string senha, out byte[] senhaHash, out byte[] senhaSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                senhaSalt = hmac.Key;
                senhaHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));
            }
        }

        public string CriarToken<T>(T usuarioParametro) where T : class
        {
            if (usuarioParametro is Usuario usuario)
            {

                return CriarTokenBase(usuario.Email, usuario.Nome);
            }

            throw new ArgumentException("Tipo de usuário inválido.");
        }

        public bool VerificarSenhaHash(string senha, byte[] senhaHash, byte[] senhaSalt)
        {
            using (var hmac = new HMACSHA512(senhaSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));
                return computedHash.SequenceEqual(senhaHash);
            }
        }

        private string CriarTokenBase(string email, string username)
        {
            var claims = new List<Claim>
            {
                new Claim("Email", email),
                new Claim("Username", username),
            };

            var tokenKey = _configuration.GetSection("AppSettings:Token").Value;
            if (string.IsNullOrEmpty(tokenKey))
            {
                throw new ArgumentException("A chave do token não pode ser nula ou vazia.");
            }

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(tokenKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}