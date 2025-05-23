using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReciclaMais.API.Models
{
    [Table("Beneficios")]
    public class Beneficio
    {
        [Key] 
        public int Id { get; set; }

        [Required] 
        public string Titulo { get; set; } // Titulo do benefício, exemplo: "R$50 Supermercados BH"

        [Required] 
        public string Descricao { get; set; } // Descrição do benefício , exemplo: "Vale R$ 50 em compras nos Supermercados BH"

        [Required] 
        public int PontosNecessarios { get; set; } // Quantos pontos são necessários para resgatar esse benefício

    }
}
