using NPushover.Exceptions;
using System;
using System.Text.RegularExpressions;

namespace NPushover.Validators
{
    /// <summary>
    /// Provides a baseclass for validators based on a <see cref="Regex">regular expression</see> to validate values.
    /// </summary>
    public abstract class RegexValidator : IValidator<string>
    {
        /// <summary>
        /// Gets the Regex for the validator.
        /// </summary>
        protected Regex ValidationRegex { get; private set; }

        /// <summary>
        /// Default options for most <see cref="Regex"/> objects.
        /// </summary>
        protected const RegexOptions DefaultRegexOptions = RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.Singleline;

        /// <summary>
        /// Initializes a new validator with the specified <see cref="Regex"/>.
        /// </summary>
        /// <param name="regex">The <see cref="Regex"/> for the validator.</param>
        /// <exception cref="ArgumentNullException">Thrown when regex is null.</exception>
        public RegexValidator(Regex regex)
        {
            if (regex == null)
                throw new ArgumentNullException("regex");

            this.ValidationRegex = regex;
        }

        /// <summary>
        /// Executes the validator's regex and throws whenever the value doesn't match the validator's regex.
        /// </summary>
        /// <param name="paramName">The name of the parameter being validated.</param>
        /// <param name="value">The value to validate/match.</param>
        /// <remarks>
        /// This method uses the <see cref="Regex"/>'s <see cref="Regex.IsMatch(System.String)"/> method.
        /// </remarks>
        /// <exception cref="ArgumentNullException">Thrown when value is null.</exception>
        /// <exception cref="InvalidKeyException">Thrown when value does not match the validator's <see cref="Regex"/>.</exception>
        public void Validate(string paramName, string value)
        {
            if (value == null)
                throw new ArgumentNullException(paramName);
            if (!this.ValidationRegex.IsMatch(value))
                throw new InvalidKeyException(paramName, value);
        }
    }

    /// <summary>
    /// Validates application keys.
    /// </summary>
    /// <seealso href="https://pushover.net/api#registration">Pushover API documentation</seealso>
    public class ApplicationKeyValidator : RegexValidator
    {
        /// <summary>
        /// Initializes a new instance of an <see cref="ApplicationKeyValidator"/>.
        /// </summary>
        public ApplicationKeyValidator()
            : base(new Regex("^[A-Za-z0-9]{30}$", DefaultRegexOptions)) { }
    }

    /// <summary>
    /// Validates user and/or group id's.
    /// </summary>
    /// <seealso href="https://pushover.net/api#identifiers">Pushover API documentation</seealso>
    public class UserOrGroupKeyValidator : RegexValidator
    {
        /// <summary>
        /// Initializes a new instance of an <see cref="UserOrGroupKeyValidator"/>.
        /// </summary>
        public UserOrGroupKeyValidator()
            : base(new Regex("^[A-Za-z0-9]{30}$", DefaultRegexOptions)) { }
    }

    /// <summary>
    /// Validates devicenames.
    /// </summary>
    /// <seealso href="https://pushover.net/api#identifiers">Pushover API documentation</seealso>
    public class DeviceNameValidator : RegexValidator
    {
        /// <summary>
        /// Initializes a new instance of an <see cref="DeviceNameValidator"/>.
        /// </summary>
        public DeviceNameValidator()
            : base(new Regex("^[A-Za-z0-9_-]{1,25}$", DefaultRegexOptions)) { }
    }

    /// <summary>
    /// Validates receipts.
    /// </summary>
    /// <seealso href="https://pushover.net/api#receipt">Pushover API documentation</seealso>
    public class ReceiptValidator : RegexValidator
    {
        /// <summary>
        /// Initializes a new instance of an <see cref="ReceiptValidator"/>.
        /// </summary>
        public ReceiptValidator()
            : base(new Regex("^[A-Za-z0-9]{30}$", DefaultRegexOptions)) { }
    }
}
