using System.ComponentModel.DataAnnotations;

namespace ReciclaMaisAPI.Models
{
    public class Agendamento
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Data { get; set; }

        [Required]
        public TimeSpan Hora { get; set; }

        [Required]
        public int PontuacaoTotal { get; private set; }

        [Required]
        public List<ItemColeta> ItensColeta { get; set; } = new();
    
        public void CalculaPontuacaoTotal()
        {
            PontuacaoTotal = ItensColeta.Sum(item => item.CalculaPontuacao());
        }
    }
}
