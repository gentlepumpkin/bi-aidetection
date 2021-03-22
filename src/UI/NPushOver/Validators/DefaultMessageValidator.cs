using AITool;

using NPushover.RequestObjects;

using System;

namespace NPushover.Validators
{
    /// <summary>
    /// Validates <see cref="Message"/>s.
    /// </summary>
    public class DefaultMessageValidator : IValidator<Message>
    {
        private const int MAXBODYLENGTH = 1024;
        private const int MAXTITLELENGTH = 250;
        private const int MAXSUPURLTITLELENGTH = 100;
        private static readonly TimeSpan MINRETRYEVERY = TimeSpan.FromSeconds(30);
        private static readonly TimeSpan MAXRETRYPERIOD = TimeSpan.FromHours(24);

        /// <summary>
        /// Validates the specified <see cref="Message"/> and throws whenever the <see cref="Message"/> is deemed invalid.
        /// </summary>
        /// <param name="paramName">The name of the parameter being validated.</param>
        /// <param name="message">The message to validate.</param>
        /// <seealso href="https://pushover.net/api">Pushover API documentation</seealso>
        /// <exception cref="ArgumentNullException">
        /// Thrown when message is null or any of the message's non-nullable properties is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when any of the message'properties contains an invalid (too long or otherwise invalid) value.
        /// </exception>
        public void Validate(string paramName, Message message)
        {
            if (message == null)
                throw new ArgumentNullException("message");

            if (message.Body == null)
                throw new ArgumentNullException("body");
            if (message.Body.Length > MAXBODYLENGTH)
                throw new ArgumentOutOfRangeException("body");

            if ((message.Title ?? string.Empty).Length > MAXTITLELENGTH)
                throw new ArgumentOutOfRangeException("title");

            if (!Enum.IsDefined(typeof(Priority), message.Priority))
                throw new ArgumentOutOfRangeException("priority");

            if (message.Priority == Priority.Emergency)
            {
                if (message.RetryOptions == null)
                    throw new ArgumentNullException("retryOptions");

                if (message.RetryOptions.RetryEvery < MINRETRYEVERY)
                    throw new ArgumentOutOfRangeException("retryOptions.retryEvery", $"RetryEvery less than {MINRETRYEVERY.FormatTS(true)}: {message.RetryOptions.RetryEvery.FormatTS(true)}");
                if (message.RetryOptions.RetryEvery > MAXRETRYPERIOD)
                    throw new ArgumentOutOfRangeException("retryOptions.retryEvery", $"RetryEvery is over {MAXRETRYPERIOD.FormatTS(true)}: {message.RetryOptions.RetryEvery.FormatTS(true)}");
                if (message.RetryOptions.RetryPeriod > MAXRETRYPERIOD)
                    throw new ArgumentOutOfRangeException("retryOptions.retryPeriod", $"RetryPeriod is over {MAXRETRYPERIOD.FormatTS(true)}: {message.RetryOptions.RetryPeriod.FormatTS(true)}");
                if (message.RetryOptions.RetryPeriod < TimeSpan.Zero)
                    throw new ArgumentOutOfRangeException("retryOptions.retryPeriod", $"RetryPeriod is less than 0?");
            }
            else
            {
                if (message.RetryOptions != null)
                    throw new ArgumentException("retryOptions");
            }

            if (message.SupplementaryUrl != null)
            {
                if (message.SupplementaryUrl.Uri == null)
                    throw new ArgumentNullException("supplementaryUrl.uri");

                if ((message.SupplementaryUrl.Title ?? string.Empty).Length > MAXSUPURLTITLELENGTH)
                    throw new ArgumentOutOfRangeException("supplementaryUrl.title");
            }
        }
    }
}
