

#region → Usings   .
using System;
using System.Collections.Generic;
using System.Linq;
using citPOINT.PrefApp.Common;
using citPOINT.PrefApp.Data.Web;
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
    /// Class for ServerEngineProvider 
    /// </summary>

    public class ServerEngineProvider : IEngineProvider
    {

        #region → Fields         .

        private PrefAppEntities mContext;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        /// <value>The context.</value>
        private PrefAppEntities Context { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is server side.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is server side; otherwise, <c>false</c>.
        /// </value>
        public bool IsServerSide
        {
            get { return true; }
        }

        #endregion Properties

        #region → Constractor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerEngineProvider"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public  ServerEngineProvider(PrefAppEntities context)
        {
            this.Context = context;
        }

        #endregion

        #region → Methods        .

        #region → Public         .

        /// <summary>
        /// Determines whether the specified preference set is checkvariation.
        /// </summary>
        /// <param name="preferenceSetID">The preference set ID.</param>
        /// <returns>
        /// 	<c>true</c> if the specified preference set is checkvariation; otherwise, <c>false</c>.
        /// </returns>
        public bool IsCheckvariation(Guid preferenceSetID)
        {
            PreferenceSet preferenceSet = GetPreferenceSet(preferenceSetID);

            return preferenceSet != null && preferenceSet.Checkvariation;
        }

        /// <summary>
        /// Gets the preference set.
        /// </summary>
        /// <param name="preferenceSetID">The preference set ID.</param>
        /// <returns></returns>
        public PreferenceSet GetPreferenceSet(Guid preferenceSetID)
        {
            return this.Context.PreferenceSets
                               .Where(ss => ss.PreferenceSetID == preferenceSetID && ss.Deleted == false)
                               .FirstOrDefault();
        }

        /// <summary>
        /// Gets the issue.
        /// </summary>
        /// <param name="issueID">The issue ID.</param>
        /// <returns></returns>
        public Issue GetIssue(Guid issueID)
        {
            return this.Context.Issues
                               .Where(ss => ss.IssueID == issueID && ss.Deleted == false)
                               .FirstOrDefault();
        }

        /// <summary>
        /// Gets the numeric issue.
        /// </summary>
        /// <param name="issueID">The issue ID.</param>
        /// <returns></returns>
        public NumericIssue GetNumericIssue(Guid issueID)
        {
            return this.Context.NumericIssues
                               .Where(ss => ss.IssueID == issueID && ss.Deleted == false)
                               .FirstOrDefault();
        }

        /// <summary>
        /// Gets the option issues.
        /// </summary>
        /// <param name="issueID">The issue ID.</param>
        /// <returns></returns>
        public IEnumerable<OptionIssue> GetOptionIssues(Guid issueID)
        {
            return this.Context.OptionIssues
                               .Where(ss => ss.IssueID == issueID && ss.Deleted == false)
                               .AsEnumerable();
        }

        /// <summary>
        /// Gets the later rated issues.
        /// </summary>
        /// <param name="issueID">The issue ID.</param>
        /// <returns></returns>
        public IEnumerable<LaterRatedIssue> GetLaterRatedIssues(Guid issueID)
        {
            return this.Context.LaterRatedIssues
                               .Where(ss => ss.IssueID == issueID && ss.Deleted == false)
                               .AsEnumerable();
        }

        /// <summary>
        /// Gets the preference set neg.
        /// </summary>
        /// <param name="preferenceSetNegID">The preference set neg ID.</param>
        /// <returns></returns>
        public PreferenceSetNeg GetPreferenceSetNeg(Guid preferenceSetNegID)
        {
            return this.Context.PreferenceSetNegs
                               .Where(ss => ss.PreferenceSetNegID == preferenceSetNegID && ss.Deleted == false)
                               .FirstOrDefault();
        }

        /// <summary>
        /// Gets the negotiation conversations.
        /// </summary>
        /// <param name="preferenceSetNegID">The preference set neg ID.</param>
        /// <returns></returns>
        public IEnumerable<NegConversation> GetNegotiationConversations(Guid preferenceSetNegID)
        {
            return this.Context.NegConversations
                               .Where(ss => ss.PreferenceSetNegID == preferenceSetNegID && ss.Deleted == false)
                               .AsEnumerable();
        }

        /// <summary>
        /// Gets the message neg conversation.
        /// </summary>
        /// <param name="negConversationID">The neg conversation ID.</param>
        /// <returns></returns>
        public NegConversation GetMessageNegConversation(Guid negConversationID)
        {
            return this.Context.NegConversations
                               .Where(ss => ss.NegConversationID == negConversationID && ss.Deleted == false)
                               .FirstOrDefault();
        }

        /// <summary>
        /// Gets the message issues.
        /// </summary>
        /// <param name="conversationMessageID">The conversation message ID.</param>
        /// <returns></returns>
        public IEnumerable<MessageIssue> GetMessageIssues(Guid conversationMessageID)
        {
            return this.Context.MessageIssues
                               .Where(ss => ss.ConversationMessageID == conversationMessageID && ss.Deleted == false)
                               .AsEnumerable();
        }

        /// <summary>
        /// Gets the message option issues.
        /// </summary>
        /// <param name="messageIssueID">The message issue ID.</param>
        /// <returns></returns>
        public IEnumerable<MessageOptionIssue> GetMessageOptionIssues(Guid messageIssueID)
        {
            return this.Context.MessageOptionIssues
                               .Where(ss => ss.MessageIssueID == messageIssueID && ss.Deleted == false)
                               .AsEnumerable();
        }

        /// <summary>
        /// Gets the message later rated issues.
        /// </summary>
        /// <param name="messageIssueID">The message issue ID.</param>
        /// <returns></returns>
        public IEnumerable<MessageLaterRatedIssue> GetMessageLaterRatedIssues(Guid messageIssueID)
        {
            return this.Context.MessageLaterRatedIssues
                               .Where(ss => ss.MessageIssueID == messageIssueID && ss.Deleted == false)
                               .AsEnumerable();
        }

        /// <summary>
        /// Gets the max numeric value.
        /// </summary>
        /// <param name="preferenceSetNegID">The preference set neg ID.</param>
        /// <param name="issueID">The issue ID.</param>
        /// <returns></returns>
        public decimal? GetMaxNumericValue(Guid preferenceSetNegID, Guid issueID)
        {
            return this.Context.GetMessageIssueSelectMaxMinValue(preferenceSetNegID, PrefAppConstant.IssueTypes.Numeric, issueID, true).FirstOrDefault();
        }

        /// <summary>
        /// Gets the min numeric value.
        /// </summary>
        /// <param name="preferenceSetNegID">The preference set neg ID.</param>
        /// <param name="issueID">The issue ID.</param>
        /// <returns></returns>
        public decimal? GetMinNumericValue(Guid preferenceSetNegID, Guid issueID)
        {
            return this.Context.GetMessageIssueSelectMaxMinValue(preferenceSetNegID, PrefAppConstant.IssueTypes.Numeric, issueID, false).FirstOrDefault();
        }

        /// <summary>
        /// Determines whether [is any numeric issue has better] [the specified preference set neg ID].
        /// </summary>
        /// <param name="preferenceSetNegID">The preference set neg ID.</param>
        /// <returns>
        /// 	<c>true</c> if [is any numeric issue has better] [the specified preference set neg ID]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsAnyNumericIssueHasBetter(Guid preferenceSetNegID)
        {

            MessageIssue messageIssue = this.Context.MessageIssues
                                 .Where(s => s.ConversationMessage != null //has a message
                                         && s.ConversationMessage.NegConversation != null //Has a negotiation
                                         && s.ConversationMessage.NegConversation.PreferenceSetNegID == preferenceSetNegID //In same Negotiation
                                         && s.Issue != null
                                         && s.Issue.IssueTypeID == PrefAppConstant.IssueTypes.Numeric //Numeric Type
                                         && s.Deleted == false  //Not Deleted
                                         && s.Issue.NumericIssues.FirstOrDefault() != null
                                         && (s.Issue.NumericIssues.FirstOrDefault().MaximumOperator == (int)Operators.Better ||
                                             s.Issue.NumericIssues.FirstOrDefault().MinimumOperator == (int)Operators.Better)
                                         )
                                   .FirstOrDefault();

            return messageIssue != null;

        }

        /// <summary>
        /// Gets all messages for negotiation.
        /// </summary>
        /// <param name="PreferenceSetNegID">The preference set neg ID.</param>
        /// <returns></returns>
        public IEnumerable<ConversationMessage> GetAllMessagesForNegotiation(Guid PreferenceSetNegID)
        {
            return this.Context.ConversationMessages
                                .Where(s => s.NegConversation.PreferenceSetNegID == PreferenceSetNegID &&
                                            s.Deleted == false &&
                                            s.MessageIssues.Count() > 0);
        }

        /// <summary>
        /// Gets all messages for negotiation but has error.
        /// </summary>
        /// <param name="PreferenceSetNegID">The preference set neg ID.</param>
        /// <returns></returns>
        public IEnumerable<ConversationMessage> GetAllMessagesForNegotiationButHasError(Guid PreferenceSetNegID)
        {
            return this.Context.ConversationMessages
                                                    .Where(s => s.NegConversation.PreferenceSetNegID == PreferenceSetNegID &&
                                                                s.MessageIssues.Count() == 0 &&
                                                                s.Deleted == false &&
                                                                s.Percentage != null);
        }

        /// <summary>
        /// Gets the neg conversations for preference set neg.
        /// </summary>
        /// <param name="PreferenceSetNegID">The preference set neg ID.</param>
        /// <returns></returns>
        public IEnumerable<NegConversation> GetNegConversationsForPreferenceSetNeg(Guid PreferenceSetNegID)
        {
            return this.Context.NegConversations
                               .Where(s => s.PreferenceSetNegID == PreferenceSetNegID &&
                                           s.Deleted == false);
        }

        /// <summary>
        /// Updates the conversation and negotiation PCT.
        /// by setting the Conversation with the last rated message
        /// and the Negotiation with the hightest message.
        /// </summary>
        /// <param name="currentMessage">The Current message.</param>
        public void UpdateConversationAndNegotiationPCT(ConversationMessage currentMessage)
        {
            //Delayed untill saving completed. 
        }

        /// <summary>
        /// Updates the negotiation by best message.
        /// </summary>
        /// <param name="crrentMessage">Current Message</param>
        public void UpdateNegotiationByBestMessage(ConversationMessage crrentMessage)
        {
            //No need in server side
        }
              
        #endregion  Public

        #endregion Methods
    }
}
