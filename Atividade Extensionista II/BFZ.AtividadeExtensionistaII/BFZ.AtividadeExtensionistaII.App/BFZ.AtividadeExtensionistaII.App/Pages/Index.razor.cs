using BFZ.AtividadeExtensionistaII.Domain.Models;
using BFZ.AtividadeExtensionistaII.Viewmodels;
using BFZ.AtividadeExtensionistaII.Viewmodels.Implementations.Auth;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BFZ.AtividadeExtensionistaII.App.Pages;

public partial class Index
{
    private bool _planejamentoDisplayValidationErrorMessages;
    private bool _atividadeDisplayValidationErrorMessages;

    [Inject] private AuthenticationViewModel AuthenticationViewModel { get; set; }

    [Inject] private LoteDeProducaoViewModel LoteDeProducaoViewModel { get; set; }

    [Inject] private AtividadeViewModel AtividadeViewModel { get; set; }

    [Inject] private ProdutoViewModel ProdutoViewModel { get; set; }

    [Inject] private NavigationManager NavigationManager { get; set; }

    public SfTab? SfTabObj { get; set; }

    public bool ShowNovoPlanejamento { get; set; }

    private bool _showNovaAtividadeModal;

    public bool ShowNovaAtividadeModal
    {
        get => _showNovaAtividadeModal;
        set
        {
            _showNovaAtividadeModal = value;
            InvokeAsync(StateHasChanged);
        }
    }

    public bool ShowPlanejamentoModal { get; set; }

    public LoteDeProducao SelectedLote { get; set; }
    public bool ShowAtividadesModal { get; set; }

    public SfDialog? PlanejamentoSfDialogObj { get; set; }
    public SfDialog? AtividadesSfDialogObj { get; set; }    
    public SfDialog? NovaAtividadesSfDialogObj { get; set; }

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

            AtividadeListaTipos = new List<AtividadeTipo>
            {
                new AtividadeTipo
                {
                    Id = (int)TipoAtividade.Plantio
                    , Descricao = "Plantio"
                }
                , new AtividadeTipo
                {
                    Id = (int)TipoAtividade.Colheita
                    , Descricao = "Colheita"
                }
                , new AtividadeTipo
                {
                    Id = (int)TipoAtividade.Doacao
                    , Descricao = "Doação"
                }
                , new AtividadeTipo
                {
                    Id = (int)TipoAtividade.AplicacaoDefensivo
                    , Descricao = "Aplicação de defensivo"
                }
                , new AtividadeTipo
                {
                    Id = (int)TipoAtividade.Descarte
                    , Descricao = "Descarte"
                }
            };
        }

        await base.OnAfterRenderAsync(firstRender);
    }

    public IEnumerable<LoteDeProducao> ListaLotes { get; set; }
    public IEnumerable<Atividade> ListaAtividades { get; set; }

    public IEnumerable<Produto> ListaProdutos { get; set; }
    public IEnumerable<AtividadeTipo> AtividadeListaTipos { get; set; }
    public EditContext PlanejamentoEditContext { get; set; }
    public EditContext AtividadeEditContext { get; set; }

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
            _planejamentoDisplayValidationErrorMessages = true;
        }
        else
        {
            _planejamentoDisplayValidationErrorMessages = false;

            ShowPlanejamentoModal = false;
        }

        return Task.CompletedTask;
    }

    private Task OnNovaAtividadeCancelarClicked()
    {
        ShowNovaAtividadeModal = false;
        return Task.CompletedTask;
    }

    private Task OnNovaAtividadeSalvarClicked()
    {
        if (!AtividadeEditContext.Validate())
        {
            _atividadeDisplayValidationErrorMessages = true;
        }
        else
        {
            _atividadeDisplayValidationErrorMessages = false;

            ShowNovaAtividadeModal = false;
        }

        return Task.CompletedTask;
    }


    private Task PlanejamentoHandleValidSubmit(
        EditContext arg)
    {
        _planejamentoDisplayValidationErrorMessages = false;
        return Task.CompletedTask;
    }

    private Task PlanejamentoOnInvalidSubmit(
        EditContext arg)
    {
        _planejamentoDisplayValidationErrorMessages = true;
        return Task.CompletedTask;
    }


    private Task AtividadeHandleValidSubmit(
        EditContext arg)
    {
        _atividadeDisplayValidationErrorMessages = false;
        ShowAtividadesModal = true;
        return Task.CompletedTask;
    }

    private Task AtividadeOnInvalidSubmit(
        EditContext arg)
    {
        _atividadeDisplayValidationErrorMessages = true;
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

    private async Task OnLoteProducaoSelected(
        RowSelectEventArgs<LoteDeProducao> arg)
    {
        if (arg?.Data?.Id != null)
        {
            ShowAtividadesModal = true;
            SelectedLote = arg.Data;

            ListaAtividades = await AtividadeViewModel.GetAllByLoteAsync((int)SelectedLote.Id);
        }
    }

    private Task OnAtividadesModalFecharClicked()
    {
        ShowAtividadesModal = false;
        return Task.CompletedTask;
    }

    private async Task OnNovaAtividade()
    {
        ShowAtividadesModal = false;
        await Task.Delay(100);
        AtividadeEditContext = new EditContext(AtividadeViewModel);
        ShowNovaAtividadeModal = true;
        
    }
}