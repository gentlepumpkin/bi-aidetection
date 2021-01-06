using Newtonsoft.Json;

namespace NPushover.ResponseObjects
{
    /// <summary>
    /// Represents a response for a <see cref="O:NPushover.Pushover.MigrateSubscriptionAsync"/> call.
    /// </summary>
    /// <remarks>
    /// Applications that formerly collected Pushover user keys are encouraged to migrate to subscription keys.
    /// </remarks>
    /// <seealso href="https://pushover.net/api/subscriptions#migration">Pushover API documentation</seealso>
    public class MigrateSubscriptionResponse : PushoverUserResponse
    {
        /// <summary>
        /// Gets the key to save in place of the user's original key (of wich the latter can be discarded).
        /// </summary>
        /// <seealso cref="O:NPushover.Pushover.MigrateSubscriptionAsync"/>
        /// <seealso href="https://pushover.net/api/subscriptions#migration">Pushover API documentation</seealso>
        [JsonProperty("subscribed_user_key")]
        public string SubscribedUserKey { get; set; }
    }
}
