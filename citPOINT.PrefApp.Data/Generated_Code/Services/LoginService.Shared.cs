#region → Usings   .
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
#endregion

#region → History  .

/* Date         User              Change
 * 
 * 07.09.10     Yousra Reda       Creation
 * 07.09.10     Yousra Reda       Custom validation for Email
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion
namespace citPOINT.PrefApp.Data.Web
{
    /// <summary>
    /// Shared Data between Web Side and Client Side
    /// </summary>
    public static partial class UserRules
    {

        #region → Methods        .

        #region → Public         . static

        /// <summary>
        /// validate user email address
        /// </summary>
        /// <param name="email">Value Of email</param>
        /// <param name="validationContext">Value Of validationContext</param>
        /// <returns>Success in validation or message error</returns>
        public static ValidationResult IsValidEmail(string EmailAddress, ValidationContext validationContext)
        {
            // user Email can be null
            if (EmailAddress == null)
                return ValidationResult.Success;

            // Return true if strIn is in valid e-mail format.
            if (Regex.IsMatch(EmailAddress,
                   @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" +
                   @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$"))
                return ValidationResult.Success;
            else
            {
                return new ValidationResult(ErrorResources.ValidationErrorInvalidEmail, new string[] { "EmailAddress" });
            }
        }

        /// <summary>
        /// Custom validation of whether password is more Than 50 Charatcers
        /// </summary>
        /// <param name="Password">Value Of Password</param>
        /// <param name="validationContext">Value Of validationContext</param>
        /// <returns>success if Password Is Max 50</returns>
        public static ValidationResult CheckPasswordMaxLength(string Password, ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(Password) && Password.Length > 50)
                return new ValidationResult(ErrorResources.ValidationErrorBadMaxPasswordLength, new string[] { validationContext.DisplayName });

            return ValidationResult.Success;
        }

        /// <summary>
        /// Checks the password min lenght.
        /// </summary>
        /// <param name="Password">The password.</param>
        /// <param name="validationContext">The validation context.</param>
        /// <returns>Success if password is less than 6</returns>
        public static ValidationResult CheckPasswordMinLength(string Password, ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(Password) && Password.Length < 6)
                return new ValidationResult(ErrorResources.ValidationErrorBadPasswordLength, new string[] { validationContext.DisplayName });

            return ValidationResult.Success;
        }
        #endregion
        #endregion
    }
}