using SQLite;

namespace LojaDeJogos.Models
{
    public class Jogo
    {
        /// <summary>
        /// Código identificador do jogo
        /// </summary>
        [PrimaryKey, AutoIncrement] 
        public int? Id { get; set; }

        /// <summary>
        /// Descrição do jogo
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Valor do jogo
        /// </summary>
        public decimal Valor { get; set; }

        /// <summary>
        /// Quantidade de unidades em estoque
        /// </summary>
        public int Estoque { get; set; }

        /// <summary>
        /// Genero do jogo e.g. Ação, Aventura, etc
        /// </summary>
        public string Genero { get; set; }

        /// <summary>
        /// Classificação eária do jogo e.g. PEGI 16, PEGI 18, etc
        /// </summary>
        public string Classificacao { get; set; }

        /// <summary>
        /// Tag para procura rapida e.g. aliens, futuristico, destopia, etc
        /// </summary>
        public string Tags { get; set; }
    }
}