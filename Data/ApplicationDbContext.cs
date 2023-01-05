using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PWEB.Models;

namespace PWEB.Data
{
    public class ApplicationDbContext : IdentityDbContext<Utilizador>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Veiculo>? Veiculos { get; set; }
        public DbSet<TipoVeiculo>? TiposVeis { get; set; }

        public DbSet<Subscricao>? Subscricoes { get; set; }
        public DbSet<TipoSubs>? TiposSubs { get; set; }

        public DbSet<Reserva>? Reserva { get; set; }

        public DbSet<Empresa>? Empresas { get; set; }

        public DbSet<Entrega>? Entregas { get; set; }

        public DbSet<Recolha>? Recolhas { get; set; }

        public DbSet<Imagem>? Imagens { get; set; }

        public DbSet<Avaliacao>? Avaliacoes { get; set; }

    }
}