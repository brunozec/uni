using BFZ.AtividadeExtensionistaII.Viewmodels.Implementations.Auth;
using Microsoft.AspNetCore.Components;

namespace BFZ.AtividadeExtensionistaII;

public partial class Main
{
    [Inject] private AuthenticationViewModel AuthenticationViewModel { get; set; }

    public Main() { }
}