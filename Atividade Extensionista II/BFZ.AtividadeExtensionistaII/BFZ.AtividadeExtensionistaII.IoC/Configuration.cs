using BFZ.AtividadeExtensionistaII.Common;
using BFZ.AtividadeExtensionistaII.Common.Stores;
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
        
        #region store

        service.AddSingleton<AuthStore>();

        #endregion

        #region viewmodels

        service.AddSingleton<AuthenticationViewModel>();
        service.AddSingleton<UnidadeDeNegocioViewModel>();

        #endregion

        #region services

        #endregion

        #region repositories

        service.AddSingleton<RepositoryBase>();

        #endregion
    }
}