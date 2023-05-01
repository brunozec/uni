using BFZ.AtividadeExtensionistaII.Domain.Abstractions;
using SQLite;

namespace BFZ.AtividadeExtensionistaII.Domain.Models;

/// <summary>
/// Classe que representa o cadastro produto
/// </summary>
[Table("produtos")]
public class Produto : IEntityId
{
    /// <summary>
    /// Código indentificador da entidade
    /// </summary>
    [PrimaryKey]
    public int? Id { get; set; }

    /// <summary>
    /// Descrição do produto
    /// </summary>
    public string Descricao { get; set; }

    /// <summary>
    /// Observações
    /// </summary>
    public string? Observacao { get; set; }
    
    public int TempoProducaoEmDias { get; set; }
}