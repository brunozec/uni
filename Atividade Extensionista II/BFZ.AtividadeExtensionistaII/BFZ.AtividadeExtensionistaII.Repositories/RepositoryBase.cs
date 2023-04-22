using SQLite;
using System.Data.SqlTypes;
using System.Reflection;
using BFZ.AtividadeExtensionistaII.Domain.Abstractions;
using BFZ.AtividadeExtensionistaII.Domain.Abstractions.UnidadesDeNegocios;
using BFZ.AtividadeExtensionistaII.Domain.Models;

namespace BFZ.AtividadeExtensionistaII.Repositories;

public class RepositoryBase
{
    public const string DatabaseFilename = "AtividadeExtensionistaII.db3";

    public const SQLite.SQLiteOpenFlags Flags =
        // open the database in read/write mode
        SQLite.SQLiteOpenFlags.ReadWrite
        |
        // create the database if it doesn't exist
        SQLite.SQLiteOpenFlags.Create
        |
        // enable multi-threaded database access
        SQLite.SQLiteOpenFlags.SharedCache;

    public static string DatabasePath => Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);

    protected SQLiteAsyncConnection DatabaseConnection;

    public async Task Init()
    {
        if (DatabaseConnection is not null)
            return;

        DatabaseConnection = new SQLiteAsyncConnection(DatabasePath, Flags);

        await DatabaseConnection.CreateTableAsync<UnidadeDeNegocio>();
    }
}