using TVPlus.Model;

namespace TVPlus.Interface
{
    public interface IIntegracaoApi
    {
        public string token { get; set; }
        public List<string> parametros { get; set; }

        Task<apiretorno> GetAPI(string nameApi);
        Task<apiretorno> PostAPI<T>(string nameApi, T body);
        Task<apiretorno> PutAPI<T>(string nameApi, T body);
        Task<apiretorno> DeleteAPI(string nameApi);
        Task<object> GetData<T>(dynamic data);

    }
}
