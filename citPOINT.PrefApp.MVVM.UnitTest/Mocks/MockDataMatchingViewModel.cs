#region → Usings   .
using citPOINT.eNeg.Common;
using citPOINT.PrefApp.Common;
using citPOINT.PrefApp.Data.Web;
using System;
using System.ComponentModel;
//using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel.DomainServices.Client;
using System.Windows;
#endregion

#region → History  .

/* Date         User              Change
 * 
 * 13.02.11     M.Whab       Creation
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
    /// Mock Data Matching View Model
    /// </summary>
    // [Export(typeof(IDataMatchingModel))]
    //[PartCreationPolicy(CreationPolicy.NonShared)]
    public class MockDataMatchingViewModel : IDataMatchingModel
    {

        #region → Properties     .
        private PrefAppContext mContext;

        /// <summary>
        /// instance of PrefAppContext of PrefApp to can use available services
        /// </summary>
        /// <value>The context.</value>
        public PrefAppContext Context
        {
            get
            {
                return mContext;
            }
            set
            {
                mContext = value;
            }
        }

        /// <summary>
        /// Gets the has changes that indicate whether context has changes or not.
        /// </summary>
        /// <value>The has changes.</value>
        public bool HasChanges
        {
            get { return false; }
        }

        /// <summary>
        /// True if either "IsLoading" or "IsSubmitting" is
        /// in progress; otherwise, false
        /// </summary>
        /// <value>The is busy.</value>
        public bool IsBusy
        {
            get { return false; }
        }

        /// <summary>
        /// Gets or sets the max query per call.
        /// </summary>
        /// <value>The max query per call.</value>
        public int MaxQueryPerCall
        {
            get
            {
                return 1000;
            }
            set { }
        }

        #endregion Properties

        #region → Events         .

        /// <summary>
        /// Occurs when [retrieve application DM status completed].
        /// </summary>
        public event Action<InvokeOperation<bool>> RetrieveApplicationDMStatusCompleted;

        /// <summary>
        /// Occurs when [update data matching status in addon completed].
        /// </summary>
        public event Action<InvokeOperation<bool>> UpdateDataMatchingStatusInAddonCompleted;

        /// <summary>
        /// Occurs when [get Preference Set Negotiations complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<PreferenceSetNeg>> GetPreferenceSetNegotiationsComplete;


        /// <summary>
        /// Occurs when [get Negotiations conversations complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<NegConversation>> GetNegotiationConversationsComplete;

        /// <summary>
        /// Occurs when [get conversation messages complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<ConversationMessage>> GetConversationMessagesComplete;

        /// <summary>
        /// Occurs when [get message issues complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<MessageIssue>> GetMessageIssuesComplete;

        /// <summary>
        /// Occurs when [get message option issues complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<MessageOptionIssue>> GetMessageOptionIssuesComplete;

        /// <summary>
        /// Occurs when [get message later rated issues complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<MessageLaterRatedIssue>> GetMessageLaterRatedIssuesComplete;

        /// <summary>
        /// Occurs when [get available negotiations to analysis complete].
        /// In case of Add New Neg to Preference Set
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<Negotiation>> GetAvailableNegotiationsToAnalysisComplete;

        /// <summary>
        /// Occurs when [get negotiations details by I ds complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<Negotiation>> GetNegotiationsDetailsByIDsComplete;

        /// <summary>
        /// Occurs when [get conversations details by I ds complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<Conversation>> GetConversationsDetailsByIDsComplete;

        /// <summary>
        /// Occurs when [get messages details by I ds complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<Message>> GetMessagesDetailsByIDsComplete;


        /// <summary>
        /// Occurs when [Saving Changing Complete].
        /// </summary>
        public event EventHandler<SubmitOperationEventArgs> SaveChangesComplete;


        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region → Methods        .

        #region → Public         .

        /// <summary>
        /// Retrieves the application DM status async.
        /// </summary>
        /// <param name="AppName">Name of the app.</param>
        /// <param name="UserID">The user ID.</param>
        public void RetrieveApplicationDMStatusAsync(string AppName, Guid UserID)
        {
            if (RetrieveApplicationDMStatusCompleted != null)
            {
                RetrieveApplicationDMStatusCompleted(null);
            }
        }

        /// <summary>
        /// Updates the data matching status in addon async.
        /// </summary>
        /// <param name="AppName">Name of the app.</param>
        /// <param name="UserID">The user ID.</param>
        /// <param name="IsActive">The is active.</param>
        public void UpdateDataMatchingStatusInAddonAsync(string AppName, Guid UserID, bool IsActive)
        {
            if (UpdateDataMatchingStatusInAddonCompleted != null)
            {
                UpdateDataMatchingStatusInAddonCompleted(null);
            }
        }

        /// <summary>
        /// Get Preference Set Negotiations for the Current login User.
        /// </summary>
        public void GetPreferenceSetNegotiationsAsync()
        {
            if (GetPreferenceSetNegotiationsComplete != null)
            {
                GetPreferenceSetNegotiationsComplete(this,
                    new eNegEntityResultArgs<PreferenceSetNeg>(this.Context.PreferenceSetNegs));
            }
        }

        /// <summary>
        /// Get Negotiation Conversation Async.
        /// </summary>
        public void GetNegotiationConversationsAsync()
        {
            if (GetNegotiationConversationsComplete != null)
            {
                GetNegotiationConversationsComplete(this,
                    new eNegEntityResultArgs<NegConversation>(this.Context.NegConversations));
            }
        }

        /// <summary>
        /// Get Conversation Messages Async.
        /// </summary>
        public void GetConversationMessagesAsync()
        {

            if (GetConversationMessagesComplete != null)
            {
                GetConversationMessagesComplete(this,
                    new eNegEntityResultArgs<ConversationMessage>(this.Context.ConversationMessages));
            }

        }

        /// <summary>
        /// Gets the message issues async.
        /// </summary>
        public void GetMessageIssuesAsync()
        {
            if (GetMessageIssuesComplete != null)
            {
                GetMessageIssuesComplete(this,
                    new eNegEntityResultArgs<MessageIssue>(this.Context.MessageIssues));
            }
        }

        /// <summary>
        /// Gets the message option issues async.
        /// </summary>
        public void GetMessageOptionIssuesAsync()
        {
            if (GetMessageIssuesComplete != null)
            {
                GetMessageIssuesComplete(this,
                    new eNegEntityResultArgs<MessageIssue>(this.Context.MessageIssues));
            }
        }

        /// <summary>
        /// Gets the message later rated issues async.
        /// </summary>
        public void GetMessageLaterRatedIssuesAsync()
        {
            if (GetMessageLaterRatedIssuesComplete != null)
            {
                GetMessageLaterRatedIssuesComplete(this,
                    new eNegEntityResultArgs<MessageLaterRatedIssue>(this.Context.MessageLaterRatedIssues));
            }
        }

        /// <summary>
        /// Gets the available negotiations to analysis async.
        /// </summary>
        public void GetAvailableNegotiationsToAnalysisAsync()
        {
            if (GetAvailableNegotiationsToAnalysisComplete != null)
            {
                GetAvailableNegotiationsToAnalysisComplete(this,
                    new eNegEntityResultArgs<Negotiation>(this.CurrentMockMaster.AvailableNegotiations));
            }
        }

        /// <summary>
        /// Gets the negotiations details by I ds async.
        /// </summary>
        /// <param name="NegotiationIDs">The negotiation I ds.</param>
        public void GetNegotiationsDetailsByIDsAsync(Guid[] NegotiationIDs)
        {
            if (GetNegotiationsDetailsByIDsComplete != null)
            {
                GetNegotiationsDetailsByIDsComplete(this,
                    new eNegEntityResultArgs<Negotiation>(this.CurrentMockMaster.Negotiations));
            }
        }

        /// <summary>
        /// Gets the conversations details by I ds async.
        /// </summary>
        /// <param name="NegotiationIDs">The negotiation I ds.</param>
        public void GetConversationsDetailsByIDsAsync(Guid[] NegotiationIDs)
        {
            if (GetConversationsDetailsByIDsComplete != null)
            {
                GetConversationsDetailsByIDsComplete(this,
                    new eNegEntityResultArgs<Conversation>(this.CurrentMockMaster.Conversations));
            }
        }

        /// <summary>
        /// Gets the messages details by I ds async.
        /// </summary>
        /// <param name="NegotiationIDs">The negotiation I ds.</param>
        public void GetMessagesDetailsByIDsAsync(Guid[] NegotiationIDs)
        {
            if (GetMessagesDetailsByIDsComplete != null)
            {
                GetMessagesDetailsByIDsComplete(this,
                    new eNegEntityResultArgs<Message>(this.CurrentMockMaster.Messages));
            }
        }

        /// <summary>
        /// Adds the preference set neg.
        /// </summary>
        /// <param name="SetInContext">The set in context.</param>
        /// <param name="Prefset">The prefset.</param>
        /// <param name="NegotiationID">The negotiation ID.</param>
        /// <param name="NegotiationName">Name of the negotiation.</param>
        /// <returns>Added PreferenceSet Neg</returns>
        public PreferenceSetNeg AddPreferenceSetNeg(bool SetInContext, PreferenceSet Prefset, Guid NegotiationID, string NegotiationName)
        {
            PreferenceSetNeg preferenceSetNeg = new PreferenceSetNeg()
            {
                PreferenceSetNegID = Guid.NewGuid(),
                PreferenceSet = Prefset,
                PreferenceSetID = Prefset.PreferenceSetID,
                NegID = NegotiationID,
                Percentage = 0,
                Deleted = false,
                DeletedOn = DateTime.Now,
                DeletedBy = PrefAppConfigurations.CurrentLoginUser.UserID,
                Name = NegotiationName
            };


            if (SetInContext)
                this.Context.PreferenceSetNegs.Add(preferenceSetNeg);

            return preferenceSetNeg;
        }

        /// <summary>
        /// Adds the neg conversation.
        /// </summary>
        /// <param name="SetInContext">if set to <c>true</c> [set in context].</param>
        /// <param name="PrefSetNeg">The pref set neg.</param>
        /// <param name="ConvID">The conv ID.</param>
        /// <param name="ConversationName">Name of the conversation.</param>
        /// <returns>Added Negotiation Conversation</returns>
        public NegConversation AddNegConversation(bool SetInContext, PreferenceSetNeg PrefSetNeg, Guid ConvID, string ConversationName)
        {
            NegConversation negConversation = new NegConversation()
            {
                NegConversationID = Guid.NewGuid(),
                PreferenceSetNegID = PrefSetNeg.PreferenceSetNegID,
                PreferenceSetNeg = PrefSetNeg,
                ConversationID = ConvID,
                Percentage = 0,
                Deleted = false,
                DeletedOn = DateTime.Now,
                DeletedBy = PrefAppConfigurations.CurrentLoginUser.UserID,
                Name = ConversationName
            };


            if (SetInContext)
                this.Context.NegConversations.Add(negConversation);

            return negConversation;
        }

        /// <summary>
        /// Adds the conversation message.
        /// </summary>
        /// <param name="SetInContext">if set to <c>true</c> [set in context].</param>
        /// <param name="NegConv">The neg conv.</param>
        /// <param name="MsgID">The MSG ID.</param>
        /// <returns>Added Conversation Message</returns>
        public ConversationMessage AddConversationMessage(bool SetInContext, NegConversation NegConv, Guid MsgID)
        {
            ConversationMessage conversationMessage = new ConversationMessage()
            {
                ConversationMessageID = Guid.NewGuid(),
                MessageID = MsgID,
                NegConversation = NegConv,
                NegConversationID = NegConv.NegConversationID,
                Percentage = null,
                Deleted = false,
                DeletedOn = DateTime.Now,
                DeletedBy = PrefAppConfigurations.CurrentLoginUser.UserID
            };


            if (SetInContext)
                this.Context.ConversationMessages.Add(conversationMessage);

            return conversationMessage;
        }

        /// <summary>
        /// Adds the message issue.
        /// </summary>
        /// <param name="SetInContext">if set to <c>true</c> [set in context].</param>
        /// <param name="ConvMessage">The conv message.</param>
        /// <param name="issue">The issue.</param>
        /// <returns>Added Message Issue</returns>
        public MessageIssue AddMessageIssue(bool SetInContext, ConversationMessage ConvMessage, Issue issue)
        {
            MessageIssue messageIssue = new MessageIssue()
            {
                IssueID = issue.IssueID,
                Issue = issue,
                ConversationMessage = ConvMessage,
                ConversationMessageID = ConvMessage.ConversationMessageID,
                MessageIssueID = Guid.NewGuid(),
                Score = 0,
                Deleted = false,
                DeletedOn = DateTime.Now,
                DeletedBy = PrefAppConfigurations.CurrentLoginUser.UserID
            };



            if (SetInContext)
                this.Context.MessageIssues.Add(messageIssue);

            return messageIssue;
        }

        /// <summary>
        /// Adds the message option issue.
        /// </summary>
        /// <param name="SetInContext">if set to <c>true</c> [set in context].</param>
        /// <param name="MsgIssue">The MSG issue.</param>
        /// <param name="optionIssue">The option issue.</param>
        /// <returns>Added Message Option Issue</returns>
        public MessageOptionIssue AddMessageOptionIssue(bool SetInContext, MessageIssue MsgIssue, OptionIssue optionIssue)
        {
            MessageOptionIssue messageOptionIssue = new MessageOptionIssue()
            {
                MessageIssue = MsgIssue,
                MessageIssueID = MsgIssue.MessageIssueID,
                MessageOptionIssueID = Guid.NewGuid(),
                OptionIssue = optionIssue,
                OptionIssueID = optionIssue.OptionIssueID,
                Deleted = false,
                DeletedOn = DateTime.Now,
                DeletedBy = PrefAppConfigurations.CurrentLoginUser.UserID
            };


            if (SetInContext)
                this.Context.MessageOptionIssues.Add(messageOptionIssue);

            return messageOptionIssue;
        }

        /// <summary>
        /// Adds the message later rated issue.
        /// </summary>
        /// <param name="SetInContext">if set to <c>true</c> [set in context].</param>
        /// <param name="MsgIssue">The MSG issue.</param>
        /// <param name="laterRatedIssue">The later rated issue.</param>
        /// <returns>Added Message LaterRated Issue</returns>
        public MessageLaterRatedIssue AddMessageLaterRatedIssue(bool SetInContext, MessageIssue MsgIssue, LaterRatedIssue laterRatedIssue)
        {
            MessageLaterRatedIssue messageLaterRatedIssue = new MessageLaterRatedIssue()
            {
                MessageLaterRatedIssueID = Guid.NewGuid(),
                LaterRatedIssue = laterRatedIssue,
                LaterRatedIssueID = laterRatedIssue.LaterRatedIssueID,
                MessageIssue = MsgIssue,
                MessageIssueID = MsgIssue.MessageIssueID,
                Deleted = false,
                DeletedOn = DateTime.Now,
                DeletedBy = PrefAppConfigurations.CurrentLoginUser.UserID
            };


            if (SetInContext)
                this.Context.MessageLaterRatedIssues.Add(messageLaterRatedIssue);

            return messageLaterRatedIssue;
        }

        /// <summary>
        /// Removes the preference set neg.
        /// </summary>
        /// <param name="PrefSetNeg">The pref set neg.</param>
        public void RemovePreferenceSetNeg(PreferenceSetNeg PrefSetNeg)
        {
            if (this.Context.PreferenceSetNegs.Contains(PrefSetNeg))
            {
                PrefSetNeg = this.Context.PreferenceSetNegs.FirstOrDefault(s => s.PreferenceSetNegID == PrefSetNeg.PreferenceSetNegID);

                while (PrefSetNeg.NegConversations.Count > 0)
                {
                    RemoveNegConversation(PrefSetNeg.NegConversations.FirstOrDefault());
                }

                PrefSetNeg.PreferenceSet.PreferenceSetNegs.Remove(PrefSetNeg);

                this.Context.PreferenceSetNegs.Remove(PrefSetNeg);
            }
        }

        /// <summary>
        /// Removes the neg conversation.
        /// </summary>
        /// <param name="NegConv">The neg conv.</param>
        public void RemoveNegConversation(NegConversation NegConv)
        {

            if (this.Context.NegConversations.Contains(NegConv))
            {
                NegConv = this.Context.NegConversations.FirstOrDefault(s => s.NegConversationID == NegConv.NegConversationID);

                while (NegConv.ConversationMessages.Count > 0)
                {
                    RemoveConversationMessage(NegConv.ConversationMessages.FirstOrDefault());
                }

                this.Context.NegConversations.Remove(NegConv);
            }
        }

        /// <summary>
        /// Removes the conversation message.
        /// </summary>
        /// <param name="ConvMessage">The conv message.</param>
        public void RemoveConversationMessage(ConversationMessage ConvMessage)
        {
            if (this.Context.ConversationMessages.Contains(ConvMessage))
            {
                ConvMessage = this.Context.ConversationMessages.FirstOrDefault(s => s.NegConversationID == ConvMessage.NegConversationID);

                this.Context.ConversationMessages.Remove(ConvMessage);
            }
        }

        /// <summary>
        /// Removes the message issue.
        /// </summary>
        /// <param name="MsgIssue"></param>
        public void RemoveMessageIssue(MessageIssue MsgIssue)
        {

            if (this.Context.MessageIssues.Contains(MsgIssue))
            {
                MsgIssue = this.Context.MessageIssues.FirstOrDefault(s => s.MessageIssueID == MsgIssue.MessageIssueID);

                while (MsgIssue.MessageOptionIssues.Count > 0)
                {
                    RemoveMessageOptionIssue(MsgIssue.MessageOptionIssues.FirstOrDefault());
                }


                while (MsgIssue.MessageLaterRatedIssues.Count > 0)
                {
                    RemoveMessageLaterRatedIssue(MsgIssue.MessageLaterRatedIssues.FirstOrDefault());
                }

                this.Context.MessageIssues.Remove(MsgIssue);
            }
        }

        /// <summary>
        /// Removes the message option issue.
        /// </summary>
        /// <param name="MsgOptionIssue">The MSG option issue.</param>
        public void RemoveMessageOptionIssue(MessageOptionIssue MsgOptionIssue)
        {
            if (this.Context.MessageOptionIssues.Contains(MsgOptionIssue))
            {
                this.Context.MessageOptionIssues.Remove(MsgOptionIssue);
            }
        }

        /// <summary>
        /// Removes the message later rated issue.
        /// </summary>
        /// <param name="MsgLaterRatedIssue">The MSG later rated issue.</param>
        public void RemoveMessageLaterRatedIssue(MessageLaterRatedIssue MsgLaterRatedIssue)
        {
            if (this.Context.MessageLaterRatedIssues.Contains(MsgLaterRatedIssue))
            {
                this.Context.MessageLaterRatedIssues.Remove(MsgLaterRatedIssue);
            }
        }


        /// <summary>
        /// Save any pending changes asynchronously
        /// </summary>
        public void SaveChangesAsync()
        {
            if (SaveChangesComplete != null)
            {
                SaveChangesComplete(this, new SubmitOperationEventArgs(null, null));
            }
        }

        /// <summary>
        /// Reject any pending changes
        /// </summary>
        public void RejectChanges()
        {
            Context.RejectChanges();
        }


        #endregion

        #endregion

        MockMaster mMockMaster;

        public MockMaster CurrentMockMaster
        {
            get { return this.mMockMaster; }
            set { this.mMockMaster = value; }
        }

        public MockDataMatchingViewModel(MockMaster currentMockMaster)
        {
            this.CurrentMockMaster = currentMockMaster;
        }

    }
}
