using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Petshop.Application.Dto;
using Petshop.Application.Interfaces.Auth;
using Petshop.Application.Common.Responses;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
namespace Petshop.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpPost("Registrar/Cliente")]
        public async Task<IActionResult> Registrar([FromBody] RegistrarClienteDto registrarDto)
        {
            var resposta = await _clienteService.Registrar(registrarDto);
            if (!resposta.Status)
            {
                return BadRequest(new { mensagem = "Erro ao registrar cliente!" });
            }
            return Ok(resposta);
        }


        [HttpGet("BuscarClientePor/{id}")]
        public async Task<IActionResult> ObterClientePorId(int id)
        {
            var resposta = await _clienteService.ObterPorId(id);

            if (!resposta.Status)
            {
                return NotFound(new { mensagem = resposta.Mensagem });
            }

            return Ok(resposta);
        }

        [HttpGet("ObterTodosClientes")]
        public async Task<IActionResult> ObterTodosClientes()
        {
            try
            {
                var clientes = await _clienteService.ObterTodosClientes();

                if (clientes == null || clientes.Count == 0)
                {
                    return NotFound(new { mensagem = "Nenhum cliente encontrado." });
                }

                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = $"Erro interno: {ex.Message}" });
            }
        }

        [HttpDelete("DeletarCliente/{id}")]
        public async Task<IActionResult> DeletarCliente(int id)
        {
            var resposta = await _clienteService.DeletarCliente(id);

            if (!resposta.Status)
            {
                return NotFound(new { mensagem = resposta.Mensagem });
            }

            return Ok(new { mensagem = "Cliente deletado com sucesso.", cliente = resposta.Dados });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditarCliente(int id, [FromBody] RegistrarClienteDto clienteDto)
        {
            var resposta = await _clienteService.EditarCliente(id, clienteDto);
            if (resposta.Status)
                return Ok(resposta);
            return BadRequest(resposta);
        }
    }
}