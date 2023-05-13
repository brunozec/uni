using BFZ.AtividadeExtensionistaII.Domain.Models;
using BFZ.AtividadeExtensionistaII.Viewmodels;
using BFZ.AtividadeExtensionistaII.Viewmodels.Implementations.Auth;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BFZ.AtividadeExtensionistaII.Pages.Cadastros;

public partial class UnidadeDeNegocio
{
    [Inject] private UnidadeDeNegocioViewModel UnidadeDeNegocioViewModel { get; set; }
    [Inject] private AuthenticationViewModel AuthenticationViewModel { get; set; }

    [Inject] private NavigationManager NavigationManager { get; set; }

    [Parameter]
    [SupplyParameterFromQuery(Name = "tipo")]
    public int Tipo { get; set; }

    public bool CheckBoxConfirmEntidadeCaridade { get; set; }

    bool _displayValidationErrorMessages = false;

    private async Task OnValidSubmit(
        EditContext arg)
    {
        _displayValidationErrorMessages = false;

        var empresa = await UnidadeDeNegocioViewModel.SaveAsync();

        AuthenticationViewModel.UserEmail = empresa.Email;
        AuthenticationViewModel.UserUnidadeDeNegocioId = (int)empresa.Id;
        AuthenticationViewModel.UserTipoUnidadeDeNegocio = empresa.Tipo;

        NavigationManager.NavigateTo("/");
    }

    private Task OnInvalidSubmit(
        EditContext arg)
    {
        _displayValidationErrorMessages = true;

        return Task.CompletedTask;
    }
}