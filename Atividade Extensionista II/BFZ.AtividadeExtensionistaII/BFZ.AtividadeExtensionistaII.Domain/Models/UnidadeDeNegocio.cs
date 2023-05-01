using BFZ.AtividadeExtensionistaII.Domain.Abstractions;
using SQLite;

namespace BFZ.AtividadeExtensionistaII.Domain.Models;

/// <summary>
/// Classe que representa o agricultor familiar ou entidade de caridade
/// </summary>
[Table("unidades_de_negocio")]
public class UnidadeDeNegocio : IEntityId
{
    /// <summary>
    /// Código identificador da entidade
    /// </summary>
    [PrimaryKey] public int? Id { get; set; }

    /// <summary>
    /// Nome do agricultor familiar/entidade de caridade
    /// </summary>
    public string Nome { get; set; }

    /// <summary>
    /// Email (endereço eletronico)
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Telefone (DDD) 9 9999-9999
    /// </summary>
    public string Fone { get; set; }

    /// <summary>
    /// Cep do endereço, o endereço será carregado com base no CEP
    /// </summary>
    public string EnderecoCep { get; set; }

    /// <summary>
    /// Número do endereço
    /// </summary>
    public string EnderecoNumero { get; set; }

    /// <summary>
    /// Informação complementar do endereço
    /// </summary>
    public string EnderecoComplementeo { get; set; }
    
    /// <summary>
    /// Tipo da unidade de negócio
    /// (TipoUnidadeDeNegocio.Agricultor) 0 = Agricultor
    /// (TipoUnidadeDeNegocio.EntidadeCaridade) 1 - Entidade de caridade
    /// </summary>
    public TipoUnidadeDeNegocio Tipo { get; set; }

    /// <summary>
    /// Campo para senha de entrada no sistema
    /// </summary>
    public string Senha { get; set; }
}