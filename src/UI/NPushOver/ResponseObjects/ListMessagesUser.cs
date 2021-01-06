using Newtonsoft.Json;

namespace NPushover.ResponseObjects
{
    /// <summary>
    /// Represents information about a user which is part of the response from a <see cref="ListMessagesResponse"/> from
    /// a <see cref="M:NPushover.Pushover.ListMessagesAsync(System.String,System.String)"/> call.
    /// </summary>
    /// <remarks>
    /// This part of the response is undocumented by Pushover.
    /// </remarks>
    public class ListMessagesUser
    {
        /// <summary>
        /// Indicates if the user is, currently(?), in quiet hours.
        /// </summary>
        [JsonProperty("quiet_hours")]
        public bool QuietHours { get; set; }

        /// <summary>
        /// Indicates if the user has a license for Android devices.
        /// </summary>
        [JsonProperty("is_android_licensed")]
        public bool IsAndroidLicensed { get; set; }

        /// <summary>
        /// Indicates if the user has a license for iOS devices.
        /// </summary>
        [JsonProperty("is_ios_licensed")]
        public bool IsIosLicensed { get; set; }

        /// <summary>
        /// Indicates if the user has a license for desktop application.
        /// </summary>
        [JsonProperty("is_desktop_licensed")]
        public bool IsDesktopLicensed { get; set; }
    }
}
