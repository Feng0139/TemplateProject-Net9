using System.Text.Json;
using System.Text.Json.Serialization;

namespace TemplateProject.Message.Converters;

public class SingleOrArrayConverter<T> : JsonConverter<List<T>>
{
    public override List<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        List<T> result = new();

        if (reader.TokenType == JsonTokenType.StartArray)
        {
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndArray)
                    break;

                var value = JsonSerializer.Deserialize<T>(ref reader, options);

                if (value != null)
                {
                    result.Add(value);
                }
            }
        }
        else
        {
            var value = JsonSerializer.Deserialize<T>(ref reader, options);

            if (value != null)
            {
                result.Add(value);
            }
        }

        return result;
    }

    public override void Write(Utf8JsonWriter writer, List<T> value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, options);
    }
}