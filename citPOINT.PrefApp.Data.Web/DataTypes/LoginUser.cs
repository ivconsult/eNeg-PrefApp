

#region → Usings   .
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Web;
using System.Data.Objects.DataClasses;

#endregion

#region → History  .

/* Date         User              Change
 * 
 * 07.09.10     Yousra Reda       Creation
 * 07.09.10     Yousra Reda       Put needed properties related to User
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
    /// LoginUser class derives from User class and implements IUser interface,
    /// it only exposes the following three data members to the client:
    /// </summary>
    [Serializable()]
    [DataContractAttribute(IsReference = true)]
    public sealed partial class LoginUser : EntityObject
    {

       #region → Properties     .

        /// <summary>
        /// Represent the UserID of current User
        /// </summary>
        [DataMemberAttribute()]
        [Key]
        public Guid UserID { get; set; }

        /// <summary>
        /// Gets or sets the full name.
        /// </summary>
        /// <value>The full name.</value>
        [DataMemberAttribute()]
        public string FullName { get; set; }

        /// <summary>
        /// Represent the EmailAddress of current User
        /// </summary>
        [DataMemberAttribute()]
        public string EmailAddress { get; set; }


        /// <summary>
        /// Represent the Password of current User
        /// </summary>
        [DataMemberAttribute()]
        public string Password { get; set; }


        /// <summary>
        /// Represent the IPAddress of current User
        /// </summary>
        [DataMemberAttribute()]
        public string IPAddress { get; set; }


        /// <summary>
        /// Represent the LastLoginDate of current User
        /// </summary>
        [DataMemberAttribute()]
        public Nullable<DateTime> LastLoginDate { get; set; }


        /// <summary>
        /// Represent the whether the current User is locked or not
        /// </summary>
        [DataMemberAttribute()]
        public bool Locked { get; set; }

        /// <summary>
        /// Represent the whether the current User is disabled or not
        /// </summary>
        [DataMemberAttribute()]
        public bool Disabled { get; set; }

        /// <summary>
        /// Represent the whether the current User is online or not
        /// </summary>
        [DataMemberAttribute()]
        public bool Online { get; set; }

        /// <summary>
        /// Represent the ClientAddress current User
        /// </summary>
        [DataMemberAttribute()]
        public string ClientAddress
        {
            get
            {
                try
                {
                    return HttpContext.Current.Request.UserHostAddress;
                }
                catch (Exception)
                {

                }
                return "127.0.0.1";

            }
        }
        #endregion

    }
}
