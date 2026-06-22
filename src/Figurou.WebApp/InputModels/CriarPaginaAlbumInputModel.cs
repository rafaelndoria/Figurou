using Figurou.WebApp.ViewModels;

using System.ComponentModel.DataAnnotations;

namespace Figurou.WebApp.InputModels
{
    public class CriarPaginaAlbumInputModel
    {
        [Required]
        public Guid AlbumId { get; set; }

        [Required(ErrorMessage = "O número da página é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "O número da página deve ser maior que zero.")]
        [Display(Name = "Número da Página")]
        public int NumeroPagina { get; set; }

        [Required(ErrorMessage = "A largura da imagem é obrigatória.")]
        [Range(0.1, double.MaxValue, ErrorMessage = "A largura deve ser maior que zero.")]
        public decimal Largura { get; set; }

        [Required(ErrorMessage = "A altura da imagem é obrigatória.")]
        [Range(0.1, double.MaxValue, ErrorMessage = "A altura deve ser maior que zero.")]
        public decimal Altura { get; set; }

        [Required(ErrorMessage = "A imagem da página é obrigatória.")]
        [DataType(DataType.Upload)]
        public IFormFile Imagem { get; set; } = null!;

        public IEnumerable<SelecaoViewModel>? Selecoes { get; set; } = Enumerable.Empty<SelecaoViewModel>();
        public Guid? SelecaoId { get; set; }
    }
}