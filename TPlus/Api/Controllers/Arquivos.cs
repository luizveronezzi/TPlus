using Api.Model;
using Api.Service;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        public List<Catalogo> CarregaArquivo(string file = "C:\\Users\\luiz.veronezzi.FW\\Downloads\\279999-full.m3u")
        {
            List<Catalogo> retorno = new();
            try
            {
                string regraFilmes = "Filmes |";
                string regraSeries = "S{1}[0-9 ]{2,3}E[0-9]{2,3}";

                Ferramentas Ferramenta = new Ferramentas();

                string conteudoArquivo = Ferramenta.CarregaArquivo(file);
                string[] listaRetorno = Ferramenta.SplitString(conteudoArquivo, "#EXTINF");

                var listaCanais = listaRetorno.Where(y => !Regex.IsMatch(y, regraSeries) && !y.Contains(regraFilmes)).Select(x => Ferramenta.RetornaDetalhes(x)).ToList();
                var listaSeries = listaRetorno.Where(y => Regex.IsMatch(y, regraSeries)).Select(x => Ferramenta.RetornaDetalhes(x)).ToList();
                var listaFilmes = listaRetorno.Where(y => y.Contains(regraFilmes)).Select(x => Ferramenta.RetornaDetalhes(x)).ToList();

                retorno.Add(new Catalogo
                {
                    Descricao = "Canais",
                    ListaTotal = listaCanais,
                    ListaFiltro = listaCanais
                });

                retorno.Add(new Catalogo
                {
                    Descricao = "Series",
                    ListaTotal = listaSeries,
                    ListaFiltro = listaSeries.GroupBy(x => x.Titulo, (key, y) => y.Select(e => new Listas { Categoria = e.Categoria, Titulo = e.Titulo, Temporada = "(" + y.DistinctBy(a => a.Temporada).Count() + ")" }).First())
                    .ToList()
                });

                retorno.Add(new Catalogo
                {
                    Descricao = "Filmes",
                    ListaTotal = listaFilmes,
                    ListaFiltro = listaFilmes.GroupBy(x => x.Logo, (key, y) => y.OrderBy(e => e.Titulo).First()).ToList()
                });

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return retorno;
        }

    }
}
