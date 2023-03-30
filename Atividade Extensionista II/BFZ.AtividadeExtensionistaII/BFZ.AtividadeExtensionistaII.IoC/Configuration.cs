using BFZ.AtividadeExtensionistaII.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BFZ.AtividadeExtensionistaII.IoC;

public static class Configuration
{
    public static void AddDepedencyInjection(this IServiceCollection service)
    {
       
        service.AddSingleton<RepositoryBase>();

    }
}