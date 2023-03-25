using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtividadePratica.Domain;

[Table("alunos")]
public class Aluno
{
    /// <summary>
    /// Código identificador, chave primária
    /// </summary>
    [Key]
    public int? Id { get; set; }
    
    /// <summary>
    /// RU do aluno
    /// </summary>
    //indica que o campo é obrigatório
    [Required(ErrorMessage = "RU é obrigatório")]
    public int RU { get; set; }

    /// <summary>
    /// Nome do aluno
    /// </summary>
    //indica que o campo é obrigatório
    [Required(ErrorMessage = "Nome é obrigatório")]
    public string Nome { get; set; }

    /// <summary>
    /// Curso do aluno
    /// </summary>
    //indica que o campo é obrigatório
    [Required(ErrorMessage = "Curso é obrigatório")]
    public string Curso { get; set; }
}