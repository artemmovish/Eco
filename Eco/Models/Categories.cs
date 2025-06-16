using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Eco.Models
{
    public class Categories
    {
        [JsonPropertyName("income")]
        public Dictionary<string, decimal> Income { get; set; } = new Dictionary<string, decimal>();

        [JsonPropertyName("expense")]
        public Dictionary<string, decimal> Expense { get; set; } = new Dictionary<string, decimal>();
    }
}
