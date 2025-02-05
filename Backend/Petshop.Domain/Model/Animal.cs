using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Petshop.Domain.Validations;

namespace Petshop.Domain.Model
{
    public class Animal
    {

        [Key]
        public int Id { get; private set; }

        [Required(ErrorMessage = "É obrigatório preencher o campo Nome")]
        [StringLength(50), MinLength(2)]
        public string Nome { get; private set; }

        [Required (ErrorMessage = "É obrigatório preencher o campo cor")]
        [StringLength(50), MinLength(3)]
        public string Cor { get; private set; }

        [Required (ErrorMessage = "É obrigatório preencher o campo Raça")]
        [StringLength(50), MinLength(3)]
        public string Raca { get; private set; }

        [Required (ErrorMessage = "É obrigatório preencher o campo peso")]
        [Range(1, double.MaxValue, ErrorMessage = "O peso deve ser maior que zero.")]
        public decimal Peso { get; private set; }

        [Required (ErrorMessage = "É obrigatório preencher o campo comprimento")]
        [Range(1, double.MaxValue, ErrorMessage = "O peso deve ser maior que zero.")]
        public decimal Comprimento { get; private set; }

        public DateTime DataCriacao { get; private set; } = DateTime.Now;
        public DateTime AtualizacaoDeInformacoes { get; private set; } = DateTime.Now;

        public Usuario Usuario { get; private set; }

        [Required (ErrorMessage = "É obrigatório preencher o campo usuario Id ")]
        public int UsuarioId { get; private set; }


        public Animal(string nome, string cor, string raca, decimal peso, decimal comprimento, int usuarioId)
        {
            DomainExceptionValidations.When(nome.Length < 2 || nome.Length > 50, "O nome deve ter entre 3 e 30 caracteres");
            DomainExceptionValidations.When(cor.Length < 3 || cor.Length > 50, "A cor do animal deve ter entre 3 e 50 caracteres");
            DomainExceptionValidations.When(raca.Length < 3 || raca.Length > 50, "O nome da raça ter entre 3 e 30 caracteres");
            DomainExceptionValidations.When(peso <= 1, "O animal deve pesar mais que 1kg");
            DomainExceptionValidations.When(comprimento <= 1, "o comprimento do animal não pode ser menor que 1cm");
            DomainExceptionValidations.When(usuarioId <= 1, "O id do usuário deve ser valido");

            ValidateDomain(nome, cor, raca, peso, comprimento, usuarioId);
        }

        public void ValidateDomain(string nome, string cor, string raca, decimal peso, decimal comprimento, int usuarioId)
        {
            Nome = nome;
            Cor = cor;
            Raca = raca;
            Peso = peso;
            Comprimento = comprimento;
            UsuarioId = usuarioId;
        }

        public void Update(string nome, string cor, string raca, decimal peso, decimal comprimento, int usuarioId)
        {
            ValidateDomain(nome, cor, raca, peso, comprimento, usuarioId);
        }
    }
}