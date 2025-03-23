namespace ReciclaMaisAPI.Models.Dto
{
    public class AgendamentoCreateDTO
    {
        public DateTime Data { get; set; }
        public TimeSpan Hora { get; set; }
        public List<ItemColetaDTO> ItensColeta { get; set; }
    }
}
