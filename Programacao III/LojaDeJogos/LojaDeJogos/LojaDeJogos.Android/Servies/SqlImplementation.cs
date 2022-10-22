using System;
using System.IO;
using LojaDeJogos.Droid.Servies;
using LojaDeJogos.Services;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(SqlImplementation))]
namespace LojaDeJogos.Droid.Servies
{
    public class SqlImplementation : ISql
    {
        public SQLiteConnection GetConnection(string dbname = "database.db3")
        {
            var sqliteFilename = dbname;
            string documentsPath = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            // Documents folder
            var path = Path.Combine(documentsPath, sqliteFilename);
            var conn = new SQLiteConnection(path, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.FullMutex);
            return conn;
        }

        public SQLiteAsyncConnection GetConnectionAsync(string dbname = "database.db3")
        {
            var sqliteFilename = dbname;
            string documentsPath = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
            var path = Path.Combine(documentsPath, sqliteFilename);
            return new SQLiteAsyncConnection(path);
        }
    }
}