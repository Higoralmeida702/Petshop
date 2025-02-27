using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Petshop.Application.Common.Responses;
using Petshop.Application.Dto;

namespace Petshop.Application.Interfaces.Auth
{
    public interface IClienteService
    {
        Task<Resposta<RegistrarClienteDto>> Registrar(RegistrarClienteDto registrarDto);

    }
}