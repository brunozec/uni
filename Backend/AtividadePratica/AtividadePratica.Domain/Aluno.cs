using System.ComponentModel.DataAnnotations;

namespace AtividadePratica.Domain;

public class Aluno
{
    /// <summary>
    /// RU do aluno
    /// </summary>
    //chave primaria para a tabela de alunos
    [Key]
    //indica que o campo é obrigatório
    [Required(ErrorMessage = "RU é obrigatório")]
    public int? RU { get; set; }

    /// <summary>
    /// Nome do aluno
    /// </summary>
    //indica que o campo é obrigatório
    [Required(ErrorMessage = "Nome é obrigatório")]
    public string? Nome { get; set; }

    /// <summary>
    /// Curso do aluno
    /// </summary>
    //indica que o campo é obrigatório
    [Required(ErrorMessage = "Curso é obrigatório")]
    public string? Curso { get; set; }
}