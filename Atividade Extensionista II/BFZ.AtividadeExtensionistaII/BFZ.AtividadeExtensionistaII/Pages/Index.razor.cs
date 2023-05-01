using BFZ.AtividadeExtensionistaII.Domain.Models;
using BFZ.AtividadeExtensionistaII.Pages.Producao;
using BFZ.AtividadeExtensionistaII.PagesMaui;
using BFZ.AtividadeExtensionistaII.Repositories;
using BFZ.AtividadeExtensionistaII.Viewmodels;
using BFZ.AtividadeExtensionistaII.Viewmodels.Implementations.Auth;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Syncfusion.Blazor.Navigations;
using Syncfusion.Blazor.Popups;

namespace BFZ.AtividadeExtensionistaII.Pages;

public partial class Index
{
    private bool _displayValidationErrorMessages;

    [Inject] private AuthenticationViewModel AuthenticationViewModel { get; set; }

    [Inject] private LoteDeProducaoViewModel LoteDeProducaoViewModel { get; set; }

    [Inject] private ProdutoViewModel ProdutoViewModel { get; set; }

    [Inject] private NavigationManager NavigationManager { get; set; }

    public SfTab? SfTabObj { get; set; }

    public bool ShowNovoPlanejamento { get; set; }

    public bool ShowPlanejamentoModal { get; set; }

    public SfDialog? SfDialogObj { get; set; }

    protected override async Task OnAfterRenderAsync(
        bool firstRender)
    {
        if (firstRender)
        {
            await ProdutoViewModel.AddDefaultAsync();
            await LoteDeProducaoViewModel.AddDefaultAsync();

            ListaProdutos = await ProdutoViewModel.GetAllAsync();
            ListaLotes = await LoteDeProducaoViewModel.GetAllAsync();
            PlanejamentoEditContext = new EditContext(LoteDeProducaoViewModel);
        }

        await base.OnAfterRenderAsync(firstRender);
    }

    public IEnumerable<LoteDeProducao> ListaLotes { get; set; }

    public IEnumerable<Produto> ListaProdutos { get; set; }
    public EditContext PlanejamentoEditContext { get; set; }

    private Task OnNovoPlanejamento()
    {
        PlanejamentoEditContext = new EditContext(LoteDeProducaoViewModel);
        ShowPlanejamentoModal = true;

        return Task.CompletedTask;
    }

    private Task OnTabSelected(
        SelectEventArgs arg)
    {
        ShowNovoPlanejamento = arg.SelectedIndex == 1;

        return Task.CompletedTask;
    }

    private Task OnCancelarClicked()
    {
        ShowPlanejamentoModal = false;
        return Task.CompletedTask;
    }

    private Task OnSalvarClicked()
    {
        if (!PlanejamentoEditContext.Validate())
        {
            _displayValidationErrorMessages = true;
        }
        else
        {
            _displayValidationErrorMessages = false;

            ShowPlanejamentoModal = false;
        }

        return Task.CompletedTask;
    }


    private Task HandleValidSubmit(
        EditContext arg)
    {
        _displayValidationErrorMessages = false;
        return Task.CompletedTask;
    }

    private Task OnInvalidSubmit(
        EditContext arg)
    {
        _displayValidationErrorMessages = true;
        return Task.CompletedTask;
    }
    public void OnTabSelecting(
        SelectingEventArgs args)
    {
        if (args.IsSwiped)
        {
            args.Cancel = true;
        }
    }
}