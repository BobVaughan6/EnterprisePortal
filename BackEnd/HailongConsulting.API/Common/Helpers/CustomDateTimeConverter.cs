using System.Text.Json;
using System.Text.Json.Serialization;

namespace HailongConsulting.API.Common.Helpers;

/// <summary>
/// 自定义DateTime JSON转换器，支持 yyyy-MM-dd HH:mm:ss 格式
/// </summary>
public class CustomDateTimeConverter : JsonConverter<DateTime>
{
    private const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var dateString = reader.GetString();
        if (string.IsNullOrEmpty(dateString))
        {
            return default;
        }

        // 尝试多种格式解析
        if (DateTime.TryParseExact(dateString, DateTimeFormat, null, System.Globalization.DateTimeStyles.None, out var result))
        {
            return result;
        }

        // 尝试ISO 8601格式
        if (DateTime.TryParse(dateString, out result))
        {
            return result;
        }

        throw new JsonException($"Unable to convert \"{dateString}\" to DateTime.");
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(DateTimeFormat));
    }
}

/// <summary>
/// 自定义可空DateTime JSON转换器，支持 yyyy-MM-dd HH:mm:ss 格式
/// </summary>
public class CustomNullableDateTimeConverter : JsonConverter<DateTime?>
{
    private const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

    public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var dateString = reader.GetString();
        if (string.IsNullOrEmpty(dateString))
        {
            return null;
        }

        // 尝试多种格式解析
        if (DateTime.TryParseExact(dateString, DateTimeFormat, null, System.Globalization.DateTimeStyles.None, out var result))
        {
            return result;
        }

        // 尝试ISO 8601格式
        if (DateTime.TryParse(dateString, out result))
        {
            return result;
        }

        throw new JsonException($"Unable to convert \"{dateString}\" to DateTime.");
    }

    public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
    {
        if (value.HasValue)
        {
            writer.WriteStringValue(value.Value.ToString(DateTimeFormat));
        }
        else
        {
            writer.WriteNullValue();
        }
    }
}