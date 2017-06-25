#region → Usings   .
using citPOINT.eNeg.Common;
using citPOINT.PrefApp.Common;
using citPOINT.PrefApp.Data.Web;
using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;

#endregion

#region → History  .

/* Date         User            Change
 * 
 * 23.09.10     Yousra Reda         • creation
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

namespace citPOINT.PrefApp.MVVM.Test
{
    /// <summary>
    /// Mock Preference Sets Model
    /// </summary>
    [Export(typeof(IPreferenceSetsModel))]
    //[PartCreationPolicy(CreationPolicy.NonShared)]
    public class MockPreferenceSetsModel : MockMaster, IPreferenceSetsModel
    {
        #region → Fields         .
        private citPOINT.PrefApp.Common.MailHelper mMailHelper;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets the mail helper.
        /// </summary>
        /// <value>The mail helper.</value>
        private citPOINT.PrefApp.Common.MailHelper MailHelper
        {
            get
            {
                if (mMailHelper == null)
                {

                    mMailHelper = new citPOINT.PrefApp.Common.MailHelper();
                }

                return mMailHelper;
            }
        }

        /// <summary>
        /// instance of PrefAppContext of PrefApp to can use available services
        /// </summary>
        /// <value>The context.</value>
        public PrefAppContext Context
        {
            get
            {
                return base.BaseContext;
            }

        }

        /// <summary>
        /// True if Domain context Has Changes ;otherwise false
        /// </summary>
        /// <value>the has changes.</value>
        public bool HasChanges
        {
            get { return false; }
        }

        /// <summary>
        /// True if either "IsLoading" or "IsSubmitting" is
        /// in progress; otherwise, false
        /// </summary>
        /// <value>the is busy.</value>
        public bool IsBusy
        {
            get { return false; }
        }

        #endregion

        #region → Events         .

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Get All Main Preference Sets CallBack --&gt; Lookup table have the following data (My Sets, Organization Sets, Set Store)
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<MainPreferenceSet>> GetMainPreferenceSetsComplete;

        /// <summary>
        /// Get All Preferenece Sets CallBack
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<PreferenceSet>> GetPreferenceSetsComplete;

        /// <summary>
        /// Occurs when [get issues complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<Issue>> GetIssuesComplete;

        /// <summary>
        /// Occurs when [get issue types complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<IssueType>> GetIssueTypesComplete;

        /// <summary>
        /// Occurs when [get later rated issues complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<LaterRatedIssue>> GetLaterRatedIssuesComplete;

        /// <summary>
        /// Occurs when [get numeric issues complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<NumericIssue>> GetNumericIssuesComplete;

        /// <summary>
        /// Occurs when [get option issues complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<OptionIssue>> GetOptionIssuesComplete;

        /// <summary>
        /// Event Handler For Method SendingMail
        /// </summary>
        public event Action<System.ServiceModel.DomainServices.Client.InvokeOperation> SendingMailCompleted;

        /// <summary>
        /// Occurs when [Saving Changes in Context Complete]
        /// </summary>
        public event EventHandler<SubmitOperationEventArgs> SaveChangesComplete;

        #endregion

        #region → Methods        .

        #region → Public         .

        /// <summary>
        /// Gets All Main Preference Sets Asynchronously -- > Lookup table have the following data (My Sets, Organization Sets, Set Store)
        /// </summary>
        public void GetMainPreferenceSetAsync()
        {
            if (GetMainPreferenceSetsComplete != null)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                    {
                        GetMainPreferenceSetsComplete(this,
                            new eNegEntityResultArgs<MainPreferenceSet>(this.MainPreferenceSets));
                    });
            }
        }

        /// <summary>
        /// Gets All Preference Sets Asynchronously
        /// </summary>
        public void GetPreferenceSetAsync()
        {
            if (GetPreferenceSetsComplete != null)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    GetPreferenceSetsComplete(this,
                        new eNegEntityResultArgs<PreferenceSet>(this.PreferenceSets));
                });
            }
        }

        /// <summary>
        /// Gets the issue types async.
        /// </summary>
        public void GetIssueTypesAsync()
        {
            if (GetIssueTypesComplete != null)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    GetIssueTypesComplete(this,
                        new eNegEntityResultArgs<IssueType>(this.IssueTypes));
                });
            }
        }

        /// <summary>
        /// Gets the issues async.
        /// </summary>
        public void GetIssuesAsync()
        {
            if (GetIssuesComplete != null)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    GetIssuesComplete(this,
                        new eNegEntityResultArgs<Issue>(this.Issues));
                });
            }
        }

        /// <summary>
        /// Gets the numeric issues async.
        /// </summary>
        public void GetNumericIssuesAsync()
        {
            if (GetNumericIssuesComplete != null)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    GetNumericIssuesComplete(this,
                        new eNegEntityResultArgs<NumericIssue>(this.NumericIssues));
                });
            }
        }

        /// <summary>
        /// Gets the option issues async.
        /// </summary>
        public void GetOptionIssuesAsync()
        {
            if (GetOptionIssuesComplete != null)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    GetOptionIssuesComplete(this,
                        new eNegEntityResultArgs<OptionIssue>(this.OptionIssues));
                });
            }
        }

        /// <summary>
        /// Gets the later rated issue async.
        /// </summary>
        public void GetLaterRatedIssueAsync()
        {
            if (GetLaterRatedIssuesComplete != null)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    GetLaterRatedIssuesComplete(this,
                        new eNegEntityResultArgs<LaterRatedIssue>(this.LaterRatedIssues));
                });
            }
        }



        /// <summary>
        /// Add new PreferenceSet
        /// </summary>
        /// <param name="SetInContext">to set PreferenceSet object in Context or not</param>
        /// <returns>Added PreferenceSet</returns>
        public PreferenceSet AddPreferenceSet(bool SetInContext)
        {
            MainPreferenceSet mainPreferenceSet = this.Context.MainPreferenceSets.First(s => s.MainPreferenceSetID == PrefAppConstant.MainPreferenceSets.MySets);

            PreferenceSet PrefSet = new PreferenceSet()
            {
                PreferenceSetID = Guid.NewGuid(),
                PreferenceSetName = "Set 001",
                Deleted = false,
                DeletedOn = DateTime.Now,
                DeletedBy = PrefAppConfigurations.CurrentLoginUser.UserID,
                UserID = PrefAppConfigurations.CurrentLoginUser.UserID,
                MainPreferenceSetID = PrefAppConstant.MainPreferenceSets.MySets,
                MainPreferenceSet = mainPreferenceSet,
                IsNewPreferenceSet = true
            };

            if (SetInContext)
            {
                this.Context.PreferenceSets.Add(PrefSet);
            }

            return PrefSet;
        }


        /// <summary>
        /// Remove PreferenceSet
        /// </summary>
        /// <param name="PrefSet">PrefSet</param>
        public void RemovePreferenceSet(PreferenceSet PrefSet)
        {
            if (this.Context.PreferenceSets.Contains(PrefSet))
            {
                PrefSet = this.Context.PreferenceSets.FirstOrDefault(s => s.PreferenceSetID == PrefSet.PreferenceSetID);


                //Remove Preference Negotaition Related to that Pereference Set.
                while (PrefSet.PreferenceSetNegs.Count() > 0)
                {
                    RemovePreferenceSetNeg(PrefSet.PreferenceSetNegs.FirstOrDefault());
                }


                //Remove Issues
                while (PrefSet.Issues.Count > 0)
                {
                    RemoveIssue(PrefSet.Issues.FirstOrDefault());
                }

                this.Context.PreferenceSets.Remove(PrefSet);
            }
        }

        /// <summary>
        /// Adds the issue.
        /// </summary>
        /// <param name="SetInContext">if set to <c>true</c> [set in context].</param>
        /// <param name="Prefset">The prefset.</param>
        /// <returns>Issue</returns>
        public Issue AddIssue(bool SetInContext, PreferenceSet Prefset)
        {
            Issue issue = new Issue()
            {
                PreferenceSet = Prefset,
                IssueID = Guid.NewGuid(),
                IssueTypeID = PrefAppConstant.IssueTypes.SelectType,
                Deleted = false,
                DeletedOn = DateTime.Now,
                DeletedBy = PrefAppConfigurations.CurrentLoginUser.UserID,
                PreferenceSetID = Prefset.PreferenceSetID,
                IssueWeight = 0,
                IsNewIssue = true,
                IsSelected = false,
                IssueName = "New Issue Name"
            };


            if (SetInContext)
                this.Context.Issues.Add(issue);

            return issue;
        }

        /// <summary>
        /// Adds the option issue.
        /// </summary>
        /// <param name="SetInContext">if set to <c>true</c> [set in context].</param>
        /// <param name="issue">The issue.</param>
        /// <returns>Option Issue</returns>
        public OptionIssue AddOptionIssue(bool SetInContext, Issue issue)
        {
            OptionIssue optionIssue = new OptionIssue()
            {
                OptionIssueID = Guid.NewGuid(),
                Deleted = false,
                DeletedOn = DateTime.Now,
                DeletedBy = PrefAppConfigurations.CurrentLoginUser.UserID,
                OptionIssueWeight = 0,
                IssueID = issue.IssueID,
                Issue = issue,
                IsNewOption = true,
                IsSelected = false,
                OptionIssueValue = "New Option Name"
            };


            if (SetInContext)
                this.Context.OptionIssues.Add(optionIssue);

            return optionIssue;
        }


        /// <summary>
        /// Adds the later rated issue.
        /// </summary>
        /// <param name="SetInContext">if set to <c>true</c> [set in context].</param>
        /// <param name="issue">The issue.</param>
        /// <returns>Later Rated Issue</returns>
        public LaterRatedIssue AddLaterRatedIssue(bool SetInContext, Issue issue)
        {
            LaterRatedIssue laterRatedIssue = new LaterRatedIssue()
            {
                LaterRatedIssueID = Guid.NewGuid(),
                Deleted = false,
                DeletedOn = DateTime.Now,
                DeletedBy = PrefAppConfigurations.CurrentLoginUser.UserID,
                LaterRatedIssueWeight = 0,
                IssueID = issue.IssueID,
                Issue = issue,
                LaterRatedIssueValue = "New Option Name"
            };


            if (SetInContext)
                this.Context.LaterRatedIssues.Add(laterRatedIssue);

            return laterRatedIssue;
        }

        /// <summary>
        /// Adds the numeric issue.
        /// </summary>
        /// <param name="SetInContext">if set to <c>true</c> [set in context].</param>
        /// <param name="issue">The issue.</param>
        /// <returns>Numeric Issue</returns>
        public NumericIssue AddNumericIssue(bool SetInContext, Issue issue)
        {
            NumericIssue numericIssue = new NumericIssue()
            {
                NumericIssueID = Guid.NewGuid(),
                Deleted = false,
                DeletedOn = DateTime.Now,
                DeletedBy = PrefAppConfigurations.CurrentLoginUser.UserID,
                MaximumOperator = (int)(Operators.Equal),
                MinimumOperator = (int)(Operators.Equal),
                MaximumValue = 100,
                MinimumValue = 0,
                OptimumValueEnd = 75,
                OptimumValueStart = 25,
                IssueID = issue.IssueID,
                Issue = issue,
                MaxOperatorBetter = false,
                MaxOperatorEqual = true,
                MaxOperatorWorse = false,
                MinOperatorBetter = false,
                MinOperatorEqual = true,
                MinOperatorWorse = false,
                Unit = "€-(EUR)"


            };


            if (SetInContext)
                this.Context.NumericIssues.Add(numericIssue);

            return numericIssue;
        }

        /// <summary>
        /// Removes the numeric issues.
        /// </summary>
        /// <param name="numericIssue">The numeric issue.</param>
        public void RemoveNumericIssue(NumericIssue numericIssue)
        {
            if (this.Context.NumericIssues.Contains(numericIssue))
            {
                numericIssue = this.Context.NumericIssues.FirstOrDefault(s => s.NumericIssueID == numericIssue.NumericIssueID);

                //Removing all Data matching Related to that Issue
                while (numericIssue.Issue.MessageIssues.Count() > 0)
                {
                    RemoveMessageIssue(numericIssue.Issue.MessageIssues.FirstOrDefault());
                }

                this.Context.NumericIssues.Remove(numericIssue);
            }
        }

        /// <summary>
        /// Removes the option issue.
        /// </summary>
        /// <param name="optionIssue">The option issue.</param>
        public void RemoveOptionIssue(OptionIssue optionIssue)
        {
            if (this.Context.OptionIssues.Contains(optionIssue))
            {
                optionIssue = this.Context.OptionIssues.FirstOrDefault(s => s.OptionIssueID == optionIssue.OptionIssueID);

                //Removing all Data matching Related to that option
                while (optionIssue.MessageOptionIssues.Count() > 0)
                {
                    RemoveMessageOptionIssue(optionIssue.MessageOptionIssues.FirstOrDefault());
                }


                this.Context.OptionIssues.Remove(optionIssue);
            }
        }

        /// <summary>
        /// Removes the later rated issue.
        /// </summary>
        /// <param name="laterRatedIssue">The later rated issue.</param>
        public void RemoveLaterRatedIssue(LaterRatedIssue laterRatedIssue)
        {
            if (this.Context.LaterRatedIssues.Contains(laterRatedIssue))
            {
                laterRatedIssue = this.Context.LaterRatedIssues.FirstOrDefault(s => s.LaterRatedIssueID == laterRatedIssue.LaterRatedIssueID);

                //Removing all Data matching Related to that option
                while (laterRatedIssue.MessageLaterRatedIssues.Count() > 0)
                {
                    RemoveMessageLaterRatedIssue(laterRatedIssue.MessageLaterRatedIssues.FirstOrDefault());
                }


                this.Context.LaterRatedIssues.Remove(laterRatedIssue);
            }
        }

        /// <summary>
        /// Removes the issue.
        /// </summary>
        /// <param name="issue">The issue.</param>
        public void RemoveIssue(Issue issue)
        {
            if (this.Context.Issues.Contains(issue))
            {
                issue = this.Context.Issues.FirstOrDefault(s => s.IssueID == issue.IssueID);


                //Removing all Data matching Related to that Issue
                while (issue.MessageIssues.Count() > 0)
                {
                    RemoveMessageIssue(issue.MessageIssues.FirstOrDefault());
                }


                while (issue.NumericIssues.Count > 0)
                {
                    RemoveNumericIssue(issue.NumericIssues.FirstOrDefault());
                }



                while (issue.OptionIssues.Count > 0)
                {
                    RemoveOptionIssue(issue.OptionIssues.FirstOrDefault());
                }


                while (issue.LaterRatedIssues.Count > 0)
                {
                    RemoveLaterRatedIssue(issue.LaterRatedIssues.FirstOrDefault());
                }


                this.Context.Issues.Remove(issue);
            }
        }


        #region →  Removing Objects related to Data Matching  .

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

                //Removing all Data matching Related to that message
                while (ConvMessage.MessageIssues.Count() > 0)
                {
                    RemoveMessageIssue(ConvMessage.MessageIssues.FirstOrDefault());
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

        #endregion


        /// <summary>
        /// Sends the mail to negotiators.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        public void SendMailToNegotiators(string from, string to, string subject, string body)
        {
            if (SendingMailCompleted != null)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    MailHelper.SendMailToNegotiators(from, to, subject, body);
                });
            }
        }

        /// <summary>
        /// Copies the preference set template.
        /// And its related tables e.g issues ,...
        /// And Negotion also if needed.
        /// </summary>
        /// <param name="currentPreferenceSet">The current preference set.</param>
        /// <param name="preferenceSetNegIDs">The preference set neg Ids.</param>
        /// <returns>New Preference sets</returns>
        public PreferenceSet CopyPreferenceSetTemplate(PreferenceSet currentPreferenceSet, System.Collections.Generic.List<Guid> preferenceSetNegIDs)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Save any pending changes asynchronously
        /// </summary>
        public void SaveChangesAsync()
        {
            if (SaveChangesComplete != null)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    SaveChangesComplete(this, new SubmitOperationEventArgs(null, null));
                });
            }
        }

        /// <summary>
        /// Rejects the changes.
        /// </summary>
        public void RejectChanges()
        {
            Context.RejectChanges();
        }
        #endregion

        #endregion





        public event EventHandler<eNegEntityResultArgs<PreferenceSetOrganization>> GetPreferenceSetOrganizationsForUserComplete;

        public event EventHandler<eNegEntityResultArgs<Organization>> GetOrganizationsForUserComplete;

        public event EventHandler<eNegEntityResultArgs<IssueStatisticalsResult>> GetIssueStatisticalsComplete;

        public void GetPreferenceSetOrganizationsForUserAsync()
        {
            throw new NotImplementedException();
        }

        public void GetOrganizationsForUserAsync()
        {
            throw new NotImplementedException();
        }

        public void GetIssueStatisticalsAsync()
        {
            throw new NotImplementedException();
        }

        public PreferenceSet PublishPreferenceSet(PreferenceSet currentPreferenceSet, Guid mainPreferenceSetID, Guid[] organizationIDs)
        {
            throw new NotImplementedException();
        }

        public void GetPreferenceSetAsync(Guid[] OrganizationIDs)
        {
            throw new NotImplementedException();
        }

        public void GetIssuesAsync(Guid[] OrganizationIDs)
        {
            throw new NotImplementedException();
        }

        public void GetNumericIssuesAsync(Guid[] OrganizationIDs)
        {
            throw new NotImplementedException();
        }

        public void GetOptionIssuesAsync(Guid[] OrganizationIDs)
        {
            throw new NotImplementedException();
        }

        public void GetLaterRatedIssueAsync(Guid[] OrganizationIDs)
        {
            throw new NotImplementedException();
        }

        public PreferenceSetOrganization AddPreferenceSetOrganization(bool SetInContext, PreferenceSet PrefSet, Guid organizationID)
        {
            throw new NotImplementedException();
        }
    }
}
