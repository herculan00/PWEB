using System.ComponentModel.DataAnnotations.Schema;

namespace PWEB.Models
{
    public class Reserva
    {
       public int Id { get; set; }
       public DateTime DataDeLevantaneto { get; set; }
       public DateTime DataDeEntrega { get; set; }
       public bool?  Confirmado { get; set; } = false;

       public DateTime? Eliminar { get; set; }

       public int EmpresaId { get; set; }  // relacao 1-N
       public Empresa empresa{ get; set; }

       public int VeiculoId { get; set; }  // relacao 1-N
       public Veiculo Veiculo { get; set; }

       public ICollection<Utilizador> EmpregadoCliente { get; set; } // relacao M-N 

       public int? EntregaId { get; set; }
       public Entrega? Entrega { get; set; }  // 1-to-1 Relacao

       public int? RecolhaId { get; set; }
       public Recolha? Recolha { get; set; }  // 1-to-1 Relacao

       public int? AvaliacaoId { get; set; }
       public Avaliacao? Avaliacao { get; set; } // 1-to-1 Relacao

    }
}
