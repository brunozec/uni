﻿using BFZ.AtividadeExtensionistaII.Domain.Abstractions.UnidadesDeNegocios;
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
}