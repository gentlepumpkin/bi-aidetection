using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NPushover.Converters;
using NPushover.RequestObjects;
using System;

namespace NPushover.ResponseObjects
{
    /// <summary>
    /// Represents information about a <see cref="ListMessagesResponse"/> message for a 
    /// <see cref="M:NPushover.Pushover.ListMessagesAsync(System.String,System.String)"/> call.
    /// </summary>
    /// <seealso href="https://pushover.net/api/client#download">Pushover API documentation</seealso>
    public class StoredMessage
    {
        /// <summary>
        /// Gets the unique id of the <see cref="Message"/>, relative to the device.
        /// </summary>
        /// <seealso href="https://pushover.net/api/client#download">Pushover API documentation</seealso>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets the unique id of the <see cref="Message"/> relative to all devices on the same user's account.
        /// </summary>
        /// <remarks>
        /// When a <see cref="Message"/> is received by Pushover and sent to all devices on a user's account, each 
        /// <see cref="Message"/> is given the same <see cref="UMId"/> value. This is primarily used for cross-device
        /// notification dismissal sync.
        /// </remarks>
        /// <seealso href="https://pushover.net/api/client#download">Pushover API documentation</seealso>
        [JsonProperty("umid")]
        public int UMId { get; set; }

        /// <summary>
        /// Gets the title of the <see cref="Message"/>, if present. If not present, the name of the application (app)
        /// should be displayed.
        /// </summary>
        /// <seealso href="https://pushover.net/api/client#download">Pushover API documentation</seealso>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets the body of the <see cref="Message"/>.
        /// </summary>
        /// <seealso href="https://pushover.net/api/client#download">Pushover API documentation</seealso>
        [JsonProperty("message")]
        public string Body { get; set; }

        /// <summary>
        /// Gets the name of the application that sent the <see cref="Message"/>. This may not be unique.
        /// </summary>
        /// <seealso href="https://pushover.net/api/client#download">Pushover API documentation</seealso>
        [JsonProperty("app")]
        public string Application { get; set; }

        /// <summary>
        /// Gets the unique id of the application that sent the <see cref="Message"/>.
        /// </summary>
        /// <seealso href="https://pushover.net/api/client#download">Pushover API documentation</seealso>
        [JsonProperty("aid")]
        public int ApplicationId { get; set; }

        /// <summary>
        /// Gets the icon filename of the application that sent the <see cref="Message"/>.
        /// </summary>
        /// <remarks>
        /// The image data can be fetched using <see cref="M:NPushover.Pushover.DownloadIconAsync(System.String)"/>. When an
        /// application changes its icon, this value will change.
        /// </remarks>
        /// <seealso cref="M:NPushover.Pushover.DownloadIconAsync(System.String)"/>
        /// <seealso href="https://pushover.net/api/client#download">Pushover API documentation</seealso>
        [JsonProperty("icon")]
        public string Icon { get; set; }

        /// <summary>
        /// Gets the date and time the <see cref="Message"/> was received by the Pushover services, unless the sender
        /// overrode the timestamp when sending it.
        /// </summary>
        /// <seealso href="https://pushover.net/api/client#download">Pushover API documentation</seealso>
        [JsonProperty("date")]
        [JsonConverter(typeof(Newtonsoft.Json.Converters.UnixDateTimeConverter))]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Gets the <see cref="Priority"/> of the <see cref="Message"/>.
        /// </summary>
        /// <seealso href="https://pushover.net/api/client#download">Pushover API documentation</seealso>
        [JsonProperty("priority")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Priority Priority { get; set; }

        /// <summary>
        /// Gets the <see cref="Sound"/>, if specified, from a <see cref="Message"/>.
        /// </summary>
        /// <remarks>
        /// This resource should be downloaded and cached.
        /// </remarks>
        /// <seealso cref="O:NPushover.Pushover.DownloadSoundAsync"/>
        /// <seealso href="https://pushover.net/api/client#download">Pushover API documentation</seealso>
        [JsonProperty("sound")]
        public string Sound { get; set; }

        /// <summary>
        /// Gets the <see cref="SupplementaryURL"/>'s url for the <see cref="Message"/>, if any.
        /// </summary>
        /// <seealso href="https://pushover.net/api/client#download">Pushover API documentation</seealso>
        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        /// Gets the <see cref="SupplementaryURL"/>'s title for the <see cref="Message"/>, if any.
        /// </summary>
        /// <seealso href="https://pushover.net/api/client#download">Pushover API documentation</seealso>
        [JsonProperty("url_title")]
        public string UrlTitle { get; set; }

        /// <summary>
        /// Gets whether the <see cref="Message"/> was acknowledged. Used for <see cref="NPushover.RequestObjects.Priority.Emergency"/> messages.
        /// </summary>
        /// <seealso href="https://pushover.net/api/client#download">Pushover API documentation</seealso>
        [JsonProperty("acked")]
        [JsonConverter(typeof(BoolConverter))]
        public bool Acknowledged { get; set; }

        /// <summary>
        /// Gets the <see cref="Message"/>'s receipt code, if any. Used for <see cref="NPushover.RequestObjects.Priority.Emergency"/> messages.
        /// </summary>
        /// <seealso href="https://pushover.net/api/client#download">Pushover API documentation</seealso>
        [JsonProperty("receipt")]
        public string Receipt { get; set; }

        /// <summary>
        /// Gets whether the <see cref="Message"/> contains HTML.
        /// </summary>
        /// <see cref="Message.IsHtmlBody"/>
        /// <seealso href="https://pushover.net/api/client#download">Pushover API documentation</seealso>
        [JsonProperty("html")]
        [JsonConverter(typeof(BoolConverter))]
        public bool IsHtmlBody { get; set; }
    }
}
