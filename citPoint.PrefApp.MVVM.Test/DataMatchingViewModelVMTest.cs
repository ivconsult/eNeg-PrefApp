
#region → Usings   .
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using citPOINT.eNeg.Common;
using citPOINT.PrefApp.Common;
using citPOINT.PrefApp.Data.Web;
using GalaSoft.MvvmLight;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Silverlight.Testing;
using citPOINT.PrefApp.ViewModel;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;
#endregion

#region → History  .

/* Date         User            Change
 * 
 * 26.09.10     Yousra Reda         • creation
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

namespace citPOINT.PrefApp.MVVM.Test
{
    /// <summary>
    /// This is a test class for PrefApp PreferenceSetModel and PreferenceSetViewModel
    /// </summary>
    [TestClass]
  
    public class DataMatchingViewModelVMTest : SilverlightTest
    {

        #region → Fields         .

        private DataMatchingViewModel mTheVM;
        private string ErrorMessage;
        private PreferenceSetsViewModel mPreferenceSetsVM;

        #endregion Fields

        #region → Properties     .
        
        /// <summary>
        /// Gets or sets the preference sets view model.
        /// </summary>
        /// <value>The preference sets view model.</value>
        [Import(PrefAppViewModelTypes.PreferenceSetsViewModel)]
        public PreferenceSetsViewModel PreferenceSetsVM
        {
            get { return mPreferenceSetsVM; }
            set
            {
                mPreferenceSetsVM = value;
            }
        }

        /// <summary>
        /// Gets or sets the preference sets view model.
        /// </summary>
        /// <value>The preference sets view model.</value>
        [Import(PrefAppViewModelTypes.DataMatchingViewModel)]
        public DataMatchingViewModel TheVM
        {
            get { return mTheVM; }
            set
            {
                mTheVM = value;
                value.PreferenceSetsVM = this.PreferenceSetsVM;
            }
        }

        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Called when [raise error message].
        /// </summary>
        /// <param name="ex">The ex.</param>
        private void OnRaiseErrorMessage(Exception ex)
        {
            if (ex != null)
            {

                if (ex.InnerException != null)
                {
                    ErrorMessage = ex.Message + "\r\n" + ex.InnerException.Message;

                }
                else
                    ErrorMessage = ex.Message;

                MessageBox.Show(ErrorMessage, "Error", MessageBoxButton.OK);
            }
        }
             
        /// <summary>
        /// Called when [confirm message].
        /// </summary>
        /// <param name="dialogMessage">The dialog message.</param>
        private void OnConfirmMessage(DialogMessage dialogMessage)
        {
            if (dialogMessage != null)
            {
                dialogMessage.Callback(MessageBoxResult.OK);
            }
        }

        /// <summary>
        /// Called when [load completed].
        /// </summary>
        /// <param name="OperationName">Name of the operation.</param>
        private void OnLoadCompleted(string OperationName)
        {
            if (OperationName == "AvailableNegotiations")
            {
                TheVM.CompleteAddNegotiationCommand.Execute(TheVM.AvailableNegotiations[0]);
            }
        }

        #endregion Private

        #region → Public         .

        /// <summary>
        /// Try Importing Using MEF 
        /// </summary>
        [TestInitialize]
        public void BuidUp()
        {
            CompositionInitializer.SatisfyImports(this);

            #region " Registeration for needed messages in eNegMessenger "
            // register for RaiseErrorMessage
            PrefAppMessanger.RaiseErrorMessage.Register(this, OnRaiseErrorMessage);
            // register for PleaseConfirmMessage
            PrefAppMessanger.ConfirmMessage.Register(this, OnConfirmMessage);
            //After choosing the New negotiation this represent the popup
            PrefAppMessanger.LoadCompleted.Register(this, OnLoadCompleted);
            #endregion

        }
        
        /// <summary>
        /// Tests the success of MEF.
        /// </summary>
        [TestMethod]
        public void TestSuccessOfMEF()
        {
            Assert.IsNotNull(TheVM, "Failed to retrieve the viewmodel via MEF");
        }

        /// <summary>
        /// Gets the main preference set async.
        /// </summary>
        [Asynchronous]
        [TestMethod]
        [Description("Add New Data Matching Numeric Value By Drag Drop")]
        public void Add_New_Data_Matching_Numeric_Value_By_Drag_Drop()
        {

            //Purchase a car
            this.PreferenceSetsVM.CurrentPreferenceSet = this.PreferenceSetsVM.PreferenceSets[MockMaster.PreferenceSetIndex.Car];
            //Set the Current Negotiation
            this.TheVM.CurrentNegotiation = this.TheVM.PreferenceSetNegotiations[MockMaster.PreferenceSetNegIndex.Car];
            //set the Current Conversation
            this.TheVM.CurrentConversation = this.TheVM.NegotiationConversations[MockMaster.ConversationIndex.Car_Conversation_BMW];
            //Set the Current Message
            this.TheVM.CurrentMessage = this.TheVM.ConversationMessages[MockMaster.MessageIndex.Car_Msg_BMW_001];

            //We Excepect to Add New Message Issue for Numeric
            int Expected = this.TheVM.CurrentMessage.MessageIssues.Count + 1;

            #region → Perform Some thing like Drag Drop .

            DataMatchingMessage dataMatchingMessage = new DataMatchingMessage()
                {
                    CurrentIssue = mTheVM.IssuesSource[MockMaster.IssueIndex.Car_Price],
                    CurrentMessage = TheVM.CurrentMessage,
                    Value = "15000"
                };


            PrefAppMessanger.DataMatchMessage.Send(dataMatchingMessage);

            #endregion

            EnqueueCallback(() => Assert.IsTrue(this.TheVM.CurrentMessage.MessageIssues.Count == Expected, "Data Matching Drag Drop Not run."));

            EnqueueTestComplete();
        }


        /// <summary>
        /// Gets the main preference set async.
        /// </summary>
        [Asynchronous]
        [TestMethod]
        [Description("Add New Data Matching Option Value By Drag Drop")]
        public void Add_New_Data_Matching_Option_Value_By_Drag_Drop()
        {


            //Purchase a car
            this.PreferenceSetsVM.CurrentPreferenceSet = this.PreferenceSetsVM.PreferenceSets[MockMaster.PreferenceSetIndex.Car];
            //Set the Current Negotiation
            this.TheVM.CurrentNegotiation = this.TheVM.PreferenceSetNegotiations[MockMaster.PreferenceSetNegIndex.Car];
            //set the Current Conversation
            this.TheVM.CurrentConversation = this.TheVM.NegotiationConversations[MockMaster.ConversationIndex.Car_Conversation_BMW];
            //Set the Current Message
            this.TheVM.CurrentMessage = this.TheVM.ConversationMessages[MockMaster.MessageIndex.Car_Msg_BMW_002];

            //We Excepect to Add New Message Issue for Numeric
            int Expected = this.TheVM.CurrentMessage.MessageIssues.Count + 1;

            #region → Perform Some thing like Drag Drop .

            DataMatchingMessage dataMatchingMessage = new DataMatchingMessage()
            {
                CurrentIssue = mTheVM.IssuesSource[MockMaster.IssueIndex.Car_Color],
                CurrentMessage = TheVM.CurrentMessage,
                Value = "Black",
                IsChecked = true
            };


            PrefAppMessanger.DataMatchMessage.Send(dataMatchingMessage);

            #endregion

            EnqueueCallback(() => Assert.IsTrue(this.TheVM.CurrentMessage.MessageIssues.Count == Expected, "Data Matching Drag Drop Not run."));

            EnqueueTestComplete();
        }



        /// <summary>
        /// Gets the main preference set async.
        /// </summary>
        [Asynchronous]
        [TestMethod]
        [Description("Add New Negotiation To A Preference Set")]
        public void Add_New_Negotiation_To_A_Preference_Set()
        {


            //Purchase a car
            this.PreferenceSetsVM.CurrentPreferenceSet = this.PreferenceSetsVM.PreferenceSets[MockMaster.PreferenceSetIndex.Computer];

            //Set the Current Negotiation
            this.TheVM.CurrentNegotiation = this.TheVM.PreferenceSetNegotiations[MockMaster.PreferenceSetNegIndex.Car];

            //set the Current Conversation
            this.TheVM.CurrentConversation = this.TheVM.NegotiationConversations[MockMaster.ConversationIndex.Car_Conversation_Fiat];

            //Set the Current Message
            this.TheVM.CurrentMessage = this.TheVM.ConversationMessages[MockMaster.MessageIndex.Car_Msg_BMW_002];

            //We Excepect to Add New Message Issue for Numeric
            int Expected = this.TheVM.PreferenceSetNegotiations.Count;


            EnqueueCallback(() => TheVM.AddNegotiationCommand.Execute(null));


            EnqueueCallback(() => Assert.IsTrue(this.TheVM.PreferenceSetNegotiations.Count > Expected, "Data Matching Drag Drop Not run."));

            EnqueueTestComplete();
        }



        /// <summary>
        /// Gets the main preference set async.
        /// </summary>
        [Asynchronous]
        [TestMethod]
        [Description("Remove Negotiation From A Preference Set")]
        public void Remove_Negotiation_From_A_Preference_Set()
        {


            //Purchase a car
            this.PreferenceSetsVM.CurrentPreferenceSet = this.PreferenceSetsVM.PreferenceSets[MockMaster.PreferenceSetIndex.Door];

            //Set the Current Negotiation
            this.TheVM.CurrentNegotiation = this.TheVM.PreferenceSetNegotiations[MockMaster.PreferenceSetNegIndex.Door];
                       

            //We Excepect to Add New Message Issue for Numeric
            int Expected = this.TheVM.PreferenceSetsVM.mPrefSetsModel.Context.PreferenceSetNegs.Count()-1;


            EnqueueCallback(() => TheVM.RemoveNegotiationCommand.Execute(this.TheVM.CurrentNegotiation));


            EnqueueCallback(() => Assert.IsTrue(this.TheVM.PreferenceSetsVM.mPrefSetsModel.Context.PreferenceSetNegs.Count()== Expected, "Data Matching Drag Drop Not run."));

            EnqueueTestComplete();
        }


        /// <summary>
        /// Used To Clean All Resources
        /// </summary>
        [TestCleanup]
        public void CleanUp()
        {
            Messenger.Default.Unregister(this);
        }
        #endregion Public

        #endregion Methods
    }
}
