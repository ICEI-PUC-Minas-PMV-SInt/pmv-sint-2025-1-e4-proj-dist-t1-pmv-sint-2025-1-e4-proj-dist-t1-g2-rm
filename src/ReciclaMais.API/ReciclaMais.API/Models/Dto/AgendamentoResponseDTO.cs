namespace ReciclaMais.API.Models.Dto
{
    public class AgendamentoResponseDTO
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan Hora { get; set; }
        public int PontuacaoTotal { get; set; }
        public List<ItemColetaResponseDTO> ItensColeta { get; set; }
    }
}
