using System;

namespace NPushover.RequestObjects
{
    /// <summary>
    /// Represents a supplementary (to a <see cref="Message"/>) URL.
    /// </summary>
    /// <remarks>
    /// <para>
    ///     It may be desirable to include a supplementary URL that is not included in the <see cref="Message"/> 
    ///     <see cref="Message.Body"/>, but available for the user to click on. This <see cref="SupplementaryURL"/> will 
    ///     be passed directly to the device client, with a URL title of the supplied <see cref="Title"/> (defaulting to 
    ///     the <see cref="Uri"/> itself if no title given).
    /// </para>
    /// <para>
    ///     Supplementary URLs can be useful for presenting long URLs in a notification as well as interacting with 3rd
    ///     party applications. For example, if a Pushover application were sending Twitter messages to a user, a 
    ///     <see cref="SupplementaryURL"/> may be sent that includes the actual link to the message that would open in
    ///     the user's browser (e.g., http://twitter.com/user/status/12345) or a URL that will perform some action in
    ///     another application installed on the device (e.g., twitter://status?id=12345). The message displayed in the
    ///     Pushover client would be the actual contents of the Twitter message (with any URLs originally contained in
    ///     it automatically turned into links), but the supplementary link will be shown underneath it as an option
    ///     available to the user when the message is highlighted.
    /// </para>
    /// <para>
    ///     While there are some standard URL schemes like tel: and sms: that will be handled by iOS and Android the
    ///     same way, others like the twitter:// scheme used above are highly specific to the platform and other
    ///     applications installed on the device. A list of common URL schemes supported by applications on iOS can be
    ///     found at handleopenurl.com, and a list handled natively by Android can be found on developer.android.com.
    ///     Since Pushover users may be on different platforms and have different 3rd party applications installed, it
    ///     is not recommended to use app-specific URL schemes as supplementary URLs in public plugins, websites, and
    ///     apps.
    /// </para>
    /// <para>
    ///     Due to limitations of the iOS push notification service, supplementary URLs are not able to be shown with
    ///     push notifications. Notifications in the Notification Center will only show the title and message. The user
    ///     must tap on the notification or otherwise open the Pushover client, which will perform a sync with the
    ///     Pushover servers, in order to download the attached supplementary URL. Since these URLs are supplementary, 
    ///     they should not be used as the primary content of your notification. If your notification is just a URL, 
    ///     include it in the message body instead.
    /// </para>
    /// </remarks>
    public class SupplementaryURL
    {
        /// <summary>
        /// Gets/sets the <see cref="Uri"/> of the <see cref="SupplementaryURL"/>.
        /// </summary>
        /// <seealso cref="SupplementaryURL"/>
        public Uri Uri { get; set; }

        /// <summary>
        /// Gets/sets the title (or text) to be displayed for the URL.
        /// </summary>
        /// <seealso cref="SupplementaryURL"/>
        public string Title { get; set; }
    }
}
