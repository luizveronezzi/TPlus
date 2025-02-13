using Newtonsoft.Json;
using System.Net.Http.Headers;
using TVPlus.Interface;
using TVPlus.Model;

namespace TVPlus.Services
{
    public class IntegracaoApi : IIntegracaoApi

    {
        public string token { get; set; }
        public List<string> parametros { get; set; } = new List<string>();

        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public IntegracaoApi(IConfiguration configuration)
        {
            _configuration = configuration;

            _httpClient = new();
            _httpClient.BaseAddress = new System.Uri(_configuration["APIConfig:UrlBase"]);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_configuration["APIConfig:ContentTypeJson"]));

        }

        public void SetUrlbase(string urlNova)
        {
            _httpClient.BaseAddress = new System.Uri(urlNova);
        }

        public async Task<apiretorno> GetAPI(string nameApi)
        {
            if (this.parametros != null)
            {
                foreach (var item in this.parametros)
                {
                    nameApi = string.Concat(nameApi, "/" + item);
                }
            }

            if (this.token != null)
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.token);
            }

            var response = await _httpClient.GetAsync(nameApi);
            var resposta = await Verifica_Acesso(response);

            return resposta;
        }

        public async Task<apiretorno> PostAPI<T>(string nameApi, T body)
        {
            if (this.parametros != null)
            {
                foreach (var item in this.parametros)
                {
                    nameApi = string.Concat(nameApi, "/" + item);
                }
            }

            if (this.token != null)
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.token); ;
            }

            var response = await _httpClient.PostAsJsonAsync(nameApi, body);
            var resposta = await Verifica_Acesso(response);

            return resposta;
        }

        public async Task<apiretorno> PutAPI<T>(string nameApi, T body)
        {
            if (this.parametros != null)
            {
                foreach (var item in this.parametros)
                {
                    nameApi = string.Concat(nameApi, "/" + item);
                }
            }

            if (this.token != null)
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.token);
            }

            var response = await _httpClient.PutAsJsonAsync(nameApi, body);
            var resposta = await Verifica_Acesso(response);

            return resposta;
        }

        public async Task<apiretorno> DeleteAPI(string nameApi)
        {
            if (this.parametros != null)
            {
                foreach (var item in this.parametros)
                {
                    nameApi = string.Concat(nameApi, "/" + item);
                }
            }

            if (this.token != null)
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.token);
            }

            var response = await _httpClient.DeleteAsync(nameApi);
            var resposta = await Verifica_Acesso(response);

            return resposta;
        }

        public async Task<object> GetData<T>(dynamic data)
        {
            string output = await Task.Run(() => JsonConvert.SerializeObject(data));
            var retorno = await Task.Run(() => JsonConvert.DeserializeObject<T>(output));
            return retorno;
        }

        private async Task<apiretorno> Verifica_Acesso(HttpResponseMessage response)
        {
            apiretorno resposta = new();
            if ((int)response.StatusCode != 200 && (int)response.StatusCode != 400)
            {
                if ((int)response.StatusCode == 500)
                {

                }

                resposta.statuscode = (int)response.StatusCode;
                resposta.success = false;
                resposta.mensage = response.ReasonPhrase;
            }
            else
            {
                resposta = await response.Content.ReadFromJsonAsync<apiretorno>();
            }
            return resposta;
        }

    }
}
