using AtividadePratica.Domain;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AtividadePratica.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> Authenticate(
        UserAuth user)
    {
        if (user.Nome != "zeclhynscki"
            || user.RU != "3672320")
        {
            return new BadRequestObjectResult("Nome ou RU inválido!");
        }
        
    }
}