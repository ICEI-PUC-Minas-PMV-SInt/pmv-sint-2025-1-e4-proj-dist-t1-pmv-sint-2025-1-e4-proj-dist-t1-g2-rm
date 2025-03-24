using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReciclaMaisAPI.Models
{
    [Table("Produtos")]
    public class ProdutoResiduo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Descricao { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "A pontuação deve ser maior que zero.")]
        public int Pontuacao { get; set; }
    }
}
