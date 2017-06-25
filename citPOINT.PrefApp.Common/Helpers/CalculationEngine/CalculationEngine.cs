#region → Usings   .
using citPOINT.PrefApp.Common;
using citPOINT.PrefApp.Data.Web;
using System;
using System.Collections.Generic;
using System.Linq;

#endregion

#region → History  .

/* Date         User              Change
 * 
 * 26.01.11     M.Wahab           • Creation
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
    /// External class to can Score and Re-score all messages
    /// </summary>
    /// <typeparam name="T">Is any class implement IEngineProvider and it reperesent the current context client or server</typeparam>
    public class CalculationEngine<T> where T : IEngineProvider
    {

        #region → Fields         .


        private ConversationMessage mCurrentMessage;
        private IEnumerable<MessageIssue> mCurrentMessageIssues;
        private IEnumerable<NegConversation> mCurrentConversations;
        private PreferenceSetNeg mCurrentNegotiation;
        private NegConversation mCurrentMessageConversation;
        private bool IsDataMatchingForOneMessage = true;
        private Guid? LastRatedMessageGuid = null;
        private OptionsCalculation mOptionsCalculator;
        private T mProvider;

        #endregion

        #region → Properties     .


        /// <summary>
        /// Gets or sets the provider.
        /// </summary>
        /// <value>The provider.</value>
        public T Provider
        {
            get
            {
                return mProvider;
            }
            set
            {
                mProvider = value;
            }
        }

        /// <summary>
        /// Gets the options calculator.
        /// </summary>
        /// <value>The options calculator.</value>
        private OptionsCalculation OptionsCalculator
        {
            get
            {
                if (mOptionsCalculator == null)
                {
                    mOptionsCalculator = new OptionsCalculation();
                }

                return mOptionsCalculator;
            }
        }

        /// <summary>
        /// Gets or sets the current message.
        /// </summary>
        /// <value>The current message.</value>
        private ConversationMessage CurrentMessage
        {
            get
            {
                return mCurrentMessage;
            }

            set
            {
                //Setting The Message
                mCurrentMessage = value;

                //Setting the Master table of Data Matching
                CurrentMessageIssues = Provider.GetMessageIssues(mCurrentMessage.ConversationMessageID);

                //Set the Conversation of the Current Message.
                CurrentMessageConversation = Provider.GetMessageNegConversation(mCurrentMessage.NegConversationID);

                //if Case of we Found  a Conversations
                if (CurrentMessageConversation != null)
                {
                    //Selecting the All Conversation  related to the Same Negotiation
                    CurrentConversations = Provider.GetNegotiationConversations(CurrentMessageConversation.PreferenceSetNegID);

                    //Current Negotiations.
                    CurrentMessageNegotiation = Provider.GetPreferenceSetNeg(CurrentMessageConversation.PreferenceSetNegID);
                }


            }
        }

        /// <summary>
        /// Gets or sets the current message issues.
        /// </summary>
        /// <value>The current message issues.</value>
        private IEnumerable<MessageIssue> CurrentMessageIssues
        {
            get
            {
                return mCurrentMessageIssues;
            }

            set
            {
                mCurrentMessageIssues = value;
            }

        }

        /// <summary>
        /// Gets or sets the current message conversation.
        /// </summary>
        /// <value>The current message conversation.</value>
        private NegConversation CurrentMessageConversation
        {
            get
            {
                return mCurrentMessageConversation;
            }

            set
            {
                mCurrentMessageConversation = value;
            }

        }

        /// <summary>
        /// Gets or sets the current message negotiation.
        /// </summary>
        /// <value>The current message negotiation.</value>
        private PreferenceSetNeg CurrentMessageNegotiation
        {
            get
            {
                return mCurrentNegotiation;
            }

            set
            {
                mCurrentNegotiation = value;
            }
        }

        /// <summary>
        /// Gets or sets the current conversations.
        /// </summary>
        /// <value>The current conversations.</value>
        private IEnumerable<NegConversation> CurrentConversations
        {
            get
            {
                return mCurrentConversations;
            }

            set
            {
                mCurrentConversations = value;
            }

        }

        /// <summary>
        /// Gets or sets all messages for negotiation.
        /// </summary>
        /// <value>All messages for negotiation.</value>
        private IEnumerable<ConversationMessage> AllMessagesForNegotiation { get; set; }

        #endregion

        #region → Constructor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="CalculationEngine&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public CalculationEngine(object context)
        {
            var provider = Activator.CreateInstance(typeof(T), context);

            this.Provider = (T)(provider);
        }


        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Sets the offer variation for message.
        /// </summary>
        /// <param name="currentMessage">The current message.</param>
        /// <param name="allMessages">All messages.</param>
        private void SetOfferVariationForMessage(ConversationMessage currentMessage, IEnumerable<ConversationMessage> allMessages)
        {
            currentMessage.IsExceedVariation = false;

            if (allMessages != null &&
                allMessages.Count() > 0 &&
                currentMessage.NegConversation != null &&
                currentMessage.NegConversation.PreferenceSetNeg != null &&
                currentMessage.NegConversation.PreferenceSetNeg.PreferenceSet != null)
            {

                NegConversation currentNegConversation = currentMessage.NegConversation;

                PreferenceSet CurrentPreferenceSet = currentNegConversation.PreferenceSetNeg.PreferenceSet;


                if (CurrentPreferenceSet.Checkvariation)
                {
                    #region → Select Offer previous of current .

                    ConversationMessage convMessage = allMessages.Where(s => s.ConversationMessageID != currentMessage.ConversationMessageID &&
                                                                             s.NegConversationID == currentMessage.NegConversationID &&
                                                                             s.RatedDate != null &&
                                                                             s.DeletedOn.Value <= currentMessage.DeletedOn &&
                                                                             s.Percentage.HasValue &&
                                                                             s.IsSent == currentMessage.IsSent)
                                                                 .OrderByDescending(s => s.DeletedOn)
                                                                 .FirstOrDefault();

                    #endregion

                    if (convMessage != null)
                    {
                        decimal variation = Math.Abs(convMessage.Percentage.Value - currentMessage.Percentage.Value);

                        currentMessage.IsExceedVariation = (variation > CurrentPreferenceSet.VariationValue);
                    }
                }
            }
        }

        #endregion

        #region → Public         .
        
        /// <summary>
        /// Calculates the specified current preference set.
        /// </summary>
        /// <param name="CurrentPreferenceSet">The current preference set.</param>
        /// <returns></returns>
        public bool Calculate(PreferenceSet CurrentPreferenceSet)
        {
            //Now you will calculate for all so no need
            //to check if thier is new Better value for Numeric Issue
            IsDataMatchingForOneMessage = false;

            foreach (var item in CurrentPreferenceSet.PreferenceSetNegs)
            {
                if (!Calculate(item))
                {
                    return false;
                }
            }

            return true;

        }

        /// <summary>
        /// Calculates the specified current preference set neg.
        /// </summary>
        /// <param name="CurrentPreferenceSetNeg">The current preference set neg.</param>
        /// <returns></returns>
        public bool Calculate(PreferenceSetNeg CurrentPreferenceSetNeg)
        {
            //Now you will calculate for all so no need
            //to check if thier is new Better value for Numeric Issue
            IsDataMatchingForOneMessage = false;


            // In case of closed Negotiation Stop Calc. Score.
            if (CurrentPreferenceSetNeg.IsClosed)
            {
                return true;
            }

            this.AllMessagesForNegotiation = this.Provider.GetAllMessagesForNegotiation(CurrentPreferenceSetNeg.PreferenceSetNegID);

            //this case happen when one delete all Issues or the only issues that was assigned to that message.
            IEnumerable<ConversationMessage> allMessagesForNegotiationButHasError = this.Provider.GetAllMessagesForNegotiationButHasError(CurrentPreferenceSetNeg.PreferenceSetNegID);


            foreach (var msg in allMessagesForNegotiationButHasError)
            {
                msg.Percentage = null;

                //setting the rating Date
                msg.RatedDate = null;

                //Exceed Variation
                msg.IsExceedVariation = false;

                this.Provider.UpdateConversationAndNegotiationPCT(msg);
            }


            //calculate for all message in current negotiation.
            foreach (var msg in this.AllMessagesForNegotiation.OrderBy(s => s.RatedDate))
            {
                if (!Calculate(msg))
                {
                    return false;
                }
                else
                {

                }
            }


            //Negotiation or Conversation has Precentage more than zero
            //but no message is rated this due to the message which is rated was deleted in eNeg
            if (allMessagesForNegotiationButHasError.Count() == 0
                && this.AllMessagesForNegotiation.Count() == 0)
            {
                CurrentPreferenceSetNeg.Percentage = 0;

                foreach (var Conv in this.Provider.GetNegConversationsForPreferenceSetNeg(CurrentPreferenceSetNeg.PreferenceSetNegID))
                {
                    Conv.Percentage = 0;
                }
            }


            return true;

        }

        /// <summary>
        /// Calculates the specified current message.
        /// </summary>
        /// <param name="CurrentMessage">The current message.</param>
        /// <returns></returns>
        public bool Calculate(ConversationMessage CurrentMessage)
        {

            this.CurrentMessage = CurrentMessage;


            if (IsDataMatchingForOneMessage)
            {
                //Set it to be false untill end of calculation for all messages in this Negotiation
                IsDataMatchingForOneMessage = false;

                //Reset Last messages
                this.AllMessagesForNegotiation = null;

                //Record Last Retad  Message.
                LastRatedMessageGuid = CurrentMessage.ConversationMessageID;


                if (this.Provider.IsAnyNumericIssueHasBetter(this.CurrentMessageNegotiation.PreferenceSetNegID) ||
                    this.Provider.IsCheckvariation(this.CurrentMessageNegotiation.PreferenceSetID))
                {
                    return Calculate(this.CurrentMessageNegotiation);
                }
            }

            #region → Calculate Message Score through Issues .

            decimal TotalScore = 0;

            Issue issue = null;

            foreach (var msgIssue in CurrentMessageIssues)
            {
                //Reset Score.
                msgIssue.Score = 0;

                issue = Provider.GetIssue(msgIssue.IssueID);

                if (issue != null)
                {
                    //in case of the Issue type is numeric or Not rated
                    if (issue.IssueTypeID == PrefAppConstant.IssueTypes.Numeric)
                    {
                        //Getting Numeric Object of Current Issue
                        NumericIssue numericIssue = Provider.GetNumericIssue(issue.IssueID);

                        //Create Boundary Object for Maximum and minimum values
                        NumericBoundary numericBoundary = new NumericBoundary(this.Provider,(INumericIssue) numericIssue, this.CurrentMessageNegotiation.PreferenceSetNegID);

                        //Numeric calculation black box
                        NumericCalculation numericCalculation = new NumericCalculation(issue.IssueWeight, numericIssue, numericBoundary);

                        //Calc score for numeric
                        msgIssue.Score = numericCalculation.CalcScore(msgIssue.Value);

                        //This set to be showen in the pannel of Data matching (Status Label)
                        msgIssue.Rate = numericCalculation.Rate;

                    }       //in Casae of Options
                    else if (issue.IssueTypeID == PrefAppConstant.IssueTypes.Options)
                    {
                        msgIssue.Score = OptionsCalculator.CalcOptionScore(issue.IssueWeight, Provider.GetOptionIssues(issue.IssueID), Provider.GetMessageOptionIssues(msgIssue.MessageIssueID));
                        msgIssue.Value = OptionsCalculator.Value;
                        msgIssue.Rate = OptionsCalculator.Rate;
                    }
                    else if (issue.IssueTypeID == PrefAppConstant.IssueTypes.LaterRated)
                    {
                        msgIssue.Score = OptionsCalculator.CalcLaterRatedScore(issue.IssueWeight, Provider.GetLaterRatedIssues(issue.IssueID), Provider.GetMessageLaterRatedIssues(msgIssue.MessageIssueID));
                        msgIssue.Value = OptionsCalculator.Value;
                        msgIssue.Rate = OptionsCalculator.Rate;
                    }

                    //Calc the Final Score.
                    TotalScore += msgIssue.Score.Value;
                }
            }

            //Message Precentage.
            this.CurrentMessage.Percentage = TotalScore;

            //setting the rating Date
            if (!this.CurrentMessage.RatedDate.HasValue ||
                (LastRatedMessageGuid != null && CurrentMessage.ConversationMessageID == LastRatedMessageGuid)
                )
            {
                /*Message Creation Date From eNeg According To Mr.Sunder needs at 17.03.2011
                 so as that the last message as the last message date from eNeg not the last rated one.*/
                this.CurrentMessage.RatedDate = this.CurrentMessage.DeletedOn; // DateTime.Now;
            }
            
            #endregion

            //Update Conversation + Negotiation Percentage
            this.Provider.UpdateConversationAndNegotiationPCT(this.CurrentMessage);

            //update status field
            this.Provider.UpdateNegotiationByBestMessage(this.CurrentMessage);

            //Offer variation to last message
            this.SetOfferVariationForMessage(this.CurrentMessage, this.AllMessagesForNegotiation);

            return true;

        }



        #endregion

        #endregion

    }
}
