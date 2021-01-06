using Newtonsoft.Json;

namespace NPushover.ResponseObjects
{
    /// <summary>
    /// Represents a response for a <see cref="O:NPushover.Pushover.ValidateUserOrGroupAsync"/> call.
    /// </summary>
    /// <seealso href="https://pushover.net/api#verification">Pushover API documentation</seealso>
    public class ValidateUserOrGroupResponse : PushoverUserResponse
    {
        /// <summary>
        /// Gets the names of the user's active devices.
        /// </summary>
        /// <seealso href="https://pushover.net/api#verification">Pushover API documentation</seealso>
        [JsonProperty("devices")]
        public string[] Devices { get; set; }
    }
}
