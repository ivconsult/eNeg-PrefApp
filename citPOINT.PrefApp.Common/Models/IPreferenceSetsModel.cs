
#region → Usings   .
using System;
using System.ComponentModel;
using citPOINT.PrefApp.Data.Web;
using citPOINT.eNeg.Common;
using System.ServiceModel.DomainServices.Client;
using System.Collections.Generic;
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

namespace citPOINT.PrefApp.Common
{
    /// <summary>
    /// Interface for PreferenceSetsModel
    /// </summary>
    public interface IPreferenceSetsModel : INotifyPropertyChanged
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
        /// Gets the context.
        /// </summary>
        /// <value>The context.</value>
        PrefAppContext Context { get; }

        #endregion Properties

        #region → Events         .

        /// <summary>
        /// Occurs when [get preference set organizations for user complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<PreferenceSetOrganization>> GetPreferenceSetOrganizationsForUserComplete;

        /// <summary>
        /// Occurs when [get organizations for user complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<Organization>> GetOrganizationsForUserComplete;

        /// <summary>
        /// Event Handler For Method SendingMail
        /// </summary>
        event Action<InvokeOperation> SendingMailCompleted;

        /// <summary>
        /// Get All Main Preference Sets CallBack --> Lookup table have the following data (My Sets, Organization Sets, Set Store)
        /// </summary>
        event EventHandler<eNegEntityResultArgs<MainPreferenceSet>> GetMainPreferenceSetsComplete;

        /// <summary>
        /// Get All Preferenece Sets CallBack
        /// </summary>
        event EventHandler<eNegEntityResultArgs<PreferenceSet>> GetPreferenceSetsComplete;
        
        /// <summary>
        /// Occurs when [get issues complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<Issue>> GetIssuesComplete;

        /// <summary>
        /// Occurs when [get issue types complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<IssueType>> GetIssueTypesComplete;

        /// <summary>
        /// Occurs when [get later rated issues complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<LaterRatedIssue>> GetLaterRatedIssuesComplete;

        /// <summary>
        /// Occurs when [get numeric issues complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<NumericIssue>> GetNumericIssuesComplete;

        /// <summary>
        /// Occurs when [get option issues complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<OptionIssue>> GetOptionIssuesComplete;

        /// <summary>
        /// SaveChangesComplete
        /// </summary>
        event EventHandler<SubmitOperationEventArgs> SaveChangesComplete;

        #endregion Events

        #region → Methods        .

        /// <summary>
        /// Gets the preference set organizations for user async.
        /// </summary>
        void GetPreferenceSetOrganizationsForUserAsync();

        /// <summary>
        /// Gets the organizations for user async.
        /// </summary>
        void GetOrganizationsForUserAsync();
        
        /// <summary>
        /// Copies the preference set template.
        /// And its related tables e.g issues ,...
        /// And Negotion also if needed.
        /// </summary>
        /// <param name="currentPreferenceSet">The current preference set.</param>
        /// <param name="preferenceSetNegIDs">The preference set neg Ids.</param>
        /// <returns>New Preference sets</returns>
        PreferenceSet CopyPreferenceSetTemplate(PreferenceSet currentPreferenceSet, List<Guid> preferenceSetNegIDs);

        /// <summary>
        /// Publishes the preference set.
        /// </summary>
        /// <param name="currentPreferenceSet">The current preference set.</param>
        /// <param name="mainPreferenceSetID">The main preference set ID.</param>
        /// <param name="organizationIDs">The organization I ds.</param>
        /// <returns></returns>
        PreferenceSet PublishPreferenceSet(PreferenceSet currentPreferenceSet, Guid mainPreferenceSetID, Guid[] organizationIDs);
        
        /// <summary>
        /// Sends the mail to negotiators.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        void SendMailToNegotiators(string from, string to, string subject, string body);

        /// <summary>
        /// Gets All Main Preference Sets Asynchronously -- > Lookup table have the following data (My Sets, Organization Sets, Set Store)
        /// </summary>
        void GetMainPreferenceSetAsync();
        
        /// <summary>
        /// Gets the preference set async.
        /// </summary>
        /// <param name="OrganizationIDs">The organization I ds.</param>
        void GetPreferenceSetAsync(Guid[] OrganizationIDs);
        
