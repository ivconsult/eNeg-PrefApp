
#region → Usings   .
using System;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel.DomainServices.Client;
using System.ComponentModel.Composition;
using System.Linq.Expressions;
using System.Collections.Generic;
using citPOINT.PrefApp.Data.Web;
using citPOINT.eNeg.Common;
using citPOINT.PrefApp.Common;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using Telerik.Windows.Controls.Charting;
using System.Collections.ObjectModel;
using citPOINT.PrefApp.Data;
using System.Windows.Controls;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using citPOINT.PrefApp.ViewModel.Helpers;
#endregion

#region → History  .

/* Date         User            Change
 * 
 * 27.03.12     Yousra Reda     creation
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
    /// Class to Manage Report in PrefApp
    /// </summary>
    public class ReportViewModel : ViewModelBase
    {
        #region → Fields         .

        private bool mIsBusy = false;
        private bool mIsInReport = true;

        private PreferenceSet mCurrentPrefSet;
        private PreferenceSetNeg mCurrentNegotiation;
        private NegConversation mCurrentConversation;

        private List<NegConversation> mNegotiationConversations;
        private List<ConversationMessage> mConversationMessages;

        //Loading Data Matching Tables.
        private IEnumerable<MessageIssue> mMessageIssues;
        private IEnumerable<MessageOptionIssue> mMessageOptionIssues;
        private IEnumerable<MessageLaterRatedIssue> mMessageLaterRatedIssues;

        #region → Commands       .

        private RelayCommand<int> mFilterDataTypeCommand;
        private RelayCommand<string> mFilterDataForConversationCommand = null;
        private RelayCommand<UIElement> mExportCommand = null;
        private RelayCommand<UIElement> mExportAllPNGCommand = null;

        #endregion

        private NegConversation mReportSelectedConversation;
        private Issue mReportSelectedIssue;
        private List<NegConversation> mReportNegotiationConversations;
        private List<Issue> mReportIssues;

        /// <summary>
        /// Current Filter Type in case of Report
        /// </summary>
        public FilterType CurrentFilterType = FilterType.LastData;

        private DataSeries[] mChartValues;

        #endregion Fields

        #region → Properties     .

        /// <summary>
        /// Gets or sets the data matching VM.
        /// </summary>
        /// <value>The data matching VM.</value>
        public DataMatchingViewModel DataMatchingVM { get; set; }
         
        /// <summary>
        /// Gets a value indicating whether this instance is show chart time.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is show chart time; otherwise, <c>false</c>.
        /// </value>
        public bool IsShowChartTime
        {
            get
            {

                DateTime? Min1 = ChartValues[0].Min(s => (s as ChartPoint).CurrentDate);
                DateTime? Max1 = ChartValues[0].Max(s => (s as ChartPoint).CurrentDate);

                DateTime? Min2 = ChartValues[1].Min(s => (s as ChartPoint).CurrentDate);
                DateTime? Max2 = ChartValues[1].Max(s => (s as ChartPoint).CurrentDate);

                if (Min1 == null)
                {
                    Min1 = Min2;
                    Max1 = Max2;

                    if (Min1 == null)
                    {
                        return false;
                    }
                    else
                    {
                        return Min1.Value.Date == Max1.Value.Date;
                    }
                }
                else if (Min2 == null)
                {
                    return Min1.Value.Date == Max1.Value.Date;
                }
                else
                {
                    if (Min1 > Min2)
                    {
                        Min1 = Min2;
                    }


                    if (Max1 < Max2)
                    {
                        Max1 = Max2;
                    }

                    return Min1.Value.Date == Max1.Value.Date;
                }

            }
        }

        /// <summary>
        /// Gets the chart values.
        /// </summary>
        /// <value>The chart values.</value>
        public DataSeries[] ChartValues
        {
            get
            {
                if (mChartValues == null)
                    mChartValues = new DataSeries[2];

                #region → Initialize Data Series for sent & received lines on graph .

                mChartValues[0] = new DataSeries();
                mChartValues[0].Definition = new LineSeriesDefinition();
                mChartValues[0].Definition.ShowItemLabels = true;
                //(mChartValues[0].Definition as LineSeriesDefinition).LabelSettings = new LabelSettings() { Distance = 20, ShowConnectors = true };
                mChartValues[0].LegendLabel = "Sent Data";

                mChartValues[1] = new DataSeries();
                mChartValues[1].Definition = new LineSeriesDefinition();
                mChartValues[1].Definition.ShowItemLabels = true;
                //(mChartValues[1].Definition as LineSeriesDefinition).LabelSettings = new LabelSettings() { Distance = 20, ShowConnectors = true };
                mChartValues[1].LegendLabel = "Received Data";
                #endregion

                #region → Case User select certain conversation & certain Issue     .

                if (mReportSelectedIssue.IssueID != new Guid() && mReportSelectedConversation.PreferenceSetNegID != new Guid())
                {

                    #region → Add Points for sent Data about certain issue              .

                    foreach (var MSG in ReportSelectedConversation.ConversationMessages.Where
                        (s => s.MessageIssues.Where(o => o.Issue == mReportSelectedIssue).FirstOrDefault() != null && s.IsSent == true)
                        .OrderBy(s => s.DeletedOn))
                    {
                        foreach (var messageIssue in MSG.MessageIssues.Where(s => s.Issue == mReportSelectedIssue))
                        {
                            mChartValues[0].Add(new ChartPoint() { YValue = (double)(messageIssue.Score), XValue = MSG.RatedDate.Value.ToOADate(), CurrentDate = MSG.RatedDate.Value });
                        }
                    }
                    #endregion

                    #region → Add Points for received Data about certain issue          .

                    foreach (var MSG in ReportSelectedConversation.ConversationMessages.Where
                        (s => s.MessageIssues.Where(o => o.Issue == mReportSelectedIssue).FirstOrDefault() != null && s.IsSent == false)
                        .OrderBy(s => s.DeletedOn))
                    {
                        foreach (var messageIssue in MSG.MessageIssues.Where(s => s.Issue == mReportSelectedIssue))
                        {
                            mChartValues[1].Add(new ChartPoint() { YValue = (double)(messageIssue.Score), XValue = MSG.RatedDate.Value.ToOADate(), CurrentDate = MSG.RatedDate.Value });
                        }
                    }
                    #endregion

                }
                #endregion

                #region → Case User select certain conversation & "All Issues"      .

                else if (mReportSelectedIssue.IssueID == new Guid() && mReportSelectedConversation.PreferenceSetNegID != new Guid())
                {
                    foreach (var MSG in ReportSelectedConversation.ConversationMessages.Where
                        (s => s.IsSent == true && s.Percentage != null).OrderBy(s => s.DeletedOn))
                    {
                        mChartValues[0].Add(new ChartPoint() { YValue = (double)(MSG.Percentage), XValue = MSG.RatedDate.Value.ToOADate(), CurrentDate = MSG.RatedDate.Value });
                    }

                    foreach (var MSG in ReportSelectedConversation.ConversationMessages.Where
                        (s => s.IsSent == false && s.Percentage != null).OrderBy(s => s.DeletedOn))
                    {
                        mChartValues[1].Add(new ChartPoint() { YValue = (double)(MSG.Percentage), XValue = MSG.RatedDate.Value.ToOADate(), CurrentDate = MSG.RatedDate.Value });
                    }
                }

                #endregion

                #region → Case User select "All conversation" & "All Issues"        .

                else if (mReportSelectedIssue.IssueID == new Guid() && mReportSelectedConversation.PreferenceSetNegID == new Guid())
                {
                    if (ConversationMessages != null && CurrentNegotiation != null)
                    {
                        foreach (var MSG in ConversationMessages.Where
                            (s => s.NegConversation.PreferenceSetNegID == CurrentNegotiation.PreferenceSetNegID &&
                                s.IsSent == true && s.Percentage != null).OrderBy(s => s.DeletedOn))
                        {
                            mChartValues[0].Add(new ChartPoint() { YValue = (double)(MSG.Percentage), XValue = MSG.RatedDate.Value.ToOADate(), CurrentDate = MSG.RatedDate.Value });
                        }

                        foreach (var MSG in ConversationMessages.Where
                            (s => s.NegConversation.PreferenceSetNegID == CurrentNegotiation.PreferenceSetNegID &&
                                s.IsSent == false && s.Percentage != null).OrderBy(s => s.DeletedOn))
                        {
                            mChartValues[1].Add(new ChartPoint() { YValue = (double)(MSG.Percentage), XValue = MSG.RatedDate.Value.ToOADate(), CurrentDate = MSG.RatedDate.Value });
                        }
                    }
                }

                #endregion

                #region → Case User select "All conversation" & certain Issue       .

                else if (mReportSelectedIssue.IssueID != new Guid() && mReportSelectedConversation.PreferenceSetNegID == new Guid())
                {
                    foreach (var MSG in ConversationMessages.Where
                        (s => s.NegConversation.PreferenceSetNegID == CurrentNegotiation.PreferenceSetNegID &&
                            s.MessageIssues.Where(o => o.Issue == mReportSelectedIssue).FirstOrDefault() != null &&
                            s.IsSent == true && s.Percentage != null).OrderBy(s => s.DeletedOn))
                    {
                        foreach (var messageIssue in MSG.MessageIssues.Where(s => s.Issue == mReportSelectedIssue))
                        {
                            mChartValues[0].Add(new ChartPoint() { YValue = (double)(messageIssue.Score), XValue = MSG.RatedDate.Value.ToOADate(), CurrentDate = MSG.RatedDate.Value });
                        }
                    }

                    foreach (var MSG in ConversationMessages.Where
                        (s => s.NegConversation.PreferenceSetNegID == CurrentNegotiation.PreferenceSetNegID &&
                            s.MessageIssues.Where(o => o.Issue == mReportSelectedIssue).FirstOrDefault() != null &&
                            s.IsSent == false && s.Percentage != null).OrderBy(s => s.DeletedOn))
                    {
                        foreach (var messageIssue in MSG.MessageIssues.Where(s => s.Issue == mReportSelectedIssue))
                        {
                            mChartValues[1].Add(new ChartPoint() { YValue = (double)(messageIssue.Score), XValue = MSG.RatedDate.Value.ToOADate(), CurrentDate = MSG.RatedDate.Value });
                        }
                    }
                }

                #endregion

                return mChartValues;
            }
        }

        /// <summary>
        /// Gets or sets the filtered issue source.
        /// </summary>
        /// <value>The filtered issue source.</value>
        public ObservableCollection<FilteredIssue> FilteredIssueSource { get; set; }

        /// <summary>
        /// Gets the report negotiation conversations.
        /// </summary>
        /// <value>The report negotiation conversations.</value>
        public List<NegConversation> ReportNegotiationConversations
        {
            get
            {
                mReportNegotiationConversations = new List<NegConversation>();

                if (NegotiationConversations != null && CurrentNegotiation != null)
                {
                    foreach (var Conv in NegotiationConversations.Where(s => s.PreferenceSetNegID == CurrentNegotiation.PreferenceSetNegID))
                    {
                        mReportNegotiationConversations.Add(Conv);
                    }
                }

                mReportNegotiationConversations.Insert(0, new NegConversation { NegConversationID = new Guid(), Name = "All Conversations" });

                return mReportNegotiationConversations;
            }
        }

        /// <summary>
        /// Gets the report issues.
        /// </summary>
        /// <value>The report issues.</value>
        public List<Issue> ReportIssues
        {
            get
            {
                mReportIssues = new List<Issue>();

                if (CurrentPreferenceSet != null)
                {
                    foreach (var issue in CurrentPreferenceSet.Issues)
                    {
                        mReportIssues.Add(issue);
                    }

                    mReportIssues.Insert(0, new Issue { IssueID = new Guid(), IssueName = "All Issues" });
                }

                return mReportIssues;
            }
        }

        /// <summary>
        /// Gets or sets the report selected conversation.
        /// </summary>
        /// <value>The report selected conversation.</value>
        public NegConversation ReportSelectedConversation
        {
            get
            {
                if (mReportSelectedConversation == null)
                    mReportSelectedConversation = new NegConversation();

                return mReportSelectedConversation;
            }
            set
            {
                //if (mReportSelectedConversation != value)
                //{
                mReportSelectedConversation = value;
                FilterDataForConversationCommand.Execute(CurrentFilterType.ToString());

                RaisePropertyChanged("ReportSelectedConversation");
                PrefAppMessanger.FlippMessage.Send(PrefAppViewTypes.ReportGraphView);
                //}
            }
        }

        /// <summary>
        /// Gets or sets the report selected issue.
        /// </summary>
        /// <value>The report selected issue.</value>
        public Issue ReportSelectedIssue
        {
            get
            {
                if (mReportSelectedIssue == null)
                    mReportSelectedIssue = new Issue();

                return mReportSelectedIssue;
            }
            set
            {
                if (mReportSelectedIssue != value)
                {
                    mReportSelectedIssue = value;
                    PrefAppMessanger.FlippMessage.Send(PrefAppViewTypes.ReportGraphView);
                    RaisePropertyChanged("ReportSelectedIssue");
                }
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
                this.RaisePropertyChanged("IsBusy");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is in report.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is in report; otherwise, <c>false</c>.
        /// </value>
        public bool IsInReport
        {
            get { return mIsInReport; }
            set
            {
                mIsInReport = value;
                this.RaisePropertyChanged("IsInReport");
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
                mCurrentNegotiation = value;
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
                this.RaisePropertyChanged("CurrentConversation");
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
                mCurrentPrefSet = DataMatchingVM.CurrentPreferenceSet;
                return mCurrentPrefSet;
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

            set
            {
                if (value != mNegotiationConversations)
                {
                    mNegotiationConversations = value;

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
            set
            {
                if (value != mConversationMessages)
                {
                    mConversationMessages = value;
                    this.RaisePropertyChanged("ConversationMessages");
                }
            }
        }

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

            set
            {
                if (value != mMessageIssues)
                {
                    mMessageIssues = value;
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

            set
            {
                if (value != mMessageOptionIssues)
                {
                    mMessageOptionIssues = value;
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

            set
            {
                if (value != mMessageLaterRatedIssues)
                {
                    mMessageLaterRatedIssues = value;
                    this.RaisePropertyChanged("MessageLaterRatedIssues");
                }
            }
        }
        #endregion Properties

        #region → Constructor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportViewModel"/> class.
        /// </summary>
        /// <param name="DataMatchingVM">The data matching VM.</param>
        public ReportViewModel(DataMatchingViewModel DataMatchingVM)
        {
            #region → Initialization Variables .

            this.DataMatchingVM = DataMatchingVM;
            FilteredIssueSource = new ObservableCollection<FilteredIssue>();

            #endregion

            #region → Set up event Handling    .

            DataMatchingVM.mDataMatchingModel.PropertyChanged += new PropertyChangedEventHandler(mDataMatchingModel_PropertyChanged);
            PDFGenerator.BeforeReportExportStart += new EventHandler(PDFGenerator_BeforeReportExportStart);
            PDFGenerator.AfterReportExported += new EventHandler(PDFGenerator_AfterReportExported);

            #endregion

        }

        #endregion

        #region → Event Handlers .

        /// <summary>
        /// Handles the PropertyChanged event of the mDataMatchingModel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void mDataMatchingModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("IsBusy"))
            {
                this.IsBusy = DataMatchingVM.mDataMatchingModel.IsBusy;

                RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Handles the AfterReportExported event of the PDFGenerator control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void PDFGenerator_AfterReportExported(object sender, EventArgs e)
        {
            this.IsInReport = true;
        }

        /// <summary>
        /// Handles the BeforeReportExportStart event of the PDFGenerator control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void PDFGenerator_BeforeReportExportStart(object sender, EventArgs e)
        {
            this.IsInReport = false;
        }

        #endregion Event Handlers

        #region → Commands       .

        /// <summary>
        /// Gets the filter data type command.
        /// </summary>
        /// <value>The filter data type command.</value>
        public RelayCommand<int> FilterDataTypeCommand
        {
            get
            {
                if (mFilterDataTypeCommand == null)
                {
                    mFilterDataTypeCommand = new RelayCommand<int>((SelectedIndex) =>
                    {
                        try
                        {
                            switch (SelectedIndex)
                            {
                                //In cae seelection is "Last Data" 
                                case 0:
                                    FilterDataForConversationCommand.Execute(FilterType.LastData.ToString());
                                    CurrentFilterType = FilterType.LastData;
                                    break;
                                //In case selection is "Best Scoring"
                                case 1:
                                    FilterDataForConversationCommand.Execute(FilterType.BestScoring.ToString());
                                    CurrentFilterType = FilterType.BestScoring;
                                    break;
                            }
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            PrefAppMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , (SelectedIndex) => !this.IsBusy);
                }
                return mFilterDataTypeCommand;
            }
        }

        /// <summary>
        /// Gets the filter data for conversation.
        /// </summary>
        /// <value>The filter data for conversation.</value>
        public RelayCommand<string> FilterDataForConversationCommand
        {
            get
            {
                if (mFilterDataForConversationCommand == null)
                {
                    mFilterDataForConversationCommand = new RelayCommand<string>((Filter) =>
                    {
                        try
                        {
                            if (!this.IsBusy)
                            {
                                DataReport report = new DataReport();
                                FilteredIssueSource = new ObservableCollection<FilteredIssue>();

                                #region → if First Filter is "All Conversation"   .
                                if (ReportSelectedConversation.NegConversationID == new Guid())
                                {
                                    if (ConversationMessages != null)
                                    {
                                        var ConvMessages = ConversationMessages.Where(s => s.NegConversation.PreferenceSetNegID == CurrentNegotiation.PreferenceSetNegID);

                                        if (ConvMessages != null)
                                        {
                                            FilteredIssueSource = report.GenerateSource(ConvMessages.ToList(), Filter);
                                        }
                                    }
                                }
                                #endregion

                                #region → if First Filter is certain Conversation .
                                else
                                {
                                    if (NegotiationConversations != null)
                                    {
                                        var Conv = NegotiationConversations.Where(o => o.NegConversationID == ReportSelectedConversation.NegConversationID).FirstOrDefault();

                                        if (Conv != null)
                                        {
                                            FilteredIssueSource = report.GenerateSource(Conv.ConversationMessages.ToList(), Filter);
                                        }
                                    }
                                }
                                #endregion

                                FilteredIssueSource = new ObservableCollection<FilteredIssue>(FilteredIssueSource.OrderBy(s => s.IssueName).AsEnumerable());
                                this.RaisePropertyChanged("FilteredIssueSource");
                            }
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            PrefAppMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , (Filter) => this.IsBusy == false);
                }
                return mFilterDataForConversationCommand;
            }
        }

        /// <summary>
        /// Gets the export to PDF command.
        /// </summary>
        /// <value>The export to PDF command.</value>
        public RelayCommand<UIElement> ExportCommand
        {
            get
            {
                if (mExportCommand == null)
                {
                    mExportCommand = new RelayCommand<UIElement>((graph) =>
                    {
                        try
                        {
                            if (!this.IsBusy)
                            {
                                if (graph == null)
                                {
                                    SaveFileDialog dialog = new SaveFileDialog();
                                    dialog.DefaultExt = "png";
                                    dialog.Filter = string.Format("{1} File (*.{0}) | *.{0}", "png", "png");

                                    if (!(bool)dialog.ShowDialog())
                                        return;

                                    Stream fileStream = dialog.OpenFile();
                                    PrefAppMessanger.ExportReport.Send(fileStream);
                                }
                                else if (graph is UIElement)
                                {
                                    PDFGenerator.ExportDiagram(graph, CurrentNegotiation.Name);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            PrefAppMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , (graph) => this.IsBusy == false);
                }
                return mExportCommand;
            }
        }

        /// <summary>
        /// Gets the export to PDF command.
        /// </summary>
        /// <value>The export to PDF command.</value>
        public RelayCommand<UIElement> ExportAllPNGCommand
        {
            get
            {
                if (mExportAllPNGCommand == null)
                {
                    mExportAllPNGCommand = new RelayCommand<UIElement>((graph) =>
                    {
                        try
                        {
                            if (!this.IsBusy)
                            {
                                if (graph is UIElement)
                                {
                                    this.IsInReport = false;

                                    //Create WriteableBitmap object which is what is being exported.
                                    WriteableBitmap wBitmap = new System.Windows.Media.Imaging.WriteableBitmap(graph, null);

                                    wBitmap.Render(graph, new TranslateTransform());

                                    wBitmap.Invalidate();

                                    this.IsInReport = true;

                                    SaveFile(wBitmap);


                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            PrefAppMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , (graph) => this.IsBusy == false);
                }
                return mExportAllPNGCommand;
            }
        }

        #endregion Events

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
        #endregion Private

        #region → Public         .

        /// <summary>
        /// Raises the can execute changed.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            ExportAllPNGCommand.RaiseCanExecuteChanged();
            ExportCommand.RaiseCanExecuteChanged();
            FilterDataForConversationCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Refereshes the sources.
        /// </summary>
        public void RefereshSources()
        {
            this.RaisePropertyChanged("ReportNegotiationConversations");

            this.RaisePropertyChanged("ReportIssues");

            if (mReportNegotiationConversations != null &&
                mReportNegotiationConversations.Count > 0 &&
                mReportIssues != null &&
                mReportIssues.Count > 0)
            {
                this.ReportSelectedConversation = mReportNegotiationConversations[0];

                this.ReportSelectedIssue = mReportIssues[0];
            }

            this.RaisePropertyChanged("ChartValues");

            if (PrefAppConfigurations.ConversationIDParameter.HasValue)
            {
                this.ReportSelectedConversation = this.ReportNegotiationConversations.Where(s => s.ConversationID == PrefAppConfigurations.ConversationIDParameter).FirstOrDefault();
            }
        }

        #endregion Public

        #endregion Methods
    }
}
