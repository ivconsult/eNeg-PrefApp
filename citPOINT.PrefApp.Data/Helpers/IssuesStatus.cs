
#region → Usings   .
using System;

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
    /// Issues Status Help Class
    /// </summary>
    public class IssuesStatus
    {
        #region → Properties     .

        /// <summary>
        /// Gets or sets the negotiation ID.
        /// </summary>
        /// <value>The negotiation ID.</value>
        public Guid NegConversationID { get; set; }
        /// <summary>
        /// Gets or sets the status_ value.
        /// </summary>
        /// <value>The status_ value.</value>
        public string Status_Value { get; set; }
        /// <summary>
        /// Gets or sets the status_ percentage.
        /// </summary>
        /// <value>The status_ percentage.</value>
        public decimal Status_Percentage { get; set; }

        #endregion

        #region → Methods        .

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{0} ({1}%)", Status_Value, this.Status_Percentage.ToString().Replace(".00", "").Replace(".0", ""));
        }

        #endregion

    }
}