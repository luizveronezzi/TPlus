using TVPlus.Model;

namespace TVPlus.Interface
{
    public interface IIntegracaoApi
    {
        public string token { get; set; }
        public string parametros { get; set; }

        Task<apiretorno> GetAPI(string nameApi);
        Task<apiretorno> PostAPI<T>(string nameApi, T body);
        Task<apiretorno> PutAPI<T>(string nameApi, T body);
        Task<apiretorno> DeleteAPI(string nameApi);
        Task<dynamic> GetData<T>(apiretorno data);
        void SetUrlbase(string urlNova);
        void SetParameters(Dictionary<string, dynamic> parameters);

    }
}
