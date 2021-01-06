using NPushover.ResponseObjects;
using System;

namespace NPushover.Exceptions
{
    /// <summary>
    /// Provides a baseclass for all exceptions encountered in Pushover responses to requests.
    /// </summary>
    public class ResponseException : PushoverException
    {
        /// <summary>
        /// Gets the <see cref="PushoverResponse"/> that caused the exception.
        /// </summary>
        public PushoverResponse Response { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public ResponseException(string message)
            : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseException"/> class with a specified error message and
        /// a reference to the <see cref="PushoverResponse"/> that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="response">The  <see cref="PushoverResponse"/> that is the cause for the exception.</param>
        public ResponseException(string message, PushoverResponse response)
            : this(message, response, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseException"/> class with a specified error message,
        /// a reference to the <see cref="PushoverResponse"/> and a reference to the inner exception that are the cause
        /// of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="response">The  <see cref="PushoverResponse"/> that is the cause for the exception.</param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null reference if no inner exception.
        /// </param>
        public ResponseException(string message, PushoverResponse response, Exception innerException)
            : base(message, innerException)
        {
            this.Response = response;
        }
    }
}
