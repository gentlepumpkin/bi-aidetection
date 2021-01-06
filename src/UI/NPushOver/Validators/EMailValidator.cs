using System;
using System.Linq;

namespace NPushover.Validators
{
    /// <summary>
    /// Provides VERY simple e-mail address validation (all that's required to validate is the string to contain an
    /// '@', the rest is up to Pushover's servers).
    /// </summary>
    public class EMailValidator : IValidator<string>
    {
        /// <summary>
        /// Validates an e-mail address and throws whenever the e-mail address is deemed invalid.
        /// </summary>
        /// <param name="paramName">The name of the parameter being verified.</param>
        /// <param name="email">The e-mail address to validate.</param>
        /// <remarks>
        /// Provides VERY simple e-mail address validation (all that's required to validate is the string to contain an
        /// '@', the rest is up to Pushover's servers).
        /// </remarks>
        /// <exception cref="ArgumentNullException">Thrown when email is null.</exception>
        /// <exception cref="ArgumentException">Thrown when email is invalid.</exception>
        public void Validate(string paramName, string email)
        {
            if (email == null)
                throw new ArgumentNullException(paramName);

            if (!email.Contains('@'))
                throw new ArgumentException("Invalid email address", paramName);
        }
    }
}
