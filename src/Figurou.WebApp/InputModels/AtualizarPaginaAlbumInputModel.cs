using System.ComponentModel.DataAnnotations;

namespace Figurou.WebApp.InputModels
{
    public class AtualizarPaginaAlbumInputModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid AlbumId { get; set; }

        [Required(ErrorMessage = "A imagem da página é obrigatória.")]
        public string ImagemPagina { get; set; } = string.Empty;

        [Required(ErrorMessage = "Informe o número da página.")]
        [Range(1, 9999, ErrorMessage = "O número da página deve ser maior que zero.")]
        [Display(Name = "Número da Página")]
        public int NumeroPagina { get; set; }

        [Required(ErrorMessage = "Informe a largura da página.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "A largura deve ser maior que zero.")]
        [Display(Name = "Largura")]
        public decimal Largura { get; set; }

        [Required(ErrorMessage = "Informe a altura da página.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "A altura deve ser maior que zero.")]
        [Display(Name = "Altura")]
        public decimal Altura { get; set; }

        [Display(Name = "Nova Imagem")]
        public IFormFile? NovaImagem { get; set; }
    }
}