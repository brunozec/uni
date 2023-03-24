using AtividadePratica.Domain;

namespace AtividadePratica.Data;

public class AlunoRepository : IAlunoRepository
{
    public AlunoRepository() { }

    public async Task<Aluno> InsertAsync(
        Aluno aluno)
    {
        //instancia o contexto do banco de dados
        await using var db = new AtividadePraticaContext();

        //adiciona no dataset
        db.Add(aluno);

        //salva as alterações no datase
        await db.SaveChangesAsync();

        return aluno;
    }

    public async Task<Aluno> UpdateAsync(
        Aluno aluno)
    {
        //instancia o contexto do banco de dados
        await using var db = new AtividadePraticaContext();

        var alunoDb = await GetByRUAsync((int)aluno.RU);

        if (alunoDb == null)
        {
            throw new Exception("Registro não encontrado");
        }

        //atualiza as propriedades do aluno
        alunoDb.Curso = aluno.Curso;
        alunoDb.Nome = aluno.Nome;

        db.Update(alunoDb);
        
        //salva as alterações no datase
        await db.SaveChangesAsync();

        return aluno;
    }

    public async Task<Aluno> GetByRUAsync(
        int ru)
    {
        //instancia o contexto do banco de dados
        await using var db = new AtividadePraticaContext();
        
        //carrega o aluno referente ao ru informado
        var alunoDb = db.Alunos.FirstOrDefault(f => f.RU == ru);

        return alunoDb;
    }

    public async Task DeleteByRUAsync(
        int ru)
    {
        //instancia o contexto do banco de dados
        await using var db = new AtividadePraticaContext();

        //carrega o aluno referente ao ru informado
        var alunoDb = db.Alunos.FirstOrDefault(f => f.RU == ru);

        if (alunoDb == null)
        {
            throw new Exception("Registro não encontrado");
        }

        db.Alunos.Remove(alunoDb);

        //salva as alterações no datase
        await db.SaveChangesAsync();
    }

    public async Task<IList<Aluno>> GetByCursoAsync(
        string curso)
    {
        await using var db = new AtividadePraticaContext();

        return db.Alunos.ToList();
    }
}