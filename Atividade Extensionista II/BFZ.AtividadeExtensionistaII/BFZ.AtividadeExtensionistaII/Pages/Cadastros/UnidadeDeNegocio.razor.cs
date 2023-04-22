using BFZ.AtividadeExtensionistaII.Domain.Models;
using BFZ.AtividadeExtensionistaII.Viewmodels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BFZ.AtividadeExtensionistaII.Pages.Cadastros;

public partial class UnidadeDeNegocio
{
    [Inject] private UnidadeDeNegocioViewModel UnidadeDeNegocioViewModel { get; set; }

    [Parameter]
    [SupplyParameterFromQuery(Name = "tipo")]
    public int Tipo { get; set; }

    public bool CheckBoxConfirmEntidadeCaridade { get; set; }

    bool _displayValidationErrorMessages = false;

    private Task OnValidSubmit(
        EditContext arg)
    {
        _displayValidationErrorMessages = false;

        if (UnidadeDeNegocioViewModel.CurrentItem.Id == null) { }

        return Task.CompletedTask;
    }

    private Task OnInvalidSubmit(
        EditContext arg)
    {
        _displayValidationErrorMessages = true;

        return Task.CompletedTask;
    }
}