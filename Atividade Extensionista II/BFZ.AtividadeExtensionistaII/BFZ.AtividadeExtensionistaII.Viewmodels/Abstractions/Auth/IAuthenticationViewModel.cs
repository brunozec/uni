using BFZ.AtividadeExtensionistaII.Domain.Models;

namespace BFZ.AtividadeExtensionistaII.Viewmodels.Abstractions.Auth;

public interface IAuthenticationViewModel : IBaseViewModel
{
    public AutenticationRequest AutenticationRequest { get; set; }
}   