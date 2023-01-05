using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace PWEB.Models
{
    public class Veiculo
    {
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }

        public int Lugares { get; set; }
        public bool MudançasManuais { get; set; }

        [Display(Name = "Preço por hora")]
        public double PreçoPorHora { get; set; }
        public string Localização { get; set; }

        public bool Disponivel { get; set; }
        public DateTime? Eliminar { get; set; }

        [Display(Name = "Categoria")]
        public int TipoId { get; set; } // relacao 1-N
        public TipoVeiculo Tipo { get; set; } // pequeno medio grande carrinha SUV comercial

        [Display(Name = "Empresa")]
        public int EmpresaId { get; set; }  // relacao 1-N
        public Empresa empresa { get; set; }

        public ICollection<Reserva>? Reservas { get; set; }  // pode n ter logo reservas // relacao 1-N

    }
}
