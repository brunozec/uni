using BFZ.AtividadeExtensionistaII.Domain.Abstractions.UnidadesDeNegocios;
using BFZ.AtividadeExtensionistaII.Domain.Models;

namespace BFZ.AtividadeExtensionistaII.Domain.Implementations;

public class AtividadeService
{
    private readonly IBaseRepository<Atividade> _repositoryBase;

    public AtividadeService(
        IBaseRepository<Atividade> repositoryBase
    )
    {
        _repositoryBase = repositoryBase;
    }

    public async Task<Atividade> Save(
        Atividade item)
    {
        return await _repositoryBase.SaveAsync(item);
    }

    public async Task<Atividade> Get(
        int id)
    {
        return await _repositoryBase.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Atividade>> GetAll()
    {
        return await _repositoryBase.GetAllAsync();
    }

    public async Task<IEnumerable<Atividade>> GetAllByLote(
        int loteId)
    {
        var atividades = await _repositoryBase.GetAllAsync();

        return atividades.Where(w => w.LoteId.Equals(loteId));
    }
}