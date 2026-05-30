using Figurou.Business.Interfaces;
using Figurou.Business.Services.Interfaces;

namespace Figurou.Business.Services.Implementations
{
    public class ArquivoService : BaseService, IArquivoService
    {
        public ArquivoService(INotificador notificador) : base(notificador) { }

        public bool DeletarImagem(string caminho)
        {
            if (string.IsNullOrWhiteSpace(caminho))
            {
                Notificar("Caminho da imagem não informado.");
                return false;
            }

            try
            {
                var caminhoCompleto = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    caminho.TrimStart('/'));

                if (!File.Exists(caminhoCompleto))
                {
                    Notificar("Imagem não encontrada.");
                    return false;
                }

                File.Delete(caminhoCompleto);

                return true;
            }
            catch
            {
                Notificar("Erro ao deletar imagem.");
                return false;
            }
        }

        public async Task<string?> SalvarAsync(Stream arquivo, string nomeArquivo, string pasta)
        {
            if (arquivo == null || arquivo.Length == 0)
            {
                Notificar("Arquivo inválido.");
                return null;
            }

            var extensoesPermitidas = new[]
            {
                ".jpg",
                ".jpeg",
                ".png",
                ".webp"
            };

            var extensao = Path
                .GetExtension(nomeArquivo)
                .ToLower();

            if (!extensoesPermitidas.Contains(extensao))
            {
                Notificar("Formato de imagem não permitido.");
                return null;
            }

            var nomeFinal = $"{Guid.NewGuid()}{extensao}";

            var pastaCompleta = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                pasta);

            if (!Directory.Exists(pastaCompleta))
                Directory.CreateDirectory(pastaCompleta);

            var caminhoArquivo = Path.Combine(
                pastaCompleta,
                nomeFinal);

            try
            {
                using (var stream = new FileStream(
                    caminhoArquivo,
                    FileMode.Create))
                {
                    await arquivo.CopyToAsync(stream);
                }

                return $"/{pasta}/{nomeFinal}";
            }
            catch
            {
                Notificar("Erro ao salvar arquivo.");
                return null;
            }
        }
    }
}
