#region → Usings   .
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.ServiceModel.DomainServices.Client;
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
    ///<summary>
    /// This is a test class for PrefApp LoginService (Data Access Layer)
    ///</summary>
    [TestClass]
    public class LoginServiceTest : ILoginServiceTest
    {

        #region → Fields         .
        private TestContext testContextInstance;
        private LoginContext mLoginContext;
        private LoginUser mUser;
        #endregion Fields


        #region → Properties     .

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        /// <summary>
        /// instance of LoginContext which wrap inside loginService of eNeg to can use available services
        /// </summary>
        public LoginContext LoginContext
        {
            get
            {
                if (mLoginContext == null)
                {
                    mLoginContext = new LoginContext(new Uri("http://localhost:9002/citPOINT-PrefApp-Data-Web-LoginService.svc", UriKind.Absolute));
                }
                return mLoginContext;
            }

        }
        #endregion Properties      
  

        #region → Constructor    .
        /// <summary>
        /// Default Constructor
        /// </summary>
        public LoginServiceTest()
        {
            UserObj();
        }
        #endregion Constructor


        #region → Methods        .

        #region Generate Mock

        /// <summary>
        /// Initialize mUser with new login user
        /// </summary>
        /// <returns>New Login User</returns>
        public LoginUser UserObj()
        {
            if (mUser == null)
            {
                mUser = new LoginUser()
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
            }
            return mUser;
        }
        #endregion Generate Mock 
                
        /// <summary>
        /// Test Logout operation used in LoginService
        /// </summary>
        [TestMethod]
        public void TestLogoutIneNeg()
        {
            try
            {
                this.LoginContext.Load<LoginUser>
                    (LoginContext.LogoutQuery(mUser.UserID), LoadBehavior.RefreshCurrent, s =>
                    {
                        if (s.HasError)
                        {
                            Assert.Fail("Fail to logout\r\n" + s.Error.Message);
                        }

                    }, true);
            }
            catch (Exception ex)
            {
                Assert.Fail("Fail to logout\r\n", ex.Message);
            }
        }

        /// <summary>
        /// Tests the make user online after successful login.
        /// </summary>
        [TestMethod]
        public void TestMakeUserOnlineAfterSuccessfulLogin()
        {
            try
            {
                if (this.LoginContext.MakeUserOnline(mUser.UserID, mUser.IPAddress).HasError)
                {
                    Assert.Fail("Fail to make user online\r\n", this.LoginContext.MakeUserOnline(mUser.UserID, mUser.IPAddress).Error);
                }
            }
            catch (Exception ex)
            {
                Assert.Fail("Fail to make user online\r\n", ex.Message);
            }
        }

        /// <summary>
        /// Tests the make user offline after successful logout.
        /// </summary>
        [TestMethod]
        public void TestMakeUserOfflineAfterSuccessfulLogout()
        {
            try
            {
                if (this.LoginContext.MakeUserOffline(mUser.UserID).HasError)
                {
                    Assert.Fail("Fail to make user offline\r\n", this.LoginContext.MakeUserOffline(mUser.UserID).Error);
                }
            }
            catch (Exception ex)
            {
                Assert.Fail("Fail to make user offline\r\n", ex.Message);
            }
        }

        /// <summary>
        /// Tests the get eNeg base address to use it in different navigation Process.
        /// </summary>
        [TestMethod]
        public void TestGeteNegBaseAddress()
        {
            try
            {
                if (this.LoginContext.GeteNegBaseAddress().HasError)
                {
                    Assert.Fail("Fail to get eNeg Base Address\r\n", this.LoginContext.GeteNegBaseAddress().Error);
                }
            }
            catch (Exception ex)
            {
                Assert.Fail("Fail to get eNeg Base Address\r\n", ex.Message);
            }
        }


        #endregion Methods
    }
}