namespace PWEB.Models
{
    public class Avaliacao
    {
        public int Id { get; set; }
        public double Valor { get; set; }
        public double TempoLevantamento{ get; set; }
        public double LimpezaCarro { get; set; }
        public double FacilidadeEncontrar { get; set; }
        public double Prestabilidade { get; set; }
        public double VelocidadeDevolucao { get; set; }
        public double CondicaoCarro { get; set; }

      //  public int ReservaId { get; set; }
      //  public Reserva Reserva { get; set; }  // 1-to-1 Relacao

        public string ClienteId { get; set; }  //relacao N-1
        public Utilizador Cliente { get; set; }

       // public int EmpresaId { get; set; }  //relacao N-1
       // public Empresa Empresa { get; set; }
    }
}
