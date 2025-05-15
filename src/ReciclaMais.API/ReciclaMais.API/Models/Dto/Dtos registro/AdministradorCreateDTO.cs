using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ReciclaMais.API.Models.Dto.Dtos_registro
{
    public class AdministradorCreateDTO
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

        [Required]
        public string Password { get; set; }

        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TipoUsuario Tipo { get; set; } = TipoUsuario.Administrador;

    }
}
