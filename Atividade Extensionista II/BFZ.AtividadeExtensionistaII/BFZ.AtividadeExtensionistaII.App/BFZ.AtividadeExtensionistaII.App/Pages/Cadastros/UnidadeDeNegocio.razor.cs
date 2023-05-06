using BFZ.AtividadeExtensionistaII.Viewmodels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BFZ.AtividadeExtensionistaII.App.Pages.Cadastros;

public partial class UnidadeDeNegocio
{
    [Inject] private UnidadeDeNegocioViewModel UnidadeDeNegocioViewModel { get; set; }

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

        await UnidadeDeNegocioViewModel.SaveAsync();

        NavigationManager.NavigateTo("/");
    }

    private Task OnInvalidSubmit(
        EditContext arg)
    {
        _displayValidationErrorMessages = true;

        return Task.CompletedTask;
    }
}