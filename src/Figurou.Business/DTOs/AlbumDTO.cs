namespace Figurou.Business.DTOs
{
    public class AlbumDTO
    {
        public AlbumDTO(Guid id, string nome, int ano, string descricao, string imagemCapa, int totalFigurinhas, bool ativo, DateTime dataCriacao)
        {
            Id = id;
            Nome = nome;
            Ano = ano;
            Descricao = descricao;
            ImagemCapa = imagemCapa;
            TotalFigurinhas = totalFigurinhas;
            Ativo = ativo;
            DataCriacao = dataCriacao;
        }

        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public int Ano { get; private set; }
        public string Descricao { get; private set; }
        public string ImagemCapa { get; private set; }
        public int TotalFigurinhas { get; private set; }
        public bool Ativo { get; private set; }
        public DateTime DataCriacao { get; private set; }
    }
}
