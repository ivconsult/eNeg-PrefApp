using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace citPOINT.PrefApp.Data.Web
{
    /// <summary>
    /// Class represent Message entity loaded from eNeg
    /// </summary>
    [Serializable()]
    [DataContractAttribute(IsReference = true)]
    public sealed partial class Message : EntityObject
    {

        #region → Properties     .

        /// <summary>
        /// Gets or sets the message ID.
        /// </summary>
        /// <value>The message ID.</value>
        [DataMemberAttribute()]
        [Key]
        public Guid MessageID { get; set; }

        /// <summary>
        /// Gets or sets the name of the message.
        /// </summary>
        /// <value>The name of the message.</value>
        [DataMemberAttribute()]
        public string MessageName { get; set; }

        /// <summary>
        /// Gets or sets the message subject.
        /// </summary>
        /// <value>The message subject.</value>
        [DataMemberAttribute()]
        public string MessageSubject { get; set; }

        /// <summary>
        /// Gets or sets the content of the message.
        /// </summary>
        /// <value>The content of the message.</value>
        [DataMemberAttribute()]
        public string MessageContent { get; set; }

        /// <summary>
        /// Gets or sets the message sender.
        /// </summary>
        /// <value>The message sender.</value>
        [DataMemberAttribute()]
        public string MessageSender { get; set; }

        /// <summary>
        /// Gets or sets the message receiver.
        /// </summary>
        /// <value>The message receiver.</value>
        [DataMemberAttribute()]
        public string MessageReceiver { get; set; }

        /// <summary>
        /// Gets or sets the message date.
        /// </summary>
        /// <value>The message date.</value>
        [DataMemberAttribute()]
        public DateTime? MessageDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is sent.
        /// </summary>
        /// <value><c>true</c> if this instance is sent; otherwise, <c>false</c>.</value>
        [DataMemberAttribute()]
        public bool IsSent { get; set; }

        /// <summary>
        /// Gets or sets the conversation ID.
        /// </summary>
        /// <value>The conversation ID.</value>
        [DataMemberAttribute()]
        public Guid ConversationID { get; set; }

        /// <summary>
        /// Gets or sets the name of the channel.
        /// </summary>
        /// <value>The name of the channel.</value>
        [DataMemberAttribute()]
        public string ChannelName { get; set; }

        #endregion
    }
}
