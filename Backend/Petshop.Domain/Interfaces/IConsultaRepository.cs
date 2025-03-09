using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Petshop.Domain.Model;

namespace Petshop.Domain.Interfaces
{
    public interface IConsultaRepository
    {
        Task AdicionarConsulta (Consulta consulta);
        Task<Consulta> ExcluirConsulta(int id);
        Task<List<Consulta>> ObterTodasConsultas();
        Task<Consulta> ObterConsultaPorId(int id);
        Task<Consulta> EditarConsulta(int id, Consulta consulta);
    }
}