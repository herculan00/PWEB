namespace PWEB.Models
{
    public class Imagem
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Observaçoes { get; set; }
        public byte[]? imagem { get; set; }
        public IFormFile imagemFile { get; set; }

        public int RecolhaId { get; set; }
        public Recolha Recolha { get; set; }
    }
}
