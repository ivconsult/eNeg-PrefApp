
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
    /// NumericIssue class client-side extensions
    /// </summary>
    public partial class NumericIssue : INumericIssue
    {
        #region → Methods        .

        /// <summary>
        /// Try validate for the NumericIssue class
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
            if (propertyName == "NumericIssueID"
             || propertyName == "IssueID"
             || propertyName == "MinimumValue"
             || propertyName == "MaximumValue"
             || propertyName == "OptimumValueStart"
             || propertyName == "OptimumValueEnd"
             || propertyName == "MinimumOperator"
             || propertyName == "MaximumOperator"
             || propertyName == "Unit"
             || propertyName == "Deleted"
             || propertyName == "DeletedBy"
             || propertyName == "DeletedOn"
            )
            {

                ValidationContext context = new ValidationContext(this, null, null) { MemberName = propertyName };
                var validationResults = new Collection<ValidationResult>();
                if (propertyName == "NumericIssueID")
                    return Validator.TryValidateProperty(this.NumericIssueID, context, validationResults);
                if (propertyName == "IssueID")
                    return Validator.TryValidateProperty(this.IssueID, context, validationResults);
                if (propertyName == "MinimumValue")
                    return Validator.TryValidateProperty(this.MinimumValue, context, validationResults);
                if (propertyName == "MaximumValue")
                    return Validator.TryValidateProperty(this.MaximumValue, context, validationResults);
                if (propertyName == "OptimumValueStart")
                    return Validator.TryValidateProperty(this.OptimumValueStart, context, validationResults);
                if (propertyName == "OptimumValueEnd")
                    return Validator.TryValidateProperty(this.OptimumValueEnd, context, validationResults);
                if (propertyName == "MinimumOperator")
                    return Validator.TryValidateProperty(this.MinimumOperator, context, validationResults);
                if (propertyName == "MaximumOperator")
                    return Validator.TryValidateProperty(this.MaximumOperator, context, validationResults);
                if (propertyName == "Unit")
                    return Validator.TryValidateProperty(this.Unit, context, validationResults);
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
        /// <returns>return new Instance of NumericIssue</returns>
        public NumericIssue Clone()
        {
            NumericIssue mNumericIssue = new NumericIssue
            {
                NumericIssueID = this.NumericIssueID,
                IssueID = this.IssueID,
                MinimumValue = this.MinimumValue,
                MaximumValue = this.MaximumValue,
                OptimumValueStart = this.OptimumValueStart,
                OptimumValueEnd = this.OptimumValueEnd,
                MinimumOperator = this.MinimumOperator,
                MaximumOperator = this.MaximumOperator,
                Unit = this.Unit,
                Deleted = this.Deleted,
                DeletedBy = this.DeletedBy,
                DeletedOn = this.DeletedOn 
            };

            return mNumericIssue;
        }

        #endregion Methods
    }

}
