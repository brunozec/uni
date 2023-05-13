using BFZ.AtividadeExtensionistaII.Domain.Implementations;
using BFZ.AtividadeExtensionistaII.Domain.Models;
using BFZ.AtividadeExtensionistaII.Viewmodels;
using BFZ.AtividadeExtensionistaII.Viewmodels.Implementations.Auth;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Navigations;
using Syncfusion.Blazor.Popups;

namespace BFZ.AtividadeExtensionistaII.Pages;

public partial class Index
{
    private bool _planejamentoDisplayValidationErrorMessages;
    private bool _atividadeDisplayValidationErrorMessages;

    [Inject] private AuthenticationViewModel AuthenticationViewModel { get; set; }

    [Inject] private LoteDeProducaoViewModel LoteDeProducaoViewModel { get; set; }

    [Inject] private AtividadeViewModel AtividadeViewModel { get; set; }

    [Inject] private ProdutoViewModel ProdutoViewModel { get; set; }
    [Inject] private UnidadeDeNegocioViewModel UnidadeDeNegocioViewModel { get; set; }

    [Inject] private NavigationManager NavigationManager { get; set; }

    public SfTab? SfTabObj { get; set; }

    public bool ShowNovoPlanejamento { get; set; } = true;

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
    public SfDialog? DoacaoSfDialogObj { get; set; }
    public SfDialog? NovaAtividadesSfDialogObj { get; set; }

    protected override async Task OnAfterRenderAsync(
        bool firstRender)
    {
        if (firstRender) { }

        await base.OnAfterRenderAsync(firstRender);
    }

    private void PopulateAtividadeListaTipos(
        bool addPlantio
        , bool addDescarte)
    {
        AtividadeListaTipos = new List<AtividadeTipo>();

        if (addPlantio)
            AtividadeListaTipos.Add(new AtividadeTipo
                {
                    Id = TipoAtividade.Plantio
                    , Descricao = "Plantio"
                }
            );

        if (!addPlantio)
            AtividadeListaTipos.Add(new AtividadeTipo
                {
                    Id = TipoAtividade.Colheita
                    , Descricao = "Colheita"
                }
            );

        if (!addPlantio)
            AtividadeListaTipos.Add(new AtividadeTipo
                {
                    Id = TipoAtividade.Doacao
                    , Descricao = "Doação"
                }
            );

        if (!addPlantio)
            AtividadeListaTipos.Add(new AtividadeTipo
                {
                    Id = TipoAtividade.AplicacaoDefensivo
                    , Descricao = "Aplicação de defensivo"
                }
            );

        if (!addPlantio && addDescarte)
            AtividadeListaTipos.Add(new AtividadeTipo
                {
                    Id = TipoAtividade.Descarte
                    , Descricao = "Descarte"
                }
            );
    }

    public IEnumerable<Atividade> ListaDoacoes { get; set; }
    public IEnumerable<LoteDeProducao> ListaLotes { get; set; }
    public IEnumerable<Atividade> ListaAtividades { get; set; }

    public IEnumerable<Produto> ListaProdutos { get; set; }
    public IEnumerable<UnidadeDeNegocio> ListaEntidades { get; set; }
    public IEnumerable<UnidadeDeNegocio> ListaEmpresas { get; set; }
    public IList<AtividadeTipo> AtividadeListaTipos { get; set; }
    public EditContext PlanejamentoEditContext { get; set; }
    public EditContext AtividadeEditContext { get; set; }

    private Task OnNovoPlanejamento()
    {
        PlanejamentoEditContext = new EditContext(LoteDeProducaoViewModel);

        LoteDeProducaoViewModel.DataPlanejado = DateTime.Now;
        LoteDeProducaoViewModel.Quantidade = 0;
        LoteDeProducaoViewModel.Observacao = string.Empty;
        LoteDeProducaoViewModel.IdProduto = null;

        ShowPlanejamentoModal = true;

        return Task.CompletedTask;
    }

    private async Task OnTabSelected(
        SelectEventArgs arg)
    {
        ShowNovoPlanejamento = arg.SelectedIndex == 0;

        await PopulateLists();
    }

