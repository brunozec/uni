using BFZ.AtividadeExtensionistaII.Domain.Abstractions;
using SQLite;

namespace BFZ.AtividadeExtensionistaII.Domain.Models;

[Table("produtos")]
public class Produto : IEntityId
{
    /// <summary>
    /// Código indentificador
    /// </summary>
    [PrimaryKey]
    public int? Id { get; set; }

    /// <summary>
    /// Descrição do produto
    /// </summary>
    public string Descricao { get; set; }

    /// <summary>
    /// Quantidades (unidades) do produto 
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

    public TipoUnidadeDeNegocio Tipo { get; set; }
}