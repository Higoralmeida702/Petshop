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
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioAuthService _usuarioAuthService;

        public UsuarioController(IUsuarioAuthService usuarioAuthService)
        {
            _usuarioAuthService = usuarioAuthService;
        }

        [HttpPost("Registrar/Usuario")]
        public async Task<IActionResult> Registrar([FromBody] RegistrarUsuarioDto usuarioDto)
        {
            var resposta = await _usuarioAuthService.Registrar(usuarioDto);
            if (!resposta.Status)
            {
                return BadRequest(new { mensagem = "Erro ao registrar usuário!" });
            }
            return Ok(resposta);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUsuarioDto loginDto)
        {
            var resposta = await _usuarioAuthService.Login(loginDto);

            if (!resposta.Status)
            {
                return BadRequest(resposta);
            }
            return Ok(resposta);
        }


        [Authorize]
        [HttpPut("atualizar-perfil")]
        public async Task<IActionResult> AtualizarPerfil([FromBody] AtualizarInfoUsuarioDto infoUsuarioDto)
        {
            var usuarioIdClaim = User.FindFirst("id")?.Value;
            if (string.IsNullOrEmpty(usuarioIdClaim))
                return Unauthorized(new { Mensagem = "Usuário não autenticado!" });

            int usuarioId = int.Parse(usuarioIdClaim);
            var resposta = await _usuarioAuthService.AtualizarPerfil(usuarioId, infoUsuarioDto);

            if (!resposta.Status)
                return BadRequest(resposta);

            return Ok(resposta);
        }

        [Authorize]
        [HttpGet("obter-perfil")]
        public async Task<IActionResult> ObterPerfil()
        {
            var usuarioIdClaim = User.FindFirst("id")?.Value;
            if (string.IsNullOrEmpty(usuarioIdClaim))
                return Unauthorized(new { Mensagem = "Usuário não autenticado!" });

            int usuarioId = int.Parse(usuarioIdClaim);
            var resposta = await _usuarioAuthService.ObterPerfil(usuarioId);

            if (!resposta.Status)
                return BadRequest(resposta);

            return Ok(resposta);
        }
    }
}
