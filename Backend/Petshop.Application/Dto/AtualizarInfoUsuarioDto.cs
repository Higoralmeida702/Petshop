using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Petshop.Application.Dto
{
    public class AtualizarInfoUsuarioDto
    {
        [Required(ErrorMessage = "É obrigatório o preenchimento do campo nome")]
        [StringLength(30, ErrorMessage = "O nome deve ter no máximo 30 caracteres."), MinLength(3, ErrorMessage = "O nome deve ter no mínimo 3 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "É obrigatório preencher o campo sobrenome")]
        [StringLength(50, ErrorMessage = "O sobrenome deve ter no máximo 50 caracteres."), MinLength(3, ErrorMessage = "O sobrenome deve ter no mínimo 3 caracteres.")]
        public string Sobrenome { get; set; }

        [Required(ErrorMessage = "É obrigatório o preenchimento do campo número de telefone")]
        [StringLength(11, ErrorMessage = "O número de telefone deve ter exatamente 11 dígitos."), MinLength(11, ErrorMessage = "O número de telefone deve ter exatamente 11 dígitos.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "O número de telefone deve ter 11 dígitos.")]
        public string NumeroTelefone { get; set; }

        [Required(ErrorMessage = "É obrigatório preencher o campo endereço")]
        [StringLength(100, ErrorMessage = "O endereço deve ter no máximo 100 caracteres."), MinLength(3, ErrorMessage = "O endereço deve ter no mínimo 3 caracteres.")]
        public string Endereco { get; set; }

        [Required(ErrorMessage = "É obrigatório preencher o campo email")]
        [StringLength(80, ErrorMessage = "O e-mail deve ter no máximo 80 caracteres."), MinLength(15, ErrorMessage = "O e-mail deve ter no mínimo 15 caracteres.")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido.")]
        public string Email { get; set; }

    }
}