using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Petshop.Domain.Validations;

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

        public ICollection<Animal> Animais { get; set; } = new List<Animal>();

        public Usuario(string nome, string sobrenome, string numeroTelefone, string endereco, string email)
        {
            ValidateDomain(nome, sobrenome, numeroTelefone, endereco, email);
        }

        public void ValidateDomain(string nome, string sobrenome, string numeroTelefone, string endereco, string email)
        {

            DomainExceptionValidations.When(nome.Length < 3 || nome.Length > 30, "O nome deve ter entre 3 e 30 caracteres");
            DomainExceptionValidations.When(sobrenome.Length < 3 || sobrenome.Length > 50, "O sobrenome deve ter entre 3 e 50 caracteres");
            DomainExceptionValidations.When(numeroTelefone.Length != 11, "O telefone deve ter 11 caracteres");
            DomainExceptionValidations.When(endereco.Length < 3 || endereco.Length > 100, "A localização deve ter entre 3 e 100 caracteres");
            DomainExceptionValidations.When(email.Length < 15 || email.Length > 80, "O email deve ter entre 15 e 80 caracteres");

            DomainExceptionValidations.When(!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"), "O email fornecido é inválido");
            DomainExceptionValidations.When(!Regex.IsMatch(numeroTelefone, @"^\d+$"), "O número de telefone deve conter apenas números.");

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