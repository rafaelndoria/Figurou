using Figurou.Business.Enums;

using System.ComponentModel.DataAnnotations;

namespace Figurou.WebApp.InputModels
{
    public class CadastroFigurinhaInputModel
    {
        [Required]
        public Guid PaginaId { get; set; }

        [Required]
        public Guid AlbumId { get; set; }

        [Required(ErrorMessage = "Informe o código da figurinha.")]
        [StringLength(20, ErrorMessage = "O código deve possuir no máximo 20 caracteres.")]
        [Display(Name = "Código")]
        public string Codigo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Informe o número da figurinha.")]
        [Range(1, 99999, ErrorMessage = "O número da figurinha deve ser maior que zero.")]
        [Display(Name = "Número")]
        public int Numero { get; set; }

        [Required(ErrorMessage = "Selecione uma raridade.")]
        [Display(Name = "Raridade")]
        public ETipoRaridade Raridade { get; set; }

        [Display(Name = "Nome do Jogador")]
        public string? NomeJogador { get; set; } = string.Empty;
    }
}