using Microsoft.Build.Framework;
using PWEB.Models;

namespace PWEB.Helpers
{
    public class InputInicial
    {
        [Required]
        public string Localizacao { get; set; }
        [Required]
        public int TipoId { get; set; }
        [Required]
        public DateTime Levantamento { get; set; }
        [Required]
        public DateTime Entrega { get; set; }

        ICollection<Veiculo>? iveiculos{ get; set; }   
      
    }
}
