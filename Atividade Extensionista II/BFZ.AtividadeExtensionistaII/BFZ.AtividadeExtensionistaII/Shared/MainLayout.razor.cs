using BFZ.AtividadeExtensionistaII.Common.Stores;
using Microsoft.AspNetCore.Components;

namespace BFZ.AtividadeExtensionistaII.Shared;

public partial class MainLayout
{
    [Inject] private AuthStore AuthStore { get; set; }
}