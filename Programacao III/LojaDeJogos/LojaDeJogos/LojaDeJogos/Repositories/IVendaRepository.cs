using System.Collections.Generic;
using System.Threading.Tasks;
using LojaDeJogos.Models;

namespace LojaDeVendas.Repositories
{
    /// <summary>
    /// Interface que representa o repositorio de vendas
    /// </summary>
    public interface IVendaRepository
    {
        /// <summary>
        /// Carrega o venda pelo código identificador
        /// </summary>
        /// <param name="id">Código identificador do venda</param>
        /// <returns>Retorna o objeto do venda, caso exista</returns>
        Task<Venda> GetByIdAsync(int id);

        /// <summary>
        /// Carrega todos os vendas cadsatrados
        /// </summary>
        /// <returns>Retorna uma lista objetos de vendas </returns>
        Task<List<Venda>> GetAllAsync();

        /// <summary>
        /// Salva o venda no banco de dados
        /// </summary>
        /// <param name="model">Objeto do venda</param>
        /// <returns>Retorna o código identificador gerado para o venda salvo</returns>
        Task<int> SaveAsync(Venda model);

        /// <summary>
        /// Exlcui o venda do banco de dados
        /// </summary>
        /// <param name="id">Código identificador do venda</param>
        /// <returns>Retorna o código identificador do venda excluido</returns>
        Task<int> DeleteByIdAsync(int id);
    }
}