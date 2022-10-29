using System.Collections.Generic;
using Newtonsoft.Json;

namespace LojaDeJogos.Models
{
    public class GamesAPI
    {
        public List<JogoAPI> Games { get; set; }
    }

    public class JogoAPI
    {
        public string Url { get; set; }

        public string Name { get; set; }

        [JsonProperty("year_published")]
        public int? YearPublished { get; set; }

        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }
    }
}