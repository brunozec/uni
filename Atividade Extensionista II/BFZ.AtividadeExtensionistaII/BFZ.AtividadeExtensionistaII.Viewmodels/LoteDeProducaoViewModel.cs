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

    [Required(ErrorMessage = "Obrigatório informar a quantidade")]
    public decimal Quantidade { get; set; }

    private DateTime? _dataPlantio;

    [Required(ErrorMessage = "Obrigatório a data de previsão do plantio")]
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
            , DataPlantio = DataPlantio
            , DataEncerramento = DataEncerramento
            , Observacao = Observacao
        });
    }

    public  async Task<IEnumerable<LoteDeProducao>> GetAllAsync()
    {
        var produtos = await _produtoService.GetAll();
        
        var lotes= await _loteDeProducaoService.GetAll();

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