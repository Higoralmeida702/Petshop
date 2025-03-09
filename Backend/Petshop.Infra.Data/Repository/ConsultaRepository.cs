using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Petshop.Domain.Interfaces;
using Petshop.Domain.Model;
using Petshop.Infra.Data.Data;

namespace Petshop.Infra.Data.Repository
{
    public class ConsultaRepository : IConsultaRepository
    {
        private readonly ApplicationDbContext _context;

        public ConsultaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarConsulta(Consulta consulta)
        {
            await _context.Consultas.AddAsync(consulta);
            await _context.SaveChangesAsync();
        }

        public async Task<Consulta> EditarConsulta(int id, Consulta consulta)
        {
            var consultas = await _context.Consultas.FindAsync(id);
            if (consulta == null)
            {
                return null;
            }

            consultas.Update(consulta.Exame, consulta.StatusConsulta, consulta.StatusExame, consulta.AgendarDia, consulta.Valor, consulta.AnimalId);
            _context.Consultas.Update(consultas);
            await _context.SaveChangesAsync();
            return consultas;

        }

        public async Task<Consulta> ExcluirConsulta(int id)
        {
            var consulta = await _context.Consultas.FindAsync(id);
            if (consulta == null)
            {
                return null;
            }

            _context.Consultas.Remove(consulta);
            await _context.SaveChangesAsync();
            return consulta;

        }

        public async Task<Consulta> ObterConsultaPorId(int id)
        {
            return await _context.Consultas.FindAsync(id);
        }

        public async Task<List<Consulta>> ObterTodasConsultas()
        {
            return await _context.Consultas.ToListAsync();
        }


    }
}