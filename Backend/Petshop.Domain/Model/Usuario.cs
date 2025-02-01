using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Petshop.Domain.Model
{
    public class Usuario
    {
        [Key]
        public int Id { get; private set; }

        [Required]
        [StringLength(30), MinLength(3)]
        public string Nome { get; private set; }

        [Required]
        [StringLength(50), MinLength(3)]
        public string Sobrenome { get; private set; }

        [Required]
        [StringLength(11), MinLength(11)]
        public string NumeroTelefone { get; private set; }

        [Required]
        [StringLength(100), MinLength(3)]
        public string Endereco { get; private set; }

        [Required]
        [StringLength(80), MinLength(15)]
        public string Email { get; private set; }

        public DateTime CriacaoConta { get; private set; } = DateTime.Now;

        public DateTime AtualizacaoDeInformacoes { get; private set; } = DateTime.Now;

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public Usuario(string nome, string sobrenome, string numeroTelefone, string endereco, string email)
        {
            ValidateDomain(nome, sobrenome, numeroTelefone, endereco, email);
        }

        public void ValidateDomain(string nome, string sobrenome, string numeroTelefone, string endereco, string email)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            NumeroTelefone = numeroTelefone;
            Endereco = endereco;
            Email = email;
        }

        public void Update(string nome, string sobrenome, string numeroTelefone, string endereco, string email)
        {
            ValidateDomain(nome, sobrenome, numeroTelefone, endereco, email);
            AtualizacaoDeInformacoes = DateTime.Now;
        }

        public void DefinirSenha(byte[] senhaHash, byte[] senhaSalt)
        {
            PasswordHash = senhaHash;
            PasswordSalt = senhaSalt;
        }

    }
}