using AtividadePratica.Data;
using AtividadePratica.Domain;
using Microsoft.AspNetCore.Mvc;

namespace AtividadePratica.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlunoController : ControllerBase
{
    private readonly IAlunoRepository _alunoRepository;

    /// <summary>
    /// Controller para ações com objeto do aluno, fazendo injeção de dependecia do repositorio de alunos
    /// </summary>
    /// <param name="alunoRepository"></param>
    public AlunoController(
        IAlunoRepository alunoRepository)
    {
        _alunoRepository = alunoRepository;
    }

    /// <summary>
    /// Insere um novo aluno
    /// </summary>
    /// <param name="aluno">Objeto contendo as informações do aluno</param>
    /// <returns>Objeto do aluno que está salvo no banco de dados</returns>
    [HttpPost]
    [Route("insert")]
    public async Task<ActionResult> InsertAsync(
        [FromBody] Aluno? aluno)
    {
        try
        {
            //verifica se está foi enviado o objeto do aluno
            if (aluno == null)
                return new BadRequestObjectResult("Informe os dados do aluno");

            //verifica se já existe um aluno com o ru cadastrado
            var alunoComRu = await _alunoRepository.GetByRUAsync((int)aluno.RU);

            //caso exista um aluno com ru cadastrado, retorna mensagem de ja cadastrado
            if (alunoComRu != null)
            {
                return new BadRequestObjectResult($"Aluno com RU {aluno.RU} já cadastrado!");
            }

            //chama o repositorio para salvar o aluno
            var alunoSalvo = await _alunoRepository.InsertAsync(aluno);

            //retorna 200 com sucesso
            return new OkObjectResult(alunoSalvo);
        }
        catch (Exception e)
        {
            //retorna mensagem de erro
            return new BadRequestObjectResult(e.Message);
        }
    }

    /// <summary>
    /// Atualiza um aluno ja existente
    /// </summary>
    /// <param name="aluno">Objeto contendo as onformações do aluno</param>
    /// <returns>Objeto do aluno atualizado que está salvo no banco de dados</returns>
    [HttpPut]
    [Route("update")]
    public async Task<ActionResult> UpdateAsync(
        [FromBody] Aluno aluno)
    {
        try
        {
            //verifica se está foi enviado o objeto do aluno
            if (aluno == null)
                return new BadRequestObjectResult("Informe os dados do aluno");

            //verifica se já existe um aluno com o ru cadastrado
            var alunoComRu = await _alunoRepository.GetByRUAsync((int)aluno.RU);

            //caso não exista um aluno com ru cadastrado, retorna mensagem de não encontrado
            if (alunoComRu == null)
            {
                return new NotFoundObjectResult($"Aluno com RU {aluno.RU} não encontrado!");
            }

            //chama o repositorio para atualizar o aluno
            var alunoSalvo = await _alunoRepository.UpdateAsync(aluno);

            //retorna 200 com sucesso
            return new OkObjectResult(alunoSalvo);
        }
        catch (Exception e)
        {
            //retorna mensagem de erro
            return new BadRequestObjectResult(e.Message);
        }
    }

    /// <summary>
    /// Carrega um aluno por ru
    /// </summary>
    /// <param name="ru">Numero RU</param>
    /// <returns>Objeto do aluno</returns>
    [HttpGet]
    [Route("get")]
    public async Task<ActionResult> GetByRUAsync(
        int? ru)
    {
        try
        {
            //verifica se está foi enviado o valor do ru
            if (ru == null)
                return new BadRequestObjectResult("Informe o RU");

            //verifica se existe um aluno com o ru cadastrado
            var aluno = await _alunoRepository.GetByRUAsync((int)ru);

            //caso não exista um aluno com ru cadastrado, retorna mensagem de não encontrado
            if (aluno == null)
            {
                return new NotFoundObjectResult($"Aluno com RU {ru} não encontrado!");
            }

            //retorna 200 com sucesso
            return new OkObjectResult(aluno);
        }
        catch (Exception e)
        {
            //retorna mensagem de erro
            return new BadRequestObjectResult(e.Message);
        }
    }

    /// <summary>
    /// Deleta um aluno por ru
    /// </summary>
    /// <param name="ru">Numero RU</param>
    [HttpDelete]
    [Route("delete")]
    public async Task<ActionResult> DeleteByRUAsync(
        int? ru)
    {
        try
        {
            //verifica se está foi enviado o valor do ru
            if (ru == null)
                return new BadRequestObjectResult("Informe o RU");

            //verifica se existe um aluno com o ru cadastrado
            var alunoComRu = await _alunoRepository.GetByRUAsync((int)ru);

            //caso não exista um aluno com ru cadastrado, retorna mensagem de não encontrado
            if (alunoComRu == null)
            {
                return new NotFoundObjectResult($"Aluno com RU {ru} não encontrado!");
            }

            //chama o repositorio para deletar o aluno pelo RU
            await _alunoRepository.DeleteByRUAsync((int)ru);

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
    /// Carrega um aluno por curso
    /// </summary>
    /// <param name="curso">Nome do curso</param>
    /// <returns>List de objeto de alunos</returns>
    [HttpGet]
    [Route("get_by_curso")]
    public async Task<ActionResult> GetByCursoAsync(
        string? curso)
    {
        try
        {
            //verifica se está foi enviado o valor do curso
            if (string.IsNullOrEmpty(curso))
                return new BadRequestObjectResult("Informe o nome do curso");

            //chama o repositorio para carregar os alunos por curso
            var alunos = await _alunoRepository.GetByCursoAsync(curso);

            //retorna 200 com sucesso
            return new OkObjectResult(alunos);
        }
        catch (Exception e)
        {
            //retorna mensagem de erro
            return new BadRequestObjectResult(e.Message);
        }
    }
}