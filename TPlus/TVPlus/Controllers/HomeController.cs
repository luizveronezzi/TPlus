using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using TVPlus.Model;
using TVPlus.Models;
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
                IntegracaoApi Api = new IntegracaoApi(_configuration);
                Api.token = _configuration["APIConfig:TokenHi"];
                Api.SetParameters(
                    new Dictionary<string, dynamic> {
                        { "query", "Round 6" },
                        { "include_adult",false },
                        { "language","pt-BR" },
                        { "page",1 }
                    }
                );

                var pesqTV = await Api.GetAPI("search/tv");
                var resultadoTV = await Api.GetData<PesquisaSeries>(pesqTV);

                Api.SetParameters(
                    new Dictionary<string, dynamic> {
                        { "query", "Mumia" },
                        { "include_adult",false },
                        { "language","pt-BR" },
                        { "page",1 }
                    }
                );

                var pesqFilme = await Api.GetAPI("search/movie");
                var resultadoFilme = await Api.GetData<PesquisaFilmes>(pesqFilme);


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
