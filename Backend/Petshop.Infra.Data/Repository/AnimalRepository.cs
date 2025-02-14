using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Petshop.Domain.Model;
using Petshop.Domain.Repository;
using Petshop.Infra.Data.Data;

namespace Petshop.Infra.Data.Repository
{
    public class AnimalRepository : IAnimalRepository
    {

        private readonly ApplicationDbContext _context;

        public AnimalRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Animal> AdicionarPet(Animal animal)
        {
            await _context.Animais.AddAsync(animal);
            await _context.SaveChangesAsync();
            return animal;
        }

        public async Task<Animal> DeletarAnimal(int id)
        {
            var animal = await _context.Animais.FindAsync(id);
            if (animal == null)
            {
                return null;
            }

            _context.Animais.Remove(animal);
            await _context.SaveChangesAsync();

            return animal;
        }


        public async Task<List<Animal>> ObterTodosAnimais()
        {
            return await _context.Animais.ToListAsync();
        }

    }
}