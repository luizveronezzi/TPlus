using TVPlus.Interface;
using TVPlus.Models;

namespace TVPlus.Service
{
    public class Tmdb : ITmdb
    {

        private readonly IConfiguration _configuration;
        private readonly IIntegracaoApi _Api;

        public Tmdb(IConfiguration configuration, IIntegracaoApi Api)
        {
            _configuration = configuration;
            _Api = Api;
            _Api.token = _configuration["APIConfig:TokenHi"];

        }

        public async Task<Generos> GenerosSeries()
        {
            _Api.SetParameters(new Dictionary<string, dynamic> { { "language", "pt" } });
            var pesqGen = await _Api.GetAPI("genre/tv/list");
            Generos genero = await _Api.GetData<Generos>(pesqGen);
            return genero;
        }

        public async Task<Generos> GenerosFilmes()
        {
            _Api.SetParameters(new Dictionary<string, dynamic> { { "language", "pt" } });
            var pesqGen = await _Api.GetAPI("genre/movie/list");
            Generos genero = await _Api.GetData<Generos>(pesqGen);
            return genero;
        }

        public async Task<PesquisaSeries> PesquisaSeries(string pesquisa)
        {
            _Api.SetParameters(
                    new Dictionary<string, dynamic> {
                        { "query", pesquisa },
                        { "include_adult",false },
                        { "language","pt-BR" },
                        { "page",1 }
                    }
            );

            var pesq = await _Api.GetAPI("search/tv");
            PesquisaSeries resultado = await _Api.GetData<PesquisaSeries>(pesq);
            resultado.results = resultado.results.OrderByDescending(x => x.popularity).ToList();
            return resultado;
        }

        public async Task<PesquisaFilmes> PesquisaFilmes(string pesquisa)
        {
            _Api.SetParameters(
                    new Dictionary<string, dynamic> {
                        { "query", pesquisa },
                        { "include_adult",false },
                        { "language","pt-BR" },
                        { "page",1 }
                    }
            );

            var pesq = await _Api.GetAPI("search/movie");
            PesquisaFilmes resultado = await _Api.GetData<PesquisaFilmes>(pesq);
            resultado.results = resultado.results.OrderByDescending(x => x.popularity).ToList();
            return resultado;
        }

        public async Task<DetalheFilmes> DetalheFilmes(int id)
        {
            _Api.SetParameters(new Dictionary<string, dynamic> { { "language", "pt" } });
            var pesq = await _Api.GetAPI(@$"movie/{id}");
            var resultado = await _Api.GetData<DetalheFilmes>(pesq);
            return resultado;
        }

        public async Task<DetalheSeries> DetalheSeries(int id)
        {
            _Api.SetParameters(new Dictionary<string, dynamic> { { "language", "pt" } });
            var pesq = await _Api.GetAPI(@$"tv/{id}");
            var resultado = await _Api.GetData<DetalheSeries>(pesq);
            return resultado;
        }

    }
}
