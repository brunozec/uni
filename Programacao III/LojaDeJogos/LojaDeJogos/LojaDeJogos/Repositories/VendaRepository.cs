using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using LojaDeJogos.Models;
using LojaDeJogos.Repositories;
using LojaDeJogos.Services;
using SQLite;
using Xamarin.Forms;

namespace LojaDeVendas.Repositories
{
    public class VendaRepository : IVendaRepository
    {
        private readonly SQLiteAsyncConnection _connection;

        public VendaRepository()
        {
            var sql = DependencyService.Resolve<ISql>();
            _connection = sql.GetConnectionAsync();
        }

        public Task<Venda> GetByIdAsync(int id)
        {
            return _connection.Table<Venda>()
                //Condição para retornar apenas itens com o código identificador passado como parametro
                .Where(where => where.Id == id)
                //Retorna apenas um item, ou nenhum
                .FirstOrDefaultAsync();
        }

        public Task<List<Venda>> GetAllAsync()
        {
            return _connection.Table<Venda>()
                .ToListAsync();
        }

        public Task<int> SaveAsync(Venda model)
        {
            //caso o objeto NÃO possua código identificador, insere no banco de dados/
            if (model.Id == null)
            {
                return _connection.InsertAsync(model);
            }
            //caso o objeto possua código identificador, insere no banco de dados/
            else
            {
                return _connection.UpdateAsync(model);
            }
        }

        public async Task<int> DeleteByIdAsync(int id)
        {
            ///carrega o item pelo código identificador
            var model = await GetByIdAsync(id);
            //deleta o item
            return await _connection.DeleteAsync(model);
        }
    }
}