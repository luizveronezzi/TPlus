using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TVPlus.Interface;
using TVPlus.Models;

namespace TVPlus.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITmdb _tmdb;
        public HomeController(ILogger<HomeController> logger,ITmdb tmdb)
        {
            _logger = logger;
            _tmdb = tmdb;  
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var generoTV = await _tmdb.GenerosSeries();
                var generoFilme = await _tmdb.GenerosFilmes();
                var resultadoFilme = await _tmdb.PesquisaFilmes("Armagedon");
                var resultadoSerie = await _tmdb.PesquisaSeries("Dexter");

                var detalheFilmes = await _tmdb.DetalheFilmes(resultadoFilme.results[0].id);
                var detalheSeries = await _tmdb.DetalheSeries(resultadoSerie.results[0].id);

                var listGen = resultadoFilme.results[0].genre_ids;
                List<string> gens = generoFilme.genres.Where(x => listGen.Contains(x.id)).Select(x => x.name).ToList();

                var vid = await _tmdb.VideosFilmes(resultadoFilme.results[0].id);
                var vid1 = await _tmdb.VideosSeries(resultadoSerie.results[0].id);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

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
