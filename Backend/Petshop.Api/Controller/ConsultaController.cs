using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Petshop.Application.Common.Responses;
using Petshop.Application.Dto;
using Petshop.Application.Interfaces;

namespace Petshop.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConsultaController : ControllerBase
    {
        private readonly IConsultaService _consultaService;

        public ConsultaController(IConsultaService consultaService)
        {
            _consultaService = consultaService;
        }

        [HttpPost("AdicionarConsulta")]
        public async Task<IActionResult> AdicionarConsulta(ConsultaDto consultaDto)
        {
            var resposta = await _consultaService.AdicionarConsulta(consultaDto);
            if (!resposta.Status)
            {
                return BadRequest(new { mensagem = "Erro ao adicionar consulta" });
            }
            return Ok(resposta);
        }

        [HttpGet("ObterConsultaPorId/{id}")]
        public async Task<IActionResult> ObterConsultaPorId(int id)
        {
            var resposta = await _consultaService.ObterConsultaPorId(id);

            if (!resposta.Status)
            {
                return NotFound(new { mensagem = resposta.Mensagem });
            }

            return Ok(resposta);
        }

        [HttpGet("ObterTodasConsultas")]
        public async Task<IActionResult> ObterTodosClientes()
        {
            try
            {
                var consultas = await _consultaService.ObterTodasConsultas();

                if (consultas == null || consultas.Count == 0)
                {
                    return NotFound(new { mensagem = "Nenhum consultas encontrado." });
                }

                return Ok(consultas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = $"Erro interno: {ex.Message}" });
            }
        }

        [HttpDelete("ExcluirConsulta/{id}")]
        public async Task<IActionResult> ExcluirConsulta(int id)
        {
            var resposta = await _consultaService.ExcluirConsulta(id);

            if (!resposta.Status)
            {
                return NotFound(new { mensagem = resposta.Mensagem });
            }

            return Ok(new { mensagem = "Cliente deletado com sucesso.", cliente = resposta.Dados });
        }

        [HttpPut("EditarConsulta/{id}")]
        public async Task<IActionResult> EditarConsulta(int id, [FromBody] ConsultaDto consultaDto)
        {
            var resposta = await _consultaService.EditarConsulta(id, consultaDto);
            if (resposta.Status)
                return Ok(resposta);
            return BadRequest(resposta);
        }
    }
}