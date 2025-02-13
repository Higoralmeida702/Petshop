using System.ComponentModel.DataAnnotations;

namespace Petshop.Domain.Enum
{
    public enum AnimalCategoriaEnum
    {
        [Display(Name = "Cachorro")]
        Cachorro = 1,

        [Display(Name = "Gato")]
        Gato = 2,

        [Display(Name = "Répteis")]
        Repteis = 3,

        [Display(Name = "Ave")]
        Ave = 4
    }
}