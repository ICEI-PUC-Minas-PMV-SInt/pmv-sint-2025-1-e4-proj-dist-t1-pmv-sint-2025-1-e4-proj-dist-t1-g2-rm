using System;
using System.ComponentModel.DataAnnotations;

namespace ReciclaMais.API.Models
{
    public class FaleConosco
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string? Telefone { get; set; }

        [Required]
        public string Mensagem { get; set; }

        public DateTime DataEnvio { get; set; } = DateTime.UtcNow;
    }
}
