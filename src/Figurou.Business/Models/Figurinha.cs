using Figurou.Business.Enums;

namespace Figurou.Business.Models
{
    public class Figurinha : Entidade
    {
        protected Figurinha() { }

        public Figurinha(
            string codigo,
            int numero,
            ETipoRaridade raridade)
        {
            Codigo = codigo;
            Numero = numero;
            Raridade = raridade;

            SlotsPaginaAlbum = new List<SlotsPaginaAlbum>();
            FigurinhasUsuario = new List<FigurinhaUsuario>();
            TrocaItens = new List<TrocaItem>();
        }

        public string Codigo { get; private set; }
        public int Numero { get; private set; }
        public ETipoRaridade Raridade { get; private set; }

        public Album Album { get; private set; } = null!;
        public Guid AlbumId { get; private set; }

        public ICollection<SlotsPaginaAlbum> SlotsPaginaAlbum { get; private set; }
        public ICollection<FigurinhaUsuario> FigurinhasUsuario { get; private set; }
        public ICollection<TrocaItem> TrocaItens { get; private set; }

        public void Atualizar(string codigo, int numero, ETipoRaridade raridade)
        {
            Codigo = codigo;
            Numero = numero;
            Raridade = raridade;
        }
    }
}