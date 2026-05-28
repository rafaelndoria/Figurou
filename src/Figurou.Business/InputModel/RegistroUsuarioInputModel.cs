using System.ComponentModel.DataAnnotations;

namespace Figurou.WebApp.InputModel
{
    public class RegistroUsuarioInputModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(50, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.", MinimumLength = 3)]
        public string Username { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(50, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.", MinimumLength = 3)]
        [EmailAddress(ErrorMessage = "O campo {0} esta inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [MinLength(6, ErrorMessage = "O campo {0} precisa ter no mínimo {1} caracteres.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z0-9]).+$", ErrorMessage = "O campo {0} precisa conter pelo menos uma letra maiúscula, um número e um caractere especial.")]
        public string Senha { get; set; }

        [Compare("Senha", ErrorMessage = "As senhas não são iguais.")]
        public string ConfirmarSenha { get; set; }
    }
}
