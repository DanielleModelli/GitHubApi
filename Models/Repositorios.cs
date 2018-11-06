using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TestePratico.Models
{
    public partial class Repositorios
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Nome { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Descricao { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
    }
}
