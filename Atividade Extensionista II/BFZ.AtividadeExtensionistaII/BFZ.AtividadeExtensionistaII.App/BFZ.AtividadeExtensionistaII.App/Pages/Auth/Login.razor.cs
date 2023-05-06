using BFZ.AtividadeExtensionistaII.Domain.Implementations;
using BFZ.AtividadeExtensionistaII.Repositories;
using BFZ.AtividadeExtensionistaII.Viewmodels.Implementations.Auth;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BFZ.AtividadeExtensionistaII.App.Pages.Auth;

public partial class Login
{
    private bool _displayValidationErrorMessages;

    [Inject] private AuthenticationViewModel AuthenticationViewModel { get; set; }

    [Inject] private NavigationManager NavigationManager { get; set; }

    [Inject] private UnidadeDeNegocioService UnidadeDeNegocioService { get; set; }

    [Inject] private RepositoryBase RepositoryBase { get; set; }

    private string Error { get; set; }

    protected override async Task OnAfterRenderAsync(
        bool firstRender)
    {
        await CheckUserLoggedIn();

        await base.OnAfterRenderAsync(firstRender);
    }

    private async Task CheckUserLoggedIn()
    {
        if (await AuthenticationViewModel.CheckLoggedUser())
        {
            App.Current.MainPage.Navigation.PushAsync(new IndexPage(), false);
        }
    }

    private Task HandleValidSubmit(
        EditContext arg)
    {
        _displayValidationErrorMessages = false;

        return Task.CompletedTask;
    }

    private async Task OnInvalidSubmit(
        EditContext arg)
    {
        _displayValidationErrorMessages = true;

        if (await AuthenticationViewModel.Login())
        {
            App.Current.MainPage.Navigation.PushAsync(new IndexPage(), false);
        }
    }
}