using Figurou.Business.Enums;

public class FigurinhaDTO
{
    public FigurinhaDTO(
        Guid id,
        Guid paginaId,
        Guid albumId,
        string nomeAlbum,
        int numeroPagina,
        string codigo,
        int numero,
        ETipoRaridade raridade,
        string nomeJogador)
    {
        Id = id;
        PaginaId = paginaId;
        AlbumId = albumId;
        NomeAlbum = nomeAlbum;
        NumeroPagina = numeroPagina;
        Codigo = codigo;
        Numero = numero;
        Raridade = raridade;
        NomeJogador = nomeJogador;
    }

    public Guid Id { get; private set; }
    public Guid PaginaId { get; private set; }
    public Guid AlbumId { get; private set; }

    public string NomeAlbum { get; private set; }
    public int NumeroPagina { get; private set; }

    public string Codigo { get; private set; }
    public int Numero { get; private set; }
    public ETipoRaridade Raridade { get; private set; }
    public string NomeJogador { get; private set; }
}