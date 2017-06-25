
#region → Usings   .

using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

#endregion

#region → History  .

/* Date         User          Change
 * 
 * 23.02.12     M.Wahab       • Creation
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
    /// Class represents the offer type.
    /// </summary>
       [DataContract]
       [Serializable()]
    public enum OfferType
    {
        #region → Properties     .

        /// <summary>
        /// Your offers Sent Offers.
        /// </summary>
        [EnumMember()]
        Own = 1,

        /// <summary>
        /// The Other Person Offers.Received Messages.
        /// </summary>
        [EnumMember()]
        Counterpart = 2,

        /// <summary>
        /// From First And Last.
        /// </summary>
        [EnumMember()]
        Mixed = 3 
        
        #endregion


    }
}
