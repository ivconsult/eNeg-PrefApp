#region → Usings   .
using citPOINT.eNeg.Common;
using citPOINT.PrefApp.Common;
using citPOINT.PrefApp.Data.Web;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel.DomainServices.Client;
using citPOINT.PrefApp.Common.Helper;

#endregion

#region → History  .

/* Date         User              Change
 * 
 * 10.01.11     M.Wahab           • Creation
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

namespace citPOINT.PrefApp.Model
{

    #region Using MEF to export Data Matching ViewModel

    /// <summary>
    /// Data Matching Model.
    /// </summary>
    [Export(typeof(IDataMatchingModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    #endregion
    public class DataMatchingModel : IDataMatchingModel
    {

        #region → Fields         .

        private Boolean mHasChanges = false;
        private Boolean mIsBusy = false;
        
        /// <summary>
        /// Gets or sets the max query per call.
        /// </summary>
        /// <value>The max query per call.</value>
        public int MaxQueryPerCall { get { return 50; } set { } }
        
        #endregion

        #region → Properties     .

        /// <summary>
        /// Context of Service eNegService
        /// </summary>
        public PrefAppContext Context
        {
            get
            {
                return Repository.Context;
            }
        }

        /// <summary>
        /// True if Domain context Has Changes ;otherwise false
        /// </summary>
        public Boolean HasChanges
        {
            get
            {
                return this.mHasChanges;
            }
            private set
            {


                this.mHasChanges = value;
                this.OnPropertyChanged("HasChanges");

            }
        }

        /// <summary>
        /// True if either "IsLoading" or "IsSubmitting" is
        /// in progress; otherwise, false
        /// </summary>
        public Boolean IsBusy
        {
            get
            {
                return this.mIsBusy;
            }

            private set
            {
                this.mIsBusy = value;
                this.OnPropertyChanged("IsBusy");

            }
        }

        #endregion

        #region → Contructor     .
        
        /// <summary>
        /// Initializes a new instance of the <see cref="DataMatchingModel"/> class.
        /// </summary>
        public DataMatchingModel()
        {
            Repository.Context.PropertyChanged += new PropertyChangedEventHandler(mPrefAppContext_PropertyChanged);
        }

        #endregion
        
        #region → Event Handlers .

        /// <summary>
        /// Executed when any property of Domain context is changed
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">PropertyChangedEventArgs</param>
        private void mPrefAppContext_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "HasChanges":
                    this.HasChanges = this.Context.HasChanges;
                    break;
                case "IsLoading":
                    this.IsBusy = this.Context.IsLoading;
                    break;
                case "IsSubmitting":
                    this.IsBusy = this.Context.IsSubmitting;
                    break;
            }
        }
        #endregion

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
        /// PropertyChanged Callback
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Get All Preference Set Negotiations CallBack 
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<PreferenceSetNeg>> GetPreferenceSetNegotiationsComplete;

        /// <summary>
        /// Get All Negotiations Conversations CallBack
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

        #region → From eNeg by Rest Protocol

        /// <summary>
        /// Occurs when [get available negotiations to analysis complete].
        /// In case of Add New
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

        #endregion

        /// <summary>
        /// SaveChangesComplete
        /// </summary>
        public event EventHandler<SubmitOperationEventArgs> SaveChangesComplete;
        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Private Method used to perform query on certain entity of PrefApp Entities
        /// </summary>
        /// <typeparam name="T">Value Of T</typeparam>
        /// <param name="qry">Value Of qry</param>
        /// <param name="evt">Value Of evt</param>
        private void PerformQuery<T>(EntityQuery<T> qry, EventHandler<eNegEntityResultArgs<T>> evt) where T : Entity
        {

            Context.Load<T>(qry, LoadBehavior.RefreshCurrent, r =>
            {
                if (evt != null)
                {
                    try
                    {
                        if (r.HasError)
                        {
                            evt(this, new eNegEntityResultArgs<T>(r.Error));
                            r.MarkErrorAsHandled();
                        }
                        else
                        {
                            evt(this, new eNegEntityResultArgs<T>(r.Entities));
                        }
                    }
                    catch (Exception ex)
                    {
                        evt(this, new eNegEntityResultArgs<T>(ex));
                    }
                }
            }, null);
        }

        #endregion

        #region → Protected      .


        #region "INotifyPropertyChanged Interface implementation"

        /// <summary>
        /// called On Property Changed
        /// </summary>
        /// <param name="propertyName">property name</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion "INotifyPropertyChanged Interface implementation"

        #endregion

        #region → Public         .

        #region IDataMatchingModel Interface Implementation

        /// <summary>
        /// Retrieves the application DM status async.
        /// </summary>
        /// <param name="AppName">Name of the app.</param>
        /// <param name="UserID">The user ID.</param>
        public void RetrieveApplicationDMStatusAsync(string AppName, Guid UserID)
        {
            this.Context.RetrieveApplicationDMStatus(AppName, UserID, RetrieveApplicationDMStatusCompleted, null);
        }

        /// <summary>
        /// Updates the data matching status in addon async.
        /// </summary>
        /// <param name="AppName">Name of the app.</param>
        /// <param name="UserID">The user ID.</param>
        /// <param name="IsActive">if set to <c>true</c> [is active].</param>
        public void UpdateDataMatchingStatusInAddonAsync(string AppName, Guid UserID, bool IsActive)
        {
            this.Context.UpdateDataMatchingStatusInAddon(AppName, UserID, IsActive, UpdateDataMatchingStatusInAddonCompleted, null);
        }

        /// <summary>
        /// Get Preference Set Negotiations for the Current login User.
        /// </summary>
        public void GetPreferenceSetNegotiationsAsync()
        {
            PerformQuery<PreferenceSetNeg>(Context.GetPreferenceSetNegsQuery().Where(s => s.Deleted == false && s.DeletedBy == PrefAppConfigurations.CurrentLoginUser.UserID), GetPreferenceSetNegotiationsComplete);
        }

        /// <summary>
        /// Get Negotiation Conversation Async.
        /// </summary>
        public void GetNegotiationConversationsAsync()
        {
            PerformQuery<NegConversation>(Context.GetNegConversationsQuery()
                                                    .Where(s => s.Deleted == false && s.DeletedBy == PrefAppConfigurations.CurrentLoginUser.UserID)
                                                    .OrderBy(s => s.DeletedOn), GetNegotiationConversationsComplete);
        }

        /// <summary>
        /// Get Conversation Messages Async.
        /// </summary>
        public void GetConversationMessagesAsync()
        {
            PerformQuery<ConversationMessage>(Context.GetConversationMessagesQuery().Where(s => s.Deleted == false && s.DeletedBy == PrefAppConfigurations.CurrentLoginUser.UserID), GetConversationMessagesComplete);
        }

        /// <summary>
        /// Gets the message issues async.
        /// </summary>
        public void GetMessageIssuesAsync()
        {
            PerformQuery<MessageIssue>(Context.GetMessageIssuesQuery().Where(s => s.Deleted == false && s.DeletedBy == PrefAppConfigurations.CurrentLoginUser.UserID), GetMessageIssuesComplete);
        }

        /// <summary>
        /// Gets the message option issues async.
        /// </summary>
        public void GetMessageOptionIssuesAsync()
        {
            PerformQuery<MessageOptionIssue>(Context.GetMessageOptionIssuesQuery().Where(s => s.Deleted == false && s.DeletedBy == PrefAppConfigurations.CurrentLoginUser.UserID), GetMessageOptionIssuesComplete);
        }

        /// <summary>
        /// Gets the message later rated issues async.
        /// </summary>
        public void GetMessageLaterRatedIssuesAsync()
        {
            PerformQuery<MessageLaterRatedIssue>(Context.GetMessageLaterRatedIssuesQuery().Where(s => s.Deleted == false && s.DeletedBy == PrefAppConfigurations.CurrentLoginUser.UserID), GetMessageLaterRatedIssuesComplete);
        }

        #region → From eNeg by Rest Protocol

        /// <summary>
        /// Gets the available negotiations to analysis async.
        /// </summary>
        public void GetAvailableNegotiationsToAnalysisAsync()
        {
            PerformQuery<Negotiation>(Context.GetAvailableNegotiationsToAnalysisQuery(PrefAppConfigurations.CurrentLoginUser.UserID, PrefAppConfigurations.AppName), GetAvailableNegotiationsToAnalysisComplete);
        }

        /// <summary>
        /// Gets the negotiations details by Ids async.
        /// </summary>
        /// <param name="NegotiationIDs">The negotiation I ds.</param>
        public void GetNegotiationsDetailsByIDsAsync(Guid[] NegotiationIDs)
        {

            //Retriving e.g. 5 By 5 due to the Limitation of URL Lenght to 2083 Charatcer.
            List<Guid> TmpNegotiationIDs = new List<Guid>();

            for (int i = 0; i < NegotiationIDs.Count(); i++)
            {
                TmpNegotiationIDs.Add(NegotiationIDs[i]);

                if (((i + 1) % MaxQueryPerCall == 0) || i == NegotiationIDs.Count() - 1)
                {
                    PerformQuery<Negotiation>(Context.GetNegotiationsByListOfIDsQuery(TmpNegotiationIDs.ToArray()), GetNegotiationsDetailsByIDsComplete);
                    TmpNegotiationIDs.Clear();
                }
            }
        }

        /// <summary>
        /// Gets the conversations details by Ids async.
        /// </summary>
        /// <param name="NegotiationIDs">The negotiation I ds.</param>
        public void GetConversationsDetailsByIDsAsync(Guid[] NegotiationIDs)
        {

            //Retriving e.g. 5 By 5 due to the Limitation of URL Lenght to 2083 Charatcer.
            List<Guid> TmpNegotiationIDs = new List<Guid>();

            for (int i = 0; i < NegotiationIDs.Count(); i++)
            {
                TmpNegotiationIDs.Add(NegotiationIDs[i]);

                if (((i + 1) % MaxQueryPerCall == 0) || i == NegotiationIDs.Count() - 1)
                {
                    PerformQuery<Conversation>(Context.GetConversationsByNegotiationIDQuery(TmpNegotiationIDs.ToArray()), GetConversationsDetailsByIDsComplete);
                    TmpNegotiationIDs.Clear();
                }
            }
        }


        /// <summary>
        /// Gets the messages details by Ids async.
        /// </summary>
        /// <param name="NegotiationIDs">The negotiation I ds.</param>
        public void GetMessagesDetailsByIDsAsync(Guid[] NegotiationIDs)
        {
            //Retriving e.g. 5 By 5 due to the Limitation of URL Lenght to 2083 Charatcer.
            List<Guid?> TmpNegotiationIDs = new List<Guid?>();

            for (int i = 0; i < NegotiationIDs.Count(); i++)
            {
                TmpNegotiationIDs.Add(NegotiationIDs[i]);

                if (((i + 1) % MaxQueryPerCall == 0) || i == NegotiationIDs.Count() - 1)
                {
                    PerformQuery<Message>(Context.GetMessagesByNegotiationIDQuery(TmpNegotiationIDs.ToArray()), GetMessagesDetailsByIDsComplete);
                    TmpNegotiationIDs.Clear();
                }
            }
        }

        #endregion

        /// <summary>
        /// Adds the preference set neg.
        /// </summary>
        /// <param name="SetInContext">if set to <c>true</c> [set in context].</param>
        /// <param name="Prefset">The prefset.</param>
        /// <param name="NegotiationID">The negotiation ID.</param>
        /// <param name="NegotiationName">Name of the negotiation.</param>
        /// <returns></returns>
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
        /// <returns></returns>
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
        /// <returns></returns>
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
                DeletedBy = PrefAppConfigurations.CurrentLoginUser.UserID,
                IsExceedVariation = false
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
        /// <returns></returns>
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
        /// <returns></returns>
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
        /// <returns></returns>
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
                ConvMessage = this.Context.ConversationMessages.FirstOrDefault(s => s.ConversationMessageID == ConvMessage.ConversationMessageID);


                while (ConvMessage.MessageIssues.Count() > 0)
                {
                    RemoveMessageIssue(ConvMessage.MessageIssues.First());
                }

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
        /// Save changes asynchronously
        /// </summary>
        public void SaveChangesAsync()
        {
            this.Context.SubmitChanges(s =>
            {
                if (SaveChangesComplete != null)
                {
                    try
                    {
                        Exception ex = null;
                        if (s.HasError)
                        {
                            ex = s.Error;
                            s.MarkErrorAsHandled();
                        }
                        SaveChangesComplete(this, new SubmitOperationEventArgs(s, ex));
                    }
                    catch (Exception ex)
                    {
                        SaveChangesComplete(this, new SubmitOperationEventArgs(ex));
                    }
                }
            }, null);
        }

        /// <summary>
        /// Reject any pending changes
        /// </summary>
        public void RejectChanges()
        {
            this.Context.RejectChanges();
        }

        #endregion IPrefernceSetsModel Interface Implementation

        #endregion

        #endregion

    }
}
