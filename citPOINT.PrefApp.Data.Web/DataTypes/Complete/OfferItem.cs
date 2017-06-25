
#region → Usings   .

using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

#endregion

#region → History  .

/* Date         User          Change
 * 
 * 27.02.12     M.Wahab       • Creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion


namespace citPOINT.PrefApp.Data.Web.Complete
{
    /// <summary>
    /// Class for Offer Item
    /// </summary>
        [DataContract]
    public class OfferItem
    {
        #region → Properties     .

        /// <summary>
        /// Gets or sets the issue ID.
        /// </summary>
        /// <value>The issue ID.</value>
        [DataMember]
        [Key]
        public Guid IssueID { get; set; }

        /// <summary>
        /// Gets or sets the option ID.
        /// </summary>
        /// <value>The option ID.</value>
        [DataMember]
        public Guid? OptionID { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        [DataMember]
        public string Value { get; set; }


        /// <summary>
        /// Gets or sets the percentage.
        /// </summary>
        /// <value>The percentage.</value>
        [DataMember]
        public decimal Percentage { get; set; }

        /// <summary>
        /// Gets or sets the type of the issue.
        /// </summary>
        /// <value>The type of the issue.</value>
        internal Guid IssueType { get; set; }

        #endregion

        #region → Constractor    .


        /// <summary>
        /// Initializes a new instance of the <see cref="OfferItem"/> class.
        /// </summary>
        public OfferItem()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OfferItem"/> class.
        /// </summary>
        /// <param name="offerItem_Result">The offer item_ result.</param>
        public OfferItem(OfferItem_Result offerItem_Result)
            : this()
        {
            this.IssueID = offerItem_Result.IssueID;
            this.OptionID = offerItem_Result.OptionID;
            this.Percentage = !offerItem_Result.Score.HasValue ? 0 : offerItem_Result.Score.Value;
            this.Value = offerItem_Result.Value;
            this.IssueType = offerItem_Result.IssueTypeID;
        }

        #endregion



    }
}
