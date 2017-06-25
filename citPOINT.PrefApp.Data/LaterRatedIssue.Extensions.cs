
#region → Usings   .
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
#endregion

#region → History  .

/* Date         User            Change
 * 
 * 19.10.10     Yousra Reda     creation
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
    /// LaterRatedIssue class client-side extensions
    /// </summary>
    public partial class LaterRatedIssue
    {
        #region → Methods        .

        /// <summary>
        /// Try validate for the OptionIssue class
        /// </summary>
        /// <returns>True Or False </returns>
        public bool TryValidateObject()
        {


            ValidationContext context = new ValidationContext(this, null, null);
            var validationResults = new Collection<ValidationResult>();

            if (Validator.TryValidateObject(this, context, validationResults, false) == false)
            {
                foreach (ValidationResult error in validationResults)
                {
                    this.ValidationErrors.Add(error);
                }
                return false;
            }


            return true;
        }


        /// <summary>
        /// Tries the validate property.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        public bool TryValidateProperty(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentNullException("propertyName");
            }
            if (propertyName == "LaterRatedIssueID"
             || propertyName == "LaterRatedIssueValue"
             || propertyName == "IssueID"
             || propertyName == "LaterRatedIssueWeight"
             || propertyName == "Deleted"
             || propertyName == "DeletedBy"
             || propertyName == "DeletedOn"
            )
            {

                ValidationContext context = new ValidationContext(this, null, null) { MemberName = propertyName };
                var validationResults = new Collection<ValidationResult>();
                if (propertyName == "LaterRatedIssueID")
                    return Validator.TryValidateProperty(this.LaterRatedIssueID, context, validationResults);
                if (propertyName == "LaterRatedIssueValue")
                    return Validator.TryValidateProperty(this.LaterRatedIssueValue, context, validationResults);
                if (propertyName == "IssueID")
                    return Validator.TryValidateProperty(this.IssueID, context, validationResults);
                if (propertyName == "LaterRatedIssueWeight")
                    return Validator.TryValidateProperty(this.LaterRatedIssueWeight, context, validationResults);
                if (propertyName == "Deleted")
                    return Validator.TryValidateProperty(this.Deleted, context, validationResults);
                if (propertyName == "DeletedBy")
                    return Validator.TryValidateProperty(this.DeletedBy, context, validationResults);
                if (propertyName == "DeletedOn")
                    return Validator.TryValidateProperty(this.DeletedOn, context, validationResults);
            }
            return false;
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>return new Instance of LaterRatedIssue</returns>
        public LaterRatedIssue Clone()
        {
            LaterRatedIssue mLaterRatedIssue = new LaterRatedIssue
            {
                LaterRatedIssueID = this.LaterRatedIssueID,
                LaterRatedIssueValue = this.LaterRatedIssueValue,
                IssueID = this.IssueID,
                LaterRatedIssueWeight = this.LaterRatedIssueWeight,
                Deleted = this.Deleted,
                DeletedBy = this.DeletedBy,
                DeletedOn = this.DeletedOn
            };

            return mLaterRatedIssue;
        }


        #endregion Methods
    }
}
