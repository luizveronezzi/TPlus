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

    }
}
