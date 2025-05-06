namespace ReciclaMais.API.Models
{
    public class Noticia
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Conteudo { get; set; }
        public string Autor { get; set; }
        public DateTime DataPublicacao { get; set; }
        public string? ImagemUrl { get; set; }
    }
}
