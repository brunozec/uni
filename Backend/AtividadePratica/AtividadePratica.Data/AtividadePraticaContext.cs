using AtividadePratica.Domain;
using Microsoft.EntityFrameworkCore;

namespace AtividadePratica.Data;

public class AtividadePraticaContext : DbContext
{
    public DbSet<Aluno> Alunos { get; set; }

    public DbSet<Livro> Livros { get; set; }

    public string DbPath { get; }

    public AtividadePraticaContext()
    {
        Database.EnsureCreated();
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(
        DbContextOptionsBuilder options) =>
        options.UseSqlServer($"Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=atividade_pratica;Integrated Security=SSPI;");
}