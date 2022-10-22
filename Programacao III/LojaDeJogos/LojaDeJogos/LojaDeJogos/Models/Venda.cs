using SQLite;

namespace LojaDeJogos.Models
{
    public class Venda
    {
        /// <summary>
        /// Código identificador da venda
        /// </summary>
        [PrimaryKey, AutoIncrement] 
        public int? Id { get; set; }

        /// <summary>
        /// CPF/CNPJ do cliente
        /// </summary>
        public string ClienteCpfCnpj { get; set; }

        /// <summary>
        /// Código do item da venda
        /// </summary>
        public int VendaItemId { get; set; }
    }
}