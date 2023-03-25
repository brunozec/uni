using AtividadePratica.Domain;

namespace AtividadePratica.Data;

public interface ILivroRepository
{
    /// <summary>
    /// Insere o objeto livro
    /// </summary>
    /// <param name="livro">Objeto que representa o livro</param>
    /// <returns></returns>
    Task<Livro> InsertAsync(
        Livro livro);

    /// <summary>
    /// Atualiza o objeto livro
    /// </summary>
    /// <param name="livro">Objeto que representa o livro</param>
    /// <returns></returns>
    Task<Livro> UpdateAsync(
        Livro livro);

    /// <summary>
    /// Carrega o objeto livro
    /// </summary>
    /// <param name="id">Id do livro</param>
    /// <returns></returns>
    Task<Livro> GetByIdAsync(
        int id);

    /// <summary>
    /// Carrega o objeto livro
    /// </summary>
    /// <param name="id">Id do livro</param>
    /// <returns></returns>
    Task DeleteByIdAsync(
        int id);

    /// <summary>
    /// Carrega todos os livros cadastrados
    /// </summary>
    /// <returns>Lista de livros</returns>
    Task<IList<Livro>> GetAllAsync();
}