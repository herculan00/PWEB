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
        public ICollection<Subscricao>? Subscricoes{ get; set; }  // tem de começar logo Subscricao  // relacao 1-N


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

        public double atualizaAvaliacao()
        {
            if (Avaliacoes == null || Avaliacoes.Count == 0  )
            {
                return 0.0;
            }

            var a1 = 0.0; var a2 = 0.0; var a3 = 0.0; var a4 = 0.0; 
            var a5 = 0.0; var a6 = 0.0; var a7 = 0.0;

            foreach (var item in Avaliacoes)
            {
                a1 += item.Valor; a2 += item.TempoLevantamento; a3 += item.LimpezaCarro;
                a4 += item.FacilidadeEncontrar; a5 += item.Prestabilidade;
                a6 += item.VelocidadeDevolucao; a7 += item.CondicaoCarro; 
            }

            return (a1 + a2 + a3 + a4 + a5 + a6 + a7) / 7.0;
        }

    }
}
