using System.ComponentModel.DataAnnotations;
using BFZ.AtividadeExtensionistaII.Domain.Implementations;
using BFZ.AtividadeExtensionistaII.Domain.Models;
using BFZ.AtividadeExtensionistaII.Viewmodels.Implementations;
using Force.DeepCloner;

namespace BFZ.AtividadeExtensionistaII.Viewmodels;

public class AtividadeViewModel : BaseViewModel
{
    private readonly AtividadeService _atividadeService;
    private readonly LoteDeProducaoService _loteDeProducaoService;
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

    private DateTime? _data = DateTime.Now;

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

    private decimal? _quantidade;

    public decimal? Quantidade
    {
        get => _quantidade;
        set
        {
            if (value == _quantidade) return;
            _quantidade = value;
            OnPropertyChanged();
        }
    }

    private int? _idEntidade;

    public int? IdEntidade
    {
        get => _idEntidade;
        set
        {
            if (value == _idEntidade) return;
            _idEntidade = value;
            OnPropertyChanged();
        }
    }

    private string? _nomeEntidade;

    public string? NomeEntidade
    {
        get => _nomeEntidade;
        set
        {
            if (value == _nomeEntidade) return;
            _nomeEntidade = value;
            OnPropertyChanged();
        }
    }

    public AtividadeViewModel(
        AtividadeService atividadeService
        , LoteDeProducaoService loteDeProducaoService
        , UnidadeDeNegocioService unidadeDeNegocioService)
    {
        _atividadeService = atividadeService;
        _loteDeProducaoService = loteDeProducaoService;
        _unidadeDeNegocioService = unidadeDeNegocioService;
    }


    public async Task<Atividade> SaveAsync()
    {
        var lote = await _loteDeProducaoService.Get(LoteId);

        var dividir = Quantidade != lote.Quantidade;

        if (dividir)
        {
            lote.Quantidade = Math.Abs((Quantidade ?? 0) - (decimal)lote.Quantidade);
            await _loteDeProducaoService.Save(lote);
        }

        var loteCloned = lote.DeepClone();

        if (dividir)
        {
            loteCloned.Id = null;
            loteCloned.Quantidade = Quantidade;
        }

        if (Tipo == TipoAtividade.Plantio)
        {
            lote.DataPlantio = Data;
            lote.Plantado = true;
            lote.Situacao = Situacao.EmProducao;
        }

        if (Tipo == TipoAtividade.Descarte)
        {
            loteCloned.DataEncerramento = Data;
            loteCloned.Situacao = Situacao.Descartado;
        }

        if (Tipo == TipoAtividade.Colheita)
        {
            loteCloned.DataEncerramento = Data;
            loteCloned.Situacao = Situacao.Colhido;
        }

        if (Tipo == TipoAtividade.Doacao)
        {
            loteCloned.DataEncerramento = Data;
            loteCloned.Situacao = Situacao.Doado;
        }

        if (!dividir)
            await _loteDeProducaoService.Save(lote);

        if (dividir)
            await _loteDeProducaoService.Save(loteCloned);

        if (IdEntidade != null)
        {
            var empresa = await _unidadeDeNegocioService.Get((int)IdEntidade);

            NomeEntidade = empresa.Nome;
        }

        return await _atividadeService.Save(new Atividade
        {
            Id = Id
            , Quantidade = Quantidade
            , Observacao = Observacao
            , IdEntidade = IdEntidade
            , NomeEntidade = NomeEntidade
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

    public async Task<IEnumerable<Atividade>> GetAllDoacoesAsync()
    {
        return await _atividadeService.GetAllDoacoesAsync();
    }
}