using System;

namespace NPushover.RequestObjects
{
    /// <summary>
    /// Represents options for retrying <see cref="Message"/> delivery.
    /// </summary>
    /// <remarks>
    /// These options can only be used/specified for <see cref="NPushover.RequestObjects.Priority.Emergency"/> messages.
    /// </remarks>
    /// <seealso cref="NPushover.Validators.DefaultMessageValidator"/>
    /// <seealso cref="NPushover.RequestObjects.Priority.Emergency"/>
    public class RetryOptions
    {
        /// <summary>
        /// Gets/sets how often the Pushover servers will send the same <see cref="Message"/> to the user. In a
        /// situation where  a user might be in a noisy environment or sleeping, retrying the notification (with sound
        /// and vibration) will help get his or her attention. The minimum value is 30 seconds between retries.
        /// </summary>
        /// <seealso cref="NPushover.Validators.DefaultMessageValidator"/>
        public TimeSpan RetryEvery { get; set; }

        /// <summary>
        /// Gets/set how long the notification will continue to be retried with the <see cref="RetryEvery">specified 
        /// interval</see>. If the notification has not been acknowledged in this time, it will be marked as expired 
        /// and will stop being sent to the user. Note that the notification is still shown to the user after it is 
        /// expired, but it will not prompt the user for acknowledgement. This maximum value allowed is 24 hours.
        /// </summary>
        /// <seealso cref="NPushover.Validators.DefaultMessageValidator"/>
        public TimeSpan RetryPeriod { get; set; }

        /// <summary>
        /// Gets/sets the (optional) callback parameter may be supplied with a publicly-accessible URL that the Pushover
        /// servers will send a request to when the user has acknowledged the message.
        /// </summary>
        public Uri CallBackUrl { get; set; }
    }
}
