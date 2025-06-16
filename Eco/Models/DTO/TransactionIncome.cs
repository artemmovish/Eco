using Eco.Converters;
using Eco.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Eco.Models.DTO
{
    class TransactionIncome
    {
            [JsonPropertyName("id")]
            public int Id { get; set; }

            [JsonPropertyName("account_id")]
            public int AccountId { get; set; }

            [JsonPropertyName("type")]
            public string Type { get; set; }

            [JsonPropertyName("income_category_id")]
            [JsonConverter(typeof(NullableEnumConverter<IncomeCategories>))]
            public IncomeCategories? IncomeCategoryId { get; set; }

            [JsonPropertyName("category")]
            public string Category { get; set; }

            [JsonPropertyName("amount")]
            [JsonConverter(typeof(DecimalJsonConverter))]  // Для конвертации строки в decimal
            public decimal Amount { get; set; }

            [JsonPropertyName("date")]
            public DateTime Date { get; set; }

            [JsonPropertyName("comment")]
            public string Comment { get; set; }

            [JsonPropertyName("created_at")]
            public DateTime CreatedAt { get; set; }

            [JsonPropertyName("updated_at")]
            public DateTime UpdatedAt { get; set; }

        public TransactionIncome(Transaction transaction)
        {
            Id = transaction.Id;
            AccountId = transaction.AccountId;
            Type = transaction.Type;
            IncomeCategoryId = transaction.IncomeCategoryId;
            Category = transaction.Category;
            Amount = transaction.Amount;
            Date = transaction.Date;
            Comment = transaction.Comment;
            CreatedAt = transaction.CreatedAt;
            UpdatedAt = transaction.UpdatedAt;
        }
    }
}
