using AtividadePratica.Domain;

namespace AtividadePratica.Data;

public class LivroRepository : ILivroRepository
{
    public LivroRepository()
    {
        AddLivrosIniciais().ConfigureAwait(false).GetAwaiter().GetResult();
    }
    
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
        livroDb.ISBN = livro.ISBN;
        livroDb.EdicaoAno = livro.EdicaoAno;

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

    public async Task<IList<Livro>> GetAllAsync()
    {
        //instancia o contexto do banco de dados
        await using var db = new AtividadePraticaContext();

        //retorna lista de livro com o curso informado
        return db.Livros.ToList();
    }

    public async Task AddLivrosIniciais()
    {
        var livro01 = new Livro
        {
            Nome = "Gestão de projetos"
            , EdicaoAno = "2 / 2018"
            , ISBN = "9788543025674"
            , Url = "https://plataforma.bvirtual.com.br/Leitor/Loader/3013/pdf"
        };
        var livro02 = new Livro
        {
            Nome = "Computação em Nuvem"
            , EdicaoAno = "1 / 2020"
            , ISBN = "9786557453636"
            , Url = "https://plataforma.bvirtual.com.br/Leitor/Loader/160695/epub"
        };
        var livro03 = new Livro
        {
            Nome = "INTELIGÊNCIA ARTIFICIAL APLICADA: UMA ABORDAGEM INTRODUTÓRIA"
            , EdicaoAno = "1 / 2018"
            , ISBN = "9788559728002"
            , Url = "https://plataforma.bvirtual.com.br/Leitor/Loader/161682/pdf"
            , CodigoReferencia = "3849IMP1"
        };

        //instancia o contexto do banco de dados
        await using var db = new AtividadePraticaContext();

        //verifica se os livros iniciais estão cadastrados, caso não estejam adiciona no dataset e salva
        if (!db.Livros.Any(any => any.ISBN.Equals(livro01.ISBN)))
        {
            db.Livros.Add(livro01);
        }

        if (!db.Livros.Any(any => any.ISBN.Equals(livro02.ISBN)))
        {
            db.Livros.Add(livro02);
        }

        if (!db.Livros.Any(any => any.ISBN.Equals(livro03.ISBN)))
        {
            db.Livros.Add(livro03);
        }

        //salva as alterações no datase
        await db.SaveChangesAsync();
    }
}