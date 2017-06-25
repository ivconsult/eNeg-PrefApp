
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
#endregion

#region → History  .

/* 
 * Date         User            Change
 * *********************************************
 * 3/27/2012 9:57:20 AM      mwahab         • creation
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

namespace citPOINT.PrefApp.ViewModel
{
    /// <summary>
    /// Class for IssuesViewModel 
    /// </summary>
    public class IssuesViewModel : ViewModelBase
    {
        #region → Fields         .

        /// <summary>
        /// Refresh Type
        /// </summary>
        public enum RefreshType
        {
            /// <summary>
            /// Issue Source Only
            /// </summary>
            IssueSource = 0,

            /// <summary>
            ///Issues Numeric Options and Pie Only Source 
            /// </summary>
            OthersIssuesSource = 1,

            /// <summary>
            /// 
            /// </summary>
            RejectChanges = 2,

            /// <summary>
            /// All
            /// </summary>
            All = 3
        }

        private bool mIsAllNegSelected = true;
        private bool IsNegSelectionChanged;
        private bool IsLastCommandFinish = false;
        private bool CanSendEditIssueMessage = true;

        private Issue mCurrentIssue;
        private PreferenceSet mCurrentPreferenceSet;

        private IEnumerable<IssueType> mIssueTypes;
        private List<Issue> mIssues;
        private IEnumerable<LaterRatedIssue> mLaterRatedIssues;
        private IEnumerable<NumericIssue> mNumericIssues;
        private IEnumerable<OptionIssue> mOptionIssues;

        private StatisticalPublisher mStatisticalPublisher;

        private bool mIsScoreNotValid = false;
        private bool mIsBusy;
        private Guid[] OrganizationIDs;

        private OptionIssue mCurrentOption;

        private LaterRatedIssue mCurrentLaterRated;

        #region → Commands            .

        private RelayCommand<string> mAddNewIssueCommand = null;
        private RelayCommand mDeleteIssueCommand = null;
        private RelayCommand mChangeIssueTypeCommand = null;
        private RelayCommand mSubmitIssuesChangesCommand = null;
        private RelayCommand<Entity> mCloseAddNewIssuePopUpWindowCommand = null;
        private RelayCommand<RoutedEventArgs> mSelectDeslectNegotiationToEffectRatingCommand;
        private RelayCommand mSubmitChangesAfterSelectNegotiationCommand;
        private RelayCommand<string> mNavigateCommand;
        private RelayCommand mFinishIssuesChangesCommand;
        private RelayCommand mNavigateToNextCommand;

        #endregion

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets the preference sets VM.
        /// </summary>
        /// <value>The preference sets VM.</value>
        public PreferenceSetsViewModel PreferenceSetsVM { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is all neg selected.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is all neg selected; otherwise, <c>false</c>.
        /// </value>
        public bool IsAllNegSelected
        {
            get
            {
                return mIsAllNegSelected;
            }
            set
            {
                mIsAllNegSelected = value;
                RaisePropertyChanged("IsAllNegSelected");
                SelectDeSelect(IsAllNegSelected);
            }
        }

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
                return PreferenceSetsVM.HasChanges;
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

                    IsScoreNotValid = false;

                    RefreshIssuesSource();

                    this.CurrentIssue = null;

                }

                this.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Total score of the Current Issues are  matched 100%.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is score matched; otherwise, <c>false</c>.
        /// </value>
        public bool IsScoreNotValid
        {
            get
            {
                return mIsScoreNotValid;
            }

            private set
            {
                if (value != mIsScoreNotValid)
                {
                    mIsScoreNotValid = value;
                    this.RaisePropertyChanged("IsScoreNotValid");
                }
            }
        }

        /// <summary>
        /// Gets or sets the issue types.
        /// </summary>
        /// <value>The issue types.</value>
        public IEnumerable<IssueType> IssueTypes
        {
            get
            {
                return mIssueTypes;
            }
            private set
            {
                if (value != mIssueTypes)
                {
                    mIssueTypes = value;
                    this.RaisePropertyChanged("IssueTypes");
                }
            }
        }

        /// <summary>
        /// Gets or sets the issues.
        /// </summary>
        /// <value>The issues.</value>
        public List<Issue> Issues
        {
            get { return mIssues; }
            set { mIssues = value; }
        }

        /// <summary>
        /// Gets or sets the numeric issues.
        /// </summary>
        /// <value>The numeric issues.</value>
        public IEnumerable<NumericIssue> NumericIssues
        {
            get { return mNumericIssues; }
            set { mNumericIssues = value; }
        }

        /// <summary>
        /// Gets or sets the option issues.
        /// </summary>
        /// <value>The option issues.</value>
        public IEnumerable<OptionIssue> OptionIssues
        {
            get { return mOptionIssues; }
            set { mOptionIssues = value; }
        }

        /// <summary>
        /// Gets or sets the later rated issues.
        /// </summary>
        /// <value>The later rated issues.</value>
        public IEnumerable<LaterRatedIssue> LaterRatedIssues
        {
            get { return mLaterRatedIssues; }
            set { mLaterRatedIssues = value; }
        }

        /// <summary>
        /// Gets or sets the current Issue
        /// </summary>
        public Issue CurrentIssue
        {
            get { return mCurrentIssue; }
            set
            {
                if (mCurrentIssue != value || true)
                {
                    mCurrentIssue = value;

                    this.RaisePropertyChanged("CurrentIssue");
                }
            }
        }

        /// <summary>
        /// Gets or sets the issues source.
        /// </summary>
        /// <value>The issues source.</value>
        public ObservableCollection<Issue> IssuesSource { get; private set; }

        /// <summary>
        /// Gets or sets the issues numeric options only source.
        /// </summary>
        /// <value>The issues numeric options only source.</value>
        public ObservableCollection<IIssueDetailsViewModel> IssuesNumericOptionsOnlySource { get; private set; }

        /// <summary>
        /// Gets or sets the issues pie source.
        /// </summary>
        /// <value>The issues pie source.</value>
        public ObservableCollection<Issue> IssuesPieSource { get; private set; }

        /// <summary>
        /// Gets or sets the current option.
        /// </summary>
        /// <value>The current option.</value>
        public OptionIssue CurrentOption
        {
            get
            {
                if (mCurrentOption == null)
                {
                    mCurrentOption = new OptionIssue();
                }
                return mCurrentOption;
            }
            set
            {
                mCurrentOption = value;
                RaisePropertyChanged("CurrentOption");
            }
        }

        /// <summary>
        /// Gets or sets the current later rated.
        /// </summary>
        /// <value>The current later rated.</value>
        public LaterRatedIssue CurrentLaterRated
        {
            get
            {
                if (mCurrentLaterRated == null)
                {
                    mCurrentLaterRated = new LaterRatedIssue();
                }
                return mCurrentLaterRated;
            }
            set
            {
                mCurrentLaterRated = value;
                RaisePropertyChanged("CurrentLaterRated");
            }
        }

        /// <summary>
        /// Gets the statistical publisher.
        /// (send messages to eNeg using RESET)
        /// </summary>
        /// <value>The statistical publisher.</value>
        private StatisticalPublisher StatisticalPublisher
        {
            get
            {
                if (mStatisticalPublisher == null)
                {
                    mStatisticalPublisher = new StatisticalPublisher(this.PreferenceSetsVM.mPrefSetsModel.Context);
                }
                return mStatisticalPublisher;
            }
        }

        #endregion Properties

        #region → Constractor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="IssuesViewModel"/> class.
        /// </summary>
        /// <param name="preferenceSetsVM">The preference sets VM.</param>
        public IssuesViewModel(PreferenceSetsViewModel preferenceSetsVM)
        {
            #region → Initialization Variables    .
            IssuesSource = new ObservableCollection<Issue>();

            IssuesNumericOptionsOnlySource = new ObservableCollection<IIssueDetailsViewModel>();

            IssuesPieSource = new ObservableCollection<Issue>();
            this.PreferenceSetsVM = preferenceSetsVM;
            #endregion

            #region → Set up event Handling       .
            preferenceSetsVM.mPrefSetsModel.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(mPrefSetsModel_PropertyChanged);
            preferenceSetsVM.mPrefSetsModel.GetIssuesComplete += new EventHandler<eNegEntityResultArgs<Issue>>(mPrefSetsModel_GetIssuesComplete);
            preferenceSetsVM.mPrefSetsModel.GetNumericIssuesComplete += new EventHandler<eNegEntityResultArgs<NumericIssue>>(mPrefSetsModel_GetNumericIssuesComplete);
            preferenceSetsVM.mPrefSetsModel.GetOptionIssuesComplete += new EventHandler<eNegEntityResultArgs<OptionIssue>>(mPrefSetsModel_GetOptionIssuesComplete);
            preferenceSetsVM.mPrefSetsModel.GetLaterRatedIssuesComplete += new EventHandler<eNegEntityResultArgs<LaterRatedIssue>>(mPrefSetsModel_GetLaterRatedIssuesComplete);
            preferenceSetsVM.mPrefSetsModel.GetIssueTypesComplete += new EventHandler<eNegEntityResultArgs<IssueType>>(mPrefSetsModel_GetIssueTypesComplete);
            preferenceSetsVM.mPrefSetsModel.SaveChangesComplete += new EventHandler<SubmitOperationEventArgs>(mPrefSetsModel_SaveChangesComplete);

            #endregion

            #region → Register needed messages    .

            PrefAppMessanger.DragIssueMessage.Register(this, OnDragIssueMessage);

            #endregion

            #region → Loading Relate Lookup Tables.
            GetIssueTypesAsync();
            #endregion
        }

        #endregion

        #region → Event Handlers .

        #region →  Loading Event Handlers .

        /// <summary>
        ///  Call back GetIssueTypes for Getting IssueType.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        void mPrefSetsModel_GetIssueTypesComplete(object sender, eNegEntityResultArgs<IssueType> e)
        {
            if (!e.HasError)
            {
                IssueTypes = e.Results;
                this.RaisePropertyChanged("IssueTypes");
            }
            else
            {
                PrefAppMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        ///Call back GetLaterRatedIssues for Getting LaterRatedIssues.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        void mPrefSetsModel_GetLaterRatedIssuesComplete(object sender, eNegEntityResultArgs<LaterRatedIssue> e)
        {
            if (!e.HasError)
            {
                LaterRatedIssues = e.Results.OrderBy(s => s.DeletedOn);
                this.RaisePropertyChanged("LaterRatedIssues");
            }
            else
            {
                //Notify User if there is any error
                PrefAppMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Call back GetOptionIssues for Getting OptionIssues.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        void mPrefSetsModel_GetOptionIssuesComplete(object sender, eNegEntityResultArgs<OptionIssue> e)
        {
            if (!e.HasError)
            {
                OptionIssues = e.Results.OrderBy(s => s.DeletedOn);
                this.RaisePropertyChanged("OptionIssues");
            }
            else
            {
                //Notify User if there is any error
                PrefAppMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        ///  Call back GetNumericIssues for Getting NumericIssues.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        void mPrefSetsModel_GetNumericIssuesComplete(object sender, eNegEntityResultArgs<NumericIssue> e)
        {
            if (!e.HasError)
            {
                NumericIssues = e.Results.OrderBy(s => s.DeletedOn);
                this.RaisePropertyChanged("NumericIssues");
            }
            else
            {
                //Notify User if there is any error
                PrefAppMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        ///Call back GetIssues for Getting Issues.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        void mPrefSetsModel_GetIssuesComplete(object sender, eNegEntityResultArgs<Issue> e)
        {
            if (!e.HasError)
            {
                Issues = e.Results.OrderBy(s => s.DeletedOn).ToList();
                IssuesSource = new ObservableCollection<Issue>(Issues);
                this.RaisePropertyChanged("IssuesSource");
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
                IsBusy = this.PreferenceSetsVM.mPrefSetsModel.IsBusy;
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
            if (this.IsLastCommandFinish)
            {
                this.OpenAddNewView();
            }
        }

        #endregion

        #region → Commands       .

        /// <summary>
        /// Gets the navigate to issues.
        /// </summary>
        /// <value>The navigate to issues.</value>
        public RelayCommand<string> NavigateCommand
        {
            get
            {
                if (mNavigateCommand == null)
                {
                    mNavigateCommand = new RelayCommand<string>((viewName) =>
                    {
                        try
                        {

                            this.ExpandFirstIssue();

                            PrefAppMessanger.FlippMessage.Send(viewName);
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            PrefAppMessanger.RaiseErrorMessage.Send(ex);
                        }
                    });
                }
                return mNavigateCommand;
            }
        }

        /// <summary>
        /// Gets the navigate to next command.
        /// </summary>
        /// <value>The navigate to next command.</value>
        public RelayCommand NavigateToNextCommand
        {
            get
            {
                if (mNavigateToNextCommand == null)
                {
                    mNavigateToNextCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            this.ExpandFirstIssue();

                            PrefAppMessanger.FlippMessage.Send(PrefAppViewTypes.InnerValuesView);
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            PrefAppMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }, () => IssuesNumericOptionsOnlySource != null &&
                          IssuesNumericOptionsOnlySource.Count > 0);
                }
                return mNavigateToNextCommand;
            }
        }

        /// <summary>
        /// Gets the add new issue command.
        /// </summary>
        /// <value>The add new issue command.</value>
        public RelayCommand<string> AddNewIssueCommand
        {
            get
            {
                if (mAddNewIssueCommand == null)
                {

                    mAddNewIssueCommand = new RelayCommand<string>((issueName) =>
                    {

                        if (!this.IsBusy)
                        {

                            CurrentIssue = null;
                            CurrentIssue = AddIssue(true, CurrentPreferenceSet);
                            Issues.Add(CurrentIssue);

                            if (!string.IsNullOrEmpty(issueName))
                            {
                                CurrentIssue.IssueName = issueName;
                            }

                            if (CanSendEditIssueMessage)
                            {
                                RefreshIssuesSource();

                                PrefAppMessanger.EditIssueMessage.Send(CurrentIssue);
                            }

                            CurrentIssue.IsNewIssue = false;

                            //Check if The current Preference Set has Negs so every add for New issue will lead to 
                            //Asking the user whether he wants to send mail to Negotiators with this new issue or not
                            if (CurrentPreferenceSet.PreferenceSetNegs.Where(s => s.Deleted == false).Count() > 0)
                            {
                                PrefAppConfigurations.IsNewIssuePending = true;

                                if (PrefAppConfigurations.PendingItems == null)
                                    PrefAppConfigurations.PendingItems = new List<PendingItem>();

                                PrefAppConfigurations.PendingItems.Add(PendingItem.CreateIssue(CurrentIssue.IssueID));
                            }
                        }
                    }, (issueName) => !this.IsBusy);
                }
                return mAddNewIssueCommand;
            }
        }

        /// <summary>
        /// Gets the change issue type command.
        /// In case of Change the Type By ComboBox
        /// </summary>
        /// <value>The change issue type command.</value>
        public RelayCommand ChangeIssueTypeCommand
        {
            get
            {
                if (mChangeIssueTypeCommand == null)
                {
                    mChangeIssueTypeCommand = new RelayCommand(() =>
                    {

                        if (!this.IsBusy &&
                            this.CurrentIssue != null &&
                            (this.CurrentIssue.EntityState == EntityState.Modified || this.CurrentIssue.EntityState == EntityState.New))
                        {
                            #region  → New Type Is LaterRated Or NotRated .

                            if (CurrentIssue.IssueTypeID == PrefAppConstant.IssueTypes.LaterRated ||
                                CurrentIssue.IssueTypeID == PrefAppConstant.IssueTypes.NotRated ||
                                CurrentIssue.IssueTypeID == PrefAppConstant.IssueTypes.SelectType)
                            {
                                while (CurrentIssue.OptionIssues.Count() > 0)
                                {
                                    this.RemoveOptionIssue(CurrentIssue.OptionIssues.First());
                                }

                                while (CurrentIssue.NumericIssues.Count() > 0)
                                {
                                    this.RemoveNumericIssue(CurrentIssue.NumericIssues.First());
                                }

                                RefreshIssuesSource(RefreshType.OthersIssuesSource);

                                return;
                            }

                            #endregion

                            #region  → New Type Is Numeric                .

                            if (CurrentIssue.IssueTypeID == PrefAppConstant.IssueTypes.Numeric)
                            {
                                while (CurrentIssue.OptionIssues.Count() > 0)
                                {
                                    this.RemoveOptionIssue(CurrentIssue.OptionIssues.First());
                                }

                                while (CurrentIssue.LaterRatedIssues.Count() > 0)
                                {
                                    this.RemoveLaterRatedIssue(CurrentIssue.LaterRatedIssues.First());
                                }
                                //add Numeric Issue
                                if (this.CurrentIssue.NumericIssues.Count == 0)
                                    AddNumericIssue(true, this.CurrentIssue);

                                RefreshIssuesSource(RefreshType.OthersIssuesSource);
                                return;
                            }

                            #endregion

                            #region  → New Type Is Options                .

                            if (CurrentIssue.IssueTypeID == PrefAppConstant.IssueTypes.Options)
                            {
                                while (CurrentIssue.NumericIssues.Count() > 0)
                                {
                                    this.RemoveNumericIssue(CurrentIssue.NumericIssues.First());
                                }

                                while (CurrentIssue.LaterRatedIssues.Count() > 0)
                                {
                                    this.RemoveLaterRatedIssue(CurrentIssue.LaterRatedIssues.First());
                                }

                                RefreshIssuesSource(RefreshType.OthersIssuesSource);
                            }

                            #endregion
                        }
                    }, () => !this.IsBusy && this.CurrentIssue != null);
                }
                return mChangeIssueTypeCommand;
            }
        }

        /// <summary>
        /// Gets the delete issue command.
        /// </summary>
        /// <value>The delete issue command.</value>
        public RelayCommand DeleteIssueCommand
        {
            get
            {
                if (mDeleteIssueCommand == null)
                {
                    mDeleteIssueCommand = new RelayCommand(() =>
                    {
                        if (!this.IsBusy)
                        {
                            #region Confirmation Message

                            Action<MessageBoxResult> callBackResult = null;

                            if (!this.IsBusy)
                            {
                                //Firstly ask user to confirm editing that item
                                DialogMessage deleteDialogMsg = new DialogMessage(
                                    this,
                                    Resources.DeleteCurrentItemMessageBoxText,
                                    result => callBackResult(result))
                                {
                                    Button = MessageBoxButton.OKCancel,
                                    Caption = Resources.ConfirmMessageBoxCaption
                                };

                                eNegMessanger.ConfirmMessage.Send(deleteDialogMsg);
                            }

                            #endregion "Confirmation Message"

                            callBackResult = (result) =>
                            {
                                if (result == MessageBoxResult.Cancel)
                                    return;

                                #region Delete Process

                                while (IssuesSource.Where(s => s.IsSelected).Count() > 0)
                                {
                                    Issue issue = IssuesSource.Where(s => s.IsSelected).FirstOrDefault();

                                    if (PrefAppConfigurations.PendingItems != null)
                                    {
                                        PendingItem pendingItem = PrefAppConfigurations.PendingItems.Where(s => s.PendingID == issue.IssueID).FirstOrDefault();

                                        if (pendingItem != null)
                                        {
                                            PrefAppConfigurations.PendingItems.Remove(pendingItem);

                                            if (IssuesSource.Where(s => s.IsSelected).Count() == 1)
                                            {
                                                PrefAppConfigurations.IsNewIssuePending = false;
                                            }
                                        }
                                    }

                                    this.RemoveIssue(issue);

                                    this.IssuesSource.Remove(issue);
                                }

                                RefreshIssuesSource();

                                #endregion
                            };


                        }
                    },
                    () => !this.IsBusy && IssuesSource != null && IssuesSource.Where(a => a.IsSelected == true).Count() > 0);
                }
                return mDeleteIssueCommand;
            }
        }

        /// <summary>
        /// Gets the select deslect negotiation to effect rating.
        /// </summary>
        /// <value>The select deslect negotiation to effect rating.</value>
        public RelayCommand<RoutedEventArgs> SelectDeslectNegotiationToEffectRatingCommand
        {
            get
            {
                if (mSelectDeslectNegotiationToEffectRatingCommand == null)
                {
                    mSelectDeslectNegotiationToEffectRatingCommand = new RelayCommand<RoutedEventArgs>((e) =>
                    {
                        if (e.OriginalSource == null)
                        {
                            if (PrefAppConfigurations.MailPreferenceSetNegID != Guid.Empty)
                            {
                                foreach (var neg in this.CurrentPreferenceSet.OngingPreferenceSetNegs)
                                {
                                    if (neg.PreferenceSetNegID == PrefAppConfigurations.MailPreferenceSetNegID)
                                    {
                                        neg.IsChecked = true;
                                        UpdateIsAllSelected();
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                IsAllNegSelected = true;
                            }
                            SubmitChangesAfterSelectNegotiationCommand.RaiseCanExecuteChanged();
                        }
                        else if (e.OriginalSource is CheckBox)
                        {
                            UpdateIsAllSelected();
                        }

                    });
                }
                return mSelectDeslectNegotiationToEffectRatingCommand;
            }
        }

        /// <summary>
        /// Gets the submit changes with effects check command.
        /// </summary>
        /// <value>The submit changes with effects check command.</value>
        public RelayCommand SubmitIssuesChangesCommand
        {
            get
            {
                if (mSubmitIssuesChangesCommand == null)
                {
                    mSubmitIssuesChangesCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            if (!this.IsBusy)
                            {
                                this.DoSave(false);
                            }
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            PrefAppMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , () => this.HasChanges);
                }
                return mSubmitIssuesChangesCommand;
            }
        }

        /// <summary>
        /// Gets the finish issues changes command.
        /// </summary>
        /// <value>The finish issues changes command.</value>
        public RelayCommand FinishIssuesChangesCommand
        {
            get
            {
                if (mFinishIssuesChangesCommand == null)
                {
                    mFinishIssuesChangesCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            if (!this.IsBusy)
                            {
                                if (this.CurrentPreferenceSet.IsEditable && this.HasChanges)
                                {


                                    this.DoSave(true);
                                }
                                else
                                {
                                    this.OpenAddNewView();

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
                return mFinishIssuesChangesCommand;
            }
        }

        /// <summary>
        /// Gets the submit changes after select negotiation command.
        /// for Popup Windows
        /// </summary>
        /// <value>The submit changes after select negotiation command.</value>
        public RelayCommand SubmitChangesAfterSelectNegotiationCommand
        {
            get
            {
                if (mSubmitChangesAfterSelectNegotiationCommand == null)
                {
                    mSubmitChangesAfterSelectNegotiationCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            // Close Pop up
                            PrefAppMessanger.FlippMessage.Send(PrefAppViewTypes.ClosePopupView);

                            PreferenceSet calcPreferenceSet = this.CurrentPreferenceSet;

                            // Thats mean you have not check all option.
                            if (CurrentPreferenceSet.OngingPreferenceSetNegs.Where(ss => ss.IsChecked == false).Count() > 0)
                            {
                                var preferenceSetIDs = new List<Guid>();

                                foreach (var preferenceSet in CurrentPreferenceSet.PreferenceSetNegs.Where(ss => ss.IsChecked == true))
                                {
                                    preferenceSetIDs.Add(preferenceSet.PreferenceSetNegID);
                                }

                                calcPreferenceSet = this.PreferenceSetsVM.mPrefSetsModel.CopyPreferenceSetTemplate(this.CurrentPreferenceSet, preferenceSetIDs);
                            }

                            //Update Maximum threshould
                            calcPreferenceSet.UpdateMaxPercentage();

                            //Update Maximum threshould
                            CurrentPreferenceSet.UpdateMaxPercentage();

                            var calculationEngine = new CalculationEngine<ClientEngineProvider>(this.PreferenceSetsVM.mPrefSetsModel.Context);
                            calculationEngine.Calculate(calcPreferenceSet);

                            // Send Statisticals to eNeg in case if changes effect Messages Score .
                            StatisticalPublisher.Send();

                            //special message for data matching to up[date new preference set.
                            PrefAppMessanger.RefreshSource.Send(PrefAppMessanger.RefreshSource.PreferenceSetChanged);

                            PrefAppMessanger.SubmitChangesMessage.SendSubmitAndMail(calcPreferenceSet);

                        }

                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            PrefAppMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , () => this.CurrentPreferenceSet != null && CurrentPreferenceSet.PreferenceSetNegs.Where(ss => ss.IsChecked == true).Count() > 0);
                }
                return mSubmitChangesAfterSelectNegotiationCommand;
            }
        }

        /// <summary>
        /// User Save changes via Calling SubmitChangesMessage so It call
        /// OnSubmitChangesMessage Method.
        /// </summary>
        public RelayCommand<Entity> CloseAddNewIssuePopUpWindowCommand
        {
            get
            {
                if (mCloseAddNewIssuePopUpWindowCommand == null)
                {
                    mCloseAddNewIssuePopUpWindowCommand = new RelayCommand<Entity>((entity) =>
                    {
                        try
                        {
                            #region → In Case of Passed entity is Issue
                            if (entity is Issue)
                            {
                                PrefAppConfigurations.IsNewIssuePending = true;

                                if (PrefAppConfigurations.PendingItems == null)
                                    PrefAppConfigurations.PendingItems = new List<PendingItem>();

                                PrefAppConfigurations.PendingItems.Add(PendingItem.CreateIssue((entity as Issue).IssueID));

                                this.CurrentPreferenceSet.Issues.Add(CurrentIssue);
                                this.RefreshIssuesSource(RefreshType.All);

                                PrefAppMessanger.ChangeScreenMessage.Send(PrefAppViewTypes.AppSettingsView);
                                Deployment.Current.Dispatcher.BeginInvoke(() =>
                                {
                                    PrefAppMessanger.EditPreferenceSetMessage
                                                    .Send(this.CurrentPreferenceSet);

                                    PrefAppMessanger.FlippMessage.Send(PrefAppViewTypes.IssuesView);
                                });

                                PrefAppMessanger.FlippMessage.Send(PrefAppViewTypes.ClosePopupView);
                            }
                            #endregion

                            #region → In Case of Passed entity is OptionIssue so I will send message to open SendMail PopUp
                            else if (entity is OptionIssue)
                            {
                                OptionsViewModel preferenceSetOptionsViewModel = new OptionsViewModel(this.CurrentIssue, this.PreferenceSetsVM.mPrefSetsModel);

                                if (IsAllIssuesValid(false) && preferenceSetOptionsViewModel.IsAllOptionsValuesValid())
                                {
                                    PrefAppConfigurations.IsNewIssuePending = true;
                                    if (PrefAppConfigurations.PendingItems == null)
                                        PrefAppConfigurations.PendingItems = new List<PendingItem>();
                                    PrefAppConfigurations.PendingItems.Add(PendingItem.CreateOptionIssue((entity as OptionIssue).OptionIssueID));
                                    PrefAppMessanger.NewPopUp.DragedValue = (entity as OptionIssue).OptionIssueValue;
                                    this.PreferenceSetsVM.mPrefSetsModel.Context.OptionIssues.Add(CurrentOption);

                                    this.RefreshIssuesSource(RefreshType.All);

                                    //Check for Data Matching
                                    PrefAppMessanger.DataMatchMessage
                                        .Send(new DataMatchingMessage()
                                        {
                                            CurrentIssue = CurrentOption.Issue,
                                            IsChecked = true,
                                            Value = CurrentOption.OptionIssueValue,
                                            CurrentMessage = null
                                        });

                                    PrefAppMessanger.SubmitChangesMessage.Send();
                                    PrefAppMessanger.FlippMessage.Send(PrefAppViewTypes.ClosePopupView);
                                }
                                preferenceSetOptionsViewModel.Cleanup();
                                preferenceSetOptionsViewModel.Dispose();
                            }
                            #endregion

                            #region → If Passed entity is LaterRatedIssue

                            if (entity is LaterRatedIssue)
                            {
                                if (IsAllIssuesValid(false) && IsAllLaterRatedValid())
                                {
                                    PrefAppConfigurations.IsNewIssuePending = true;
                                    if (PrefAppConfigurations.PendingItems == null)
                                        PrefAppConfigurations.PendingItems = new List<PendingItem>();
                                    PrefAppConfigurations.PendingItems.Add(PendingItem.CreateLaterRatedIssue((entity as LaterRatedIssue).LaterRatedIssueID));

                                    PrefAppMessanger.NewPopUp.DragedValue = (entity as LaterRatedIssue).LaterRatedIssueValue;

                                    this.PreferenceSetsVM.mPrefSetsModel.Context.LaterRatedIssues.Add(CurrentLaterRated);

                                    this.RefreshIssuesSource(RefreshType.All);

                                    //Check for Data Matching
                                    PrefAppMessanger.DataMatchMessage
                                        .Send(new DataMatchingMessage()
                                        {
                                            CurrentIssue = CurrentLaterRated.Issue,
                                            IsChecked = true,
                                            Value = CurrentLaterRated.LaterRatedIssueValue,
                                            CurrentMessage = null
                                        });

                                    PrefAppMessanger.SubmitChangesMessage.Send();
                                    PrefAppMessanger.FlippMessage.Send(PrefAppViewTypes.ClosePopupView);
                                }
                            }
                            #endregion
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            PrefAppMessanger.RaiseErrorMessage.Send(ex);
                        }
                    });
                }
                return mCloseAddNewIssuePopUpWindowCommand;
            }
        }

        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Selects the de select.
        /// </summary>
        /// <param name="state">if set to <c>true</c> [state].</param>
        private void SelectDeSelect(bool state)
        {
            if (IsNegSelectionChanged)
            {
                IsNegSelectionChanged = false;
                return;
            }
            foreach (var neg in this.CurrentPreferenceSet.OngingPreferenceSetNegs)
            {
                neg.IsChecked = state;
            }

            SubmitChangesAfterSelectNegotiationCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Updates the is all selected.
        /// </summary>
        private void UpdateIsAllSelected()
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                IsNegSelectionChanged = true;
                if (CurrentPreferenceSet.PreferenceSetNegs.Where(ss => ss.IsChecked == false).Count() > 0)
                {
                    IsAllNegSelected = false;
                }
                else
                {
                    IsAllNegSelected = true;
                }

                SubmitChangesAfterSelectNegotiationCommand.RaiseCanExecuteChanged();
            });

        }

        /// <summary>
        /// Opens the add new view.
        /// </summary>
        private void OpenAddNewView()
        {
            this.IsLastCommandFinish = false;

            //by threading to make delay untill any other save event finished.
            Deployment.Current
                      .Dispatcher
                      .BeginInvoke(() =>
                          {
                              PrefAppMessanger.FlippMessage.Send(PrefAppViewTypes.ForceAddPrefSetView);
                          });
        }

        /// <summary>
        /// Expands the first issue.
        /// </summary>
        private void ExpandFirstIssue()
        {
            if (this.IssuesNumericOptionsOnlySource.Where(s => s.IsExpanded).Count() <= 0)
            {
                IIssueDetailsViewModel issue = this.IssuesNumericOptionsOnlySource.FirstOrDefault();

                if (issue != null)
                {
                    issue.IsExpanded = true;
                }
            }
        }

        /// <summary>
        /// Does the save.
        /// </summary>
        private void DoSave(bool isLastCommandFinish)
        {
            if (!IsAllIssuesValid(true))
            {
                PrefAppMessanger.FlippMessage.Send(PrefAppViewTypes.InnerIssuesView);
                return;
            }

            bool isValid = true;

            foreach (var vmInstanceItem in this.IssuesNumericOptionsOnlySource)
            {
                if (!vmInstanceItem.IsAllValid)
                {
                    PrefAppMessanger.FlippMessage.Send(PrefAppViewTypes.InnerValuesView);

                    vmInstanceItem.IsExpanded = true;

                    isValid = false;
                }
                else
                {
                    vmInstanceItem.IsExpanded = false;
                }
            }


            if (isValid)
            {
                this.ExpandFirstIssue();

                this.IsLastCommandFinish = isLastCommandFinish;

                //Check if negotiation in the prefence set will effect all or not.
                PrefAppMessanger.SubmitChangesMessage.Send(true);
            }
        }

        /// <summary>
        /// Called when [drag issue message].
        /// </summary>
        /// <param name="issuesNames">The issues names.</param>
        private void OnDragIssueMessage(IEnumerable<string> issuesNames)
        {
            var issuesNameList = issuesNames.ToList<string>();
            //check repeating
            foreach (var issue in this.CurrentPreferenceSet.Issues)
            {
                var tmpIssue = issuesNameList.Where(s => issue.IssueName.Equals(s)).FirstOrDefault();

                if (!string.IsNullOrEmpty(tmpIssue))
                {
                    issuesNameList.Remove(tmpIssue);
                }
            }

            if (issuesNameList.Count > 0)
            {
                //CanSendEditIssueMessage = false;

                for (var i = 0; i < issuesNameList.Count; i++)
                {
                    if (i == (issuesNameList.Count - 1))
                    {
                        CanSendEditIssueMessage = true;
                    }

                    AddNewIssueCommand.Execute(issuesNameList[i]);
                }
            }
        }

        #endregion

        #region → Public         .

        /// <summary>
        /// Refreshes the issues source.
        /// </summary>
        public void RefreshIssuesSource(RefreshType refreshType = RefreshType.All)
        {
            try
            {
                if (CurrentPreferenceSet != null)
                {
                    if (refreshType == RefreshType.All || refreshType == RefreshType.IssueSource || refreshType == RefreshType.RejectChanges)
                    {
                        IssuesSource = new ObservableCollection<Issue>(CurrentPreferenceSet.Issues.OrderBy(s => s.DeletedOn));
                        this.RaisePropertyChanged("IssuesSource");
                    }
                    if (refreshType == RefreshType.All || refreshType == RefreshType.OthersIssuesSource || refreshType == RefreshType.RejectChanges)
                    {
                        IssuesPieSource = new ObservableCollection<Issue>(this.CurrentPreferenceSet.Issues.Where
                            (s => s.IssueTypeID != PrefAppConstant.IssueTypes.NotRated && s.IssueTypeID != PrefAppConstant.IssueTypes.SelectType)
                            .OrderBy(s => s.DeletedOn));
                        this.RaisePropertyChanged("IssuesPieSource");
                    }
                    if (refreshType == RefreshType.All || refreshType == RefreshType.OthersIssuesSource)
                    {
                        var viewModelsList = new List<IIssueDetailsViewModel>();

                        foreach (var issueItem in this.CurrentPreferenceSet
                                                      .Issues.Where(s => (s.IssueTypeID == PrefAppConstant.IssueTypes.Numeric ||
                                                                          s.IssueTypeID == PrefAppConstant.IssueTypes.Options))
                                                      .OrderBy(s => s.DeletedOn))
                        {
                            if (issueItem.IssueTypeID == PrefAppConstant.IssueTypes.Numeric)
                            {
                                viewModelsList.Add(new NumericViewModel(issueItem));
                            }
                            else if (issueItem.IssueTypeID == PrefAppConstant.IssueTypes.Options)
                            {
                                viewModelsList.Add(new OptionsViewModel(issueItem, this.PreferenceSetsVM.mPrefSetsModel));
                            }
                        }

                        //Setting Issues with Type Numeric And Options Only
                        IssuesNumericOptionsOnlySource = new ObservableCollection<IIssueDetailsViewModel>(viewModelsList);

                        this.RaisePropertyChanged("IssuesNumericOptionsOnlySource");

                        this.ExpandFirstIssue();
                        this.RaiseCanExecuteChanged();

                    }
                }
            }
            catch (Exception ex)
            {
                PrefAppMessanger.RaiseErrorMessage.Send(ex);
            }
        }

        /// <summary>
        /// Loads the preference issues datails.
        /// </summary>
        /// <param name="organizationIDs">The organization I ds.</param>
        public void LoadPreferenceIssuesDatails(Guid[] organizationIDs)
        {
            this.OrganizationIDs = organizationIDs;

            GetIssuesAsync();

            GetLaterRatedIssuesAsync();

            GetNumericIssuesAsync();

            GetOptionIssuesAsync();
        }

        /// <summary>
        /// Determines whether [is all issues valid].
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if [is all issues valid]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsAllIssuesValid(bool checkForScoreMatch)
        {
            //Hack: Sleect Type 

            bool isAllValid = true;
            IsScoreNotValid = false;

            if (this.CurrentPreferenceSet.Issues.Count > 0)
            {
                foreach (var issue in this.CurrentPreferenceSet.Issues)
                {
                    try //Special for problem in grid
                    {
                        issue.ValidationErrors.Clear();
                    }
                    catch { }

                    if (string.IsNullOrEmpty(issue.IssueName))
                    {
                        issue.IssueName = string.Empty;
                        isAllValid = false;
                    }
                    else if (issue.IssueName.Length > 300)
                    {
                        issue.ValidationErrors.Add(new ValidationResult(Resources.IssueLongName, new string[] { "IssueName" }));
                        isAllValid = false;
                    }
                    else
                    {
                        if (CurrentPreferenceSet.Issues.Count(s => s.IssueName.ToLower() == issue.IssueName.ToLower()
                                                  && s.IssueID != issue.IssueID) > 0)
                        {
                            issue.ValidationErrors.Add(new ValidationResult(Resources.RepeatedIssue, new string[] { "IssueName" }));
                            isAllValid = false;
                        }
                    }

                    if (checkForScoreMatch)
                    {
                        if (issue.IssueTypeID == PrefAppConstant.IssueTypes.SelectType)
                        {
                            try //Special for problem in grid
                            {
                                issue.ValidationErrors.Add(new ValidationResult(Resources.UndefiendType, new string[] { "IssueTypeID" }));
                            }
                            catch
                            { }

                            isAllValid = false;
                        }
                    }

                    if (!(issue.TryValidateObject() &&
                          issue.TryValidateProperty("IssueName")))
                    {
                        isAllValid = false;
                    }
                }
                if (checkForScoreMatch)
                {
                    //Check if the Total of score is 100%
                    Func<Issue, decimal> selector = s => { return s.IssueWeight; };
                    IsScoreNotValid = IssuesSource.Sum(selector) != 100;

                    if (IsScoreNotValid)
                        isAllValid = false;
                }
            }


            return isAllValid;

        }

        /// <summary>
        /// Determines whether [is all later rated valid].
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if [is all later rated valid]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsAllLaterRatedValid()
        {
            bool IsAllvalid = true;

            if (CurrentIssue != null && CurrentIssue.LaterRatedIssues != null && CurrentIssue.LaterRatedIssues.Count() > 0)
            {
                foreach (var item in CurrentIssue.LaterRatedIssues)
                {
                    item.ValidationErrors.Clear();

                    if (string.IsNullOrEmpty(item.LaterRatedIssueValue))
                    {
                        item.LaterRatedIssueValue = string.Empty;
                        IsAllvalid = false;
                    }
                    else if (item.LaterRatedIssueValue.Length > 100)
                    {
                        item.ValidationErrors.Add(new ValidationResult(Resources.OptionLongName, new string[] { "LaterRatedIssueValue" }));
                        IsAllvalid = false;
                    }
                    else
                    {
                        if (CurrentIssue.LaterRatedIssues.Count(s => (s.LaterRatedIssueValue.ToLower() == item.LaterRatedIssueValue.ToLower())
                                                && (s.LaterRatedIssueID != item.LaterRatedIssueID)) > 0)
                        {
                            (item as LaterRatedIssue).ValidationErrors.Add(new ValidationResult(Resources.RepeatedOptions, new string[] { "LaterRatedIssueValue" }));
                            IsAllvalid = false;
                        }
                    }

                    if (!(item.TryValidateObject() &&
                        item.TryValidateProperty("LaterRatedIssueValue")))
                    {
                        IsAllvalid = false;
                    }
                }
            }
            return IsAllvalid;

        }

        /// <summary>
        /// Gets the issue types async.
        /// </summary>
        public void GetIssueTypesAsync()
        {

            this.PreferenceSetsVM.mPrefSetsModel.GetIssueTypesAsync();
        }

        /// <summary>
        /// Gets the issues async.
        /// </summary>
        public void GetIssuesAsync()
        {
            this.PreferenceSetsVM.mPrefSetsModel.GetIssuesAsync(OrganizationIDs);
        }

        /// <summary>
        /// Gets the numeric issues async.
        /// </summary>
        public void GetNumericIssuesAsync()
        {
            this.PreferenceSetsVM.mPrefSetsModel.GetNumericIssuesAsync(OrganizationIDs);
        }

        /// <summary>
        /// Gets the option issues async.
        /// </summary>
        public void GetOptionIssuesAsync()
        {
            this.PreferenceSetsVM.mPrefSetsModel.GetOptionIssuesAsync(OrganizationIDs);
        }

        /// <summary>
        /// Gets the later rated issue async.
        /// </summary>
        public void GetLaterRatedIssuesAsync()
        {
            this.PreferenceSetsVM.mPrefSetsModel.GetLaterRatedIssueAsync(OrganizationIDs);
        }

        /// <summary>
        /// Adds the issue.
        /// </summary>
        /// <param name="setInContext">if set to <c>true</c> [set in context].</param>
        /// <param name="prefSet">The pref set.</param>
        /// <returns></returns>
        public Issue AddIssue(bool setInContext, PreferenceSet prefSet)
        {
            return this.PreferenceSetsVM.mPrefSetsModel.AddIssue(setInContext, prefSet);
        }

        /// <summary>
        /// Adds the numeric issue.
        /// </summary>
        /// <param name="setInContext">if set to <c>true</c> [set in context].</param>
        /// <param name="issue">The issue.</param>
        /// <returns></returns>
        public NumericIssue AddNumericIssue(bool setInContext, Issue issue)
        {
            return this.PreferenceSetsVM.mPrefSetsModel.AddNumericIssue(setInContext, issue);
        }

        /// <summary>
        /// Adds the option issue.
        /// </summary>
        /// <param name="setInContext">if set to <c>true</c> [set in context].</param>
        /// <param name="issue">The issue.</param>
        /// <returns></returns>
        public OptionIssue AddOptionIssue(bool setInContext, Issue issue)
        {
            return this.PreferenceSetsVM.mPrefSetsModel.AddOptionIssue(setInContext, issue);
        }

        /// <summary>
        /// Adds the later rated issue.
        /// </summary>
        /// <param name="setInContext">if set to <c>true</c> [set in context].</param>
        /// <param name="issue">The issue.</param>
        /// <returns></returns>
        public LaterRatedIssue AddLaterRatedIssue(bool setInContext, Issue issue)
        {
            return this.PreferenceSetsVM.mPrefSetsModel.AddLaterRatedIssue(setInContext, issue);
        }

        /// <summary>
        /// Removes the issue.
        /// </summary>
        /// <param name="issue">The issue.</param>
        public void RemoveIssue(Issue issue)
        {
            this.PreferenceSetsVM.mPrefSetsModel.RemoveIssue(issue);
        }

        /// <summary>
        /// Removes the numeric issue.
        /// </summary>
        /// <param name="numericIssue">The numeric issue.</param>
        public void RemoveNumericIssue(NumericIssue numericIssue)
        {
            this.PreferenceSetsVM.mPrefSetsModel.RemoveNumericIssue(numericIssue);
        }

        /// <summary>
        /// Removes the option issue.
        /// </summary>
        /// <param name="optionIssue">The option issue.</param>
        public void RemoveOptionIssue(OptionIssue optionIssue)
        {
            this.PreferenceSetsVM.mPrefSetsModel.RemoveOptionIssue(optionIssue);
        }

        /// <summary>
        /// Removes the later rated issue.
        /// </summary>
        /// <param name="laterRatedIssue">The later rated issue.</param>
        public void RemoveLaterRatedIssue(LaterRatedIssue laterRatedIssue)
        {
            this.PreferenceSetsVM.mPrefSetsModel.RemoveLaterRatedIssue(laterRatedIssue);
        }

        /// <summary>
        /// Rejects the changes.
        /// </summary>
        public void RejectChanges()
        {
            if (PrefAppConfigurations.IsNewIssuePending)
            {
                RefreshIssuesSource(RefreshType.All);
            }
            else
            {
                IsScoreNotValid = false;
                this.PreferenceSetsVM.RejectChanges();
                RefreshIssuesSource(RefreshType.RejectChanges);
                RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Raises the can execute changed.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            SubmitChangesAfterSelectNegotiationCommand.RaiseCanExecuteChanged();
            AddNewIssueCommand.RaiseCanExecuteChanged();
            DeleteIssueCommand.RaiseCanExecuteChanged();
            ChangeIssueTypeCommand.RaiseCanExecuteChanged();
            SubmitIssuesChangesCommand.RaiseCanExecuteChanged();
            NavigateToNextCommand.RaiseCanExecuteChanged();
        }

        #region "ICleanup interface implementation"

        /// <summary>
        /// Cleanup negotiation model
        /// </summary>
        public override void Cleanup()
        {
            // unregister all events
            this.PreferenceSetsVM.mPrefSetsModel.PropertyChanged -= new System.ComponentModel.PropertyChangedEventHandler(mPrefSetsModel_PropertyChanged);
            this.PreferenceSetsVM.mPrefSetsModel.GetIssuesComplete -= new EventHandler<eNegEntityResultArgs<Issue>>(mPrefSetsModel_GetIssuesComplete);
            this.PreferenceSetsVM.mPrefSetsModel.GetNumericIssuesComplete -= new EventHandler<eNegEntityResultArgs<NumericIssue>>(mPrefSetsModel_GetNumericIssuesComplete);
            this.PreferenceSetsVM.mPrefSetsModel.GetOptionIssuesComplete -= new EventHandler<eNegEntityResultArgs<OptionIssue>>(mPrefSetsModel_GetOptionIssuesComplete);
            this.PreferenceSetsVM.mPrefSetsModel.GetLaterRatedIssuesComplete -= new EventHandler<eNegEntityResultArgs<LaterRatedIssue>>(mPrefSetsModel_GetLaterRatedIssuesComplete);
            this.PreferenceSetsVM.mPrefSetsModel.GetIssueTypesComplete -= new EventHandler<eNegEntityResultArgs<IssueType>>(mPrefSetsModel_GetIssueTypesComplete);

            // unregister any messages for this ViewModel
            // Cleanup itself
            Messenger.Default.Unregister(this);

            base.Cleanup();
        }

        #endregion "ICleanup interface implementation"

        #endregion  Public

        #endregion Methods
    }
}
