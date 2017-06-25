
#region → Usings   .
using System;
using System.Linq;
using System.Windows;
//using System.ComponentModel.Composition;
using citPOINT.PrefApp.Common;
using citPOINT.PrefApp.ViewModel;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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

namespace citPOINT.PrefApp.MVVM.UnitTest
{
    /// <summary>
    /// This is a test class for PrefApp PreferenceSetModel and PreferenceSetViewModel
    /// </summary>
    [TestClass]
    public class DataMatchingViewModelViewModel_Test
    {
        #region → Fields         .

        private DataMatchingViewModel mTheVM;
        private string ErrorMessage;
        private PreferenceSetsViewModel mPreferenceSetsVM;

        private bool lastCallIsAddNegotiationCommand = false;

        #endregion Fields

        #region → Properties     .

        /// <summary>
        /// Gets or sets the preference sets view model.
        /// </summary>
        /// <value>The preference sets view model.</value>
        //[Import(PrefAppViewModelTypes.PreferenceSetsViewModel)]
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
        //[Import(PrefAppViewModelTypes.DataMatchingViewModel)]
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

        #region → On Raise Error Message  .

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

                // MessageBox.Show(ErrorMessage, "Error", MessageBoxButton.OK);
            }
        }

        #endregion

        #region → On Confirm Message      .

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

        #endregion

        #region → On Load Completed       .


        /// <summary>
        /// Called when [load completed].
        /// </summary>
        /// <param name="OperationName">Name of the operation.</param>
        private void OnLoadCompleted(string OperationName)
        {
            if (lastCallIsAddNegotiationCommand && OperationName == "AvailableNegotiationsCompleted")
            {
                TheVM.CompleteAddNegotiationCommand.Execute(TheVM.AvailableNegotiations[0]);
            }
        }

        #endregion

        /// <summary>
        /// Makes the data matching for messages.
        /// </summary>
        private void MakeDataMatchingForMessages()
        {
            //Purchase a car
            this.PreferenceSetsVM.CurrentPreferenceSet = this.PreferenceSetsVM.PreferenceSets[MockMaster.PreferenceSetIndex.Car];

            //Set the Current Negotiation
            this.TheVM.CurrentNegotiation = this.TheVM.PreferenceSetNegotiations.Where(ss => ss.PreferenceSetNegID == MockMaster.PreferenceSetNegIndex.Car_Guid).FirstOrDefault();


            #region → Conversation BMW .

            //set the Current Conversation
            this.TheVM.CurrentConversation = this.TheVM.NegotiationConversations.Where(ss => ss.NegConversationID == MockMaster.ConversationIndex.Car_Conversation_BMW_Guid).FirstOrDefault();


            #region → Message 1 (Sent)    .

            /* Issue Name     Value
             *------------------------------
             * Price          11500
             * Color          Black,Silver
             * Model          Mercedes
             * 
             */

            //Set the Current Message
            this.TheVM.CurrentMessage = this.TheVM.ConversationMessages.Where(ss => ss.ConversationMessageID == MockMaster.MessageIndex.Car_Msg_BMW_001_Guid).FirstOrDefault();

            #region → Price .

            DataMatchingMessage dataMatchingMessage = new DataMatchingMessage()
            {
                CurrentIssue = mTheVM.IssuesSource[MockMaster.IssueIndex.Car_Price],
                CurrentMessage = TheVM.CurrentMessage,
                Value = "11500"
            };

            PrefAppMessanger.DataMatchMessage.Send(dataMatchingMessage);

            #endregion

            #region → Color .

            dataMatchingMessage = new DataMatchingMessage()
            {
                CurrentIssue = mTheVM.IssuesSource[MockMaster.IssueIndex.Car_Color],
                CurrentMessage = TheVM.CurrentMessage,
                Value = "Black",
                IsChecked = true
            };
            PrefAppMessanger.DataMatchMessage.Send(dataMatchingMessage);


            dataMatchingMessage = new DataMatchingMessage()
            {
                CurrentIssue = mTheVM.IssuesSource[MockMaster.IssueIndex.Car_Color],
                CurrentMessage = TheVM.CurrentMessage,
                Value = "Silver",
                IsChecked = true
            };
            PrefAppMessanger.DataMatchMessage.Send(dataMatchingMessage);

            #endregion

            #region → Model .

            dataMatchingMessage = new DataMatchingMessage()
            {
                CurrentIssue = mTheVM.IssuesSource[MockMaster.IssueIndex.Car_Model],
                CurrentMessage = TheVM.CurrentMessage,
                Value = "Mercedes",
                IsChecked = true
            };
            PrefAppMessanger.DataMatchMessage.Send(dataMatchingMessage);

            #endregion

            #endregion

            #region → Message 2 (Received).

            /* Issue Name     Value
             *------------------------------
             * Price          12000
             * Color          White
             * Model          Fiat
             * 
             */

            //Set the Current Message
            this.TheVM.CurrentMessage = this.TheVM.ConversationMessages.Where(ss => ss.ConversationMessageID == MockMaster.MessageIndex.Car_Msg_BMW_002_Guid).FirstOrDefault();

            #region → Price .

            dataMatchingMessage = new DataMatchingMessage()
            {
                CurrentIssue = mTheVM.IssuesSource[MockMaster.IssueIndex.Car_Price],
                CurrentMessage = TheVM.CurrentMessage,
                Value = "12000"
            };

            PrefAppMessanger.DataMatchMessage.Send(dataMatchingMessage);

            #endregion

            #region → Color .

            dataMatchingMessage = new DataMatchingMessage()
            {
                CurrentIssue = mTheVM.IssuesSource[MockMaster.IssueIndex.Car_Color],
                CurrentMessage = TheVM.CurrentMessage,
                Value = "White",
                IsChecked = true
            };
            PrefAppMessanger.DataMatchMessage.Send(dataMatchingMessage);

            #endregion

            #region → Model .

            dataMatchingMessage = new DataMatchingMessage()
            {
                CurrentIssue = mTheVM.IssuesSource[MockMaster.IssueIndex.Car_Model],
                CurrentMessage = TheVM.CurrentMessage,
                Value = "Fiat",
                IsChecked = true
            };
            PrefAppMessanger.DataMatchMessage.Send(dataMatchingMessage);

            #endregion

            #endregion

            #endregion

            #region → Conversation Fiat.

            //set the Current Conversation
            this.TheVM.CurrentConversation = this.TheVM.NegotiationConversations.Where(ss => ss.NegConversationID == MockMaster.ConversationIndex.Car_Conversation_Fiat_Guid).FirstOrDefault();


            #region → Message 1 (Sent)    .

            /* Issue Name     Value
             *------------------------------
             * Price          19000
             * Color          White,Silver
             * Power          300
             * Model          Mercedes,Fiat
             * 
             */

            //Set the Current Message
            this.TheVM.CurrentMessage = this.TheVM.ConversationMessages.Where(ss => ss.ConversationMessageID == MockMaster.MessageIndex.Car_Msg_Fiat_003_Guid).FirstOrDefault();

            #region → Price .

            dataMatchingMessage = new DataMatchingMessage()
            {
                CurrentIssue = mTheVM.IssuesSource[MockMaster.IssueIndex.Car_Price],
                CurrentMessage = TheVM.CurrentMessage,
                Value = "19000"
            };

            PrefAppMessanger.DataMatchMessage.Send(dataMatchingMessage);

            #endregion

            #region → Color .

            dataMatchingMessage = new DataMatchingMessage()
            {
                CurrentIssue = mTheVM.IssuesSource[MockMaster.IssueIndex.Car_Color],
                CurrentMessage = TheVM.CurrentMessage,
                Value = "White",
                IsChecked = true
            };
            PrefAppMessanger.DataMatchMessage.Send(dataMatchingMessage);


            dataMatchingMessage = new DataMatchingMessage()
            {
                CurrentIssue = mTheVM.IssuesSource[MockMaster.IssueIndex.Car_Color],
                CurrentMessage = TheVM.CurrentMessage,
                Value = "Silver",
                IsChecked = true
            };
            PrefAppMessanger.DataMatchMessage.Send(dataMatchingMessage);

            #endregion

            #region → Power .

            dataMatchingMessage = new DataMatchingMessage()
            {
                CurrentIssue = mTheVM.IssuesSource[MockMaster.IssueIndex.Car_Power],
                CurrentMessage = TheVM.CurrentMessage,
                Value = "300"
            };

            PrefAppMessanger.DataMatchMessage.Send(dataMatchingMessage);

            #endregion

            #region → Model .

            dataMatchingMessage = new DataMatchingMessage()
            {
                CurrentIssue = mTheVM.IssuesSource[MockMaster.IssueIndex.Car_Model],
                CurrentMessage = TheVM.CurrentMessage,
                Value = "Mercedes",
                IsChecked = true
            };
            PrefAppMessanger.DataMatchMessage.Send(dataMatchingMessage);

            dataMatchingMessage = new DataMatchingMessage()
            {
                CurrentIssue = mTheVM.IssuesSource[MockMaster.IssueIndex.Car_Model],
                CurrentMessage = TheVM.CurrentMessage,
                Value = "Fiat",
                IsChecked = true
            };
            PrefAppMessanger.DataMatchMessage.Send(dataMatchingMessage);

            #endregion

            #endregion

            #endregion

        }

        #endregion

        #region → Public         .

        /// <summary>
        /// Try Importing Using MEF 
        /// </summary>
        [TestInitialize]
        public void BuidUp()
        {
            //CompositionInitializer.SatisfyImports(this);
            this.lastCallIsAddNegotiationCommand = false;

            MockPreferenceSetsModel mockPreferenceSetsModel = new MockPreferenceSetsModel();

            this.PreferenceSetsVM = new PreferenceSetsViewModel(mockPreferenceSetsModel);

            this.TheVM = new DataMatchingViewModel(new MockDataMatchingViewModel(mockPreferenceSetsModel));


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
        /// Test the View Model existance.
        /// </summary>
        [TestMethod]
        public void TestVM_Existance_HaveInstance()
        {
            Assert.IsNotNull(TheVM, "Failed to retrieve the viewmodel via MEF");
        }

        /// <summary>
        /// Add the new numeric value_ by drag drop_ add success.
        /// </summary>
        [TestMethod]
        public void AddNewNumericValue_ByDragDrop_AddSuccess()
        {
            #region → Arrange .

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

            #endregion

            #region → Act     .

            DataMatchingMessage dataMatchingMessage = new DataMatchingMessage()
            {
                CurrentIssue = mTheVM.IssuesSource[MockMaster.IssueIndex.Car_Price],
                CurrentMessage = TheVM.CurrentMessage,
                Value = "15000"
            };

            PrefAppMessanger.DataMatchMessage.Send(dataMatchingMessage);

            #endregion

            #region → Assert  .

            Assert.IsTrue(this.TheVM.CurrentMessage.MessageIssues.Count == Expected &&
                                    this.TheVM.CurrentMessage.Percentage == 25, "Data Matching Drag Drop Not run.");

            #endregion
        }

        /// <summary>
        /// Add the new option value_ by drag drop_ add success.
        /// </summary>
        [TestMethod]
        public void AddNewOptionValue_ByDragDrop_AddSuccess()
        {
            #region → Arrange .

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

            #endregion

            #region → Act     .

            DataMatchingMessage dataMatchingMessage = new DataMatchingMessage()
            {
                CurrentIssue = mTheVM.IssuesSource[MockMaster.IssueIndex.Car_Color],
                CurrentMessage = TheVM.CurrentMessage,
                Value = "Black",
                IsChecked = true
            };


            PrefAppMessanger.DataMatchMessage.Send(dataMatchingMessage);

            #endregion

            #region → Assert  .

            Assert.IsTrue(this.TheVM.CurrentMessage.MessageIssues.Count == Expected &&
                                this.TheVM.CurrentMessage.Percentage == 30, "Data Matching Drag Drop Not run.");

            #endregion

        }

        /// <summary>
        /// Add the new later rated value_ by drag drop_ add success.
        /// </summary>
        [TestMethod]
        public void AddNewLaterRatedValue_ByDragDrop_AddSuccess()
        {

            #region → Arrange .

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

            #endregion

            #region → Act     .

            DataMatchingMessage dataMatchingMessage = new DataMatchingMessage()
                       {
                           CurrentIssue = mTheVM.IssuesSource[MockMaster.IssueIndex.Car_Model],
                           CurrentMessage = TheVM.CurrentMessage,
                           Value = "Mercedes",
                           IsChecked = true
                       };

            PrefAppMessanger.DataMatchMessage.Send(dataMatchingMessage);

            #endregion

            #region → Assert  .

            Assert.IsTrue(this.TheVM.CurrentMessage.MessageIssues.Count == Expected &&
                                     this.TheVM.CurrentMessage.Percentage == 32, "Data Matching Drag Drop Not run.");

            #endregion

        }

        /// <summary>
        /// Add the new option value_ by drag drop_ add success.
        /// </summary>
        [TestMethod]
        public void RemovingOptionValue_ByUnSelect_RemoveSuccess()
        {
            #region → Arrange .

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

            DataMatchingMessage dataMatchingMessage = new DataMatchingMessage()
            {
                CurrentIssue = mTheVM.IssuesSource[MockMaster.IssueIndex.Car_Color],
                CurrentMessage = TheVM.CurrentMessage,
                Value = "Black",
                IsChecked = true
            };


            PrefAppMessanger.DataMatchMessage.Send(dataMatchingMessage);

            Assert.IsTrue(this.TheVM.CurrentMessage.MessageIssues.Count == Expected &&
                                this.TheVM.CurrentMessage.Percentage == 30, "Data Matching Drag Drop Not run.");
            #endregion

            #region → Act     .

            dataMatchingMessage = new DataMatchingMessage()
            {
                CurrentIssue = mTheVM.IssuesSource[MockMaster.IssueIndex.Car_Color],
                CurrentMessage = TheVM.CurrentMessage,
                Value = "Black",
                IsChecked = false
            };


            PrefAppMessanger.DataMatchMessage.Send(dataMatchingMessage);

            #endregion

            #region → Assert  .

            Assert.IsTrue(string.IsNullOrEmpty(this.TheVM.CurrentMessage.MessageIssues.First().Value) &&
                                this.TheVM.CurrentMessage.Percentage == 0, "Data Matching Drag Drop Not run.");

            #endregion

        }
        
        /// <summary>
        /// Add the new later rated value_ by drag drop_ add success.
        /// </summary>
        [TestMethod]
        public void RemoveLaterRatedValue_ByUnSelect_RemoveSuccess()
        {

            #region → Arrange .

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

            DataMatchingMessage dataMatchingMessage = new DataMatchingMessage()
            {
                CurrentIssue = mTheVM.IssuesSource[MockMaster.IssueIndex.Car_Model],
                CurrentMessage = TheVM.CurrentMessage,
                Value = "Mercedes",
                IsChecked = true
            };

            PrefAppMessanger.DataMatchMessage.Send(dataMatchingMessage);

            Assert.IsTrue(this.TheVM.CurrentMessage.MessageIssues.Count == Expected &&
                                  this.TheVM.CurrentMessage.Percentage == 32, "Remove Later Rated Value-By Un Select-Failed");
            #endregion

            #region → Act     .

            dataMatchingMessage = new DataMatchingMessage()
            {
                CurrentIssue = mTheVM.IssuesSource[MockMaster.IssueIndex.Car_Model],
                CurrentMessage = TheVM.CurrentMessage,
                Value = "Mercedes",
                IsChecked = false
            };

            PrefAppMessanger.DataMatchMessage.Send(dataMatchingMessage);

            #endregion

            #region → Assert  .

            Assert.IsTrue(string.IsNullOrEmpty(this.TheVM.CurrentMessage.MessageIssues.FirstOrDefault().Value) &&
                          this.TheVM.CurrentMessage.Percentage == 0, "Remove Later Rated Value-By Un Select-Failed");

            #endregion

        }

        /// <summary>
        /// Add the negotiation_ to preference set_ add success.
        /// </summary>
        [TestMethod]
        public void AddNegotiation_ToPreferenceSet_AddSuccess()
        {
            #region → Arrange .

            this.lastCallIsAddNegotiationCommand = true;

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

            #endregion

            #region → Act     .

            TheVM.AddNegotiationCommand.Execute(null);

            #endregion

            #region → Assert  .

            Assert.IsTrue(this.TheVM.PreferenceSetNegotiations.Count > Expected, "Data Matching Drag Drop Not run.");

            #endregion

        }

        /// <summary>
        /// Removes the negotiation_ from preference set_ remove success.
        /// </summary>
        [TestMethod]
        public void RemoveNegotiation_FromPreferenceSet_RemoveSuccess()
        {
            #region → Arrange .

            //Purchase a car
            this.PreferenceSetsVM.CurrentPreferenceSet = this.PreferenceSetsVM.PreferenceSets[MockMaster.PreferenceSetIndex.Door];

            //Set the Current Negotiation
            this.TheVM.CurrentNegotiation = this.TheVM.PreferenceSetNegotiations[MockMaster.PreferenceSetNegIndex.Door];


            //We Excepect to Add New Message Issue for Numeric
            int Expected = this.TheVM.PreferenceSetsVM.mPrefSetsModel.Context.PreferenceSetNegs.Count() - 1;

            #endregion

            #region → Act     .

            TheVM.RemoveNegotiationCommand.Execute(this.TheVM.CurrentNegotiation);

            #endregion

            #region → Assert  .

            Assert.IsTrue(this.TheVM.PreferenceSetsVM.mPrefSetsModel.Context.PreferenceSetNegs.Count() == Expected, "Data Matching Drag Drop Not run.");

            #endregion

        }

        /// <summary>
        /// Report all conversation_ filter by last data_ return true collection.
        /// </summary>
        [TestMethod]
        public void Report_AllConversation_FilterByLastData_ReturnTrueCollection()
        {
            #region → Arrange .

            this.MakeDataMatchingForMessages();

            #endregion

            #region → Act     .

            this.TheVM.FilterDataForConversationCommand.Execute(citPOINT.PrefApp.ViewModel.DataMatchingViewModel.FilterType.LastData.ToString());

            #endregion

            #region → Assert  .

            Decimal TotalSentScore = 0;
            Decimal TotalReceivedScore = 0;

            foreach (Data.FilteredIssue filteredIssueItem in this.TheVM.FilteredIssueSource)
            {
                TotalSentScore += filteredIssueItem.SentValueScore;
                TotalReceivedScore += filteredIssueItem.ReceivedValueScore;

                switch (filteredIssueItem.IssueName)
                {
                    case "Price":
                        Assert.IsTrue(filteredIssueItem.SentValue == "19000 $", "Filter Issue Report Error [All Conversation Last Values]");
                        Assert.IsTrue(filteredIssueItem.SentValueScore == 5, "Filter Issue Report Error [All Conversation Last Values]");

                        Assert.IsTrue(filteredIssueItem.ReceivedValue == "12000 $", "Filter Issue Report Error [All Conversation Last Values]");
                        Assert.IsTrue(filteredIssueItem.ReceivedValueScore == 25, "Filter Issue Report Error [All Conversation Last Values]");
                        break;

                    case "Color":
                        Assert.IsTrue(filteredIssueItem.SentValue.Trim() == "White,Silver", "Filter Issue Report Error [All Conversation Last Values]");
                        Assert.IsTrue(filteredIssueItem.SentValueScore == 12, "Filter Issue Report Error [All Conversation Last Values]");

                        Assert.IsTrue(filteredIssueItem.ReceivedValue.Trim() == "White", "Filter Issue Report Error [All Conversation Last Values]");
                        Assert.IsTrue(filteredIssueItem.ReceivedValueScore == 3, "Filter Issue Report Error [All Conversation Last Values]");
                        break;

                    case "Power":
                        Assert.IsTrue(filteredIssueItem.SentValue.Trim() == "300 Hourse", "Filter Issue Report Error [All Conversation Last Values]");
                        Assert.IsTrue(filteredIssueItem.SentValueScore == 5, "Filter Issue Report Error [All Conversation Last Values]");

                        Assert.IsNull(filteredIssueItem.ReceivedValue, "Filter Issue Report Error [All Conversation Last Values]");
                        Assert.IsTrue(filteredIssueItem.ReceivedValueScore == 0, "Filter Issue Report Error [All Conversation Last Values]");

                        break;

                    case "Model":
                        Assert.IsTrue(filteredIssueItem.SentValue.Trim() == "Mercedes,Fiat", "Filter Issue Report Error [All Conversation Last Values]");
                        Assert.IsTrue(filteredIssueItem.SentValueScore == 32, "Filter Issue Report Error [All Conversation Last Values]");

                        Assert.IsTrue(filteredIssueItem.ReceivedValue.Trim() == "Fiat", "Filter Issue Report Error [All Conversation Last Values]");
                        Assert.IsTrue(filteredIssueItem.ReceivedValueScore == 4, "Filter Issue Report Error [All Conversation Last Values]");

                        break;

                    default:
                        break;
                }

            }


            Assert.IsTrue(TotalSentScore == 54, "Filter Issue Report Error [All Conversation Last Values]");
            Assert.IsTrue(TotalReceivedScore == 32, "Filter Issue Report Error [All Conversation Last Values]");


            Assert.IsTrue(this.TheVM.FilteredIssueSource.Count() > 0, "Filter Issue Report Error [All Conversation Last Values]");

            #endregion

        }

        /// <summary>
        /// Report all conversation_ filter by best data_ return true collection.
        /// </summary>
        [TestMethod]
        public void Report_AllConversation_FilterByBestData_ReturnTrueCollection()
        {
            #region → Arrange .

            this.MakeDataMatchingForMessages();

            #endregion

            #region → Act     .

            this.TheVM.FilterDataForConversationCommand.Execute(citPOINT.PrefApp.ViewModel.DataMatchingViewModel.FilterType.BestScoring.ToString());

            #endregion

            #region → Assert  .

            Decimal TotalSentScore = 0;
            Decimal TotalReceivedScore = 0;

            foreach (Data.FilteredIssue filteredIssueItem in this.TheVM.FilteredIssueSource)
            {
                TotalSentScore += filteredIssueItem.SentValueScore;
                TotalReceivedScore += filteredIssueItem.ReceivedValueScore;

                switch (filteredIssueItem.IssueName)
                {
                    case "Price":
                        Assert.IsTrue(filteredIssueItem.SentValue == "11500 $", "Filter Issue Report Error [All Conversation Best Values]");
                        Assert.IsTrue(filteredIssueItem.SentValueScore == 18.75M, "Filter Issue Report Error [All Conversation Best Values]");

                        Assert.IsTrue(filteredIssueItem.ReceivedValue == "12000 $", "Filter Issue Report Error [All Conversation Best Values]");
                        Assert.IsTrue(filteredIssueItem.ReceivedValueScore == 25, "Filter Issue Report Error [All Conversation Best Values]");
                        break;

                    case "Color":
                        Assert.IsTrue(filteredIssueItem.SentValue.Trim() == "Black,Silver", "Filter Issue Report Error [All Conversation Best Values]");
                        Assert.IsTrue(filteredIssueItem.SentValueScore == 30, "Filter Issue Report Error [All Conversation Best Values]");

                        Assert.IsTrue(filteredIssueItem.ReceivedValue.Trim() == "White", "Filter Issue Report Error [All Conversation Best Values]");
                        Assert.IsTrue(filteredIssueItem.ReceivedValueScore == 3, "Filter Issue Report Error [All Conversation Best Values]");
                        break;

                    case "Model":
                        Assert.IsTrue(filteredIssueItem.SentValue.Trim() == "Mercedes", "Filter Issue Report Error [All Conversation Best Values]");
                        Assert.IsTrue(filteredIssueItem.SentValueScore == 32, "Filter Issue Report Error [All Conversation Best Values]");

                        Assert.IsTrue(filteredIssueItem.ReceivedValue.Trim() == "Fiat", "Filter Issue Report Error [All Conversation Best Values]");
                        Assert.IsTrue(filteredIssueItem.ReceivedValueScore == 4, "Filter Issue Report Error [All Conversation Best Values]");

                        break;

                    default:
                        break;
                }

            }


            Assert.IsTrue(TotalSentScore == 80.75M, "Filter Issue Report Error [All Conversation Best Values]");
            Assert.IsTrue(TotalReceivedScore == 32, "Filter Issue Report Error [All Conversation Best Values]");


            Assert.IsTrue(this.TheVM.FilteredIssueSource.Count() > 0, "Filter Issue Report Error [All Conversation Best Values]");

            #endregion

        }

        /// <summary>
        /// Report the certain conversation_ filter by last data_ return true collection.
        /// </summary>
        [TestMethod]
        public void Report_CertainConversation_FilterByLastData_ReturnTrueCollection()
        {
            #region → Arrange .

            this.MakeDataMatchingForMessages();

            #endregion

            #region → Act     .

            //set the Current Conversation
            this.TheVM.ReportSelectedConversation = this.TheVM.NegotiationConversations.Where(ss => ss.NegConversationID == MockMaster.ConversationIndex.Car_Conversation_Fiat_Guid).FirstOrDefault();

            this.TheVM.FilterDataForConversationCommand.Execute(citPOINT.PrefApp.ViewModel.DataMatchingViewModel.FilterType.LastData.ToString());

            #endregion

            #region → Assert  .

            Decimal TotalSentScore = 0;
            Decimal TotalReceivedScore = 0;

            foreach (Data.FilteredIssue filteredIssueItem in this.TheVM.FilteredIssueSource)
            {
                TotalSentScore += filteredIssueItem.SentValueScore;
                TotalReceivedScore += filteredIssueItem.ReceivedValueScore;

                switch (filteredIssueItem.IssueName)
                {
                    case "Price":
                        Assert.IsTrue(filteredIssueItem.SentValue == "19000 $", "Filter Issue Report Error [Certain Conversation Last Values]");
                        Assert.IsTrue(filteredIssueItem.SentValueScore == 5, "Filter Issue Report Error [Certain Conversation Last Values]");

                        Assert.IsNull(filteredIssueItem.ReceivedValue, "Filter Issue Report Error [Certain Conversation Last Values]");
                        Assert.IsTrue(filteredIssueItem.ReceivedValueScore == 0, "Filter Issue Report Error [Certain Conversation Last Values]");
                        break;

                    case "Color":
                        Assert.IsTrue(filteredIssueItem.SentValue.Trim() == "White,Silver", "Filter Issue Report Error [Certain Conversation Last Values]");
                        Assert.IsTrue(filteredIssueItem.SentValueScore == 12, "Filter Issue Report Error [Certain Conversation Last Values]");

                        Assert.IsNull(filteredIssueItem.ReceivedValue, "Filter Issue Report Error [Certain Conversation Last Values]");
                        Assert.IsTrue(filteredIssueItem.ReceivedValueScore == 0, "Filter Issue Report Error [Certain Conversation Last Values]");
                        break;

                    case "Power":
                        Assert.IsTrue(filteredIssueItem.SentValue.Trim() == "300 Hourse", "Filter Issue Report Error [Certain Conversation Last Values]");
                        Assert.IsTrue(filteredIssueItem.SentValueScore == 5, "Filter Issue Report Error [Certain Conversation Last Values]");

                        Assert.IsNull(filteredIssueItem.ReceivedValue, "Filter Issue Report Error [Certain Conversation Last Values]");
                        Assert.IsTrue(filteredIssueItem.ReceivedValueScore == 0, "Filter Issue Report Error [Certain Conversation Last Values]");

                        break;

                    case "Model":
                        Assert.IsTrue(filteredIssueItem.SentValue.Trim() == "Mercedes,Fiat", "Filter Issue Report Error [Certain Conversation Last Values]");
                        Assert.IsTrue(filteredIssueItem.SentValueScore == 32, "Filter Issue Report Error [Certain Conversation Last Values]");

                        Assert.IsNull(filteredIssueItem.ReceivedValue, "Filter Issue Report Error [Certain Conversation Last Values]");
                        Assert.IsTrue(filteredIssueItem.ReceivedValueScore == 0, "Filter Issue Report Error [Certain Conversation Last Values]");

                        break;

                    default:
                        break;
                }

            }


            Assert.IsTrue(TotalSentScore == 54M, "Filter Issue Report Error [Certain Conversation Last Values]");
            Assert.IsTrue(TotalReceivedScore == 0, "Filter Issue Report Error [Certain Conversation Last Values]");


            Assert.IsTrue(this.TheVM.FilteredIssueSource.Count() > 0, "Filter Issue Report Error [Certain Conversation Last Values]");

            #endregion

        }

        /// <summary>
        /// Report the certain conversation_ filter by best data_ return true collection.
        /// </summary>
        [TestMethod]
        public void Report_CertainConversation_FilterByBestData_ReturnTrueCollection()
        {
            #region → Arrange .

            this.MakeDataMatchingForMessages();

            #endregion

            #region → Act     .

            //set the Current Conversation
            this.TheVM.ReportSelectedConversation = this.TheVM.NegotiationConversations.Where(ss => ss.NegConversationID == MockMaster.ConversationIndex.Car_Conversation_Fiat_Guid).FirstOrDefault();

            this.TheVM.FilterDataForConversationCommand.Execute(citPOINT.PrefApp.ViewModel.DataMatchingViewModel.FilterType.BestScoring.ToString());

            #endregion

            #region → Assert  .

            Decimal TotalSentScore = 0;
            Decimal TotalReceivedScore = 0;

            foreach (Data.FilteredIssue filteredIssueItem in this.TheVM.FilteredIssueSource)
            {
                TotalSentScore += filteredIssueItem.SentValueScore;
                TotalReceivedScore += filteredIssueItem.ReceivedValueScore;

                switch (filteredIssueItem.IssueName)
                {
                    case "Price":
                        Assert.IsTrue(filteredIssueItem.SentValue == "19000 $", "Filter Issue Report Error [Certain Conversation Last Values]");
                        Assert.IsTrue(filteredIssueItem.SentValueScore == 5, "Filter Issue Report Error [Certain Conversation Last Values]");

                        Assert.IsNull(filteredIssueItem.ReceivedValue, "Filter Issue Report Error [Certain Conversation Last Values]");
                        Assert.IsTrue(filteredIssueItem.ReceivedValueScore == 0, "Filter Issue Report Error [Certain Conversation Last Values]");
                        break;

                    case "Color":
                        Assert.IsTrue(filteredIssueItem.SentValue.Trim() == "White,Silver", "Filter Issue Report Error [Certain Conversation Last Values]");
                        Assert.IsTrue(filteredIssueItem.SentValueScore == 12, "Filter Issue Report Error [Certain Conversation Last Values]");

                        Assert.IsNull(filteredIssueItem.ReceivedValue, "Filter Issue Report Error [Certain Conversation Last Values]");
                        Assert.IsTrue(filteredIssueItem.ReceivedValueScore == 0, "Filter Issue Report Error [Certain Conversation Last Values]");
                        break;

                    case "Power":
                        Assert.IsTrue(filteredIssueItem.SentValue.Trim() == "300 Hourse", "Filter Issue Report Error [Certain Conversation Last Values]");
                        Assert.IsTrue(filteredIssueItem.SentValueScore == 5, "Filter Issue Report Error [Certain Conversation Last Values]");

                        Assert.IsNull(filteredIssueItem.ReceivedValue, "Filter Issue Report Error [Certain Conversation Last Values]");
                        Assert.IsTrue(filteredIssueItem.ReceivedValueScore == 0, "Filter Issue Report Error [Certain Conversation Last Values]");

                        break;

                    case "Model":
                        Assert.IsTrue(filteredIssueItem.SentValue.Trim() == "Mercedes,Fiat", "Filter Issue Report Error [Certain Conversation Last Values]");
                        Assert.IsTrue(filteredIssueItem.SentValueScore == 32, "Filter Issue Report Error [Certain Conversation Last Values]");

                        Assert.IsNull(filteredIssueItem.ReceivedValue, "Filter Issue Report Error [Certain Conversation Last Values]");
                        Assert.IsTrue(filteredIssueItem.ReceivedValueScore == 0, "Filter Issue Report Error [Certain Conversation Last Values]");

                        break;

                    default:
                        break;
                }

            }


            Assert.IsTrue(TotalSentScore == 54M, "Filter Issue Report Error [Certain Conversation Last Values]");
            Assert.IsTrue(TotalReceivedScore == 0, "Filter Issue Report Error [Certain Conversation Last Values]");


            Assert.IsTrue(this.TheVM.FilteredIssueSource.Count() > 0, "Filter Issue Report Error [Certain Conversation Last Values]");

            #endregion

        }

        [TestCleanup]
        public void CleanUp()
        {
            // call Cleanup on its ViewModel
            this.PreferenceSetsVM.Cleanup();
            this.TheVM.Cleanup();

            // Cleanup itself
            Messenger.Default.Unregister(this);

        }

        #endregion

        #endregion
    }
}


