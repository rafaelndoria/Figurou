namespace Figurou.WebApp.Auth
{
    public interface IUsuarioAutenticado
    {
        Guid UsuarioId { get; }
        Guid? AlbumId { get; }

        void DefinirAlbum(Guid albumId);
        void LimparAlbum();
    }
}
