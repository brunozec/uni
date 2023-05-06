using BFZ.AtividadeExtensionistaII.Domain.Models;
using BFZ.AtividadeExtensionistaII.Viewmodels;
using Microsoft.AspNetCore.Components;

namespace BFZ.AtividadeExtensionistaII.App.Pages.Producao;

public partial class Planejamento
{
    [Inject] private LoteDeProducaoViewModel ViewModel { get; set; }
    [Inject] private ProdutoViewModel ProdutoViewModel { get; set; }
    
    [Inject] private LoteDeProducaoViewModel LoteDeProducaoViewModel { get; set; }
    
    [Inject] private NavigationManager NavigationManager { get; set; }

    public IEnumerable<Produto> ListaProdutos { get; set; }
    public IEnumerable<LoteDeProducao> ListaLotes { get; set; }

    public Planejamento() { }

    protected override async Task OnAfterRenderAsync(
        bool firstRender)
    {
        if (firstRender)
        {
            await ProdutoViewModel.AddDefaultAsync();

            ListaProdutos = await ProdutoViewModel.GetAllAsync();
            ListaLotes = await LoteDeProducaoViewModel.GetAllAsync();
        }

        await base.OnAfterRenderAsync(firstRender);
    }

    private Task OnCancelar()
    {
        App.Current.MainPage.Navigation.PopAsync(false);
        return Task.CompletedTask;
    }

    private Task OnSalvar()
    {
        App.Current.MainPage.Navigation.PopAsync(false);
        return Task.CompletedTask;
    }
}