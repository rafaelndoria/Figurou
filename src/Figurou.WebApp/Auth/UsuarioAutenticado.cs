using Figurou.WebApp.Auth;

public class UsuarioAutenticado : IUsuarioAutenticado
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    private const string ALBUM_KEY = "AlbumId";

    public UsuarioAutenticado(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid UsuarioId
    {
        get
        {
            var value = _httpContextAccessor.HttpContext?
                .User?
                .FindFirst("UsuarioId")?
                .Value;

            return Guid.TryParse(value, out var id)
                ? id
                : Guid.Empty;
        }
    }

    public Guid? AlbumId
    {
        get
        {
            var value = _httpContextAccessor.HttpContext?
                .Session?
                .GetString(ALBUM_KEY);

            return Guid.TryParse(value, out var id)
                ? id
                : null;
        }
    }

    public void DefinirAlbum(Guid albumId)
    {
        _httpContextAccessor.HttpContext?
            .Session?
            .SetString(ALBUM_KEY, albumId.ToString());
    }

    public void LimparAlbum()
    {
        _httpContextAccessor.HttpContext?
            .Session?
            .Remove(ALBUM_KEY);
    }
}
