using System.ComponentModel.DataAnnotations;

namespace Petshop.Domain.Enum
{
    public enum GeneroEnum
    {
        [Display(Name = "Masculino")]
        Macho = 1,

        [Display(Name = "Fêmea")]
        Femea = 2,
    }
}