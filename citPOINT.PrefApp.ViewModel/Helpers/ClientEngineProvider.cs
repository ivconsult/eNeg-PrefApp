

#region → Usings   .
using System;
using System.ComponentModel;
using System.Linq;
using System.Collections.Generic;
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
    /// Class for ClientEngineProvider 
    /// </summary>
    public class ClientEngineProvider : IEngineProvider
    {

        #region → Fields         .

        private PrefAppContext mContext;

        #endregion

        #region → Properties     .


        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        /// <value>The context.</value>
        private PrefAppContext Context { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is server side.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is server side; otherwise, <c>false</c>.
        /// </value>
        public bool IsServerSide
        {
            get
            {
                return false;
            }
        }

        #endregion Properties

        #region → Constractor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientEngineProvider"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public ClientEngineProvider(PrefAppContext context)
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
            MessageIssue messageIssue
                = this.Context.MessageIssues
                              .Where(s => s.ConversationMessage != null //has a message
                                       && s.ConversationMessage.NegConversation != null //Has a negotiation
                                       && s.ConversationMessage.NegConversation.PreferenceSetNegID == preferenceSetNegID //In same Negotiation
                                       && s.Issue != null //this issue still exist and not deleted
                                       && s.Issue.IssueTypeID == PrefAppConstant.IssueTypes.Numeric //Numeric Type
                                       && s.Deleted == false  //Not Deleted
                                       && s.IssueID == issueID) //Values related to Current Numeric Issue
                              .OrderByDescending(dd => Convert.ToDecimal(dd.Value))
                              .FirstOrDefault();

            if (messageIssue != null && !string.IsNullOrEmpty(messageIssue.Value))
            {
                return decimal.Parse(messageIssue.Value);
            }

            return null;
        }

        /// <summary>
        /// Gets the min numeric value.
        /// </summary>
        /// <param name="preferenceSetNegID">The preference set neg ID.</param>
        /// <param name="issueID">The issue ID.</param>
        /// <returns></returns>
        public decimal? GetMinNumericValue(Guid preferenceSetNegID, Guid issueID)
        {
            MessageIssue messageIssue
                = this.Context.MessageIssues
                              .Where(s => s.ConversationMessage != null //has a message
                                       && s.ConversationMessage.NegConversation != null //Has a negotiation
                                       && s.ConversationMessage.NegConversation.PreferenceSetNegID == preferenceSetNegID //In same Negotiation
                                       && s.Issue != null //this issue still exist and not deleted
                                       && s.Issue.IssueTypeID == PrefAppConstant.IssueTypes.Numeric //Numeric Type
                                       && s.Deleted == false  //Not Deleted
                                       && s.IssueID == issueID) //Values related to Current Numeric Issue
                              .OrderBy(dd => Convert.ToDecimal(dd.Value))
                              .FirstOrDefault();

            if (messageIssue != null && !string.IsNullOrEmpty(messageIssue.Value))
            {
                return decimal.Parse(messageIssue.Value);
            }

            return null;
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
                               .Where(s => s.NegConversation != null &&
                                           s.NegConversation.PreferenceSetNegID == PreferenceSetNegID &&
                                           s.MessageIssues.Count() == 0 &&
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
                               .Where(s => s.PreferenceSetNegID == PreferenceSetNegID);
        }

        /// <summary>
        /// Updates the conversation and negotiation PCT.
        /// by setting the Conversation with the last rated message
        /// and the Negotiation with the hightest message.
        /// </summary>
        /// <param name="currentMessage">The message.</param>
        public void UpdateConversationAndNegotiationPCT(ConversationMessage currentMessage)
        {

            NegConversation currentMessageConversation = currentMessage.NegConversation;

            PreferenceSetNeg currentMessagePreferenceSetNeg = currentMessage.NegConversation.PreferenceSetNeg;

            #region → Updating the Negotiation by Max Percentage of All Messages .

            if (currentMessagePreferenceSetNeg != null)
            {
                IEnumerable<ConversationMessage> AllMessagesForNegotiation
                                          = this.Context.ConversationMessages
                                                .Where(s => s.NegConversation.PreferenceSetNegID == currentMessagePreferenceSetNeg.PreferenceSetNegID
                                                            && s.MessageIssues.Count() > 0).OrderByDescending(s => s.Percentage);

                ConversationMessage MaxPCTMessage = AllMessagesForNegotiation.FirstOrDefault();

                if (MaxPCTMessage != null)
                {
                    currentMessagePreferenceSetNeg.Percentage = MaxPCTMessage.Percentage.Value;
                }
                else
                {
                    currentMessagePreferenceSetNeg.Percentage = 0;
                }

            }

            #endregion

            #region → Updating the Conversation by Last Rated Message Percentage .


            if (currentMessageConversation != null)
            {

                #region → Updaet Message By Last Score                                  .

                ConversationMessage LastRatedMessage = currentMessageConversation.ConversationMessages
                                    .Where(s => s.Percentage != null)
                                    .OrderByDescending(s => s.RatedDate)
                                    .FirstOrDefault();

                if (LastRatedMessage != null)
                {
                    currentMessageConversation.Percentage = LastRatedMessage.Percentage.Value;
                }
                else
                {
                    currentMessageConversation.Percentage = 0;
                }

                #endregion //------------------------------------------------------------------------------

                #region → Make History for Message (used to send statisticals for eneg) .


                ConversationMessage LastRatedReceivedMessage = currentMessageConversation.ConversationMessages
                                   .Where(s => s.Percentage != null && s.IsSent == false)
                                   .OrderByDescending(s => s.RatedDate)
                                   .FirstOrDefault();

                ConversationMessage LastRatedSentMessage = currentMessageConversation.ConversationMessages
                                   .Where(s => s.Percentage != null && s.IsSent == true)
                                   .OrderByDescending(s => s.RatedDate)
                                   .FirstOrDefault();

                currentMessageConversation.DMLastReceivedMessage = LastRatedReceivedMessage;
                currentMessageConversation.DMLastSentMessage = LastRatedSentMessage;

                #endregion//-----------------------------------------------------------------

            }

            #endregion


        }

        /// <summary>
        /// Updates the negotiation by best message.
        /// </summary>
        public void UpdateNegotiationByBestMessage(ConversationMessage crrentMessage)
        {


            ConversationMessage BestMessage
                       = this.Context.ConversationMessages.Where(s => s.NegConversationID == crrentMessage.NegConversationID && s.Percentage.HasValue  /*&& s.IsSent.Value==false*/)
                                                          .OrderByDescending(o => o.Percentage.Value)
                                                          .FirstOrDefault();

            if (BestMessage != null &&
                BestMessage.NegConversation != null &&
                BestMessage.NegConversation.PreferenceSetNeg != null &&
                BestMessage.NegConversation.PreferenceSetNeg.PreferenceSet != null &&
                BestMessage.NegConversation.PreferenceSetNeg.PreferenceSet.Issues != null)
            {

                //Clear all Values for that Negotiation e.g Purchase a Car
                foreach (var issue in BestMessage.NegConversation.PreferenceSetNeg.PreferenceSet.Issues)
                {
                    issue.RemoveStatusByNegConvID(crrentMessage.NegConversationID);

                    //Set the status by default value.
                    issue.Status = null;
                }

                /*--------------------------------------------------------------------------------------*/


                foreach (var messageIssue in BestMessage.MessageIssues)
                {
                    Issue issue = messageIssue.Issue;


                    #region → Numeric     .

                    if (issue.IssueTypeID == PrefAppConstant.IssueTypes.Numeric)
                    {
                        issue.AddStatus(crrentMessage.NegConversationID, messageIssue.Value, messageIssue.Rate);

                        issue.Status = issue.GetStatus(crrentMessage.NegConversationID);
                    }

                    #endregion

                    #region → Options     .

                    else if (issue.IssueTypeID == PrefAppConstant.IssueTypes.Options)
                    {

                        var maxOptionRate = messageIssue.MessageOptionIssues.OrderByDescending(s => s.OptionIssue.OptionIssueWeight).FirstOrDefault();

                        if (maxOptionRate != null && maxOptionRate.OptionIssue != null)
                        {

                            issue.AddStatus(crrentMessage.NegConversationID, maxOptionRate.OptionIssue.OptionIssueValue, maxOptionRate.OptionIssue.OptionIssueWeight);

                            issue.Status = issue.GetStatus(crrentMessage.NegConversationID);

                        }

                    }

                    #endregion

                    #region → Later Rated .

                    else if (issue.IssueTypeID == PrefAppConstant.IssueTypes.LaterRated)
                    {

                        var maxLaterRate = messageIssue.MessageLaterRatedIssues.OrderByDescending(s => s.LaterRatedIssue.LaterRatedIssueWeight).FirstOrDefault();

                        if (maxLaterRate != null && maxLaterRate.LaterRatedIssue != null)
                        {

                            issue.AddStatus(crrentMessage.NegConversationID, maxLaterRate.LaterRatedIssue.LaterRatedIssueValue, maxLaterRate.LaterRatedIssue.LaterRatedIssueWeight);

                            issue.Status = issue.GetStatus(crrentMessage.NegConversationID);

                        }

                    }

                    #endregion

                }

            }

        }

        #endregion  Public

        #endregion Methods

    }
}
