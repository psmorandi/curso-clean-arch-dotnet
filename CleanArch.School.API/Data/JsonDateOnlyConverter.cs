using System;
using System.Text.Json.Serialization;

namespace CleanArch.School.API.Data
{
    using CleanArch.School.TypeExtensions;
    using System.Globalization;
    using System.Text.Json;

    public class JsonDateOnlyConverter : JsonConverter<DateOnly>
    {
        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
            DateTime.ParseExact(reader.GetString()!, "yyyy-MM-dd", CultureInfo.InvariantCulture).ToDateOnly();

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
            => writer.WriteStringValue(value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
    }
}