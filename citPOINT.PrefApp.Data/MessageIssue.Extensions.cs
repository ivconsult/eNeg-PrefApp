
#region → Usings   .
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
#endregion

#region → History  .

/* Date         User            Change
 * 
 *15.03.11     M.Wahab     creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion

namespace  citPOINT.PrefApp.Data.Web
{


    /// <summary>
    /// MessageIssue class client-side extensions
    /// </summary>
    public partial class MessageIssue
    {

        #region → Fields         .

       
                
        #endregion

       

        #region → Methods        .

        /// <summary>
        /// Try validate for the MessageIssue class
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
            if (propertyName == "MessageIssueID"
             || propertyName == "ConversationMessageID"
             || propertyName == "IssueID"
             || propertyName == "Score"
             || propertyName == "Value"
             || propertyName == "Deleted"
             || propertyName == "DeletedBy"
             || propertyName == "DeletedOn"
            )
            {

                ValidationContext context = new ValidationContext(this, null, null) { MemberName = propertyName };
                var validationResults = new Collection<ValidationResult>();
                if (propertyName == "MessageIssueID")
                    return Validator.TryValidateProperty(this.MessageIssueID, context, validationResults);
                if (propertyName == "ConversationMessageID")
                    return Validator.TryValidateProperty(this.ConversationMessageID, context, validationResults);
                if (propertyName == "IssueID")
                    return Validator.TryValidateProperty(this.IssueID, context, validationResults);
                if (propertyName == "Score")
                    return Validator.TryValidateProperty(this.Score, context, validationResults);
                if (propertyName == "Value")
                    return Validator.TryValidateProperty(this.Value, context, validationResults);
                if (propertyName == "Deleted")
                    return Validator.TryValidateProperty(this.Deleted, context, validationResults);
                if (propertyName == "DeletedBy")
                    return Validator.TryValidateProperty(this.DeletedBy, context, validationResults);
                if (propertyName == "DeletedOn")
                    return Validator.TryValidateProperty(this.DeletedOn, context, validationResults);
            }
            return false;
        }


        #endregion Methods

    }

}
