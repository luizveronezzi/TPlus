using Api.Model;
using System.Text.RegularExpressions;

namespace Api.Service
{
    public class Ferramentas
    {
        public string CarregaArquivo(string file)
        {
            string conteudoArquivo = File.ReadAllText(file);
            return conteudoArquivo;
        }

        public string[] SplitString(string strOrigem,string strPadrao)
        {
            Regex regraPadrao = new Regex(strPadrao, RegexOptions.Multiline);
            var listaRetorno = regraPadrao.Split(strOrigem);

            return listaRetorno;
        }

        public int SplitPos(string strOrigem, string strPadrao)
        {
            Regex regraPadrao = new Regex(strPadrao, RegexOptions.Multiline);
            Match posicao = regraPadrao.Match(strOrigem);

            return posicao.Index;
        }

        public Listas RetornaDetalhes(string noLista)
        {
            Listas retorno = new();

            int posicao = SplitPos(noLista, "tvg-name=");

            if (posicao > 0) {
                string novaLista = noLista.Substring(posicao+9);
                posicao = SplitPos(novaLista, "tvg-logo=");
                retorno.Categoria = novaLista.Substring(0,posicao-1);

                novaLista = novaLista.Substring(posicao + 9);
                posicao = SplitPos(novaLista, "group-title=");
                retorno.Logo = novaLista.Substring(0, posicao - 1);

                novaLista = novaLista.Substring(posicao + 12);
                posicao = SplitPos(novaLista, "http");
                retorno.Titulo = novaLista.Substring(0, posicao - 1);

                novaLista = novaLista.Substring(posicao);
                retorno.Link = novaLista;
            }

            return retorno;
        }

    }
}
