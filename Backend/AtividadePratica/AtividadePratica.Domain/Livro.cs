using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtividadePratica.Domain;

[Table("livros")]
public class Livro
{
    /// <summary>
    /// Código identificador, chave primária
    /// </summary>
    [Key]
    public int? Id { get; set; }

    /// <summary>
    /// Nome do livro
    /// </summary>
    //indica que o campo é obrigatório
    [Required(ErrorMessage = "Nome é obrigatório")]
    public string Nome { get; set; }

    /// <summary>
    /// Url do livro
    /// </summary>
    //indica que o campo é obrigatório
    [Required(ErrorMessage = "Url é obrigatório")]
    public string Url { get; set; }

    /// <summary>
    /// Edição / ano
    /// </summary>
    public string EdicaoAno { get; set; }
    
    /// <summary>
    ///  International Standard Book Number 
    /// </summary>
    public string ISBN { get; set; }

    /// <summary>
    /// Código de referencia do livro
    /// </summary>
    public string? CodigoReferencia { get; set; }
}