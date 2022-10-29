using LojaDeJogos.Models;
using SQLite;

namespace LojaDeJogos.Services
{
    public static class Database
    {
        public static void CreateDatabase(SQLiteAsyncConnection connection)
        {
            connection.CreateTableAsync<Jogo>().Wait();
        }
    }
}