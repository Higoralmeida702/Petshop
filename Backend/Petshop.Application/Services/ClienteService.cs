using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Petshop.Application.Common.Responses;
using Petshop.Application.Dto;
using Petshop.Application.Interfaces.Auth;
using Petshop.Domain.Interfaces;
using Petshop.Domain.Model;

namespace Petshop.Application.Services.Auth
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<Resposta<RegistrarClienteDto>> Registrar(RegistrarClienteDto registrarDto)
        {
            try
            {

                Cliente cliente = new Cliente(
                    registrarDto.Nome,
                    registrarDto.NumeroTelefone,
                    registrarDto.Endereco
                );

                await _clienteRepository.AdicionarClienteAsync(cliente);
                return new Resposta<RegistrarClienteDto> { Mensagem = "Cliente criado com sucesso", Status = true, Dados = registrarDto };
            }
            catch (Exception ex)
            {
                return new Resposta<RegistrarClienteDto> { Mensagem = ex.Message, Status = false };
            }
        }

    }
}
