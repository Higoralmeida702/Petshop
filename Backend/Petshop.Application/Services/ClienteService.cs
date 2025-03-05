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

        public async Task<Resposta<Cliente>> DeletarCliente(int id)
        {
            try
            {
                var cliente = await _clienteRepository.DeletarCliente(id);

                if (cliente == null)
                {
                    return new Resposta<Cliente> { Mensagem = "Cliente não encontrado", Status = false };
                }

                return new Resposta<Cliente> { Mensagem = "Cliente deletado com sucesso", Status = true, Dados = cliente };
            }
            catch (Exception ex)
            {
                return new Resposta<Cliente> { Mensagem = $"Erro ao deletar cliente: {ex.Message}", Status = false };
            }
        }


        public async Task<Resposta<Cliente>> ObterPorId(int id)
        {
            try
            {
                var cliente = await _clienteRepository.ObterPorId(id);

                if (cliente == null)
                {
                    return new Resposta<Cliente> { Mensagem = "Cliente não encontrado", Status = false };
                }

                return new Resposta<Cliente> { Mensagem = "Cliente encontrado com sucesso", Status = true, Dados = cliente };
            }
            catch (Exception error)
            {
                return new Resposta<Cliente> { Mensagem = $"Erro ao obter cliente {error.Message}", Status = false };
            }
        }

        public async Task<List<Cliente>> ObterTodosClientes()
        {
            return await _clienteRepository.ObterTodosClientes();
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

        public async Task<Resposta<RegistrarClienteDto>> EditarCliente(int id, RegistrarClienteDto clienteDto)
        {
            try
            {
                var clienteExiste = await _clienteRepository.ObterPorId(id);

                if (clienteExiste == null)
                {
                    return new Resposta<RegistrarClienteDto> { Mensagem = "Cliente não encontrado", Status = false };
                }

                clienteExiste.Update(clienteDto.Nome, clienteDto.NumeroTelefone, clienteDto.Endereco);

                var clienteAtualizado = await _clienteRepository.EditarCliente(id, clienteExiste);

                return new Resposta<RegistrarClienteDto>
                {
                    Mensagem = "Cliente atualizado com sucesso",
                    Status = true,
                    Dados = clienteDto
                };
            }
            catch (Exception error)
            {
                return new Resposta<RegistrarClienteDto> { Mensagem = $"Erro ao atualizar cliente: {error.Message}", Status = false };
            }
        }

    }
}
