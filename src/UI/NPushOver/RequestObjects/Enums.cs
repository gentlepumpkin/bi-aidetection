namespace NPushover.RequestObjects
{
    /// <summary>
    /// Defines the available priorities for <see cref="NPushover.RequestObjects.Message"/>s.
    /// </summary>
    public enum Priority
    {
        /// <summary>
        /// When the priority parameter is specified with this value, messages will be considered lowest priority and
        /// will not generate any notification. On iOS, the application badge number will be increased.
        /// </summary>
        Lowest = -2,

        /// <summary>
        /// Messages this priority will be considered low priority and will not generate any sound or vibration, but
        /// will still generate a popup/scrolling notification depending on the client operating system. Messages 
        /// delivered during a user's quiet hours are sent as though they had a priority of <see cref="Lowest"/>.
        /// </summary>
        Low = -1,

        /// <summary>
        /// Messages sent with this priority will have the default priority. These messages trigger sound, vibration, 
        /// and display an alert according to the user's device settings. On iOS, the message will display at the top 
        /// of the screen or as a modal dialog, as well as in the notification center. On Android, the message will 
        /// scroll at the top of the screen and appear in the notification center. If a user has quiet hours set and 
        /// the message is received during those times, your message will be delivered as though it had a priority of
        /// </summary>
        Normal = 0,

        /// <summary>
        /// Messages sent with this priority are high priority messages that bypass a user's quiet hours. These
        /// messages will always play a sound and vibrate (if the user's device is configured to) regardless of the
        /// delivery time. High-priority should only be used when necessary and appropriate. High-priority messages are
        /// highlighted in red in the device clients.
        /// </summary>
        High = 1,

        /// <summary>
        /// Emergency-priority notifications are similar to high-priority notifications, but they are repeated until
        /// the notification is acknowledged by the user. These are designed for dispatching and on-call situations
        /// where it is critical that a notification be repeatedly shown to the user (or all users of the group that
        /// the message was sent to) until it is acknowledged. Applications sending emergency notifications are issued
        /// a receipt that can be used to get the status of a notification and find out whether it was acknowledged, or
        /// automatically receive a callback when the user has acknowledged the notification. To send an 
        /// emergency-priority notification, the priority parameter must be set to this value and the 
        /// <see cref="Message.RetryOptions"/> must be supplied.
        /// </summary>
        Emergency = 2
    }

    /// <summary>
    /// Defines the available sounds (however, more sounds may become available; see <see cref="M:NPushover.Pushover.ListSoundsAsync"/>).
    /// </summary>
    public enum Sounds
    {
        /// <summary>Pushover (default)</summary>
        Pushover,

        /// <summary>Bike</summary>
        Bike,

        /// <summary>Bugle</summary>
        Bugle,

        /// <summary>Cash register</summary>
        Cashregister,

        /// <summary>Classical</summary>
        Classical,

        /// <summary>Cosmic</summary>
        Cosmic,

        /// <summary>Falling</summary>
        Falling,

        /// <summary>Gamelan</summary>
        Gamelan,

        /// <summary>Incoming</summary>
        Incoming,

        /// <summary>Intermission</summary>
        Intermission,

        /// <summary>Magic</summary>
        Magic,

        /// <summary>Mechanical</summary>
        Mechanical,

        /// <summary>Piano bar</summary>
        Pianobar,

        /// <summary>Siren</summary>
        Siren,

        /// <summary>Space alarm</summary>
        Spacealarm,

        /// <summary>Tugboat</summary>
        Tugboat,

        /// <summary>Alien alarm (long)</summary>
        Alien,

        /// <summary>Climb (long)</summary>
        Climb,

        /// <summary>Persistent (long)</summary>
        Persistent,

        /// <summary>Pushover echo (long)</summary>
        Echo,

        /// <summary>Up down (long)</summary>
        Updown,

        /// <summary>None (silent)</summary>
        None
    }

    /// <summary>
    /// Defines the available Operating System choices for licensing (see 
    /// <see cref="O:NPushover.Pushover.AssignLicenseAsync"/>).
    /// </summary>
    public enum OS
    {
        /// <summary>Assign the license to the first operating system the user registers with</summary>
        Any,

        /// <summary>Android</summary>
        Android,

        /// <summary>iOS</summary>
        iOS,

        /// <summary>Desktop</summary>
        Desktop
    }

    /// <summary>
    /// Defines the available audio formats for retrieval (see <see cref="O:NPushover.Pushover.DownloadSoundAsync"/>).
    /// </summary>
    public enum AudioFormat
    {
        /// <summary>Mp3</summary>
        Mp3,

        /// <summary>Wave</summary>
        Wav
    }
}
