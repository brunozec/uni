using System.ComponentModel.DataAnnotations;
using BFZ.AtividadeExtensionistaII.Domain.Abstractions;
using Microsoft.Maui.Controls;
using SQLite;

namespace BFZ.AtividadeExtensionistaII.Domain.Models;

[Table("unidades_de_negocio")]
public class UnidadeDeNegocio : BindableObject, IEntityId
{
    private int? _id;

    [PrimaryKey]
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
}