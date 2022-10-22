using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using LojaDeJogos.Models;

namespace LojaDeJogos.Repositories
{
    /// <summary>
    /// Interface que representa o repositorio de jogos
    /// </summary>
    public interface IJogoRepository
    {
        /// <summary>
        /// Carrega o jogo pelo código identificador
        /// </summary>
        /// <param name="id">Código identificador do jogo</param>
        /// <returns>Retorna o objeto do jogo, caso exista</returns>
        Task<Jogo> GetByIdAsync(int id);

        /// <summary>
        /// Carrega todos os jogos cadsatrados
        /// </summary>
        /// <returns>Retorna uma lista objetos de jogos </returns>
        Task<List<Jogo>> GetAllAsync();

        /// <summary>
        /// Salva o jogo no banco de dados
        /// </summary>
        /// <param name="model">Objeto do jogo</param>
        /// <returns>Retorna o código identificador gerado para o jogo salvo</returns>
        Task<int> SaveAsync(Jogo model);

        /// <summary>
        /// Exlcui o jogo do banco de dados
        /// </summary>
        /// <param name="id">Código identificador do jogo</param>
        /// <returns>Retorna o código identificador do jogo excluido</returns>
        Task<int> DeleteByIdAsync(int id);
    }
}