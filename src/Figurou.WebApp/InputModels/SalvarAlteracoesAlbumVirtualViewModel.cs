namespace Figurou.WebApp.InputModels
{
    public class SalvarAlteracoesAlbumVirtualViewModel
    {
        public Guid AlbumId { get; set; }
        public List<AtualizarFigurinhaAlbumVirtualInputModel> Figurinhas { get; set; }
    }

    public class AtualizarFigurinhaAlbumVirtualInputModel
    {
        public Guid FigurinhaId { get; set; }
        public int Quantidade { get; set; }
    }
}
