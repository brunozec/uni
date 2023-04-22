namespace BFZ.AtividadeExtensionistaII.Domain.Abstractions.UnidadesDeNegocios;

public interface IBaseRepository<T>
    where T : IEntityId, new()
{
    public Task<T> SaveAsync(
        T entity);

    public Task<T> GetByIdAsync(
        int? id);

    public Task<int> DeleteByIdAsync(
        int? id);

    public Task<IEnumerable<T>> GetAllAsync();
}