using BFZ.AtividadeExtensionistaII.Domain.Abstractions;
using SQLite;

namespace BFZ.AtividadeExtensionistaII.Domain.Models;

[Table("unidades_de_negocio")]
public class UnidadeDeNegocio : IEntityId
{
    [PrimaryKey] public int? Id { get; set; }

    public string Nome { get; set; }

    public string Email { get; set; }

    public string Fone { get; set; }

    public string EnderecoCep { get; set; }

    public string EnderecoNumero { get; set; }
}