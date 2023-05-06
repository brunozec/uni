using System.ComponentModel.DataAnnotations;
using BFZ.AtividadeExtensionistaII.Domain.Implementations;
using BFZ.AtividadeExtensionistaII.Domain.Models;
using BFZ.AtividadeExtensionistaII.Viewmodels.Implementations;

namespace BFZ.AtividadeExtensionistaII.Viewmodels;

public class AtividadeViewModel : BaseViewModel
{
    private readonly AtividadeService _atividadeService;
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

    private string _observacao;

    public string Observacao
    {
        get => _observacao;
        set
        {
            if (value == _observacao) return;
            _observacao = value;
            OnPropertyChanged();
        }
    }

    private DateTime? _data;

    [Required(ErrorMessage = "Obrigatório informar a data da atividade")]
    public DateTime? Data
    {
        get => _data;
        set
        {
            if (value.Equals(_data)) return;
            _data = value;
            OnPropertyChanged();
        }
    }

    private TipoAtividade _tipo;

    [Required(ErrorMessage = "Obrigatório selecionar o tipo da atividade")]
    public TipoAtividade Tipo
    {
        get => _tipo;
        set
        {
            if (value == _tipo) return;
            _tipo = value;
            OnPropertyChanged();
        }
    }

    private int _loteId;

    public int LoteId
    {
        get => _loteId;
        set
        {
            if (value == _loteId) return;
            _loteId = value;
            OnPropertyChanged();
        }
    }

    public AtividadeViewModel(
        AtividadeService atividadeService)
    {
        _atividadeService = atividadeService;
    }


    public async Task<Atividade> SaveAsync()
    {
        return await _atividadeService.Save(new Atividade
        {
            Id = Id
            , Observacao = Observacao
            , Data = Data
            , Tipo = Tipo
            , LoteId = LoteId
        });
    }

    public async Task<IEnumerable<Atividade>> GetAllByLoteAsync(
        int loteId)
    {
        return await _atividadeService.GetAllByLote(loteId);
    }
}