    private Task OnPlanejamentoCancelarClicked()
    {
        ShowPlanejamentoModal = false;
        return Task.CompletedTask;
    }

    private async Task OnPlanejamentoSalvarClicked()
    {
        if (!PlanejamentoEditContext.Validate())
        {
            _planejamentoDisplayValidationErrorMessages = true;
        }
        else
        {
            _planejamentoDisplayValidationErrorMessages = false;

            LoteDeProducaoViewModel.IdEmpresa = AuthenticationViewModel.UserUnidadeDeNegocioId;
            await LoteDeProducaoViewModel.SaveAsync();
            await PopulateLists();
            ShowPlanejamentoModal = false;
        }
    }

    private Task OnNovaAtividadeCancelarClicked()
    {
        ShowNovaAtividadeModal = false;
        return Task.CompletedTask;
    }

    private async Task OnNovaAtividadeSalvarClicked()
    {
        if (!AtividadeEditContext.Validate())
        {
            _atividadeDisplayValidationErrorMessages = true;
        }
        else
        {
            _atividadeDisplayValidationErrorMessages = false;

            await AtividadeViewModel.SaveAsync();

            ShowNovaAtividadeModal = false;
            ListaAtividades = await AtividadeViewModel.GetAllByLoteAsync((int)SelectedLote.Id);
            ListaLotes = await LoteDeProducaoViewModel.GetAllAsync();
            await PopulateLists();
            ShowAtividadesModal = true;
        }
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

    private Task OnNovaAtividade()
    {
        ShowAtividadesModal = false;

        AtividadeEditContext = new EditContext(AtividadeViewModel);
        AtividadeViewModel.LoteId = (int)SelectedLote.Id;

        AtividadeViewModel.Data = SelectedLote.Plantado ? DateTime.Now : SelectedLote.DataPlanejado;

        AtividadeViewModel.Observacao = string.Empty;
        AtividadeViewModel.Tipo = SelectedLote.Plantado ? TipoAtividade.Doacao : TipoAtividade.Plantio;
        AtividadeViewModel.Quantidade = SelectedLote.Quantidade;

        PopulateAtividadeListaTipos(SelectedLote.Situacao is Situacao.Planejado, SelectedLote.Situacao is Situacao.EmProducao);

        ShowNovaAtividadeModal = true;
        return Task.CompletedTask;
    }

    private async Task OnTabCreated()
    {
        await PopulateLists();
    }

    private async Task PopulateLists()
    {
        await ProdutoViewModel.AddDefaultAsync();
        await LoteDeProducaoViewModel.AddDefaultAsync();
        await UnidadeDeNegocioViewModel.AddDefaultAsync();

        ListaEntidades = await UnidadeDeNegocioViewModel.GetAllEntidadesAsync();
        ListaProdutos = await ProdutoViewModel.GetAllAsync();
        ListaLotes = await LoteDeProducaoViewModel.GetAllAsync();
        PlanejamentoEditContext = new EditContext(LoteDeProducaoViewModel);

        ListaDoacoes = await AtividadeViewModel.GetAllDoacoesAsync();

        PopulateAtividadeListaTipos(true, true);
    }

    private async Task OnDoacaoSelected(
        RowSelectEventArgs<Atividade> arg)
    {
        if (arg?.Data?.Id != null)
        {
            ShowDoacaoModal = true;
            SelectedDoacao = arg.Data;


            var lote = (await LoteDeProducaoViewModel.GetAllAsync()).First(f => f.Id == SelectedDoacao.LoteId);

            SelectedDoacaoEmpresa = (await UnidadeDeNegocioViewModel.GetAllAsync()).First(f => f.Id == lote.IdEmpresa);

            SelectedDoacao.IdEmpresa = SelectedDoacaoEmpresa.Id;
            SelectedDoacao.NomeEmpresa = SelectedDoacaoEmpresa.Nome;
        }
    }

    public UnidadeDeNegocio? SelectedDoacaoEmpresa { get; set; }
    public Atividade? SelectedDoacao { get; set; }

    public bool ShowDoacaoModal { get; set; }

    private Task OnDoacaoModalFecharClicked()
    {
        ShowDoacaoModal = false;
        SelectedDoacao = null;

        return Task.CompletedTask;
    }
}