using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Petshop.Domain.Model;

namespace Petshop.Domain.Interfaces
{
    public interface IClienteRepository
    {
        Task AdicionarClienteAsync(Cliente cliente);
        Task<Cliente> ObterPorId (int id);
        Task<List<Cliente>> ObterTodosClientes();
        Task<Cliente> DeletarCliente (int id);
        Task<Cliente> EditarCliente (int id, Cliente cliente); 
    }
}