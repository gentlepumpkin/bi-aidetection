using Newtonsoft.Json;

namespace NPushover.ResponseObjects
{
    /// <summary>
    /// Represents a response for a <see cref="M:NPushover.Pushover.LoginAsync(System.String,System.String)"/> call.
    /// </summary>
    /// <seealso href="https://pushover.net/api/client#login">Pushover API documentation</seealso>
    public class LoginResponse : PushoverUserResponse
    {
        /// <summary>
        /// Gets the user key.
        /// </summary>
        /// <seealso href="https://pushover.net/api/client#login">Pushover API documentation</seealso>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets the user's secret.
        /// </summary>
        /// <seealso href="https://pushover.net/api/client#login">Pushover API documentation</seealso>
        [JsonProperty("secret")]
        public string Secret { get; set; }
    }
}
