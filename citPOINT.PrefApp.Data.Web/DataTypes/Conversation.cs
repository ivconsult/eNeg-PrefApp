using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace citPOINT.PrefApp.Data.Web
{
    /// <summary>
    /// Class represent Conversation entity loaded from eNeg
    /// </summary>
    [Serializable()]
    [DataContractAttribute(IsReference = true)]
    public sealed partial class Conversation : EntityObject
    {
        #region → Properties     .

        /// <summary>
        /// Gets or sets the conversation ID.
        /// </summary>
        /// <value>The conversation ID.</value>
        [DataMemberAttribute()]
        [Key]
        public Guid ConversationID { get; set; }

        /// <summary>
        /// Gets or sets the name of the conversation.
        /// </summary>
        /// <value>The name of the conversation.</value>
        [DataMemberAttribute()]
        public string ConversationName { get; set; }


        [DataMemberAttribute()]
        public Guid NegotiationID { get; set; }

        #endregion
    }
}
