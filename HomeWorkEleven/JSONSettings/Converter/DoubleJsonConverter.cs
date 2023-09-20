using System.Text.Json;
using System.Text.Json.Serialization;

namespace HomeWorkEleven.JSONSettings.Converter;

public class DoubleJsonConverter : JsonConverter<double>
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert == typeof(double);
    }

    public override double Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            var value = reader.GetString();
            switch (value)
            {
                case "-∞":
                    return double.NegativeInfinity;
                case "+∞":
                    return double.PositiveInfinity;
                case "�":
                    return double.NaN;
            }
        }

        return Convert.ToDouble(reader.GetString());
    }

    public override void Write(Utf8JsonWriter writer, double value, JsonSerializerOptions options)
    {
        if (double.IsNegativeInfinity(value))
        {
            writer.WriteStringValue("-∞");
        }
        else if (double.IsPositiveInfinity(value))
        {
            writer.WriteStringValue("+∞");
        }
        else if (double.IsNaN(value))
        {
            writer.WriteStringValue("�");
        }
        else
        {
            writer.WriteNumberValue(value);
        }
    }
}