
#region → Usings   .
using System;
using System.Linq;
using System.Windows;
using citPOINT.PrefApp.Common;
using citPOINT.PrefApp.ViewModel;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using citPOINT.PrefApp.Data.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
#endregion

#region → History  .

/* Date         User            Change
 * 
 * 18.07.11     Yousra Reda         • creation
 * 
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
    ///This is a test class for PrefApp LoginModel and ViewModel
    ///</summary>
    [TestClass]
    public class MainPageViewModel_Test
    {
        #region → Fields         .

        private MainPageViewModel mMainPageViewModel;
        private string ErrorMessage;

        #endregion Fields

        #region → Properties     .

        /// <summary>
        /// Gets or sets the main page view model.
        /// </summary>
        /// <value>The main page view model.</value>
        public MainPageViewModel TheVM
        {
            get { return mMainPageViewModel; }
            set
            {
                mMainPageViewModel = value;
            }
        }

        #endregion

        #region → Constructor    .
        /// <summary>
        /// Test_s the intialization_ pass.
        /// </summary>
        [TestInitialize]
        public void Test_Intialization_Pass()
        {
            TheVM = new MainPageViewModel(new MockLoginModel());

            #region → Registeration for needed messages in eNegMessenger
            // register for RaiseErrorMessage
            PrefAppMessanger.RaiseErrorMessage.Register(this, OnRaiseErrorMessage);
            #endregion
        }
        #endregion

        #region → Methods        .

        #region → Private        .

        #region → Raise Error Message  .

        /// <summary>
        /// Raise error message if there is any layer send RaiseErrorMessage
        /// </summary>
        /// <param name="ex">exception to raise</param>
        private void OnRaiseErrorMessage(Exception ex)
        {
            if (ex != null)
            {
                if (ex.InnerException != null)
                {
                    ErrorMessage = ex.Message + "\r\n" + ex.InnerException.Message;
                }
                else
                {
                    ErrorMessage = ex.Message;
                }
            }
        }

        #endregion

        #endregion

        #region → Public         .

        /// <summary>
        /// Getes the neg base address_ completed.
        /// </summary>
        [TestMethod]
        public void GeteNegBaseAddress_Completed()
        {
            TheVM.GeteNegBaseAddressAsync();
            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(!string.IsNullOrEmpty(PrefAppConfigurations.eNegHostBaseAddress), "Couldn't get eNeg base address");
        }

        /// <summary>
        /// Logout_s the completed.
        /// </summary>
        [TestMethod]
        public void Logout_Completed()
        {
            TheVM.LogoutAsync();
            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(!TheVM.IsLoggedIn, "Couldn't logout from PrefApp");
        }

        /// <summary>
        /// Changes the screen command_ screen name parameter_ succeeded.
        /// </summary>
        [TestMethod]
        public void ChangeScreenCommand_ScreenNameParameter_Succeeded()
        {
            string OriginalScreenName = TheVM.CurrentScreenText;
            TheVM.ChangeScreenCommand.Execute(PrefAppViewTypes.ReportGraphView);
            string ModifiedScreenName = TheVM.CurrentScreenText;
            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(OriginalScreenName != ModifiedScreenName, "Change Screen Command execution Failed");
        }

        /// <summary>
        /// Changes the screen command_ same screen name parameter_ failed.
        /// </summary>
        [TestMethod]
        public void ChangeScreenCommand_SameScreenNameParameter_Failed()
        {
            string OriginalScreenName = TheVM.CurrentScreenText;
            TheVM.ChangeScreenCommand.Execute(OriginalScreenName);
            string ModifiedScreenName = TheVM.CurrentScreenText;
            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(OriginalScreenName == ModifiedScreenName || ModifiedScreenName == string.Empty, "Change Screen Command execution done successfully");
        }

        /// <summary>
        /// Makes the user online_ completed.
        /// </summary>
        [TestMethod]
        public void MakeUserOnline_Completed()
        {
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
            TheVM.MakeUserOnline(NewUser);
        }
        #endregion Public

        #endregion Methods
    }
}
