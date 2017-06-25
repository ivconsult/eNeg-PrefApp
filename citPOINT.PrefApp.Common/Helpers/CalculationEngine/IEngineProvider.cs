

#region → Usings   .
using System;
using citPOINT.PrefApp.Data.Web;
using System.Collections.Generic;
#endregion

#region → History  .

/* 
 * Date         User            Change
 * *********************************************
 * 06/06/2011   mwahab         • creation
 * **********************************************
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
    /// 
    /// </summary>
    public interface IEngineProvider
    {
        #region → Properties     .

        /// <summary>
        /// Gets or sets a value indicating whether this instance is server side.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is server side; otherwise, <c>false</c>.
        /// </value>
        bool IsServerSide { get; }

        #endregion

        #region → Methods        .

        /// <summary>
        /// Determines whether the specified preference set is checkvariation.
        /// </summary>
        /// <param name="preferenceSetID">The preference set ID.</param>
        /// <returns>
        /// 	<c>true</c> if the specified preference set is checkvariation; otherwise, <c>false</c>.
        /// </returns>
        bool IsCheckvariation(Guid preferenceSetID);

        /// <summary>
        /// Gets the preference set.
        /// </summary>
        /// <param name="preferenceSetID">The preference set ID.</param>
        /// <returns></returns>
        PreferenceSet GetPreferenceSet(Guid preferenceSetID);

        /// <summary>
        /// Gets the issue.
        /// </summary>
        /// <param name="issueID">The issue ID.</param>
        /// <returns></returns>
        Issue GetIssue(Guid issueID);

        /// <summary>
        /// Gets the numeric issue.
        /// </summary>
        /// <param name="issueID">The issue ID.</param>
        /// <returns></returns>
        NumericIssue GetNumericIssue(Guid issueID);

        /// <summary>
        /// Gets the option issues.
        /// </summary>
        /// <param name="issueID">The issue ID.</param>
        /// <returns></returns>
        IEnumerable<OptionIssue> GetOptionIssues(Guid issueID);

        /// <summary>
        /// Gets the later rated issues.
        /// </summary>
        /// <param name="issueID">The issue ID.</param>
        /// <returns></returns>
        IEnumerable<LaterRatedIssue> GetLaterRatedIssues(Guid issueID);

        /// <summary>
        /// Gets the preference set neg.
        /// </summary>
        /// <param name="preferenceSetNegID">The preference set neg ID.</param>
        /// <returns></returns>
        PreferenceSetNeg GetPreferenceSetNeg(Guid preferenceSetNegID);

        /// <summary>
        /// Gets the message neg conversation.
        /// </summary>
        /// <param name="negConversationID">The neg conversation ID.</param>
        /// <returns></returns>
        NegConversation GetMessageNegConversation(Guid negConversationID);

        /// <summary>
        /// Gets the negotiation conversations.
        /// </summary>
        /// <param name="preferenceSetNegID">The preference set neg ID.</param>
        /// <returns></returns>
        IEnumerable<NegConversation> GetNegotiationConversations(Guid preferenceSetNegID);

        /// <summary>
        /// Gets the message issues.
        /// </summary>
        /// <param name="conversationMessageID">The conversation message ID.</param>
        /// <returns></returns>
        IEnumerable<MessageIssue> GetMessageIssues(Guid conversationMessageID);

        /// <summary>
        /// Gets the message option issues.
        /// </summary>
        /// <param name="messageIssueID">The message issue ID.</param>
        /// <returns></returns>
        IEnumerable<MessageOptionIssue> GetMessageOptionIssues(Guid messageIssueID);

        /// <summary>
        /// Gets the message later rated issues.
        /// </summary>
        /// <param name="messageIssueID">The message issue ID.</param>
        /// <returns></returns>
        IEnumerable<MessageLaterRatedIssue> GetMessageLaterRatedIssues(Guid messageIssueID);

        /// <summary>
        /// Gets the max numeric value.
        /// </summary>
        /// <param name="preferenceSetNegID">The preference set neg ID.</param>
        /// <param name="issueID">The issue ID.</param>
        /// <returns></returns>
        decimal? GetMaxNumericValue(Guid preferenceSetNegID, Guid issueID);

        /// <summary>
        /// Gets the min numeric value.
        /// </summary>
        /// <param name="preferenceSetNegID">The preference set neg ID.</param>
        /// <param name="issueID">The issue ID.</param>
        /// <returns></returns>
        decimal? GetMinNumericValue(Guid preferenceSetNegID, Guid issueID);

        /// <summary>
        /// Determines whether [is any numeric issue has better] [the specified preference set neg ID].
        /// </summary>
        /// <param name="preferenceSetNegID">The preference set neg ID.</param>
        /// <returns>
        /// 	<c>true</c> if [is any numeric issue has better] [the specified preference set neg ID]; otherwise, <c>false</c>.
        /// </returns>
        bool IsAnyNumericIssueHasBetter(Guid preferenceSetNegID);

        /// <summary>
        /// Gets all messages for negotiation.
        /// </summary>
        /// <param name="PreferenceSetNegID">The preference set neg ID.</param>
        /// <returns></returns>
        IEnumerable<ConversationMessage> GetAllMessagesForNegotiation(Guid PreferenceSetNegID);

        /// <summary>
        /// Gets all messages for negotiation but has error.
        /// </summary>
        /// <param name="PreferenceSetNegID">The preference set neg ID.</param>
        /// <returns></returns>
        IEnumerable<ConversationMessage> GetAllMessagesForNegotiationButHasError(Guid PreferenceSetNegID);

        /// <summary>
        /// Gets the neg conversations for preference set neg.
        /// </summary>
        /// <param name="PreferenceSetNegID">The preference set neg ID.</param>
        /// <returns></returns>
        IEnumerable<NegConversation> GetNegConversationsForPreferenceSetNeg(Guid PreferenceSetNegID);

        /// <summary>
        /// Updates the conversation and negotiation PCT.
        /// by setting the Conversation with the last rated message
        /// and the Negotiation with the hightest message.
        /// </summary>
        /// <param name="currentMessage">The Current message.</param>
        void UpdateConversationAndNegotiationPCT(ConversationMessage currentMessage);

        /// <summary>
        /// Updates the negotiation by best message.
        /// </summary>
        void UpdateNegotiationByBestMessage(ConversationMessage crrentMessage);

        #endregion
    }
}
