using System.ComponentModel.DataAnnotations;

namespace PWEB.Models
{
    public class Avaliacao
    {
        public int Id { get; set; }
        public double? Valor { get; set; }

        [Range(0.0, 10.0, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public double TempoLevantamento{ get; set; }
        [Range(0.0, 10.0, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public double LimpezaCarro { get; set; }
        [Range(0.0, 10.0, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public double FacilidadeEncontrar { get; set; }
        [Range(0.0, 10.0, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public double Prestabilidade { get; set; }
        [Range(0.0, 10.0, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public double VelocidadeDevolucao { get; set; }
        [Range(0.0, 10.0, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public double CondicaoCarro { get; set; }

      //  public int ReservaId { get; set; }
      //  public Reserva Reserva { get; set; }  // 1-to-1 Relacao

        public string ClienteId { get; set; }  //relacao N-1
        public Utilizador Cliente { get; set; }

       // public int EmpresaId { get; set; }  //relacao N-1
       // public Empresa Empresa { get; set; }
    }
}
