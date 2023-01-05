namespace PWEB.Models
{
    public class TipoVeiculo
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Veiculo>? Veiculos { get; set; }  // pode n ter logo veiculos  // relacao 1-N
    }
}
