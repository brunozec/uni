using AtividadePratica.Domain;

namespace AtividadePratica.Data;

public interface IAlunoRepository
{
    /// <summary>
    /// Insere o objeto aluno
    /// </summary>
    /// <param name="aluno">Objeto que representa o aluno</param>
    /// <returns></returns>
    Task<Aluno> InsertAsync(Aluno aluno);
    
    /// <summary>
    /// Atualiza o objeto aluno
    /// </summary>
    /// <param name="aluno">Objeto que representa o aluno</param>
    /// <returns></returns>
    Task<Aluno> UpdateAsync(Aluno aluno);
    
    /// <summary>
    /// Carrega o objeto aluno
    /// </summary>
    /// <param name="ru">RU do aluno</param>
    /// <returns></returns>
    Task<Aluno> GetByRUAsync(int ru);
    
    /// <summary>
    /// Carrega o objeto aluno
    /// </summary>
    /// <param name="ru">RU do aluno</param>
    /// <returns></returns>
    Task DeleteByRUAsync(int ru);
    
    /// <summary>
    /// Carrega uma lista de alunos
    /// </summary>
    /// <param name="curso">Nome do curso</param>
    /// <returns></returns>
    Task<IList<Aluno>> GetByCursoAsync(string curso);
}