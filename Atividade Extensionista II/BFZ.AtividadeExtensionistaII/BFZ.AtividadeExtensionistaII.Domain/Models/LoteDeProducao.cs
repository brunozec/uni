using BFZ.AtividadeExtensionistaII.Domain.Abstractions;
using SQLite;

namespace BFZ.AtividadeExtensionistaII.Domain.Models;

/// <summary>
/// Classe que representa o lote de produção que será planejado e produzido
/// </summary>
[Table("lote_de_producao")]
public class LoteDeProducao : IEntityId
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

    [SQLite.Ignore] public string DescricaoProduto { get; set; }

    /// <summary>
    /// Quantidade (unidades) do produto 
    /// </summary>
    public decimal? Quantidade { get; set; }

    /// <summary>
    /// Data de plantio do produto
    /// </summary>
    public DateTime? DataPlanejado { get; set; }

    public DateTime? DataPlantio { get; set; }

    /// <summary>
    /// Data de encerramento do produto (colhido, entregue, etc)
    /// </summary>
    public DateTime? DataEncerramento { get; set; }

    /// <summary>
    /// Observações
    /// </summary>
    public string? Observacao { get; set; }

    public bool Plantado { get; set; }

    public Situacao Situacao { get; set; }

    [Ignore]
    public string SituacaoDescricao
    {
        get
        {
            switch (Situacao)
            {
                case Situacao.Planejado:
                    return "Planejado";
                case Situacao.EmProducao:
                    return "Em produção";
                case Situacao.Colhido:
                    return "Colhido";
                case Situacao.Doado:
                    return "Doado";
                case Situacao.Descartado:
                    return "Descartado";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public int? IdEmpresa { get; set; }
}