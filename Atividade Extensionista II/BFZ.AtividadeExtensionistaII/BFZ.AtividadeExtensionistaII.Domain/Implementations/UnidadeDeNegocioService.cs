using BFZ.AtividadeExtensionistaII.Domain.Abstractions.UnidadesDeNegocios;
using BFZ.AtividadeExtensionistaII.Domain.Models;

namespace BFZ.AtividadeExtensionistaII.Domain.Implementations;

public class UnidadeDeNegocioService
{
    private readonly IBaseRepository<UnidadeDeNegocio> _repositoryBase;

    public UnidadeDeNegocioService(
        IBaseRepository<UnidadeDeNegocio> repositoryBase)
    {
        _repositoryBase = repositoryBase;
    }

    public async Task<UnidadeDeNegocio> Save(
        UnidadeDeNegocio item)
    {
        return await _repositoryBase.SaveAsync(item);
    }

    public async Task<UnidadeDeNegocio> Get(
        int id)
    {
        return await _repositoryBase.GetByIdAsync(id);
    }

    public async Task<IEnumerable<UnidadeDeNegocio>> GetAllEntidadesAsync()
    {
        var empresas = await _repositoryBase.GetAllAsync();

        return empresas.Where(w => w.Tipo is TipoUnidadeDeNegocio.EntidadeCaridade);
    }

    public async Task AddDefaultAsync()
    {
        var empresas = await _repositoryBase.GetAllAsync();

        if (!empresas.Any() || empresas.Count() == 1)
        {
            await Save(new UnidadeDeNegocio()
            {
                Tipo = TipoUnidadeDeNegocio.EntidadeCaridade
                , Nome = "FUNDAÇÃO LUZ DO DIA"
                , Email = "fundacao@luzdodia.com.br"
                , Senha = "1234"
            });
            await Save(new UnidadeDeNegocio
            {
                Tipo = TipoUnidadeDeNegocio.EntidadeCaridade
                , Nome = "SEMPRE MAIS AMOR"
                , Email = "sempre@maisamor.com.br"
                , Senha = "1234"
            });
            await Save(new UnidadeDeNegocio
            {
                Tipo = TipoUnidadeDeNegocio.EntidadeCaridade
                , Nome = "ASSOCIAÇÃO DE MORADORES DO JARDIM..."
            });
        }
    }

    public async Task<IEnumerable<UnidadeDeNegocio>> GetAllAsync()
    {
        return await _repositoryBase.GetAllAsync();
    }
}