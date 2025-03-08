using System.ComponentModel.DataAnnotations;

namespace Petshop.Domain.Enum.Status
{
    public enum StatusExameEnum
    {
        [Display(Name = "Em coleta")]
        Emcoleta = 1,

        [Display(Name = "Em espera")]
        EmEspera = 2,

        [Display(Name = "Exame realizado")]
        Realizado = 3,

    }
}