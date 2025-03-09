using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Petshop.Application.Common.Responses;
using Petshop.Application.Dto;
using Petshop.Domain.Model;

namespace Petshop.Application.Interfaces
{
    public interface IConsultaService
    {
        Task<Resposta<ConsultaDto>> AdicionarConsulta(ConsultaDto consultaDto);
        Task<Resposta<Consulta>> ExcluirConsulta(int id);
        Task<List<Consulta>> ObterTodasConsultas();
        Task<Resposta<Consulta>> ObterConsultaPorId(int id);
        Task<Resposta<ConsultaDto>> EditarConsulta(int id, ConsultaDto consultaDto);
    }
}