using Newtonsoft.Json;
namespace NPushover.ResponseObjects
{
    /// <summary>
    /// Provides a base class for responses received from calls to the Pushover services.
    /// </summary>
    /// <seealso href="https://pushover.net/api#response">Pushover API documentation</seealso>
    public class PushoverResponse
    {
        /// <summary>
        /// Gets the status of the response; a value of 0 indicates a problem, 1 indicates OK.
        /// </summary>
        /// <seealso href="https://pushover.net/api#response">Pushover API documentation</seealso>
        [JsonProperty("status")]
        public int Status { get; set; }

        /// <summary>
        /// Gets the unique id for the request.
        /// </summary>
        /// <seealso href="https://pushover.net/api#response">Pushover API documentation</seealso>
        [JsonProperty("request")]
        public string Request { get; set; }

        /// <summary>
        /// Gets a receipt code (if any). Null otherwise.
        /// </summary>
        /// <remarks>
        /// Only <see cref="NPushover.RequestObjects.Message"/>s that are sent with a 
        /// <see cref="NPushover.RequestObjects.Priority"/> <see cref="NPushover.RequestObjects.Priority.Emergency"/> a
        /// receipt code will be returned.
        /// </remarks>
        /// <seealso href="https://pushover.net/api#response">Pushover API documentation</seealso>
        /// <seealso href="https://pushover.net/api#receipt">Pushover API documentation</seealso>
        [JsonProperty("receipt")]
        public string Receipt { get; set; }

        /// <summary>
        /// Gets any errors returned by the Pushover services. Null otherwise.
        /// </summary>
        /// <seealso href="https://pushover.net/api#response">Pushover API documentation</seealso>
        [JsonProperty("errors")]
        public string[] Errors { get; set; }

        /// <summary>
        /// Gets information about ratelimiting returned by the Pushover services, if any. Null otherwise.
        /// </summary>
        /// <remarks>
        /// Usually only a response to a <see cref="O:NPushover.Pushover.SendPushoverMessageAsync"/> contains this
        /// information.
        /// </remarks>
        /// <seealso href="https://pushover.net/api#response">Pushover API documentation</seealso>
        /// <seealso href="https://pushover.net/api#limits">Pushover API documentation</seealso>
        [JsonIgnore]
        public RateLimitInfo RateLimitInfo { get; set; }

        /// <summary>
        /// Indicates if the <see cref="PushoverResponse"/> contains any errors.
        /// </summary>
        public bool HasErrors
        {
            get { return this.Errors != null && this.Errors.Length > 0; }
        }

        /// <summary>
        /// Indicates if the <see cref="PushoverResponse"/> has a <see cref="Status"/> code that represents an OK result.
        /// </summary>
        public bool IsOkStatus
        {
            get { return this.Status == 1; }
        }

        /// <summary>
        /// Indicates if the <see cref="PushoverResponse"/> is OK: no <see cref="Errors"/> and a <see cref="Status"/>
        /// that represents an OK value.
        /// </summary>
        public bool IsOk
        {
            get { return this.IsOkStatus && !this.HasErrors; }
        }
    }

    /// <summary>
    /// Represents a response from the Pushover services that may also contain user information.
    /// </summary>
    /// <seealso href="https://pushover.net/api#response">Pushover API documentation</seealso>
    public class PushoverUserResponse : PushoverResponse
    {
        /// <summary>
        /// Gets the user information, if any. Null otherwise.
        /// </summary>
        /// <seealso href="https://pushover.net/api#response">Pushover API documentation</seealso>
        [JsonProperty("user")]
        public string User { get; set; }
    }
}
