
#region → Usings   .
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Web;
using System.Data.Objects.DataClasses;
using System.ComponentModel;

#endregion

#region → History  .

/* Date         User              Change
 * 
 * 11.01.12     M.Whaab       Creation
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
    /// Class reprsents the result from Last Offer Details
    /// </summary>
    [Serializable()]
    [DataContractAttribute(IsReference = true)]
    public sealed class LastOfferDetails : EntityObject
    {

        #region → Properties     .

        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        /// <value>The ID.</value>
        [DataMemberAttribute()]
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the percentage.
        /// </summary>
        /// <value>The percentage.</value>
        [DataMemberAttribute()]
        public Decimal Percentage { get; set; }

        /// <summary>
        /// Gets or sets the offer date.
        /// </summary>
        /// <value>The offer date.</value>
        [DataMemberAttribute()]
        public DateTime OfferDate { get; set; }

        #endregion

    }
}
