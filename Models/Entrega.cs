namespace PWEB.Models
{
    public class Entrega
    {
        public int Id { get; set; }
        public int Kilometros { get; set; }
        public bool Danos { get; set; } 
        public string? Observaçoes { get; set; }
    

        public string EmpregadoId { get; set; }  //relacao N-1
        public Utilizador Empregado { get; set; }

      //  public int ReservaId { get; set; }
      //  public Reserva Reserva { get; set; }  // 1-to-1 Relacao

    }
}
