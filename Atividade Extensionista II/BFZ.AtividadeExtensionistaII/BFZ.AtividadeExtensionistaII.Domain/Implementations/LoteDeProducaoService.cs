using System.Runtime.InteropServices.ComTypes;
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
            var d = DateTime.UtcNow.AddDays(r.Next(1, 45));
            await Save(new LoteDeProducao
            {
                Quantidade = r.Next(100, 9999)
                , DataPlanejado = d
                , IdProduto = produtos.First().Id
                , Plantado = false
                , Situacao = Situacao.Planejado
                , IdEmpresa = 1
            });
            d = DateTime.UtcNow.AddDays(r.Next(1, 45));
            await Save(new LoteDeProducao
            {
                Quantidade = r.Next(100, 9999)
                , DataPlanejado = d
                , DataPlantio = d
                , IdProduto = produtos.Last().Id
                , Plantado = true
                , Situacao = Situacao.EmProducao
                , IdEmpresa = 1
            });
            d = DateTime.UtcNow.AddDays(r.Next(1, 45));
            await Save(new LoteDeProducao
            {
                Quantidade = r.Next(100, 9999)
                , DataPlanejado = d
                , DataPlantio = d
                , IdProduto = produtos.First().Id
                , Plantado = true
                , Situacao = Situacao.EmProducao
                , IdEmpresa = 1
            });
            d = DateTime.UtcNow.AddDays(r.Next(1, 45));
            await Save(new LoteDeProducao
            {
                Quantidade = r.Next(100, 9999)
                , DataPlanejado = d
                , DataPlantio = d
                , DataEncerramento = d.AddDays(45)
                , IdProduto = produtos.Last().Id
                , Plantado = true
                , Situacao = Situacao.Colhido
                , IdEmpresa = 1
            });
            d = DateTime.UtcNow.AddDays(r.Next(1, 45));
            await Save(new LoteDeProducao
            {
                Quantidade = r.Next(100, 9999)
                , DataPlanejado = d
                , DataPlantio = d
                , DataEncerramento = d.AddDays(45)
                , IdProduto = produtos.First().Id
                , Plantado = true
                , Situacao = Situacao.Colhido
                , IdEmpresa = 1
            });
            d = DateTime.UtcNow.AddDays(r.Next(1, 45));
            await Save(new LoteDeProducao
            {
                Quantidade = r.Next(100, 9999)
                , DataPlanejado = d
                , DataPlantio = d
                , IdProduto = produtos.Last().Id
                , Plantado = true
                , DataEncerramento = d.AddDays(7)
                , Situacao = Situacao.Descartado
                , IdEmpresa = 1
            });
        }
    }
}