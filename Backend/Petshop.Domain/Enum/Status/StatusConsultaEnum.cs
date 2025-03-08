using System.ComponentModel.DataAnnotations;

namespace Petshop.Domain.Enum.Status
{
    public enum StatusConsultaEnum
    {
        [Display(Name = "Em andamento")]
        EmAndamento = 1,

        [Display(Name = "Consulta finalizada")]
        ConsultaFinalizada = 2
    }
}