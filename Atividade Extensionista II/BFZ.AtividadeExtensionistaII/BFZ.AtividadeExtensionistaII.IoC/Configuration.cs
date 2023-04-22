﻿using BFZ.AtividadeExtensionistaII.Common;
using BFZ.AtividadeExtensionistaII.Domain.Abstractions.UnidadesDeNegocios;
using BFZ.AtividadeExtensionistaII.Domain.Implementations;
using BFZ.AtividadeExtensionistaII.Domain.Models;
using BFZ.AtividadeExtensionistaII.Repositories;
using BFZ.AtividadeExtensionistaII.Viewmodels;
using BFZ.AtividadeExtensionistaII.Viewmodels.Implementations.Auth;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;

namespace BFZ.AtividadeExtensionistaII.IoC;

public static class Configuration
{
    public static void AddDepedencyInjection(
        this IServiceCollection service)
    {
        service.AddSingleton(typeof(ISyncfusionStringLocalizer), typeof(SyncfusionLocalizer));


        #region viewmodels

        service.AddSingleton<AuthenticationViewModel>();
        service.AddSingleton<UnidadeDeNegocioViewModel>();

        #endregion

        #region services

        service.AddSingleton<UnidadeDeNegocioService>();

        #endregion

        #region repositories

        service.AddSingleton<RepositoryBase>();
        service.AddSingleton<IBaseRepository<UnidadeDeNegocio>, RepositoryBase<UnidadeDeNegocio>>();

        #endregion
    }
}