using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace citPOINT.PrefApp.Data.Web
{
    /// <summary>
    /// Class represent Negotiation entity loaded from eNeg
    /// </summary>
    [Serializable()]
    [DataContractAttribute(IsReference = true)]
    public sealed partial class Negotiation : EntityObject
    {
        #region → Properties     .

        /// <summary>
        /// Gets or sets the negotiation ID.
        /// </summary>
        /// <value>The negotiation ID.</value>
        [DataMemberAttribute()]
        [Key]
        public Guid NegotiationID { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is closed.
        /// </summary>
        /// <value><c>true</c> if this instance is closed; otherwise, <c>false</c>.</value>
        [DataMemberAttribute()]
        public bool IsClosed { get; set; }

        /// <summary>
        /// Gets or sets the status ID.
        /// </summary>
        /// <value>The status ID.</value>
        [DataMemberAttribute()]
        public Guid? StatusID { get; set; }

        /// <summary>
        /// Gets or sets the name of the negotiation.
        /// </summary>
        /// <value>The name of the negotiation.</value>
        [DataMemberAttribute()]
        public string NegotiationName { get; set; }

        /// <summary>
        /// Gets or sets the negotiators.
        /// </summary>
        /// <value>The negotiators.</value>
        [DataMemberAttribute()]
        public string Negotiators { get; set; }



        #endregion
    }
}
