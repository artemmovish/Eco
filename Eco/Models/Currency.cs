using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Eco.Models
{
    public class Currency
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime? CreatedAt { get; set; }  // Nullable, так как в JSON может быть null

        [JsonPropertyName("updated_at")]
        public DateTime? UpdatedAt { get; set; }  // Nullable, так как в JSON может быть null
    }
}
