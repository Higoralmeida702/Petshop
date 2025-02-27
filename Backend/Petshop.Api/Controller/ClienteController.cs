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

    }
}
