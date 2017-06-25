
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
    /// Class reprsents the Status of query.
    /// </summary>
     [DataContract]
    public enum Status
    {
        #region → Properties     .

        /// <summary>
        /// Success.
        /// </summary>
        [EnumMember()]
        Success = 0,

        /// <summary>
        /// Failed.
        /// </summary>
        [EnumMember()]
        Failed = 1,

        /// <summary>
        /// Date Out of Period.
        /// </summary>
        [EnumMember()]
        DateOutOfPeriod = 2,

        /// <summary>
        /// NO Settings.
        /// </summary>
        [EnumMember()]
        NoSettings = 3

        #endregion


    }
}
