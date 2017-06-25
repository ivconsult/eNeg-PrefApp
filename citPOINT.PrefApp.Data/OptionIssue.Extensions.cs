
#region → Usings   .
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
#endregion

#region → History  .

/* Date         User            Change
 * 
 *22.09.10     M.Wahab     creation
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
    /// OptionIssue class client-side extensions
    /// </summary>
    public partial class OptionIssue
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
        /// Try Try Validate by Property name  
        /// </summary> 
        /// <returns>True Or False </returns> 
        public bool TryValidateProperty(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentNullException("propertyName");
            }
            if (propertyName == "OptionIssueID"
             || propertyName == "OptionIssueValue"
             || propertyName == "IssueID"
             || propertyName == "OptionIssueWeight"
             || propertyName == "Deleted"
             || propertyName == "DeletedBy"
             || propertyName == "DeletedOn"
            )
            {

                ValidationContext context = new ValidationContext(this, null, null) { MemberName = propertyName };
                var validationResults = new Collection<ValidationResult>();
                if (propertyName == "OptionIssueID")
                    return Validator.TryValidateProperty(this.OptionIssueID, context, validationResults);
                if (propertyName == "OptionIssueValue")
                    return Validator.TryValidateProperty(this.OptionIssueValue, context, validationResults);
                if (propertyName == "IssueID")
                    return Validator.TryValidateProperty(this.IssueID, context, validationResults);
                if (propertyName == "OptionIssueWeight")
                    return Validator.TryValidateProperty(this.OptionIssueWeight, context, validationResults);
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
        /// <returns>return new Instance of OptionIssue</returns>
        public OptionIssue Clone()
        {
            OptionIssue mOptionIssue = new OptionIssue
            {
                OptionIssueID = this.OptionIssueID,
                OptionIssueValue = this.OptionIssueValue,
                IssueID = this.IssueID,
                OptionIssueWeight = this.OptionIssueWeight,
                Deleted = this.Deleted,
                DeletedBy = this.DeletedBy,
                DeletedOn = this.DeletedOn
            };

            return mOptionIssue;
        }


        #endregion Methods

    }

}
