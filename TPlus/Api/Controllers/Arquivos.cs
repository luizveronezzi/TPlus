using Api.Model;
using Api.Service;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Arquivos : ControllerBase
    {

        private readonly ILogger<Arquivos> _logger;

        public Arquivos(ILogger<Arquivos> logger)
        {
            _logger = logger;
        }

        [HttpGet("CarregarArquivo")]
        public List<string> CarregaArquivo(string file = "C:\\Users\\luiz.veronezzi.FW\\Downloads\\279999-full.m3u")
        {
            List<string> retorno = new();
            try
            {
                string regraFilmes = "Filmes |";
                string regraSeries = "S{1}[0-9]{2}E{1}[0-9]{2}";

                Ferramentas Ferramenta = new Ferramentas();

                string conteudoArquivo = Ferramenta.CarregaArquivo(file);
                string[] listaRetorno = Ferramenta.SplitString(conteudoArquivo, "#EXTINF");
                //string[] listaRetorno = Ferramenta.SplitString(conteudoArquivo, "group-title=");


                var listaFilmes = listaRetorno.Where(y => y.Contains(regraFilmes)).ToList();
                var listaSeries = listaRetorno.Where(y => Regex.IsMatch(y, regraSeries)).ToList();
                var listaCanais = listaRetorno.Where(y => !Regex.IsMatch(y, regraSeries) && !y.Contains(regraFilmes)).ToList();
                List<Listas> canais = listaCanais.
                                Select(x => Ferramenta.RetornaDetalhes(x))
                                .ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return retorno;
        }

    }
}
