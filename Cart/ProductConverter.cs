using System.Text.Json;
using System.Text.Json.Serialization;
using CartDaoApp.Entities;

// todo тут відбувається магія для десереалізації поліморфних типів
namespace CartDaoApp;

public class ProductConverter : JsonConverter<Product>
{
    public override Product Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var jsonDoc = JsonDocument.ParseValue(ref reader);
        var root = jsonDoc.RootElement;

        // Перевіряємо, чи має JSON властивість, яка визначає тип продукту, наприклад, "Type"
        if (root.TryGetProperty("Type", out var typeProp))
        {
            var type = typeProp.GetString();
            switch (type)
            {
                case "Book":
                    return JsonSerializer.Deserialize<Book>(root.GetRawText(), options);
                // Додайте випадки для інших типів, якщо потрібно
            }
        }

        throw new JsonException("Unknown product type.");
    }

    public override void Write(Utf8JsonWriter writer, Product value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, value.GetType(), options);
    }
}