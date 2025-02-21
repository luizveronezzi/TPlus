using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TVPlus.Models;
using TVPlus.Service;
using TVPlus.Services;

namespace TVPlus.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                Tmdb tmdb = new(_logger,_configuration);

                var generoTV = await tmdb.GenerosSeries();
                var generoFilme = await tmdb.GenerosFilmes();
                var resultadoFilme = await tmdb.PesquisaFilmes("Armagedon");
                var resultadoSerie = await tmdb.PesquisaSeries("Dexter");

                var detalheFilmes = await tmdb.DetalheFilmes(resultadoFilme.results[0].id);
                var detalheSeries = await tmdb.DetalheSeries(resultadoSerie.results[0].id);

                var listGen = resultadoFilme.results[0].genre_ids;
                List<string> gens = generoFilme.genres.Where(x => listGen.Contains(x.id)).Select(x => x.name).ToList();


            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            ///search/movie?query=Mumia&include_adult=false&language=en-US&page=1'
            ///search/tv?query=Round%206&include_adult=false&language=en-US&page=1' 
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
