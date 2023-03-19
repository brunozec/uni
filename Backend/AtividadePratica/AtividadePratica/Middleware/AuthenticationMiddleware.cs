using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Net;

namespace AtividadePratica.Middleware;

public class AuthenticationMiddleware : IMiddleware
{
    public async Task InvokeAsync(
        HttpContext context
        , RequestDelegate next)
    {
        var endpoint = context.GetEndpoint();

        if (endpoint != null)
        {
            var allowAnonymousAttribute = endpoint.Metadata.GetMetadata<AllowAnonymousAttribute>();

            //Caso o metodo possua o atributo AllowAnonymousAttribute, não verifica as permissões de acesso ao metodo
            if (allowAnonymousAttribute == null) { }
        }

        await next(context);
    }
}