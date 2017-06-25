
#region → Usings   .

using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

#endregion

#region → History  .

/* Date         User          Change
 * 
 * 26.02.12     M.Wahab       • Creation
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
    /// Issue Type Enum.
    /// </summary>
       [DataContract]
    public enum CompleteIssueType
    {
        /// <summary>
        /// Numeric.
        /// </summary>
        [EnumMember()]
        Numeric = 0,

        /// <summary>
        /// Option.
        /// </summary>
        [EnumMember()]
        Option = 1,

        /// <summary>
        /// Later Rated
        /// </summary>
        [EnumMember()]
        LaterRated=2,

        /// <summary>
        /// Not Rated.
        /// </summary>
        [EnumMember()]
        NotRated=3
    }
}
