#region → Usings   .
using citPOINT.eNeg.Common;
using citPOINT.PrefApp.Common;
using citPOINT.PrefApp.Data.Web;
using System;
using System.ComponentModel;
//using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Collections.Generic;
using System.ServiceModel.DomainServices.Client;

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

namespace citPOINT.PrefApp.MVVM.UnitTest
{
    /// <summary>
    /// Mock Preference Sets Model
    /// </summary>
    //[Export(typeof(IPreferenceSetsModel))]
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
        /// Occurs when [get organizations for user complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<Organization>> GetOrganizationsForUserComplete;
        
        /// <summary>
        /// Occurs when [Get Preference Set Organizations For User Complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<PreferenceSetOrganization>> GetPreferenceSetOrganizationsForUserComplete;

        /// <summary>
        /// Occurs when [get issue statisticals complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<IssueStatisticalsResult>> GetIssueStatisticalsComplete;

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

        #region → Loading Data   .
     
        /// <summary>
        /// Gets All Main Preference Sets Asynchronously -- > Lookup table have the following data (My Sets, Organization Sets, Set Store)
        /// </summary>
        public void GetMainPreferenceSetAsync()
        {
            if (GetMainPreferenceSetsComplete != null)
            {
                GetMainPreferenceSetsComplete(this,
                    new eNegEntityResultArgs<MainPreferenceSet>(this.MainPreferenceSets));
            }
        }

        /// <summary>
        /// Gets the organizations for user async.
        /// </summary>
        public void GetOrganizationsForUserAsync()
        {
            if (GetOrganizationsForUserComplete != null)
            {
                GetOrganizationsForUserComplete(this,
                    new eNegEntityResultArgs<Organization>(this.Organizations));
            }
        }

        /// <summary>
        /// Gets the preference set organizations for user async.
        /// </summary>
        public void GetPreferenceSetOrganizationsForUserAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the issue statisticals async.
        /// </summary>
        public void GetIssueStatisticalsAsync()
        {
            if (GetIssueStatisticalsComplete != null)
            {
                GetIssueStatisticalsComplete(this,
                    new eNegEntityResultArgs<IssueStatisticalsResult>(this.IssueStatisticals));
            }         
        }

        /// <summary>
        /// Gets All Preference Sets Asynchronously
        /// </summary>
        public void GetPreferenceSetAsync(Guid[] OrganizationIDs)
        {
            if (GetPreferenceSetsComplete != null)
            {
                GetPreferenceSetsComplete(this,
                    new eNegEntityResultArgs<PreferenceSet>(this.PreferenceSets));
            }
        }

        /// <summary>
        /// Gets the issue types async.
        /// </summary>
        public void GetIssueTypesAsync()
        {
            if (GetIssueTypesComplete != null)
            {
                GetIssueTypesComplete(this,
                    new eNegEntityResultArgs<IssueType>(this.IssueTypes));
            }
        }

        /// <summary>
        /// Gets the issues async.
        /// </summary>
        public void GetIssuesAsync(Guid[] OrganizationIDs)
        {
            if (GetIssuesComplete != null)
            {
                GetIssuesComplete(this,
                    new eNegEntityResultArgs<Issue>(this.Issues));
            }
        }

        /// <summary>
        /// Gets the numeric issues async.
        /// </summary>
        public void GetNumericIssuesAsync(Guid[] OrganizationIDs)
        {
            if (GetNumericIssuesComplete != null)
            {
                GetNumericIssuesComplete(this,
                    new eNegEntityResultArgs<NumericIssue>(this.NumericIssues));
            }
        }

        /// <summary>
        /// Gets the option issues async.
        /// </summary>
        public void GetOptionIssuesAsync(Guid[] OrganizationIDs)
        {
            if (GetOptionIssuesComplete != null)
            {
                GetOptionIssuesComplete(this,
                    new eNegEntityResultArgs<OptionIssue>(this.OptionIssues));
            }
        }

        /// <summary>
        /// Gets the later rated issue async.
        /// </summary>
        public void GetLaterRatedIssueAsync(Guid[] OrganizationIDs)
        {
            if (GetLaterRatedIssuesComplete != null)
            {
                GetLaterRatedIssuesComplete(this,
                    new eNegEntityResultArgs<LaterRatedIssue>(this.LaterRatedIssues));
            }
        }

