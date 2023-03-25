using AtividadePratica.Domain;

namespace AtividadePratica.Data;

public class LivroRepository : ILivroRepository
{
    public async Task<Livro> InsertAsync(
        Livro livro)
    {
        //instancia o contexto do banco de dados
        await using var db = new AtividadePraticaContext();

        //adiciona no dataset
        db.Add(livro);

        //salva as alterações no datase
        await db.SaveChangesAsync();

        //retorna o livro
        return livro;
    }

    public async Task<Livro> UpdateAsync(
        Livro livro)
    {
        //instancia o contexto do banco de dados
        await using var db = new AtividadePraticaContext();

        var livroDb = await GetByIdAsync((int)livro.Id);

        if (livroDb == null)
        {
            throw new Exception("Registro não encontrado");
        }

        //atualiza as propriedades do livro
        livroDb.Url = livro.Url;
        livroDb.Nome = livro.Nome;

        //atualiza o livro no dataset
        db.Update(livroDb);

        //salva as alterações no datase
        await db.SaveChangesAsync();

        //retorna o livro
        return livro;
    }

    public async Task<Livro> GetByIdAsync(
        int id)
    {
        //instancia o contexto do banco de dados
        await using var db = new AtividadePraticaContext();

        //carrega o livro referente ao id informado
        var livroDb = db.Livros.FirstOrDefault(f => f.Id == id);

        //retorna o livro encontrado por id
        return livroDb;
    }

    public async Task DeleteByIdAsync(
        int id)
    {
        //instancia o contexto do banco de dados
        await using var db = new AtividadePraticaContext();

        //carrega o livro referente ao id informado
        var livroDb = db.Livros.FirstOrDefault(f => f.Id == id);

        //livro nao encontrado
        if (livroDb == null)
        {
            throw new Exception("Registro não encontrado");
        }

        //remove o livro do dataset
        db.Livros.Remove(livroDb);

        //salva as alterações no datase
        await db.SaveChangesAsync();
    }

    public async Task<IList<Livro>> GetAllAsync(
        string curso)
    {
        //instancia o contexto do banco de dados
        await using var db = new AtividadePraticaContext();

        //retorna lista de livro com o curso informado
        return db.Livros.ToList();
    }
}