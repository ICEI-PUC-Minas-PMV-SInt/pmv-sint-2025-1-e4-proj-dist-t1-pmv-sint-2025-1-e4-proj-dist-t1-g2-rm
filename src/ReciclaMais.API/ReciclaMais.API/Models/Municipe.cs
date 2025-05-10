using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Municipe
{
    [Key, ForeignKey("Usuario")]
    public int UsuarioId { get; set; }
    public DateOnly DataNascimento { get; set; }
    public string Cpf { get; set; }
    public int Pontuacao { get; set; }  

    public Usuario Usuario { get; set; }
}
