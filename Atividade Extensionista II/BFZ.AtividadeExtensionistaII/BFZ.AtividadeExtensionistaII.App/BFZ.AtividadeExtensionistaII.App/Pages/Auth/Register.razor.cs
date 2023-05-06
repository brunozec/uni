using BFZ.AtividadeExtensionistaII.Domain.Models;
using BFZ.AtividadeExtensionistaII.Viewmodels.Implementations.Auth;
using Microsoft.AspNetCore.Components;

namespace BFZ.AtividadeExtensionistaII.App.Pages.Auth;

public partial class Register
{
    [Inject] private AuthenticationViewModel AuthenticationViewModel { get; set; }

    [Inject] private NavigationManager NavigationManager { get; set; }

    private string Error { get; set; }

    private Task OnClick(
        TipoUnidadeDeNegocio tipo)
    {
        NavigationManager.NavigateTo($"/UnidadeDeNegocio?tipo={(int)tipo}");

        return Task.CompletedTask;
    }
}