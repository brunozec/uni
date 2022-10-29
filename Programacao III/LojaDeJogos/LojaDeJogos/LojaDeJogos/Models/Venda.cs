using System.Collections.Generic;
using System.Linq;
using SQLite;

namespace LojaDeJogos.Models
{
    public class Venda
    {
        private string _nomeCliente;

        /// <summary>
        /// Código identificador da venda
        /// </summary>
        [PrimaryKey, AutoIncrement] 
        public int? Id { get; set; }

        /// <summary>
        /// Nome do cliente
        /// </summary>
        public string NomeCliente
        {
            get
            {
                //retorna 'Não informado' quando não foi informado o nome do cliente
                if (string.IsNullOrEmpty(_nomeCliente))
                    return "Não informado";

                return _nomeCliente;
            }
            set => _nomeCliente = value;
        }

        /// <summary>
        /// CPF/CNPJ do cliente
        /// </summary>
        public string ClienteCpfCnpj { get; set; }
        
        /// <summary>
        /// Valor total da venda (valor dos itens * quantidade dos itens - descontos)
        /// Não será gravado no banco de dados
        /// </summary>
        [Ignore]
        public decimal ValorTotal => Itens.Sum(sum => sum.ValorFinal);

        /// <summary>
        /// Itens da venda
        /// </summary>
        [Ignore]
        public IEnumerable<VendaItem> Itens { get; set; }

        public Venda()
        {
            //instancia a lista com valor padrao
            Itens = new List<VendaItem>();
        }
    }
}