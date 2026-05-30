namespace Figurou.Business.DTOs
{
    public class SalvarAlbumDTO
    {
        public SalvarAlbumDTO(string nome, int ano, string descricao, string imagemCapa, int totalFigurinhas)
        {
            Nome = nome;
            Ano = ano;
            Descricao = descricao;
            ImagemCapa = imagemCapa;
            TotalFigurinhas = totalFigurinhas;
        }

        public string Nome { get; private set; }
        public int Ano { get; private set; }
        public string Descricao { get; private set; }
        public string ImagemCapa { get; private set; }
        public int TotalFigurinhas { get; private set; }
    }
}
