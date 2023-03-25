using AtividadePratica.Data;
using AtividadePratica.Domain;
using Microsoft.AspNetCore.Mvc;

namespace AtividadePratica.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LivroController : ControllerBase
{
    private readonly ILivroRepository _livroRepository;

    /// <summary>
    /// Controller para ações com objeto do livro, fazendo injeção de dependecia do repositorio de livros
    /// </summary>
    /// <param name="livroRepository"></param>
    public LivroController(
        ILivroRepository livroRepository)
    {
        _livroRepository = livroRepository;
    }

    /// <summary>
    /// Insere um novo livro
    /// </summary>
    /// <param name="livro">Objeto contendo as informações do livro</param>
    /// <returns>Objeto do livro que está salvo no banco de dados</returns>
    [HttpPost]
    [Route("insert")]
    public async Task<ActionResult> InsertAsync(
        [FromBody] Livro? livro)
    {
        try
        {
            //verifica se está foi enviado o objeto do livro
            if (livro == null)
                return new BadRequestObjectResult("Informe os dados do livro");

            //verifica se já existe um livro com o id cadastrado
            var livroComId = await _livroRepository.GetByIdAsync((int)livro.Id);

            //caso exista um livro com id cadastrado, retorna mensagem de ja cadastrado
            if (livroComId != null)
            {
                return new BadRequestObjectResult($"Livro com Id {livro.Id} já cadastrado!");
            }

            //chama o repositorio para salvar o livro
            var livroSalvo = await _livroRepository.InsertAsync(livro);

            //retorna 200 com sucesso
            return new OkObjectResult(livroSalvo);
        }
        catch (Exception e)
        {
            //retorna mensagem de erro
            return new BadRequestObjectResult(e.Message);
        }
    }

    /// <summary>
    /// Atualiza um livro ja existente
    /// </summary>
    /// <param name="livro">Objeto contendo as onformações do livro</param>
    /// <returns>Objeto do livro atualizado que está salvo no banco de dados</returns>
    [HttpPut]
    [Route("update")]
    public async Task<ActionResult> UpdateAsync(
        [FromBody] Livro livro)
    {
        try
        {
            //verifica se está foi enviado o objeto do livro
            if (livro == null)
                return new BadRequestObjectResult("Informe os dados do livro");

            //verifica se já existe um livro com o id cadastrado
            var livroComId = await _livroRepository.GetByIdAsync((int)livro.Id);

            //caso não exista um livro com id cadastrado, retorna mensagem de não encontrado
            if (livroComId == null)
            {
                return new NotFoundObjectResult($"Livro com Id {livro.Id} não encontrado!");
            }

            //chama o repositorio para atualizar o livro
            var livroSalvo = await _livroRepository.UpdateAsync(livro);

            //retorna 200 com sucesso
            return new OkObjectResult(livroSalvo);
        }
        catch (Exception e)
        {
            //retorna mensagem de erro
            return new BadRequestObjectResult(e.Message);
        }
    }

    /// <summary>
    /// Carrega um livro por id
    /// </summary>
    /// <param name="id">Numero Id</param>
    /// <returns>Objeto do livro</returns>
    [HttpGet]
    [Route("get")]
    public async Task<ActionResult> GetByIdAsync(
        int? id)
    {
        try
        {
            //verifica se está foi enviado o valor do id
            if (id == null)
                return new BadRequestObjectResult("Informe o Id");

            //verifica se existe um livro com o id cadastrado
            var livro = await _livroRepository.GetByIdAsync((int)id);

            //caso não exista um livro com id cadastrado, retorna mensagem de não encontrado
            if (livro == null)
            {
                return new NotFoundObjectResult($"Livro com Id {id} não encontrado!");
            }

            //retorna 200 com sucesso
            return new OkObjectResult(livro);
        }
        catch (Exception e)
        {
            //retorna mensagem de erro
            return new BadRequestObjectResult(e.Message);
        }
    }

    /// <summary>
    /// Deleta um livro por id
    /// </summary>
    /// <param name="id">Numero Id</param>
    [HttpDelete]
    [Route("delete")]
    public async Task<ActionResult> DeleteByIdAsync(
        int? id)
    {
        try
        {
            //verifica se está foi enviado o valor do id
            if (id == null)
                return new BadRequestObjectResult("Informe o Id");

            //verifica se existe um livro com o id cadastrado
            var livroComId = await _livroRepository.GetByIdAsync((int)id);

            //caso não exista um livro com id cadastrado, retorna mensagem de não encontrado
            if (livroComId == null)
            {
                return new NotFoundObjectResult($"Livro com Id {id} não encontrado!");
            }

            //chama o repositorio para deletar o livro pelo Id
            await _livroRepository.DeleteByIdAsync((int)id);

            //retorna 200 com sucesso
            return new OkResult();
        }
        catch (Exception e)
        {
            //retorna mensagem de erro
            return new BadRequestObjectResult(e.Message);
        }
    }

    /// <summary>
    /// Carrega todos os livros
    /// </summary>
    /// <returns>List de objeto de livros</returns>
    [HttpGet]
    [Route("get_all")]
    public async Task<ActionResult> GetByAllAsync()
    {
        try
        {
            //chama o repositorio para carregar os livros por curso
            var livros = await _livroRepository.GetAllAsync();

            //retorna 200 com sucesso
            return new OkObjectResult(livros);
        }
        catch (Exception e)
        {
            //retorna mensagem de erro
            return new BadRequestObjectResult(e.Message);
        }
    }
}