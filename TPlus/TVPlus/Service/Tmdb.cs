using TVPlus.Controllers;
using TVPlus.Models;
using TVPlus.Services;

namespace TVPlus.Service
{
    public class Tmdb
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public Tmdb(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }   

        public async Task<Generos> GenerosSeries()
        {
            IntegracaoApi Api = new IntegracaoApi(_configuration);
            Api.token = _configuration["APIConfig:TokenHi"];

            Api.SetParameters(
                new Dictionary<string, dynamic> {
                        { "language","pt" }
                }
            );

            var pesqGen = await Api.GetAPI("genre/tv/list");
            Generos genero = await Api.GetData<Generos>(pesqGen);
            return genero;
        }

        public async Task<Generos> GenerosFilmes()
        {
            IntegracaoApi Api = new IntegracaoApi(_configuration);
            Api.token = _configuration["APIConfig:TokenHi"];

            Api.SetParameters(
                new Dictionary<string, dynamic> {
                        { "language","pt" }
                }
            );

            // Carrega Lista de Generos
            var pesqGen = await Api.GetAPI("genre/movie/list");
            Generos genero = await Api.GetData<Generos>(pesqGen);
            return genero;
        }

        public async Task<PesquisaSeries> PesquisaSeries(string pesquisa)
        {
            IntegracaoApi Api = new IntegracaoApi(_configuration);
            Api.token = _configuration["APIConfig:TokenHi"];

            Api.SetParameters(
                    new Dictionary<string, dynamic> {
                        { "query", pesquisa },
                        { "include_adult",false },
                        { "language","pt-BR" },
                        { "page",1 }
                    }
                );

            var pesq = await Api.GetAPI("search/tv");
            PesquisaSeries resultado = await Api.GetData<PesquisaSeries>(pesq);
            resultado.results = resultado.results.OrderByDescending(x => x.popularity).ToList();
            return resultado;
        }
        
        public async Task<PesquisaFilmes> PesquisaFilmes(string pesquisa)
        {
            IntegracaoApi Api = new IntegracaoApi(_configuration);
            Api.token = _configuration["APIConfig:TokenHi"];

            Api.SetParameters(
                    new Dictionary<string, dynamic> {
                        { "query", pesquisa },
                        { "include_adult",false },
                        { "language","pt-BR" },
                        { "page",1 }
                    }
                );

            var pesq = await Api.GetAPI("search/movie");
            PesquisaFilmes resultado = await Api.GetData<PesquisaFilmes>(pesq);
            resultado.results = resultado.results.OrderByDescending(x => x.popularity).ToList();
            return resultado;
        }
        
        public async Task<DetalheFilmes> DetalheFilmes(int id)
        {
            IntegracaoApi Api = new IntegracaoApi(_configuration);
            Api.token = _configuration["APIConfig:TokenHi"];

            Api.SetParameters(
                    new Dictionary<string, dynamic> {
                        { "language","pt-BR" }
                    }
                );

            var pesq = await Api.GetAPI(@$"movie/{id}");
            var resultado = await Api.GetData<DetalheFilmes>(pesq);
            return resultado;
        }
        
        public async Task<DetalheSeries> DetalheSeries(int id)
        {
            IntegracaoApi Api = new IntegracaoApi(_configuration);
            Api.token = _configuration["APIConfig:TokenHi"];

            Api.SetParameters(
                    new Dictionary<string, dynamic> {
                        { "language","pt-BR" }
                    }
                );

            var pesq = await Api.GetAPI(@$"tv/{id}");
            var resultado = await Api.GetData<DetalheSeries>(pesq);
            return resultado;
        }

    }
}
