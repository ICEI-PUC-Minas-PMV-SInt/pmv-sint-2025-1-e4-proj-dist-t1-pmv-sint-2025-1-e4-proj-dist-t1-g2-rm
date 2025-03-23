using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReciclaMaisAPI.Models
{
    [Table("Agendamentos")]
    public class Agendamento
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Data { get; set; }

        [Required]
        public TimeSpan Hora { get; set; }

        [Required]
        public int PontuacaoTotal { get; set; }

        [Required]
        public List<ItemColeta> ItensColeta { get; set; } = new();
    }
}
