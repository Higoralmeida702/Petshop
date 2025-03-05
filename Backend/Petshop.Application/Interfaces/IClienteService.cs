using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Petshop.Application.Common.Responses;
using Petshop.Application.Dto;
using Petshop.Domain.Model;

namespace Petshop.Application.Interfaces.Auth
{
    public interface IClienteService
    {
        Task<Resposta<RegistrarClienteDto>> Registrar(RegistrarClienteDto registrarDto);
        Task<List<Cliente>> ObterTodosClientes();
        Task<Resposta<Cliente>> DeletarCliente(int id);
        Task<Resposta<Cliente>> ObterPorId (int id);
        Task<Resposta<RegistrarClienteDto>> EditarCliente (int id, RegistrarClienteDto clienteDto);

    }
}