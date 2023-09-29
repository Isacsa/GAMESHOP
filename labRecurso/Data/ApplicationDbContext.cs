using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using labRecurso.Models;

namespace labRecurso.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<labRecurso.Models.Avalicao> Avalicao { get; set; }
        public DbSet<labRecurso.Models.Categoria> Categoria { get; set; }
        public DbSet<labRecurso.Models.Comentario> Comentario { get; set; }
        public DbSet<labRecurso.Models.Jogos> Jogos { get; set; }
        public DbSet<labRecurso.Models.Perfil> Perfil { get; set; }
        public DbSet<labRecurso.Models.PerfilCategoria> PerfilCategoria { get; set; }
        public DbSet<labRecurso.Models.PerfilJogos> PerfilJogos { get; set; }
        public DbSet<labRecurso.Models.Plataforma> Plataforma { get; set; }
    }
}