        public PreferenceSet PublishPreferenceSet(PreferenceSet currentPreferenceSet, Guid mainPreferenceSetID, Guid[] organizationIDs)
        {
            #region → Define Variables                                           .

            // All Cloned tables
            List<Entity> allCloneEntities = currentPreferenceSet.CloneAll(null);

            //Reject changes here for reject any changes in the orignal table.
            this.RejectChanges();

            #endregion

            #region → Clone the Current Preference Set and its Related tables    .

            PreferenceSet lastPreferenceSet = null;
            Issue lastIssue = null;

            foreach (var objEntity in allCloneEntities)
            {
                #region → Add Pref Set to context         .
                if (objEntity.GetType().Equals(typeof(PreferenceSet)))
                {
                    MainPreferenceSet mainPreferenceSet = this.MainPreferenceSets
                                                               .Where(s => s.MainPreferenceSetID == mainPreferenceSetID)
                                                               .FirstOrDefault();
                    if (mainPreferenceSet != null)
                    {
                        (objEntity as PreferenceSet).MainPreferenceSet = mainPreferenceSet;
                    }

                    (objEntity as PreferenceSet).MainPreferenceSetID = mainPreferenceSetID;

                    //In case that the publish type is "Publish To Organization Sets"
                    if (mainPreferenceSetID == PrefAppConstant.MainPreferenceSets.OrganizationSets)
                    {
                        foreach (var orgID in organizationIDs)
                        {
                            this.AddPreferenceSetOrganization(true, (objEntity as PreferenceSet), orgID);
                        }
                    }

                    this.PreferenceSets.Add(objEntity as PreferenceSet);

                    lastPreferenceSet = this.PreferenceSets.Where(s => s.PreferenceSetID == (objEntity as PreferenceSet).PreferenceSetID).FirstOrDefault();
                }
                #endregion

                #region → Add Issue to context            .
                else if (objEntity.GetType().Equals(typeof(Issue)))
                {
                    (objEntity as Issue).PreferenceSet = lastPreferenceSet;

                    this.Issues.Add(objEntity as Issue);

                    lastIssue = this.Issues.Where(s => s.IssueID == (objEntity as Issue).IssueID).FirstOrDefault();
                }
                #endregion

                #region → Add NumericIssue to context     .
                else if (objEntity.GetType().Equals(typeof(NumericIssue)))
                {
                    (objEntity as NumericIssue).Issue = lastIssue;

                    this.NumericIssues.Add(objEntity as NumericIssue);
                }
                #endregion

                #region → Add OptionIssue to context      .
                else if (objEntity.GetType().Equals(typeof(OptionIssue)))
                {
                    (objEntity as OptionIssue).Issue = lastIssue;

                    this.OptionIssues.Add(objEntity as OptionIssue);
                }
                #endregion

                #region → Add LaterRatedIssue to context  .
                else if (objEntity.GetType().Equals(typeof(LaterRatedIssue)))
                {
                    (objEntity as LaterRatedIssue).Issue = lastIssue;

                    this.LaterRatedIssues.Add(objEntity as LaterRatedIssue);
                }
                #endregion
            }

            #endregion

            return lastPreferenceSet;
        }

        /// <summary>
        /// Adds the preference set organization.
        /// </summary>
        /// <param name="SetInContext">if set to <c>true</c> [set in context].</param>
        /// <param name="PrefSet">The pref set.</param>
        /// <param name="organizationID">The organization ID.</param>
        /// <returns></returns>
        public PreferenceSetOrganization AddPreferenceSetOrganization(bool SetInContext, PreferenceSet PrefSet, Guid organizationID)
        {
            PreferenceSetOrganization PrefSetOrg = new PreferenceSetOrganization()
            {
                PreferenceSetOrganizationID = Guid.NewGuid(),
                PreferenceSetID = PrefSet.PreferenceSetID,
                PreferenceSet = PrefSet,
                OrganizationID = organizationID,
                Deleted = false,
                DeletedOn = DateTime.Now,
                DeletedBy = PrefSet.UserID
            };
                        
            return PrefSetOrg;
        }

        #endregion

        #region → Add entries    .
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
                IsNewPreferenceSet = true,
                Checkvariation=false,
                VariationValue=0
            };

            if (SetInContext)
            {
                this.Context.PreferenceSets.Add(PrefSet);
            }
            return PrefSet;
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
        #endregion 

        #region → Remove entries .
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
                MailHelper.SendMailToNegotiators(from, to, subject, body);
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
                SaveChangesComplete(this, new SubmitOperationEventArgs(null, null));
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
        
      

    }
}
