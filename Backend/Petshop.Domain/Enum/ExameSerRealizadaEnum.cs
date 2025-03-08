using System.ComponentModel.DataAnnotations;

namespace Petshop.Domain.Enum.Status
{
    public enum ExameSerRealizadaEnum
    {
        [Display(Name = "Rotina")]
        Rotina = 1,

        [Display(Name = "Tosa")]
        Tosa = 2,

        [Display(Name = "Exame de sagune")]
        ExameDeSangue = 3,

        [Display(Name = "Exame de pele")]
        ExameDePele = 4,

        [Display(Name = "Exame de urina")]
        ExameDeUrina = 5,

        [Display(Name = "Exame de fezes")]
        ExameDeFezes = 6

    }
}