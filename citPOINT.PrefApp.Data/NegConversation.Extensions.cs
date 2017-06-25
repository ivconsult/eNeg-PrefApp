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
    /// NegConversation class client-side extensions
    /// </summary>
    public partial class NegConversation
    {
        #region → Fields         .
        private string mName;
        private bool mHasDataMacthingChanges = false;
                 
        private ConversationMessage mDMLastReceivedMessage = null;
        private Guid mDMLastReceivedMessageID;
        private decimal? mDMLastReceivedMessagePercentage;

        private ConversationMessage mDMLastSentMessage = null;
        private Guid mDMLastSentMessageID;
        private decimal? mDMLastSentMessagePercentage;

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
                return mName;
            }

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    mName = value;
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
                return "   (" + Percentage + "%)";
            }
        }

        #region → Data matching Tracker Properties .

        /// <summary>
        /// Gets or sets a value indicating whether this instance has data macthing changes.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has data macthing changes; otherwise, <c>false</c>.
        /// </value>
        public bool HasDataMacthingChanges
        {
            get { return mHasDataMacthingChanges; }
            set { mHasDataMacthingChanges = value; }
        }

        #region → Recieved Message  .

        /// <summary>
        /// Gets or sets the DM last received message.
        /// </summary>
        /// <value>The DM last received message.</value>
        public ConversationMessage DMLastReceivedMessage
        {
            get { return mDMLastReceivedMessage; }
            set
            {

                mDMLastReceivedMessage = value;

                if (mDMLastReceivedMessage != null)
                {
                    this.DMLastReceivedMessageID = mDMLastReceivedMessage.ConversationMessageID;
                    this.DMLastReceivedMessagePercentage = mDMLastReceivedMessage.Percentage;
                }
                else
                {
                    this.DMLastReceivedMessageID = Guid.Empty;
                    this.DMLastReceivedMessagePercentage = null;
                }

            }
        }

        /// <summary>
        /// Gets or sets the Data Matching last received message ID.
        /// </summary>
        /// <value>The Data Matching last received message ID.</value>
        private Guid DMLastReceivedMessageID
        {
            get { return mDMLastReceivedMessageID; }
            set
            {
                if (mDMLastReceivedMessageID != value)
                {
                    this.HasDataMacthingChanges = true;

                    mDMLastReceivedMessageID = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the Data Matching last received message percentage.
        /// </summary>
        /// <value>The  Data Matching last received message percentage.</value>
        private decimal? DMLastReceivedMessagePercentage
        {
            get { return mDMLastReceivedMessagePercentage; }
            set
            {
                if (mDMLastReceivedMessagePercentage != value)
                {
                    this.HasDataMacthingChanges = true;

                    mDMLastReceivedMessagePercentage = value;
                }
            }
        }

        #endregion

        #region → Sent     Message  .

        /// <summary>
        /// Gets or sets the DM last Sent message.
        /// </summary>
        /// <value>The DM last Sent message.</value>
        public ConversationMessage DMLastSentMessage
        {
            get { return mDMLastSentMessage; }
            set
            { 
                    mDMLastSentMessage = value;

                    if (mDMLastSentMessage != null)
                    {
                        this.DMLastSentMessageID = mDMLastSentMessage.ConversationMessageID;
                        this.DMLastSentMessagePercentage = mDMLastSentMessage.Percentage;
                    }
                    else
                    {
                        this.DMLastSentMessageID = Guid.Empty;
                        this.DMLastSentMessagePercentage = null;
                    }
                 
            }
        }

        /// <summary>
        /// Gets or sets the Data Matching last Sent message ID.
        /// </summary>
        /// <value>The Data Matching last Sent message ID.</value>
        private Guid DMLastSentMessageID
        {
            get { return mDMLastSentMessageID; }
            set
            {
                if (mDMLastSentMessageID != value)
                {
                    this.HasDataMacthingChanges = true;

                    mDMLastSentMessageID = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the Data Matching last Sent message percentage.
        /// </summary>
        /// <value>The  Data Matching last Sent message percentage.</value>
        private decimal? DMLastSentMessagePercentage
        {
            get { return mDMLastSentMessagePercentage; }
            set
            {
                if (mDMLastSentMessagePercentage != value)
                {
                    this.HasDataMacthingChanges = true;

                    mDMLastSentMessagePercentage = value;
                }
            }
        }

        #endregion

        #endregion

        #endregion

        #region → Event Handlers .


        /// <summary>
        /// Called when an <see cref="T:System.ServiceModel.DomainServices.Client.Entity"/> property has changed.
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnPropertyChanged(System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.PropertyName == "Percentage")
            {
                this.RaisePropertyChanged("PCT");
            }

        }

        #endregion

        #region → Methods        .

        /// <summary>
        /// Try validate for the NegConversation class
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
            if (propertyName == "ConversationID"
             || propertyName == "Percentage"
             || propertyName == "PreferenceSetNegID"
            )
            {

                ValidationContext context = new ValidationContext(this, null, null) { MemberName = propertyName };
                var validationResults = new Collection<ValidationResult>();
                if (propertyName == "ConversationID")
                    return Validator.TryValidateProperty(this.ConversationID, context, validationResults);
                if (propertyName == "Percentage")
                    return Validator.TryValidateProperty(this.Percentage, context, validationResults);
                if (propertyName == "PreferenceSetNegID")
                    return Validator.TryValidateProperty(this.PreferenceSetNegID, context, validationResults);
            }
            return false;
        }


        #endregion Methods




    }

}
