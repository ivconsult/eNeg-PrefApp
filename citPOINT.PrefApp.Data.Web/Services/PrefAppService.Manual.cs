#region → Usings   .
using citPOINT.PrefApp.Data.Web.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.DomainServices.Server;
using citPOINT.PrefApp.Common;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Configuration;
using citPOINT.eNeg.Infrastructure.ExceptionHandling;
using citPOINT.PrefApp.Data.Web.Complete;

#endregion

#region → History  .

/* Date         User              Change
 * 
 * 21.10.10     Yousra Reda       Creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion

namespace citPOINT.PrefApp.Data.Web
{
    /// <summary>
    /// Provide additional PrefApp Services rather than CRUD operations
    /// </summary>
    public partial class PrefAppService
    {
        #region → Fields         .

        eNegServiceSoapClient mLoader;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets the loader.
        /// </summary>
        /// <value>The loader.</value>
        public eNegServiceSoapClient Loader
        {
            get
            {
                if (mLoader == null)
                {
                    mLoader = new eNegServiceSoapClient();
                    InjectCredentials();
                }
                return mLoader;
            }
        }

        #endregion Properties

        #region → Methods        .

        #region → Public         .

        #region → Services Created especially for Mobile Add-on .

        /// <summary>
        /// Gets the preference set for negotiation.
        /// </summary>
        /// <param name="negotiationID">The negotiation ID.</param>
        /// <returns>Preference Set that contain this negotiation</returns>
        public PreferenceSet GetPreferenceSetForNegotiation(Guid negotiationID)
        {
            if (ServiceAuthentication.IsValid())
            {
                PreferenceSetNeg Neg = this.ObjectContext.PreferenceSetNegs.Where(s => s.NegID == negotiationID && s.Deleted == false).FirstOrDefault();
                if (Neg != null)
                {
                    PreferenceSet preferenceSet = this.ObjectContext.PreferenceSets.Where(ss => ss.PreferenceSetID == Neg.PreferenceSetID && ss.Deleted == false).FirstOrDefault();

                    return preferenceSet;
                }
                return null;
            }
            else
            {
                // throw fault exception to indicate the caller that the service need valid credentials                       
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Gets the issues for preference set.
        /// </summary>
        /// <param name="PreferenceSetID">The preference set ID.</param>
        /// <returns>list of issues related to certain Preference Set</returns>
        public IQueryable<Issue> GetIssuesForPreferenceSet(Guid PreferenceSetID)
        {
            if (ServiceAuthentication.IsValid())
            {
                var qry = this.ObjectContext.Issues.Where(s => s.PreferenceSetID == PreferenceSetID && s.Deleted == false);
                return qry;
            }
            else
            {
                // throw fault exception to indicate the caller that the service need valid credentials                       
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Gets the conversation score.
        /// </summary>
        /// <param name="conversationID">The conversation ID.</param>
        /// <param name="isSent">if set to <c>true</c> [is sent].</param>
        /// <returns></returns>
        public decimal GetConversationScore(Guid conversationID, bool isSent)
        {
            if (ServiceAuthentication.IsValid())
            {
                ConversationMessage conversationMessage
                                                  = this.ObjectContext
                                                        .ConversationMessages
                                                        .Where(ss => ss.IsSent == isSent &&
                                                                     ss.Deleted == false &&
                                                                     ss.RatedDate != null &&
                                                                     ss.NegConversation.ConversationID == conversationID)
                                                         .OrderByDescending(ss => ss.RatedDate)
                                                         .FirstOrDefault();

                if (conversationMessage != null)
                {
                    return conversationMessage.Percentage.Value;
                }
                return 0;
            }
            else
            {
                // throw fault exception to indicate the caller that the service need valid credentials                      
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Gets set of points that represt points that will be drawn on x-axis and y-axis for negotiation.
        /// </summary>
        /// <param name="negotiationID">The negotiation ID.</param>
        /// <param name="isSent">if set to <c>true</c> [is sent].</param>
        /// <returns>
        /// list of points that represent points that will be drawn on x-axis and y-axis
        /// </returns>
        public List<CoordinatesPoints> GetGraphForNegotiation(Guid negotiationID, bool isSent)
        {
            if (ServiceAuthentication.IsValid())
            {
                List<CoordinatesPoints> Points = new List<CoordinatesPoints>();

                List<ConversationMessage> ConvMsgs = this.ObjectContext.ConversationMessages.
                                                         Where(s => s.NegConversation.PreferenceSetNeg.NegID == negotiationID &&
                                                                    s.IsSent == isSent &&
                                                                    s.Percentage != null &&
                                                                    s.Deleted == false)
                                                         .OrderBy(s => s.DeletedOn).ToList();

                foreach (var MSG in ConvMsgs)
                {
                    Points.Add(new CoordinatesPoints(MSG.RatedDate.Value, MSG.Percentage.Value));
                }
                return Points;
            }
            else
            {
                // throw fault exception to indicate the caller that the service need valid credentials                      
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Gets set of points that represt points that will be drawn on x-axis and y-axis for conversation.
        /// </summary>
        /// <param name="conversationID">The conversation ID.</param>
        /// <param name="isSent">if set to <c>true</c> [is sent].</param>
        /// <returns>
        /// list of points that represent points that will be drawn on x-axis and y-axis
        /// </returns>
        public List<CoordinatesPoints> GetGraphForConversation(Guid conversationID, bool isSent)
        {
            if (ServiceAuthentication.IsValid())
            {
                List<CoordinatesPoints> Points = new List<CoordinatesPoints>();

                List<ConversationMessage> ConvMsgs = this.ObjectContext.ConversationMessages.
                                                        Where(s => s.NegConversation.ConversationID == conversationID &&
                                                                   s.IsSent == isSent &&
                                                                   s.Percentage != null &&
                                                                   s.Deleted == false)
                                                        .OrderBy(s => s.DeletedOn).ToList();

                foreach (var MSG in ConvMsgs)
                {
                    Points.Add(new CoordinatesPoints(MSG.RatedDate.Value, MSG.Percentage.Value));
                }
                return Points;
            }
            else
            {
                // throw fault exception to indicate the caller that the service need valid credentials                      
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Gets the min value of numeric Issue.
        /// </summary>
        /// <param name="issueID">The issue ID.</param>
        /// <returns>double value of minimum</returns>
        public double GetMinValue(Guid issueID)
        {
            if (ServiceAuthentication.IsValid())
            {
                NumericIssue numericIssue = this.ObjectContext.NumericIssues.Where(ss => ss.IssueID == issueID && ss.Deleted == false).FirstOrDefault();

                if (numericIssue != null)
                {
                    return (double)numericIssue.MinimumValue;
                }
                return 0;
            }
            else
            {
                // throw fault exception to indicate the caller that the service need valid credentials                      
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Gets the max value of numeric Issue.
        /// </summary>
        /// <param name="issueID">The issue ID.</param>
        /// <returns>double value of maximum</returns>
        public double GetMaxValue(Guid issueID)
        {
            if (ServiceAuthentication.IsValid())
            {
                NumericIssue numericIssue = this.ObjectContext.NumericIssues.Where(ss => ss.IssueID == issueID && ss.Deleted == false).FirstOrDefault();

                if (numericIssue != null)
                {
                    return (double)numericIssue.MaximumValue;
                }
                return 0;
            }
            else
            {
                // throw fault exception to indicate the caller that the service need valid credentials                      
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Gets the possible values of option issue or later rated issues..
        /// </summary>
        /// <param name="issueID">The issue ID.</param>
        /// <returns>a list of all options values</returns>
        [Invoke]
        public List<string> GetPossibleValues(Guid issueID)
        {
            if (ServiceAuthentication.IsValid())
            {
                List<string> lsOptions = new List<string>();
                List<OptionIssue> optionIssues = this.ObjectContext.OptionIssues.Where(ss => ss.IssueID == issueID && ss.Deleted == false).ToList();

                //Mean it may be Options Issue
                if (optionIssues.Count > 0)
                {
                    foreach (var optionIssue in optionIssues)
                    {
                        lsOptions.Add(optionIssue.OptionIssueValue);
                    }
                }
                else //May be it is later rated Issues
                {
                    List<LaterRatedIssue> LaterRatedIssues = this.ObjectContext.LaterRatedIssues.Where(ss => ss.IssueID == issueID && ss.Deleted == false).ToList();

                    foreach (var laterRatedIssue in LaterRatedIssues)
                    {
                        lsOptions.Add(laterRatedIssue.LaterRatedIssueValue);
                    }
                }
                return lsOptions;
            }
            else
            {
                // throw fault exception to indicate the caller that the service need valid credentials                      
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Gets the last value of issue.
        /// </summary>
        /// <param name="issueID">The issue ID.</param>
        /// <param name="conversationID">The conversation ID.</param>
        /// <param name="isSent">if set to <c>true</c> [is sent]. <c>fasle</c> [is Recieved].</param>
        /// <returns>last issue value</returns>
        public string GetValueOfIssue(Guid issueID, Guid conversationID, bool isSent)
        {
            if (ServiceAuthentication.IsValid())
            {
                ConversationMessage conversationMessage
                                                   = this.ObjectContext
                                                         .ConversationMessages
                                                         .Where(ss => ss.IsSent == isSent &&
                                                                      ss.Deleted == false &&
                                                                      ss.RatedDate != null &&
                                                                      ss.NegConversation.ConversationID == conversationID &&
                                                                      ss.MessageIssues.Count() > 0 &&
                                                                      ss.MessageIssues.
                                                                      Where(s => s.Deleted == false &&
                                                                          s.IssueID == issueID &&
                                                                          !string.IsNullOrEmpty(s.Value)).
                                                                          FirstOrDefault() != null)
                                                          .OrderByDescending(ss => ss.RatedDate)
                                                          .FirstOrDefault();

                if (conversationMessage == null)
                    return string.Empty;

                return conversationMessage.MessageIssues
                                          .Where(s => s.Deleted == false &&
                                                      s.IssueID == issueID &&
                                                      !string.IsNullOrEmpty(s.Value))
                                          .FirstOrDefault().Value;
            }
            else
            {
                // throw fault exception to indicate the caller that the service need valid credentials                      
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Gets the last value of issue.
        /// </summary>
        /// <param name="issueID">The issue ID.</param>
        /// <param name="conversationID">The conversation ID.</param>
        /// <param name="isSent">if set to <c>true</c> [is sent].</param>
        /// <returns></returns>
        public string GetLastValueOfIssue(Guid issueID, Guid conversationID, bool isSent)
        {
            if (ServiceAuthentication.IsValid())
            {
                ConversationMessage conversationMessage
                                                   = this.ObjectContext
                                                         .ConversationMessages
                                                         .Where(ss => ss.IsSent == isSent &&
                                                                      ss.Deleted == false &&
                                                                      ss.RatedDate != null &&
                                                                      ss.NegConversation.ConversationID == conversationID)
                                                          .OrderByDescending(ss => ss.RatedDate)
                                                          .FirstOrDefault();

                if (conversationMessage == null)
                    return string.Empty;

                MessageIssue messageIssue = this.ObjectContext
                                                .MessageIssues
                                                .Where(ss => ss.ConversationMessageID == conversationMessage.ConversationMessageID &&
                                                             ss.Deleted == false &&
                                                             ss.IssueID == issueID)
                                                .FirstOrDefault();

                if (messageIssue == null)
                {
                    return string.Empty;
                }
                return messageIssue.Value;
            }
            else
            {
                // throw fault exception to indicate the caller that the service need valid credentials                      
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Gets the next offer ID.
        /// generating new offer
        /// </summary>
        /// <param name="ConversationID">The conversation ID.</param>
        /// <param name="isSent">if set to <c>true</c> [is sent].</param>
        /// <returns>id of offer</returns>
        public string GetNextOfferID(Guid ConversationID, bool isSent)
        {
            if (ServiceAuthentication.IsValid())
            {
                NegConversation negConversation = this.ObjectContext.NegConversations.Where(ss => ss.ConversationID == ConversationID && ss.Deleted == false).FirstOrDefault();

                if (negConversation == null)
                {
                    return null;
                }

                string messageSubject = "Mobile Add-on-" + DateTime.Now.ToString();
                string messageContent = "Mobile Addon dummy Message";
                string messageReceiver = "no_mail@domain.com";
                string messageSender = "no_mail@domain.com";

                string messageID = Loader.AddMessageToConversation(
                                                   "Mobile Add-on",
                                                    negConversation.DeletedBy.Value,
                                                    ConversationID,
                                                    messageContent,
                                                    messageSubject,
                                                    messageReceiver,
                                                    messageSender,
                                                    isSent);


                if (!string.IsNullOrEmpty(messageID))
                {
                    ConversationMessage conversationMessage = new ConversationMessage();

                    conversationMessage.ConversationMessageID = Guid.NewGuid();
                    conversationMessage.Deleted = false;
                    conversationMessage.DeletedBy = negConversation.DeletedBy;
                    conversationMessage.DeletedOn = DateTime.Now;
                    conversationMessage.IsSent = isSent;
                    conversationMessage.MessageID = new Guid(messageID);
                    conversationMessage.NegConversationID = negConversation.NegConversationID;
                    conversationMessage.Percentage = null;
                    conversationMessage.RatedDate = null;

                    this.ObjectContext.AddToConversationMessages(conversationMessage);

                    if (this.ObjectContext.SaveChanges() > 0)
                    {
                        return conversationMessage.ConversationMessageID.ToString();
                    }
                }
                return null;
            }
            else
            {
                // throw fault exception to indicate the caller that the service need valid credentials                      
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Sets the value of issue.
        /// </summary>
        /// <param name="offerID">The offer ID.</param>
        /// <param name="issueID">The issue ID.</param>
        /// <param name="value">The value.</param>
        /// <returns>if set to <c>true</c> [Success]. <c>fasle</c> [failed].</returns>
        public bool SetValueOfIssue(Guid offerID, Guid issueID, string value)
        {
            if (ServiceAuthentication.IsValid())
            {
                ConversationMessage conversationMessage = this.ObjectContext.ConversationMessages.Where(ss => ss.ConversationMessageID == offerID && ss.Deleted == false).FirstOrDefault();

                if (conversationMessage != null)
                {
                    Issue issue = this.ObjectContext.Issues.Where(ss => ss.IssueID == issueID && ss.Deleted == false).FirstOrDefault();

                    if (issue != null)
                    {
                        #region → Check Message Issue Existance and Adding it if needed

                        MessageIssue messageIssue = this.ObjectContext.MessageIssues.Where(ss => ss.ConversationMessageID == offerID && ss.IssueID == issueID && ss.Deleted == false).FirstOrDefault();

                        if (messageIssue == null)
                        {
                            messageIssue = new MessageIssue()
                            {
                                ConversationMessageID = offerID,
                                Deleted = false,
                                DeletedBy = conversationMessage.DeletedBy,
                                DeletedOn = DateTime.Now,
                                IssueID = issueID,
                                MessageIssueID = Guid.NewGuid(),
                                Score = 0
                            };

                            this.ObjectContext.AddToMessageIssues(messageIssue);
                        }

                        #endregion

                        #region → Options     .

                        if (issue.IssueTypeID == PrefAppConstant.IssueTypes.Options)
                        {
                            //Check if option is exist or not
                            OptionIssue optionIssue = this.ObjectContext
                                                          .OptionIssues
                                                          .Where(ss => ss.IssueID == issueID &&
                                                                       ss.Deleted == false &&
                                                                       ss.OptionIssueValue.ToLower() == value.ToLower())
                                                          .FirstOrDefault();

                            //in case option is exist
                            if (optionIssue != null)
                            {
                                MessageOptionIssue messageOptionIssue = null;

                                // Mean this Issue Exist before e.g adding option to existing one.
                                if (messageIssue.EntityState != System.Data.EntityState.Added)
                                {
                                    messageOptionIssue = this.ObjectContext.MessageOptionIssues.Where(ss => ss.MessageIssueID == messageIssue.MessageIssueID && ss.OptionIssueID == optionIssue.OptionIssueID && ss.Deleted == false).FirstOrDefault();
                                }

                                //in case the option is not exist
                                if (messageOptionIssue == null)
                                {
                                    this.ObjectContext.AddToMessageOptionIssues(new MessageOptionIssue()
                                    {
                                        Deleted = false,
                                        DeletedBy = conversationMessage.DeletedBy,
                                        DeletedOn = DateTime.Now,
                                        MessageIssueID = messageIssue.MessageIssueID,
                                        MessageOptionIssueID = Guid.NewGuid(),
                                        OptionIssueID = optionIssue.OptionIssueID
                                    });
                                }
                            }
                        }

                        #endregion

                        #region → Later Rated .

                        else if (issue.IssueTypeID == PrefAppConstant.IssueTypes.LaterRated)
                        {
                            //Check if later Rated is exist or not
                            LaterRatedIssue laterRatedIssue = this.ObjectContext
                                                          .LaterRatedIssues
                                                          .Where(ss => ss.IssueID == issueID &&
                                                                       ss.Deleted == false &&
                                                                       ss.LaterRatedIssueValue.ToLower() == value.ToLower())
                                                          .FirstOrDefault();

                            //in case later Rated is exist
                            if (laterRatedIssue != null)
                            {
                                MessageLaterRatedIssue messageLaterRatedIssue = null;

                                // Mean this Issue Exist before e.g adding later Rated to existing one.
                                if (messageIssue.EntityState != System.Data.EntityState.Added)
                                {
                                    messageLaterRatedIssue = this.ObjectContext
                                                                 .MessageLaterRatedIssues
                                                                 .Where(ss => ss.MessageIssueID == messageIssue.MessageIssueID &&
                                                                              ss.LaterRatedIssueID == laterRatedIssue.LaterRatedIssueID &&
                                                                              ss.Deleted == false)
                                                                 .FirstOrDefault();
                                }

                                //in case the laterRated is not exist
                                if (messageLaterRatedIssue == null)
                                {
                                    this.ObjectContext.AddToMessageLaterRatedIssues(new MessageLaterRatedIssue()
                                    {
                                        Deleted = false,
                                        DeletedBy = conversationMessage.DeletedBy,
                                        DeletedOn = DateTime.Now,
                                        MessageIssueID = messageIssue.MessageIssueID,
                                        MessageLaterRatedIssueID = Guid.NewGuid(),
                                        LaterRatedIssueID = laterRatedIssue.LaterRatedIssueID
                                    });
                                }
                            }
                        }

                        #endregion

                        #region → Numeric     .

                        else if (issue.IssueTypeID == PrefAppConstant.IssueTypes.Numeric)
                        {
                            decimal xValue = 0;

                            string numericValue = null;

                            if (decimal.TryParse(value, out xValue))
                            {
                                numericValue = decimal.Round(xValue, 2).ToString();
                            }

                            messageIssue.Value = numericValue;
                        }

                        #endregion

                        #region → Not Rated   .

                        else if (issue.IssueTypeID == PrefAppConstant.IssueTypes.NotRated)
                        {
                            messageIssue.Value = value;
                        }

                        #endregion
                    }
                    return this.ObjectContext.SaveChanges() > 0;
                }
                return false;
            }
            else
            {
                // throw fault exception to indicate the caller that the service need valid credentials                      
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }

        }

        /// <summary>
        /// Offers the finished.
        /// </summary>
        /// <param name="OfferID">The offer ID.</param>
        [Invoke]
        public void OfferFinished(Guid OfferID)
        {
            if (ServiceAuthentication.IsValid())
            {
                try
                {
                    //Orignal Calculation Engine
                    CalculationEngine<ServerEngineProvider> calculationEngine = new CalculationEngine<ServerEngineProvider>(this.ObjectContext);

                    //Get Message
                    ConversationMessage conversationMessage = this.ObjectContext.ConversationMessages.Where(ss => ss.ConversationMessageID == OfferID).FirstOrDefault();

                    if (conversationMessage == null)
                    {
                        return;
                    }

                    //Get Conversation of the Message
                    NegConversation negConversation = calculationEngine.Provider.GetMessageNegConversation(conversationMessage.NegConversationID);

                    //Get Negotiation of the Message
                    PreferenceSetNeg preferenceSetNeg = calculationEngine.Provider.GetPreferenceSetNeg(negConversation.PreferenceSetNegID);

                    //Check if thier is a better creteria in numeric numbers
                    //if (calculationEngine.Provider.IsAnyNumericIssueHasBetter(negConversation.PreferenceSetNegID))
                    //{
                    //    //Calculate the whole Preference Set
                    //    calculationEngine.Calculate(preferenceSetNeg);
                    //}
                    //else
                    //{
                    //Calculate message only
                    calculationEngine.Calculate(conversationMessage);
                    //}

                    //Check number of success changes
                    int numberOfChanges = this.ObjectContext.SaveChanges(System.Data.Objects.SaveOptions.AcceptAllChangesAfterSave);

                    //in case of success then update conversation Percentage and negotiation percentage.
                    if (numberOfChanges > 1)
                    {
                        this.ObjectContext.UpdateConversationAndNegotiationPercentage(conversationMessage.ConversationMessageID);
                    }
                }
                catch (Exception ex)
                {
                    ExceptionManager.Instance.HandleException(ex, "Policy1");
                }
            }
            else
            {
                // throw fault exception to indicate the caller that the service need valid credentials                      
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }

        }
        #endregion

        #region → Others                                        .

        /// <summary>
        /// Gets the message issues by neg I ds.
        /// </summary>
        /// <param name="msgIDs">The MSG I ds.</param>
        /// <returns></returns>
        public IQueryable<MessageIssue> GetMessageIssuesByNegIDs(Guid[] msgIDs)
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.MessageIssues.Where(s => msgIDs.Contains(s.ConversationMessageID)
                                                              && s.Deleted == false);
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Gets the conv messages by conv I ds.
        /// </summary>
        /// <param name="conversationIDs">The conv I ds.</param>
        /// <returns></returns>
        public IQueryable<ConversationMessage> GetConvMessagesByConvIDs(Guid[] conversationIDs)
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.ConversationMessages.Where(s => conversationIDs.Contains(s.NegConversationID)
                                                              && s.Deleted == false);
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Gets the message option issues by neg I ds.
        /// </summary>
        /// <param name="msgIssueIDs">The MSG issue I ds.</param>
        /// <returns></returns>
        public IQueryable<MessageOptionIssue> GetMessageOptionIssuesByNegIDs(Guid[] msgIssueIDs)
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.MessageOptionIssues.Where(s => msgIssueIDs.Contains(s.MessageIssueID)
                                                              && s.Deleted == false);
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Gets the message later rated issues by neg I ds.
        /// </summary>
        /// <param name="msgIssueIDs">The MSG issue I ds.</param>
        /// <returns></returns>
        public IQueryable<MessageLaterRatedIssue> GetMessageLaterRatedIssuesByNegIDs(Guid[] msgIssueIDs)
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.MessageLaterRatedIssues.Where(s => msgIssueIDs.Contains(s.MessageIssueID)
                                                              && s.Deleted == false);
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }


        /// <summary>
        /// Gets the issue History.
        /// </summary>
        /// <param name="searchKeyWord">The search key word.</param>
        /// <param name="currentNegotiationID"></param>
        /// <returns></returns>
        public IQueryable<IssueHistoryResult> GetIssuesHistory(string searchKeyWord,Guid currentNegotiationID)
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.GetIssueHistory(searchKeyWord, currentNegotiationID).AsQueryable<IssueHistoryResult>();
            }
            else
            {
                // throw fault exception to indicate the caller that the service need valid credentials                      
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }
        
        /// <summary>
        /// Gets the issue statisticals.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <returns></returns>
        public IQueryable<IssueStatisticalsResult> GetIssueStatisticals(Guid userID)
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.GetIssueStatisticals(userID).AsQueryable<IssueStatisticalsResult>();
            }
            else
            {
                // throw fault exception to indicate the caller that the service need valid credentials                      
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Gets the preference sets for user.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <param name="organizationID">The organization ID.</param>
        /// <returns></returns>
        public IQueryable<PreferenceSet> GetPreferenceSetsForUser(Guid userID, Guid[] organizationID)
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.GetPreferenceSetForUser(userID, FromListToString(organizationID)).AsQueryable<PreferenceSet>();
            }
            else
            {
                // throw fault exception to indicate the caller that the service need valid credentials                      
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Gets the preference sets by ID.
        /// </summary>
        /// <param name="preferenceSetID">The preference set ID.</param>
        /// <returns></returns>
        public IQueryable<PreferenceSet> GetPreferenceSetsByID(Guid preferenceSetID)
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.PreferenceSets.Where(s => s.PreferenceSetID == preferenceSetID && s.Deleted == false).AsQueryable<PreferenceSet>();
            }
            else
            {
                // throw fault exception to indicate the caller that the service need valid credentials                      
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Gets the preference set organizations for user.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <returns></returns>
        public IQueryable<PreferenceSetOrganization> GetPreferenceSetOrganizationsForUser(Guid userID)
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.PreferenceSetOrganizations;
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Gets the issues.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <param name="organizationID">The organization ID.</param>
        /// <returns>Issues</returns>
        public IQueryable<Issue> GetIssuesRelatedToSpecificUser(Guid userID, Guid[] organizationID)
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.GetIssueForUser(userID, FromListToString(organizationID)).AsQueryable<Issue>();
            }
            else
            {
                // throw fault exception to indicate the caller that the service need valid credentials                      
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Gets the numeric issues.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <returns>Numeriic Issue</returns>
        public IQueryable<NumericIssue> GetNumericIssuesRelatedToSpecificUser(Guid userID, Guid[] organizationID)
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.GetNumericIssueForUser(userID, FromListToString(organizationID)).AsQueryable<NumericIssue>();
            }
            else
            {
                // throw fault exception to indicate the caller that the service need valid credentials                      
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Gets the option issues.
        /// </summary>
        /// <param name="UserID">The user ID.</param>
        /// <returns>Option Issue</returns>
        public IQueryable<OptionIssue> GetOptionIssuesRelatedToSpecificUser(Guid UserID, Guid[] organizationID)
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.GetOptionIssueForUser(UserID, FromListToString(organizationID)).AsQueryable<OptionIssue>();
            }
            else
            {
                // throw fault exception to indicate the caller that the service need valid credentials                      
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Gets the later rated issues.
        /// </summary>
        /// <param name="UserID">The user ID.</param>
        /// <returns>LaterRated Issue</returns>
        public IQueryable<LaterRatedIssue> GetLaterRatedIssuesRelatedToSpecificUser(Guid UserID, Guid[] organizationID)
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.GetLaterRatedIssueForUser(UserID, FromListToString(organizationID)).AsQueryable<LaterRatedIssue>();
            }
            else
            {
                // throw fault exception to indicate the caller that the service need valid credentials                      
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Updates the data matching status in addon.
        /// </summary>
        /// <param name="AppName">Name of the app.</param>
        /// <param name="UserID">The user ID.</param>
        /// <returns></returns>
        public bool RetrieveApplicationDMStatus(string AppName, Guid UserID)
        {
            if (ServiceAuthentication.IsValid())
            {
                var found = Loader.RetrieveApplicationDMStatus(AppName, UserID).RootResults.FirstOrDefault();
                if (found != null)
                    return found.IsDMActive.Value;

                return false;
            }
            else
            {
                // throw fault exception to indicate the caller that the service need valid credentials                      
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Updates the data matching status in addon.
        /// </summary>
        /// <param name="AppName">Name of the app.</param>
        /// <param name="UserID">The user ID.</param>
        /// <param name="IsActive">if set to <c>true</c> [is active].</param>
        public bool UpdateDataMatchingStatusInAddon(string AppName, Guid UserID, bool IsActive)
        {
            if (ServiceAuthentication.IsValid())
            {
                var found = Loader.UpdateDataMatchingStatusInAddon(AppName, UserID, IsActive).RootResults.FirstOrDefault();
                if (found != null)
                    return true;
                else
                    return false;
            }
            else
            {
                // throw fault exception to indicate the caller that the service need valid credentials                      
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Gets the organizations for user.
        /// </summary>
        /// <param name="UserID">The user ID.</param>
        /// <returns></returns>
        [Query(HasSideEffects = true)]
        public IQueryable<Organization> GetOrganizationsForUser(Guid UserID)
        {
            if (ServiceAuthentication.IsValid())
            {
                List<Organization> JoinedOrgs = new List<Organization>();
                var results = Loader.GetOrganizationsForUser(UserID).RootResults.AsQueryable();
                foreach (var org in results)
                {
                    Organization current = new Organization()
                    {
                        OrganizationID = org.OrganizationID,
                        OrganizationName = org.OrganizationName,
                    };

                    JoinedOrgs.Add(current);
                }
                return JoinedOrgs.AsQueryable();
            }
            else
            {
                // throw fault exception to indicate the caller that the service need valid credentials                      
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Gets the available negotiations to analysis.
        /// </summary>
        /// <param name="UserID">The user ID.</param>
        /// <param name="AppName">Name of the app.</param>
        /// <returns>Negotiations</returns>
        [Query(HasSideEffects = true)]
        public IQueryable<Negotiation> GetAvailableNegotiationsToAnalysis(Guid UserID, string AppName)
        {
            if (ServiceAuthentication.IsValid())
            {
                List<Negotiation> AvailableNegs = new List<Negotiation>();
                var results = Loader.GetAvailableNegotiationsToAnalysis(UserID, AppName).RootResults.AsQueryable();
                foreach (var Neg in results)
                {
                    Negotiation current = new Negotiation()
                    {
                        NegotiationID = Neg.NegotiationID,
                        StatusID = Neg.StatusID,
                        NegotiationName = Neg.NegotiationName
                    };

                    AvailableNegs.Add(current);
                }
                return AvailableNegs.AsQueryable();
            }
            else
            {
                // throw fault exception to indicate the caller that the service need valid credentials                      
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Gets the negotiations by list of I ds.
        /// </summary>
        /// <param name="NegIDs">The neg I ds.</param>
        /// <returns>Negotiation</returns>
        [Query(HasSideEffects = true)]
        public IQueryable<Negotiation> GetNegotiationsByListOfIDs(Guid[] NegIDs)
        {
            if (ServiceAuthentication.IsValid())
            {
                List<Negotiation> Negs = new List<Negotiation>();
                var results = Loader.GetNegotiationsByListOfIDs(NegIDs).RootResults.AsQueryable();
                foreach (var Neg in results)
                {
                    Negotiation current = new Negotiation()
                    {
                        NegotiationID = Neg.NegotiationID,
                        StatusID = Neg.StatusID,
                        NegotiationName = Neg.NegotiationName,
                        IsClosed = Neg.StatusID.Value != new Guid("e3b0b130-133e-4c1d-957c-14c67869446c") //This indicating that the Current Negotiation Is Closed
                    };
                    Negs.Add(current);
                }
                return Negs.AsQueryable();
            }
            else
            {
                // throw fault exception to indicate the caller that the service need valid credentials                      
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Gets the conversations by negotiation ID.
        /// </summary>
        /// <param name="NegIDs">The neg I ds.</param>
        /// <returns>Conversations</returns>
        [Query(HasSideEffects = true)]
        public IQueryable<Conversation> GetConversationsByNegotiationID(Guid[] NegIDs)
        {
            if (ServiceAuthentication.IsValid())
            {
                List<Conversation> Convs = new List<Conversation>();
                var results = Loader.GetConversationsByNegotiationID(NegIDs).RootResults.AsQueryable();

                foreach (var Conv in results)
                {
                    Conversation current = new Conversation()
                    {
                        ConversationID = Conv.ConversationID,
                        ConversationName = Conv.ConversationName,
                        NegotiationID = Conv.NegotiationID
                    };

                    Convs.Add(current);
                }

                return Convs.AsQueryable();
            }
            else
            {
                // throw fault exception to indicate the caller that the service need valid credentials                      
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Gets the conversations by negotiation ID.
        /// </summary>
        /// <param name="NegIDs">The neg I ds.</param>
        /// <returns></returns>
        [Query(HasSideEffects = true)]
        public IQueryable<Message> GetMessagesByNegotiationID(Guid?[] NegIDs)
        {
            if (ServiceAuthentication.IsValid())
            {
                List<Message> returnMessahesResult = new List<Message>();

                #region → Channels .

                var channels = Loader.GetChannels().RootResults.AsQueryable();

                Func<Guid, string> GetChannnelName = (channelID) =>
                {
                    var chn = channels.Where(s => s.ChannelID == channelID).FirstOrDefault();
                    if (chn != null)
                        return chn.ChannelName;
                    else
                        return string.Empty;
                };

                #endregion


                var results = Loader.GetMessagesByNegotiationIDForApps(NegIDs).RootResults.AsQueryable();

                foreach (var messageItem in results)
                {
                    Message current = new Message()
                    {
                        MessageID = messageItem.MessageID,
                        MessageSubject = messageItem.MessageSubject,
                        MessageContent = messageItem.MessageContent,
                        MessageSender = messageItem.MessageSender,
                        MessageReceiver = messageItem.MessageReceiver,
                        MessageDate = messageItem.MessageDate,
                        IsSent = messageItem.IsSent,
                        ConversationID = messageItem.ConversationID.Value,
                        ChannelName = GetChannnelName(messageItem.ChannelID)
                    };

                    returnMessahesResult.Add(current);
                }
                return returnMessahesResult.AsQueryable();
            }
            else
            {
                // throw fault exception to indicate the caller that the service need valid credentials                      
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Sends the apps statisticals messages.
        /// </summary>
        /// <param name="AppName">Name of the app.</param>
        /// <param name="UserID">The user ID.</param>
        /// <param name="conversationID">The conversation ID.</param>
        /// <param name="messageContent">Content of the message.</param>
        /// <param name="messageSubject">The message subject.</param>
        /// <param name="messageSender">The message sender.</param>
        /// <param name="messageReceiver">The message receiver.</param>
        /// <returns></returns>
        public bool SendAppsStatisticalsMessages(string AppName, Guid UserID, Guid conversationID, string messageContent, string messageSubject, string messageSender, string messageReceiver)
        {
            if (ServiceAuthentication.IsValid())
            {
                return Loader.UpdateAppsStatisticalsByMessages(AppName, UserID, conversationID, messageContent, messageSubject, messageSender, messageReceiver);
            }
            else
            {
                // throw fault exception to indicate the caller that the service need valid credentials                      
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        #endregion

        #region → Special functions For Strategy App            .

        /// <summary>
        /// Gets the Negotaition period.
        /// Special for Startegy App
        /// </summary>
        /// <param name="negotiationID">The negotiation ID.</param>
        /// <returns></returns>
        public IQueryable<ConversationPeriod> GetNegotiationPeriod(Guid negotiationID)
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.GetNegotiationPeriod(negotiationID).AsQueryable<ConversationPeriod>();
            }
            else
            {
                // throw fault exception to indicate the caller that the service need valid credentials                      
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Gets the conversation period.
        /// Special for Startegy App
        /// </summary>
        /// <param name="conversationID">The conversation ID.</param>
        /// <returns></returns>
        public IQueryable<ConversationPeriod> GetConversationPeriod(Guid conversationID)
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.GetConversationPeriod(conversationID).AsQueryable<ConversationPeriod>();
            }
            else
            {
                // throw fault exception to indicate the caller that the service need valid credentials                      
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Gets the last offer for conversation.
        /// </summary>
        /// <param name="conversationID">The conversation ID.</param>
        /// <param name="isSent">if set to <c>true</c> [is sent].</param>
        /// <returns></returns>
        public LastOfferDetails GetLastOfferForConversation(Guid conversationID, bool isSent)
        {
            if (ServiceAuthentication.IsValid())
            {
                ConversationMessage conversationMessage
                                                  = this.ObjectContext
                                                        .ConversationMessages
                                                        .Where(ss => ss.IsSent == isSent &&
                                                                     ss.Deleted == false &&
                                                                     ss.RatedDate != null &&
                                                                     ss.NegConversation.ConversationID == conversationID)
                                                         .OrderByDescending(ss => ss.RatedDate)
                                                         .FirstOrDefault();


                if (conversationMessage != null)
                {
                    LastOfferDetails lastOfferDetails = new LastOfferDetails();
                    lastOfferDetails.ID = 1;
                    lastOfferDetails.OfferDate = conversationMessage.RatedDate.Value;
                    lastOfferDetails.Percentage = conversationMessage.Percentage.Value;

                    return lastOfferDetails;
                }

                return null;
            }
            else
            {
                // throw fault exception to indicate the caller that the service need valid credentials                      
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        #endregion

        #region → Special for Offer App                         .

        /// <summary>
        /// Gets the next expected target for negotiation.
        /// </summary>
        /// <param name="negotiationID">The negotiation ID.</param>
        /// <param name="offerType">Type of the offer[Own-Counterpart-Mixed].</param>
        /// <param name="maxPercentage">The max percentage of preference Set.</param>
        /// <returns></returns>
        public ExpectedTarget GetNextExpectedTargetForNegotiation(Guid negotiationID, OfferType offerType, decimal maxPercentage)
        {
            if (ServiceAuthentication.IsValid())
            {
                return GetNextTarget(negotiationID, null, offerType, maxPercentage);
            }
            else
            {
                // throw fault exception to indicate the caller that the service need valid credentials                       
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Gets the next expected target for conversation.
        /// </summary>
        /// <param name="conversationID">The conversation ID.</param>
        /// <param name="offerType">Type of the offer[Own-Counterpart-Mixed].</param>
        /// <param name="maxPercentage">The max percentage of preference Set.</param>
        /// <returns></returns>
        public ExpectedTarget GetNextExpectedTargetForConversation(Guid conversationID, OfferType offerType, decimal maxPercentage)
        {
            if (ServiceAuthentication.IsValid())
            {
                return GetNextTarget(null, conversationID, offerType, maxPercentage);
            }
            else
            {
                // throw fault exception to indicate the caller that the service need valid credentials                       
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Gets the complete preference set for negotiation.
        /// </summary>
        /// <param name="negotiationID">The negotiation ID.</param>
        /// <returns></returns>
        [OperationContract]
        public CompletePreferenceSet GetCompletePreferenceSetForNegotiation(Guid negotiationID)
        {
            if (ServiceAuthentication.IsValid())
            {
                #region → Getting Preference Set .

                PreferenceSetNeg Neg = this.ObjectContext.PreferenceSetNegs.Where(s => s.NegID == negotiationID && s.Deleted == false).FirstOrDefault();

                if (Neg == null)
                {
                    return null;
                }

                PreferenceSet prefSet = this.GetPreferenceSetsByID(Neg.PreferenceSetID).FirstOrDefault();

                if (prefSet == null)
                {
                    return null;
                }

                #endregion

                //Adapt PreferenceSet to Complete Prefrence Set.
                CompletePreferenceSet completePreferenceSet = new CompletePreferenceSet(prefSet);

                //Getting Issues in Details
                completePreferenceSet.Issues.AddRange(GetCompleteIssuesForPreferenceSet(prefSet.PreferenceSetID, Neg.PreferenceSetNegID).ToList());

                return completePreferenceSet;
            }
            else
            {
                // throw fault exception to indicate the caller that the service need valid credentials                       
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Gets the base offer for negotiation by offer Type [Own-Counterpart-Mix].
        /// </summary>
        /// <param name="negotiationID">The negotiation ID.</param>
        /// <param name="offerType">Type of the offer.</param>
        /// <returns></returns>
        public List<OfferItem> GetBaseOfferForNegotiation(Guid negotiationID, OfferType offerType)
        {
            if (ServiceAuthentication.IsValid())
            {
                return GetBaseOffer(negotiationID, null, offerType);
            }
            else
            {
                // throw fault exception to indicate the caller that the service need valid credentials                       
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Gets the base offer for conversation by offer Type [Own-Counterpart-Mix].
        /// </summary>
        /// <param name="conversationID">The conversation ID.</param>
        /// <param name="offerType">Type of the offer.</param>
        /// <returns></returns>
        public List<OfferItem> GetBaseOfferForConversation(Guid conversationID, OfferType offerType)
        {
            if (ServiceAuthentication.IsValid())
            {
                return GetBaseOffer(null, conversationID, offerType);
            }
            else
            {
                // throw fault exception to indicate the caller that the service need valid credentials                       
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        #endregion
        
        #endregion

        #region → Private        .

        /// <summary>
        /// Injects the credentials into message header.
        /// </summary>
        private void InjectCredentials()
        {
            OperationContextScope scope = new OperationContextScope((IContextChannel)Loader.InnerChannel);

            MessageHeaders messageHeadersElement = OperationContext.Current.OutgoingMessageHeaders;
            messageHeadersElement.Add(MessageHeader.CreateHeader("username", "http://tempori.org", ConfigurationManager.AppSettings["username"]));
            messageHeadersElement.Add(MessageHeader.CreateHeader("password", "http://tempori.org", ConfigurationManager.AppSettings["password"]));
        }

        /// <summary>
        /// Froms the list to string.
        /// </summary>
        /// <param name="ListOfIDs">The list of I ds.</param>
        /// <returns></returns>
        private string FromListToString(Guid[] ListOfIDs)
        {
            if (ListOfIDs != null && ListOfIDs.Count() > 0)
            {
                string strList = "";

                foreach (var itemID in ListOfIDs)
                {
                    strList += "|" + itemID + "|";
                }

                strList = strList.Replace("||", "|");

                return strList;
            }
            return null;

        }

        #region → Special for Offer App   [Helpers Methods]     .

        /// <summary>
        /// Gets the next target.
        /// </summary>
        /// <param name="negotiationID">The negotiation ID.</param>
        /// <param name="conversationID">The conversation ID.</param>
        /// <param name="offerType">Type of the offer.</param>
        /// <param name="maxPercentage">The max percentage of the preference set.</param>
        /// <returns></returns>
        private ExpectedTarget GetNextTarget(Guid? negotiationID, Guid? conversationID, OfferType offerType, decimal maxPercentage)
        {
            try
            {
                ExpectedTarget expectedTarget = new ExpectedTarget()
                {
                    ID = 1,
                    Status = Complete.Status.Failed,
                    Target = 0
                };

                var result = this.ObjectContext.GetLastTwoOffersRate(negotiationID, conversationID, (byte)offerType).ToList();

                #region → No Settings exist              .

                if (result.Count < 2)//In case thier is now 2 offers.
                {
                    expectedTarget.Status = Status.NoSettings;
                }

                #endregion

                #region → Main calculation of next offer .

                else
                {
                    var firstResult = result.First().Value; //The most recent offer.
                    var secondResult = result.Last().Value; //the old one.

                    //diff may be Positive or negative
                    //Sequence like 70  ->> 80 ->> so diff is +10 so next offer 90
                    //Sequence like 65  ->> 60 ->> so diff is -05 so next offer 55
                    decimal diff = firstResult - secondResult;

                    expectedTarget.Target = firstResult + diff;

                    if (expectedTarget.Target < 0)
                    {
                        expectedTarget.Target = 0;
                    }
                    else if (expectedTarget.Target > maxPercentage)
                    {
                        expectedTarget.Target = maxPercentage;
                    }

                    expectedTarget.Status = Status.Success;
                }
                #endregion

                return expectedTarget;

            }
            catch (Exception)
            {
                return new ExpectedTarget()
                {
                    ID = 1,
                    Status = Status.Failed,
                    Target = 0
                };

            }
        }

        /// <summary>
        /// Gets the base offer for negotiation or Conversation by Offer Type [Own-Counterpart-Mix].
        /// </summary>
        /// <param name="negotiationID">The negotiation ID.</param>
        /// <param name="conversationID">The conversation ID.</param>
        /// <param name="offerType">Type of the offer.</param>
        /// <returns>a List of offer Items</returns>
        private List<OfferItem> GetBaseOffer(Guid? negotiationID, Guid? conversationID, OfferType offerType)
        {
            try
            {
                List<OfferItem> BaseOffer = new List<OfferItem>();

                var result = this.ObjectContext.GetLastTwoOffersIDs(negotiationID, conversationID, (byte)offerType).ToList();

                #region → No Offers exist                .

                if (result.Count <= 0)//In case thier is no offers.
                {
                    return BaseOffer;
                }

                #endregion

                #region → Main calculation of next offer .

                else
                {
                    List<OfferItem> baseOffer1;
                    List<OfferItem> baseOffer2 = null;

                    var Offer1 = this.ObjectContext.GetOfferDetailsForConversationMessage(result.FirstOrDefault()).ToList();

                    //Delete Repeated Options and convert to OfferItem
                    baseOffer1 = ExtractBaseOffer(Offer1);

                    if (result.Count > 1)
                    {
                        var Offer2 = this.ObjectContext.GetOfferDetailsForConversationMessage(result.LastOrDefault()).ToList();
                        baseOffer2 = ExtractBaseOffer(Offer2);
                    }

                    if (baseOffer2 == null)
                    {
                        return baseOffer1;
                    }
                    else
                    {
                        #region → Mixing two Offers .

                        foreach (var firstOfferItem in baseOffer1)
                        {
                            OfferItem secondOfferItem = baseOffer2.Where(s => s.IssueID == firstOfferItem.IssueID).FirstOrDefault();

                            if (secondOfferItem != null)
                            {
                                if (firstOfferItem.IssueType == PrefAppConstant.IssueTypes.Numeric)
                                {
                                    //Get Avg.
                                    //TODO: Detect Integers..
                                    firstOfferItem.Value = Math.Round(((decimal.Parse(firstOfferItem.Value) + decimal.Parse(secondOfferItem.Value)) / 2M), 2).ToString();

                                    //Calculate score in Offer App.
                                    firstOfferItem.Percentage = 0;
                                }
                                else if (firstOfferItem.OptionID.HasValue)
                                {
                                    //Choose Option with High score.
                                    if (firstOfferItem.Percentage < secondOfferItem.Percentage)
                                    {
                                        firstOfferItem.OptionID = secondOfferItem.OptionID;
                                        firstOfferItem.Percentage = secondOfferItem.Percentage;
                                        firstOfferItem.Value = secondOfferItem.Value;
                                    }
                                }

                                baseOffer2.Remove(secondOfferItem);
                            }

                            BaseOffer.Add(firstOfferItem);
                        }

                        //In case there Issue rated in second offer 
                        //and not rated in first one.
                        foreach (var OfferItem in baseOffer2)
                        {
                            BaseOffer.Add(OfferItem);
                        }

                        #endregion
                    }
                }

                #endregion

                return BaseOffer;

            }
            catch (Exception)
            {
                return new List<OfferItem>();

            }
        }

        /// <summary>
        /// Extracts the base offer.
        /// </summary>
        /// <param name="offer">The offer.</param>
        /// <returns></returns>
        private List<OfferItem> ExtractBaseOffer(List<OfferItem_Result> offer)
        {
            List<OfferItem> BaseOffer = new List<OfferItem>();

            foreach (var offerItem in offer)
            {
                //Check if exist before or not (Happen only in case of options (red,Blue))
                if (BaseOffer.Where(s => s.IssueID == offerItem.IssueID).Count() == 0)
                {
                    //In case if option or later Rated
                    if (offerItem.OptionID.HasValue)
                    {
                        Int64 rank = offerItem.Rank;

                        foreach (var item in offer.Where(d => d.IssueID == offerItem.IssueID && d.OptionID == offerItem.OptionID))
                        { //for e.g. one choose Red and Green so choose one with better Score.
                            if (offerItem.OptionRate < item.OptionRate)
                            {
                                rank = item.Rank;
                            }
                        }

                        BaseOffer.Add(new OfferItem(offer.Where(d => d.Rank == rank).FirstOrDefault()));
                    }
                    else
                    {
                        BaseOffer.Add(new OfferItem(offerItem));
                    }
                }
            }

            return BaseOffer;
        }

        #region → Related To Get Complete Preference Set .

        /// <summary>
        /// Gets the complete issues for preference set.
        /// </summary>
        /// <param name="preferenceSetID">The preference set ID.</param>
        /// <param name="preferenceSetNegID">The preference set neg ID.</param>
        /// <returns></returns>
        private List<CompleteIssue> GetCompleteIssuesForPreferenceSet(Guid preferenceSetID, Guid preferenceSetNegID)
        {
            if (ServiceAuthentication.IsValid())
            {
                IEnumerable<Issue> PrefSetIssues = GetIssuesForPreferenceSet(preferenceSetID);

                if (PrefSetIssues == null || PrefSetIssues.Count() <= 0)
                {
                    return new List<CompleteIssue>();
                }

                //Reset IDs.
                CompleteIssue.rank = 0;

                CompleteNumeric.nextID = 0;

                List<CompleteIssue> completeIssues = new List<CompleteIssue>();

                foreach (var issueItem in PrefSetIssues.OrderBy(s => s.DeletedOn))
                {
                    //Adapt Issue to complete Issue.
                    CompleteIssue completeIssue = new CompleteIssue(issueItem);

                    #region → Get Issue Details .

                    switch (completeIssue.IssueType)
                    {
                        case CompleteIssueType.Numeric:
                            completeIssue.Numeric = GetCompleteNumericForIsssue(issueItem.IssueID, preferenceSetNegID);
                            break;
                        case CompleteIssueType.Option:
                            completeIssue.Options = GetCompleteOptionsForIsssue(issueItem.IssueID, CompleteIssueType.Option);
                            break;
                        case CompleteIssueType.LaterRated:
                            completeIssue.Options = GetCompleteOptionsForIsssue(issueItem.IssueID, CompleteIssueType.LaterRated);
                            break;
                        case CompleteIssueType.NotRated:
                            break;
                        default:
                            break;
                    }

                    #endregion

                    completeIssues.Add(completeIssue);
                }

                return completeIssues;
            }
            else
            {
                // throw fault exception to indicate the caller that the service need valid credentials                       
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Gets the complete numeric for isssue.
        /// </summary>
        /// <param name="issueID">The issue ID.</param>
        /// <param name="preferenceSetNegID">The preference set neg ID.</param>
        /// <returns></returns>
        private CompleteNumeric GetCompleteNumericForIsssue(Guid issueID, Guid preferenceSetNegID)
        {
            #region → Get The Orignal Numeric            .

            NumericIssue numericIssue = this.ObjectContext
                       .NumericIssues
                       .Where(s => s.Deleted == false &&
                                   s.IssueID == issueID).FirstOrDefault();
            if (numericIssue == null)
            {
                return null;
            }

            //Adapt it To Complete Numeric
            CompleteNumeric completeNumeric = new CompleteNumeric(numericIssue);

            #endregion

            #region → In case if it is Unbounded Numeric .

            #region → Max side .

            if (completeNumeric.MaxOperatorBetter || completeNumeric.MaxOperatorEqual)
            {
                var topMax = this.ObjectContext
                                 .GetMessageIssueSelectMaxMinValue(preferenceSetNegID,
                                                                   PrefAppConstant.IssueTypes.Numeric,
                                                                   issueID,
                                                                   true).FirstOrDefault();

                if (topMax != null &&
                    topMax.HasValue &&
                    completeNumeric.MaximumValue < topMax.Value)
                {
                    completeNumeric.MaximumValue = topMax.Value;
                }
            }

            #endregion

            #region → Min Side .

            else if (completeNumeric.MinOperatorBetter ||
                     completeNumeric.MinOperatorEqual)
            {
                var topMin = this.ObjectContext
                                 .GetMessageIssueSelectMaxMinValue(preferenceSetNegID,
                                                                   PrefAppConstant.IssueTypes.Numeric,
                                                                   issueID,
                                                                   false).FirstOrDefault();

                if (topMin != null &&
                    topMin.HasValue &&
                    completeNumeric.MinimumValue > topMin.Value)
                {
                    completeNumeric.MinimumValue = topMin.Value;
                }
            }

            #endregion

            #endregion

            return completeNumeric;
        }

        /// <summary>
        /// Gets the complete options for isssue.
        /// </summary>
        /// <param name="issueID">The issue ID.</param>
        /// <param name="issueType">Type of the issue.</param>
        /// <returns></returns>
        private List<CompleteOption> GetCompleteOptionsForIsssue(Guid issueID, CompleteIssueType issueType)
        {
            #region → Option Type      .

            if (issueType == CompleteIssueType.Option)
            {
                var listOfOptions = this.ObjectContext
                           .OptionIssues
                           .Where(s => s.Deleted == false &&
                                       s.IssueID == issueID).ToList();

                List<CompleteOption> listofCompleteOption = new List<CompleteOption>();

                foreach (var option in listOfOptions)
                {
                    listofCompleteOption.Add(new CompleteOption(option));
                }

                return listofCompleteOption;
            }

            #endregion

            #region → Later rated Type .

            if (issueType == CompleteIssueType.LaterRated)
            {
                var lst = this.ObjectContext
                           .LaterRatedIssues
                           .Where(s => s.Deleted == false &&
                                       s.IssueID == issueID).ToList();

                List<CompleteOption> listofCompleteOption = new List<CompleteOption>();

                foreach (var option in lst)
                {
                    listofCompleteOption.Add(new CompleteOption(option));
                }

                return listofCompleteOption;
            }

            #endregion

            return null;
        }

        #endregion

        #endregion

        #endregion

        #endregion
    }
}
