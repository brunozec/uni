using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtividadePratica.Domain;

[Table("livros")]
public class Livro
{
    //chave primaria para a tabela de livros
    [Key] public int? Id { get; set; }

    //indica que o campo é obrigatório
    [Required(ErrorMessage = "Nome é obrigatório")]
    public string? Nome { get; set; }

    //indica que o campo é obrigatório
    [Required(ErrorMessage = "Url é obrigatório")]
    public string? Url { get; set; }
}