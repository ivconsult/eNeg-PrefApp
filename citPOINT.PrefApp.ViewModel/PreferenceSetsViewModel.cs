#region → Usings   .
using citPOINT.eNeg.Common;
using citPOINT.PrefApp.Common;
using citPOINT.PrefApp.Data;
using citPOINT.PrefApp.Data.Web;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.ServiceModel.DomainServices.Client;
using System.Text.RegularExpressions;
using System.Windows;
using Telerik.Windows.Controls.Charting;
using System.Windows.Controls;
using System.Text;
#endregion

#region → History  .

/* Date         User              Change
 * 
 * 26.09.10     Yousra Reda         • creation
 * 08.11.10     M.Wahab             • Creation
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
    #region  Using MEF to export PreferenceSetsViewModel
    /// <summary>
    /// Class to Manage Preference Sets
    /// Issues,Numeric,Options
    /// </summary>
    [Export(PrefAppViewModelTypes.PreferenceSetsViewModel)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    #endregion
    public partial class PreferenceSetsViewModel : ViewModelBase
    {
        #region → Fields         .

        /// <summary>
        /// Preference Set Model
        /// </summary>
        public IPreferenceSetsModel mPrefSetsModel;

        private IEnumerable<MainPreferenceSet> mMainPrefSets;
        private List<PreferenceSet> mPreferenceSets;
        private IEnumerable<Organization> mOrganizationSource;
        private PreferenceSet mCurrentPreferenceSet;

        private Mail mCurrentMail;
        private bool mIsBusy;
        private bool mStopRejectChanges;
        private bool mLastViewIssue = false;

        private PreferenceSet tmpPereferenceSet = null;

        private StatisticalPublisher mStatisticalPublisher;

        private object mSelectedItem;

        private Guid[] mOrganizationIDs;
        private PreferenceSet orignalPerefenceSet;
        private PreferenceSet publishedBeforePrefSet = null;

        private IssuesViewModel mIssuesVM;

        #region → Commands            .

        private RelayCommand mSubmitChangesCommand = null;
        private RelayCommand mCancelChangesCommand = null;
        private RelayCommand mDeleteItemCommand = null;
        private RelayCommand mAddNewPreferenceSetCommand = null;
        private RelayCommand mSendMailToNegotiatorsCommand = null;
        private RelayCommand<string> mEditSentMessageCommand = null;
        private RelayCommand<string> mPublishPreferenceSetToSetStoreCommand;
        private RelayCommand<string> mPublishPreferenceSetToOrganizationCommand;
        private RelayCommand<string> mPublishPreferenceSetToMySetsCommand;
        private RelayCommand mRaisePublishToOrgCommand;
        private RelayCommand<string> mReplacePublishedPreferenceSetCommand;
        private MainPreferenceSet mMySetsCollection;
        private MainPreferenceSet mOrganizationSetsCollection;
        private MainPreferenceSet mSetStoreCollection;

        private RelayCommand mSubmitPreferenceSetChangesCommand;
        private RelayCommand mRenamePreferenceSetCommand;
        private RelayCommand mCancelPreferenceSetChangesCommand;

        #endregion Commands

        #endregion Fields

        #region → Properties     .

        /// <summary>
        /// Gets a value indicating whether this instance has changes.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has changes; otherwise, <c>false</c>.
        /// </value>
        public bool HasChanges
        {
            get
            {
                return mPrefSetsModel.HasChanges;
            }
            set
            {
                this.SetCheckForCancelPreferenceSetChanges();
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is busy.
        /// </summary>
        /// <value><c>true</c> if this instance is busy; otherwise, <c>false</c>.</value>
        public bool IsBusy
        {
            get
            {
                return mIsBusy;
            }

            set
            {
                mIsBusy = value;
                RaisePropertyChanged("IsBusy");
            }
        }

        /// <summary>
        /// Gets or sets the issues View Model.
        /// </summary>
        /// <value>The issues VM.</value>
        public IssuesViewModel IssuesVM
        {
            get { return mIssuesVM; }
            set
            {
                mIssuesVM = value; this.RaisePropertyChanged("IssuesVM");
            }
        }

        /// <summary>
        /// Gets or sets the current PreferenceSet
        /// </summary>
        public PreferenceSet CurrentPreferenceSet
        {
            get { return mCurrentPreferenceSet; }
            set
            {

                if (mCurrentPreferenceSet != value)
                {
                    mCurrentPreferenceSet = value;

                    this.RaisePropertyChanged("CurrentPreferenceSet");

                    this.IssuesVM.CurrentPreferenceSet = value;
                }

                this.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Gets a value indicating whether [delete rename context visibility].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [delete rename context visibility]; otherwise, <c>false</c>.
        /// </value>
        public bool DeleteRenameContextVisibility
        {
            get
            {
                return (SelectedItem != null &&
                    SelectedItem.GetType().Equals(typeof(PreferenceSet)) &&
                    (SelectedItem as PreferenceSet).MainPreferenceSetID == PrefAppConstant.MainPreferenceSets.MySets);
            }
        }

        /// <summary>
        /// Gets a value indicating whether [copy to my sets context visibility].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [copy to my sets context visibility]; otherwise, <c>false</c>.
        /// </value>
        public bool CopyToMySetsContextVisibility
        {
            get
            {
                return (SelectedItem != null &&
                    SelectedItem.GetType().Equals(typeof(PreferenceSet)));// &&
                //(SelectedItem as PreferenceSet).MainPreferenceSetID != PrefAppConstant.MainPreferenceSets.MySets);
            }
        }

        /// <summary>
        /// Gets a value indicating whether [remove neg from pref set context visibility].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [remove neg from pref set context visibility]; otherwise, <c>false</c>.
        /// </value>
        public bool RemoveNegFromPrefSetContextVisibility
        {
            get
            {
                return (SelectedItem != null &&
                    SelectedItem.GetType().Equals(typeof(PreferenceSetNeg)) &&
                    PrefAppConfigurations.CurrentScreenName == PrefAppViewTypes.DataMatchingView);
            }
        }

        /// <summary>
        /// Gets a value indicating whether [add neg to pref set context visibility].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [add neg to pref set context visibility]; otherwise, <c>false</c>.
        /// </value>
        public bool AddNegToPrefSetContextVisibility
        {
            get
            {
                return (SelectedItem != null &&
                    SelectedItem.GetType().Equals(typeof(PreferenceSet)) &&
                    PrefAppConfigurations.CurrentScreenName == PrefAppViewTypes.DataMatchingView &&
                    (SelectedItem as PreferenceSet).MainPreferenceSetID == PrefAppConstant.MainPreferenceSets.MySets);
            }
        }

        /// <summary>
        /// Gets a value indicating whether [add new set context visibility].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [add new set context visibility]; otherwise, <c>false</c>.
        /// </value>
        public bool AddNewSetContextVisibility
        {
            get
            {
                return (SelectedItem == null ||
                    SelectedItem.GetType().Equals(typeof(MainPreferenceSet)));
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is editable.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is editable; otherwise, <c>false</c>.
        /// </value>
        public bool IsEditable
        {
            get
            {
                if (SelectedItem != null)
                {
                    return SelectedItem.GetType().Equals(typeof(PreferenceSet));
                }
                return false;
            }
        }

        /// <summary>
        /// Gets or sets the selected item.
        /// in the closed or open tree
        /// </summary>
        /// <value>The selected item.</value>
        public object SelectedItem
        {
            get
            {
                return mSelectedItem;
            }
            set
            {
                if (mSelectedItem != value)
                {
                    mSelectedItem = value;
                    this.RejectChanges();
                    this.SetSelectedItem(value);
                    this.RaisePropertyChanged("SelectedItem");

                    //Raise all properties needed to customize context Menu visibility
                    this.RaisePropertyChanged("AddNewSetContextVisibility");
                    this.RaisePropertyChanged("AddNegToPrefSetContextVisibility");
                    this.RaisePropertyChanged("RemoveNegFromPrefSetContextVisibility");
                    this.RaisePropertyChanged("DeleteRenameContextVisibility");
                    this.RaisePropertyChanged("CopyToMySetsContextVisibility");
                    this.RaisePropertyChanged("IsEditable");
                }
            }
        }

        /// <summary>
        /// Gets or sets the Main Preference Sets
        /// </summary>
        public IEnumerable<MainPreferenceSet> MainPreferenceSets
        {
            get
            {
                return mMainPrefSets;
            }

            private set
            {
                mMainPrefSets = value;
                this.RaisePropertyChanged("MainPreferenceSets");
                this.RaisePropertyChanged("MySetsCollection");
                this.RaisePropertyChanged("OrganizationSetsCollection");
                this.RaisePropertyChanged("SetStoreCollection");
            }
        }

        /// <summary>
        /// Gets or sets my sets collection.
        /// </summary>
        /// <value>My sets collection.</value>
        public MainPreferenceSet MySetsCollection
        {
            get
            {
                if (MainPreferenceSets != null)
                {
                    return MainPreferenceSets.Where(s => s.MainPreferenceSetID == PrefAppConstant.MainPreferenceSets.MySets).FirstOrDefault();
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Gets or sets the organization sets collection.
        /// </summary>
        /// <value>The organization sets collection.</value>
        public MainPreferenceSet OrganizationSetsCollection
        {
            get
            {
                if (MainPreferenceSets != null)
                {
                    return MainPreferenceSets.Where(s => s.MainPreferenceSetID == PrefAppConstant.MainPreferenceSets.OrganizationSets).FirstOrDefault();
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Gets or sets the set store collection.
        /// </summary>
        /// <value>The set store collection.</value>
        public MainPreferenceSet SetStoreCollection
        {
            get
            {
                if (MainPreferenceSets != null)
                {
                    return MainPreferenceSets.Where(s => s.MainPreferenceSetID == PrefAppConstant.MainPreferenceSets.SetStore).FirstOrDefault();
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Gets or sets the organization source.
        /// </summary>
        /// <value>The organization source.</value>
        public IEnumerable<Organization> OrganizationSource
        {
            get
            {
                return mOrganizationSource;
            }
            set
            {
                mOrganizationSource = value;

                this.RaisePropertyChanged("OrganizationSource");

                #region → Collecting User Organization Ids .

                if (mOrganizationSource != null)
                {
                    List<Guid> lstOfOrganization = new List<Guid>();

                    foreach (var orgItem in mOrganizationSource)
                    {
                        lstOfOrganization.Add(orgItem.OrganizationID);
                    }

                    this.OrganizationIDs = lstOfOrganization.ToArray();
                }
                #endregion
            }
        }

        /// <summary>
        /// Gets or sets the organization I ds.
        /// </summary>
        /// <value>The organization I ds.</value>
        public Guid[] OrganizationIDs
        {
            get
            {
                return mOrganizationIDs;
            }
            set
            {
                mOrganizationIDs = value;
                this.RaisePropertyChanged("OrganizationIDs");
            }
        }

        /// <summary>
        /// Gets or sets the Preference Sets
        /// </summary>
        public List<PreferenceSet> PreferenceSets
        {
            get
            {
                return mPreferenceSets;
            }
            private set
            {
                if (value != mPreferenceSets)
                {
                    mPreferenceSets = value;
                    this.RaisePropertyChanged("PreferenceSets");
                }
            }
        }

        /// <summary>
        /// Observable collection of Preference Sets
        /// </summary>
        /// <value>The preference sets source.</value>
        public ObservableCollection<PreferenceSet> PreferenceSetsSource { get; set; }

        /// <summary>
        /// Gets or sets the current mail.
        /// </summary>
        /// <value>The current mail.</value>
        public Mail CurrentMail
        {
            get
            {
                if (mCurrentMail == null)
                    mCurrentMail = new Mail();
                return mCurrentMail;
            }
            set
            {
                mCurrentMail = value;
                RaisePropertyChanged("CurrentMail");
            }
        }

        /// <summary>
        /// Gets the statistical publisher.
        /// (send messages to eNeg using RESET)
        /// </summary>
        /// <value>The statistical publisher.</value>
        StatisticalPublisher StatisticalPublisher
        {
            get
            {
                if (mStatisticalPublisher == null)
                {
                    mStatisticalPublisher = new StatisticalPublisher(this.mPrefSetsModel.Context);
                }
                return mStatisticalPublisher;
            }
        }

        /// <summary>
        /// Gets or sets the options mail list.
        /// </summary>
        /// <value>The options mail list.</value>
        private string OptionsMailList { get; set; }

        /// <summary>
        /// Gets or sets the issues mail list.
        /// </summary>
        /// <value>The issues mail list.</value>
        private string IssuesMailList { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [last view issue].
        /// </summary>
        /// <value><c>true</c> if [last view issue]; otherwise, <c>false</c>.</value>
        private bool LastViewIssue
        {
            get
            {
                return mLastViewIssue;
            }
            set
            {
                mLastViewIssue = value;

                this.SetCheckForCancelPreferenceSetChanges();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [stop reject changes].
        /// </summary>
        /// <value><c>true</c> if [stop reject changes]; otherwise, <c>false</c>.</value>
        private bool StopRejectChanges
        {
            get { return mStopRejectChanges; }
            set
            {
                mStopRejectChanges = value;

                SetCheckForCancelPreferenceSetChanges();
            }
        }

        #endregion Properties

        #region → Constructor    .

        /// <summary>
        /// constructor that take a parameter IPreferenceSetsModel and give its values to mPreferenceSetsModel and register required events 
        /// </summary>
        /// <param name="preferenceSetsModel">Value of PreferenceSetsModel</param>
        [ImportingConstructor]
        public PreferenceSetsViewModel(IPreferenceSetsModel preferenceSetsModel)
        {
            #region → Initialization Variables    .

            OrganizationSource = new List<Organization>();
            OrganizationIDs = new List<Guid>().ToArray();

            mPrefSetsModel = preferenceSetsModel;

            this.IssuesVM = new IssuesViewModel(this);

            mPreferenceSets = new List<PreferenceSet>();

            PreferenceSetsSource = new ObservableCollection<PreferenceSet>(mPreferenceSets);

            #endregion

            #region → Set up event Handling       .
            mPrefSetsModel.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(mPrefSetsModel_PropertyChanged);
            mPrefSetsModel.GetMainPreferenceSetsComplete += new EventHandler<eNegEntityResultArgs<MainPreferenceSet>>(mPrefSetsModel_GetMainPreferenceSetsComplete);
            mPrefSetsModel.GetPreferenceSetsComplete += new EventHandler<eNegEntityResultArgs<PreferenceSet>>(mPrefSetsModel_GetPreferenceSetsComplete);
            mPrefSetsModel.SaveChangesComplete += new EventHandler<SubmitOperationEventArgs>(mPrefSetsModel_SaveChangesComplete);
            mPrefSetsModel.GetOrganizationsForUserComplete += new EventHandler<eNegEntityResultArgs<Organization>>(mPrefSetsModel_GetOrganizationsForUserComplete);
            mPrefSetsModel.SendingMailCompleted += new Action<InvokeOperation>(mPrefSetsModel_SendingMailCompleted);
            #endregion

            #region → Loading Relate Lookup Tables.
            GetOrganizationsForUserAsync();
            GetMainPreferenceSetAsync();
            #endregion

            #region → Register needed messages    .
            PrefAppMessanger.SubmitChangesMessage.Register(this, OnSubmitChangesMessage);
            PrefAppMessanger.CancelChangesMessage.Register(this, OnCanceChangesMessage);
            PrefAppMessanger.SubmitChangesMessage.RegisterSubmitAndMail(this, OnSubmitChangesAndmailMessage);
            #endregion
        }

        #endregion Constructors

        #region → Event Handlers .

        #region →  Loading Event Handlers .

        /// <summary>
        /// Call back of Get organizations for user.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void mPrefSetsModel_GetOrganizationsForUserComplete(object sender, eNegEntityResultArgs<Organization> e)
        {
            if (!e.HasError)
            {
                this.OrganizationSource = e.Results.OrderBy(g => g.OrganizationName).ToList();

                GetPreferenceSetAsync();

                // Reload data messages
                this.IssuesVM.LoadPreferenceIssuesDatails(this.OrganizationIDs);
            }
            else
            {
                //Notify User if there is any error
                PrefAppMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Call back for GetPreferenceSets for Getting Preference Sets
        /// </summary>
        /// <param name="sender">Value of Sender</param>
        /// <param name="e">Value of eNegEntityResultArgs</param>
        private void mPrefSetsModel_GetPreferenceSetsComplete(object sender, eNegEntityResultArgs<PreferenceSet> e)
        {
            if (!e.HasError)
            {
                PreferenceSets = e.Results.OrderBy(g => g.PreferenceSetName).ToList();
                PreferenceSetsSource = new ObservableCollection<PreferenceSet>(PreferenceSets);
                this.RaisePropertyChanged("PreferenceSetsSource");
            }
            else
            {
                //Notify User if there is any error
                PrefAppMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Call back GetMainPreferenceSets for Getting MainPreferenceSets
        /// </summary>
        /// <param name="sender">Value of Sender</param>
        /// <param name="e">Value of eNegEntityResultArgs</param>
        private void mPrefSetsModel_GetMainPreferenceSetsComplete(object sender, eNegEntityResultArgs<MainPreferenceSet> e)
        {
            if (!e.HasError)
            {
                MainPreferenceSets = e.Results.OrderBy(s => s.MainPreferenceSetName);

                //MySetsCollection = MainPreferenceSets.Where(s => s.MainPreferenceSetID == PrefAppConstant.MainPreferenceSets.MySets).FirstOrDefault();

                //OrganizationSetsCollection = MainPreferenceSets.Where(s => s.MainPreferenceSetID == PrefAppConstant.MainPreferenceSets.OrganizationSets).FirstOrDefault();

                //SetStoreCollection = MainPreferenceSets.Where(s => s.MainPreferenceSetID == PrefAppConstant.MainPreferenceSets.SetStore).FirstOrDefault();
            }
            else
            {
                //Notify User if there is any error
                PrefAppMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        #endregion

        /// <summary>
        /// PropertyChanged event handler
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">PropertyChangedEventArgs</param>
        private void mPrefSetsModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("HasChanges") || e.PropertyName.Equals("IsBusy"))
            {
                IsBusy = mPrefSetsModel.IsBusy;
                HasChanges = mPrefSetsModel.HasChanges;
                RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Handles the SaveChangesComplete event of the mPrefSetsModel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="citPOINT.eNeg.Common.SubmitOperationEventArgs"/> instance containing the event data.</param>
        private void mPrefSetsModel_SaveChangesComplete(object sender, SubmitOperationEventArgs e)
        {
            if (e != null && e.HasError)
                PrefAppMessanger.RaiseErrorMessage.Send(e.Error);
            else
            {
                #region → Prepaire mail for negotiators .

                //Check whether there is new issue added from Data Matching module 
                //to ask user if he wants to send mail to Negotiators or not  
                if (PrefAppConfigurations.IsNewIssuePending)
                {
                    //the only case will have avalue in case of "OnSubmitChangesAndmailMessage"
                    if (this.tmpPereferenceSet == null)
                    {
                        this.tmpPereferenceSet = this.CurrentPreferenceSet;
                    }

                    this.OptionsMailList = string.Empty;
                    this.IssuesMailList = string.Empty;

                    string newItemsName = string.Empty;

                    string tempName = string.Empty;

                    while (PrefAppConfigurations.PendingItems.Count > 0)
                    {
                        PendingItem pendingItem = PrefAppConfigurations.PendingItems[0];

                        if (pendingItem.PendingType == PrefAppConfigurations.IssueTypes.Issue)
                        {
                            tempName = tmpPereferenceSet.Issues
                                                                .Where(s => s.IssueID == pendingItem.PendingID)
                                                                .FirstOrDefault()
                                                                .IssueName;

                            newItemsName += tempName;

                            if (PrefAppConfigurations.PendingItems.Count > 1)
                                newItemsName += ", ";

                            if (this.IssuesMailList != string.Empty)
                            {
                                this.IssuesMailList += ", ";
                            }

                            this.IssuesMailList += tempName;


                        }
                        else if (pendingItem.PendingType == PrefAppConfigurations.IssueTypes.Option)
                        {
                            var optionIssue = GetOptionIssue(PrefAppConfigurations.PendingItems[0].PendingID);

                            if (optionIssue != null)
                            {
                                tempName = optionIssue.OptionIssueValue;

                                newItemsName += tempName;

                                if (PrefAppConfigurations.PendingItems.Count > 1)
                                    newItemsName += ", ";

                                if (this.OptionsMailList != string.Empty)
                                {
                                    this.OptionsMailList += ", ";
                                }

                                this.OptionsMailList += tempName;
                            }
                        }
                        else if (pendingItem.PendingType == PrefAppConfigurations.IssueTypes.LaterRated)
                        {
                            var optionIssue = GetLaterRatedIssue(PrefAppConfigurations.PendingItems[0].PendingID);

                            if (optionIssue != null)
                            {
                                tempName = optionIssue.LaterRatedIssueValue;

                                newItemsName += tempName;

                                if (PrefAppConfigurations.PendingItems.Count > 1)
                                    newItemsName += ", ";

                                if (this.OptionsMailList != string.Empty)
                                {
                                    this.OptionsMailList += ", ";
                                }

                                this.OptionsMailList += tempName;
                            }
                        }
                        PrefAppConfigurations.PendingItems.RemoveAt(0);
                    }

                    PrefAppMessanger.NewPopUp.Send(newItemsName, PrefAppMessanger.PopUpType.SendMail);

                    PrefAppMessanger.RefreshSource.Send(PrefAppMessanger.RefreshSource.IssuesSource);
                }

                //To Remove any Pending Issue indication 
                PrefAppConfigurations.IsNewIssuePending = false;
                PrefAppConfigurations.PendingItems = new List<PendingItem>();
                #endregion
            }

            RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Ms the pref sets model_ sending mail completed.
        /// </summary>
        /// <param name="obj">The obj.</param>
        private void mPrefSetsModel_SendingMailCompleted(InvokeOperation obj)
        {
            if (!obj.HasError)
            {
                //To Do
                //Till now nothing
            }
            else
            {
                PrefAppMessanger.RaiseErrorMessage.Send(obj.Error);
            }
        }

        #endregion Event Handlers

        #region → Commands       .

        /// <summary>
        /// Gets the send mail to negotiators.
        /// </summary>
        /// <value>The send mail to negotiators.</value>
        public RelayCommand<string> EditSentMessageCommand
        {
            get
            {
                if (mEditSentMessageCommand == null)
                {
                    mEditSentMessageCommand = new RelayCommand<string>((operationName) =>
                    {
                        try
                        {
                            //Switch on user choice to send mail to negotiators in case of choosing "Yes" and closing it in case of "No"
                            if (operationName == MessageBoxResult.No.ToString())
                            {
                                //To Send message to close PopUp Window
                                PrefAppMessanger.FlippMessage.Send(PrefAppViewTypes.ClosePopupView);
                                PrefAppConfigurations.MailNegotiationName = string.Empty;
                            }
                            else if (operationName == MessageBoxResult.Yes.ToString())
                            {
                                PrefAppMessanger.FlippMessage.Send(PrefAppViewTypes.ClosePopupView);

                                #region → Prepairing the Message .

                                string signature = PrefAppConfigurations.CurrentLoginUser.FullName != string.Empty ?
                                                             PrefAppConfigurations.CurrentLoginUser.FullName
                                                             : PrefAppConfigurations.CurrentLoginUser.EmailAddress;


                                this.CurrentMail = new Mail();
                                this.CurrentMail.Sender = PrefAppConfigurations.CurrentLoginUser.EmailAddress;
                                this.CurrentMail.Receiver = BuildNegotiators();


                                var messageBody = new StringBuilder("");
                                messageBody.Append("Dear Partner,");
                                messageBody.AppendLine("There is a new issue in our negotiation: " + PrefAppConfigurations.MailNegotiationName);
                                messageBody.AppendLine("Please provide me with your details concerning");

                                if (!string.IsNullOrEmpty(this.IssuesMailList))
                                {
                                    messageBody.AppendLine("  the issue(s) " + this.IssuesMailList);
                                }

                                if (!string.IsNullOrEmpty(this.OptionsMailList))
                                {
                                    messageBody.AppendLine("  the Options(s) " + this.OptionsMailList);
                                }

                                messageBody.AppendLine();
                                messageBody.AppendLine("Thanks and Best Regards");
                                messageBody.AppendLine(signature);

                                this.CurrentMail.Body = messageBody.ToString();

                                #endregion

                                //To send message to open another PopUp window to can edit in the sent message 
                                PrefAppMessanger.NewPopUp.Send("", PrefAppMessanger.PopUpType.MailEditor);
                            }

                            PrefAppConfigurations.MailPreferenceSetNegID = Guid.Empty;

                            this.tmpPereferenceSet = null;
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            PrefAppMessanger.RaiseErrorMessage.Send(ex);
                        }
                    });
                }
                return mEditSentMessageCommand;
            }
        }

        /// <summary>
        /// Gets the send mail to negotiators command.
        /// </summary>
        /// <value>The send mail to negotiators command.</value>
        public RelayCommand SendMailToNegotiatorsCommand
        {
            get
            {
                if (mSendMailToNegotiatorsCommand == null)
                {
                    mSendMailToNegotiatorsCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            if (!IsMailReceicersValid())
                            {
                                CurrentMail.ValidationErrors.Add(new ValidationResult(ErrorResources.ValidationErrorInvalidEmail, new string[] { "Receiver" }));
                                return;
                            }
                            mPrefSetsModel.SendMailToNegotiators(CurrentMail.Sender, CurrentMail.Receiver, CurrentMail.Subject, CurrentMail.Body);

                            PrefAppMessanger.FlippMessage.Send(PrefAppViewTypes.ClosePopupView);
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            PrefAppMessanger.RaiseErrorMessage.Send(ex);
                        }
                    });
                }
                return mSendMailToNegotiatorsCommand;
            }
        }

        /// <summary>
        /// User Save changes via Calling SubmitChangesMessage so It call
        /// OnSubmitChangesMessage Method.
        /// </summary>
        public RelayCommand SubmitChangesCommand
        {
            get
            {
                if (mSubmitChangesCommand == null)
                {
                    mSubmitChangesCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            if (!mPrefSetsModel.IsBusy)
                            {

                                PrefAppMessanger.SubmitChangesMessage.Send();
                            }
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            PrefAppMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , () => mPrefSetsModel.HasChanges);
                }
                return mSubmitChangesCommand;
            }
        }

        /// <summary>
        /// Used to Add New Negotiation Node To The Leaf of the Tree
        /// Used In case of Click New Button (Add-on)
        /// </summary>
        public RelayCommand AddNewPreferenceSetCommand
        {
            get
            {
                if (mAddNewPreferenceSetCommand == null)
                {
                    mAddNewPreferenceSetCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            if (!this.mPrefSetsModel.IsBusy)
                            {
                                this.RejectChanges();

                                this.orignalPerefenceSet = this.CurrentPreferenceSet;

                                this.CurrentPreferenceSet = this.AddPreferenceSet(true);

                                this.CurrentPreferenceSet.IsNewPreferenceSet = false;

                                PrefAppMessanger.FlippMessage.Send(PrefAppViewTypes.AddOrRenamePreferenceSetViews);
                            }
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            PrefAppMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , () => true);
                }
                return mAddNewPreferenceSetCommand;
            }
        }

        /// <summary>
        /// Gets the rename preference set command.
        /// </summary>
        /// <value>The rename preference set command.</value>
        public RelayCommand RenamePreferenceSetCommand
        {
            get
            {
                if (mRenamePreferenceSetCommand == null)
                {
                    mRenamePreferenceSetCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            if (!mPrefSetsModel.IsBusy && this.CurrentPreferenceSet != null)
                            {
                                this.RejectChanges();

                                this.orignalPerefenceSet = this.CurrentPreferenceSet;

                                PrefAppMessanger.FlippMessage.Send(PrefAppViewTypes.AddOrRenamePreferenceSetViews);
                            }
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            PrefAppMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , () => this.CurrentPreferenceSet != null &&
                        this.CurrentPreferenceSet.MainPreferenceSetID == PrefAppConstant.MainPreferenceSets.MySets);
                }
                return mRenamePreferenceSetCommand;
            }
        }

        /// <summary>
        /// User Save changes via Calling SubmitChangesMessage so It call
        /// OnSubmitChangesMessage Method.
        /// </summary>
        /// <value>The submit preference set changes command.</value>
        public RelayCommand SubmitPreferenceSetChangesCommand
        {
            get
            {
                if (mSubmitPreferenceSetChangesCommand == null)
                {
                    mSubmitPreferenceSetChangesCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            if (!mPrefSetsModel.IsBusy)
                            {
                                if (this.CurrentPreferenceSet != null &&
                                    this.IsAllPreferenceSetsValid(this.CurrentPreferenceSet))
                                {
                                    if (this.CurrentPreferenceSet.EntityState == EntityState.New)
                                    {
                                        //Flag to stop reject changes.
                                        this.StopRejectChanges = true;

                                        this.mPreferenceSets.Add(this.CurrentPreferenceSet);

                                        PreferenceSetsSource = new ObservableCollection<PreferenceSet>(PreferenceSets);

                                        this.RaisePropertyChanged("PreferenceSetsSource");

                                        SelectedItem = CurrentPreferenceSet;

                                        PrefAppMessanger.EditPreferenceSetMessage.Send(this.CurrentPreferenceSet);

                                        this.CurrentPreferenceSet.IsNewPreferenceSet = false;

                                        this.StopRejectChanges = false;
                                    }

                                    PrefAppMessanger.FlippMessage.Send(PrefAppViewTypes.ClosePopupView);

                                    this.mPrefSetsModel.SaveChangesAsync();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            PrefAppMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , () => true);
                }
                return mSubmitPreferenceSetChangesCommand;
            }
        }

        /// <summary>
        /// Gets the cancel preference set changes command.
        /// </summary>
        /// <value>The cancel preference set changes command.</value>
        public RelayCommand CancelPreferenceSetChangesCommand
        {
            get
            {
                if (mCancelPreferenceSetChangesCommand == null)
                {
                    mCancelPreferenceSetChangesCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            this.RejectChanges();

                            PrefAppMessanger.FlippMessage.Send(PrefAppViewTypes.ClosePopupView);

                            this.CurrentPreferenceSet = this.orignalPerefenceSet;

                            if (this.CurrentPreferenceSet != null)
                            {
                                PrefAppMessanger.EditPreferenceSetMessage.Send(this.CurrentPreferenceSet);
                            }
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            PrefAppMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , () => true);
                }
                return mCancelPreferenceSetChangesCommand;
            }
        }

        /// <summary>
        /// User Cancel changes via Calling CancelChangesMessage so It call
        /// OnCancelChangesMessage Method.
        /// </summary>
        public RelayCommand CancelChangesCommand
        {
            get
            {
                if (mCancelChangesCommand == null)
                {
                    mCancelChangesCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            if (!mPrefSetsModel.IsBusy)
                            {
                                //Firstly ask user to confirm editing that item
                                DialogMessage dialogMessage = new DialogMessage(this,
                                    Resources.CancelEditingItem, result =>
                                    {
                                        if (result == MessageBoxResult.OK)
                                        {
                                            //if Confirmed cancel changes
                                            PrefAppMessanger.CancelChangesMessage.Send();
                                        }
                                    })
                                    {
                                        Button = MessageBoxButton.OKCancel,
                                        Caption = Resources.ConfirmMessageBoxCaption
                                    };

                                eNegMessanger.ConfirmMessage.Send(dialogMessage);
                            }
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            PrefAppMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , () => mPrefSetsModel.HasChanges);
                }
                return mCancelChangesCommand;
            }
        }

        /// <summary>
        /// Used to Delete the Current PreferenceSet
        /// Used In case of Right Click Delete this Item 
        /// </summary>
        public RelayCommand DeleteItemCommand
        {
            get
            {
                if (mDeleteItemCommand == null)
                {
                    mDeleteItemCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            #region Confirmation Message

                            Action<MessageBoxResult> callBackResult = null;

                            if (!mPrefSetsModel.IsBusy)
                            {
                                //Firstly ask user to confirm editing that item
                                DialogMessage dialogMessage = new DialogMessage(
                                    this,
                                    Resources.DeleteCurrentItemMessageBoxText,
                                    result => callBackResult(result))
                                {
                                    Button = MessageBoxButton.OKCancel,
                                    Caption = Resources.ConfirmMessageBoxCaption
                                };

                                eNegMessanger.ConfirmMessage.Send(dialogMessage);
                            }

                            #endregion Confirmation Message

                            callBackResult = (result) =>
                                {
                                    if (result == MessageBoxResult.Cancel)
                                        return;
                                    #region Process of Delete
                                    if (this.CurrentPreferenceSet != null)
                                    {
                                        this.StopRejectChanges = true;

                                        RemovePreferenceSet(CurrentPreferenceSet);
                                        PreferenceSets.Remove(CurrentPreferenceSet);

                                        PreferenceSetsSource = new ObservableCollection<PreferenceSet>(PreferenceSets);
                                        this.RaisePropertyChanged("PreferenceSetsSource");

                                        this.CurrentPreferenceSet = null;

                                        PrefAppMessanger.SubmitChangesMessage.Send();

                                        PrefAppMessanger.RefreshSource.Send(PrefAppMessanger.RefreshSource.PreferenceSetDeleted);

                                        this.StopRejectChanges = false;
                                    }
                                    #endregion Process of Delete
                                };
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            PrefAppMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , () => this.CurrentPreferenceSet != null &&
                        this.CurrentPreferenceSet.MainPreferenceSetID == PrefAppConstant.MainPreferenceSets.MySets);
                }
                return mDeleteItemCommand;
            }
        }

        /// <summary>
        /// Gets the publish preference set to set store command.
        /// </summary>
        /// <value>The publish preference set to set store command.</value>
        public RelayCommand<string> PublishPreferenceSetToSetStoreCommand
        {
            get
            {
                if (mPublishPreferenceSetToSetStoreCommand == null)
                {
                    mPublishPreferenceSetToSetStoreCommand = new RelayCommand<string>((operationName) =>
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(operationName))
                            {
                                #region → First Step Cloning and Raise Popup      .

                                if (operationName == "START")
                                {
                                    if (this.CurrentPreferenceSet != null &&
                                        this.CurrentPreferenceSet.MainPreferenceSetID == PrefAppConstant.MainPreferenceSets.MySets &&
                                        !this.mPrefSetsModel.IsBusy)
                                    {


                                        PrefAppConfigurations.CanContinueProcess((result) =>
                                        {
                                            if (result == MessageBoxResult.OK)
                                            {

                                                orignalPerefenceSet = this.CurrentPreferenceSet;

                                                //Reject changes in case if there changes
                                                this.OnCanceChangesMessage(true);

                                                this.SelectedItem = orignalPerefenceSet;

                                                this.CurrentPreferenceSet = new PreferenceSet()
                                                {
                                                    MainPreferenceSetID = PrefAppConstant.MainPreferenceSets.SetStore,
                                                    PreferenceSetName = orignalPerefenceSet.PreferenceSetName
                                                };

                                                PrefAppMessanger.NewPopUp.Send("Confirm",
                                                                               PrefAppMessanger.PopUpType.PublishToSetStore);
                                            }
                                        });
                                    }
                                }
                                #endregion

                                #region → In case if user Click OK-Replace Button .

                                else if (operationName == "OK" || operationName == "REPLACE")
                                {
                                    if (this.IsPublishedPreferenceSetsValid(this.CurrentPreferenceSet) || operationName == "REPLACE")
                                    {
                                        PublishPreferenceSet(PrefAppConstant.MainPreferenceSets.SetStore, null);
                                    }
                                    else if (publishedBeforePrefSet != null)//Mean it is Already published
                                    {
                                        PrefAppMessanger.NewPopUp.Send("Confirmation",
                                              PrefAppMessanger.PopUpType.PublishToOrganizationAndReplace);
                                    }
                                }
                                #endregion

                                #region → In case if user Click Cancel Button     .

                                else if (operationName == "CANCEL")
                                {
                                    PrefAppMessanger.FlippMessage.Send(PrefAppViewTypes.ClosePopupView);

                                    this.RejectChanges();

                                    this.CurrentPreferenceSet = this.orignalPerefenceSet;
                                }
                                #endregion
                            }
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            PrefAppMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }, (operationName) => !string.IsNullOrEmpty(operationName) &&
                                            ((operationName == "START" &&
                                               this.CurrentPreferenceSet != null &&
                                               this.CurrentPreferenceSet.MainPreferenceSetID == PrefAppConstant.MainPreferenceSets.MySets &&
                                               !this.mPrefSetsModel.IsBusy) || (operationName != "START")));
                }
                return mPublishPreferenceSetToSetStoreCommand;
            }
        }

        /// <summary>
        /// Gets the publish preference set to organization command.
        /// </summary>
        /// <value>The publish preference set to organization command.</value>
        public RelayCommand<string> PublishPreferenceSetToOrganizationCommand
        {
            get
            {
                if (mPublishPreferenceSetToOrganizationCommand == null)
                {
                    mPublishPreferenceSetToOrganizationCommand = new RelayCommand<string>((operationName) =>
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(operationName))
                            {
                                #region → First Step Cloning and Raise Popup      .

                                if (operationName == "START")
                                {
                                    if (this.CurrentPreferenceSet != null &&
                                        this.CurrentPreferenceSet.MainPreferenceSetID == PrefAppConstant.MainPreferenceSets.MySets &&
                                        !this.mPrefSetsModel.IsBusy)
                                    {
                                        PrefAppConfigurations.CanContinueProcess((result) =>
                                        {
                                            if (result == MessageBoxResult.OK)
                                            {
                                                orignalPerefenceSet = this.CurrentPreferenceSet;

                                                //Reject changes in case if there changes
                                                this.OnCanceChangesMessage(true);

                                                this.SelectedItem = orignalPerefenceSet;

                                                if (this.OrganizationSource.Count() <= 0)
                                                {
                                                    // Rasie message told user that he is not join any organization.
                                                    eNegMessanger.SendCustomMessage.Send(
                                                        new eNegMessage(Resources.UserHaveNotOrganization, ImageType.Info, PrefAppConfigurations.ApplicationID));
                                                    return;
                                                }

                                                foreach (var orgitem in this.OrganizationSource)
                                                {
                                                    orgitem.IsSelected = true;
                                                }

                                                this.CurrentPreferenceSet = new PreferenceSet()
                                                {
                                                    MainPreferenceSetID = PrefAppConstant.MainPreferenceSets.OrganizationSets,
                                                    PreferenceSetName = orignalPerefenceSet.PreferenceSetName
                                                };

                                                PrefAppMessanger.NewPopUp.Send("Confirmation",
                                                    PrefAppMessanger.PopUpType.PublishToOrganization);

                                            }
                                        });
                                    }
                                }
                                #endregion

                                #region → In case if user Click OK-Replace Button .

                                else if (operationName == "OK" || operationName == "REPLACE")
                                {
                                    if (this.IsPublishedPreferenceSetsValid(this.CurrentPreferenceSet) || operationName == "REPLACE")
                                    {
                                        #region → Collecting User Organization Ids .

                                        List<Guid> lstOfOrganization = new List<Guid>();
                                        if (mOrganizationSource != null)
                                        {
                                            foreach (var orgItem in mOrganizationSource.Where(s => s.IsSelected))
                                            {
                                                lstOfOrganization.Add(orgItem.OrganizationID);
                                            }
                                        }
                                        #endregion

                                        PublishPreferenceSet(PrefAppConstant.MainPreferenceSets.OrganizationSets, lstOfOrganization.ToArray());
                                    }
                                    else if (publishedBeforePrefSet != null)//Mean it is Already published before by the same user.
                                    {
                                        PrefAppMessanger.NewPopUp.Send("Confirmation",
                                              PrefAppMessanger.PopUpType.PublishToOrganizationAndReplace);
                                    }
                                }
                                #endregion

                                #region → In case if user Click No Replace Button .

                                else if (operationName == "NO")
                                {
                                    if (!this.IsAllPreferenceSetsValid(this.CurrentPreferenceSet))
                                    {
                                        PrefAppMessanger.FlippMessage.Send(PrefAppViewTypes.CloseReplacePopupView);
                                    }
                                }
                                #endregion

                                #region → In case if user Click Cancel Button     .

                                else if (operationName == "CANCEL")
                                {
                                    PrefAppMessanger.FlippMessage.Send(PrefAppViewTypes.ClosePopupView);

                                    this.RejectChanges();

                                    this.CurrentPreferenceSet = this.orignalPerefenceSet;

                                }
                                #endregion
                            }
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            PrefAppMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }, (operationName) => !string.IsNullOrEmpty(operationName) &&
                                            ((operationName == "START" &&
                                               this.CurrentPreferenceSet != null &&
                                               this.CurrentPreferenceSet.MainPreferenceSetID == PrefAppConstant.MainPreferenceSets.MySets &&
                                               !this.mPrefSetsModel.IsBusy) ||
                                               (operationName == "OK" && this.OrganizationSource.Where(s => s.IsSelected).Count() > 0) ||
                                               (operationName == "CANCEL") ||
                                               (operationName == "REPLACE") ||
                                               (operationName == "NO")));
                }
                return mPublishPreferenceSetToOrganizationCommand;
            }
        }

        /// <summary>
        /// Gets the publish preference set to my sets command.
        /// </summary>
        /// <value>The publish preference set to my sets command.</value>
        public RelayCommand<string> PublishPreferenceSetToMySetsCommand
        {
            get
            {
                if (mPublishPreferenceSetToMySetsCommand == null)
                {
                    mPublishPreferenceSetToMySetsCommand = new RelayCommand<string>((operationName) =>
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(operationName))
                            {
                                #region → First Step Cloning and Raise Popup  .

                                if (operationName == "START")
                                {
                                    if (this.CurrentPreferenceSet != null &&
                                       !this.mPrefSetsModel.IsBusy)
                                    {
                                        orignalPerefenceSet = this.CurrentPreferenceSet;

                                        this.CurrentPreferenceSet = new PreferenceSet()
                                        {
                                            MainPreferenceSetID = PrefAppConstant.MainPreferenceSets.MySets,
                                            PreferenceSetName = orignalPerefenceSet.PreferenceSetName
                                        };

                                        PrefAppMessanger.NewPopUp.Send("Confirmation",
                                            PrefAppMessanger.PopUpType.PublishToMySets);
                                    }
                                }
                                #endregion

                                #region → In case if user Click OK Button     .

                                else if (operationName == "OK")
                                {
                                    if (this.IsAllPreferenceSetsValid(this.CurrentPreferenceSet))
                                    {
                                        PublishPreferenceSet(PrefAppConstant.MainPreferenceSets.MySets, null);
                                    }
                                }
                                #endregion

                                #region → In case if user Click Cancel Button .

                                else if (operationName == "CANCEL")
                                {
                                    PrefAppMessanger.FlippMessage.Send(PrefAppViewTypes.ClosePopupView);

                                    this.RejectChanges();

                                    this.CurrentPreferenceSet = this.orignalPerefenceSet;
                                }
                                #endregion
                            }
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            PrefAppMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }, (operationName) => !string.IsNullOrEmpty(operationName) &&
                                            ((operationName == "START" &&
                                               this.CurrentPreferenceSet != null &&
                                               !this.mPrefSetsModel.IsBusy) || (operationName != "START")));
                }
                return mPublishPreferenceSetToMySetsCommand;
            }
        }

        /// <summary>
        /// Gets the replace published preference set command.
        /// </summary>
        /// <value>The replace published preference set command.</value>
        public RelayCommand<string> ReplacePublishedPreferenceSetCommand
        {
            get
            {
                if (mReplacePublishedPreferenceSetCommand == null)
                {
                    mReplacePublishedPreferenceSetCommand = new RelayCommand<string>((operationName) =>
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(operationName))
                            {
                                #region → REPLACE  .

                                if (operationName == "REPLACE")
                                {
                                    if (publishedBeforePrefSet != null)
                                    {
                                        //Publis to Set Store
                                        if (publishedBeforePrefSet.MainPreferenceSetID == PrefAppConstant.MainPreferenceSets.SetStore)
                                        {
                                            PublishPreferenceSetToSetStoreCommand.Execute("REPLACE");
                                        }
                                        //Publish to Organizations
                                        else if (publishedBeforePrefSet.MainPreferenceSetID == PrefAppConstant.MainPreferenceSets.OrganizationSets)
                                        {
                                            PublishPreferenceSetToOrganizationCommand.Execute("REPLACE");
                                        }
                                    }
                                }
                                #endregion

                                #region → NO       .

                                else if (operationName == "NO")
                                {
                                    if (!this.IsAllPreferenceSetsValid(this.CurrentPreferenceSet))
                                    {
                                        //To clear Selected One
                                        this.publishedBeforePrefSet = null;

                                        //Close replace Popup
                                        PrefAppMessanger.FlippMessage.Send(PrefAppViewTypes.CloseReplacePopupView);
                                    }
                                }
                                #endregion
                            }
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            PrefAppMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }, (operationName) => true);
                }
                return mReplacePublishedPreferenceSetCommand;
            }
        }

        /// <summary>
        /// Gets the raise publish to org command.
        /// </summary>
        /// <value>The raise publish to org command.</value>
        public RelayCommand RaisePublishToOrgCommand
        {
            get
            {
                if (mRaisePublishToOrgCommand == null)
                {
                    mRaisePublishToOrgCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            if (!mPrefSetsModel.IsBusy)
                            {
                                Deployment.Current.Dispatcher.BeginInvoke(() =>
                                    {
                                        PublishPreferenceSetToOrganizationCommand.RaiseCanExecuteChanged();
                                    });
                            }
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            PrefAppMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , () => true);
                }
                return mRaisePublishToOrgCommand;
            }
        }

        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// update in Negotiation and conversation according to the type of obj
        /// </summary>
        /// <param name="selectedObject">The obj.</param>
        private void SetSelectedItem(object selectedObject)
        {
            LastViewIssue = false;

            if (selectedObject == null)
            {
                PrefAppMessanger.FlippMessage.Send(PrefAppViewTypes.AddPrefSetView);
            }
            else
            {
                if (selectedObject.GetType().Equals(typeof(PreferenceSet)))
                {
                    CurrentPreferenceSet = (selectedObject as PreferenceSet);

                    PrefAppMessanger.FlippMessage.Send(PrefAppViewTypes.IssuesView);

                    LastViewIssue = true;
                }
            }
        }

        /// <summary>
        /// Determines whether [is published preference sets valid] [the specified pref set].
        /// </summary>
        /// <param name="prefSet">The pref set.</param>
        /// <returns>
        /// 	<c>true</c> if [is published preference sets valid] [the specified pref set]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsPublishedPreferenceSetsValid(PreferenceSet prefSet)
        {
            if (prefSet == null) return false;

            bool IsAllValid = true;

            if (MainPreferenceSets.Where(s => s.MainPreferenceSetID == prefSet.MainPreferenceSetID)
                                  .First()
                                  .PreferenceSets.Count > 0)
            {


                #region → Check Repeating .

                PreferenceSet otherPrefSet = MainPreferenceSets.Where(d => d.MainPreferenceSetID == prefSet.MainPreferenceSetID)
                                                          .First()
                                                          .PreferenceSets
                                                          .Where(s => s.PreferenceSetName.ToLower() == prefSet.PreferenceSetName.ToLower() &&
                                                                      s.PreferenceSetID != prefSet.PreferenceSetID)
                                                          .FirstOrDefault();

                if (otherPrefSet != null)
                {
                    // Repeated but published by an other persone
                    if (otherPrefSet.UserID != PrefAppConfigurations.CurrentLoginUser.UserID)
                    {
                        prefSet.ValidationErrors.Add(new ValidationResult(Resources.RepeatedPrefSet, new string[] { "PreferenceSetName" }));
                        IsAllValid = false;
                    }
                    else//Published before by my self
                    {
                        publishedBeforePrefSet = otherPrefSet;
                        IsAllValid = false;
                    }
                }

                #endregion

                if (string.IsNullOrEmpty(prefSet.PreferenceSetName))
                    prefSet.PreferenceSetName = string.Empty;

                if (!(prefSet.TryValidateObject() &&
                        prefSet.TryValidateProperty("PreferenceSetName")))
                {
                    IsAllValid = false;
                }
            }

            return IsAllValid;
        }

        /// <summary>
        /// Determines whether [is mail receicers valid].
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if [is mail receicers valid]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsMailReceicersValid()
        {
            if (string.IsNullOrWhiteSpace(CurrentMail.Receiver) || string.IsNullOrEmpty(CurrentMail.Receiver))
            {
                return false;
            }

            string[] AllReceivers = CurrentMail.Receiver.Trim().Split(new string[] { ";" }, StringSplitOptions.None);
            if (AllReceivers[AllReceivers.Length - 1].Contains(';'))
            {
                AllReceivers[AllReceivers.Length - 1] = AllReceivers[AllReceivers.Length - 1].Replace(";", string.Empty);
            }
            foreach (var receiver in AllReceivers)
            {
                if (!string.IsNullOrEmpty(receiver) && !Regex.IsMatch(receiver.Trim(),
                       @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" +
                       @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$"))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Executed when SubmitChangesMessage is received
        /// </summary>
        /// <param name="alsoCheckNegotiationEffects">if set to <c>true</c> [alsocheck negotiationeffects].</param>
        private void OnSubmitChangesMessage(Boolean alsoCheckNegotiationEffects)
        {
            if (mPrefSetsModel.HasChanges)
            {
                if (this.IsAllPreferenceSetsValid(CurrentPreferenceSet) &&
                    this.CurrentPreferenceSet != null)

                    if (this.CurrentPreferenceSet != null)
                    {
                        // pop up. 
                        // In case the Current Preference Set has Two Negotiation
                        if (alsoCheckNegotiationEffects && CurrentPreferenceSet.OngingPreferenceSetNegs.Count() > 1)
                        {
                            Common.PrefAppMessanger.NewPopUp.Send("Negotiation that will be effected", PrefAppMessanger.PopUpType.ClonePreferenceSet);
                            return;
                        }

                        CalculationEngine<ClientEngineProvider> calculationEngine = new CalculationEngine<ClientEngineProvider>(this.mPrefSetsModel.Context);

                        //Update Max Threshould for preference Set.
                        this.CurrentPreferenceSet.UpdateMaxPercentage();

                        calculationEngine.Calculate(this.CurrentPreferenceSet);

                        // Send Statisticals to eNeg in case if changes effect Messages Score .
                        StatisticalPublisher.Send();

                    }

                mPrefSetsModel.SaveChangesAsync();

            }
        }

        /// <summary>
        /// Called when [submit changes andmail message].
        /// used incase of cloning to send mail
        /// </summary>
        /// <param name="preferenceset">The preferenceset.</param>
        private void OnSubmitChangesAndmailMessage(PreferenceSet preferenceset)
        {
            this.tmpPereferenceSet = preferenceset;

            mPrefSetsModel.SaveChangesAsync();
        }

        /// <summary>
        /// Executed when CancelChangesMessage is received
        /// </summary>
        /// <param name="ignore">ignore</param>
        private void OnCanceChangesMessage(Boolean ignore)
        {
            this.mPrefSetsModel.RejectChanges();
            PrefAppConfigurations.IsNewIssuePending = false;
            this.IssuesVM.RejectChanges();
            this.SelectedItem = null;
        }

        /// <summary>
        /// Builds the negotiators.
        /// </summary>
        /// <returns>Concatenation of all destinct Negotatiators separated by ;</returns>
        private string BuildNegotiators()
        {
            #region → Build Negotiators         .

            var sbNegotiators = new StringBuilder();

            var allNegotiators = new List<string>();

            var conversationMessages = mPrefSetsModel.Context.ConversationMessages.Where(s => s.NegConversation.PreferenceSetNeg.PreferenceSet == tmpPereferenceSet);

            foreach (var message in conversationMessages)
            {
                if (message.MessageReceiver != null && !allNegotiators.Contains(message.MessageReceiver))
                {
                    allNegotiators.Add(message.MessageReceiver);
                }

                if (message.MessageSender != null && !allNegotiators.Contains(message.MessageSender))
                {
                    allNegotiators.Add(message.MessageSender);
                }
            }

            #endregion

            allNegotiators.Sort();

            //Concat Negotiators' Name to be in following format →→→  aa@aa.com; bb@bb.com; cc@cc.com;

            foreach (var word in allNegotiators)
            {
                var wordSubstring = word.Replace("<", "").Replace(">", "");

                if (wordSubstring != PrefAppConfigurations.CurrentLoginUser.EmailAddress && IsValidEmail(wordSubstring))
                {
                    sbNegotiators.Append(wordSubstring + "; ");
                }
            }

            return sbNegotiators.ToString();
        }
        
        /// <summary>
        /// Isvalids the email.
        /// </summary>
        /// <param name="emailAddress">The email address.</param>
        /// <returns>
        /// 	<c>true</c> if [is valid email] [the specified email address]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsValidEmail(string emailAddress)
        {
            return Regex.IsMatch(emailAddress,
                 @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" +
                 @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
        }

        /// <summary>
        /// Publishes the preference set.
        /// </summary>
        /// <param name="mainPreferenceSetID">The main preference set ID.</param>
        /// <param name="organizationIDs">The organization Ids.</param>
        private void PublishPreferenceSet(Guid mainPreferenceSetID, Guid[] organizationIDs)
        {
            this.StopRejectChanges = true;

            PreferenceSet prefSet = this.mPrefSetsModel.PublishPreferenceSet(this.orignalPerefenceSet, mainPreferenceSetID, organizationIDs);

            prefSet.PreferenceSetName = this.CurrentPreferenceSet.PreferenceSetName;

            this.CurrentPreferenceSet = null;

            this.CurrentPreferenceSet = prefSet;

            if (this.publishedBeforePrefSet != null)
            {
                this.RemovePreferenceSet(this.publishedBeforePrefSet);
            }

            this.publishedBeforePrefSet = null;


            #region → Referesh Tree Source .

            this.mPreferenceSets.Add(this.CurrentPreferenceSet);

            PreferenceSetsSource = new ObservableCollection<PreferenceSet>(PreferenceSets);

            this.RaisePropertyChanged("PreferenceSetsSource");

            this.MainPreferenceSets = new List<MainPreferenceSet>(this.MainPreferenceSets);

            #endregion

            SelectedItem = CurrentPreferenceSet;

            //To slect node in the the Tree
            PrefAppMessanger.EditPreferenceSetMessage.Send(this.CurrentPreferenceSet);


            //Save in database
            PrefAppMessanger.SubmitChangesMessage.Send();

            //Close popup
            PrefAppMessanger.FlippMessage.Send(PrefAppViewTypes.ClosePopupView);

            this.StopRejectChanges = false;

        }

        /// <summary>
        /// Gets the option issue.
        /// </summary>
        /// <param name="optionIssueID">The option issue ID.</param>
        /// <returns></returns>
        private OptionIssue GetOptionIssue(Guid optionIssueID)
        {
            if (this.tmpPereferenceSet != null)
            {
                foreach (var issue in this.tmpPereferenceSet.Issues.Where(s => s.IssueTypeID == PrefAppConstant.IssueTypes.Options))
                {

                    if (issue.OptionIssues.Where(s => s.OptionIssueID == optionIssueID).Count() > 0)
                    {
                        return issue.OptionIssues.Where(s => s.OptionIssueID == optionIssueID).FirstOrDefault();
                    }

                }
            }

            return null;
        }

        /// <summary>
        /// Gets the later rated issue.
        /// </summary>
        /// <param name="laterRatedIssueID">The later rated issue ID.</param>
        /// <returns></returns>
        private LaterRatedIssue GetLaterRatedIssue(Guid laterRatedIssueID)
        {
            if (this.tmpPereferenceSet != null)
            {
                foreach (var issue in this.tmpPereferenceSet.Issues.Where(s => s.IssueTypeID == PrefAppConstant.IssueTypes.LaterRated))
                {
                    if (issue.LaterRatedIssues.Where(s => s.LaterRatedIssueID == laterRatedIssueID).Count() > 0)
                    {
                        return issue.LaterRatedIssues.Where(s => s.LaterRatedIssueID == laterRatedIssueID).FirstOrDefault();
                    }

                }
            }

            return null;
        }

        /// <summary>
        /// Sets the check for cancel preference set changes.
        /// </summary>
        private void SetCheckForCancelPreferenceSetChanges()
        {
            PrefAppConfigurations.CheckForCancelPreferenceSetChanges = this.HasChanges &&
                                                                       this.LastViewIssue &&
                                                                       !this.StopRejectChanges;
        }

        #endregion

        #region → Public         .

        /// <summary>
        /// Gets the organizations for user async.
        /// </summary>
        public void GetOrganizationsForUserAsync()
        {
            mPrefSetsModel.GetOrganizationsForUserAsync();
        }

        /// <summary>
        /// Gets All Preference Sets Asynchronously
        /// </summary>
        public void GetMainPreferenceSetAsync()
        {
            mPrefSetsModel.GetMainPreferenceSetAsync();
        }

        /// <summary>
        /// Gets All Preference Sets Asynchronously
        /// </summary>
        public void GetPreferenceSetAsync()
        {
            mPrefSetsModel.GetPreferenceSetAsync(OrganizationIDs);
        }

        /// <summary>
        /// Add New PreferenceSet
        /// </summary>
        /// <param name="SetInContext">Set new PrefSet object In Context or not</param>
        /// <returns>Preference Set</returns>
        public PreferenceSet AddPreferenceSet(bool SetInContext)
        {
            return mPrefSetsModel.AddPreferenceSet(SetInContext);
        }

        /// <summary>
        /// Remove PreferenceSet
        /// </summary>
        /// <param name="PrefSet">The pref set.</param>
        public void RemovePreferenceSet(PreferenceSet PrefSet)
        {
            mPrefSetsModel.RemovePreferenceSet(PrefSet);
        }

        /// <summary>
        /// Rejects the changes.
        /// </summary>
        public void RejectChanges()
        {
            if (!PrefAppConfigurations.IsNewIssuePending && !this.StopRejectChanges)
            {
                mPrefSetsModel.RejectChanges();
                RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Determines whether [is all preference sets valid].
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if [is all preference sets valid]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsAllPreferenceSetsValid(PreferenceSet PrefSet)
        {
            if (PrefSet == null) return false;

            bool IsAllValid = true;
            if (MainPreferenceSets.Where(s => s.MainPreferenceSetID == PrefSet.MainPreferenceSetID).First()
                     .PreferenceSets.Count > 0)
            {

                //PrefSet.ValidationErrors.Clear();
                if (MainPreferenceSets.Where(s => s.MainPreferenceSetID == PrefSet.MainPreferenceSetID).First()
                    .PreferenceSets.Count(s => s.PreferenceSetName.ToLower() == PrefSet.PreferenceSetName.ToLower()
                                            && s.PreferenceSetID != PrefSet.PreferenceSetID) > 0)
                {
                    PrefSet.ValidationErrors.Add(new ValidationResult(Resources.RepeatedPrefSet, new string[] { "PreferenceSetName" }));
                    IsAllValid = false;
                }

                if (string.IsNullOrEmpty(PrefSet.PreferenceSetName))
                    PrefSet.PreferenceSetName = string.Empty;

                if (!(PrefSet.TryValidateObject() &&
                        PrefSet.TryValidateProperty("PreferenceSetName")))
                {
                    IsAllValid = false;
                }
            }

            return IsAllValid;
        }

        /// <summary>
        /// Raises the can execute changed.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            SubmitChangesCommand.RaiseCanExecuteChanged();
            CancelChangesCommand.RaiseCanExecuteChanged();
            DeleteItemCommand.RaiseCanExecuteChanged();
            AddNewPreferenceSetCommand.RaiseCanExecuteChanged();
            PublishPreferenceSetToSetStoreCommand.RaiseCanExecuteChanged();
            PublishPreferenceSetToOrganizationCommand.RaiseCanExecuteChanged();
            PublishPreferenceSetToMySetsCommand.RaiseCanExecuteChanged();

            RenamePreferenceSetCommand.RaiseCanExecuteChanged();
            SubmitPreferenceSetChangesCommand.RaiseCanExecuteChanged();
            CancelPreferenceSetChangesCommand.RaiseCanExecuteChanged();


        }

        #region "ICleanup interface implementation"

        /// <summary>
        /// Cleanup negotiation model
        /// </summary>
        public override void Cleanup()
        {
            // unregister all events
            mPrefSetsModel.PropertyChanged -= new System.ComponentModel.PropertyChangedEventHandler(mPrefSetsModel_PropertyChanged);
            mPrefSetsModel.GetMainPreferenceSetsComplete -= new EventHandler<eNegEntityResultArgs<MainPreferenceSet>>(mPrefSetsModel_GetMainPreferenceSetsComplete);
            mPrefSetsModel.GetPreferenceSetsComplete -= new EventHandler<eNegEntityResultArgs<PreferenceSet>>(mPrefSetsModel_GetPreferenceSetsComplete);
            mPrefSetsModel.SaveChangesComplete -= new EventHandler<SubmitOperationEventArgs>(mPrefSetsModel_SaveChangesComplete);
            mPrefSetsModel.GetOrganizationsForUserComplete -= new EventHandler<eNegEntityResultArgs<Organization>>(mPrefSetsModel_GetOrganizationsForUserComplete);

            // unregister any messages for this ViewModel
            // Cleanup itself
            Messenger.Default.Unregister(this);

            base.Cleanup();
        }

        #endregion "ICleanup interface implementation"

        #endregion

        #endregion
    }
}
