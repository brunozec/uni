using System.ComponentModel.DataAnnotations;
using BFZ.AtividadeExtensionistaII.Domain.Implementations;
using BFZ.AtividadeExtensionistaII.Domain.Models;
using BFZ.AtividadeExtensionistaII.Viewmodels.Implementations;

namespace BFZ.AtividadeExtensionistaII.Viewmodels;

public class LoteDeProducaoViewModel : BaseViewModel
{
    private readonly LoteDeProducaoService _loteDeProducaoService;
    private readonly ProdutoService _produtoService;

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

    private int? _idProduto;

    [Required(ErrorMessage = "Obrigatório selecionar um produto")]
    public int? IdProduto
    {
        get => _idProduto;
        set
        {
            if (value == _idProduto) return;
            _idProduto = value;
            OnPropertyChanged();
        }
    }

    private decimal? _quantidade;

    [Required(ErrorMessage = "Obrigatório informar a quantidade")]
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

    private DateTime? _dataPlantio;

    public DateTime? DataPlantio
    {
        get => _dataPlantio;
        set
        {
            if (Nullable.Equals(value, _dataPlantio)) return;
            _dataPlantio = value;
            OnPropertyChanged();
        }
    }

    private DateTime? _dataPlanejado;

    [Required(ErrorMessage = "Obrigatório a data de previsão do plantio")]
    public DateTime? DataPlanejado
    {
        get => _dataPlanejado;
        set
        {
            if (Nullable.Equals(value, _dataPlanejado)) return;
            _dataPlanejado = value;
            OnPropertyChanged();
        }
    }

    private DateTime? _dataEncerramento;

    public DateTime? DataEncerramento
    {
        get => _dataEncerramento;
        set
        {
            if (Nullable.Equals(value, _dataEncerramento)) return;
            _dataEncerramento = value;
            OnPropertyChanged();
        }
    }

    private string? _observacao;

    public string? Observacao
    {
        get => _observacao;
        set
        {
            if (value == _observacao) return;
            _observacao = value;
            OnPropertyChanged();
        }
    }

    private Situacao _situacao;

    public Situacao Situacao
    {
        get => _situacao;
        set
        {
            if (value == _situacao) return;
            _situacao = value;
            OnPropertyChanged();
        }
    }

    public string SituacaoDescricao
    {
        get
        {
            switch (Situacao)
            {
                case Situacao.Planejado:
                    return "Planejado";
                case Situacao.EmProducao:
                    return "Em produção";
                case Situacao.Colhido:
                    return "Colhido";
                case Situacao.Doado:
                    return "Doado";
                case Situacao.Descartado:
                    return "Descartado";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    private int? _idEmpresa;

    public int? IdEmpresa
    {
        get => _idEmpresa;
        set
        {
            if (value == _idEmpresa) return;
            _idEmpresa = value;
            OnPropertyChanged();
        }
    }

    public LoteDeProducaoViewModel(
        LoteDeProducaoService loteDeProducaoService
        , ProdutoService produtoService)
    {
        _loteDeProducaoService = loteDeProducaoService;
        _produtoService = produtoService;
    }

    public async Task<LoteDeProducao> SaveAsync()
    {
        return await _loteDeProducaoService.Save(new LoteDeProducao
        {
            Id = Id
            , IdProduto = IdProduto
            , Quantidade = Quantidade
            , DataPlanejado = DataPlanejado
            , DataEncerramento = DataEncerramento
            , Observacao = Observacao
            , Situacao = Situacao
            , IdEmpresa = IdEmpresa
        });
    }

    public async Task<IEnumerable<LoteDeProducao>> GetAllAsync()
    {
        var produtos = await _produtoService.GetAll();

        var lotes = await _loteDeProducaoService.GetAll();

        var loteDeProducaos = lotes as LoteDeProducao[] ?? lotes.ToArray();

        foreach (var lote in loteDeProducaos)
        {
            lote.DescricaoProduto = produtos.First(f => f.Id == lote.IdProduto).Descricao;
        }

        return loteDeProducaos;
    }

    public Task AddDefaultAsync()
    {
        return _loteDeProducaoService.AddDefaultAsync();
    }
}