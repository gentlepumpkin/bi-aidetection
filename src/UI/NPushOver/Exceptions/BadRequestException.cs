using NPushover.ResponseObjects;
using System;

namespace NPushover.Exceptions
{
    /// <summary>
    /// Represents an error that occurs when pushover returned an HTTP status 400: bad request.
    /// </summary>
    /// <remarks>
    /// Exceptions of this type are typically caused by sending incorrect values or not sending all required values
    /// to the Pushover service.
    /// </remarks>
    public class BadRequestException : ResponseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BadRequestException"/> class with an empty (null) 
        /// <see cref="PushoverResponse"/>.
        /// </summary>
        public BadRequestException()
            : this(null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BadRequestException"/> class with a reference to the 
        /// <see cref="PushoverResponse"/> a that is the cause of this exception.
        /// </summary>
        /// <param name="response">The <see cref="PushoverResponse"/> that is the cause for the exception.</param>
        public BadRequestException(PushoverResponse response)
            : this(response, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BadRequestException"/> class with a reference to the 
        /// <see cref="PushoverResponse"/> and a reference to the inner exception that are the cause of this exception.
        /// </summary>
        /// <param name="response">The <see cref="PushoverResponse"/> that is the cause for the exception.</param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null reference if no inner exception.
        /// </param>
        public BadRequestException(PushoverResponse response, Exception innerException)
            : base("Bad request", response, innerException) { }
    }
}
