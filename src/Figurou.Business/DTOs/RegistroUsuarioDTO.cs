namespace Figurou.Business.DTOs
{
    public class CadastrarUsuarioDTO
    {
        public CadastrarUsuarioDTO(string username, string email, string senha)
        {
            Username = username;
            Email = email;
            Senha = senha;
        }

        public string Username { get; private set; }
        public string Email { get; private set; }
        public string Senha { get; private set; }
    }
}
