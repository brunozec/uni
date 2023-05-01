using BFZ.AtividadeExtensionistaII.Domain.Abstractions.UnidadesDeNegocios;
using BFZ.AtividadeExtensionistaII.Domain.Models;

namespace BFZ.AtividadeExtensionistaII.Domain.Implementations;

public class LoteDeProducaoService
{
    private readonly IBaseRepository<LoteDeProducao> _repositoryBase;
    private readonly IBaseRepository<Produto> _produtoRepositoryBase;

    public LoteDeProducaoService(
        IBaseRepository<LoteDeProducao> repositoryBase
        , IBaseRepository<Produto> produtoRepositoryBase
    )
    {
        _repositoryBase = repositoryBase;
        _produtoRepositoryBase = produtoRepositoryBase;
    }

    public async Task<LoteDeProducao> Save(
        LoteDeProducao item)
    {
        return await _repositoryBase.SaveAsync(item);
    }

    public async Task<LoteDeProducao> Get(
        int id)
    {
        return await _repositoryBase.GetByIdAsync(id);
    }

    public async Task<IEnumerable<LoteDeProducao>> GetAll()
    {
        return await _repositoryBase.GetAllAsync();
    }

    public async Task AddDefaultAsync()
    {
        var produtos = await _produtoRepositoryBase.GetAllAsync();
        var lotes = await _repositoryBase.GetAllAsync();
        var r = new Random(DateTime.UtcNow.Second);

        if (!lotes.Any())
        {
            await Save(new LoteDeProducao
            {
                Quantidade = r.Next(100, 9999)
                , DataPlantio = DateTime.UtcNow.AddDays(r.Next(1, 45))
                , IdProduto = produtos.First().Id
            });
            await Save(new LoteDeProducao
            {
                Quantidade = r.Next(100, 9999)
                , DataPlantio = DateTime.UtcNow.AddDays(r.Next(1, 45))
                , IdProduto = produtos.Last().Id
            });
            await Save(new LoteDeProducao
            {
                Quantidade = r.Next(100, 9999)
                , DataPlantio = DateTime.UtcNow.AddDays(r.Next(1, 45))
                , IdProduto = produtos.First().Id
            });
            await Save(new LoteDeProducao
            {
                Quantidade = r.Next(100, 9999)
                , DataPlantio = DateTime.UtcNow.AddDays(r.Next(1, 45))
                , IdProduto = produtos.Last().Id
            });
            await Save(new LoteDeProducao
            {
                Quantidade = r.Next(100, 9999)
                , DataPlantio = DateTime.UtcNow.AddDays(r.Next(1, 45))
                , IdProduto = produtos.First().Id
            });
            await Save(new LoteDeProducao
            {
                Quantidade = r.Next(100, 9999)
                , DataPlantio = DateTime.UtcNow.AddDays(r.Next(1, 45))
                , IdProduto = produtos.Last().Id
            });
        }
    }
}