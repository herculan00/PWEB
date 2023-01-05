namespace PWEB.Models
{
    public class Subscricao
    {
        public int Id { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }
        public bool Activa { get; set; }

        public int TipoId { get; set; } // relacao 1-N
        public TipoSubs Tipo { get; set; } // Base Standard Premium

        public int EmpresaId { get; set; } // relacao 1-N
        public Empresa Empresa { get; set; } 

    }
}
