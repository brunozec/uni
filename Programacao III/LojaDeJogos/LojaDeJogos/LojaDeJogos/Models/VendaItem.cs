using SQLite;

namespace LojaDeJogos.Models
{
    public class VendaItem
    {
        /// <summary>
        /// Código identificador do item da venda
        /// </summary>
        [PrimaryKey, AutoIncrement] 
        public int? Id { get; set; }
        
        /// <summary>
        /// Código identificador do jogo
        /// </summary>
        public int JogoId { get; set; }

        /// <summary>
        /// Quantidade vendida 
        /// </summary>
        public int Quantidade { get; set; }

        /// <summary>
        /// Valor vendido
        /// </summary>
        public decimal Valor { get; set; }

        /// <summary>
        /// Desconto concedido
        /// </summary>
        public decimal Desconto { get; set; }

        /// <summary>
        /// Não grava apenas para exibição
        /// </summary>
        [Ignore]
        public decimal ValorFinal => (Quantidade * Valor) - Desconto;
    }
}