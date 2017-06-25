
#region → Usings   .
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Silverlight.Testing;
using citPOINT.PrefApp.ViewModel;
using System.ComponentModel.Composition;
using citPOINT.PrefApp.Common;
using citPOINT.PrefApp.Data.Web;
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

namespace citPOINT.PrefApp.MVVM.Test
{

    /// <summary>
    ///This is a test class for PrefApp LoginModel and ViewModel
    ///</summary>
    [TestClass]
    public class MainPageVMTest : SilverlightTest
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
        [Import(PrefAppViewModelTypes.MainPageViewModel)]
        public MainPageViewModel MainPageViewModel
        {
            get { return mMainPageViewModel; }
            set
            {
                mMainPageViewModel = value;
                //PrefAppConfigurations.eNegHostBaseAddress = "http://localhost:9002/";
                value.SessionContext = new SessionContext(new Uri("http://localhost:9002/citPOINT-PrefApp-Data-Web-SessionService.svc", UriKind.Absolute));

            }
        }

        #endregion

        #region → Methods        .

        #region → Public         .
                
        /// <summary>
        /// Try Importing Using MEF 
        /// </summary>
        [TestInitialize]
        public void BuidUp()
        {
            CompositionInitializer.SatisfyImports(this);
        }

        /// <summary>
        /// Tests the basics.
        /// </summary>
        [TestMethod]
        public void TestSuccessOfMEF()
        {
            Assert.IsNotNull(mMainPageViewModel, "Failed to retrieve the viewmodel via MEF");
            PrefAppMessanger.RaiseErrorMessage.Register(this, OnRaiseErrorMessage);
        }

        /// <summary>
        /// Called when [raise error message].
        /// </summary>
        /// <param name="ex">The ex.</param>
        public void OnRaiseErrorMessage(Exception ex)
        {
            ErrorMessage = ex.Message;
        }

 

       
        /// <summary>
        /// Logouts this instance.
        /// </summary>
        [Asynchronous]
        [TestMethod]
        public void Logout()
        {
            EnqueueCallback(() => MainPageViewModel.LogoutAsync());
            EnqueueCallback(() => Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage)));
            EnqueueCallback(() => Assert.IsTrue(MainPageViewModel.IsLoggedOut, "User Cannot Logout"));
            EnqueueTestComplete();
        }

        /// <summary>
        /// Getes the neg base address.
        /// </summary>
        [Asynchronous]
        [TestMethod]
        public void GeteNegBaseAddress()
        {
            EnqueueCallback(() => MainPageViewModel.GeteNegBaseAddressAsync());
            EnqueueCallback(() => Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage)));
            EnqueueCallback(() => Assert.IsTrue(PrefAppConfigurations.eNegHostBaseAddress != "", "Cannot get eNeg Host Base Address"));
            EnqueueTestComplete();
        }
        #endregion Public

        #endregion Methods
    }
}