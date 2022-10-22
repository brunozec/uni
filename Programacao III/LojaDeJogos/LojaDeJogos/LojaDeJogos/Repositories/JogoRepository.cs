using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using LojaDeJogos.Models;
using LojaDeJogos.Services;
using SQLite;
using Xamarin.Forms;

namespace LojaDeJogos.Repositories
{
    public class JogoRepository : IJogoRepository
    {
        private readonly SQLiteAsyncConnection _connection;

        public JogoRepository()
        {
            var sql = DependencyService.Resolve<ISql>();
            _connection = sql.GetConnectionAsync();
        }

        public Task<Jogo> GetByIdAsync(int id)
        {
            return _connection.Table<Jogo>()
                //Condição para retornar apenas itens com o código identificador passado como parametro
                .Where(where => where.Id == id)
                //Retorna apenas um item, ou nenhum
                .FirstOrDefaultAsync();
        }

        public Task<List<Jogo>> GetAllAsync()
        {
            return _connection.Table<Jogo>()
                .ToListAsync();
        }

        public Task<int> SaveAsync(Jogo model)
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