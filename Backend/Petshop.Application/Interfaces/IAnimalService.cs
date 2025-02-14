using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Petshop.Application.Common.Responses;
using Petshop.Application.Dto;
using Petshop.Domain.Model;

namespace Petshop.Application.Interfaces
{
    public interface IAnimalService
    {
        Task<Animal> AdicionarPet(AnimalDto animalDto);
        Task<List<Animal>> ObterTodosAnimais ();
        Task<Resposta<Animal>> DeletarAnimal (int id);
    }
}