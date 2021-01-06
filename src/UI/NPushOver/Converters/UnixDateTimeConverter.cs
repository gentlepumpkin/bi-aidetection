using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace NPushover.Converters
{
    /// <summary>
    /// Handles explicitly the conversion of UNIX (Epoch) time to DateTime and vice versa.
    /// </summary>
    /// <remarks>
    /// Dates are specified in seconds from UNIX epoch. This converter helps to convert these values to DateTime
    /// objects with the Kind property explicitly set to Utc.
    /// </remarks>
    internal class UnixDateTimeConverter : DateTimeConverterBase
    {
        private static DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="JsonWriter"/> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var date = (DateTime)value;
            if (date.Kind == DateTimeKind.Utc)
                writer.WriteValue((long)date.Subtract(epoch).TotalSeconds);
            else
                writer.WriteValue((long)TimeZoneInfo.ConvertTimeToUtc(date).Subtract(epoch).TotalSeconds);
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="JsonReader"/> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The object value.</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.Integer)
                throw new JsonSerializationException(String.Format("Unexpected token parsing date. Expected Integer, got {0}.", reader.TokenType));

            return epoch.AddSeconds((long)reader.Value);
        }

        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>True if this instance can convert the specified object type; otherwise, false.</returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime);
        }
    }

    /// <summary>
    /// Handles explicitly the conversion of UNIX (Epoch) time to nullable DateTime and vice versa.
    /// </summary>
    /// <remarks>
    /// Dates are specified in seconds from UNIX epoch. This converter helps to convert these values to DateTime 
    /// objects with the Kind property explicitly set to Utc.
    /// </remarks>
    internal class NullableUnixDateTimeConverter : UnixDateTimeConverter
    {
        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="JsonWriter"/> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
                writer.WriteNull();
            else
                base.WriteJson(writer, value, serializer);
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="JsonReader"/> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The object value.</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null)
                return null;
            if ((reader.TokenType == JsonToken.Integer) && ((long)reader.Value == 0))
                return null;
            return base.ReadJson(reader, objectType, existingValue, serializer);
        }

        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>True if this instance can convert the specified object type; otherwise, false.</returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime?);
        }
    }
}
