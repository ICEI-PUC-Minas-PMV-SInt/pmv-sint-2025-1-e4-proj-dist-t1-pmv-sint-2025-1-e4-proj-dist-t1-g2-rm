using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class OrgaoPublico
{
    [Key, ForeignKey("Usuario")]
    public int UsuarioId { get; set; }

    [Required]
    public string CNPJ { get; set; }

    public Usuario Usuario { get; set; }
}
