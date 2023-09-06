using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.ViewModel
{
    public class DateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TryGetDateTime(out DateTime result))
            {
                return result;
            }
            else if (reader.TryGetDateTimeOffset(out DateTimeOffset dateTimeOffset))
            {
                return dateTimeOffset.DateTime;
            }

            throw new JsonException($"Unable to parse datetime from {reader.GetString()}");
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
        }
    }
}
