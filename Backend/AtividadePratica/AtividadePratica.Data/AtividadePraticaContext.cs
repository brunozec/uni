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
        
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "blogging.db");
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}