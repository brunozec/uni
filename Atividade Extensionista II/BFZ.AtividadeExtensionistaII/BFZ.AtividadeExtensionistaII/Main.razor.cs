using BFZ.AtividadeExtensionistaII.Common.Stores;
using Microsoft.AspNetCore.Components;

namespace BFZ.AtividadeExtensionistaII;

public partial class Main
{
    [Inject] private AuthStore AuthStore { get; set; }

    public Main() { }
}