namespace ReciclaMaisAPI.Models.Dto
{
    public class ItemColetaResponseDTO
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public string ProdutoNome { get; set; }
        public int Quantidade { get; set; }
        public int Estado { get; set; }
        public int Pontuacao { get; set; }
    }
}
