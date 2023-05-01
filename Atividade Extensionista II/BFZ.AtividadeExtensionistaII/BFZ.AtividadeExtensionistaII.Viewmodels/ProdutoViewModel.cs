using BFZ.AtividadeExtensionistaII.Domain.Implementations;
using BFZ.AtividadeExtensionistaII.Domain.Models;
using BFZ.AtividadeExtensionistaII.Viewmodels.Implementations;

namespace BFZ.AtividadeExtensionistaII.Viewmodels;

public class ProdutoViewModel : BaseViewModel
{
    private readonly ProdutoService _produtoService;

    public ProdutoViewModel(
        ProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    public Task<Produto> GetByIdAsync(
        int id)
    {
        return _produtoService.Get(id);
    }

    public Task<IEnumerable<Produto>> GetAllAsync()
    {
        return _produtoService.GetAll();
    }

    public Task AddDefaultAsync()
    {
        return _produtoService.AddDefaultAsync();
    }
}