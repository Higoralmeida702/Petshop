using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Petshop.Application.Common.Responses;
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
                animalDto.ClienteId,
                animalDto.AnimalCategoria,
                animalDto.Genero
            );

            return await _repository.AdicionarPet(animal);
        }

        public async Task<Resposta<Animal>> DeletarAnimal(int id)
        {
            var animal = await _repository.DeletarAnimal(id);
            if (animal == null)
            {
                return new Resposta<Animal>
                {
                    Dados = null,
                    Mensagem = "Animal não encontrado.",
                    Status = false
                };
            }

            return new Resposta<Animal>
            {
                Dados = animal,
                Mensagem = "Animal deletado com sucesso.",
                Status = true
            };
        }


        public async Task<List<Animal>> ObterTodosAnimais()
        {
            return await _repository.ObterTodosAnimais();
        }

    }
}