
#region → Usings   .
using System;
using System.Linq;
using System.Windows;
using citPOINT.PrefApp.Common;
using citPOINT.PrefApp.ViewModel;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using citPOINT.PrefApp.Data.Web;
using System.Linq;
#endregion

#region → History  .

/* Date         User            Change
 * 
 * 18.07.11     Yousra Reda         • creation & Testing all Methods used
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
    /// External Report ViewModel unit Test
    /// </summary>
    [TestClass]
    public class ExternalReportViewModel_Test
    {
        #region → Fields         .

        private ExternalReportViewModel mexternalReportViewModel;
        private string ErrorMessage;

        #endregion Fields

        #region → Properties     .

        /// <summary>
        /// Gets or sets the VM.
        /// </summary>
        /// <value>The VM.</value>
        public ExternalReportViewModel TheVM
        {
            get { return mexternalReportViewModel; }
            set
            {
                mexternalReportViewModel = value;
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
            TheVM = new ExternalReportViewModel(new MockExternalReportModel());

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
        /// Gets the preference set neg_ return collection.
        /// </summary>
        [TestMethod]
        public void GetPreferenceSetNeg_ReturnNegEntity()
        {
            TheVM.GetPreferenceSetNegAsync();
            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsNotNull(TheVM.CurrentNegotiation, "Couldn't get current negotiation");
        }

        /// <summary>
        /// Gets the last received message_ return message entity.
        /// </summary>
        [TestMethod]
        public void GetLastReceivedMessage_ReturnMessageEntity()
        {
            TheVM.GetLastReceivedMessageAsync();
            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsNotNull(TheVM.LastReceivedMessage, "Couldn't get last received message");
        }

        /// <summary>
        /// Gets the last sent message_ return message entity.
        /// </summary>
        [TestMethod]
        public void GetLastSentMessage_ReturnMessageEntity()
        {
            TheVM.GetLastSentMessageAsync();
            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsNotNull(TheVM.LastSentMessage, "Couldn't get last sent message");
        }

        /// <summary>
        /// Gets the issues by preference I d_ return issue collection.
        /// </summary>
        [TestMethod]
        public void GetIssuesByPreferenceID_ReturnIssueCollection()
        {
            TheVM.GetIssuesByPreferenceIDAsync(Guid.NewGuid());
            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(TheVM.IsGetIssuesDone, "Couldn't get issues of certain preference set");
        }

        /// <summary>
        /// Gets the option issues by preference I d_ return option issue collection.
        /// </summary>
        [TestMethod]
        public void GetOptionIssuesByPreferenceID_ReturnOptionIssueCollection()
        {
            TheVM.GetOptionIssuesByPreferenceIDAsync(Guid.NewGuid());
            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(TheVM.IsGetOptionIssuesDone, "Couldn't get option issues of certain preference set");
        }

        /// <summary>
        /// Gets the numeric issues by preference I d_ return numeric issue collection.
        /// </summary>
        [TestMethod]
        public void GetNumericIssuesByPreferenceID_ReturnNumericIssueCollection()
        {
            TheVM.GetNumericIssuesByPreferenceIDAsync(Guid.NewGuid());
            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(TheVM.IsGetNumericIssuesDone, "Couldn't get numeric issues of certain preference set");
        }

        /// <summary>
        /// Gets the later rated issues by preference I d_ return later rated issue collection.
        /// </summary>
        [TestMethod]
        public void GetLaterRatedIssuesByPreferenceID_ReturnLaterRatedIssueCollection()
        {
            TheVM.GetLaterRatedByPreferenceIDAsync(Guid.NewGuid());
            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(TheVM.IsGetLaterRatedIssuesDone, "Couldn't get later rated issues of certain preference set");
        }

        /// <summary>
        /// Gets the message issues by message I d_ return message issue collection.
        /// </summary>
        [TestMethod]
        public void GetMessageIssuesByMessageID_ReturnMessageIssueCollection()
        {
            TheVM.GetMessageIssuesByMessageIDAsync(Guid.NewGuid());
            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(TheVM.IsGetMessageIssueDone, "Couldn't get offers of certain message");
        }

        /// <summary>
        /// Gets the message option issue by message I d_ return option message collection.
        /// </summary>
        [TestMethod]
        public void GetMessageOptionIssueByMessageID_ReturnOptionMessageCollection()
        {
            TheVM.GetMessageOptionIssueByMessageIDAsync(Guid.NewGuid());
            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(TheVM.IsGetMessageOptionIssueDone, "Couldn't get option offers of certain message");
        }

        /// <summary>
        /// Gets the message later rated issue by message I d_ return later rated collection.
        /// </summary>
        [TestMethod]
        public void GetMessageLaterRatedIssueByMessageID_ReturnLaterRatedCollection()
        {
            TheVM.GetMessageLaterRatedIssueByMessageIDAsync(Guid.NewGuid());
            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(TheVM.IsGetLaterRatedIssuesDone, "Couldn't get LaterRated offers of certain message");
        }
        #endregion Public

        #endregion Methods
    }
}
