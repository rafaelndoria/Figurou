namespace Figurou.Business.DTOs
{
    public class LoginUsuarioDTO
    {
        public LoginUsuarioDTO(string username, string senha)
        {
            Username = username;
            Senha = senha;
        }

        public string Username { get; private set; }
        public string Senha { get; private set; }
    }
}
