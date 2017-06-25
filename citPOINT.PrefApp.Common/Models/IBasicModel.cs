
#region → Usings   .
using System;
using citPOINT.PrefApp.Data.Web;
using citPOINT.eNeg.Common;
using System.ComponentModel;
#endregion

#region → History  .

/* Date         User            Change
 * 
 * 27.03.12     Yousra Reda     creation
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
    /// Interface for the basic model
    /// </summary>
    public interface IBasicModel
    {
        #region → Properties     .
        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>The context.</value>
        PrefAppContext Context { get; }

        /// <summary>
        /// Gets a value indicating whether this instance has changes.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has changes; otherwise, <c>false</c>.
        /// </value>
        bool HasChanges { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is busy.
        /// </summary>
        /// <value><c>true</c> if this instance is busy; otherwise, <c>false</c>.</value>
        bool IsBusy { get; }
        #endregion Properties

        #region → Methods        .

        /// <summary>
        /// Cleans up.
        /// </summary>
        void CleanUp();

        /// <summary>
        /// Gets the conversation messages by conv ID async.
        /// </summary>
        /// <param name="NegConvID">The neg conv ID.</param>
        void GetConversationMessagesByConvIDsAsync(Guid[] NegConvID);

        /// <summary>
        /// Gets the current neg conversation by ID async.
        /// </summary>
        /// <param name="conversationID">The conversation ID.</param>
        void GetCurrentNegConversationByIDAsync(Guid conversationID);

        /// <summary>
        /// Gets the current preference set async.
        /// </summary>
        /// <param name="negotiationID">The negotiation ID.</param>
        void GetCurrentPreferenceSetAsync(Guid negotiationID);

        /// <summary>
        /// Gets the current pref set neg by ID async.
        /// </summary>
        /// <param name="negotiationID">The negotiation ID.</param>
        void GetCurrentPrefSetNegByIDAsync(Guid negotiationID);

        /// <summary>
        /// Gets the message issues async.
        /// </summary>
        /// <param name="msgIDs">The MSG I ds.</param>
        void GetMessageIssuesAsync(Guid[] msgIDs);

        /// <summary>
        /// Gets the message later rated issues async.
        /// </summary>
        /// <param name="msgIssueIDs">The MSG issue I ds.</param>
        void GetMessageLaterRatedIssuesAsync(Guid[] msgIssueIDs);

        /// <summary>
        /// Gets the message option issues async.
        /// </summary>
        /// <param name="msgIssueIDs">The MSG issue I ds.</param>
        void GetMessageOptionIssuesAsync(Guid[] msgIssueIDs);

        /// <summary>
        /// Gets the negotiation conversations by neg ID async.
        /// </summary>
        /// <param name="prefSetNegID">The pref set neg ID.</param>
        void GetNegotiationConversationsByNegIDAsync(Guid prefSetNegID);
        
        #endregion

        #region → Events         .

        /// <summary>
        /// Occurs when [get conversation messages complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<ConversationMessage>> GetConversationMessagesComplete;

        /// <summary>
        /// Occurs when [get current neg conversation by ID async completed].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<NegConversation>> GetCurrentNegConversationByIDAsyncCompleted;

        /// <summary>
        /// Occurs when [get current preference set completed].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<PreferenceSet>> GetCurrentPreferenceSetCompleted;

        /// <summary>
        /// Occurs when [get current pref set neg by ID async completed].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<PreferenceSetNeg>> GetCurrentPrefSetNegByIDAsyncCompleted;

        /// <summary>
        /// Occurs when [get message issues complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<MessageIssue>> GetMessageIssuesComplete;

        /// <summary>
        /// Occurs when [get message later rated issues complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<MessageLaterRatedIssue>> GetMessageLaterRatedIssuesComplete;

        /// <summary>
        /// Occurs when [get message option issues complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<MessageOptionIssue>> GetMessageOptionIssuesComplete;

        /// <summary>
        /// Occurs when [get negotiation conversations complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<NegConversation>> GetNegotiationConversationsComplete;

        /// <summary>
        /// Occurs when [property changed].
        /// </summary>
        event PropertyChangedEventHandler PropertyChanged;
        
        #endregion
    }
}
