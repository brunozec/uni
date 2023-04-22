using BFZ.AtividadeExtensionistaII.Domain.Abstractions.UnidadesDeNegocios;
using BFZ.AtividadeExtensionistaII.Domain.Models;

namespace BFZ.AtividadeExtensionistaII.Viewmodels.Implementations.Auth;

public class AuthenticationViewModel : BaseViewModel
{
    private readonly IBaseRepository<UnidadeDeNegocio> _unidadeDeNegocioRepository;
    public AutenticationRequest AutenticationRequest { get; set; } = new AutenticationRequest();

    public string UserEmail { get; set; }

    public int UserUnidadeDeNegocioId { get; set; }

    public TipoUnidadeDeNegocio UserTipoUnidadeDeNegocio { get; set; }

    public bool UserIsAuthenticated => !string.IsNullOrWhiteSpace(UserEmail);

    public AuthenticationViewModel(
        IBaseRepository<UnidadeDeNegocio> unidadeDeNegocioRepository)
    {
        _unidadeDeNegocioRepository = unidadeDeNegocioRepository;
    }

    public async Task<bool> CheckLoggedUser()
    {
        var empresas = await _unidadeDeNegocioRepository.GetAllAsync();

        if (empresas.Any())
        {
            var empresa = empresas.First();
            UserEmail = empresa.Email;
            UserUnidadeDeNegocioId = (int)empresa.Id;
            UserTipoUnidadeDeNegocio = empresa.Tipo;
            
            return true;
        }

        return false;
    }

    public async Task<bool> Login()
    {
        var empresas = await _unidadeDeNegocioRepository.GetAllAsync();

        if (empresas.Any())
        {
            var empresa = empresas.First();

            if (empresa.Email.Equals(AutenticationRequest.Login, StringComparison.InvariantCultureIgnoreCase)
                && empresa.Senha.Equals(AutenticationRequest.Senha, StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }
        }

        return false;
    }
}