
#region → Usings   .
using System;
using System.Linq;
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
    /// PreferenceSetNeg class client-side extensions
    /// </summary>
    public partial class PreferenceSetNeg
    {

        #region → Fields         .
        
        private bool mIsClosed;
        private bool mIsChecked = false;
        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get
            {
                return this.NegotiationName;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.NegotiationName = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }

        /// <summary>
        /// Gets the PCT.
        /// </summary>
        /// <value>The PCT.</value>
        public string PCT
        {
            get
            {
                return " (" + Percentage + " of " + GetMaxPercentage + "%)";
            }
        }



        /// <summary>
        /// Gets or sets a value indicating whether this instance is checked.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is checked; otherwise, <c>false</c>.
        /// </value>
        public bool IsChecked
        {
            get { return mIsChecked; }
            set
            {
                if (mIsChecked != value)
                {
                    mIsChecked = value;
                    this.RaisePropertyChanged("IsChecked");
                }
            }
        }

        /// <summary>
        /// Gets or sets the max percentage.
        /// </summary>
        /// <value>The max percentage.</value>
        private decimal GetMaxPercentage
        {
            get
            {
                if (this.PreferenceSet!=null)
                {
                    return this.PreferenceSet.MaxPercentage;
                }
                return 0;
            }
        }

        #endregion

        #region → Event Handlers .

        /// <summary>
        /// Called when an <see cref="T:System.ServiceModel.DomainServices.Client.Entity"/> property has changed.
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnPropertyChanged(System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.PropertyName == "Percentage" || e.PropertyName == "MaxPercentage")
            {
                this.RaisePropertyChanged("PCT");
            }
        }

        #endregion

        #region → Methods        .

        /// <summary>
        /// Try validate for the PreferenceSetNeg class
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
            if (propertyName == "NegID"
             || propertyName == "Percentage"
             || propertyName == "PreferenceSetID"
             || propertyName == "Deleted"
             || propertyName == "DeletedBy"
             || propertyName == "DeletedOn"
            )
            {

                ValidationContext context = new ValidationContext(this, null, null) { MemberName = propertyName };
                var validationResults = new Collection<ValidationResult>();
                if (propertyName == "NegID")
                    return Validator.TryValidateProperty(this.NegID, context, validationResults);
                if (propertyName == "Percentage")
                    return Validator.TryValidateProperty(this.Percentage, context, validationResults);
                if (propertyName == "PreferenceSetID")
                    return Validator.TryValidateProperty(this.PreferenceSetID, context, validationResults);
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
