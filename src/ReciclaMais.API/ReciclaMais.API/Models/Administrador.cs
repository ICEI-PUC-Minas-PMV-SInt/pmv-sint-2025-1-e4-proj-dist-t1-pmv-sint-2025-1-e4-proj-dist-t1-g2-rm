using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Administrador
{
    [Key, ForeignKey("Usuario")]
    public int UsuarioId { get; set; }

    [Required]
    public string NomeAdmin { get; set; }

    public Usuario Usuario { get; set; }
}
