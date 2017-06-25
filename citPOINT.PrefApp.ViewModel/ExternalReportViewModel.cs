
#region → Usings   .
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using citPOINT.eNeg.Common;
using citPOINT.PrefApp.Common;
using citPOINT.PrefApp.Data;
using citPOINT.PrefApp.Data.Web;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows;
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
    #region  Using MEF to export ExternalReportViewModel
    /// <summary>
    /// Class to Manage Data Matching
    /// </summary>
    [Export(PrefAppViewModelTypes.ExternalReportViewModel)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    #endregion
    public class ExternalReportViewModel : ViewModelBase
    {
        #region → Fields         .

        private IExternalReportModel mExternalReportModel;
        private PreferenceSetNeg mCurrentNegotiation;
        private PreferenceSet mCurrentPreferenceSet;

        private bool mIsBusy = false;
        private bool mHasReport = false;
        private bool mShowNoInfoAvailable = false;

        private string mSentMessagePercentage = "N/A";
        private string mReceivedMessagePercentage = "N/A";
        private string mFeedBackMessage = string.Empty;

        private ConversationMessage mLastReceivedMessage;
        private ConversationMessage mLastSentMessage;

        private RelayCommand mNavigateToSeeDetailsCommand;
        private RelayCommand mNavigateToAppDetailsCommand;

        #region → Flags Used to indicate thatcallback has been done successfully to check on them in unit test  .
        public bool IsGetIssuesDone = false;
        public bool IsGetLaterRatedIssuesDone = false;
        public bool IsGetOptionIssuesDone = false;
        public bool IsGetNumericIssuesDone = false;
        public bool IsGetMessageIssueDone = false;
        public bool IsGetMessageOptionIssueDone = false;
        public bool IsGetMessageLaterRatedIssueDone = false;

        #endregion

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets the filtered issue source.
        /// </summary>
        /// <value>The filtered issue source.</value>
        public ObservableCollection<FilteredIssue> FilteredIssueSource { get; set; }

        /// <summary>
        /// Gets or sets the sent message percentage.
        /// </summary>
        /// <value>The sent message percentage.</value>
        public string SentMessagePercentage
        {
            get { return mSentMessagePercentage; }
            set
            {
                mSentMessagePercentage = value;

                if (value != null)
                {
                    mSentMessagePercentage += "%";
                }
                else
                {
                    mSentMessagePercentage += "N/A";
                }

                this.RaisePropertyChanged("SentMessagePercentage");
            }
        }
        /// <summary>
        /// Gets or sets the received message percentage.
        /// </summary>
        /// <value>The received message percentage.</value>
        public string ReceivedMessagePercentage
        {
            get { return mReceivedMessagePercentage; }
            set
            {
                mReceivedMessagePercentage = value;
                if (value != null)
                {
                    mReceivedMessagePercentage += "%";
                }
                else
                {
                    mReceivedMessagePercentage += "N/A";
                }
                this.RaisePropertyChanged("ReceivedMessagePercentage");
            }
        }

        /// <summary>
        /// Gets or sets the last sent message.
        /// </summary>
        /// <value>The last sent message.</value>
        public ConversationMessage LastSentMessage
        {
            get
            {
                return mLastSentMessage;
            }
            set
            {
                mLastSentMessage = value;
                if (value != null && value.Percentage.HasValue)
                {
                    SentMessagePercentage = value.Percentage.Value.ToString();
                }
                this.RaisePropertyChanged("LastSentMessage");
            }
        }

        /// <summary>
        /// Gets or sets the last received message.
        /// </summary>
        /// <value>The last received message.</value>
        public ConversationMessage LastReceivedMessage
        {
            get
            {
                return mLastReceivedMessage;
            }
            set
            {
                mLastReceivedMessage = value;

                if (value != null && value.Percentage.HasValue)
                {
                    ReceivedMessagePercentage = value.Percentage.Value.ToString();
                }

                this.RaisePropertyChanged("LastReceivedMessage");
            }
        }

        /// <summary>
        /// Gets or sets the current preference set.
        /// </summary>
        /// <value>The current preference set.</value>
        public PreferenceSet CurrentPreferenceSet
        {
            get { return mCurrentPreferenceSet; }
            set
            {
                mCurrentPreferenceSet = value;
                this.RaisePropertyChanged("CurrentPreferenceSet");
            }
        }

        /// <summary>
        /// Gets or sets the current negotiation.
        /// </summary>
        /// <value>The current negotiation.</value>
        public PreferenceSetNeg CurrentNegotiation
        {
            get
            {
                return mCurrentNegotiation;
            }
            set
            {
                mCurrentNegotiation = value;
                this.RaisePropertyChanged("CurrentNegotiation");
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
                return mExternalReportModel.HasChanges;
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
                      mExternalReportModel.IsBusy;
            }

            set
            {

                mIsBusy = value;
                this.RaisePropertyChanged("IsBusy");

            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has report.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has report; otherwise, <c>false</c>.
        /// </value>
        public bool HasReport
        {
            get
            {
                return mHasReport;
            }

            set
            {

                mHasReport = value;
                this.RaisePropertyChanged("HasReport");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show no info available].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [show no info available]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowNoInfoAvailable
        {
            get { return mShowNoInfoAvailable; }
            set
            {
                mShowNoInfoAvailable = value;
                this.RaisePropertyChanged("ShowNoInfoAvailable");
            }
        }

        /// <summary>
        /// Gets or sets the feed back message.
        /// </summary>
        /// <value>The feed back message.</value>
        public string FeedBackMessage
        {
            get
            {
                return mFeedBackMessage;
            }
            set
            {
                mFeedBackMessage = value;
                this.RaisePropertyChanged("FeedBackMessage");
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is exceed variation.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is exceed variation; otherwise, <c>false</c>.
        /// </value>
        public bool IsExceedVariation
        {
            get
            {
                return (this.LastReceivedMessage != null && this.LastReceivedMessage.IsExceedVariation) ||
                       (this.LastSentMessage != null && this.LastSentMessage.IsExceedVariation);
            }

        }

        #endregion Properties

        #region → Constructor    .

        /// <summary>
        /// constructor that take a parameter IExternalReportModel and give its values to mExternalReportModel and register required events 
        /// </summary>
        /// <param name="ExternalReportModel">Value of ExternalReportModel</param>
        [ImportingConstructor]
        public ExternalReportViewModel(IExternalReportModel ExternalReportModel)
        {
            #region → Initialization Variables .

            mExternalReportModel = ExternalReportModel;

            #endregion

            #region → Set up event Handling    .

            mExternalReportModel.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(mExternalReportModel_PropertyChanged);
            mExternalReportModel.GetLastSentMessageComplete += new EventHandler<eNegEntityResultArgs<ConversationMessage>>(mExternalReportModel_GetLastSentMessageComplete);
            mExternalReportModel.GetLastReceivedMessageComplete += new EventHandler<eNegEntityResultArgs<ConversationMessage>>(mExternalReportModel_GetLastReceivedMessageComplete);
            mExternalReportModel.GetPreferenceSetByIDComplete += new EventHandler<eNegEntityResultArgs<PreferenceSet>>(mExternalReportModel_GetPreferenceSetByIDComplete);
            mExternalReportModel.GetPreferenceSetNegComplete += new EventHandler<eNegEntityResultArgs<PreferenceSetNeg>>(mExternalReportModel_GetPreferenceSetNegComplete);
            mExternalReportModel.GetIssuesComplete += new EventHandler<eNegEntityResultArgs<Issue>>(mExternalReportModel_GetIssuesComplete);
            mExternalReportModel.GetOptionIssuesComplete += new EventHandler<eNegEntityResultArgs<OptionIssue>>(mExternalReportModel_GetOptionIssuesComplete);
            mExternalReportModel.GetLaterRatedIssueComplete += new EventHandler<eNegEntityResultArgs<LaterRatedIssue>>(mExternalReportModel_GetLaterRatedIssueComplete);
            mExternalReportModel.GetNumericIssuesComplete += new EventHandler<eNegEntityResultArgs<NumericIssue>>(mExternalReportModel_GetNumericIssuesComplete);

            mExternalReportModel.GetMessageIssuesByMessageIDComplete += new EventHandler<eNegEntityResultArgs<MessageIssue>>(mExternalReportModel_GetMessageIssuesByMessageIDComplete);
            mExternalReportModel.GetMessageOptionIssueByMessageIDComplete += new EventHandler<eNegEntityResultArgs<MessageOptionIssue>>(mExternalReportModel_GetMessageOptionIssueByMessageIDComplete);
            mExternalReportModel.GetMessageLaterRatedIssueByMessageIDComplete += new EventHandler<eNegEntityResultArgs<MessageLaterRatedIssue>>(mExternalReportModel_GetMessageLaterRatedIssueByMessageIDComplete);

            #endregion

            #region → Loading Related Tables   .

            this.GetPreferenceSetNegAsync();
            this.GetLastReceivedMessageAsync();
            this.GetLastSentMessageAsync();

            #endregion
        }


        #endregion

        #region → Event Handlers .

        #region →  Loading Event Handlers .

        #region → Get Last Sent Received Message .

        /// <summary>
        /// Ms the external report model_ get last received message complete.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void mExternalReportModel_GetLastReceivedMessageComplete(object sender, eNegEntityResultArgs<ConversationMessage> e)
        {
            if (!e.HasError)
            {
                this.LastReceivedMessage = e.Results.FirstOrDefault();

                //to rebind variation message
                this.RaisePropertyChanged("IsExceedVariation");

                if (this.LastReceivedMessage != null)
                {

                    GetMessageDataMatchingDetailsByMessageID(this.LastReceivedMessage.ConversationMessageID);
                }
            }
            else
            {
                // Notify User if there is any error
                PrefAppMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Ms the external report model_ get last sent message complete.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void mExternalReportModel_GetLastSentMessageComplete(object sender, eNegEntityResultArgs<ConversationMessage> e)
        {
            if (!e.HasError)
            {
                this.LastSentMessage = e.Results.FirstOrDefault();

                //to rebind variation message
                this.RaisePropertyChanged("IsExceedVariation");

                if (this.LastSentMessage != null)
                {
                    GetMessageDataMatchingDetailsByMessageID(this.LastSentMessage.ConversationMessageID);
                }
            }
            else
            {
                // Notify User if there is any error
                PrefAppMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        #endregion

        #region → Preference Set Details         .

        /// <summary>
        /// Call back of Get preference set by ID.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void mExternalReportModel_GetPreferenceSetByIDComplete(object sender, eNegEntityResultArgs<PreferenceSet> e)
        {
            if (!e.HasError)
            {
                this.CurrentPreferenceSet = e.Results.FirstOrDefault();
            }
            else
            {
                // Notify User if there is any error
                PrefAppMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Ms the external report model_ get preference set neg complete.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void mExternalReportModel_GetPreferenceSetNegComplete(object sender, eNegEntityResultArgs<PreferenceSetNeg> e)
        {
            if (!e.HasError)
            {
                this.CurrentNegotiation = e.Results.FirstOrDefault();

                if (this.CurrentNegotiation != null)
                {
                    this.GetPreferenceSetByIDAsync(this.CurrentNegotiation.PreferenceSetID);
                    this.GetIssuesByPreferenceIDAsync(this.CurrentNegotiation.PreferenceSetID);
                    this.GetOptionIssuesByPreferenceIDAsync(this.CurrentNegotiation.PreferenceSetID);
                    this.GetNumericIssuesByPreferenceIDAsync(this.CurrentNegotiation.PreferenceSetID);
                    this.GetLaterRatedByPreferenceIDAsync(this.CurrentNegotiation.PreferenceSetID);
                }
            }
            else
            {
                // Notify User if there is any error
                PrefAppMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Ms the external report model_ get issues complete.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void mExternalReportModel_GetIssuesComplete(object sender, eNegEntityResultArgs<Issue> e)
        {
            if (e.HasError)
            {
                // Notify User if there is any error
                PrefAppMessanger.RaiseErrorMessage.Send(e.Error);
            }
            else
            {
                IsGetIssuesDone = true;
            }
        }

        /// <summary>
        /// Ms the external report model_ get later rated issue complete.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void mExternalReportModel_GetLaterRatedIssueComplete(object sender, eNegEntityResultArgs<LaterRatedIssue> e)
        {
            if (e.HasError)
            {
                //Notify User if there is any error
                PrefAppMessanger.RaiseErrorMessage.Send(e.Error);
            }
            else
            {
                IsGetLaterRatedIssuesDone = true;
            }
        }

        /// <summary>
        /// Ms the external report model_ get option issues complete.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void mExternalReportModel_GetOptionIssuesComplete(object sender, eNegEntityResultArgs<OptionIssue> e)
        {
            if (e.HasError)
            {
                // Notify User if there is any error
                PrefAppMessanger.RaiseErrorMessage.Send(e.Error);
            }
            else
            {
                IsGetOptionIssuesDone = true;
            }
        }

        /// <summary>
        /// Ms the external report model_ get numeric issues complete.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void mExternalReportModel_GetNumericIssuesComplete(object sender, eNegEntityResultArgs<NumericIssue> e)
        {
            if (e.HasError)
            {
                //Notify User if there is any error
                PrefAppMessanger.RaiseErrorMessage.Send(e.Error);
            }
            else
            {
                IsGetNumericIssuesDone = true;
            }
        }

        #endregion

        #region → Messages Data Matching Details .

        /// <summary>
        /// Ms the external report model_ get message later rated issue by message ID complete.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void mExternalReportModel_GetMessageLaterRatedIssueByMessageIDComplete(object sender, eNegEntityResultArgs<MessageLaterRatedIssue> e)
        {
            if (e.HasError)
            {
                // Notify User if there is any error
                PrefAppMessanger.RaiseErrorMessage.Send(e.Error);
            }
            else
            {
                IsGetMessageLaterRatedIssueDone = true;
            }
        }

        /// <summary>
        /// Ms the external report model_ get message option issue by message ID complete.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void mExternalReportModel_GetMessageOptionIssueByMessageIDComplete(object sender, eNegEntityResultArgs<MessageOptionIssue> e)
        {
            if (e.HasError)
            {
                //Notify User if there is any error
                PrefAppMessanger.RaiseErrorMessage.Send(e.Error);
            }
            else
            {
                IsGetMessageOptionIssueDone = true;
            }
        }

        /// <summary>
        /// Ms the external report model_ get message issues by message ID complete.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void mExternalReportModel_GetMessageIssuesByMessageIDComplete(object sender, eNegEntityResultArgs<MessageIssue> e)
        {
            if (e.HasError)
            {
                // Notify User if there is any error
                PrefAppMessanger.RaiseErrorMessage.Send(e.Error);
            }
            else
            {
                IsGetMessageIssueDone = true;
            }
        }

        #endregion

        #endregion

        /// <summary>
        /// PropertyChanged event handler
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">PropertyChangedEventArgs</param>
        private void mExternalReportModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("HasChanges") || e.PropertyName.Equals("IsBusy"))
            {
                this.IsBusy = mExternalReportModel.IsBusy;

                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {

                    if (!this.IsBusy)
                    {
                        this.HasReport = this.LastReceivedMessage != null || this.LastSentMessage != null;
                        this.ShowNoInfoAvailable = !this.HasReport;

                        // Check if not report will be shown
                        if (this.ShowNoInfoAvailable)
                        {
                            // Case 1 that the current Negotiation has not yet attached 
                            // to any Preference set.
                            if (this.CurrentNegotiation == null)
                            {
                                FeedBackMessage = Resources.MsgConfigureNeeded;
                            }
                            else // Case 2 Already attached but no data matching.
                            {
                                if (!PrefAppConfigurations.ConversationIDParameter.HasValue)
                                {
                                    FeedBackMessage = Resources.MsgNoAvailableNegotiationInfo;
                                }
                                else
                                {
                                    FeedBackMessage = Resources.MsgNoAvailableConversationInfo;
                                }
                            }
                        }

                        RefereshSource();
                    }

                });
            }
        }

        #endregion Event Handlers

        #region → Commands       .

        /// <summary>
        /// Relay Command for Changing screen according to navigation
        /// </summary>
        public RelayCommand NavigateToSeeDetailsCommand
        {
            get
            {
                if (mNavigateToSeeDetailsCommand == null)
                {
                    mNavigateToSeeDetailsCommand = new RelayCommand(() =>
                    {
                        try
                        {



                            string url
                                = string.Format("{0}default.aspx?ActionType=Report&NegotiationID={1}&ConversationID={2}&ApplicationID={3}",
                                           AppConfigurations.HostBaseAddress,
                                           PrefAppConfigurations.NegotiationIDParameter.Value.ToString(),
                                           (PrefAppConfigurations.ConversationIDParameter.HasValue ? PrefAppConfigurations.ConversationIDParameter.Value.ToString() : string.Empty),
                                           PrefAppConfigurations.MainPlatformInfo.GetApplicationInfo(PrefAppConfigurations.AppName).ApplicationID.ToString());

                            PrefAppNavigation.NavigateToUrl(url, false);
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            PrefAppMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }, () => true);
                }
                return mNavigateToSeeDetailsCommand;
            }
        }


        /// <summary>
        /// Gets the navigate to app details command.
        /// </summary>
        /// <value>The navigate to app details command.</value>
        public RelayCommand NavigateToAppDetailsCommand
        {
            get
            {
                if (mNavigateToAppDetailsCommand == null)
                {
                    mNavigateToAppDetailsCommand = new RelayCommand(() =>
                    {
                        try
                        { //Conversation
                            string url
                                = string.Format("{0}default.aspx?ActionType=AppSettings&NegotiationID={1}&ConversationID={2}&ApplicationID={3}",
                                           AppConfigurations.HostBaseAddress,
                                           PrefAppConfigurations.NegotiationIDParameter.Value.ToString(),
                                           (PrefAppConfigurations.ConversationIDParameter.HasValue ? PrefAppConfigurations.ConversationIDParameter.Value.ToString() : string.Empty),
                                           PrefAppConfigurations.MainPlatformInfo.GetApplicationInfo(PrefAppConfigurations.AppName).ApplicationID.ToString());

                            PrefAppNavigation.NavigateToUrl(url, false);
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            PrefAppMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }, () => true);
                }
                return mNavigateToAppDetailsCommand;
            }
        }


        #endregion

        #region → Methods        .

        #region → Public         .

        #region → Get Last Sent Received Message .

        /// <summary>
        /// Gets the last received message async.
        /// </summary>
        public void GetLastReceivedMessageAsync()
        {
            this.mExternalReportModel.GetLastReceivedMessageAsync();
        }

        /// <summary>
        /// Gets the last sent message async.
        /// </summary>
        public void GetLastSentMessageAsync()
        {
            this.mExternalReportModel.GetLastSentMessageAsync();
        }

        #endregion

        #region → Preference Set Details         .


        /// <summary>
        /// Gets the preference set by ID async.
        /// </summary>
        /// <param name="PreferenceID">The preference ID.</param>
        public void GetPreferenceSetByIDAsync(Guid PreferenceID)
        {
            this.mExternalReportModel.GetPreferenceSetByIDAsync(PreferenceID);
        }

        /// <summary>
        /// Gets the preference set neg async.
        /// </summary>
        public void GetPreferenceSetNegAsync()
        {
            this.mExternalReportModel.GetPreferenceSetNegAsync();
        }

        /// <summary>
        /// Gets the issues by preference ID async.
        /// </summary>
        /// <param name="PreferenceID">The preference ID.</param>
        public void GetIssuesByPreferenceIDAsync(Guid PreferenceID)
        {
            this.mExternalReportModel.GetIssuesByPreferenceIDAsync(PreferenceID);
        }

        /// <summary>
        /// Gets the option issues by preference ID async.
        /// </summary>
        /// <param name="PreferenceID">The preference ID.</param>
        public void GetOptionIssuesByPreferenceIDAsync(Guid PreferenceID)
        {
            this.mExternalReportModel.GetOptionIssuesByPreferenceIDAsync(PreferenceID);
        }

        /// <summary>
        /// Gets the numeric issues by preference ID async.
        /// </summary>
        /// <param name="PreferenceID">The preference ID.</param>
        public void GetNumericIssuesByPreferenceIDAsync(Guid PreferenceID)
        {
            this.mExternalReportModel.GetNumericIssuesByPreferenceIDAsync(PreferenceID);
        }

        /// <summary>
        /// Gets the later rated by preference ID async.
        /// </summary>
        /// <param name="PreferenceID">The preference ID.</param>
        public void GetLaterRatedByPreferenceIDAsync(Guid PreferenceID)
        {
            this.mExternalReportModel.GetLaterRatedByPreferenceIDAsync(PreferenceID);
        }

        #endregion

        #region → Messages Data Matching Details .

        /// <summary>
        /// Gets the message data matching details by message ID.
        /// </summary>
        /// <param name="MessageID">The message ID.</param>
        public void GetMessageDataMatchingDetailsByMessageID(Guid MessageID)
        {
            this.GetMessageIssuesByMessageIDAsync(MessageID);
            this.GetMessageOptionIssueByMessageIDAsync(MessageID);
            this.GetMessageLaterRatedIssueByMessageIDAsync(MessageID);
        }

        /// <summary>
        /// Gets the message issues by message ID async.
        /// </summary>
        /// <param name="MessageID">The message ID.</param>
        public void GetMessageIssuesByMessageIDAsync(Guid MessageID)
        {
            this.mExternalReportModel.GetMessageIssuesByMessageIDAsync(MessageID);
        }

        /// <summary>
        /// Gets the message option issue by message ID async.
        /// </summary>
        /// <param name="MessageID">The message ID.</param>
        public void GetMessageOptionIssueByMessageIDAsync(Guid MessageID)
        {
            this.mExternalReportModel.GetMessageOptionIssueByMessageIDAsync(MessageID);
        }

        /// <summary>
        /// Gets the message later rated issue by message ID async.
        /// </summary>
        /// <param name="MessageID">The message ID.</param>
        public void GetMessageLaterRatedIssueByMessageIDAsync(Guid MessageID)
        {
            this.mExternalReportModel.GetMessageLaterRatedIssueByMessageIDAsync(MessageID);
        }

        #endregion

        #endregion

        #region → Private        .

        /// <summary>
        /// Refereshes the source.
        /// </summary>
        private void RefereshSource()
        {
            DataReport dataReport = new DataReport();

            FilteredIssueSource = new ObservableCollection<FilteredIssue>(dataReport.GenerateSource(this.LastSentMessage, this.LastReceivedMessage));

            this.RaisePropertyChanged("FilteredIssueSource");

            if (this.HasReport)
            {
                PrefAppMessanger.FlippMessage.Send(PrefAppViewTypes.ReportView);
            }
            else
            {
                PrefAppMessanger.FlippMessage.Send(PrefAppViewTypes.NotificationView);
            }

            PrefAppMessanger.FlippMessage.Send(PrefAppViewTypes.ExitLoadingView);
        }

        #endregion

        #endregion
    }
}
