using System.ComponentModel.DataAnnotations;

namespace Figurou.WebApp.InputModels
{
    public class CadastroSelecaoInputModel
    {
        public Guid Id { get; set; }
        public Guid AlbumId { get; set; }

        [Required(ErrorMessage = "Informe o código da seleção.")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "O código deve possuir exatamente 3 caracteres.")]
        [Display(Name = "Código")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "Informe o nome da seleção.")]
        [StringLength(100, ErrorMessage = "O nome deve possuir no máximo 100 caracteres.")]
        [Display(Name = "Nome da Seleção")]
        public string Nome { get; set; }
    }
}