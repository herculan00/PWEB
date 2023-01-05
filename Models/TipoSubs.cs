namespace PWEB.Models
{
    public class TipoSubs
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Preço { get; set; }
        public int Duracao { get; set; }

        public ICollection<Subscricao>? Subscricoes { get; set; }  // pode n ter logo Subscricao  // relacao 1-N
    }
}
