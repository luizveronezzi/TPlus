using Microsoft.AspNetCore.Mvc;

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

        [HttpGet(Name = "CarregarArquivo")]
        public List<string> Get(string file)
        {
            List<string> line = new();
            try
            {
                StreamReader sr = new StreamReader(file);
                line.Add(sr.ReadLine());
                while (line != null)
                {
                    line.Add(sr.ReadLine());
                }
                sr.Close();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return line;
        }
    }
}
