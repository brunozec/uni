using BFZ.AtividadeExtensionistaII.Repositories;
using BFZ.AtividadeExtensionistaII.Viewmodels.Implementations.Auth;
using Microsoft.AspNetCore.Components;

namespace BFZ.AtividadeExtensionistaII.Pages;

public partial class Index
{
    [Inject] private AuthenticationViewModel AuthenticationViewModel { get; set; }

    protected override async Task OnAfterRenderAsync(
        bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
    }
}