using Microsoft.EntityFrameworkCore;
using ProjetoNADD.Models;

namespace ProjetoNADD.Data
{
    public class ProjetoNADDContext : DbContext
    {
        public ProjetoNADDContext(DbContextOptions<ProjetoNADDContext> options) : base(options)
        {
        }
        public DbSet<Area> Area { get; set; }
        public DbSet<Avaliacao> Avaliacao { get; set; }
        public DbSet<Curso> Curso { get; set; }
        public DbSet<Disciplina> Disciplina { get; set; }
        public DbSet<Professor> Professor { get; set; }
        public DbSet<Questao> Questao { get; set; }
        public DbSet<DisciplinaProfessor> DisciplinaProfessor { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DisciplinaProfessor>().HasKey(dp =>
                new { dp.Disciplina_id, dp.Professor_id });
        }
    }
}
