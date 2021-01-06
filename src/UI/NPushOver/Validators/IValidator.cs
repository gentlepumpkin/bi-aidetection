namespace NPushover.Validators
{
    /// <summary>
    /// Provides the base validator interface for NPushover validators.
    /// </summary>
    /// <typeparam name="T">The type the validator operates on.</typeparam>
    public interface IValidator<T>
    {
        /// <summary>
        /// Validates the given object and throws any exceptions when the object is not valid.
        /// </summary>
        /// <param name="paramName">The name of the parameter being validated.</param>
        /// <param name="obj">The object to validate.</param>
        void Validate(string paramName, T obj);
    }
}
