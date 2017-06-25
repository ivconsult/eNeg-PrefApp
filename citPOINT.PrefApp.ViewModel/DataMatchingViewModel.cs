/* Comments
 * this section will Discribe All relations between All Objects Here
 * 
 * Suppose we Have  Preference Set called "Purchase a Car"e
 * 
 * 
 *  → Purchase a Car             (PreferenceSet       Table)
 *    → Purchase BMW             (PreferenceSetNeg    Table)(Negotiation) ← ← (NegotiationDetails  Obj. from eNeg).
 *      →Conversation With Cario (NegConversation     Table)(Conversatin) ← ← (ConversationDetails Obj. from eNeg).
 *          →Message 1           (ConversationMessage Table)(Messages   ) ← ← (MessageDetails      Obj. from eNeg).
 * 
 * 
 */

#region → Usings   .
using citPOINT.eNeg.Common;
using citPOINT.PrefApp.Common;
using citPOINT.PrefApp.Data;
using citPOINT.PrefApp.Data.Web;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.ServiceModel.DomainServices.Client;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Telerik.Windows.Controls.Charting;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using citPOINT.eNeg.Common.eNegApps;
using System.Diagnostics;
#endregion

#region → History  .

/* Date         User              Change
 * 
 * 11.10.11     M.Wahab             • Creation
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
    #region  Using MEF to export DataMatchingViewModel
    /// <summary>
    /// Class to Manage Data Matching
    /// </summary>
    [Export(PrefAppViewModelTypes.DataMatchingViewModel)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    #endregion
    public class DataMatchingViewModel : GalaSoft.MvvmLight.ViewModelBase
    {
        #region → Fields         .

        public IDataMatchingModel mDataMatchingModel;
        private PreferenceSetsViewModel mPreferenceSetsVM;
        private bool mIsBusy = false;
        private bool mIseNegServicesBusy = true;
        private bool RunQueueForApplyChanges = false;
        private bool mIsDMInAddonActive = false;
        private bool mIsChangingDMStatusInAddonPossible = true;
        private bool mIsSyncroized = false;
        private bool SynchronizeIsLocked = false;
        private bool IsLastActionCommandUnAssigned = false;

        private int mLoadingLock = 3;

        private bool isFirstTime = true;

        private ReportViewModel mReportVM;

        private bool mAllowSendStatisticals = true;

        //Used To manage the Busy State of the REST Protocol.
        private int NumberOfExpectedCallBacks = 0;
        //Used To Manage the Busy State of the REST Protocol. as Temp Counter.
        private int NumberOfExpectedCallBacksCounter = 0;

        //Tables related to Preference Set (With its Scores).
        private List<PreferenceSetNeg> mPreferenceSetNegotiations;
        private List<NegConversation> mNegotiationConversations;
        private List<ConversationMessage> mConversationMessages;

        // → From eNeg ← .
        private List<Negotiation> mNegotiationDetails;
        private List<Negotiation> mAvailableNegotiations;
        private List<Negotiation> mActiveOngingNegotiations;

        private List<Conversation> mConversationDetails;
        private List<Message> mMessageDetails;

        //Loading Data Matching Tables.
        private IEnumerable<MessageIssue> mMessageIssues;
        private IEnumerable<MessageOptionIssue> mMessageOptionIssues;
        private IEnumerable<MessageLaterRatedIssue> mMessageLaterRatedIssues;

        private PreferenceSet mCurrentPreferenceSet;
        private NegConversation mCurrentConversation;
        private ConversationMessage mCurrentMessage;
        private MessageFilter mMessageFilterType;
        private ObservableCollection<ConversationMessage> mCurrentConversationMessagesSource;
        private List<FormatedWord> mWordsFormatSource;
        private PreferenceSetNeg mCurrentNegotiation;
        private CalculationEngine<ClientEngineProvider> mCalculationEngine;
        private StatisticalPublisher mStatisticalPublisher;

        #region → Commands       .

        private RelayCommand mSubmitChangesCommand = null;
        private RelayCommand mCancelChangesCommand = null;
        private RelayCommand mAddNegotiationCommand = null;
        private RelayCommand mAssignPreferenceSetCommand = null;
        private RelayCommand mRemoveNegotiationCommand = null;
        private RelayCommand mAddUndefinedIssue = null;
        private RelayCommand mSwitchDataMatchingStatusInAddon = null;
        private PreferenceSet mSelectedPrefSet;

        #endregion

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets the selected pref set.
        /// </summary>
        /// <value>The selected pref set.</value>
        public PreferenceSet SelectedPrefSet
        {
            get
            {
                return mSelectedPrefSet;
            }
            set
            {
                mSelectedPrefSet = value;
                RaisePropertyChanged("SelectedPrefSet");
                RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Gets or sets the report VM.
        /// </summary>
        /// <value>The report VM.</value>
        public ReportViewModel ReportVM
        {
            get
            {
                return mReportVM;
            }
            set
            {
                mReportVM = value;
                RaisePropertyChanged("ReportVM");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is syncroized.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is syncroized; otherwise, <c>false</c>.
        /// </value>
        public bool IsSyncroized
        {
            get
            {
                return mIsSyncroized;
            }
            set
            {
                mIsSyncroized = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is changing DM status in addon possible.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is changing DM status in addon possible; otherwise, <c>false</c>.
        /// </value>
        public bool IsChangingDMStatusInAddonPossible
        {
            get
            {
                return mIsChangingDMStatusInAddonPossible;
            }
            set
            {
                mIsChangingDMStatusInAddonPossible = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is DM in addon active.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is DM in addon active; otherwise, <c>false</c>.
        /// </value>
        public bool IsDMInAddonActive
        {
            get
            {
                return mIsDMInAddonActive;
            }
            set
            {
                mIsDMInAddonActive = value;
                RaisePropertyChanged("IsDMInAddonActive");
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
                return mDataMatchingModel.HasChanges;
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
                return mIsBusy ||
                      mDataMatchingModel.IsBusy ||
                      this.IseNegServicesBusy ||
                      mPreferenceSetsVM.IsBusy;
            }

            set
            {
                mIsBusy = value;

                this.RaisePropertyChanged("IsBusy");

                if (!this.IsBusy)
                {
                    if (RunQueueForApplyChanges)
                    {
                        IsSyncroized = true; //because it will not enter synco area
                        ApplyChanges();
                    }
                    else if (!IsSyncroized && (NumberOfExpectedCallBacks > 0))
                    {
                        Deployment.Current.Dispatcher.BeginInvoke(() =>
                            {
                                //System.Threading.Thread.Sleep(100);

                                SynchronizeData();
                            });
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [is eNeg services busy].
        /// </summary>
        /// <value><c>true</c> if [ise neg services busy]; otherwise, <c>false</c>.</value>
        public bool IseNegServicesBusy
        {
            get { return mIseNegServicesBusy; }
            set
            {
                mIseNegServicesBusy = value;
                this.RaisePropertyChanged("IseNegServicesBusy");

                if (!this.IsBusy && !IsSyncroized)
                {
                    Deployment.Current.Dispatcher.BeginInvoke(() =>
                        {
                            if (!this.IsBusy && !IsSyncroized)
                            {
                                //System.Threading.Thread.Sleep(100);

                                SynchronizeData();
                            }
                        });
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [allow send statisticals].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [allow send statisticals]; otherwise, <c>false</c>.
        /// </value>
        public bool AllowSendStatisticals
        {
            get { return mAllowSendStatisticals; }
            set { mAllowSendStatisticals = value; }
        }

        /// <summary>
        /// Gets the calculation engine.
        /// </summary>
        /// <value>The calculation engine.</value>
        CalculationEngine<ClientEngineProvider> CalculationEngine
        {
            get
            {
                //if (mCalculationEngine==null)
                //{
                mCalculationEngine = new CalculationEngine<ClientEngineProvider>(this.mDataMatchingModel.Context);
                //}
                return mCalculationEngine;
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
                    mStatisticalPublisher = new StatisticalPublisher(this.mDataMatchingModel.Context);
                }
                return mStatisticalPublisher;
            }
        }

        /// <summary>
        /// Gets or sets the preference sets VM.
        /// </summary>
        /// <value>The preference sets VM.</value>
        public PreferenceSetsViewModel PreferenceSetsVM
        {
            get
            {
                return mPreferenceSetsVM;
            }
            set
            {
                mPreferenceSetsVM = value;

                //loading data
                LoadAll();
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has messages.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has messages; otherwise, <c>false</c>.
        /// </value>
        public bool HasMessages
        {
            get
            {

                return this.CurrentMessage != null &&
                    this.CurrentConversationMessagesSource.Count > 0;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has no message.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has no message; otherwise, <c>false</c>.
        /// </value>
        public bool HasNoMessages
        {
            get { return !HasMessages; }
        }

        /// <summary>
        /// Gets or sets the type of the message filter.
        /// </summary>
        /// <value>The type of the message filter.</value>
        public MessageFilter MessageFilterType
        {
            get
            {
                return mMessageFilterType;
            }
            set
            {
                mMessageFilterType = value;

                //Filter the Message by received or Send Messages.
                FilterMessages();
            }
        }

        /// <summary>
        /// Gets or sets the current Negotiation.
        /// </summary>
        /// <value>The current Negotiation.</value>
        public PreferenceSetNeg CurrentNegotiation
        {
            get
            {
                return mCurrentNegotiation;
            }

            set
            {
                PrefAppConfigurations.IsNegotiationAttachedPreferenceSet = value != null;

                mCurrentNegotiation = value;

                ReportVM.CurrentNegotiation = value;

                this.CurrentConversation = value == null ? null : value.NegConversations.FirstOrDefault();

                this.RaisePropertyChanged("CurrentNegotiation");

            }
        }

        /// <summary>
        /// Gets or sets the current conversation.
        /// </summary>
        /// <value>The current conversation.</value>
        public NegConversation CurrentConversation
        {
            get
            {
                return mCurrentConversation;
            }

            set
            {
                mCurrentConversation = value;

                this.ReportVM.CurrentConversation = value;

                this.RaisePropertyChanged("CurrentConversation");

                FilterMessages();
            }
        }

        /// <summary>
        /// Gets or sets the current conversation messages source.
        /// </summary>
        /// <value>The current conversation messages source.</value>
        public ObservableCollection<ConversationMessage> CurrentConversationMessagesSource
        {
            get
            {
                return mCurrentConversationMessagesSource;
            }

            set
            {
                mCurrentConversationMessagesSource = value;

                this.RaisePropertyChanged("CurrentConversationMessagesSource");

                #region → Setting the Current Message the First Message in the Source .

                ConversationMessage conversationMessage = mCurrentConversationMessagesSource.FirstOrDefault();

                //To Update Formating
                UpdateWordsFormatSource();

                ConversationMessage.WordsFormatStaticSource = this.WordsFormatSource;


                if (conversationMessage != null)
                {
                    this.CurrentMessage = conversationMessage;
                }
                else
                {
                    this.CurrentMessage = null;
                }

                //To Set the Current Message in View (ComboBox)
                PrefAppMessanger.EditConversationMessage.Send(this.CurrentMessage);

                #endregion
            }
        }

        /// <summary>
        /// Gets or sets the words format source.
        /// </summary>
        /// <value>The words format source.</value>
        public List<FormatedWord> WordsFormatSource
        {
            get
            {
                return mWordsFormatSource;
            }

            set
            {
                mWordsFormatSource = value;
                this.RaisePropertyChanged("WordsFormatSource");
            }
        }

        /// <summary>
        /// Gets the current preference set.
        /// </summary>
        /// <value>The current preference set.</value>
        public PreferenceSet CurrentPreferenceSet
        {
            get
            {
                return mCurrentPreferenceSet;
            }

            set
            {
                if (mPreferenceSetsVM != null)
                    mPreferenceSetsVM.CurrentPreferenceSet = value;

                mCurrentPreferenceSet = value;

                this.RaisePropertyChanged("IssuesSource");
                this.RaisePropertyChanged("CurrentPreferenceSet");
            }
        }

        /// <summary>
        /// Gets the issues source.
        /// </summary>
        /// <value>The issues source.</value>
        public ObservableCollection<Issue> IssuesSource
        {
            get
            {
                return CurrentPreferenceSet == null ?
                    new ObservableCollection<Issue>() :
                    new ObservableCollection<Issue>(CurrentPreferenceSet.Issues.OrderBy(s => s.DeletedOn));
            }
        }

        /// <summary>
        /// Gets or sets the current message.
        /// </summary>
        /// <value>The current message.</value>
        public ConversationMessage CurrentMessage
        {
            get
            {
                return mCurrentMessage;
            }

            set
            {
                mCurrentMessage = value;

                this.RaisePropertyChanged("CurrentMessage");

                this.RaisePropertyChanged("IssuesSource");

                if (mCurrentMessage != null)
                {
                    mCurrentMessage.RefereshWordsFormatSource();
                }
            }
        }

        /// <summary>
        /// Gets or sets the preference set negotiations.
        /// </summary>
        /// <value>The preference set negotiations.</value>
        public List<PreferenceSetNeg> PreferenceSetNegotiations
        {
            get
            {
                return mPreferenceSetNegotiations;
            }

            set
            {
                if (value != mPreferenceSetNegotiations)
                {
                    mPreferenceSetNegotiations = value;
                    this.RaisePropertyChanged("PreferenceSetNegotiations");
                }
            }
        }

        /// <summary>
        /// Gets or sets the negotiation conversations.
        /// </summary>
        /// <value>The negotiation conversations.</value>
        public List<NegConversation> NegotiationConversations
        {
            get
            {
                return mNegotiationConversations;
            }

            private set
            {
                if (value != mNegotiationConversations)
                {
                    mNegotiationConversations = value;
                    this.ReportVM.NegotiationConversations = value;
                    this.RaisePropertyChanged("NegotiationConversations");
                }
            }
        }

        /// <summary>
        /// Gets or sets the conversation messages.
        /// </summary>
        /// <value>The conversation messages.</value>
        public List<ConversationMessage> ConversationMessages
        {
            get
            {
                return mConversationMessages;
            }
            private set
            {
                if (value != mConversationMessages)
                {
                    mConversationMessages = value;
                    this.ReportVM.ConversationMessages = value;
                    this.RaisePropertyChanged("ConversationMessages");
                }
            }
        }

        #region → From eNeg by Rest Protocol  .

        /// <summary>
        /// Gets or sets the active onging negotiations.
        /// </summary>
        /// <value>The active onging negotiations.</value>
        public List<Negotiation> ActiveOngingNegotiations
        {
            get { return mActiveOngingNegotiations; }
            set { mActiveOngingNegotiations = value; }
        }

        /// <summary>
        /// Gets or sets the available negotiations.
        /// </summary>
        /// <value>The available negotiations.</value>
        public List<Negotiation> AvailableNegotiations
        {
            get
            {
                return mAvailableNegotiations;
            }
            private set
            {
                if (value != mAvailableNegotiations)
                {
                    mAvailableNegotiations = value;
                    this.RaisePropertyChanged("AvailableNegotiations");
                }
            }
        }

        /// <summary>
        /// Gets or sets the negotiation details.
        /// </summary>
        /// <value>The negotiation details.</value>
        public List<Negotiation> NegotiationDetails
        {
            get
            {
                return mNegotiationDetails;
            }

            private set
            {
                if (value != mNegotiationDetails)
                {
                    mNegotiationDetails = value;
                    this.RaisePropertyChanged("NegotiationDetails");
                }
            }
        }

        /// <summary>
        /// Gets or sets the conversation details.
        /// </summary>
        /// <value>The conversation details.</value>
        public List<Conversation> ConversationDetails
        {
            get
            {
                return mConversationDetails;
            }

            private set
            {
                if (value != mConversationDetails)
                {
                    mConversationDetails = value;
                    this.RaisePropertyChanged("ConversationDetails");
                }
            }
        }

        /// <summary>
        /// Gets or sets the message details.
        /// </summary>
        /// <value>The message details.</value>
        public List<Message> MessageDetails
        {
            get
            {
                return mMessageDetails;
            }

            private set
            {
                if (value != mMessageDetails)
                {
                    mMessageDetails = value;
                    this.RaisePropertyChanged("MessageDetails");
                }
            }
        }

        #endregion

        /// <summary>
        /// Gets or sets the message issues.
        /// </summary>
        /// <value>The message issues.</value>
        public IEnumerable<MessageIssue> MessageIssues
        {
            get
            {
                return mMessageIssues;
            }

            private set
            {
                if (value != mMessageIssues)
                {
                    mMessageIssues = value;
                    this.ReportVM.MessageIssues = value;
                    this.RaisePropertyChanged("MessageIssues");
                }
            }
        }

        /// <summary>
        /// Gets or sets the message option issues.
        /// </summary>
        /// <value>The message option issues.</value>
        public IEnumerable<MessageOptionIssue> MessageOptionIssues
        {
            get
            {
                return mMessageOptionIssues;
            }

            private set
            {
                if (value != mMessageOptionIssues)
                {
                    mMessageOptionIssues = value;
                    this.ReportVM.MessageOptionIssues = value;
                    this.RaisePropertyChanged("MessageOptionIssues");
                }
            }
        }

        /// <summary>
        /// Gets or sets the message later rated issues.
        /// </summary>
        /// <value>The message later rated issues.</value>
        public IEnumerable<MessageLaterRatedIssue> MessageLaterRatedIssues
        {
            get
            {
                return mMessageLaterRatedIssues;
            }

            private set
            {
                if (value != mMessageLaterRatedIssues)
                {
                    mMessageLaterRatedIssues = value;
                    this.ReportVM.MessageLaterRatedIssues = value;
                    this.RaisePropertyChanged("MessageLaterRatedIssues");
                }
            }
        }

        /// <summary>
        /// Gets or sets the loading lock.
        /// for wiating loading from database.
        /// </summary>
        /// <value>The loading lock.</value>
        private int LoadingLock
        {
            get
            {
                return mLoadingLock;
            }
            set
            {
                if (value < 0)
                {
                    value = 0;
                }

                mLoadingLock = value;

                if (value <= 0 && !this.IsBusy && !IsSyncroized)
                {
                    Deployment.Current.Dispatcher.BeginInvoke(() =>
                    {
                        if (!this.IsBusy && !IsSyncroized)
                        {
                            SynchronizeData();
                        }
                    });
                }
            }
        }

        #endregion

        #region → Constructor    .

        /// <summary>
        /// constructor that take a parameter IDataMatchingModel and give its values to mDataMatchingModel and register required events 
        /// </summary>
        /// <param name="DataMatchingModel">Value of DataMatchingModel</param>
        [ImportingConstructor]
        public DataMatchingViewModel(IDataMatchingModel DataMatchingModel)
        {
            #region → Initialization Variables .

            mDataMatchingModel = DataMatchingModel;
            NegotiationDetails = new List<Negotiation>();
            ConversationDetails = new List<Conversation>();
            MessageDetails = new List<Message>();

            PreferenceSetNegotiations = new List<PreferenceSetNeg>();
            ReportVM = new ReportViewModel(this);
            #endregion

            #region → Set up event Handling    .

            mDataMatchingModel.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(mDataMatchingModel_PropertyChanged);
            mDataMatchingModel.SaveChangesComplete += new EventHandler<SubmitOperationEventArgs>(mDataMatchingModel_SaveChangesComplete);

            mDataMatchingModel.GetAvailableNegotiationsToAnalysisComplete += new EventHandler<eNegEntityResultArgs<Negotiation>>(mDataMatchingModel_GetAvailableNegotiationsToAnalysisComplete);
            mDataMatchingModel.GetConversationMessagesComplete += new EventHandler<eNegEntityResultArgs<ConversationMessage>>(mDataMatchingModel_GetConversationMessagesComplete);
            mDataMatchingModel.GetConversationsDetailsByIDsComplete += new EventHandler<eNegEntityResultArgs<Conversation>>(mDataMatchingModel_GetConversationsDetailsByIDsComplete);
            mDataMatchingModel.GetMessageIssuesComplete += new EventHandler<eNegEntityResultArgs<MessageIssue>>(mDataMatchingModel_GetMessageIssuesComplete);
            mDataMatchingModel.GetMessageLaterRatedIssuesComplete += new EventHandler<eNegEntityResultArgs<MessageLaterRatedIssue>>(mDataMatchingModel_GetMessageLaterRatedIssuesComplete);
            mDataMatchingModel.GetMessageOptionIssuesComplete += new EventHandler<eNegEntityResultArgs<MessageOptionIssue>>(mDataMatchingModel_GetMessageOptionIssuesComplete);
            mDataMatchingModel.GetMessagesDetailsByIDsComplete += new EventHandler<eNegEntityResultArgs<Message>>(mDataMatchingModel_GetMessagesDetailsByIDsComplete);
            mDataMatchingModel.GetNegotiationConversationsComplete += new EventHandler<eNegEntityResultArgs<NegConversation>>(mDataMatchingModel_GetNegotiationConversationsComplete);
            mDataMatchingModel.GetNegotiationsDetailsByIDsComplete += new EventHandler<eNegEntityResultArgs<Negotiation>>(mDataMatchingModel_GetNegotiationsDetailsByIDsComplete);
            mDataMatchingModel.GetPreferenceSetNegotiationsComplete += new EventHandler<eNegEntityResultArgs<PreferenceSetNeg>>(mDataMatchingModel_GetPreferenceSetNegotiationsComplete);
            mDataMatchingModel.UpdateDataMatchingStatusInAddonCompleted += new Action<InvokeOperation<bool>>(mDataMatchingModel_UpdateDataMatchingStatusInAddonCompleted);
            mDataMatchingModel.RetrieveApplicationDMStatusCompleted += new Action<InvokeOperation<bool>>(mDataMatchingModel_RetrieveApplicationDMStatusCompleted);
            #endregion

            #region → Register needed messages .

            PrefAppMessanger.CancelChangesMessage.Register(this, OnCancelChangesMessage);
            PrefAppMessanger.EditNegConversationMessage.Register(this, OnEditNegConversationMessage);
            PrefAppMessanger.DataMatchMessage.Register(this, OnDataMatchingMessage);
            PrefAppMessanger.RefreshSource.Register(this, OnRefreshSource);

            #endregion
        }

        #endregion

        #region → Event Handlers .

        #region →  Loading Event Handlers .

        #region → Negotiations-Conversation-Message From Light Version (PrefApp.)

        /// <summary>
        /// Get preference set negotiations complete.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        void mDataMatchingModel_GetPreferenceSetNegotiationsComplete(object sender, eNegEntityResultArgs<PreferenceSetNeg> e)
        {
            if (!e.HasError)
            {
                PreferenceSetNegotiations = e.Results.OrderBy(s => s.DeletedOn).ToList();

                this.RaisePropertyChanged("PreferenceSetNegotiations");

                if (PreferenceSetNegotiations.Count() == 0)
                {
                    PrefAppMessanger.FlippMessage.Send(PrefAppViewTypes.ExitLoadingView);
                }

                #region → Get Details From eNeg  .

                List<Guid> NegotiationIDs = new List<Guid>();
                foreach (var Neg in PreferenceSetNegotiations)
                    NegotiationIDs.Add(Neg.NegID);

                LoadDetailsForNegotiations(NegotiationIDs.ToArray());

                #endregion

                LoadingLock--;
            }
            else
            {
                //Notify User if there is any error
                PrefAppMessanger.RaiseErrorMessage.Send(e.Error);
            }

        }

        /// <summary>
        /// Get negotiation conversations complete.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        void mDataMatchingModel_GetNegotiationConversationsComplete(object sender, eNegEntityResultArgs<NegConversation> e)
        {
            if (!e.HasError)
            {
                NegotiationConversations = e.Results.OrderBy(s => s.DeletedOn).ToList();

                this.RaisePropertyChanged("NegotiationConversations");

                LoadingLock--;
            }
            else
            {
                //Notify User if there is any error
                PrefAppMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Get conversation messages complete.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        void mDataMatchingModel_GetConversationMessagesComplete(object sender, eNegEntityResultArgs<ConversationMessage> e)
        {
            if (!e.HasError)
            {
                ConversationMessages = e.Results.OrderBy(s => s.DeletedOn).ToList();

                this.RaisePropertyChanged("ConversationMessages");

                LoadingLock--;
            }
            else
            {
                //Notify User if there is any error
                PrefAppMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }
        #endregion

        #region → From eNeg by Rest Protocol

        /// <summary>
        /// Get available negotiations to analysis complete.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        void mDataMatchingModel_GetAvailableNegotiationsToAnalysisComplete(object sender, eNegEntityResultArgs<Negotiation> e)
        {
            if (!e.HasError)
            {
                AvailableNegotiations = e.Results.OrderBy(s => s.NegotiationName).ToList();
                ActiveOngingNegotiations = e.Results.OrderBy(s => s.NegotiationName).ToList();

                #region → Remove any Neg. Assigned to Pref. Set.

                PreferenceSetNegotiations = this.mDataMatchingModel.Context.PreferenceSetNegs.ToList();

                foreach (var Neg in this.PreferenceSetNegotiations)
                {
                    if (AvailableNegotiations.Count(s => s.NegotiationID == Neg.NegID) > 0)
                    {
                        AvailableNegotiations.Remove(AvailableNegotiations.First(s => s.NegotiationID == Neg.NegID));
                    }
                }
                #endregion

                this.RaisePropertyChanged("AvailableNegotiations");

                //Send message with Load Available Negotiations complete to indicate that rebinding complete 
                PrefAppMessanger.LoadCompleted.Send(PrefAppMessanger.OperationType.AvailableNegotiationsCompleted.ToString());
            }
            else
            {
                //Notify User if there is any error
                PrefAppMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Get negotiations details by Ids complete.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        void mDataMatchingModel_GetNegotiationsDetailsByIDsComplete(object sender, eNegEntityResultArgs<Negotiation> e)
        {
            if (!e.HasError)
            {
                foreach (var item in e.Results.OrderBy(s => s.NegotiationName))
                {
                    NegotiationDetails.Add(item);
                }

                this.RaisePropertyChanged("NegotiationDetails");

                NumberOfExpectedCallBacksCounter += 1;
            }
            else
            {
                //Notify User if there is any error
                PrefAppMessanger.RaiseErrorMessage.Send(e.Error);
            }

            this.IseNegServicesBusy = NumberOfExpectedCallBacksCounter != NumberOfExpectedCallBacks;

            if (NumberOfExpectedCallBacksCounter == NumberOfExpectedCallBacks)
            {
                //SynchronizeData();
            }
        }

        /// <summary>
        /// Get conversations details by Ids complete.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        void mDataMatchingModel_GetConversationsDetailsByIDsComplete(object sender, eNegEntityResultArgs<Conversation> e)
        {
            if (!e.HasError)
            {
                foreach (var item in e.Results.OrderBy(s => s.ConversationName))
                {
                    ConversationDetails.Add(item);
                }

                this.RaisePropertyChanged("ConversationDetails");

                NumberOfExpectedCallBacksCounter += 1;
            }
            else
            {
                //Notify User if there is any error
                PrefAppMessanger.RaiseErrorMessage.Send(e.Error);
            }

            this.IseNegServicesBusy = NumberOfExpectedCallBacksCounter != NumberOfExpectedCallBacks;

            if (NumberOfExpectedCallBacksCounter == NumberOfExpectedCallBacks)
            {
                //SynchronizeData();
            }
        }

        /// <summary>
        /// get messages details by Ids complete.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        void mDataMatchingModel_GetMessagesDetailsByIDsComplete(object sender, eNegEntityResultArgs<Message> e)
        {
            if (!e.HasError)
            {
                foreach (var item in e.Results.OrderBy(s => s.MessageDate))
                {
                    MessageDetails.Add(item);
                }

                this.RaisePropertyChanged("MessageDetails");

                NumberOfExpectedCallBacksCounter += 1;
            }
            else
            {
                //Notify User if there is any error
                PrefAppMessanger.RaiseErrorMessage.Send(e.Error);
            }

            this.IseNegServicesBusy = NumberOfExpectedCallBacksCounter != NumberOfExpectedCallBacks;

            if (NumberOfExpectedCallBacksCounter == NumberOfExpectedCallBacks)
            {
                //SynchronizeData();
            }
        }

        /// <summary>
        /// Ms the data matching model_ update data matching status in addon completed.
        /// </summary>
        /// <param name="obj">The obj.</param>
        private void mDataMatchingModel_UpdateDataMatchingStatusInAddonCompleted(InvokeOperation<bool> obj)
        {
            if (!obj.HasError)
            {
                IsChangingDMStatusInAddonPossible = true;
            }
            else
            {
                //Notify User if there is any error
                PrefAppMessanger.RaiseErrorMessage.Send(obj.Error);
            }
        }

        /// <summary>
        /// Ms the data matching model_ retrieve application DM status completed.
        /// </summary>
        /// <param name="obj">The obj.</param>
        private void mDataMatchingModel_RetrieveApplicationDMStatusCompleted(InvokeOperation<bool> obj)
        {
            if (obj == null || !obj.HasError)
            {
                IsDMInAddonActive = obj != null ? obj.Value : false;
            }
            else
            {
                //Notify User if there is any error
                PrefAppMessanger.RaiseErrorMessage.Send(obj.Error);
            }
        }

        #endregion

        /// <summary>
        /// Get message issues complete.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        void mDataMatchingModel_GetMessageIssuesComplete(object sender, eNegEntityResultArgs<MessageIssue> e)
        {
            if (!e.HasError)
            {
                MessageIssues = e.Results.OrderBy(s => s.DeletedOn).ToList();
                this.RaisePropertyChanged("MessageIssues");
            }
            else
            {
                //Notify User if there is any error
                PrefAppMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Get message option issues complete.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        void mDataMatchingModel_GetMessageOptionIssuesComplete(object sender, eNegEntityResultArgs<MessageOptionIssue> e)
        {
            if (!e.HasError)
            {
                MessageOptionIssues = e.Results.OrderBy(s => s.DeletedOn).ToList();
                this.RaisePropertyChanged("MessageOptionIssues");
            }
            else
            {
                //Notify User if there is any error
                PrefAppMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Get message later rated issues complete.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        void mDataMatchingModel_GetMessageLaterRatedIssuesComplete(object sender, eNegEntityResultArgs<MessageLaterRatedIssue> e)
        {
            if (!e.HasError)
            {
                MessageLaterRatedIssues = e.Results.OrderBy(s => s.DeletedOn).ToList();
                this.RaisePropertyChanged("MessageLaterRatedIssues");
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
        private void mDataMatchingModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("HasChanges") || e.PropertyName.Equals("IsBusy"))
            {
                RaiseCanExecuteChanged();

                if (!mDataMatchingModel.IsBusy)
                {
                    Deployment.Current.Dispatcher.BeginInvoke(() => this.IsBusy = mDataMatchingModel.IsBusy);
                }
                else
                {
                    this.IsBusy = mDataMatchingModel.IsBusy;
                }
            }
        }

        /// <summary>
        /// Handles the SaveChangesComplete event of the mDataMatchingModel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="citPOINT.eNeg.Common.SubmitOperationEventArgs"/> instance containing the event data.</param>
        private void mDataMatchingModel_SaveChangesComplete(object sender, SubmitOperationEventArgs e)
        {
            if (e != null && !e.HasError)
            {
                CheckIfNegotiationAssigned();
            }
            else
            {
                PrefAppMessanger.RaiseErrorMessage.Send(e.Error);
            }

            RaiseCanExecuteChanged();
        }

        #endregion Event Handlers

        #region → Commands       .

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
                            if (!mDataMatchingModel.IsBusy)
                            {

                                #region → Send Statisticals  to eNeg  .

                                if (AllowSendStatisticals)
                                {
                                    // Send Statisticals to eNeg in case if changes effect Messages Score .
                                    StatisticalPublisher.Send();
                                }
                                else
                                {
                                    //Reset conversations flag
                                    StatisticalPublisher.Reset();
                                }

                                #endregion

                                OnSubmitChangesMessage();
                            }
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            PrefAppMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , () => mDataMatchingModel.HasChanges);
                }
                return mSubmitChangesCommand;
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

                            if (!mDataMatchingModel.IsBusy)
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
                    , () => mDataMatchingModel.HasChanges);
                }
                return mCancelChangesCommand;
            }
        }

        /// <summary>
        /// Gets the add negotiation command.
        /// </summary>
        /// <value>The add negotiation command.</value>
        public RelayCommand AddNegotiationCommand
        {
            get
            {
                if (mAddNegotiationCommand == null)
                {
                    mAddNegotiationCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            if (!mDataMatchingModel.IsBusy)
                            {
                                GetAvailableNegotiationsToAnalysisAsync();
                            }
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            PrefAppMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , () => mDataMatchingModel.HasChanges);
                }
                return mAddNegotiationCommand;
            }
        }

        /// <summary>
        /// Gets the assign preference set command.
        /// </summary>
        /// <value>The assign preference set command.</value>
        public RelayCommand AssignPreferenceSetCommand
        {
            get
            {
                if (mAssignPreferenceSetCommand == null)
                {
                    mAssignPreferenceSetCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            if (!mDataMatchingModel.IsBusy)
                            {
                                PrefAppMessanger.FlippMessage.Send(PrefAppViewTypes.LoadingView);

                                isFirstTime = true;

                                IsSyncroized = false;

                                mDataMatchingModel.AddPreferenceSetNeg(true, this.SelectedPrefSet, MainPlatformInfo.Instance.CurrentNegotiation.NegotiationID, MainPlatformInfo.Instance.CurrentNegotiation.NegotiationName);

                                //to Update local variable.
                                this.PreferenceSetNegotiations = this.mDataMatchingModel.Context.PreferenceSetNegs.ToList();

                                LoadDetailsForNegotiations(new Guid[] { MainPlatformInfo.Instance.CurrentNegotiation.NegotiationID });
                            }
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            PrefAppMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , () => SelectedPrefSet != null &&
                        !mDataMatchingModel.IsBusy);
                }
                return mAssignPreferenceSetCommand;
            }
        }

        /// <summary>
        /// Gets the remove negotiation command.
        /// </summary>
        /// <value>The remove negotiation command.</value>
        public RelayCommand RemoveNegotiationCommand
        {
            get
            {
                if (mRemoveNegotiationCommand == null)
                {
                    mRemoveNegotiationCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            if (!mDataMatchingModel.IsBusy && this.CurrentNegotiation != null)
                            {
                                Action<MessageBoxResult> callBackResult = null;

                                #region "Confirmation Message"

                                // ask to confirm canceling this new issue first
                                DialogMessage dialogMessage = new DialogMessage(
                                    this,
                                    Resources.UnAssignNegotiationConfirm,
                                    result => callBackResult(result))
                                {
                                    Button = MessageBoxButton.OKCancel,
                                    Caption = Resources.UnassignedConfirmMessageBoxCaption
                                };

                                eNegMessanger.ConfirmMessage.Send(dialogMessage);

                                #endregion "Confirmation Message"

                                callBackResult = (result) =>
                                {
                                    if (result == MessageBoxResult.Cancel)
                                        return;

                                    PreferenceSet preferenceSet = this.CurrentNegotiation.PreferenceSet;

                                    //this.CurrentPreferenceSet.PreferenceSetName
                                    this.mDataMatchingModel.RemovePreferenceSetNeg(this.CurrentNegotiation);

                                    this.PreferenceSetNegotiations.Remove(this.CurrentNegotiation);

                                    //Remove From Details
                                    this.RemoveNegFromDetails(new Guid[] { this.CurrentNegotiation.NegID });

                                    this.RaisePropertyChanged("PreferenceSetNegotiations");

                                    //flag for recheck on current negotiation prefence set after save.
                                    isFirstTime = true;

                                    //to not naviaging to any other view in "CheckIfNegotiationAssigned"
                                    IsLastActionCommandUnAssigned = true;

                                    this.CurrentPreferenceSet = null;

                                    this.CurrentNegotiation = null;

                                    this.OnSubmitChangesMessage();

                                    //PrefAppMessanger.FlippMessage.Send(PrefAppViewTypes.AddPrefSetView);
                                };
                            }
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            PrefAppMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , () => !mDataMatchingModel.IsBusy);
                }
                return mRemoveNegotiationCommand;
            }
        }

        /// <summary>
        /// Gets the add undefined issue.
        /// </summary>
        /// <value>The add undefined issue.</value>
        public RelayCommand AddUndefinedIssue
        {
            get
            {
                if (mAddUndefinedIssue == null)
                {
                    mAddUndefinedIssue = new RelayCommand(() =>
                    {
                        try
                        {
                            if (!mDataMatchingModel.IsBusy)
                            {
                                PrefAppConfigurations.MailPreferenceSetNegID = this.CurrentNegotiation.PreferenceSetNegID;

                                PrefAppMessanger.NewPopUp.Send("New Issue Name", PrefAppMessanger.PopUpType.NewIssue);
                            }
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            PrefAppMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , () => mDataMatchingModel.IsBusy == false);
                }
                return mAddUndefinedIssue;
            }
        }

        /// <summary>
        /// Gets the switch data matching status in addon.
        /// </summary>
        /// <value>The switch data matching status in addon.</value>
        public RelayCommand SwitchDataMatchingStatusInAddon
        {
            get
            {
                if (mSwitchDataMatchingStatusInAddon == null)
                {
                    mSwitchDataMatchingStatusInAddon = new RelayCommand(() =>
                    {
                        try
                        {
                            if (!mDataMatchingModel.IsBusy)
                            {
                                IsChangingDMStatusInAddonPossible = false;
                                IsDMInAddonActive = !IsDMInAddonActive;
                                mDataMatchingModel.UpdateDataMatchingStatusInAddonAsync
                                    (PrefAppConfigurations.AppName, PrefAppConfigurations.CurrentLoginUser.UserID, IsDMInAddonActive);
                            }
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            PrefAppMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , () => IsChangingDMStatusInAddonPossible && mDataMatchingModel.IsBusy == false);
                }
                return mSwitchDataMatchingStatusInAddon;
            }
        }

        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Saves the file.
        /// </summary>
        /// <param name="writableBitmap">The writable bitmap.</param>
        private static void SaveFile(WriteableBitmap writableBitmap)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = "png";
            saveFileDialog.Filter = "PNG Files (*.png)|*.png|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 1;

            if (saveFileDialog.ShowDialog() == true)
            {
                using (Stream stream = saveFileDialog.OpenFile())
                {
                    byte[] buffer = PDFGenerator.GetBuffer(writableBitmap);
                    stream.Write(buffer, 0, buffer.Length);
                    stream.Close();
                }
            }
        }

        /// <summary>
        /// Filters the messages.
        /// </summary>
        private void FilterMessages()
        {
            if (mCurrentConversation != null)
            {

                //Setting The Current Conversation.
                this.CurrentConversationMessagesSource
                    = new ObservableCollection<ConversationMessage>(
                             mCurrentConversation.ConversationMessages
                              .Where(s => (this.MessageFilterType == MessageFilter.All) || (s.IsSent == (this.MessageFilterType == MessageFilter.SendDataOnly)))
                              .OrderByDescending(s => s.DeletedOn)); //Order by Message Date
            }

            this.RaisePropertyChanged("HasMessages");

            this.RaisePropertyChanged("HasNoMessages");
        }

        /// <summary>
        /// Loads all.
        /// </summary>
        private void LoadAll()
        {
            GetAvailableNegotiationsToAnalysisAsync();

            mDataMatchingModel.GetPreferenceSetNegotiationsAsync();
            mDataMatchingModel.GetNegotiationConversationsAsync();
            mDataMatchingModel.GetConversationMessagesAsync();

            /************************************************************/

            mDataMatchingModel.GetMessageIssuesAsync();
            mDataMatchingModel.GetMessageOptionIssuesAsync();
            mDataMatchingModel.GetMessageLaterRatedIssuesAsync();

            #region → Retrieve status of this App According to making DataMatching in Addon
            try
            {
                mDataMatchingModel.RetrieveApplicationDMStatusAsync(PrefAppConfigurations.AppName, PrefAppConfigurations.CurrentLoginUser.UserID);
            }
            catch (Exception ex)
            {
                PrefAppMessanger.RaiseErrorMessage.Send(ex);
            }
            #endregion
        }

        /// <summary>
        /// Removes the neg from details.
        /// </summary>
        /// <param name="NegotiationIDs">The negotiation I ds.</param>
        private void RemoveNegFromDetails(Guid[] NegotiationIDs)
        {
            ConversationDetails.Clear();

            MessageDetails.Clear();

            if (NegotiationDetails.Count() > 0)
            {
                foreach (var negotiationid in NegotiationIDs)
                {
                    while (this.NegotiationDetails.Count(s => s.NegotiationID == negotiationid) > 0)
                    {
                        //Select First Negotaion
                        Negotiation neg = NegotiationDetails.Where(s => s.NegotiationID == negotiationid).FirstOrDefault();

                        //Remove  Negotiations.
                        NegotiationDetails.Remove(neg);

                    }
                }
            }
        }

        /// <summary>
        /// Executed when SubmitChangesMessage is received
        /// </summary>
        private void OnSubmitChangesMessage()
        {
            mDataMatchingModel.SaveChangesAsync();
        }

        /// <summary>
        /// Executed when CancelChangesMessage is received
        /// </summary>
        /// <param name="ignore">ignore</param>
        private void OnCancelChangesMessage(Boolean ignore)
        {
            this.MessageFilterType = MessageFilter.All;

            this.mDataMatchingModel.RejectChanges();
        }

        /// <summary>
        /// Called when [data matching message].
        /// in case if any changes in the inputs of data matching.
        /// </summary>
        /// <param name="dataMatchingMsg">The data matching MSG.</param>
        private void OnDataMatchingMessage(DataMatchingMessage dataMatchingMsg)
        {
            if (dataMatchingMsg.CurrentMessage==null)
            {
                dataMatchingMsg.CurrentMessage = this.CurrentMessage;
            }

            //Retrieving Data matching that already saved for this Message
            MessageIssue msgIssue = dataMatchingMsg.CurrentMessage.MessageIssues.FirstOrDefault(s => s.IssueID == dataMatchingMsg.CurrentIssue.IssueID);

            //in case we have not do any data matching before.
            if (msgIssue == null)
            {
                msgIssue = mDataMatchingModel.AddMessageIssue(true, dataMatchingMsg.CurrentMessage, dataMatchingMsg.CurrentIssue);
            }

            //in case of the Issue type is numeric or Not rated
            if (dataMatchingMsg.CurrentIssue.IssueTypeID == PrefAppConstant.IssueTypes.Numeric || dataMatchingMsg.CurrentIssue.IssueTypeID == PrefAppConstant.IssueTypes.NotRated)
            {
                msgIssue.Value = dataMatchingMsg.Value;
            }



            //in Casae of Options
            else if (dataMatchingMsg.CurrentIssue.IssueTypeID == PrefAppConstant.IssueTypes.Options)
            {
                //Selecting The Option Issue by Option Value e.g. "Blue"
                OptionIssue optionIssue = dataMatchingMsg.CurrentIssue.OptionIssues.FirstOrDefault(s => s.OptionIssueValue.ToLower() == dataMatchingMsg.Value.ToLower());

                //Selecting Message Option For that issue by Option ID. 
                MessageOptionIssue msgOptionIssue = msgIssue.MessageOptionIssues.FirstOrDefault(s => s.OptionIssueID == optionIssue.OptionIssueID);

                //in case of adding the Current Option to the Message.
                if (dataMatchingMsg.IsChecked)
                {
                    if (msgOptionIssue == null)
                    {
                        msgOptionIssue = mDataMatchingModel.AddMessageOptionIssue(true, msgIssue, optionIssue);
                    }
                }
                else
                {
                    //Deleting that option.
                    if (msgOptionIssue != null)
                    {
                        mDataMatchingModel.RemoveMessageOptionIssue(msgOptionIssue);
                    }
                }
            }
            else if (dataMatchingMsg.CurrentIssue.IssueTypeID == PrefAppConstant.IssueTypes.LaterRated)
            {
                //Selecting The Later rated Issue by LaterRated Value e.g. "Wood"
                LaterRatedIssue laterRatedIssue = dataMatchingMsg.CurrentIssue.LaterRatedIssues.FirstOrDefault(s => s.LaterRatedIssueValue.ToLower() == dataMatchingMsg.Value.ToLower());

                //Selecting Message Later rated  For that issue by Later rated  ID. 
                MessageLaterRatedIssue msgLaterRatedIssue = msgIssue.MessageLaterRatedIssues.FirstOrDefault(s => s.LaterRatedIssueID == laterRatedIssue.LaterRatedIssueID);

                //in case of adding the Current Later rated  to the Message.
                if (dataMatchingMsg.IsChecked)
                {
                    if (msgLaterRatedIssue == null)
                    {
                        msgLaterRatedIssue = mDataMatchingModel.AddMessageLaterRatedIssue(true, msgIssue, laterRatedIssue);
                    }
                }
                else
                {
                    //Deleting that Later rated Option.
                    if (msgLaterRatedIssue != null)
                    {
                        mDataMatchingModel.RemoveMessageLaterRatedIssue(msgLaterRatedIssue);
                    }
                }
            }



            CalculationEngine.Calculate(dataMatchingMsg.CurrentMessage);


            // mDataMatchingModel.SaveChangesAsync();
        }

        /// <summary>
        /// Called when [edit neg conversation message].
        /// </summary>
        /// <param name="entity">The entity.</param>
        private void OnEditNegConversationMessage(Entity entity)
        {
            if (entity is NegConversation)
            {
                NegConversation negotiationConversation = entity as NegConversation;
                this.CurrentNegotiation = negotiationConversation.PreferenceSetNeg;
                this.CurrentPreferenceSet = this.CurrentNegotiation.PreferenceSet;
                this.CurrentConversation = negotiationConversation;
            }
            else if (entity is PreferenceSetNeg)
            {
                PreferenceSetNeg preferenceSetNeg = entity as PreferenceSetNeg;
                this.CurrentNegotiation = preferenceSetNeg;
                this.CurrentPreferenceSet = this.CurrentNegotiation.PreferenceSet;
            }
            PrefAppConfigurations.MailNegotiationName = this.CurrentNegotiation.Name;
        }

        /// <summary>
        /// Updates the words format source.
        /// </summary>
        private void UpdateWordsFormatSource()
        {
            List<FormatedWord> tmpFormat = new List<FormatedWord>();

            if (this.CurrentPreferenceSet != null)
            {
                foreach (var issue in this.CurrentPreferenceSet.Issues)
                {
                    //In case of Issue So Black  Bold.
                    tmpFormat.Add(new FormatedWord(issue.IssueName, Colors.Black, FontWeights.Bold));

                    foreach (var option in issue.OptionIssues)
                    {
                        //In case of Issue So Green  Bold.
                        tmpFormat.Add(new FormatedWord(option.OptionIssueValue, Colors.Green, FontWeights.Bold));
                    }


                    foreach (var laterRated in issue.LaterRatedIssues)
                    {
                        //In case of Issue So Magenta  Bold.
                        tmpFormat.Add(new FormatedWord(laterRated.LaterRatedIssueValue, Color.FromArgb(255, 140, 140, 20), FontWeights.Bold));
                    }
                }
            }

            this.WordsFormatSource = tmpFormat;
        }

        /// <summary>
        /// Called when [refresh source].
        /// </summary>
        /// <param name="sourceName">Name of the source.</param>
        private void OnRefreshSource(string sourceName)
        {
            if (sourceName == PrefAppMessanger.RefreshSource.PreferenceSetDeleted)
            {
                //In case if user delete the prefence set which assigned to current negotiation
                if (this.CurrentPreferenceSet != null && this.CurrentPreferenceSet.EntityState == EntityState.Deleted)
                {
                    //flag for recheck on current negotiation prefence set after save.
                    isFirstTime = true;

                    //to not naviaging to any other view in "CheckIfNegotiationAssigned"
                    IsLastActionCommandUnAssigned = true;

                    this.CurrentPreferenceSet = null;

                    this.CurrentNegotiation = null;

                    CheckIfNegotiationAssigned();

                    IsLastActionCommandUnAssigned = false;
                }
            }
            //In case if user make changes in current prefrence set and effect it
            else if (sourceName == PrefAppMessanger.RefreshSource.PreferenceSetChanged)
            {
                //flag for recheck on current negotiation prefence set after save.
                isFirstTime = true;

                //to not naviaging to any other view in "CheckIfNegotiationAssigned"
                IsLastActionCommandUnAssigned = true;

                this.CurrentPreferenceSet = null;

                this.CurrentNegotiation = null;

                PrefAppConfigurations.DefaultView = PrefAppViewTypes.AppSettingsView;

                CheckIfNegotiationAssigned();

                IsLastActionCommandUnAssigned = false;
            }

            else
            {
                //To Update Formating
                UpdateWordsFormatSource();

                //To Update Formating
                this.UpdateWordsFormatSource();

                ConversationMessage.WordsFormatStaticSource = this.WordsFormatSource;

                //Way to Update Sources
                //Step 1 save the value of the Current Message.
                var x = this.CurrentMessage;

                //Setp 2 Set Current Message to be null.
                this.CurrentMessage = null;

                //Setp 2 Set Current Message by the Saved Message.
                this.CurrentMessage = x;


                if (this.CurrentMessage != null)
                {
                    this.CurrentMessage.RefereshWordsFormatSource();
                }
            }
        }

        /// <summary>
        /// Checks if negotiation assigned.
        /// </summary>
        private void CheckIfNegotiationAssigned()
        {
            if (PrefAppConfigurations.NegotiationIDParameter.HasValue && isFirstTime)
            {
                isFirstTime = false;

                PreferenceSetNegotiations = this.mDataMatchingModel.Context.PreferenceSetNegs.ToList();
                NegotiationConversations = this.mDataMatchingModel.Context.NegConversations.ToList();
                ConversationMessages = this.mDataMatchingModel.Context.ConversationMessages.ToList();

                PreferenceSetNeg neg = null;

                neg = PreferenceSetNegotiations.Where(s => s.NegID == PrefAppConfigurations.NegotiationIDParameter).FirstOrDefault();

                this.CurrentConversation = null;

                #region → If Wanted Negotiation exist to certain PreferenceSet   .

                if (neg != null)
                {
                    try
                    {
                        this.CurrentPreferenceSet = neg.PreferenceSet;

                        this.CurrentNegotiation = neg;

                        if (PrefAppConfigurations.ConversationIDParameter != null)
                        {
                            CurrentConversation = NegotiationConversations.Where(s => s.ConversationID == PrefAppConfigurations.ConversationIDParameter).FirstOrDefault();

                            if (PrefAppConfigurations.MessageIDParameter != null)
                            {
                                CurrentMessage = ConversationMessages.Where(s => s.MessageID == PrefAppConfigurations.MessageIDParameter).FirstOrDefault();

                                //To Set the Current Message in View (ComboBox)
                                PrefAppMessanger.EditConversationMessage.Send(this.CurrentMessage);
                            }
                        }

                        ReportVM.RefereshSources();

                        this.SelectPerfectView(PrefAppViewTypes.ReportView);

                    }
                    catch (Exception ex)
                    {
                        PrefAppMessanger.RaiseErrorMessage.Send(ex);
                    }
                }

                #endregion

                #region → If Wanted Negotiation doesn't exist in any PreferenceSet
                else
                {
                    if (!IsLastActionCommandUnAssigned)
                    {
                        PrefAppMessanger.ChangeScreenMessage.Send(PrefAppViewTypes.ReportView);

                        //In case they need setting apps.
                        this.SelectPerfectView(PrefAppViewTypes.AssignPrefSetToNegotiationView);
                    }
                    else
                    {
                        IsLastActionCommandUnAssigned = false;
                    }
                }
                #endregion

                PrefAppMessanger.FlippMessage.Send(PrefAppViewTypes.ExitLoadingView);
            }
        }

        /// <summary>
        /// Selects the perfect view.
        /// </summary>
        /// <param name="prefDefaultView">The pref default view.</param>
        private void SelectPerfectView(string prefDefaultView)
        {
            if (PrefAppConfigurations.DefaultView == PrefAppViewTypes.AppSettingsView)
            {
                if (!IsLastActionCommandUnAssigned)
                {
                    PrefAppMessanger.ChangeScreenMessage.Send(PrefAppViewTypes.AppSettingsView);
                }

                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    if (this.CurrentPreferenceSet != null)
                    {
                        PrefAppMessanger.EditPreferenceSetMessage
                                        .Send(this.CurrentPreferenceSet);

                        PrefAppMessanger.FlippMessage
                                        .Send(PrefAppViewTypes.IssuesView);
                    }

                });

                PrefAppConfigurations.DefaultView = PrefAppViewTypes.ReportView;
            }
            else
            {
                PrefAppMessanger.ChangeScreenMessage
                                .Send(prefDefaultView);
            }
        }

        #endregion

        #region → Public         .

        /// <summary>
        /// Gets the available negotiations to analysis async.
        /// </summary>
        public void GetAvailableNegotiationsToAnalysisAsync()
        {
            mDataMatchingModel.GetAvailableNegotiationsToAnalysisAsync();
        }

        /// <summary>
        /// Loads the details for negotiations.
        /// </summary>
        /// <param name="NegotiationIDs">The negotiation Ids.</param>
        public void LoadDetailsForNegotiations(Guid[] NegotiationIDs)
        {
            RemoveNegFromDetails(NegotiationIDs);

            NumberOfExpectedCallBacksCounter = 0;

            //Some calculations to know number of call backs.
            NumberOfExpectedCallBacks = (int)Math.Ceiling(((double)NegotiationIDs.Count() / (double)mDataMatchingModel.MaxQueryPerCall));

            NumberOfExpectedCallBacks *= 3;

            this.IseNegServicesBusy = NumberOfExpectedCallBacksCounter != NumberOfExpectedCallBacks;

            try
            {
                if (NegotiationIDs.Count() > 0)
                {
                    //Get Negotiations
                    mDataMatchingModel.GetNegotiationsDetailsByIDsAsync(NegotiationIDs.ToArray());


                    mDataMatchingModel.GetConversationsDetailsByIDsAsync(new Guid[] { PrefAppConfigurations.NegotiationIDParameter.Value });/////////NegotiationIDs.ToArray());

                    //Get Messages
                    //HAck:Review
                    mDataMatchingModel.GetMessagesDetailsByIDsAsync(new Guid[] { PrefAppConfigurations.NegotiationIDParameter.Value });//////  NegotiationIDs.ToArray());

                    ////Get Conversations
                    //mDataMatchingModel.GetConversationsDetailsByIDsAsync(NegotiationIDs.ToArray());

                    ////Get Messages
                    ////HAck:Review
                    //mDataMatchingModel.GetMessagesDetailsByIDsAsync(NegotiationIDs.ToArray());
                }
            }
            catch (Exception ex)
            {
                PrefAppMessanger.RaiseErrorMessage.Send(ex);
            }
        }

        /// <summary>
        /// Synchronizes the data.
        /// to let PrefApp be Like eNeg Data.
        /// </summary>
        public void SynchronizeData()
        {
            if (LoadingLock <= 0 &&
                !this.IsBusy &&
                !SynchronizeIsLocked &&
                ActiveOngingNegotiations != null)
            {
                //Flag idicating that the current process 
                //is in sycnco so we can't perform another request.
                this.SynchronizeIsLocked = true;

                bool tmpIsSyncroized = IsSyncroized;
                IsSyncroized = true;

                PreferenceSetNegotiations = this.mDataMatchingModel.Context.PreferenceSetNegs.ToList();
                NegotiationConversations = this.mDataMatchingModel.Context.NegConversations.ToList();
                ConversationMessages = this.mDataMatchingModel.Context.ConversationMessages.ToList();

                List<Guid> Tmp = new List<Guid>();

                #region → Pref Set Neg     .

                #region → Check Deleted Items       .

                foreach (var Neg in PreferenceSetNegotiations)
                {
                    //In case the Negotiations Deleted From the Source
                    if (NegotiationDetails.Count(s => s.NegotiationID == Neg.NegID) == 0)

                        Tmp.Add(Neg.NegID);

                }

                while (Tmp.Count > 0)
                {
                    mDataMatchingModel.RemovePreferenceSetNeg(PreferenceSetNegotiations.First(s => s.NegID == Tmp[0]));
                    PreferenceSetNegotiations.Remove(PreferenceSetNegotiations.First(s => s.NegID == Tmp[0]));
                    Tmp.Remove(Tmp[0]);
                }

                #endregion

                #region → Update Negotiation Name   .

                foreach (var Neg in NegotiationDetails)
                {

                    if (PreferenceSetNegotiations.Count(s => s.NegID == Neg.NegotiationID) > 0)
                    {
                        PreferenceSetNegotiations.First(s => s.NegID == Neg.NegotiationID).Name = Neg.NegotiationName;
                        PreferenceSetNegotiations.First(s => s.NegID == Neg.NegotiationID).StatusID = Neg.StatusID;

                        #region Check Closed and Deactive negotiations
                        if (Neg.IsClosed ||
                            (ActiveOngingNegotiations.Where(ss => ss.NegotiationID == Neg.NegotiationID).FirstOrDefault() == null) /*Mean it is Deactive to that App*/)
                        {
                            PreferenceSetNegotiations.First(s => s.NegID == Neg.NegotiationID).IsClosed = true;
                        }
                        #endregion
                    }
                }


                PreferenceSetNegotiations = this.mDataMatchingModel.Context.PreferenceSetNegs.ToList();
                this.RaisePropertyChanged("PreferenceSetNegotiations");
                #endregion


                #endregion

                /*********************************************/

                this.CurrentNegotiation = PreferenceSetNegotiations
                                            .Where(s => s.NegID == PrefAppConfigurations.NegotiationIDParameter.Value)
                                            .FirstOrDefault();

                #region → Conversations    .

                if (CurrentNegotiation != null)
                {
                    #region → Check Deleted Items        .

                    Tmp.Clear();

                    foreach (var Conv in NegotiationConversations.Where(s => s.NegConversationID != Guid.Empty && s.PreferenceSetNeg.NegID == PrefAppConfigurations.NegotiationIDParameter.Value))
                    {
                        //In case the Conversation Deleted From the Source
                        if (ConversationDetails.Count(s => s.ConversationID == Conv.ConversationID) == 0)

                            Tmp.Add(Conv.ConversationID.Value);

                    }

                    while (Tmp.Count > 0)
                    {
                        mDataMatchingModel.RemoveNegConversation(NegotiationConversations.First(s => s.ConversationID == Tmp[0]));
                        NegotiationConversations.Remove(NegotiationConversations.First(s => s.ConversationID == Tmp[0]));
                        Tmp.Remove(Tmp[0]);
                    }

                    #endregion

                    #region → Update Conversation Name   .

                    foreach (var Conv in ConversationDetails)
                    {
                        NegConversation NegConv = null;

                        if (NegotiationConversations.Count(s => s.ConversationID == Conv.ConversationID) > 0)
                        {
                            NegConv = NegotiationConversations.First(s => s.ConversationID == Conv.ConversationID);
                        }
                        else
                        {
                            PreferenceSetNeg x = PreferenceSetNegotiations.FirstOrDefault(s => s.NegID == Conv.NegotiationID);
                            NegConv = mDataMatchingModel.AddNegConversation(true, x, Conv.ConversationID, Conv.ConversationName);
                        }

                        NegConv.Name = Conv.ConversationName;
                    }

                    NegotiationConversations = this.mDataMatchingModel.Context.NegConversations.ToList();
                    this.RaisePropertyChanged("ConversationDetails");

                    #endregion
                }

                #endregion

                /*********************************************/

                #region → Messages         .

                if (CurrentNegotiation != null)
                {
                    #region → Check Deleted Items         .

                    Tmp.Clear();

                    foreach (var Msg in ConversationMessages.Where(s => s.NegConversation.PreferenceSetNeg.NegID == PrefAppConfigurations.NegotiationIDParameter.Value))
                    {
                        //In case the Messages Deleted From the Source

                        if (MessageDetails.Count(s => s.MessageID == Msg.MessageID) == 0)

                            Tmp.Add(Msg.MessageID.Value);

                    }

                    while (Tmp.Count > 0)
                    {
                        mDataMatchingModel.RemoveConversationMessage(ConversationMessages.First(s => s.MessageID == Tmp[0]));
                        ConversationMessages.Remove(ConversationMessages.First(s => s.MessageID == Tmp[0]));
                        Tmp.Remove(Tmp[0]);
                    }

                    #endregion

                    #region → Update Messages Name        .

                    foreach (var Msg in MessageDetails)
                    {

                        ConversationMessage convMsg = null;

                        if (ConversationMessages.Count(s => s.MessageID == Msg.MessageID) > 0)
                        {
                            convMsg = ConversationMessages.First(s => s.MessageID == Msg.MessageID);
                        }
                        else
                        {
                            NegConversation x = NegotiationConversations.FirstOrDefault(s => s.ConversationID == Msg.ConversationID);
                            convMsg = mDataMatchingModel.AddConversationMessage(true, x, Msg.MessageID);
                        }

                        convMsg.IsSent = Msg.IsSent;
                        convMsg.MessageContent = Msg.MessageContent;
                        convMsg.MessageReceiver = Msg.MessageReceiver;
                        convMsg.MessageSender = Msg.MessageSender;
                        convMsg.MessageSubject = Msg.MessageSubject;
                        convMsg.MessageDate = Msg.MessageDate.Value;
                        convMsg.DeletedOn = Msg.MessageDate;
                        convMsg.ChannelName = Msg.ChannelName;
                    }

                    #endregion
                }

                ConversationMessages = this.mDataMatchingModel.Context.ConversationMessages.ToList();
                this.RaisePropertyChanged("ConversationMessages");

                #endregion

                /*********************************************/

                #region → Re Score All Msg .

                if (!tmpIsSyncroized)
                {
                    CalculationEngine<ClientEngineProvider> calculationEngine = new CalculationEngine<ClientEngineProvider>(this.mDataMatchingModel.Context);

                    foreach (var prefNego in this.PreferenceSetNegotiations)
                    {
                        calculationEngine.Calculate(prefNego);
                    }

                }

                #endregion

                StatisticalPublisher.Reset();

                if (this.HasChanges)
                {
                    OnSubmitChangesMessage();
                }
                else
                {
                    CheckIfNegotiationAssigned();
                }

                //Release lock.
                SynchronizeIsLocked = false;
            }
        }

        /// <summary>
        /// Rejects the changes.
        /// </summary>
        public void RejectChanges()
        {
            this.mDataMatchingModel.RejectChanges();
            RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Raises the can execute changed.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            SubmitChangesCommand.RaiseCanExecuteChanged();
            CancelChangesCommand.RaiseCanExecuteChanged();
            AddNegotiationCommand.RaiseCanExecuteChanged();
            AssignPreferenceSetCommand.RaiseCanExecuteChanged();
            RemoveNegotiationCommand.RaiseCanExecuteChanged();
            AddUndefinedIssue.RaiseCanExecuteChanged();
            SwitchDataMatchingStatusInAddon.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Applies the changes.
        /// </summary>
        public void ApplyChanges()
        {
            RunQueueForApplyChanges = true;

            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {

                if (!this.IsBusy && ActiveOngingNegotiations != null && IsSyncroized && !SynchronizeIsLocked)
                {
                    RunQueueForApplyChanges = false;

                    PrefAppConfigurations.IsNewIssuePending = false;

                    if (PrefAppConfigurations.PendingItems != null)
                    {
                        PrefAppConfigurations.PendingItems.Clear();
                    }

                    PrefAppConfigurations.IsNegotiationAttachedPreferenceSet = false;

                    this.RejectChanges();

                    MessageDetails.Clear();

                    ConversationDetails.Clear();

                    isFirstTime = true;

                    IsSyncroized = false;

                    IsLastActionCommandUnAssigned = false;

                    //LoadingLock = 0;

                    LoadDetailsForNegotiations(new Guid[] { PrefAppConfigurations.NegotiationIDParameter.Value });
                }

            });

        }

        #endregion

        #endregion
    }
}

