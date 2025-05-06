namespace ReciclaMais.API.Models.Dto
{
    public class ItemColetaCreateDTO
    {
        public int? Id { get; set; }
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public int Estado { get; set; }
    }
}
