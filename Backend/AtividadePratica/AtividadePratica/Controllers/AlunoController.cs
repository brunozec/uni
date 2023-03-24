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

    [HttpPost]
    [Route("insert")]
    public async Task<ActionResult> InsertAsync(
        [FromBody] Aluno? aluno)
    {
        try
        {
            if (aluno == null)
                return new BadRequestObjectResult("Informe os dados do aluno");

            var alunoComRu = await _alunoRepository.GetByRUAsync((int)aluno.RU);
            
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

    [HttpPut]
    [Route("update")]
    public async Task<ActionResult> UpdateAsync(
        [FromBody] Aluno aluno)
    {
        try
        {
            if (aluno == null)
                return new BadRequestObjectResult("Informe os dados do aluno");

            var alunoComRu = await _alunoRepository.GetByRUAsync((int)aluno.RU);
            
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

    [HttpGet]
    [Route("get")]
    public async Task<ActionResult> GetByRUAsync(
        int? ru)
    {
        try
        {
            if (ru == null)
                return new BadRequestObjectResult("Informe o RU");

            //chama o repositorio para carregar o aluno pelo RU
            var aluno = await _alunoRepository.GetByRUAsync((int)ru);

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

    [HttpDelete]
    [Route("delete")]
    public async Task<ActionResult> DeleteByRUAsync(
        int? ru)
    {
        try
        {
            if (ru == null)
                return new BadRequestObjectResult("Informe o RU");

            var alunoComRu = await _alunoRepository.GetByRUAsync((int)ru);

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

    [HttpGet]
    [Route("get_by_curso")]
    public async Task<ActionResult> GetByCursoAsync(
        string? curso)
    {
        try
        {
            if (string.IsNullOrEmpty(curso))
                return new BadRequestObjectResult("Informe o nome do curso");

            //chama o repositorio para carregar os alunos por curso
            var alunoSalvo = await _alunoRepository.GetByCursoAsync(curso);

            //retorna 200 com sucesso
            return new OkObjectResult(alunoSalvo);
        }
        catch (Exception e)
        {
            //retorna mensagem de erro
            return new BadRequestObjectResult(e.Message);
        }
    }
}