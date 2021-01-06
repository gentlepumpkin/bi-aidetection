using Newtonsoft.Json;

namespace NPushover.ResponseObjects
{
    /// <summary>
    /// Represents a response for a <see cref="M:NPushover.Pushover.RegisterDeviceAsync(System.String,System.String)"/> call.
    /// </summary>
    /// <seealso href="https://pushover.net/api/client#register">Pushover API documentation</seealso>
    public class RegisterDeviceResponse : PushoverUserResponse
    {
        /// <summary>
        /// Gets the device's unique id.
        /// </summary>
        /// <remarks>
        /// Store this value in a secure location.
        /// </remarks>
        /// <seealso href="https://pushover.net/api/client#register">Pushover API documentation</seealso>
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
