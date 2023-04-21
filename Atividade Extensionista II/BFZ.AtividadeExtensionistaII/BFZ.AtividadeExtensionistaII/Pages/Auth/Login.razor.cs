using BFZ.AtividadeExtensionistaII.Common.Stores;
using BFZ.AtividadeExtensionistaII.Viewmodels.Implementations.Auth;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;

namespace BFZ.AtividadeExtensionistaII.Pages.Auth;

public partial class Login
{
    [Inject] private LoginViewModel LoginViewModel { get; set; }

    [Inject] private AuthStore AuthStore { get; set; }

    private string Error { get; set; }

    private Task HandleValidSubmit(
        EditContext arg)
    {
        throw new NotImplementedException();
    }

}