        /// <summary>
        /// Gets the issue types async.
        /// </summary>
        void GetIssueTypesAsync();

        /// <summary>
        /// Gets the issues async.
        /// </summary>
        /// <param name="OrganizationIDs">The organization I ds.</param>
        void GetIssuesAsync(Guid[] OrganizationIDs);

        /// <summary>
        /// Gets the numeric issues async.
        /// </summary>
        /// <param name="OrganizationIDs">The organization I ds.</param>
        void GetNumericIssuesAsync(Guid[] OrganizationIDs);

        /// <summary>
        /// Gets the option issues async.
        /// </summary>
        /// <param name="OrganizationIDs">The organization I ds.</param>
        void GetOptionIssuesAsync(Guid[] OrganizationIDs);

        /// <summary>
        /// Gets the later rated issue async.
        /// </summary>
        /// <param name="OrganizationIDs">The organization I ds.</param>
        void GetLaterRatedIssueAsync(Guid[] OrganizationIDs);

        /// <summary>
        /// Remove PreferenceSet
        /// </summary>
        /// <param name="PrefSet">The pref set.</param>
        void RemovePreferenceSet(PreferenceSet PrefSet);

        /// <summary>
        /// Add new PreferenceSet
        /// </summary>
        /// <param name="SetInContext">to set PreferenceSet object in Context or not</param>
        /// <returns>Added PreferenceSet</returns>
        PreferenceSet AddPreferenceSet(bool SetInContext);

        /// <summary>
        /// Adds the preference set organization.
        /// </summary>
        /// <param name="SetInContext">if set to <c>true</c> [set in context].</param>
        /// <param name="PrefSet">The pref set.</param>
        /// <param name="organizationID">The organization ID.</param>
        /// <returns></returns>
        PreferenceSetOrganization AddPreferenceSetOrganization(bool SetInContext, PreferenceSet PrefSet, Guid organizationID);

        /// <summary>
        /// Adds the issue.
        /// </summary>
        /// <param name="SetInContext">if set to <c>true</c> [set in context].</param>
        /// <param name="Prefset">The prefset.</param>
        /// <returns></returns>
        Issue AddIssue(bool SetInContext, PreferenceSet Prefset);

        /// <summary>
        /// Adds the option issue.
        /// </summary>
        /// <param name="SetInContext">if set to <c>true</c> [set in context].</param>
        /// <param name="issue">The issue.</param>
        /// <returns></returns>
        OptionIssue AddOptionIssue(bool SetInContext, Issue issue);

        /// <summary>
        /// Adds the numeric issue.
        /// </summary>
        /// <param name="SetInContext">if set to <c>true</c> [set in context].</param>
        /// <param name="issue">The issue.</param>
        /// <returns></returns>
        NumericIssue AddNumericIssue(bool SetInContext, Issue issue);

        /// <summary>
        /// Adds the later rated issue.
        /// </summary>
        /// <param name="SetInContext">if set to <c>true</c> [set in context].</param>
        /// <param name="issue">The issue.</param>
        /// <returns></returns>
        LaterRatedIssue AddLaterRatedIssue(bool SetInContext, Issue issue); 

        /// <summary>
        /// Removes the issue.
        /// </summary>
        /// <param name="issue">The issue.</param>
        void RemoveIssue(Issue issue);
                
        /// <summary>
        /// Removes the later rated issue.
        /// </summary>
        /// <param name="laterRatedIssue">The later rated issue.</param>
        void RemoveLaterRatedIssue(LaterRatedIssue laterRatedIssue);

        /// <summary>
        /// Removes the option issue.
        /// </summary>
        /// <param name="optionIssue">The option issue.</param>
        void RemoveOptionIssue(OptionIssue optionIssue);

        /// <summary>
        /// Removes the numeric issues.
        /// </summary>
        /// <param name="numericIssue">The numeric issue.</param>
        void RemoveNumericIssue(NumericIssue numericIssue);

        #region →  Removing Objects related to datamatching  .

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
        /// <param name="MsgIssue"></param>
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
        
        #endregion
        
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
