using System;
using System.Threading.Tasks;
using Petshop.Application.Common.Responses;
using Petshop.Application.Dto;
using Petshop.Application.Interfaces.Auth;
using Petshop.Domain.Interfaces;
using Petshop.Domain.Model;

namespace Petshop.Application.Services.Auth
{
    public class UsuarioAuthService : IUsuarioAuthService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ISenhaService _senhaService;

        public UsuarioAuthService(IUsuarioRepository usuarioRepository, ISenhaService senhaService)
        {
            _usuarioRepository = usuarioRepository;
            _senhaService = senhaService;
        }

        public async Task<Resposta<string>> Login(LoginUsuarioDto loginDto)
        {
            Resposta<string> respostaServico = new Resposta<string>();

            try
            {
                var usuario = await _usuarioRepository.ObterEmailAsync(loginDto.Email);
                if (usuario == null)
                {
                    respostaServico.Mensagem = "Credenciais inválidas!";
                    respostaServico.Status = false;
                    return respostaServico;
                }

                if (!_senhaService.VerificarSenhaHash(loginDto.Senha, usuario.PasswordHash, usuario.PasswordSalt))
                {
                    respostaServico.Mensagem = "Credenciais inválidas!";
                    respostaServico.Status = false;
                    return respostaServico;
                }

                var token = _senhaService.CriarToken(usuario);
                respostaServico.Dados = token;
                respostaServico.Mensagem = "Usuário logado com sucesso!";
                respostaServico.Status = true;
            }
            catch (Exception ex)
            {
                respostaServico.Dados = null;
                respostaServico.Mensagem = $"Erro ao processar login: {ex.Message}";
                respostaServico.Status = false;
            }

            return respostaServico;
        }

        public async Task<Resposta<RegistrarUsuarioDto>> Registrar(RegistrarUsuarioDto registrarDto)
        {
            Resposta<RegistrarUsuarioDto> respostaServico = new Resposta<RegistrarUsuarioDto>();
            try
            {
                _senhaService.CriarSenhaHash(registrarDto.Senha, out byte[] senhaHash, out byte[] senhaSalt);

                Usuario usuario = new Usuario(
                    registrarDto.Nome,
                    registrarDto.Sobrenome,
                    registrarDto.NumeroTelefone,
                    registrarDto.Endereco,
                    registrarDto.Email
                );
                usuario.DefinirSenha(senhaHash, senhaSalt);

                await _usuarioRepository.AdicionarUsuarioAsync(usuario);
                respostaServico.Mensagem = "Usuário criado com sucesso";
                respostaServico.Status = true;
                respostaServico.Dados = registrarDto;
                return respostaServico;
            }
            catch (Exception error)
            {
                respostaServico.Dados = null;
                respostaServico.Mensagem = error.Message;
                respostaServico.Status = false;
            }
            return respostaServico;
        }
        
        public async Task<bool> VerificaEmailJaCadastrado(RegistrarUsuarioDto registrarDto)
        {
            var usuario = await _usuarioRepository.ObterEmailAsync(registrarDto.Email);
            return usuario != null;
        }
    }
}
