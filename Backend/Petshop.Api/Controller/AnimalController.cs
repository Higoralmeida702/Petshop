using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Petshop.Application.Dto;
using Petshop.Application.Interfaces;
using Petshop.Domain.Model;

namespace Petshop.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnimalController : ControllerBase
    {
        private readonly IAnimalService _animalService;

        public AnimalController(IAnimalService animalService)
        {
            _animalService = animalService;
        }

        [HttpPost]
        public async Task<ActionResult<Animal>> AdicionarPet([FromBody] AnimalDto animalDto)
        {
            if (animalDto == null)
            {
                return BadRequest("Dados do animal não foram fornecidos.");
            }

            var animal = await _animalService.AdicionarPet(animalDto);

            return Ok(animal);

        }

    }
}