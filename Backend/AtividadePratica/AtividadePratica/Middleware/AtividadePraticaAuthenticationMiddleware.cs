using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace AtividadePratica.Middleware;

public class AtividadePraticaAuthenticationMiddleware : IMiddleware
{
    public async Task InvokeAsync(
        HttpContext context
        , RequestDelegate next)
    {
        var senha = "3672320";
        var usuario = "zeclhynscki";
        try
        {
            //carrega as informações de autenticação do header da requisição
            var authenticationHeader = AuthenticationHeaderValue.Parse(context.Request.Headers["Authorization"]);
            
            var credentialBytes = Convert.FromBase64String(authenticationHeader.Parameter);
            
            // carrega um array, separando os itens por :
            var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':', 2);
            
            var username = credentials[0];
            var password = credentials[1];

            // verifica se os valores estão corretos
            if (!username.Equals(usuario)
                || !senha.Equals(password))
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync("Usuário e/ou senha incorretos!");
                return;
            }
            
        }
        catch
        {
            //se aconteceu algum erro, retorna não autorizado
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            await context.Response.WriteAsync("Usuário e/ou senha incorretos!");
        }

        await next(context);
    }
}