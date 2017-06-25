
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
    /// MainPreferenceSet class client-side extensions
    /// </summary>
    public partial class MainPreferenceSet
    {


        #region → Methods        .

        /// <summary>
        /// Try validate for the MainPreferenceSet class
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
            if (propertyName == "MainPreferenceSetID"
             || propertyName == "MainPreferenceSetName"
            )
            {

                ValidationContext context = new ValidationContext(this, null, null) { MemberName = propertyName };
                var validationResults = new Collection<ValidationResult>();
                if (propertyName == "MainPreferenceSetID")
                    return Validator.TryValidateProperty(this.MainPreferenceSetID, context, validationResults);
                if (propertyName == "MainPreferenceSetName")
                    return Validator.TryValidateProperty(this.MainPreferenceSetName, context, validationResults);
            }
            return false;
        }


        #endregion Methods

    }

}
