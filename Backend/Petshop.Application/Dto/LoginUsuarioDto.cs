using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Petshop.Application.Dto
{
    public class LoginUsuarioDto
    {
        [Required(ErrorMessage = "É obrigatório preencher o campo email")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido.")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "É necessário preencher o campo senha")]
        public string Senha { get; set; }
    }
}