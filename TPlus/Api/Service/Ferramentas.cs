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

        public string[] SplitString(string strOrigem, string strPadrao)
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
            string regraSeries = "S{1}[0-9 ]{2,3}E[0-9]{2,3}";

            int posicao = SplitPos(noLista, "tvg-name=");
            posicao = posicao == 0 ? SplitPos(noLista, "tvg-id=") : posicao;

            if (posicao > 0)
            {

                // Inicia a Separação Basica
                string novaLista = noLista.Substring(posicao + 9);
                posicao = SplitPos(novaLista, "tvg-logo=");
                retorno.Titulo = novaLista.Substring(1, posicao - 3);

                novaLista = novaLista.Substring(posicao + 9);
                posicao = SplitPos(novaLista, "group-title=");
                retorno.Logo = novaLista.Substring(1, posicao - 3);

                novaLista = novaLista.Substring(posicao + 12);
                posicao = SplitPos(novaLista, "http");
                retorno.Categoria = novaLista.Substring(0, posicao - 1);

                novaLista = novaLista.Substring(posicao);
                retorno.Link = novaLista;

                // Inicia a Separacao Detalhada

                //retorno.Categoria = retorno.Categoria.Replace("\"\"","\"");
                int posini = SplitPos(retorno.Categoria, "[|]");
                posini = posini > 0 ? posini + 2 : 1;
                retorno.Categoria = retorno.Categoria.Substring(posini);
                posicao = SplitPos(retorno.Categoria, ",");

                if (posicao > 1)
                {
                    retorno.Categoria = retorno.Categoria.Substring(0, posicao - 1);
                }
                else
                {
                    retorno.Categoria = retorno.Categoria.Substring(posicao + 1);
                }

                posicao = SplitPos(retorno.Titulo, regraSeries);

                if (posicao > 0)
                {
                    string temporada = retorno.Titulo.Substring(posicao);
                    retorno.Titulo = retorno.Titulo.Substring(0, posicao - 1);
                    posicao = SplitPos(temporada, "E");
                    retorno.Temporada = temporada.Substring(0, posicao);
                    retorno.Episodios = temporada.Substring(posicao);
                }
            }

            return retorno;
        }

    }
}
