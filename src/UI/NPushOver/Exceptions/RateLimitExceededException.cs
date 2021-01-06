using NPushover.ResponseObjects;
using System;

namespace NPushover.Exceptions
{
    /// <summary>
    /// Represents an error that occurs when the Pushover service returns a "rate limit exceeded" response.
    /// </summary>
    /// <remarks>
    /// Exceptions of this type are caused by the Pushover service.
    /// </remarks>
    public class RateLimitExceededException : ResponseException
    {
        /// <summary>
        /// Returns <see cref="RateLimitInfo"/> reported by the Pushover service containing information on why a request
        /// was ratelimited and when the ratelimit is reset.
        /// </summary>
        public RateLimitInfo RateLimitInfo { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RateLimitExceededException"/> class with a reference to the 
        /// <see cref="PushoverResponse"/> a that is the cause of this exception.
        /// </summary>
        /// <param name="response">The <see cref="PushoverResponse"/> that is the cause for the exception.</param>
        public RateLimitExceededException(PushoverResponse response)
            : this(response, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="RateLimitExceededException"/> class with a reference to the 
        /// <see cref="PushoverResponse"/> and a reference to the inner exception that are the cause of this exception.
        /// </summary>
        /// <param name="response">The <see cref="PushoverResponse"/> that is the cause for the exception.</param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null reference if no inner exception.
        /// </param>
        public RateLimitExceededException(PushoverResponse response, Exception innerException)
            : base("Rate limit exceeded", response, innerException)
        {
            this.RateLimitInfo = response.RateLimitInfo;
        }
    }
}
