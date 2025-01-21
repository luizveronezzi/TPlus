namespace Api.Model
{
    public class Catalogo
    {
        public string Descricao { get; set; }
        public string Logo { get; set; }
        public List<Listas> ListaTotal { get; set; }
        public List<Listas> ListaFiltro { get; set; }
    }
}
