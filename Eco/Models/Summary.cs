using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Eco.Models
{
    public class Summary
    {
        [JsonPropertyName("total_income")]
        public decimal TotalIncome { get; set; }  // Используем decimal для денежных значений

        [JsonPropertyName("total_expense")]
        public decimal TotalExpense { get; set; }

        [JsonPropertyName("categories")]
        public Categories Categories { get; set; } = new Categories();
    }
}

//{
//    "total_income": 1500,
//    "total_expense": 75.5,
//    "categories": {
//        "income": {
//            "Зарплата": 1500
//        },
//        "expense": {
//            "Еда": 75.5
//        }
//    }
//}