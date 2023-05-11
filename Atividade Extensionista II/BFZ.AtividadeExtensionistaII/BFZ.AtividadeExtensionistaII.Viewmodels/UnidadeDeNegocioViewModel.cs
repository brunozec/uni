using System.ComponentModel.DataAnnotations;
using BFZ.AtividadeExtensionistaII.Domain.Implementations;
using BFZ.AtividadeExtensionistaII.Domain.Models;
using BFZ.AtividadeExtensionistaII.Viewmodels.Implementations;

namespace BFZ.AtividadeExtensionistaII.Viewmodels;

public class UnidadeDeNegocioViewModel : BaseViewModel
{
    private readonly UnidadeDeNegocioService _unidadeDeNegocioService;
    private int? _id;

    public int? Id
    {
        get => _id;
        set
        {
            if (value == _id) return;
            _id = value;
            OnPropertyChanged();
        }
    }

    private string _nome;

    [Required(ErrorMessage = "Campo {0} obrigatório")]
    public string Nome
    {
        get => _nome;
        set
        {
            if (value == _nome) return;
            _nome = value;
            OnPropertyChanged();
        }
    }

    private string _email;

    [EmailAddress]
    [Required(ErrorMessage = "Campo {0} obrigatório")]
    public string Email
    {
        get => _email;
        set
        {
            if (value == _email) return;
            _email = value;
            OnPropertyChanged();
        }
    }

    private string _fone;

    [Required(ErrorMessage = "Campo {0} obrigatório")]
    public string Fone
    {
        get => _fone;
        set
        {
            if (value == _fone) return;
            _fone = value;
            OnPropertyChanged();
        }
    }

    private string _enderecoCep;

    [Required(ErrorMessage = "Campo {0} obrigatório")]
    public string EnderecoCep
    {
        get => _enderecoCep;
        set
        {
            if (value == _enderecoCep) return;
            _enderecoCep = value;
            OnPropertyChanged();
        }
    }

    private string _enderecoNumero;

    [Required(ErrorMessage = "Campo {0} obrigatório")]
    public string EnderecoNumero
    {
        get => _enderecoNumero;
        set
        {
            if (value == _enderecoNumero) return;
            _enderecoNumero = value;
            OnPropertyChanged();
        }
    }

    private TipoUnidadeDeNegocio _tipo;

    [Required(ErrorMessage = "Campo {0} obrigatório")]
    public TipoUnidadeDeNegocio Tipo
    {
        get => _tipo;
        set
        {
            if (value == _tipo) return;
            _tipo = value;
            OnPropertyChanged();
        }
    }

    private string _senha;

    [Required(ErrorMessage = "Campo {0} obrigatório")]
    public string Senha
    {
        get => _senha;
        set
        {
            if (value == _senha) return;
            _senha = value;
            OnPropertyChanged();
        }
    }

    private string _senhaConfirmacao;

    [Compare(nameof(Senha), ErrorMessage = "Senha diferente da informada")]
    [Required(ErrorMessage = "Campo {0} obrigatório")]
    public string SenhaConfirmacao
    {
        get => _senhaConfirmacao;
        set
        {
            if (value == _senhaConfirmacao) return;
            _senhaConfirmacao = value;
            OnPropertyChanged();
        }
    }


    public UnidadeDeNegocioViewModel(
        UnidadeDeNegocioService unidadeDeNegocioService)
    {
        _unidadeDeNegocioService = unidadeDeNegocioService;
    }

    public async Task<UnidadeDeNegocio> SaveAsync()
    {
        return await _unidadeDeNegocioService.Save(new UnidadeDeNegocio
        {
            Id = Id
            , Email = Email
            , Fone = Fone
            , Nome = Nome
            , Senha = Senha
            , Tipo = Tipo
            , EnderecoCep = EnderecoCep
            , EnderecoNumero = EnderecoNumero
        });
    }

    public async Task<IEnumerable<UnidadeDeNegocio>> GetAllEntidadesAsync()
    {
        return await _unidadeDeNegocioService.GetAllEntidadesAsync();
    }

    public  Task AddDefaultAsync()
    {
        return _unidadeDeNegocioService.AddDefaultAsync();
    }
}