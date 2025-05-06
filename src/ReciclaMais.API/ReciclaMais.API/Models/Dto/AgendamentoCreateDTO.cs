namespace ReciclaMais.API.Models.Dto
{
    public class AgendamentoCreateDTO
    {
        public DateTime Data { get; set; }
        public TimeSpan Hora { get; set; }
        public List<ItemColetaCreateDTO> ItensColeta { get; set; }
    }
}
