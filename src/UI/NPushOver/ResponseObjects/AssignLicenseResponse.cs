using Newtonsoft.Json;

namespace NPushover.ResponseObjects
{
    /// <summary>
    /// Represents a response for a <see cref="M:NPushover.Pushover.AssignLicenseAsync(System.String,System.String)"/> call.
    /// </summary>
    /// <seealso href="https://pushover.net/api/licensing#assign">Pushover API documentation</seealso>
    public class AssignLicenseResponse : PushoverUserResponse
    {
        /// <summary>
        /// Gets the number of credits available for the application.
        /// </summary>
        /// <seealso href="https://pushover.net/api/licensing#assign">Pushover API documentation</seealso>
        [JsonProperty("credits")]
        public int Credits { get; set; }
    }
}
