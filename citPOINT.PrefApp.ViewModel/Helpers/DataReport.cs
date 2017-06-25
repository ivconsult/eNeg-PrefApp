
#region → Usings   .
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel.DomainServices.Client;
using citPOINT.PrefApp.Common;
using citPOINT.PrefApp.Data;
using citPOINT.PrefApp.Data.Web;
using System.Text;
#endregion

#region → History  .

/* Date         User              Change
 * 
 * 20.02.11     Yousra Reda         • creation
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

namespace citPOINT.PrefApp.ViewModel
{
    /// <summary>
    /// External class to can generate source for data report
    /// </summary> 
    public class DataReport
    {
        #region → Fields         .
        private ConversationMessage MessageSent;
        private ConversationMessage MessageReceived;
        private ObservableCollection<FilteredIssue> FilteredIssueSource;

        #endregion

        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="DataReport"/> class.
        /// </summary>
        public DataReport()
        {
            FilteredIssueSource = new ObservableCollection<FilteredIssue>();
            MessageSent = new ConversationMessage();
            MessageReceived = new ConversationMessage();
        }

        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Filters the data.
        /// </summary>
        /// <param name="Filter">The filter.</param>
        /// <param name="ConversationMessages">The conversation messages.</param>
        private void GetSentAndReceivedMessage
            (string Filter, List<ConversationMessage> ConversationMessages)
        {
            #region → if Second Filter is "Last Data"     .
            if (Filter == FilterType.LastData.ToString())
            {
                var sentItem = ConversationMessages.OrderByDescending(s => s.RatedDate).Where(s => s.IsSent == true && s.MessageIssues.Count > 0).FirstOrDefault();
                if (sentItem != null)
                    MessageSent = sentItem;

                var receivedItem = ConversationMessages.OrderByDescending(s => s.RatedDate).Where(s => s.IsSent == false && s.MessageIssues.Count > 0).FirstOrDefault();
                if (receivedItem != null)
                    MessageReceived = receivedItem;
            }
            #endregion

            #region → if Second Filter is "Best Scoring"  .
            else if (Filter == FilterType.BestScoring.ToString())
            {
                var sentItem = ConversationMessages.Where(s => s.Percentage == ConversationMessages.Where(o => o.IsSent == true && o.MessageIssues.Count > 0).Max(o => o.Percentage) && s.IsSent == true && s.MessageIssues.Count > 0)
                    .OrderByDescending(s => s.RatedDate).FirstOrDefault();
                if (sentItem != null)
                    MessageSent = sentItem;

                var receivedItem = ConversationMessages.Where(s => s.Percentage == ConversationMessages.Where(o => o.IsSent == false && o.MessageIssues.Count > 0).Max(o => o.Percentage) && s.IsSent == false && s.MessageIssues.Count > 0)
                    .OrderByDescending(s => s.RatedDate).FirstOrDefault();
                if (receivedItem != null)
                    MessageReceived = receivedItem;
            }
            #endregion
        }

        /// <summary>
        /// Adds the new filtered issue.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="isSent">if set to <c>true</c> [is sent].</param>
        private void AddNewFilteredIssue(Entity entity, bool isSent = true)
        {
            FilteredIssue filteredIssue = null;
            FilteredIssue FilteredIssueObj = null;
            MessageIssue msgIssue = null;

            #region → Case entity is Message Issue            .

            if (entity is MessageIssue)
            {
                msgIssue = entity as MessageIssue;

                var ExistIssue = FilteredIssueSource.Where(s => s.IssueName == msgIssue.Issue.IssueName).FirstOrDefault();
                if (ExistIssue != null)
                {
                    string unit = string.Empty;
                    if (msgIssue.Issue.IssueTypeID == PrefAppConstant.IssueTypes.Numeric)
                    {
                        unit = msgIssue.Issue.NumericIssues.Where(s => s.IssueID == msgIssue.IssueID).First().Unit;
                        if (isSent)
                            ExistIssue.SentValue += ExistIssue.SentValue.Contains(msgIssue.Value + " " + unit) ? string.Empty : Environment.NewLine + msgIssue.Value + " " + unit;
                        else
                            ExistIssue.ReceivedValue += ExistIssue.ReceivedValue.Contains(msgIssue.Value + " " + unit) ? string.Empty : Environment.NewLine + msgIssue.Value + " " + unit;
                    }
                    if (isSent)
                        ExistIssue.SentValue += ExistIssue.SentValue.Contains(msgIssue.Value) ? string.Empty : Environment.NewLine + msgIssue.Value + " " + unit;
                    else
                        ExistIssue.ReceivedValue += ExistIssue.ReceivedValue.Contains(msgIssue.Value) ? string.Empty : Environment.NewLine + msgIssue.Value + " " + unit;

                    FilteredIssueObj = ExistIssue;
                }
                else
                {
                    filteredIssue = new FilteredIssue();
                    filteredIssue.IssueName = msgIssue.Issue.IssueName;
                    string unit = string.Empty;

                    if (msgIssue.Issue.IssueTypeID == PrefAppConstant.IssueTypes.Numeric)
                    {
                        unit = msgIssue.Issue.NumericIssues.Where(s => s.IssueID == msgIssue.IssueID).First().Unit;
                    }

                    if (isSent)
                        filteredIssue.SentValue = msgIssue.Value + " " + unit;
                    else
                        filteredIssue.ReceivedValue = msgIssue.Value + " " + unit;

                    FilteredIssueObj = filteredIssue;
                }
            }
            #endregion

            if (FilteredIssueObj != null)
            {
                if (isSent)
                    FilteredIssueObj.SentValueScore = msgIssue.Score.Value;
                else
                    FilteredIssueObj.ReceivedValueScore = msgIssue.Score.Value;
            }
            if (filteredIssue != null)
                FilteredIssueSource.Add(filteredIssue);
        }

        /// <summary>
        /// Generates the filtered issue source.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <param name="issueType">Type of the issue.</param>
        /// <param name="isSent">if set to <c>true</c> [is sent].</param>
        private void GenerateFilteredIssueSource(ConversationMessage msg, bool isSent = true,
            PrefAppConfigurations.IssueTypes issueType = PrefAppConfigurations.IssueTypes.Issue)
        {
            #region → Case issueType is Issue            .
            if (issueType == PrefAppConfigurations.IssueTypes.Issue)
            {
                foreach (var msgIssue in msg.MessageIssues.Where(s => s.Value != null))
                {
                    string unit = string.Empty;
                    if (isSent)
                    {
                        var issue = FilteredIssueSource.
                            Where(s => s.IssueName == msgIssue.Issue.IssueName && s.SentValue == null).FirstOrDefault();
                        if (issue != null)
                        {
                            if (msgIssue.Issue.IssueTypeID == PrefAppConstant.IssueTypes.Numeric)
                            {
                                unit = msgIssue.Issue.NumericIssues.Where(s => s.IssueID == msgIssue.IssueID).First().Unit;
                            }
                            issue.SentValue = msgIssue.Value + " " + unit;
                            issue.SentValueScore = msgIssue.Score.Value;
                        }
                        else
                        {
                            AddNewFilteredIssue(msgIssue);
                        }
                    }
                    else
                    {
                        var issue = FilteredIssueSource.
                            Where(s => s.IssueName == msgIssue.Issue.IssueName && s.ReceivedValue == null).FirstOrDefault();
                        if (issue != null)
                        {
                            if (msgIssue.Issue.IssueTypeID == PrefAppConstant.IssueTypes.Numeric)
                            {
                                unit = msgIssue.Issue.NumericIssues.Where(s => s.IssueID == msgIssue.IssueID).First().Unit;
                            }
                            issue.ReceivedValue = msgIssue.Value + " " + unit;
                            issue.ReceivedValueScore = msgIssue.Score.Value;
                        }
                        else
                        {
                            AddNewFilteredIssue(msgIssue, false);
                        }
                    }
                }
            }
            #endregion
        }

        /// <summary>
        /// Gets the preference set.
        /// </summary>
        /// <param name="SentMessage">The sent message.</param>
        /// <param name="ReceivedMessage">The received message.</param>
        /// <returns></returns>
        private PreferenceSet GetPreferenceSet(ConversationMessage SentMessage, ConversationMessage ReceivedMessage)
        {
            if (SentMessage != null &&
                SentMessage.NegConversation != null &&
                SentMessage.NegConversation.PreferenceSetNeg != null &&
                SentMessage.NegConversation.PreferenceSetNeg.PreferenceSet != null)
            {
                return SentMessage.NegConversation.PreferenceSetNeg.PreferenceSet;
            }


            if (ReceivedMessage != null &&
                ReceivedMessage.NegConversation != null &&
                ReceivedMessage.NegConversation.PreferenceSetNeg != null &&
                ReceivedMessage.NegConversation.PreferenceSetNeg.PreferenceSet != null)
            {
                return ReceivedMessage.NegConversation.PreferenceSetNeg.PreferenceSet;
            }

            return null;
        }
        #endregion

        #region → Public         .

        /// <summary>
        /// Generates the source.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <param name="Filter">The filter.</param>
        /// <returns></returns>
        public ObservableCollection<FilteredIssue> GenerateSource(List<ConversationMessage> msg, string Filter)
        {
            FilteredIssueSource.Clear();

            GetSentAndReceivedMessage(Filter, msg);

            GenerateFilteredIssueSource(MessageSent);
            GenerateFilteredIssueSource(MessageReceived, false);

            return new ObservableCollection<FilteredIssue>(FilteredIssueSource.OrderBy(s => s.IssueName));
        }

        /// <summary>
        /// Generates the source.
        /// </summary>
        /// <param name="SentMessage">The sent message.</param>
        /// <param name="ReceivedMessage">The received message.</param>
        /// <returns></returns>
        public ObservableCollection<FilteredIssue> GenerateSource(ConversationMessage SentMessage, ConversationMessage ReceivedMessage)
        {
            List<ConversationMessage> Msg = new List<ConversationMessage>();
            if (SentMessage != null)
            {
                Msg.Add(SentMessage);
            }

            if (ReceivedMessage != null)
            {
                Msg.Add(ReceivedMessage);
            }
            return GenerateSource(Msg, FilterType.LastData.ToString());
        }

        /// <summary>
        /// Generates the finale neg report.
        /// </summary>
        /// <param name="SentMessage">The sent message.</param>
        /// <param name="ReceivedMessage">The received message.</param>
        /// <returns>report in string format</returns>
        public string GenerateFinaleNegReport(ConversationMessage SentMessage, ConversationMessage ReceivedMessage)
        {

            #region Sample
            /*
Statistical message from Preference App:-

--------------------------------------------------
Last score of received message : 77 %
Last score of sent message     : 24 %
--------------------------------------------------

Current issues and status:-

		Price : 20.00 €-(EUR) (40% / 8%) 
		Colour : building,Wood. (21% / 3%) 
		Options : MVC (8% / 8%) 
		LaterRated : Material,presentation (8% / 5%) 
		Comments : no  (0% / 0%) 
             * 
             */

            #endregion //-------------------------------------

            StringBuilder sp = new StringBuilder();

            ObservableCollection<FilteredIssue> reportList = GenerateSource(SentMessage, ReceivedMessage);

            if (reportList != null && reportList.Count > 0)
            {
                PreferenceSet preferenceSet =this.GetPreferenceSet(SentMessage,ReceivedMessage);

                #region → Message Header          .

                sp.Append("Statistical message from Preference App:-");
                sp.Append(Environment.NewLine);

                #region → Send in case if offer variation happen .

                if (((SentMessage     != null && SentMessage.IsExceedVariation    ) ||
                     (ReceivedMessage != null && ReceivedMessage.IsExceedVariation)) &&
                     preferenceSet!=null && 
                     preferenceSet.Checkvariation)
                {
                    sp.Append(string.Format(" * This offer varies more than {0}% compared to the last offer!",preferenceSet.VariationValue));
                }

                #endregion

                sp.Append(Environment.NewLine);
                sp.Append("--------------------------------------------------");
                sp.Append(Environment.NewLine);
                #endregion//------------------------------------------------------------

                #region → Received message Score  .

                sp.Append("Last score of received message : ");
                if (ReceivedMessage != null && ReceivedMessage.Percentage.HasValue)
                {
                    sp.Append(ReceivedMessage.Percentage.Value.ToString().Replace(".00", ""));
                    sp.Append(" %");
                }
                else
                {
                    sp.Append("N/A");
                }

                sp.Append(Environment.NewLine);


                #endregion//------------------------------------------------------------

                #region → sent message Score      .

                sp.Append("Last score of sent message     : ");
                if (SentMessage != null && SentMessage.Percentage.HasValue)
                {
                    sp.Append(SentMessage.Percentage.Value.ToString().Replace(".00", ""));
                    sp.Append(" %");
                }
                else
                {
                    sp.Append("N/A");
                }
                sp.Append(Environment.NewLine);

                sp.Append("--------------------------------------------------");
                sp.Append(Environment.NewLine);
                sp.Append(Environment.NewLine);
                #endregion//------------------------------------------------------------

                #region → message datails         .

                sp.Append("Current issues and status:-");

                sp.Append(Environment.NewLine);
                sp.Append(Environment.NewLine);


                foreach (var repotLine in reportList)
                {
                    //IssueName:(Value Unit) (Received Score / sent Score)

                    sp.Append(string.Format("\t\t{0} : {1} ({2}% / {3}%) {4}",
                                                           repotLine.IssueName,
                                                           (string.IsNullOrEmpty(repotLine.ReceivedValue) ? string.Empty : repotLine.ReceivedValue).Replace(Environment.NewLine, ","),
                                                           repotLine.ReceivedValueScore.ToString().Replace(".00", ""),
                                                           repotLine.SentValueScore.ToString().Replace(".00", ""),
                                                           Environment.NewLine));

                }

                #endregion//------------------------------------------------------------
            }
            return sp.ToString();
        }

        #endregion

        #endregion
    }
}
