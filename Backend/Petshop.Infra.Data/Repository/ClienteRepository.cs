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
    public class ClienteRepository : IClienteRepository
    {
        private readonly ApplicationDbContext _context;

        public ClienteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarClienteAsync(Cliente cliente)
        {
            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task<Cliente> DeletarCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return null;
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }

        public async Task<Cliente> EditarCliente(int id, Cliente cliente)
        {
            var clienteExistente = await _context.Clientes.FindAsync(id);
            if (clienteExistente == null)
            {
                return null;
            }

            clienteExistente.Update(cliente.Nome, cliente.NumeroTelefone, cliente.Endereco );

            _context.Clientes.Update(clienteExistente);
            await _context.SaveChangesAsync();
            return clienteExistente;
        }

        public async Task<Cliente> ObterPorId(int id)
        {
            return await _context.Clientes.FindAsync(id);
        }

        public async Task<List<Cliente>> ObterTodosClientes()
        {
            return await _context.Clientes.ToListAsync();
        }


    }
}