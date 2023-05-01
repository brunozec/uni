using SQLite;
using System.Reflection;
using BFZ.AtividadeExtensionistaII.Domain.Abstractions;
using BFZ.AtividadeExtensionistaII.Domain.Abstractions.UnidadesDeNegocios;

namespace BFZ.AtividadeExtensionistaII.Repositories;

public class RepositoryBase<T> : RepositoryBase, IBaseRepository<T> where T : IEntityId, new()
{
    public virtual async Task<T> SaveAsync(
        T entity)
    {
        await Init();
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

    public virtual async Task<T> GetByIdAsync(
        int? id)
    {
        await Init();
        return await DatabaseConnection.GetAsync<T>(id);
    }

    public virtual async Task<int> DeleteByIdAsync(
        int? id)
    {
        await Init();
        return await DatabaseConnection.DeleteAsync<T>(id);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        await Init();
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