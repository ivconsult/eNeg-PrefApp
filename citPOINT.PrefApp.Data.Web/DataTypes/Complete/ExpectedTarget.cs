
#region → Usings   .
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Web;
using System.Data.Objects.DataClasses;
using System.ComponentModel;

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
    /// Class reprsents the target off offer at a certain Time.
    /// </summary>
    [DataContract]
    public sealed class ExpectedTarget  
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
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        [DataMemberAttribute()]
        public Status Status { get; set; }

        /// <summary>
        /// Gets or sets the target.
        /// </summary>
        /// <value>The target.</value>
        [DataMemberAttribute()]
        public Decimal Target { get; set; }

        #endregion

    }
}
