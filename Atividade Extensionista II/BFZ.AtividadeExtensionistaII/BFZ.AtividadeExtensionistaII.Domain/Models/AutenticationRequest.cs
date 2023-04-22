using System.ComponentModel.DataAnnotations;

namespace BFZ.AtividadeExtensionistaII.Domain.Models;

public class AutenticationRequest
{
    [EmailAddress]
    [Required(ErrorMessage = "Campo {0} obrigatório")]
    public string Login { get; set; }

    [Required(ErrorMessage = "Campo {0} obrigatório")]
    public string Senha { get; set; }
}