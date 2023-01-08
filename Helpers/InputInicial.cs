using Microsoft.Build.Framework;
using PWEB.Models;

namespace PWEB.Helpers
{
    public class InputInicial
    {
        public string Localizacao { get; set; }
        public int TipoId { get; set; }
        public DateTime Levantamento { get; set; }
        public DateTime Entrega { get; set; }

        ICollection<Veiculo>? iveiculos{ get; set; }   
      
    }
}
