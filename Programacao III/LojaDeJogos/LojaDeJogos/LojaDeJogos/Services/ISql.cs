using SQLite;

namespace LojaDeJogos.Services
{
    public interface ISql
    {
        SQLiteConnection GetConnection(string dbname = "database.db3");
        SQLiteAsyncConnection GetConnectionAsync(string dbname = "database.db3");
    }
}