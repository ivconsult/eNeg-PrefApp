#region → Usings   .
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
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
    /// ConversationMessage class client-side extensions
    /// </summary>
    public partial class ConversationMessage
    {
        /// <summary>
        /// Gets or sets the words format source.
        /// </summary>
        /// <value>The words format source.</value>
        public List<FormatedWord> WordsFormatSource
        {
            get
            {
                return mWordsFormatSource;
            }
            set
            {
                mWordsFormatSource = value;
                this.RaisePropertyChanged("WordsFormatSource");
            }
        }

        /// <summary>
        /// Gets or sets the words format static source.
        /// </summary>
        /// <value>The words format static source.</value>
        public static List<FormatedWord> WordsFormatStaticSource
        {
            get
            {
                return mWordsFormatSource;
            }
            set
            {
                mWordsFormatSource = value;
            }
        }

        #region → Fields         .


        private string mMessageContent;
        private string mMessageSubject;

        private string mMessageSender;
        private string mMessageReceiver;
        private DateTime mMessageDate;
        private string mChannelName;

        private static List<FormatedWord> mWordsFormatSource;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets the formated percentage.
        /// </summary>
        /// <value>The formated percentage.</value>
        private string FormatedPercentage
        {
            get
            {
                return String.Format("{0:0.00}", this.Percentage.Value) + "%";

            }
        }

        /// <summary>
        /// Gets the percentage string.
        /// </summary>
        /// <value>The percentage string.</value>
        public string PercentageString
        {
            get
            {
                return (!this.Percentage.HasValue ? " (unrated) " : FormatedPercentage);
            }
        }

        /// <summary>
        /// Gets or sets the content of the message.
        /// </summary>
        /// <value>The content of the message.</value>
        public string MessageContent
        {
            get
            {
                return mMessageContent;
            }

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    mMessageContent = value;
                    this.RaisePropertyChanged("MessageSubject");
                }
            }
        }

        /// <summary>
        /// Gets or sets the message subject.
        /// </summary>
        /// <value>The message subject.</value>
        public string MessageSubject
        {
            get
            {
                return mMessageSubject;
            }

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    mMessageSubject = value;
                    this.RaisePropertyChanged("MessageSubject");
                }
            }
        }

        /// <summary>
        /// Gets or sets the message sender.
        /// </summary>
        /// <value>The message sender.</value>
        public string MessageSender
        {
            get
            {
                return mMessageSender;
            }

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    mMessageSender = value;
                    this.RaisePropertyChanged("MessageSenderWithoutBrackets");
                }
            }
        }

        /// <summary>
        /// Gets or sets the message receiver.
        /// </summary>
        /// <value>The message receiver.</value>
        public string MessageReceiver
        {
            get
            {
                return mMessageReceiver;
            }

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    mMessageReceiver = value;
                    this.RaisePropertyChanged("MessageReceiverWithoutBrackets");
                }
            }
        }

        /// <summary>
        /// Gets the message sender without brackets.
        /// </summary>
        /// <value>The message sender without brackets.</value>
        public string MessageSenderWithoutBrackets
        {
            get
            {
                if (!string.IsNullOrEmpty(mMessageSender))
                {
                    return mMessageSender.Replace("<", "").Replace(">", "");
                }
                return mMessageSender;
            }
        }

        /// <summary>
        /// Gets the message receiver without brackets.
        /// </summary>
        /// <value>The message receiver without brackets.</value>
        public string MessageReceiverWithoutBrackets
        {
            get
            {
                if (!string.IsNullOrEmpty(mMessageReceiver))
                {
                    return mMessageReceiver.Replace("<", "").Replace(">", "");
                }
                return mMessageReceiver;
            }
        }

        /// <summary>
        /// Gets or sets the message date.
        /// </summary>
        /// <value>The message date.</value>
        public DateTime MessageDate
        {
            get
            {
                return mMessageDate;
            }
            set
            {
                if (value != null && value != new DateTime()) //Mean {01/01/01  00:00}
                {
                    mMessageDate = value;
                    this.RaisePropertyChanged("MessageDate");
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is closed.
        /// </summary>
        /// <value><c>true</c> if this instance is closed; otherwise, <c>false</c>.</value>
        public bool IsClosed
        {
            get
            {
                if (this.NegConversation != null && this.NegConversation.PreferenceSetNeg != null)
                {
                    return this.NegConversation.PreferenceSetNeg.IsClosed;
                }
                return true;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is ongiong.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is ongiong; otherwise, <c>false</c>.
        /// </value>
        public bool IsOngiong
        {
            get
            {
                return !IsClosed;
            }
        }

        /// <summary>
        /// Gets or sets the name of the channel.
        /// </summary>
        /// <value>The name of the channel.</value>
        public string ChannelName
        {
            get { return mChannelName; }
            set
            {

                if (!string.IsNullOrEmpty(value))
                {
                    mChannelName = value;
                    this.RaisePropertyChanged("ChannelName");
                }
            }
        }

        #endregion

        #region → Event Handler  .

        /// <summary>
        /// Called when an <see cref="T:System.ServiceModel.DomainServices.Client.Entity"/> property has changed.
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnPropertyChanged(System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.PropertyName == "Percentage")
            {
                this.RaisePropertyChanged("PercentageString");
            }
        }
        #endregion

        #region → Methods        .

        /// <summary>
        /// Try validate for the ConversationMessage class
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
            if (propertyName == "MessageID"
             || propertyName == "Percentage"
             || propertyName == "NegConversationID"
            )
            {

                ValidationContext context = new ValidationContext(this, null, null) { MemberName = propertyName };
                var validationResults = new Collection<ValidationResult>();
                if (propertyName == "MessageID")
                    return Validator.TryValidateProperty(this.MessageID, context, validationResults);
                if (propertyName == "Percentage")
                    return Validator.TryValidateProperty(this.Percentage, context, validationResults);
                if (propertyName == "NegConversationID")
                    return Validator.TryValidateProperty(this.NegConversationID, context, validationResults);
            }
            return false;
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>return new Instance of ConversationMessage</returns>
        public ConversationMessage Clone()
        {
            ConversationMessage mConversationMessage = new ConversationMessage
            {
                ConversationMessageID = this.ConversationMessageID,
                MessageID = this.MessageID,
                Percentage = this.Percentage,
                NegConversationID = this.NegConversationID,
                IsSent = this.IsSent,
                Deleted = this.Deleted,
                DeletedBy = this.DeletedBy,
                DeletedOn = this.DeletedOn,
                RatedDate = this.RatedDate
            };

            return mConversationMessage;
        }

        /// <summary>
        /// Refereshes the words format source.
        /// </summary>
        public void RefereshWordsFormatSource()
        {
            this.RaisePropertyChanged("WordsFormatSource");
        }

        #endregion

    }

}
