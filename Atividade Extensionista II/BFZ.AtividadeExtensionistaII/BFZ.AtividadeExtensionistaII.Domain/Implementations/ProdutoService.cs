using BFZ.AtividadeExtensionistaII.Domain.Abstractions.UnidadesDeNegocios;
using BFZ.AtividadeExtensionistaII.Domain.Models;

namespace BFZ.AtividadeExtensionistaII.Domain.Implementations;

public class ProdutoService
{
    private readonly IBaseRepository<Produto> _repositoryBase;

    public ProdutoService(
        IBaseRepository<Produto> repositoryBase)
    {
        _repositoryBase = repositoryBase;
    }

    public async Task<Produto> Save(
        Produto item)
    {
        return await _repositoryBase.SaveAsync(item);
    }

    public async Task<Produto> Get(
        int id)
    {
        return await _repositoryBase.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Produto>> GetAll()
    {
        return await _repositoryBase.GetAllAsync();
    }

    public async Task AddDefaultAsync()
    {
        var produtos = await _repositoryBase.GetAllAsync();

        if (!produtos.Any(a => a.Descricao.Equals("Alface americana")))
        {
            await Save(new Produto
            {
                Descricao = "Alface americana"
                , TempoProducaoEmDias = 100
            });
        }

        if (!produtos.Any(a => a.Descricao.Equals("Alface lisa")))
        {
            await Save(new Produto
            {
                Descricao = "Alface lisa"
                , TempoProducaoEmDias = 50
            });
        }

        if (!produtos.Any(a => a.Descricao.Equals("Alface roxa")))
        {
            await Save(new Produto
            {
                Descricao = "Alface roxa"
                , TempoProducaoEmDias = 60
            });
        }

        if (!produtos.Any(a => a.Descricao.Equals("Alface crespa")))
        {
            await Save(new Produto
            {
                Descricao = "Alface crespa"
                , TempoProducaoEmDias = 25
            });
        }

        if (!produtos.Any(a => a.Descricao.Equals("Alface frisada")))
        {
            await Save(new Produto
            {
                Descricao = "Alface frisada"
                , TempoProducaoEmDias = 60
            });
        }

        if (!produtos.Any(a => a.Descricao.Equals("Alface romana")))
        {
            await Save(new Produto
            {
                Descricao = "Alface romana"
                , TempoProducaoEmDias = 70
            });
        }
    }
}