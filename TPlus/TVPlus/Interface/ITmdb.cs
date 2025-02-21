using TVPlus.Models;

namespace TVPlus.Interface
{
    public interface ITmdb
    {
        Task<DetalheFilmes> DetalheFilmes(int id);
        Task<DetalheSeries> DetalheSeries(int id);
        Task<Generos> GenerosFilmes();
        Task<Generos> GenerosSeries();
        Task<PesquisaFilmes> PesquisaFilmes(string pesquisa);
        Task<PesquisaSeries> PesquisaSeries(string pesquisa);
    }
}