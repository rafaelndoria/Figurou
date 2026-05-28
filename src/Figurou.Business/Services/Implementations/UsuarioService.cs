using Figurou.Business.Interfaces;
using Figurou.Business.Models;
using Figurou.Business.Services.Interfaces;
using Figurou.WebApp.InputModel;

namespace Figurou.Business.Services.Implementations
{
    public class UsuarioService : BaseService, IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IAuthService _authService;

        public UsuarioService(
            INotificador notificador,
            IUsuarioRepository usuarioRepository,
            IAuthService authService) : base(notificador)
        {
            _usuarioRepository = usuarioRepository;
            _authService = authService;
        }

        public async Task<Usuario?> Cadastrar(RegistroUsuarioInputModel registroUsuario)
        {
            var usuarioExistente = (await _usuarioRepository.BuscarAsync(
                x => x.Username == registroUsuario.Username
                || x.Email == registroUsuario.Email))
                .FirstOrDefault();

            if (usuarioExistente != null)
            {
                Notificar("Usuário já cadastrado.");
                return null;
            }

            var usuario = new Usuario(
                registroUsuario.Username,
                registroUsuario.Email,
                _authService.CriptografarSenha(registroUsuario.Senha));

            await _usuarioRepository.AdicionarAsync(usuario);

            return usuario;
        }

        public async Task<Usuario?> Login(LoginUsuarioInputModel loginUsuario)
        {
            var senhaCodificada = _authService.CriptografarSenha(loginUsuario.Senha);

            var usuario = (await _usuarioRepository.BuscarAsync(
                x => x.Username == loginUsuario.Username
                || x.Email == loginUsuario.Username))
                .FirstOrDefault();

            if (usuario is null)
            {
                Notificar("Usuário ou senha incorreto.");
                return null;
            }

            if (!usuario.Ativo)
            {
                Notificar("Usuário desativado.");
                return null;
            }

            if (usuario.SenhaCodificada != senhaCodificada)
            {
                Notificar("Usuário ou senha incorreto.");
                return null;
            }

            return usuario;
        }
    }
}