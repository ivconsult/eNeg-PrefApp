
#region → Usings   .
using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Collections;
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
    /// Issue class client-side extensions
    /// </summary>
    public partial class Issue
    {
        #region → Fields         .
        private string mStatus = "-";

        private List<IssuesStatus> mNegotiationsIssueStatus;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets the negotiations issue status.
        /// </summary>
        /// <value>The negotiations issue status.</value>
        public List<IssuesStatus> NegotiationsIssueStatus
        {
            get
            {
                if (mNegotiationsIssueStatus == null)
                    mNegotiationsIssueStatus = new List<IssuesStatus>();

                return mNegotiationsIssueStatus;
            }

        }

        /// <summary>
        /// Gets the current instance.
        /// Used in hirarchy of radpanel.
        /// </summary>
        /// <value>The current instance.</value>
        public IEnumerable<Issue> CurrentInstance
        {
            get
            {
                return new List<Issue> { this }.AsEnumerable<Issue>();
            }
        }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public string Status
        {
            get
            {
                return mStatus;
            }
            set
            {
                mStatus = value;
                RaisePropertyChanged("Status");
            }
        }

        #endregion

        #region → Methods        .

        /// <summary>
        /// Try validate for the Issue class
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
        /// Removes the status by neg conv ID.
        /// </summary>
        /// <param name="NegConversationID">The neg conversation ID.</param>
        public void RemoveStatusByNegConvID(Guid NegConversationID)
        {
            while (NegotiationsIssueStatus.Where(s => s.NegConversationID == NegConversationID).Count() > 0)
            {
                NegotiationsIssueStatus.Remove(NegotiationsIssueStatus.Where(s => s.NegConversationID == NegConversationID).First());

            }
        }

        /// <summary>
        /// Adds the status.
        /// </summary>
        /// <param name="NegConversationID">The neg conversation ID.</param>
        /// <param name="Value">The value.</param>
        /// <param name="Percentage">The percentage.</param>
        public void AddStatus(Guid NegConversationID, string Value, decimal Percentage)
        {
            NegotiationsIssueStatus.Add(new IssuesStatus()
                                               {
                                                   NegConversationID = NegConversationID,
                                                   Status_Percentage = Percentage,
                                                   Status_Value = Value
                                               });
        }

        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <param name="NegConversationID">The neg conversation ID.</param>
        /// <returns></returns>
        public string GetStatus(Guid NegConversationID)
        {
            IssuesStatus issuesStatus = NegotiationsIssueStatus.Where(s => s.NegConversationID == NegConversationID).FirstOrDefault();

            if (issuesStatus != null)
            {
                return issuesStatus.ToString();

            }

            return "";
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
            if (propertyName == "IssueID"
             || propertyName == "IssueName"
             || propertyName == "PreferenceSetID"
             || propertyName == "IssueTypelD"
             || propertyName == "IssueWeight"
             || propertyName == "Deleted"
             || propertyName == "DeletedBy"
             || propertyName == "DeletedOn"
            )
            {

                ValidationContext context = new ValidationContext(this, null, null) { MemberName = propertyName };
                var validationResults = new Collection<ValidationResult>();
                if (propertyName == "IssueID")
                    return Validator.TryValidateProperty(this.IssueID, context, validationResults);
                if (propertyName == "IssueName")
                    return Validator.TryValidateProperty(this.IssueName, context, validationResults);
                if (propertyName == "PreferenceSetID")
                    return Validator.TryValidateProperty(this.PreferenceSetID, context, validationResults);
                if (propertyName == "IssueTypelD")
                    return Validator.TryValidateProperty(this.IssueTypeID, context, validationResults);
                if (propertyName == "IssueWeight")
                    return Validator.TryValidateProperty(this.IssueWeight, context, validationResults);
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
        /// <returns>return new Instance of Issue</returns>
        public Issue Clone()
        {
            Issue mIssue = new Issue
            {
                IssueID = this.IssueID,
                IssueName = this.IssueName,
                PreferenceSetID = this.PreferenceSetID,
                IssueTypeID = this.IssueTypeID,
                IssueWeight = this.IssueWeight,
                Deleted = this.Deleted,
                DeletedBy = this.DeletedBy,
                DeletedOn = this.DeletedOn
            };

            return mIssue;
        }

        #endregion Methods

    }

}
