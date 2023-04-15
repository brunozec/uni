using SQLite;
using System.Data.SqlTypes;
using System.Reflection;
using BFZ.AtividadeExtensionistaII.Domain.Abstractions;
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

    SQLiteAsyncConnection DatabaseConnection;

    public RepositoryBase() { }

    async Task Init()
    {
        if (DatabaseConnection is not null)
            return;

        DatabaseConnection = new SQLiteAsyncConnection(DatabasePath, Flags);

        await DatabaseConnection.CreateTableAsync<UnidadeDeNegocio>();
    }

    public virtual async Task<T> SaveAsync<T>(
        T entity)
        where T : IEntityId
    {
        if (entity.Id == null)
        {
            entity.Id = await DatabaseConnection.InsertAsync(entity);
        }
        else
        {
            await DatabaseConnection.UpdateAsync(entity);
        }

        return entity;
    }

    public virtual async Task<T> GetByIdAsync<T>(
        int? id)
        where T : IEntityId, new()
    {
        return await DatabaseConnection.GetAsync<T>(id);
    }

    public virtual async Task<int> DeleteByIdAsync<T>(
        int? id)
        where T : IEntityId, new()
    {
        return await DatabaseConnection.DeleteAsync<T>(id);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync<T>()
        where T : IEntityId, new()
    {
        var tableAttribute = typeof(T).GetCustomAttribute(typeof(TableAttribute));

        if (tableAttribute is TableAttribute t)
        {
            var tableInfo = await DatabaseConnection.GetTableInfoAsync(t.Name);

            var query = @$"select * from {t.Name} ";
            
            return await DatabaseConnection.QueryAsync<T>(query);
        }

        return new List<T>();
    }
}