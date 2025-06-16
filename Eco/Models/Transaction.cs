using Eco.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using Eco.Converters;

namespace Eco.Models
{
    public class Transaction
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("account_id")]
        public int AccountId { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("expense_category_id")]
        [JsonConverter(typeof(NullableEnumConverter<ExpenseCategories>))]
        public ExpenseCategories? ExpenseCategoryId { get; set; }

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
    }

    // Конвертер для nullable enum
    public class NullableEnumConverter<T> : JsonConverter<T?> where T : struct, Enum
    {
        public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null) return null;
            if (reader.TokenType == JsonTokenType.Number && reader.TryGetInt32(out int value))
            {
                return (T)Enum.ToObject(typeof(T), value);
            }
            return null;
        }

        public override void Write(Utf8JsonWriter writer, T? value, JsonSerializerOptions options)
        {
            if (value == null) writer.WriteNullValue();
            else writer.WriteNumberValue(Convert.ToInt32(value));
        }
    }
}

//{
//    "type": "expense",
//    "expense_category_id": 2,
//    "amount": 2000,
//    "date": "2025-05-20",
//    "comment": ""
//}