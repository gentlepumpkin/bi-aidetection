using Newtonsoft.Json;

namespace NPushover.ResponseObjects
{
    /// <summary>
    /// Represents a response for a <see cref="M:NPushover.Pushover.ListMessagesAsync(System.String,System.String)"/> call.
    /// </summary>
    /// <seealso href="https://pushover.net/api/client#download">Pushover API documentation</seealso>
    public class ListMessagesResponse : PushoverResponse
    {
        /// <summary>
        /// Gets the messages retrieved from the Pushover services.
        /// </summary>
        /// <seealso href="https://pushover.net/api/client#download">Pushover API documentation</seealso>
        [JsonProperty("messages")]
        public StoredMessage[] Messages { get; set; }

        /// <summary>
        /// Gets information about the current user; see <see cref="ListMessagesUser"/>.
        /// </summary>
        /// <remarks>
        /// This is an undocumented property.
        /// </remarks>
        /// <seealso href="https://pushover.net/api/client#download">Pushover API documentation</seealso>
        [JsonProperty("user")]
        public ListMessagesUser User { get; set; }
    }
}
