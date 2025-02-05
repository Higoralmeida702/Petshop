using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Petshop.Domain.Model;

namespace Petshop.Domain.Repository
{
    public interface IAnimalRepository
    {
        Task<Animal> AdicionarPet (Animal animal); 
    }
}