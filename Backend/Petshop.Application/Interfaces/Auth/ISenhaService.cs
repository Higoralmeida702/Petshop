using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petshop.Application.Interfaces.Auth
{
    public interface ISenhaService
    {
        void CriarSenhaHash(string senha, out byte[] senhaHash, out byte[] senhaSalt);
        bool VerificarSenhaHash(string senha, byte[] senhaHash, byte[] senhaSalt);
        string CriarToken<T>(T usuario) where T : class;
    }
}