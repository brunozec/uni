using BFZ.AtividadeExtensionistaII.Domain.Abstractions;
using SQLite;

namespace BFZ.AtividadeExtensionistaII.Domain.Models;

/// <summary>
/// Classe que representa o manejo que será aplicado no produto planejado ou em processo de produção
/// </summary>
[Table("atividades")]
public class Atividade : IEntityId
{
    /// <summary>
    /// Código identificador da entidade
    /// </summary>
    [PrimaryKey]
    public int? Id { get; set; }

    /// <summary>
    /// Descrição da atividade sendo realizada
    /// </summary>
    public string Descricao { get; set; }

    /// <summary>
    /// Observação e dados adicionais
    /// </summary>
    public string Observacao { get; set; }

    /// <summary>
    /// Data de realização da atividade
    /// </summary>
    public DateTime Data { get; set; }

    /// <summary>
    /// Indica o tipo da atividade
    /// Plantio = 0 
    /// AplicacaoDefensivo = 1
    /// Colheita = 2 
    /// Doacao = 3
    /// </summary>
    public TipoAtividade Tipo { get; set; }
}