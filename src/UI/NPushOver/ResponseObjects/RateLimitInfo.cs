using System;

namespace NPushover.ResponseObjects
{
    /// <summary>
    /// Represents rate limiting info returned by the Pushover services (if any).
    /// </summary>
    /// <seealso cref="NPushover.Exceptions.RateLimitExceededException"/>
    /// <seealso href="https://pushover.net/api#limits">Pushover API documentation</seealso>
    public class RateLimitInfo
    {
        /// <summary>
        /// Gets the monthly message limit (plus any additional purchased capacity).
        /// </summary>
        /// <seealso href="https://pushover.net/api#limits">Pushover API documentation</seealso>
        public int Limit { get; private set; }
        
        /// <summary>
        /// Gets the remaining monthly message limit (plus any additional purchased capacity).
        /// </summary>
        /// <seealso href="https://pushover.net/api#limits">Pushover API documentation</seealso>
        public int Remaining { get; private set; }

        /// <summary>
        /// Gets the date when the count for the rate limit will be rest.
        /// </summary>
        /// <seealso href="https://pushover.net/api#limits">Pushover API documentation</seealso>
        public DateTime Reset { get; private set; }

        /// <summary>
        /// Initializes a new <see cref="RateLimitInfo"/> object.
        /// </summary>
        internal RateLimitInfo(int limit, int remaining, DateTime reset)
        {
            this.Limit = limit;
            this.Remaining = remaining;
            this.Reset = reset;
        }
    }
}
