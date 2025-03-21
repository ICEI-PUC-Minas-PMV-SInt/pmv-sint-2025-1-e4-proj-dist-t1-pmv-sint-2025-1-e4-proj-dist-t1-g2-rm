using ReciclaMaisAPI.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReciclaMaisAPI.Models
{
    [Table("ItensColeta")]
    public class ItemColeta
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public ProdutoResiduo Produto { get; set; } = new();

        [Required]
        public int Quantidade { get; set; }

        [Required]
        public EstadoConservacao Estado { get; set; }

        [Required]
        public int AgendamentoId { get; set; }

        public Agendamento Agendamento { get; set; }


        // Métodos.
        public int CalculaPontuacao()
        {
            return (Produto.Pontuacao * Quantidade * (int)Estado / 100);
        }
    }
}
