using System;
using System.ComponentModel;
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
            try
            {
                var usuario = await _usuarioRepository.ObterEmailAsync(loginDto.Email);
                if (usuario == null || !_senhaService.VerificarSenhaHash(loginDto.Senha, usuario.PasswordHash, usuario.PasswordSalt))
                {
                    return new Resposta<string> { Mensagem = "Credenciais inválidas!", Status = false };
                }

                var token = _senhaService.CriarToken(usuario);
                return new Resposta<string> { Dados = token, Mensagem = "Usuário logado com sucesso!", Status = true };
            }
            catch (Exception ex)
            {
                return new Resposta<string> { Mensagem = $"Erro ao processar login: {ex.Message}", Status = false };
            }
        }

        public async Task<Resposta<RegistrarUsuarioDto>> Registrar(RegistrarUsuarioDto registrarDto)
        {
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
                return new Resposta<RegistrarUsuarioDto> { Mensagem = "Usuário criado com sucesso", Status = true, Dados = registrarDto };
            }
            catch (Exception ex)
            {
                return new Resposta<RegistrarUsuarioDto> { Mensagem = ex.Message, Status = false };
            }
        }

        public async Task<bool> VerificaEmailJaCadastrado(RegistrarUsuarioDto registrarDto)
        {
            var usuario = await _usuarioRepository.ObterEmailAsync(registrarDto.Email);
            return usuario != null;
        }

        public async Task<Resposta<AtualizarInfoUsuarioDto>> AtualizarPerfil(int usuarioId, AtualizarInfoUsuarioDto infoUsuarioDto)
        {
            if (infoUsuarioDto == null)
            {
                return new Resposta<AtualizarInfoUsuarioDto> { Mensagem = "DTO inválido!", Status = false };
            }

            var usuarioExistente = await _usuarioRepository.ObterPorId(usuarioId);
            if (usuarioExistente == null)
            {
                return new Resposta<AtualizarInfoUsuarioDto> { Mensagem = "Usuário não encontrado!", Status = false };
            }

            try
            {
                usuarioExistente.Update(
                    infoUsuarioDto.Nome,
                    infoUsuarioDto.Sobrenome,
                    infoUsuarioDto.NumeroTelefone,
                    infoUsuarioDto.Endereco,
                    infoUsuarioDto.Email
                );

                await _usuarioRepository.AtualizarInformacoesDoUsuario(usuarioExistente);

                return new Resposta<AtualizarInfoUsuarioDto> { Mensagem = "Perfil atualizado com sucesso!", Status = true, Dados = infoUsuarioDto };
            }
            catch (Exception ex)
            {
                return new Resposta<AtualizarInfoUsuarioDto> { Mensagem = $"Erro ao atualizar perfil: {ex.Message}", Status = false };
            }
        }

        public async Task<Resposta<AtualizarInfoUsuarioDto>> ObterPerfil(int usuarioId)
        {
            var usuario = await _usuarioRepository.ObterPorId(usuarioId);
            if (usuario == null)
            {
                return new Resposta<AtualizarInfoUsuarioDto>
                {
                    Mensagem = "Usuário não encontrado!",
                    Status = false
                };
            }

            var perfilDto = new AtualizarInfoUsuarioDto
            {
                Nome = usuario.Nome,
                Sobrenome = usuario.Sobrenome,
                NumeroTelefone = usuario.NumeroTelefone,
                Endereco = usuario.Endereco,
                Email = usuario.Email
            };

            return new Resposta<AtualizarInfoUsuarioDto>
            {
                Dados = perfilDto,
                Mensagem = "Perfil obtido com sucesso!",
                Status = true
            };
        }
    }
}
