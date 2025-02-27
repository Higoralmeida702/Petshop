using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Petshop.Domain.Validations;

namespace Petshop.Domain.Model
{
    public class Cliente
    {
        [Key]
        public int Id { get; private set; }

        [Required(ErrorMessage = "É obrigatório o preenchimento do campo nome")]
        [StringLength(30), MinLength(3)]
        public string Nome { get; private set; }

        [Required(ErrorMessage = "É obrigatório o preenchimento do campo telefone")]
        [StringLength(11), MinLength(11)]
        public string NumeroTelefone { get; private set; }

        [Required(ErrorMessage = "É obrigatório o preenchimento do campo endereço")]
        [StringLength(100), MinLength(3)]
        public string Endereco { get; private set; }

        public DateTime CriacaoConta { get; private set; } = DateTime.Now;

        public DateTime AtualizacaoDeInformacoes { get; private set; } = DateTime.Now;

        public ICollection<Animal> Animais { get; set; } = new List<Animal>();

        public Cliente(string nome, string numeroTelefone, string endereco)
        {
            ValidateDomain(nome, numeroTelefone, endereco);
        }

        public void ValidateDomain(string nome, string numeroTelefone, string endereco)
        {

            DomainExceptionValidations.When(nome.Length < 3 || nome.Length > 30, "O nome deve ter entre 3 e 30 caracteres");
            DomainExceptionValidations.When(numeroTelefone.Length != 11, "O telefone deve ter 11 caracteres");
            DomainExceptionValidations.When(endereco.Length < 3 || endereco.Length > 100, "A localização deve ter entre 3 e 100 caracteres");

            DomainExceptionValidations.When(!Regex.IsMatch(numeroTelefone, @"^\d+$"), "O número de telefone deve conter apenas números.");

            Nome = nome;
            NumeroTelefone = numeroTelefone;
            Endereco = endereco;
        }

        public void Update(string nome, string numeroTelefone, string endereco)
        {
            ValidateDomain(nome, numeroTelefone, endereco);
            AtualizacaoDeInformacoes = DateTime.Now;
        }

    }
}