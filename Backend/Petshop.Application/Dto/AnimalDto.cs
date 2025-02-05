using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Petshop.Application.Dto
{
    public class AnimalDto
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(50, ErrorMessage = "O nome deve ter no máximo 50 caracteres."), MinLength(3, ErrorMessage = "O nome deve ter no mínimo 3 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A cor é obrigatória.")]
        [StringLength(50, ErrorMessage = "A cor deve ter no máximo 50 caracteres."), MinLength(3, ErrorMessage = "A cor deve ter no mínimo 3 caracteres.")]
        public string Cor { get; set; }

        [Required(ErrorMessage = "A raça é obrigatória.")]
        [StringLength(50, ErrorMessage = "A raça deve ter no máximo 50 caracteres."), MinLength(3, ErrorMessage = "A raça deve ter no mínimo 3 caracteres.")]
        public string Raca { get; set; }

        [Required(ErrorMessage = "O peso é obrigatório.")]
        [Range(1, double.MaxValue, ErrorMessage = "O peso deve ser maior que zero.")]
        public decimal Peso { get; set; }

        [Required(ErrorMessage = "O comprimento é obrigatório.")]
        [Range(1, double.MaxValue, ErrorMessage = "O comprimento deve ser maior que zero.")]
        public decimal Comprimento { get; set; }

        [Required(ErrorMessage = "O ID do usuário é obrigatório.")]
        public int UsuarioId { get; set; }
    }
}
