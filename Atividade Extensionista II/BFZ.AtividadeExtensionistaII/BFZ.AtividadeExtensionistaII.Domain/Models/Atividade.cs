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
    /// Código do lote de produção
    /// </summary>
    public int LoteId { get; set; }

    /// <summary>
    /// Observação e dados adicionais
    /// </summary>
    public string Observacao { get; set; }
    
    public decimal? Quantidade { get; set; }

    /// <summary>
    /// Data de realização da atividade
    /// </summary>
    public DateTime? Data { get; set; }

    /// <summary>
    /// Indica o tipo da atividade
    /// Plantio = 0 
    /// AplicacaoDefensivo = 1
    /// Colheita = 2 
    /// Doacao = 3
    /// Descarte =4
    /// </summary>
    public TipoAtividade Tipo { get; set; }

    [Ignore]
    public string DescricaoTipo
    {
        get
        {
            return Tipo switch
            {
                TipoAtividade.Plantio => "Plantio"
                , TipoAtividade.AplicacaoDefensivo => "Aplicação de defensivo"
                , TipoAtividade.Colheita => "Colheita"
                , TipoAtividade.Doacao => "Doação"
                , TipoAtividade.Descarte => "Descarte"
                , _ => throw new ArgumentOutOfRangeException()
            };
        }
    }

    public int? IdEntidade { get; set; }
    
    public string? NomeEntidade { get; set; }
}

public class AtividadeTipo
{
    public TipoAtividade Id { get; set; }

    public string Descricao { get; set; }
}