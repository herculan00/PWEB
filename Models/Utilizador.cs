using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace PWEB.Models
{
    public class Utilizador : IdentityUser
    {

        public string Nome { get; set; }
        public string Apelido{ get; set; }
        public int NIF { get; set; }
        public string Morada{ get; set; }
        public DateTime DataNascimento { get; set; }
        public int NumeroCartaoMultibanco { get; set; }
        public DateTime ValidadeCartaoMultibanco { get; set; }
        public int CvdCartaoMultibanco { get; set; }
        public bool Disponivel { get; set; }
        public DateTime? Eliminar { get; set; }

        public int? EmpresaId { get; set; }
        public Empresa? empresa { get; set; }  // pode nao pertencer a uma empresa

        public ICollection<Reserva>? Reservas { get; set; }  // pode n ter logo reservas // relacao M-N 
        public ICollection<Entrega>? Entregas { get; set; }  // pode n ter logo Entregas//relacao N-1
        public ICollection<Recolha>? Recolhas { get; set; }  // pode n ter logo Recolhas //relacao N-1
        public ICollection<Avaliacao>? Avaliacoes { get; set; } // relacao M-N


    }
}

