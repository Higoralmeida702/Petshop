using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Petshop.Application.Common.Responses;
using Petshop.Application.Dto;
using Petshop.Application.Interfaces;
using Petshop.Domain.Interfaces;
using Petshop.Domain.Model;

namespace Petshop.Application.Services
{
    public class ConsultaService : IConsultaService
    {
        private readonly IConsultaRepository _repository;

        public ConsultaService(IConsultaRepository repository)
        {
            _repository = repository;
        }

        public async Task<Resposta<ConsultaDto>> AdicionarConsulta(ConsultaDto consultaDto)
        {
            try
            {
                Consulta consulta = new Consulta(
                    consultaDto.Exame,
                    consultaDto.StatusConsulta,
                    consultaDto.StatusExame,
                    consultaDto.AgendarDia,
                    consultaDto.Valor,
                    consultaDto.AnimalId
                );

                await _repository.AdicionarConsulta(consulta);
                return new Resposta<ConsultaDto> { Mensagem = "Consulta criada com sucesso", Status = true, Dados = consultaDto };
            }
            catch (Exception error)
            {
                return new Resposta<ConsultaDto> { Mensagem = error.Message, Status = false };
            }
        }

        public async Task<Resposta<ConsultaDto>> EditarConsulta(int id, ConsultaDto consultaDto)
        {
            try
            {
                var consulta = await _repository.ObterConsultaPorId(id);
                if (consulta == null)
                {
                    return new Resposta<ConsultaDto> { Mensagem = "Consulta não encontrada", Status = false };
                }

                consulta.Update(consultaDto.Exame, consultaDto.StatusConsulta, consultaDto.StatusExame, consultaDto.AgendarDia, consultaDto.Valor, consultaDto.AnimalId);
                var consultaAtualizada = await _repository.EditarConsulta(id, consulta);

                return new Resposta<ConsultaDto>
                {
                    Mensagem = "Consulta atualizado com sucesso",
                    Status = true,
                    Dados = consultaDto
                };
            }
            catch (Exception error)
            {
                return new Resposta<ConsultaDto> { Mensagem = $"Erro ao atualizar cliente: {error.Message}", Status = false };
            }
        }

        public async Task<Resposta<Consulta>> ExcluirConsulta(int id)
        {
            try
            {
                var consulta = await _repository.ExcluirConsulta(id);

                if (consulta == null)
                {
                    return new Resposta<Consulta> { Mensagem = "Consulta não encontrado", Status = false };
                }

                return new Resposta<Consulta> { Mensagem = "Cliente deletado com sucesso", Status = true, Dados = consulta };
            }
            catch (Exception ex)
            {
                return new Resposta<Consulta> { Mensagem = $"Erro ao deletar cliente: {ex.Message}", Status = false };
            }
        }

        public async Task<Resposta<Consulta>> ObterConsultaPorId(int id)
        {
            try
            {
                var consulta = await _repository.ObterConsultaPorId(id);

                if (consulta == null)
                {
                    return new Resposta<Consulta> { Mensagem = "Consulta não encontrado", Status = false };
                }

                return new Resposta<Consulta> { Mensagem = "Consulta encontrado com sucesso", Status = true, Dados = consulta };
            }
            catch (Exception error)
            {
                return new Resposta<Consulta> { Mensagem = $"Erro ao obter Consulta {error.Message}", Status = false };
            }
        }

        public async Task<List<Consulta>> ObterTodasConsultas()
        {
            return await _repository.ObterTodasConsultas();
        }

    }
}