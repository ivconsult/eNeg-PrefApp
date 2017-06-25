#region → Usings   .
using citPOINT.PrefApp.Common;
using citPOINT.PrefApp.Data.Web;
using citPOINT.PrefApp.ViewModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
#endregion

#region → History  .

/* Date         User              Change
 * 
 * 26.09.10     M.Wahab       Creation
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
    /// For Manage Preference Sets Issues 
    /// </summary>
    public partial class IssuesView : UserControl, ICleanup
    {

        #region → Fields         .

        /// <summary>
        /// Is Last Clicked is the Star control
        /// </summary>
        bool IsLastControlIsStarts = false;
        bool IsLastTextUpdated = false;
        bool isIssueComboFocused = false;
        double ScoreValue = 0;

        #endregion Fields

        #region → Properties     .

        /// <summary>
        /// Gets the view model.
        /// </summary>
        /// <value>The view model.</value>
        private IssuesViewModel ViewModel
        {
            get
            {
                return (this.DataContext as IssuesViewModel);
            }
        }
        #endregion Properties

        #region → Constructors   .

        /// <summary>
        /// Default Constuctor
        /// </summary>
        public IssuesView()
        {
            InitializeComponent();

            #region Registration for needed messages in PrefAppMessanger
            PrefAppMessanger.EditIssueMessage.Register(this, OnAddNewIssue);
            #endregion
        }

        #endregion Constructors

        #region → Event Handlers .

        #region Control Event Handlers

        /// <summary>
        /// Handles the CheckChanged event of the CheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void CheckBox_CheckChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            if (e.OriginalSource is CheckBox &&
                (e.OriginalSource as CheckBox).DataContext != null &&
                (e.OriginalSource as CheckBox).DataContext is Issue)
            {
                if ((e.OriginalSource as CheckBox).IsChecked.Value)
                {
                    ((e.OriginalSource as CheckBox).DataContext as Issue).IsSelected = true;
                }
                else
                {
                    ((e.OriginalSource as CheckBox).DataContext as Issue).IsSelected = false;
                }
                ViewModel.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Handles the ValueChanged event of the uxScoreRating control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedPropertyChangedEventArgs&lt;System.Nullable&lt;System.Double&gt;&gt;"/> instance containing the event data.</param>
        private void uxScoreRating_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double?> e)
        {

            if (IsLastControlIsStarts || IsLastTextUpdated)
            {
                IsLastControlIsStarts = false;
                IsLastTextUpdated = false;

                UIElement radRating = ((sender as eNegRatingControl).Parent as StackPanel).Children.Where(s => s.GetType().Equals(typeof(RadNumericUpDown))).FirstOrDefault();

                if (radRating != null)
                {
                    (radRating as RadNumericUpDown).Value = (sender as eNegRatingControl).CalValue;
                }

                this.ViewModel.RefreshIssuesSource(IssuesViewModel.RefreshType.OthersIssuesSource);

                uxScoreChart.Rebind();

                CalcTotalScore();
            }
        }

        /// <summary>
        /// Handles the GotFocus event of the RadNumericUpDown control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void RadNumericUpDown_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            //Save Current Value Before Changing
            ScoreValue = (sender as RadNumericUpDown).Value.Value;
            IsLastTextUpdated = false;

            SetCurrentRow((Guid)(sender as RadNumericUpDown).Tag);
        }

        /// <summary>
        /// Handles the LostFocus event of the RadNumericUpDown control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void RadNumericUpDown_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            IsLastTextUpdated = (ScoreValue != (sender as RadNumericUpDown).Value.Value);

            CalcTotalScore();

            this.ViewModel.RefreshIssuesSource(IssuesViewModel.RefreshType.OthersIssuesSource);

            uxScoreChart.Rebind();
        }

        /// <summary>
        /// Handles the GotFocus event of the TextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void TextBox_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            SetCurrentRow((Guid)(sender as TextBox).Tag);
        }

        /// <summary>
        /// Handles the GotFocus event of the uxcboIssueType control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void uxcboIssueType_GotFocus(object sender, RoutedEventArgs e)
        {
            isIssueComboFocused = true;
        }

        /// <summary>
        /// Handles the SelectionChanged event of the uxcboIssueType control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Windows.Controls.SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void uxcboIssueType_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangedEventArgs e)
        {
            RadComboBox uxcboIssueType = (sender as RadComboBox);

            if (uxcboIssueType.DataContext != null && isIssueComboFocused)
            {
                SetCurrentRow((uxcboIssueType.DataContext as Issue).IssueID);

                ViewModel.CurrentIssue = uxIssueGridView.SelectedItem as Issue;

                if (ViewModel.CurrentIssue != null &&
                    ((Guid)uxcboIssueType.SelectedValue == PrefAppConstant.IssueTypes.NotRated ||
                    (Guid)uxcboIssueType.SelectedValue == PrefAppConstant.IssueTypes.SelectType)
                    && ViewModel.CurrentIssue.IssueWeight != 0)
                {
                    ViewModel.CurrentIssue.IssueWeight = 0;

                    CalcTotalScore();

                    uxScoreChart.Rebind();
                }

                ViewModel.ChangeIssueTypeCommand.Execute(null);

                isIssueComboFocused = false;
            }

        }
        /// <summary>
        /// Handles the DataLoaded event of the uxIssueGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void uxIssueGridView_DataLoaded(object sender, EventArgs e)
        {
            CalcTotalScore();

            if (ViewModel.IssuesSource.Count == 0)
            {
                uxScoreChart.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                uxScoreChart.Visibility = System.Windows.Visibility.Visible;
            }
        }


        /// <summary>
        /// Handles the LostFocus event of the uxtxtIssueName control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs" /> instance containing the event data.</param>
        private void uxtxtIssueName_LostFocus(object sender, RoutedEventArgs e)
        {
            Dispatcher.BeginInvoke(() =>
            {
                this.ViewModel.IsAllIssuesValid(false);
            });
        }

        /// <summary>
        /// Handles the MouseLeftButtonDown event of the uxScoreRating control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs" /> instance containing the event data.</param>
        private void uxScoreRating_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            IsLastControlIsStarts = true;
        }

        /// <summary>
        /// Handles the KeyDown event of the uxtxtIssueName control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs"/> instance containing the event data.</param>
        private void uxtxtIssueName_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            TextBox IssueName = (sender as TextBox);
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                //To Focus on another item to act as lost focus from current item 
                OnAddNewIssue(null);

                //To focus on that item again
                foreach (var item in uxIssueGridView.Items)
                {
                    if ((item as Issue).IssueID == (Guid)IssueName.Tag)
                    {
                        FocusCertainIssueName(item, false);
                    }
                }
            }
        }

        #endregion Control Event Handlers

        #endregion Event Handlers

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Called when [add new issue].
        /// </summary>
        /// <param name="issue">The issue.</param>
        private void OnAddNewIssue(Issue issue)
        {
            if (uxIssueGridView.Items.Count > 0)
            {
                var item = uxIssueGridView.Items[uxIssueGridView.Items.Count - 1];

                FocusCertainIssueName(item, true);
            }
        }

        /// <summary>
        /// Focuses the name of the certain issue.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="selectAll">if set to <c>true</c> [select all].</param>
        private void FocusCertainIssueName(object item, bool selectAll)
        {
            uxIssueGridView.ScrollIntoViewAsync(item, s =>
            {
                var row = s as GridViewRow;
                if (row != null)
                {
                    row.IsCurrent = true;
                    row.Focus();
                    row.Cells[1].Focus();

                    var currentCell = (row.Cells[1].Content as TextBox);

                    if (currentCell != null)
                    {
                        if (selectAll)
                        {
                            currentCell.SelectAll();
                        }
                        else
                        {
                            currentCell.Select(currentCell.Text.Length, 0);
                        }
                    }
                }
            });
        }

        /// <summary>
        /// Calcs the total score.
        /// </summary>
        private void CalcTotalScore()
        {
            if (ViewModel.IssuesSource.Count > 0)
            {
                var sum = ViewModel.IssuesSource.Sum(i => i.IssueWeight);

                uxtbTotalScore.Text = "Total Score: " + sum + "%";

                uxtbTotalScore.Foreground = new SolidColorBrush(sum != 100 ? Colors.Red : Colors.Black);

            }
            else
            {
                uxtbTotalScore.Text = string.Empty;
            }
        }

        /// <summary>
        /// Sets the current row.
        /// </summary>
        /// <param name="issueID">The issue ID.</param>
        private void SetCurrentRow(Guid issueID)
        {
            foreach (var item in uxIssueGridView.Items)
            {
                if (((Issue) item).IssueID == issueID)
                    uxIssueGridView.SelectedItem = item;
            }
        }

        #endregion Private

        #region → Public         .

        /// <summary>
        /// ICleanup interface implementation
        /// </summary>
        public void Cleanup()
        {
            Messenger.Default.Unregister(this);
        }

        #endregion Public

        #endregion Methods


    }
}
