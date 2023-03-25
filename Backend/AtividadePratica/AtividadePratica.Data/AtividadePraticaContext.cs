using AtividadePratica.Domain;
using Microsoft.EntityFrameworkCore;

namespace AtividadePratica.Data;

public class AtividadePraticaContext : DbContext
{
    /// <summary>
    /// dataset que representa os alunos do banco de dados
    /// </summary>
    public DbSet<Aluno> Alunos { get; set; }

    /// <summary>
    /// dataset que representa os livros do banco de dados
    /// </summary>
    public DbSet<Livro> Livros { get; set; }

    public AtividadePraticaContext()
    {
        //garante que o banco de dados está criado com as tabelas dos datasets
        Database.EnsureCreated();
    }

    //configuração da connectionstring para acesso ao banco de dados
    protected override void OnConfiguring(
        DbContextOptionsBuilder options) =>
        options.UseSqlServer($"Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=atividade_pratica;Integrated Security=SSPI;");
}