

#region → Usings   .
using System;
using citPOINT.PrefApp.Data.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endregion

#region → History  .

/* Date         User              Change
 * 
 * 15.09.10     Yousra Reda       Creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/
# endregion
namespace citPOINT.PrefApp.Data.Web.Test
{
    /// <summary>
    ///This is an Interface for PrefApp LoginService (Data Access Layer)
    ///</summary>
    interface ILoginServiceTest
    {
        
        #region → Properties     .

        /// <summary>
        /// instance of LoginContext which wrap inside loginService of eNeg to can use available services
        /// </summary>
        LoginContext LoginContext { get; }

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>  
        TestContext TestContext { get; set; }
        
        #endregion Properties


        #region → Methods        .

        /// <summary>
        /// Initialize mUser with new login user
        /// </summary>
        /// <returns>New Login User</returns>
        LoginUser UserObj();

        /// <summary>
        /// Test GetBase operation used in LoginService
        /// </summary>
        void TestGeteNegBaseAddress();

        /// <summary>
        /// Test Logout operation used in LoginService
        /// </summary>
        void TestLogoutIneNeg();

        /// <summary>
        /// Test MakeUserOffline operation used in LoginService
        /// </summary>       
        void TestMakeUserOfflineAfterSuccessfulLogout();

        /// <summary>
        /// Test MakeUserOnline operation used in LoginService
        /// </summary>
        void TestMakeUserOnlineAfterSuccessfulLogin();
        #endregion Methods
    }
}
