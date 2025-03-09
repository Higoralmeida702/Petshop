using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Petshop.Domain.Enum.Status;

namespace Petshop.Application.Dto
{
    public class ConsultaDto
    {
        [Required(ErrorMessage = "É obrigatório preencher o campo informando o tipo de exame")]
        public ExameSerRealizadaEnum Exame { get; set; }

        [Required(ErrorMessage = "É obrigatório informar o status da consulta")]
        public StatusConsultaEnum StatusConsulta { get; set; } = StatusConsultaEnum.EmAndamento;

        [Required(ErrorMessage = "É obrigatório informar o status do exame")]
        public StatusExameEnum StatusExame { get; set; } = StatusExameEnum.Emcoleta;

        [Required(ErrorMessage = "É obrigatório agendar o dia do exame")]
        public DateTime AgendarDia { get; set; }

        [Required(ErrorMessage = "É obrigatório preencher o campo informando o valor da consulta")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "É necessario informar o id do animal para prosseguir")]
        public int AnimalId { get; set; }
    }
}