using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Petshop.Application.Dto;
using Petshop.Application.Interfaces;
using Petshop.Domain.Model;
using Petshop.Domain.Repository;

namespace Petshop.Application.Services
{
    public class AnimalService : IAnimalService
    {

        private readonly IAnimalRepository _repository;

        public AnimalService(IAnimalRepository repository)
        {
            _repository = repository;
        }

        public async Task<Animal> AdicionarPet(AnimalDto animalDto)
        {
            var animal = new Animal
            (
                animalDto.Nome,
                animalDto.Cor,
                animalDto.Raca,
                animalDto.Peso,
                animalDto.Comprimento,
                animalDto.UsuarioId
            );

            return await _repository.AdicionarPet(animal);
        }
    }
}