using Newtonsoft.Json;
using NPushover.Converters;
using System;

namespace NPushover.ResponseObjects
{
    /// <summary>
    /// Represents a response for a <see cref="M:NPushover.Pushover.GetReceiptAsync(System.String)"/> call.
    /// </summary>
    /// <seealso href="https://pushover.net/api#receipt">Pushover API documentation</seealso>
    public class ReceiptResponse : PushoverUserResponse
    {
        /// <summary>
        /// Gets whether the user has acknowledged the notification.
        /// </summary>
        /// <seealso href="https://pushover.net/api#receipt">Pushover API documentation</seealso>
        [JsonProperty("acknowledged")]
        [JsonConverter(typeof(BoolConverter))]
        public bool Acknowledged { get; set; }

        /// <summary>
        /// Gets the date of when the user has acknowledged the notification, if any.
        /// </summary>
        /// <seealso href="https://pushover.net/api#receipt">Pushover API documentation</seealso>
        [JsonProperty("acknowledged_at")]
        [JsonConverter(typeof(NullableUnixDateTimeConverter))]
        public DateTime? AcknowledgedAt { get; set; }

        /// <summary>
        /// Gets the user key of the user that first acknowledged the notification.
        /// </summary>
        /// <seealso href="https://pushover.net/api#receipt">Pushover API documentation</seealso>
        [JsonProperty("acknowledged_by")]
        public string AcknowledgedBy { get; set; }

        /// <summary>
        /// Gets when the notification was last retried / delivered, if at all.
        /// </summary>
        /// <seealso href="https://pushover.net/api#receipt">Pushover API documentation</seealso>
        [JsonProperty("last_delivered_at")]
        [JsonConverter(typeof(NullableUnixDateTimeConverter))]
        public DateTime? LastDeliveredAt { get; set; }

        /// <summary>
        /// Gets whether the expiration date has passed.
        /// </summary>
        /// <seealso href="https://pushover.net/api#receipt">Pushover API documentation</seealso>
        [JsonProperty("expired")]
        [JsonConverter(typeof(BoolConverter))]
        public bool Expired { get; set; }
        
        /// <summary>
        /// Gets when the notification will stop being retried.
        /// </summary>
        /// <seealso href="https://pushover.net/api#receipt">Pushover API documentation</seealso>
        [JsonProperty("expires_at")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime ExpiresAt { get; set; }
        
        /// <summary>
        /// Gets whether the Pushover services have called back the callback URL (if any).
        /// </summary>
        /// <seealso href="https://pushover.net/api#receipt">Pushover API documentation</seealso>
        [JsonProperty("called_back")]
        [JsonConverter(typeof(BoolConverter))]
        public bool CalledBack { get; set; }

        /// <summary>
        /// Gets when the Pushover services have called back the callback URL (if any, if at all).
        /// </summary>
        /// <seealso href="https://pushover.net/api#receipt">Pushover API documentation</seealso>
        [JsonProperty("called_back_at")]
        [JsonConverter(typeof(NullableUnixDateTimeConverter))]
        public DateTime? CalledBackAt { get; set; }
    }
}
