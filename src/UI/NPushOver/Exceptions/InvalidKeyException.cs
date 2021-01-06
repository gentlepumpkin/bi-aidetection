namespace NPushover.Exceptions
{
    /// <summary>
    /// Represents an error that occurs when a key, id or token is determined to be invalid based on it's format.
    /// </summary>
    public class InvalidKeyException : PushoverException
    {
        /// <summary>
        /// Gets the key, id or token that caused the current exception.
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidKeyException"/> class with the name of the parameter and
        /// a reference to the key, id or token that is the cause of this exception.
        /// </summary>
        /// <param name="paramName">The name of the parameter that caused the current exception.</param>
        /// <param name="key">The key, id or token that is the cause of this exception.</param>
        public InvalidKeyException(string paramName, string key)
            : base(string.Format("Invalid argument: {0}", paramName))
        {
            this.Key = key;
        }
    }
}
