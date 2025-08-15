using System.Text.Json;
using System.Text.Json.Serialization;

namespace ShopAccAPI.Helpers
{
    public class DateOnlyJsonConverter : JsonConverter<DateTime?>
    {
        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => DateTime.TryParse(reader.GetString(), out var date) ? date : null;

        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
            => writer.WriteStringValue(value?.ToString("dd/MM/yyyy"));
    }
}
