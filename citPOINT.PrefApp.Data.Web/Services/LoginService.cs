
#region → Usings   .
using System;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.DomainServices.Hosting;
using System.ServiceModel.DomainServices.Server;
using citPOINT.PrefApp.Data.Web.ServiceReference1;
using System.ServiceModel.Channels;
using System.Configuration;
#endregion

#region → History  .

/* Date         User              Change
 * 
 * 07.09.10     Yousra Reda       Creation
 * 07.09.10     Yousra Reda       Validate User using eNeg RIA Service 
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
    /// Login service that wrap eNeg LoginService
    /// </summary>
    [EnableClientAccess()]
    [ServiceContract]
    public class LoginService : DomainService
    {

        #region → Fields         .
        eNegServiceSoapClient mLoader;

        private static LoginUser DefaultUser = new LoginUser()
        {
            EmailAddress = String.Empty
        };
        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets the loader.
        /// </summary>
        /// <value>The loader.</value>
        public eNegServiceSoapClient Loader
        {
            get
            {
                if (mLoader == null)
                {
                    mLoader = new eNegServiceSoapClient();
                    InjectCredentials();
                }
                return mLoader;
            }
        }

        #endregion Properties

        #region → Methods        .

        #region → Public         .

        /// <summary>
        /// Gets the user frome neg.
        /// </summary>
        /// <param name="UserID">The user ID.</param>
        /// <returns>
        /// the found user in eNeg with this user ID
        /// </returns>
        [OperationContract]
        public LoginUser GetUserFromeNeg(Guid UserID)
        {
            var found = Loader.GetUserByID(UserID).RootResults.FirstOrDefault();
            
            if (found == null)
            {
                return LoginService.DefaultUser;
            }
            else
            {
                LoginUser CurrentUser = new LoginUser();
                if (found.EmailAddress == string.Empty || found.Locked == true || found.Disabled == true)
                {
                    return LoginService.DefaultUser;
                }
                CurrentUser.UserID = found.UserID;
                CurrentUser.EmailAddress = found.EmailAddress;
                CurrentUser.FullName = found.FirstName + " " + found.LastName;
                return CurrentUser;
            }
        }

        /// <summary>
        /// Update some fields in current user in eNeg DB to indicate that he is online 
        /// </summary>
        /// <param name="UserID">The user ID.</param>
        /// <param name="IPAddress">The IP address.</param>
        /// <returns>bool that indicates the success or failer of the operation</returns>
        [OperationContract]
        public bool MakeUserOnline(Guid? UserID, string IPAddress)
        {
            var found = Loader.MakeUserOnline(UserID, IPAddress).RootResults.FirstOrDefault();
            if (found != null)
                return true;

            return false;
        }

        /// <summary>
        /// Update some fields in current user in eNeg DB to indicate that he is offline 
        /// </summary>
        /// <param name="UserID">The user ID.</param>
        /// <returns>bool that indicates the success or failer of the operation</returns>
        [OperationContract]
        public bool MakeUserOffline(Guid? UserID)
        {
            var found = Loader.MakeUserOffline(UserID).RootResults.FirstOrDefault();
            if (found != null)
                return true;
            else
                return false;
        }
        
        /// <summary>
        /// Logouts the specified user ID.
        /// </summary>
        /// <param name="UserID">The user ID.</param>
        /// <returns>the default user to indicate that no current user is logged in</returns>
        [OperationContract]
        public LoginUser Logout(Guid? UserID)
        {
            MakeUserOffline(UserID);
            return LoginService.DefaultUser;
        }

        /// <summary>
        /// Get End Point Used to cal eNeg RIA Service and then split to get Host Base Address
        /// </summary>
        /// <returns>Host base address of eNeg to can navigate "Register" and "Forget Login" Links</returns>
        [OperationContract]
        public string GeteNegBaseAddress()
        {
            string SoapEndpoint = ((ChannelFactory)(((ClientBase<eNegServiceSoap>)(Loader)).ChannelFactory)).Endpoint.Address.ToString();
            int index = SoapEndpoint.IndexOf("/Services/", StringComparison.CurrentCultureIgnoreCase);
            return SoapEndpoint.Substring(0, index) + "/";
        }
        #endregion Public

        #region → Private        .

        /// <summary>
        /// Injects the credentials into message header.
        /// </summary>
        private void InjectCredentials()
        {
            OperationContextScope scope = new OperationContextScope((IContextChannel)Loader.InnerChannel);

            MessageHeaders messageHeadersElement = OperationContext.Current.OutgoingMessageHeaders;
            messageHeadersElement.Add(MessageHeader.CreateHeader("username", "http://tempori.org", ConfigurationManager.AppSettings["username"]));
            messageHeadersElement.Add(MessageHeader.CreateHeader("password", "http://tempori.org", ConfigurationManager.AppSettings["password"]));
        }
        #endregion

        #endregion Methods
    }
}


