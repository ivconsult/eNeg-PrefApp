
#region → Usings   .
using System;
using System.ComponentModel;
using System.ServiceModel.DomainServices.Client;
using citPOINT.PrefApp.Common;
using citPOINT.PrefApp.Data.Web;
using System.Windows;
using System.Collections.ObjectModel;
using System.Linq;
using citPOINT.eNeg.Common;
using System.Collections.Generic;
#endregion

#region → History  .

/* Date         User              Change
 * 
 * 15.07.11     Yousra Reda       Creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/
# endregion

namespace citPOINT.PrefApp.MVVM.UnitTest
{
    /// <summary>
    /// Mock Login View Model
    /// </summary>
    //[Export(typeof(ILoginModel))]
    public class MockLoginModel : ILoginModel
    {

        #region → Fields         .
        LoginContext mLoginContext;
        LoginUser NewUser = new LoginUser()
           {
               UserID = Guid.NewGuid(),
               EmailAddress = "yousra.reda@gmail.com",
               Password = "123456",
               Locked = false,
               IPAddress = "10.0.02.2",
               LastLoginDate = DateTime.Now,
               Online = false,
               Disabled = false,
           };
        #endregion Fields

        #region → Properties     .

        /// <summary>
        /// property with a getter only to can use our custom login Service which Call RIA Service
        /// </summary>
        /// <value>The login context.</value>
        public LoginContext LoginContext
        {
            get
            {
                if (mLoginContext == null)
                {
                    mLoginContext = new LoginContext(new Uri("http://localhost:9002/Services/citPOINT-PrefApp-Data-Web-LoginService.svc", UriKind.Absolute));
                }
                return mLoginContext;
            }
        }

        /// <summary>
        /// True if mLoginContext.HasChanges is true; otherwise, false
        /// </summary>
        /// <value>the has Changes</value>
        public bool HasChanges
        {
            get { return false; }
        }

        /// <summary>
        /// True if either "IsLoading" or "IsSubmitting" is
        /// in progress; otherwise, false
        /// </summary>
        /// <value>the Is Busy</value>
        public bool IsBusy
        {
            get { return false; }
        }

        #endregion Properties

        #region → Events         .

        /// <summary>
        /// Event for callback of LoginAsync Function
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<LoginUser>> LoadUserCompleted;

        /// <summary>
        /// Event for callback of LogoutAsync Function
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<LoginUser>> LogoutCompleted;

        /// <summary>
        /// Event for callback of BaseAddress Function
        /// </summary>
        public event Action<InvokeOperation<string>> GeteNegBaseAddressCompleted;

        /// <summary>
        /// Occurs when [make user online completed].
        /// </summary>
        public event Action<InvokeOperation<bool>> MakeUserOnlineCompleted;

        /// <summary>
        /// Occurs when [make user offline completed].
        /// </summary>
        public event Action<InvokeOperation<bool>> MakeUserOfflineCompleted;

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region → Methods        .

        #region → Public         .

        /// <summary>
        /// Try to load user first by checking cookies and then by checking session
        /// if both not found return fail login indication.
        /// </summary>
        public void LoadUserAsync()
        {
            if (LoadUserCompleted != null)
            {
                LoadUserCompleted(this, new eNegEntityResultArgs<LoginUser>(
                    new List<LoginUser>()
                            {
                                NewUser
                            }
                    ));
            }
        }

        /// <summary>
        /// Function that execute MakeUserOnline service which wrap inside call eNeg RIA Service
        /// </summary>
        /// <param name="UserID">Value of UserID of login User</param>
        /// <param name="IPAddress">Value of IPAddress of login User</param>
        /// <returns>bool that indicates whether operaton complete or not</returns>
        public bool MakeUserOnline(Guid? UserID, string IPAddress)
        {
            //if (MakeUserOnlineCompleted != null)
            //{
                //MakeUserOnlineCompleted(
                    LoginContext.MakeUserOnline(UserID, IPAddress);//);
            //}
            return true;
        }

        public void MakeUserOnline_Completed(InvokeOperation<bool> Obj)
        {

        }

        /// <summary>
        /// Function that execute MakeUserOffline service which wrap inside call eNeg RIA Service
        /// </summary>
        /// <param name="UserID">Value of UserID of login User</param>
        /// <returns>bool that indicates whether operaton complete or not</returns>
        public bool MakeUserOffline(Guid? UserID)
        {
            if (MakeUserOfflineCompleted != null)
            {
                MakeUserOfflineCompleted(LoginContext.MakeUserOffline(UserID));
            }
            return true;
        }

        /// <summary>
        /// Make user Logged out and update his data in DB with that new state
        /// </summary>
        /// <param name="UserID">Value of UserID of login User</param>
        public void LogoutAsync(Guid? UserID)
        {
            if (LogoutCompleted != null)
            {
                LogoutCompleted(this, new eNegEntityResultArgs<LoginUser>(
                        new List<LoginUser>()
                            {
                                new LoginUser()
                                {
                                    EmailAddress = string.Empty
                                }
                            }
                        ));
            }
        }

        /// <summary>
        /// Get End Point Used to cal eNeg RIA Service and then split to get Host Base Address
        /// </summary>
        public void GeteNegBaseAddressAsync()
        {
            if (GeteNegBaseAddressCompleted != null)
            {
                GeteNegBaseAddressCompleted(LoginContext.GeteNegBaseAddress());
            }
        }
        #endregion Public

        #endregion Methods
    }
}
