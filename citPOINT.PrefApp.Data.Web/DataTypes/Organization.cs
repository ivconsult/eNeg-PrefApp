
#region → Usings   .
using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using System.ComponentModel;
#endregion

#region → History  .

/* Date         User              Change
 * 
 * 04.10.11     M.Wahab           Creation
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
    /// represent Organization
    /// </summary>
    [Serializable()]
    [DataContractAttribute(IsReference = true)]
    public partial class Organization : EntityObject
    {
        #region → Properties     .
        /// <summary>
        /// Gets or sets the organization ID.
        /// </summary>
        /// <value>The organization ID.</value>
        [DataMemberAttribute()]
        [Key]
        public Guid OrganizationID { get; set; }

        /// <summary>
        /// Gets or sets the name of the organization.
        /// </summary>
        /// <value>The name of the organization.</value>
        [DataMemberAttribute()]
        public string OrganizationName { get; set; }

        #endregion
    }

}
