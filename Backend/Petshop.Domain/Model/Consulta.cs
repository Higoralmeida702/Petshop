using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Petshop.Domain.Enum.Status;

namespace Petshop.Domain.Model
{
    public class Consulta
    {

        [Key]
        public int Id { get; private set; }

        [Required(ErrorMessage = "É obrigatório preencher o campo informando o tipo de exame")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ExameSerRealizadaEnum Exame { get; private set; }

        [Required(ErrorMessage = "É obrigatório informar o status da consulta")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public StatusConsultaEnum StatusConsulta { get; private set; } = StatusConsultaEnum.EmAndamento;

        [Required(ErrorMessage = "É obrigatório informar o status do exame")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public StatusExameEnum StatusExame { get; private set; } = StatusExameEnum.Emcoleta;

        [Required(ErrorMessage = "É obrigatório agendar o dia do exame")]
        public DateTime AgendarDia { get; private set; }

        [Required(ErrorMessage = "É obrigatório preencher o campo informando o valor da consulta")]
        [Range(1, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero.")]                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       
        public decimal Valor { get; private set; }

        public DateTime CriacaoConsulta { get; private set; } = DateTime.Now;

        [Required(ErrorMessage = "É necessario informar o id do animal para prosseguir")]
        public int AnimalId { get; private set; }
        public Animal Animal { get; private set; }

        public Consulta(ExameSerRealizadaEnum exame, StatusConsultaEnum statusConsulta, StatusExameEnum statusExame, DateTime agendarDia, decimal valor, int animalId)
        {
            ValidateDomain(exame, statusConsulta, statusExame, agendarDia, valor, animalId);
        }

        public void ValidateDomain(ExameSerRealizadaEnum exame, StatusConsultaEnum statusConsulta, StatusExameEnum statusExame, DateTime agendarDia, decimal valor, int animalId)
        {
            Exame = exame;
            StatusConsulta = statusConsulta;
            StatusExame = statusExame;
            AgendarDia = agendarDia;
            Valor = valor;
            AnimalId = animalId;
        }

        public void Update(ExameSerRealizadaEnum exame, StatusConsultaEnum statusConsulta, StatusExameEnum statusExame, DateTime agendarDia, decimal valor, int animalId)
        {
            ValidateDomain(exame, statusConsulta, statusExame, agendarDia, valor, animalId);
        }
    }
} 