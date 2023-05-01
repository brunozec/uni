using BFZ.AtividadeExtensionistaII.Common;
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
        service.AddSingleton<ProdutoViewModel>();
        service.AddSingleton<AtividadeViewModel>();
        service.AddSingleton<LoteDeProducaoViewModel>();

        #endregion

        #region services

        service.AddSingleton<UnidadeDeNegocioService>();
        service.AddSingleton<LoteDeProducaoService>();
        service.AddSingleton<ProdutoService>();

        #endregion

        #region repositories

        service.AddSingleton<RepositoryBase>();
        service.AddSingleton<IBaseRepository<UnidadeDeNegocio>, RepositoryBase<UnidadeDeNegocio>>();
        service.AddSingleton<IBaseRepository<LoteDeProducao>, RepositoryBase<LoteDeProducao>>();
        service.AddSingleton<IBaseRepository<Atividade>, RepositoryBase<Atividade>>();
        service.AddSingleton<IBaseRepository<Produto>, RepositoryBase<Produto>>();

        #endregion
    }
}