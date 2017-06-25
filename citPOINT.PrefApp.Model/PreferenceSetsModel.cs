
#region → Usings   .

using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ServiceModel.DomainServices.Client;
using citPOINT.eNeg.Common;
using citPOINT.PrefApp.Common;
using citPOINT.PrefApp.Data.Web;
using System.Linq;
using System.Threading;
using System.Collections.Generic;
using citPOINT.PrefApp.Data;
using citPOINT.PrefApp.Common.Helper;

#endregion

#region → History  .

/* Date         User              Change
 * 
 * 23.09.10     Yousra Reda         • creation
 * 02.11.10     M.Wahab             • Creation
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

namespace citPOINT.PrefApp.Model
{

    #region Using MEF to export PreferenceSetsViewModel

    /// <summary>
    /// PreferenceSets Model
    /// </summary>
    [Export(typeof(IPreferenceSetsModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    #endregion
    public class PreferenceSetsModel : IPreferenceSetsModel
    {

        #region → Fields         .

        private Boolean mHasChanges = false;
        private Boolean mIsBusy = false;
        DateTime LastActionDate = DateTime.Now;
        private citPOINT.PrefApp.Common.MailHelper mMailHelper;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Property with getter only Used to send mail to user when he request login
        /// </summary>
        private citPOINT.PrefApp.Common.MailHelper MailHelper
        {
            get
            {
                if (mMailHelper == null)
                {

                    mMailHelper = new citPOINT.PrefApp.Common.MailHelper();

                    mMailHelper.MailSendComplete += new Action<InvokeOperation>(SendingMailCompleted);
                }

                return mMailHelper;
            }
        }


        /// <summary>
        /// Context of Service eNegService
        /// </summary>
        public PrefAppContext Context
        {
            get
            {
                return Repository.Context;
            }
        }

        /// <summary>
        /// True if Domain context Has Changes ;otherwise false
        /// </summary>
        public Boolean HasChanges
        {
            get
            {
                return this.mHasChanges;
            }
            private set
            {
                if (this.mHasChanges != value)
                {

                    this.mHasChanges = value;
                    this.OnPropertyChanged("HasChanges");
                }
            }
        }

        /// <summary>
        /// True if either "IsLoading" or "IsSubmitting" is
        /// in progress; otherwise, false
        /// </summary>
        public Boolean IsBusy
        {
            get
            {
                return this.mIsBusy;
            }

            private set
            {
                if (this.mIsBusy != value)
                {
                    this.mIsBusy = value;
                    this.OnPropertyChanged("IsBusy");
                }
            }
        }

        #endregion

        #region → Contructor     .

        /// <summary>
        /// Initializes a new instance of the <see cref="DataMatchingModel"/> class.
        /// </summary>
        public PreferenceSetsModel()
        {
            Repository.Context.PropertyChanged += new PropertyChangedEventHandler(mPrefAppContext_PropertyChanged);
        }

        #endregion

        #region → Event Handlers .

        /// <summary>
        /// Executed when any property of Domain context is changed
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">PropertyChangedEventArgs</param>
        private void mPrefAppContext_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "HasChanges":
                    this.HasChanges = this.Context.HasChanges;
                    break;
                case "IsLoading":
                    this.IsBusy = this.Context.IsLoading;
                    break;
                case "IsSubmitting":
                    this.IsBusy = this.Context.IsSubmitting;
                    break;
            }
        }
        #endregion

        #region → Events         .

        /// <summary>
        /// Event Handler For Method SendingMail
        /// </summary>
        public event Action<InvokeOperation> SendingMailCompleted;

        /// <summary>
        /// PropertyChanged Callback
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Occurs when [get preference set organizations for user complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<PreferenceSetOrganization>> GetPreferenceSetOrganizationsForUserComplete;

        /// <summary>
        /// Occurs when [get organizations for user complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<Organization>> GetOrganizationsForUserComplete;

        /// <summary>
        /// Get All Main Preference Sets CallBack --> Lookup table have the following data (My Sets, Organization Sets, Set Store)
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
        /// SaveChangesComplete
        /// </summary>
        public event EventHandler<SubmitOperationEventArgs> SaveChangesComplete;
        #endregion

        #region → Methods        .

        #region → Private        .
        
        /// <summary>
        /// Private Method used to perform query on certain entity of PrefApp Entities
        /// </summary>
        /// <typeparam name="T">Value Of T</typeparam>
        /// <param name="qry">Value Of qry</param>
        /// <param name="evt">Value Of evt</param>
        private void PerformQuery<T>(EntityQuery<T> qry, EventHandler<eNegEntityResultArgs<T>> evt) where T : Entity
        {

            Context.Load<T>(qry, LoadBehavior.RefreshCurrent, r =>
            {
                if (evt != null)
                {
                    try
                    {
                        if (r.HasError)
                        {
                            evt(this, new eNegEntityResultArgs<T>(r.Error));
                            r.MarkErrorAsHandled();
                        }
                        else
                        {
                            evt(this, new eNegEntityResultArgs<T>(r.Entities));
                        }
                    }
                    catch (Exception ex)
                    {
                        evt(this, new eNegEntityResultArgs<T>(ex));
                    }
                }
            }, null);
        }
        
        /// <summary>
        /// Gets the name of the item.
        /// </summary>
        /// <param name="preName">Name of the pre.</param>
        /// <returns>New Name have postfix of current date time</returns>
        private string GetItemName(string preName)
        {
            TimeSpan span = DateTime.Now.Subtract(LastActionDate);

            if (span.TotalSeconds < 1)
            {
                Thread.Sleep(1000 - (int)span.TotalMilliseconds);
            }

            LastActionDate = DateTime.Now;

            return preName + DateTime.Now.ToString(" (MM/dd/yyyy HH:mm:ss)");
        }

        /// <summary>
        /// Gets the new name of the preference.
        /// </summary>
        /// <param name="oldPreferenceSet">The old preference set.</param>
        /// <param name="preferenceSetNegIDs">The preference set neg Ids.</param>
        /// <returns>a string New Name "prefernce Set Name " + For +" Negotiation Name"</returns>
        private string GetNewPreferenceName(PreferenceSet oldPreferenceSet, ICollection<Guid> preferenceSetNegIDs)
        {
            string NegNames = string.Empty;

            // Collect all negotiation Names e.g "Car for My Self"
            foreach (var neg in oldPreferenceSet.PreferenceSetNegs.Where(s => preferenceSetNegIDs.Contains(s.PreferenceSetNegID)))
            {
                NegNames += "," + neg.Name;
            }

            // e.g "Purchase a Car"
            string NewPreferenceName = oldPreferenceSet.PreferenceSetName;


            if (!string.IsNullOrEmpty(NegNames))
            {
                NewPreferenceName += " for " + NegNames.Substring(1);
            }
            else
            {
                //Clone Only Preference Set.
                NewPreferenceName += "_Copy";
            }

            //Max Length
            if (NewPreferenceName.Length > 300)
            {
                NewPreferenceName = NewPreferenceName.Substring(0, NewPreferenceName.Length - 1);
            }

            //Check name uniqueness
            if (this.Context.PreferenceSets.Where(s => s.PreferenceSetName == NewPreferenceName).Count() > 0)
            {
                NewPreferenceName = GetItemName("Preference Set");
            }

            return NewPreferenceName;

        }

        /// <summary>
        /// Maps the pending items.
        /// </summary>
        /// <param name="mapperTables">The mapper tables.</param>
        private static void MapPendingItems(IEnumerable<MapperTable> mapperTables)
        {
            if (PrefAppConfigurations.PendingItems==null)
            {
                return;
            }

            foreach (var pendingItem in PrefAppConfigurations.PendingItems)
            {
                MapperTable mapperItem;
                switch (pendingItem.PendingType)
                {
                    case PrefAppConfigurations.IssueTypes.Issue:
                        mapperItem = mapperTables.FirstOrDefault(s => s.OldGuid == pendingItem.PendingID && s.TableName == TableNames.Issue);
                        if (mapperItem != null)
                        {
                            pendingItem.PendingID = mapperItem.NewGuid;
                        }
                        else
                        {
                            pendingItem.PendingID = Guid.Empty;
                        }
                        break;
                    case PrefAppConfigurations.IssueTypes.Option:
                        mapperItem = mapperTables.FirstOrDefault(s => s.OldGuid == pendingItem.PendingID && s.TableName == TableNames.OptionIssue);
                        if (mapperItem != null)
                        {
                            pendingItem.PendingID = mapperItem.NewGuid;
                        }
                        else
                        {
                            pendingItem.PendingID = Guid.Empty;
                        }
                        break;
                    case PrefAppConfigurations.IssueTypes.LaterRated:
                        mapperItem = mapperTables.FirstOrDefault(s => s.OldGuid == pendingItem.PendingID && s.TableName == TableNames.LaterRatedIssue);
                        if (mapperItem != null)
                        {
                            pendingItem.PendingID = mapperItem.NewGuid;
                        }
                        else
                        {
                            pendingItem.PendingID = Guid.Empty;
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
        
        #endregion

        #region → Protected      .


        #region "INotifyPropertyChanged Interface implementation"

        /// <summary>
        /// called On Property Changed
        /// </summary>
        /// <param name="propertyName">property name</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion "INotifyPropertyChanged Interface implementation"

        #endregion

        #region → Public         .

        #region IPrefernceSetsModel Interface Implementation

        /// <summary>
        /// Gets the preference set organizations for user async.
        /// </summary>
        public void GetPreferenceSetOrganizationsForUserAsync()
        {
            PerformQuery<PreferenceSetOrganization>(Context.GetPreferenceSetOrganizationsForUserQuery(PrefAppConfigurations.CurrentLoginUser.UserID), GetPreferenceSetOrganizationsForUserComplete);
        }

        /// <summary>
        /// Gets the organizations for user async.
        /// </summary>
        public void GetOrganizationsForUserAsync()
        {
            PerformQuery<Organization>(Context.GetOrganizationsForUserQuery(PrefAppConfigurations.CurrentLoginUser.UserID), GetOrganizationsForUserComplete);
        }

        /// <summary>
        /// Sends the mail to negotiators.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        public void SendMailToNegotiators(string from, string to, string subject, string body)
        {
            this.MailHelper.SendMailToNegotiators(from, to, subject, body);
        }

        /// <summary>
        /// Gets All Main Preference Sets Asynchronously -- > Lookup table have the following data (My Sets, Organization Sets, Set Store)
        /// </summary>
        public void GetMainPreferenceSetAsync()
        {
            PerformQuery<MainPreferenceSet>(Context.GetMainPreferenceSetsQuery(), GetMainPreferenceSetsComplete);
        }

        /// <summary>
        /// Gets the issue types async.
        /// </summary>
        public void GetIssueTypesAsync()
        {
            PerformQuery<IssueType>(Context.GetIssueTypesQuery(), GetIssueTypesComplete);
        }

        /// <summary>
        /// Gets All Preference Sets Asynchronously
        /// </summary>
        public void GetPreferenceSetAsync(Guid[] OrganizationIDs)
        {
            PerformQuery<PreferenceSet>(Context.GetPreferenceSetsForUserQuery(PrefAppConfigurations.CurrentLoginUser.UserID, OrganizationIDs), GetPreferenceSetsComplete);
        }

        /// <summary>
        /// Gets the issues async.
        /// </summary>
        public void GetIssuesAsync(Guid[] OrganizationIDs)
        {
            PerformQuery<Issue>(Context.GetIssuesRelatedToSpecificUserQuery(PrefAppConfigurations.CurrentLoginUser.UserID, OrganizationIDs), GetIssuesComplete);
        }

        /// <summary>
        /// Gets the numeric issues async.
        /// </summary>
        public void GetNumericIssuesAsync(Guid[] OrganizationIDs)
        {
            PerformQuery<NumericIssue>(Context.GetNumericIssuesRelatedToSpecificUserQuery(PrefAppConfigurations.CurrentLoginUser.UserID, OrganizationIDs), GetNumericIssuesComplete);
        }

        /// <summary>
        /// Gets the option issues async.
        /// </summary>
        public void GetOptionIssuesAsync(Guid[] OrganizationIDs)
        {
            PerformQuery<OptionIssue>(Context.GetOptionIssuesRelatedToSpecificUserQuery(PrefAppConfigurations.CurrentLoginUser.UserID, OrganizationIDs), GetOptionIssuesComplete);
        }

        /// <summary>
        /// Gets the later rated issue async.
        /// </summary>
        public void GetLaterRatedIssueAsync(Guid[] OrganizationIDs)
        {
            PerformQuery<LaterRatedIssue>(Context.GetLaterRatedIssuesRelatedToSpecificUserQuery(PrefAppConfigurations.CurrentLoginUser.UserID, OrganizationIDs), GetLaterRatedIssuesComplete);
        }

        /// <summary>
        /// Publishes the preference set.
        /// </summary>
        /// <param name="currentPreferenceSet">The current preference set.</param>
        /// <param name="mainPreferenceSetID">The main preference set ID.</param>
        /// <param name="organizationIDs">The organization I ds.</param>
        /// <returns></returns>
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
                    MainPreferenceSet mainPreferenceSet = this.Context
                                                               .MainPreferenceSets
                                                               .Where(s => s.MainPreferenceSetID == mainPreferenceSetID)
                                                               .FirstOrDefault();
                    if (mainPreferenceSet != null)
                    {
                        (objEntity as PreferenceSet).MainPreferenceSet = mainPreferenceSet;
                    }

                    (objEntity as PreferenceSet).MainPreferenceSetID = mainPreferenceSetID;

                    (objEntity as PreferenceSet).UserID = PrefAppConfigurations.CurrentLoginUser.UserID;
                    (objEntity as PreferenceSet).DeletedBy = PrefAppConfigurations.CurrentLoginUser.UserID;

                    //In case that the publish type is "Publish To Organization Sets"
                    if (mainPreferenceSetID == PrefAppConstant.MainPreferenceSets.OrganizationSets)
                    {
                        foreach (var orgID in organizationIDs)
                        {
                            this.AddPreferenceSetOrganization(true, (objEntity as PreferenceSet), orgID);
                        }
                    }

                    this.Context.PreferenceSets.Add(objEntity);

                    lastPreferenceSet = this.Context.PreferenceSets.Where(s => s.PreferenceSetID == (objEntity as PreferenceSet).PreferenceSetID).FirstOrDefault();
                }
                #endregion

                #region → Add Issue to context            .
                else if (objEntity.GetType().Equals(typeof(Issue)))
                {
                    (objEntity as Issue).PreferenceSet = lastPreferenceSet;

                    (objEntity as Issue).DeletedBy = PrefAppConfigurations.CurrentLoginUser.UserID;

                    this.Context.Issues.Add(objEntity);

                    lastIssue = this.Context.Issues.Where(s => s.IssueID == (objEntity as Issue).IssueID).FirstOrDefault();
                }
                #endregion

                #region → Add NumericIssue to context     .
                else if (objEntity.GetType().Equals(typeof(NumericIssue)))
                {
                    (objEntity as NumericIssue).Issue = lastIssue;
                    (objEntity as NumericIssue).DeletedBy = PrefAppConfigurations.CurrentLoginUser.UserID;
                    this.Context.NumericIssues.Add(objEntity);
                }
                #endregion

                #region → Add OptionIssue to context      .
                else if (objEntity.GetType().Equals(typeof(OptionIssue)))
                {
                    (objEntity as OptionIssue).Issue = lastIssue;
                    (objEntity as OptionIssue).DeletedBy = PrefAppConfigurations.CurrentLoginUser.UserID;
                    this.Context.OptionIssues.Add(objEntity);
                }
                #endregion

                #region → Add LaterRatedIssue to context  .
                else if (objEntity.GetType().Equals(typeof(LaterRatedIssue)))
                {
                    (objEntity as LaterRatedIssue).Issue = lastIssue;
                    (objEntity as LaterRatedIssue).DeletedBy = PrefAppConfigurations.CurrentLoginUser.UserID;
                    this.Context.LaterRatedIssues.Add(objEntity);
                }
                #endregion
            }

            #endregion

            return lastPreferenceSet;
        }

        /// <summary>
        /// Copies the preference set template.
        /// And its related tables e.g issues ,...
        /// And Negotion also if needed.
        /// </summary>
        /// <param name="currentPreferenceSet">The current preference set.</param>
        /// <param name="preferenceSetNegIDs">The preference set neg Ids.</param>
        /// <returns>New Preference sets</returns>
        public PreferenceSet CopyPreferenceSetTemplate(PreferenceSet currentPreferenceSet, List<Guid> preferenceSetNegIDs)
        {
            #region → Define Variables                                           .
            // Mapper Table that map fromOld Issues to New One (Clone One).
            List<MapperTable> mapperTables = new List<MapperTable>();

            // All Cloned tables
            List<Entity> allCloneEntities = currentPreferenceSet.CloneAll(preferenceSetNegIDs);

            // Actual mapper table.
            mapperTables = currentPreferenceSet.MapperTables;

            //Reject changes here for reject any changes in the orignal table.
            this.RejectChanges();

            MapPendingItems(mapperTables);

            //List all negotiation that will transfer to new prefence Set.
            List<PreferenceSetNeg> preferenceSetNegList = currentPreferenceSet.PreferenceSetNegs.Where(s => preferenceSetNegIDs.Contains(s.PreferenceSetNegID)).ToList();

            #endregion

            #region → Clone the Current Preference Set and its Related tables    .

            PreferenceSet lastPreferenceSet = null;
            Issue lastIssue = null;


            foreach (var objEntity in allCloneEntities)
            {
                if (objEntity.GetType().Equals(typeof(PreferenceSet)))
                {
                    ((PreferenceSet)objEntity).MainPreferenceSet = currentPreferenceSet.MainPreferenceSet;

                    (objEntity as PreferenceSet).PreferenceSetName = GetNewPreferenceName(currentPreferenceSet, preferenceSetNegIDs);

                    this.Context.PreferenceSets.Add(objEntity);

                    lastPreferenceSet = this.Context.PreferenceSets.Where(s => s.PreferenceSetID == (objEntity as PreferenceSet).PreferenceSetID).FirstOrDefault();
                }
                else if (objEntity.GetType().Equals(typeof(Issue)))
                {
                    ((Issue)objEntity).PreferenceSet = lastPreferenceSet;

                    this.Context.Issues.Add(objEntity);

                    lastIssue = this.Context.Issues.Where(s => s.IssueID == (objEntity as Issue).IssueID).FirstOrDefault();
                }

                else if (objEntity.GetType().Equals(typeof(NumericIssue)))
                {
                    ((NumericIssue)objEntity).Issue = lastIssue;

                    this.Context.NumericIssues.Add(objEntity);
                }
                else if (objEntity.GetType().Equals(typeof(OptionIssue)))
                {
                    ((OptionIssue)objEntity).Issue = lastIssue;

                    this.Context.OptionIssues.Add(objEntity);
                }
                else if (objEntity.GetType().Equals(typeof(LaterRatedIssue)))
                {
                    ((LaterRatedIssue)objEntity).Issue = lastIssue;

                    this.Context.LaterRatedIssues.Add(objEntity);
                }
            }

            #endregion

            #region → Modifing tables related to Data Matching                   .


            var tmpDeletedMessageIssue = new List<MessageIssue>();

            foreach (var messageIssue in this.Context.MessageIssues.Where(ss => preferenceSetNegIDs.Contains(ss.ConversationMessage.NegConversation.PreferenceSetNegID)).ToList())
            {
                //Exist before Reject Changes
                if (mapperTables.Where(ss => ss.TableName == TableNames.MessageIssue && ss.OldGuid == messageIssue.MessageIssueID).Count() > 0)
                {
                    MapperTable mapper = mapperTables.Where(ss => ss.TableName == TableNames.Issue && ss.OldGuid == messageIssue.IssueID).FirstOrDefault();

                    messageIssue.Issue = this.Context.Issues.Where(s => s.IssueID == mapper.NewGuid).FirstOrDefault();
                    messageIssue.IssueID = mapper.NewGuid;
                }
                else
                {
                    tmpDeletedMessageIssue.Add(messageIssue);
                }
            }

            //Delete Rows in case if changes was deleting its parent.
            // e.g change type from numeric to options
            while (tmpDeletedMessageIssue.Count() > 0)
            {
                this.RemoveMessageIssue(tmpDeletedMessageIssue[0]);
                tmpDeletedMessageIssue.Remove(tmpDeletedMessageIssue[0]);
            }

            //========================================================================================================

            var tmpDeletedMessageOptionIssue = new List<MessageOptionIssue>();

            foreach (var messageOptionIssue in this.Context.MessageOptionIssues.Where(ss => ss.MessageIssue != null && preferenceSetNegIDs.Contains(ss.MessageIssue.ConversationMessage.NegConversation.PreferenceSetNegID)).ToList())
            {

                //Exist before Reject Changes
                if (mapperTables.Where(ss => ss.TableName == TableNames.MessageOptionIssue && ss.OldGuid == messageOptionIssue.MessageOptionIssueID).Count() > 0)
                {
                    MapperTable mapper = mapperTables.Where(ss => ss.TableName == TableNames.OptionIssue && ss.OldGuid == messageOptionIssue.OptionIssueID).FirstOrDefault();

                    messageOptionIssue.OptionIssue = this.Context.OptionIssues.Where(s => s.OptionIssueID == mapper.NewGuid).FirstOrDefault();

                    messageOptionIssue.OptionIssueID = mapper.NewGuid;
                }
                else
                {
                    tmpDeletedMessageOptionIssue.Add(messageOptionIssue);
                }
            }

            while (tmpDeletedMessageOptionIssue.Count() > 0)
            {
                this.RemoveMessageOptionIssue(tmpDeletedMessageOptionIssue[0]);
                tmpDeletedMessageOptionIssue.Remove(tmpDeletedMessageOptionIssue[0]);
            }


            //========================================================================================================




            var tmpDeletedMessageLaterRatedIssue = new List<MessageLaterRatedIssue>();

            foreach (var messageLaterRatedIssue in this.Context.MessageLaterRatedIssues.Where(ss => ss.MessageIssue != null && preferenceSetNegIDs.Contains(ss.MessageIssue.ConversationMessage.NegConversation.PreferenceSetNegID)).ToList())
            {

                //Exist before Reject Changes
                if (mapperTables.Where(ss => ss.TableName == TableNames.MessageLaterRatedIssue && ss.OldGuid == messageLaterRatedIssue.MessageLaterRatedIssueID).Count() > 0)
                {
                    MapperTable mapper = mapperTables.Where(ss => ss.TableName == TableNames.LaterRatedIssue && ss.OldGuid == messageLaterRatedIssue.LaterRatedIssueID).FirstOrDefault();

                    messageLaterRatedIssue.LaterRatedIssue = this.Context.LaterRatedIssues.Where(s => s.LaterRatedIssueID == mapper.NewGuid).FirstOrDefault();
                    messageLaterRatedIssue.LaterRatedIssueID = mapper.NewGuid;
                }
                else
                {
                    tmpDeletedMessageLaterRatedIssue.Add(messageLaterRatedIssue);
                }
            }

            while (tmpDeletedMessageLaterRatedIssue.Count() > 0)
            {
                this.RemoveMessageLaterRatedIssue(tmpDeletedMessageLaterRatedIssue[0]);
                tmpDeletedMessageLaterRatedIssue.Remove(tmpDeletedMessageLaterRatedIssue[0]);
            }

            //========================================================================================================

            #endregion

            #region → Updation Negotiation with new Preference Set               .


            foreach (var prefsetNeg in this.Context.PreferenceSetNegs.Where(ss => preferenceSetNegIDs.Contains(ss.PreferenceSetNegID)))
            {
                prefsetNeg.PreferenceSet = lastPreferenceSet;
                prefsetNeg.PreferenceSetID = lastPreferenceSet.PreferenceSetID;
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

            if (SetInContext)
            {
                this.Context.PreferenceSetOrganizations.Add(PrefSetOrg);
            }
            return PrefSetOrg;
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
                PreferenceSetName = GetItemName("Set"),
                Deleted = false,
                DeletedOn = DateTime.Now,
                DeletedBy = PrefAppConfigurations.CurrentLoginUser.UserID,
                UserID = PrefAppConfigurations.CurrentLoginUser.UserID,
                MainPreferenceSetID = PrefAppConstant.MainPreferenceSets.MySets,
                MainPreferenceSet = mainPreferenceSet,
                IsNewPreferenceSet = true,
                Checkvariation = false,
                VariationValue = 0
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


                //Remove relation with organizations
                while (PrefSet.PreferenceSetOrganizations.Count() > 0)
                {
                    this.RemovePreferenceSetOrganization(PrefSet.PreferenceSetOrganizations.FirstOrDefault());
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
                IssueName = GetItemName("Issue")

            };

            IssueType issueType = this.Context
                                      .IssueTypes
                                      .Where(s => s.IssueTypeID == PrefAppConstant.IssueTypes.SelectType)
                                      .FirstOrDefault();

            issue.IssueType = issueType;

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

        /// <summary>
        /// Removes the preference set organization.
        /// </summary>
        /// <param name="preferenceSetOrganization">The preference set organization.</param>
        public void RemovePreferenceSetOrganization(PreferenceSetOrganization preferenceSetOrganization)
        {
            if (this.Context.PreferenceSetOrganizations.Contains(preferenceSetOrganization))
            {
                preferenceSetOrganization = this.Context.PreferenceSetOrganizations
                                                        .Where(s => s.PreferenceSetOrganizationID == preferenceSetOrganization.PreferenceSetOrganizationID)
                                                        .FirstOrDefault();

                this.Context.PreferenceSetOrganizations.Remove(preferenceSetOrganization);
            }
        }

        #region →  Removing Objects related to datamatching  .


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
                ConvMessage = this.Context.ConversationMessages.FirstOrDefault(s => s.ConversationMessageID == ConvMessage.ConversationMessageID);

                //Removing all Data matching Related to that message
                while (ConvMessage.MessageIssues.Count() > 0)
                {
                    RemoveMessageIssue(ConvMessage.MessageIssues.First());
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
        /// Save changes asynchronously
        /// </summary>
        public void SaveChangesAsync()
        {
            this.Context.SubmitChanges(s =>
            {
                if (SaveChangesComplete != null)
                {
                    try
                    {
                        Exception ex = null;
                        if (s.HasError)
                        {
                            ex = s.Error;
                            s.MarkErrorAsHandled();
                        }
                        SaveChangesComplete(this, new SubmitOperationEventArgs(s, ex));
                    }
                    catch (Exception ex)
                    {
                        SaveChangesComplete(this, new SubmitOperationEventArgs(ex));
                    }
                }
            }, null);
        }

        /// <summary>
        /// Reject any pending changes
        /// </summary>
        public void RejectChanges()
        {
            this.Context.RejectChanges();
        }

        #endregion IPrefernceSetsModel Interface Implementation

        #endregion

        #endregion

    }
}
