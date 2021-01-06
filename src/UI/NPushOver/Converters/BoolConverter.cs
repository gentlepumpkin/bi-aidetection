using Newtonsoft.Json;
using System;

namespace NPushover.Converters
{
    /// <summary>
    /// Handles explicitly the conversion of integers to booleans and vice versa.
    /// </summary>
    /// <remarks>
    /// The values 0 or null are interpreted as false, anything other than those are interpreted as true.
    /// </remarks>
    internal class BoolConverter : JsonConverter
    {
        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="JsonWriter"/> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((bool)value) ? 1 : 0);
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
                return false;
            if ((reader.TokenType != JsonToken.Integer) && (reader.TokenType != JsonToken.String))
                throw new JsonSerializationException(String.Format("Unexpected token parsing bool. Expected String/Integer, got {0}.", reader.TokenType));
            return int.Parse(reader.Value.ToString()) != 0;
        }

        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>True if this instance can convert the specified object type; otherwise, false.</returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(bool);
        }
    }

    /// <summary>
    /// Handles explicitly the conversion of integers to nullable booleans and vice versa.
    /// </summary>
    /// <remarks>
    /// The value 0 is interpreted as false, null is interpreted as null; anything other than this is interpreted as true.
    /// </remarks>
    internal class NullableBoolConverter : BoolConverter
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
            return base.ReadJson(reader, objectType, existingValue, serializer);
        }

        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>True if this instance can convert the specified object type; otherwise, false.</returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(bool?);
        }
    }
}
