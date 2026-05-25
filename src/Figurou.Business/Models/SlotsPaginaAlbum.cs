namespace Figurou.Business.Models
{
    public class SlotsPaginaAlbum : Entidade
    {
        protected SlotsPaginaAlbum() { }

        public SlotsPaginaAlbum(
            decimal posicaoX,
            decimal posicaoY,
            decimal largura,
            decimal altura,
            int ordem)
        {
            PosicaoX = posicaoX;
            PosicaoY = posicaoY;
            Largura = largura;
            Altura = altura;
            Ordem = ordem;
        }

        public decimal PosicaoX { get; private set; }
        public decimal PosicaoY { get; private set; }
        public decimal Largura { get; private set; }
        public decimal Altura { get; private set; }
        public int Ordem { get; private set; }

        public PaginaAlbum PaginaAlbum { get; private set; } = null!;
        public Guid PaginaAlbumId { get; private set; }
        public Figurinha Figurinha { get; private set; } = null!;
        public Guid FigurinhaId { get; private set; }

        public void Atualizar(decimal posicaoX, decimal posicaoY, decimal largura, decimal altura, int ordem)
        {
            PosicaoX = posicaoX;
            PosicaoY = posicaoY;
            Largura = largura;
            Altura = altura;
            Ordem = ordem;
        }
    }
}