using BFZ.AtividadeExtensionistaII.Domain.Abstractions;
using SQLite;

namespace BFZ.AtividadeExtensionistaII.Domain.Models;

/// <summary>
/// Classe que representa o lote de produção que será planejado e produzido
/// </summary>
[Table("lote_de_producao")]
public class LoteDeProducao: IEntityId
{
    /// <summary>
    /// Código indentificador da entidade
    /// </summary>
    [PrimaryKey]
    public int? Id { get; set; }

    /// <summary>
    /// Código identificador do cadastro do produto
    /// </summary>
    public int? IdProduto { get; set; }

    /// <summary>
    /// Quantidade (unidades) do produto 
    /// </summary>
    public decimal Quantidade { get; set; }

    /// <summary>
    /// Data de plantio do produto
    /// </summary>
    public DateTime? DataPlantio { get; set; }

    /// <summary>
    /// Data de encerramento do produto (colhido, entregue, etc)
    /// </summary>
    public DateTime? DataEncerramento { get; set; }

    /// <summary>
    /// Observações
    /// </summary>
    public string? Observacao { get; set; }
}