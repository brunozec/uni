using BFZ.AtividadeExtensionistaII.Domain.Abstractions.UnidadesDeNegocios;
using BFZ.AtividadeExtensionistaII.Domain.Models;

namespace BFZ.AtividadeExtensionistaII.Viewmodels.Implementations.Auth;

public class AuthenticationViewModel : BaseViewModel
{
    private readonly IBaseRepository<UnidadeDeNegocio> _unidadeDeNegocioRepository;
    public AutenticationRequest AutenticationRequest { get; set; } = new AutenticationRequest();

    private string? _userEmail;

    public string? UserEmail
    {
        get => _userEmail;
        set
        {
            if (value == _userEmail) return;
            _userEmail = value;

            OnPropertyChanged();
        }
    }

    private int? _userUnidadeDeNegocioId;

    public int? UserUnidadeDeNegocioId
    {
        get => _userUnidadeDeNegocioId;
        set
        {
            if (value == _userUnidadeDeNegocioId) return;
            _userUnidadeDeNegocioId = value;
            OnPropertyChanged();
        }
    }

    private TipoUnidadeDeNegocio? _userTipoUnidadeDeNegocio;

    public TipoUnidadeDeNegocio? UserTipoUnidadeDeNegocio
    {
        get => _userTipoUnidadeDeNegocio;
        set
        {
            if (value == _userTipoUnidadeDeNegocio) return;
            _userTipoUnidadeDeNegocio = value;

            OnPropertyChanged();
        }
    }

    public bool IsAgricultorFamiliar => UserTipoUnidadeDeNegocio == TipoUnidadeDeNegocio.Agricultor;

    public bool UserIsAuthenticated => !string.IsNullOrWhiteSpace(UserEmail);

    public AuthenticationViewModel(
        IBaseRepository<UnidadeDeNegocio> unidadeDeNegocioRepository)
    {
        _unidadeDeNegocioRepository = unidadeDeNegocioRepository;
    }

    public bool CheckLoggedUser()
    {
        if (!string.IsNullOrWhiteSpace(UserEmail))
        {
            return true;
        }

        return false;
    }

    public void Logout()
    {
        UserEmail = null;
        UserUnidadeDeNegocioId = null;
        UserTipoUnidadeDeNegocio = null;
    }

    public async Task<bool> Login()
    {
        var empresas = await _unidadeDeNegocioRepository.GetAllAsync();

        foreach (var empresa in empresas)
        {
            if (empresa.Email.Equals(AutenticationRequest.Login, StringComparison.InvariantCultureIgnoreCase)
                && empresa.Senha.Equals(AutenticationRequest.Senha, StringComparison.InvariantCultureIgnoreCase))
            {
                UserEmail = empresa.Email;
                UserUnidadeDeNegocioId = (int)empresa.Id;
                UserTipoUnidadeDeNegocio = empresa.Tipo;
                return true;
            }
        }

        return false;
    }
}