
#region → Usings   .
using System;
using System.ComponentModel;
using citPOINT.PrefApp.Data.Web;
using citPOINT.eNeg.Common;
using System.ServiceModel.DomainServices.Client;
#endregion

#region → History  .

/* Date         User            Change
 * 
 * 10.01.11     M.Wahab         • creation
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

namespace citPOINT.PrefApp.Common
{
    /// <summary>
    /// Interface for  Data Matching Model
    /// </summary>
    public interface IDataMatchingModel : INotifyPropertyChanged
    {
        #region → Properties     .

        /// <summary>
        /// True if Domain context Has Changes ;otherwise false
        /// </summary>
        bool HasChanges { get; }

        /// <summary>
        /// True if either "IsLoading" or "IsSubmitting" is
        /// in progress; otherwise, false
        /// </summary>
        bool IsBusy { get; }

        /// <summary>
        /// Gets or sets the max query per call.
        /// </summary>
        /// <value>The max query per call.</value>
        int MaxQueryPerCall { get; set; }

        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>The context.</value>
        PrefAppContext Context { get; }
        #endregion Properties

        #region → Events         .

        /// <summary>
        /// Occurs when [retrieve application DM status completed].
        /// </summary>
        event Action<InvokeOperation<bool>> RetrieveApplicationDMStatusCompleted;

        /// <summary>
        /// Occurs when [update data matching status in addon completed].
        /// </summary>
        event Action<InvokeOperation<bool>> UpdateDataMatchingStatusInAddonCompleted;

        /// <summary>
        /// Get All Preference Set Negotiations CallBack 
        /// </summary>
        event EventHandler<eNegEntityResultArgs<PreferenceSetNeg>> GetPreferenceSetNegotiationsComplete;

        /// <summary>
        /// Get All Negotiations Conversations CallBack
        /// </summary>
        event EventHandler<eNegEntityResultArgs<NegConversation>> GetNegotiationConversationsComplete;

        /// <summary>
        /// Occurs when [get conversation messages complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<ConversationMessage>> GetConversationMessagesComplete;




        /// <summary>
        /// Occurs when [get message issues complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<MessageIssue>> GetMessageIssuesComplete;

        /// <summary>
        /// Occurs when [get message option issues complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<MessageOptionIssue>> GetMessageOptionIssuesComplete;

        /// <summary>
        /// Occurs when [get message later rated issues complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<MessageLaterRatedIssue>> GetMessageLaterRatedIssuesComplete;




        #region → From eNeg by Rest Protocol

        /// <summary>
        /// Occurs when [get available negotiations to analysis complete].
        /// In case of Add New
        /// </summary>
        event EventHandler<eNegEntityResultArgs<Negotiation>> GetAvailableNegotiationsToAnalysisComplete;

        /// <summary>
        /// Occurs when [get negotiations details by I ds complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<Negotiation>> GetNegotiationsDetailsByIDsComplete;

        /// <summary>
        /// Occurs when [get conversations details by I ds complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<Conversation>> GetConversationsDetailsByIDsComplete;

        /// <summary>
        /// Occurs when [get messages details by I ds complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<Message>> GetMessagesDetailsByIDsComplete;

        #endregion

        /// <summary>
        /// SaveChangesComplete
        /// </summary>
        event EventHandler<SubmitOperationEventArgs> SaveChangesComplete;

        #endregion Events

        #region → Methods        .

        /// <summary>
        /// Retrieves the application DM status async.
        /// </summary>
        /// <param name="AppName">Name of the app.</param>
        /// <param name="UserID">The user ID.</param>
        void RetrieveApplicationDMStatusAsync(string AppName, Guid UserID);

        /// <summary>
        /// Updates the data matching status in addon async.
        /// </summary>
        /// <param name="AppName">Name of the app.</param>
        /// <param name="UserID">The user ID.</param>
        /// <param name="IsActive">if set to <c>true</c> [is active].</param>
        void UpdateDataMatchingStatusInAddonAsync(string AppName, Guid UserID, bool IsActive);

        /// <summary>
        /// Get Preference Set Negotiations for the Current login User.
        /// </summary>
        void GetPreferenceSetNegotiationsAsync();

        /// <summary>
        /// Get Negotiation Conversation Async.
        /// </summary>
        void GetNegotiationConversationsAsync();

        /// <summary>
        /// Get Conversation Messages Async.
        /// </summary>
        void GetConversationMessagesAsync();

        /// <summary>
        /// Gets the message issues async.
        /// </summary>
        void GetMessageIssuesAsync();

        /// <summary>
        /// Gets the message option issues async.
        /// </summary>
        void GetMessageOptionIssuesAsync();

        /// <summary>
        /// Gets the message later rated issues async.
        /// </summary>
        void GetMessageLaterRatedIssuesAsync();




        /// <summary>
        /// Gets the available negotiations to analysis async.
        /// </summary>
        void GetAvailableNegotiationsToAnalysisAsync();


        /// <summary>
        /// Gets the negotiations details by I ds async.
        /// </summary>
        /// <param name="NegotiationIDs">The negotiation I ds.</param>
        void GetNegotiationsDetailsByIDsAsync(Guid[] NegotiationIDs);

        /// <summary>
        /// Gets the conversations details by I ds async.
        /// </summary>
        /// <param name="NegotiationIDs">The negotiation I ds.</param>
        void GetConversationsDetailsByIDsAsync(Guid[] NegotiationIDs);


        /// <summary>
        /// Gets the messages details by I ds async.
        /// </summary>
        /// <param name="NegotiationIDs">The negotiation I ds.</param>
        void GetMessagesDetailsByIDsAsync(Guid[] NegotiationIDs);




        /// <summary>
        /// Adds the preference set neg.
        /// </summary>
        /// <param name="SetInContext">if set to <c>true</c> [set in context].</param>
        /// <param name="Prefset">The prefset.</param>
        /// <param name="NegotiationID">The negotiation ID.</param>
        /// <param name="NegotiationName">Name of the negotiation.</param>
        /// <returns></returns>
        PreferenceSetNeg AddPreferenceSetNeg(bool SetInContext, PreferenceSet Prefset, Guid NegotiationID, string NegotiationName);

        /// <summary>
        /// Adds the neg conversation.
        /// </summary>
        /// <param name="SetInContext">if set to <c>true</c> [set in context].</param>
        /// <param name="PrefSetNeg">The pref set neg.</param>
        /// <param name="ConvID">The conv ID.</param>
        /// <param name="ConversationName">Name of the conversation.</param>
        /// <returns></returns>
        NegConversation AddNegConversation(bool SetInContext, PreferenceSetNeg PrefSetNeg, Guid ConvID, string ConversationName);
        /// <summary>
        /// Adds the conversation message.
        /// </summary>
        /// <param name="SetInContext">if set to <c>true</c> [set in context].</param>
        /// <param name="NegConv">The neg conv.</param>
        /// <param name="MsgID">The MSG ID.</param>
        /// <returns></returns>
        ConversationMessage AddConversationMessage(bool SetInContext, NegConversation NegConv, Guid MsgID);

        /// <summary>
        /// Adds the message issue.
        /// </summary>
        /// <param name="SetInContext">if set to <c>true</c> [set in context].</param>
        /// <param name="ConvMessage">The conv message.</param>
        /// <param name="issue">The issue.</param>
        /// <returns></returns>
        MessageIssue AddMessageIssue(bool SetInContext, ConversationMessage ConvMessage, Issue issue);

        /// <summary>
        /// Adds the message option issue.
        /// </summary>
        /// <param name="SetInContext">if set to <c>true</c> [set in context].</param>
        /// <param name="MsgIssue">The MSG issue.</param>
        /// <param name="optionIssue">The option issue.</param>
        /// <returns></returns>
        MessageOptionIssue AddMessageOptionIssue(bool SetInContext, MessageIssue MsgIssue, OptionIssue optionIssue);

        /// <summary>
        /// Adds the message later rated issue.
        /// </summary>
        /// <param name="SetInContext">if set to <c>true</c> [set in context].</param>
        /// <param name="MsgIssue">The MSG issue.</param>
        /// <param name="laterRatedIssue">The later rated issue.</param>
        /// <returns></returns>
        MessageLaterRatedIssue AddMessageLaterRatedIssue(bool SetInContext, MessageIssue MsgIssue, LaterRatedIssue laterRatedIssue);

        //MessageIssue AddMessageIssue(bool SetInContext, ConversationMessage ConvMessage, Guid IssueID);


        /// <summary>
        /// Removes the preference set neg.
        /// </summary>
        /// <param name="PrefSetNeg">The pref set neg.</param>
        void RemovePreferenceSetNeg(PreferenceSetNeg PrefSetNeg);
        /// <summary>
        /// Removes the neg conversation.
        /// </summary>
        /// <param name="NegConv">The neg conv.</param>
        void RemoveNegConversation(NegConversation NegConv);
        /// <summary>
        /// Removes the conversation message.
        /// </summary>
        /// <param name="ConvMessage">The conv message.</param>
        void RemoveConversationMessage(ConversationMessage ConvMessage);

        /// <summary>
        /// Removes the message issue.
        /// </summary>
        /// <param name="MsgIssue">The MSG issue.</param>
        void RemoveMessageIssue(MessageIssue MsgIssue);

        /// <summary>
        /// Removes the message option issue.
        /// </summary>
        /// <param name="MsgOptionIssue">The MSG option issue.</param>
        void RemoveMessageOptionIssue(MessageOptionIssue MsgOptionIssue);

        /// <summary>
        /// Removes the message later rated issue.
        /// </summary>
        /// <param name="MsgLaterRatedIssue">The MSG later rated issue.</param>
        void RemoveMessageLaterRatedIssue(MessageLaterRatedIssue MsgLaterRatedIssue);

        /// <summary>
        /// Save any pending changes asynchronously
        /// </summary>
        void SaveChangesAsync();

        /// <summary>
        /// Reject any pending changes
        /// </summary>
        void RejectChanges();

        #endregion Methods


    }
}
