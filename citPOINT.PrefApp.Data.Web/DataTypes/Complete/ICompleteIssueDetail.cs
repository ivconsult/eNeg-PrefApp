
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
    /// IComplete Issue Detail for Numeric , LaterRated and Options.
    /// </summary>
    [Serializable]
    [DataContractAttribute(IsReference = true)]
    public class CompleteIssueDetail
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        /// <value>The ID.</value>
        [DataMemberAttribute]
        [Key]
        public int ID { get; set; }


        /// <summary>
        /// Gets or sets the issue ID.
        /// </summary>
        /// <value>The issue ID.</value>
        [DataMemberAttribute()]
        public Guid IssueID { get; set; }
    }
}
