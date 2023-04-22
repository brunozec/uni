using BFZ.AtividadeExtensionistaII.Viewmodels.Implementations.Auth;
using Microsoft.AspNetCore.Components;

namespace BFZ.AtividadeExtensionistaII.Shared;

public partial class MainLayout
{
    [Inject] private AuthenticationViewModel AuthenticationViewModel { get; set; }
}