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

        //retorna o aluno
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

        //atualiza o aluno no dataset
        db.Update(alunoDb);

        //salva as alterações no datase
        await db.SaveChangesAsync();

        //retorna o aluno
        return aluno;
    }

    public async Task<Aluno> GetByRUAsync(
        int ru)
    {
        //instancia o contexto do banco de dados
        await using var db = new AtividadePraticaContext();

        //carrega o aluno referente ao ru informado
        var alunoDb = db.Alunos.FirstOrDefault(f => f.RU == ru);

        //retorna o aluno encontrado por ru
        return alunoDb;
    }

    public async Task DeleteByRUAsync(
        int ru)
    {
        //instancia o contexto do banco de dados
        await using var db = new AtividadePraticaContext();

        //carrega o aluno referente ao ru informado
        var alunoDb = db.Alunos.FirstOrDefault(f => f.RU == ru);

        //aluno nao encontrado
        if (alunoDb == null)
        {
            throw new Exception("Registro não encontrado");
        }

        //remove o aluno do dataset
        db.Alunos.Remove(alunoDb);

        //salva as alterações no datase
        await db.SaveChangesAsync();
    }

    public async Task<IList<Aluno>> GetByCursoAsync(
        string curso)
    {
        //instancia o contexto do banco de dados
        await using var db = new AtividadePraticaContext();

        //retorna lista de aluno com o curso informado
        return db.Alunos.Where(where => where.Curso.ToLower() == curso.ToLower()).ToList();
    }
}