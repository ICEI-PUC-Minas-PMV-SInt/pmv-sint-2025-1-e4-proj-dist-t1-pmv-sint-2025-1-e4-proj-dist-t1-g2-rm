using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class UsuarioCreateDTO
{
    [Required]
    public string Nome { get; set; }

    [Required]
    public string Estado { get; set; }

    [Required]
    public string Cidade { get; set; }

    [Required]
    public string Bairro { get; set; }

    [Required]
    public string Rua { get; set; }

    [Required]
    public int CEP { get; set; }

    [Required]
    public int Numero { get; set; }

    public string Complemento { get; set; }

    [Required]
    public string Username { get; set; }

    public string Password { get; set; }

    [Required]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TipoUsuario Tipo { get; set; }

    public OrgaoPublico OrgaoPublico { get; set; }
    public Administrador Administrador { get; set; }
    public Municipe Municipe { get; set; }
}
