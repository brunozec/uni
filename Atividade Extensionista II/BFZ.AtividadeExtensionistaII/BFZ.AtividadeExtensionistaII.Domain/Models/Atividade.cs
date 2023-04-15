using BFZ.AtividadeExtensionistaII.Domain.Abstractions;
using SQLite;

namespace BFZ.AtividadeExtensionistaII.Domain.Models;

[Table("atividades")]
public class Atividade : IEntityId
{
    [PrimaryKey] public int? Id { get; set; }

    public string Descricao { get; set; }

    public string Observacao { get; set; }

    public DateTime Data { get; set; }
}