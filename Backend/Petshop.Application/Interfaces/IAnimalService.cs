using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Petshop.Application.Dto;
using Petshop.Domain.Model;

namespace Petshop.Application.Interfaces
{
    public interface IAnimalService
    {
        Task<Animal> AdicionarPet(AnimalDto animalDto);

    }
}