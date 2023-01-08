namespace PWEB.Models
{
    public class Empresa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Disponivel { get; set; }

        public DateTime? Eliminar { get; set; }

        public ICollection<Veiculo>? Veiculos { get; set; }  // pode n ter logo veiculos  // relacao 1-N
        public ICollection<Reserva>? Reservas { get; set; }  // pode n ter logo reservas  // relacao 1-N
        public ICollection<Utilizador>? Empregados { get; set; }  // tem de começar com o gestor // relacao 1-N
        public ICollection<Avaliacao>? Avaliacoes { get; set; }  // pode n ter logo Avaliacao  // relacao 1-N
        public ICollection<Subscricao>? Subscricoes { get; set; }  // tem de começar logo Subscricao  // relacao 1-N


        public bool atualizaSubs()
        {
            if (Subscricoes == null || Subscricoes.Count == 0)
            {
                return false;
            }
            foreach (var s in Subscricoes)
            {
                if (s.Activa == true)
                {
                    return true;
                }
            }
            return false;
        }

        public double? atualizaAvaliacao()
        {
            if (Avaliacoes == null || Avaliacoes.Count == 0)
            {
                return 0.0;
            }

            double? a1 = 0.0;

            foreach (var item in Avaliacoes)
            {
                if (item.Valor != null)
                {
                    a1 += item.Valor;
                }
                else
                {
                    continue;
                }

            }

            return a1 / Avaliacoes.Count;

        }
    }
}
