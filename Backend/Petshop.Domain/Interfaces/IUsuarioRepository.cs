using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Petshop.Domain.Model;

namespace Petshop.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task AdicionarUsuarioAsync(Usuario usuario);
        Task<Usuario> ObterEmailAsync (string email);
    }
}