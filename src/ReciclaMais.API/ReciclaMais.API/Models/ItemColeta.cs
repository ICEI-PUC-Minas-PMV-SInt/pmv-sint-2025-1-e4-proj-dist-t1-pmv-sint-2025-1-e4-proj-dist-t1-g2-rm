using ReciclaMais.API.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ReciclaMais.API.Models
{
    [Table("ItensColeta")]
    public class ItemColeta
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProdutoId { get; set; }

        [ForeignKey("ProdutoId")]
        public ProdutoResiduo Produto { get; set; }

        [Required]
        public int Quantidade { get; set; }

        [Required]
        public EstadoConservacao Estado { get; set; }

        public int AgendamentoId { get; set; }

        [ForeignKey("AgendamentoId")]
        [JsonIgnore]
        public Agendamento Agendamento { get; set; }

        [NotMapped]
        public int Pontuacao
        {
            get
            {
                return Produto != null
                    ? (Produto.Pontuacao * Quantidade * (int)Estado) / 100
                    : 0;
            }
        }
    }
}
