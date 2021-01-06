using Newtonsoft.Json;
using System.Collections.Generic;

namespace NPushover.ResponseObjects
{
    /// <summary>
    /// Represents a response for a <see cref="M:NPushover.Pushover.ListSoundsAsync"/> call.
    /// </summary>
    /// <seealso href="https://pushover.net/api#sounds">Pushover API documentation</seealso>
    public class SoundsResponse : PushoverUserResponse
    {
        /// <summary>
        /// Gets available sounds from the Pushover services (name / description).
        /// </summary>
        /// <seealso href="https://pushover.net/api#sounds">Pushover API documentation</seealso>
        [JsonProperty("sounds")]
        public IDictionary<string, string> Sounds { get; set; }
    }
}
