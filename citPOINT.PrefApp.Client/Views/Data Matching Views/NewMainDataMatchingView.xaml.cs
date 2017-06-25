
#region → Usings   .
using System;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using citPOINT.PrefApp.Common;
using citPOINT.PrefApp.Data.Web;
using citPOINT.PrefApp.ViewModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System.Collections;
using System.Collections.Generic;
using Telerik.Windows.Controls.GridView;
using System.Windows;
#endregion

#region → History  .

/* Date         User              Change
 * 
 * 12.01.11     Yousra Reda       Creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion

namespace citPOINT.PrefApp.Client
{
    /// <summary>
    /// Used as container to all sub-view used in Data Matching Module 
    /// </summary>
    public partial class NewMainDataMatchingView : UserControl, ICleanup
    {
        #region → Properties     .

        #region Using MEF to import Preference Sets ViewModel

        /// <summary>
        /// Set View Model By MEF
        /// </summary>
        public DataMatchingViewModel ViewModel
        {
            get
            {
                return (DataContext as DataMatchingViewModel);
            }
            set
            {
                DataContext = value;
            }
        }

        #endregion " Using MEF to import MainPageViewModel "

        /// <summary>
        /// Gets the preference sets VM.
        /// </summary>
        /// <value>The preference sets VM.</value>
        private PreferenceSetsViewModel PreferenceSetsVM
        {
            get
            {
                return this.ViewModel.PreferenceSetsVM;
            }
        }

        /// <summary>
        /// Gets the issues VM.
        /// </summary>
        /// <value>The issues VM.</value>
        private IssuesViewModel IssuesVM
        {
            get
            {
                return this.PreferenceSetsVM.IssuesVM;
            }
        }

        #endregion

        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="NewMainDataMatchingView"/> class.
        /// </summary>
        public NewMainDataMatchingView(DataMatchingViewModel dataMatchingViewModel)
        {
            InitializeComponent();

            this.ViewModel = dataMatchingViewModel;

            #region → Registing Gala Soft Messages               .
            {
                PrefAppMessanger.EditConversationMessage.Register(this, OnEditConversationMessage);

                PrefAppMessanger.NewPopUp.Register(this, OnAddNewPopUp);
                PrefAppMessanger.NewPopUp.Register(this, OnNewOptionPopUp);

            }
            #endregion
        }

        #endregion

        #region → Event Handlers .

        /// <summary>
        /// Handles the SelectionChanged event of the uxcboReceivedSend control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Windows.Controls.SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void uxcboReceivedSend_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (this.ViewModel != null)
            {
                switch (uxcboReceivedSend.SelectedIndex)
                {
                    case 0://All
                        this.ViewModel.MessageFilterType = MessageFilter.All;
                        break;
                    case 1:
                        this.ViewModel.MessageFilterType = MessageFilter.ReceivedDataOnly;
                        break;
                    case 2:
                        this.ViewModel.MessageFilterType = MessageFilter.SendDataOnly;
                        break;
                }
            }
        }

        /// <summary>
        /// Handles the DragDropCompleted event of the dragDrop control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="citPOINT.PrefApp.Common.DragEventArgs"/> instance containing the event data.</param>
        private void dragDrop_DragDropCompleted(object sender, Common.DragEventArgs e)
        {
            if (e.TargetControl.GetType().Equals(typeof(Border)))
            {
                PrefAppConfigurations.MailPreferenceSetNegID = this.ViewModel.CurrentNegotiation.PreferenceSetNegID;
                PrefAppMessanger.NewPopUp.Send(e.Value.TrimEnd().TrimStart(), PrefAppMessanger.PopUpType.NewIssue);
            }
        }

        /// <summary>
        /// Handles the LoadingRowDetails event of the uxMessagesGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Windows.Controls.GridView.GridViewRowDetailsEventArgs"/> instance containing the event data.</param>
        private void uxMessagesGridView_LoadingRowDetails(object sender, Telerik.Windows.Controls.GridView.GridViewRowDetailsEventArgs e)
        {
            eNegRichTextBox txtMatching = (eNegRichTextBox)(e.DetailsElement as System.Windows.Controls.Border).Child;

            #region → Handling the drag drop Operations          .

            //Create new Drag Drop manager
            DragDrop dragDrop = new DragDrop(this, TopCanvas, txtMatching.MainStackPanel);

            //Set it to common to be use in any other place.
            PrefAppConfigurations.MainDragDropManager = dragDrop;

            //Addin the sources of drag.
            dragDrop.SourceTargets.Add(txtMatching.DragLabel);

            //Adding Button to accept drag items.
            dragDrop.DestinationTargets.Add((Border)this.uxBtnAddNewIssue.Content);

            dragDrop.DragDropCompleted += new EventHandler<Common.DragEventArgs>(dragDrop_DragDropCompleted);

            uxacrdIssuesDatamatchingAccordion.UpdateDragDropArea();

            #endregion
        }

        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Called when [edit conversation message].
        /// </summary>
        /// <param name="ConvMessage">The convn message.</param>
        private void OnEditConversationMessage(ConversationMessage ConvMessage)
        {
            if (ConvMessage != null)
            {
                if (uxcboReceivedSend.SelectedIndex == 0)
                {
                    uxcboReceivedSend.SelectedIndex = 0;
                }
            }

            Dispatcher.BeginInvoke(() =>
            {
                FocusCertainMessage(ConvMessage);
            });
        }

        /// <summary>
        /// Focuses the name of the certain issue.
        /// </summary>
        /// <param name="item">The item.</param>
        private void FocusCertainMessage(object item)
        {
            //uxMessagesGridView.ScrollIntoViewAsync(item, s =>
            //{
            //    var row = s as GridViewRow;

            //    if (row != null)
            //    {
            //        row.IsCurrent = true;
            //        row.Focus();
            //    }
            //});
        }

        /// <summary>
        /// Called when [add new pop up].
        /// </summary>
        /// <param name="DragedValue">The draged value.</param>
        private void OnAddNewPopUp(string DragedValue)
        {
            if (PrefAppMessanger.NewPopUp.PopUpType == PrefAppMessanger.PopUpType.NewIssue.ToString())
            {
                this.SelectTruePrefSet();

                //Add new Issue to binded to the Add issue pop up window items
                this.IssuesVM.CurrentIssue = this.IssuesVM.AddIssue(false, ViewModel.CurrentPreferenceSet);
                this.IssuesVM.CurrentIssue.IssueName = DragedValue;

                #region Show PopUp window to choose and identify an Issue and add it
                PopUpWindow AddIssueWindow = new PopUpWindow("Add New Issue");
                AddIssueWindow.DataContext = this.IssuesVM;
                AddIssueWindow.Content = new AddIssuePopUp();
                AddIssueWindow.ShowDialog();
                #endregion
            }
        }
        
        /// <summary>
        /// Called when [new option pop up].
        /// </summary>
        /// <param name="RelatedIssue">The related issue.</param>
        private void OnNewOptionPopUp(Issue RelatedIssue)
        {
            if (PrefAppMessanger.NewPopUp.PopUpType == PrefAppMessanger.PopUpType.NewOption.ToString())
            {
                #region → Add new Option Issue to binded to the Add Option pop up window items  .

                this.SelectTruePrefSet();

                this.IssuesVM.CurrentIssue = RelatedIssue;

                this.IssuesVM.CurrentOption = this.IssuesVM.AddOptionIssue(false, RelatedIssue);

                RelatedIssue.OptionIssues.Add(this.IssuesVM.CurrentOption);

                this.IssuesVM.CurrentOption.OptionIssueValue = PrefAppMessanger.NewPopUp.DragedValue;

                #endregion

                #region → Show PopUp window to define an Option and add it                      .

                PopUpWindow AddOptionWindow = new PopUpWindow("Add New Option");
                AddOptionWindow.DataContext = this.IssuesVM;
                AddOptionWindow.Content = new AddOptionPopUp();
                AddOptionWindow.ShowDialog();

                #endregion

            }
            else if (PrefAppMessanger.NewPopUp.PopUpType == PrefAppMessanger.PopUpType.NewLaterRated.ToString())
            {
                #region → Add new LaterRated Issue to binded to the Add LaterRated pop up window items .

                this.SelectTruePrefSet();

                this.IssuesVM.CurrentLaterRated = this.IssuesVM.AddLaterRatedIssue(false, RelatedIssue);

                this.IssuesVM.CurrentIssue = RelatedIssue;

                this.IssuesVM.CurrentIssue.LaterRatedIssues.Add(this.IssuesVM.CurrentLaterRated);

                this.IssuesVM.CurrentLaterRated.LaterRatedIssueValue = PrefAppMessanger.NewPopUp.DragedValue;
                #endregion

                #region → Show PopUp window to define an LaterRated option and add it                  .

                PopUpWindow AddLaterRatedWindow = new PopUpWindow("Add New Option");
                AddLaterRatedWindow.DataContext = this.IssuesVM;
                AddLaterRatedWindow.Content = new AddLaterRatedPopUp();
                AddLaterRatedWindow.ShowDialog();

                #endregion
            }
        }
        
        /// <summary>
        /// Selects the true pref set.
        /// </summary>
        private void SelectTruePrefSet()
        {

            if (ViewModel.CurrentPreferenceSet != ViewModel.PreferenceSetsVM.CurrentPreferenceSet)
            {
                ViewModel.PreferenceSetsVM.CurrentPreferenceSet = ViewModel.CurrentPreferenceSet;
            }
        }

        #endregion

        #region → Public         .

        /// <summary>
        /// Cleanups this instance.
        /// </summary>
        public void Cleanup()
        {
            Messenger.Default.Unregister(this);
        }

        #endregion

        #endregion
    }
}
