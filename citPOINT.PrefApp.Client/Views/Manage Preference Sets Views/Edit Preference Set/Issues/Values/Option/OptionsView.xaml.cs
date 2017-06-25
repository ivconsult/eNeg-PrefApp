#region → Usings   .

using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using citPOINT.PrefApp.Common;
using citPOINT.PrefApp.Data.Web;
using citPOINT.PrefApp.ViewModel;
using GalaSoft.MvvmLight;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using GalaSoft.MvvmLight.Messaging;
using System.ComponentModel.Composition;


#endregion

#region → History  .

/* Date         User              Change
 * 
 * 10.11.10     M.Wahab       Creation
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
    /// Options Issues Value View
    /// </summary>
    [Export(PrefAppViewTypes.OptionIssue)]
    public partial class OptionsIssuesValueView : UserControl, ICleanup
    {
        #region → Fields         .

        /// <summary>
        /// Is Last Clicked is the Star control
        /// </summary>
        bool IsLastControlIsStarts = false;
        bool IsLastTextUpdated = false;
        double ScoreValue = 0;

        #endregion Fields

        #region → Properties     .
        /// <summary>
        /// Gets the view model.
        /// </summary>
        /// <value>The view model.</value>
        private OptionsViewModel ViewModel
        {
            get
            {
                return (this.DataContext as OptionsViewModel);
            }
        }

        #endregion Properties

        #region → Constructor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="OptionsIssuesValueView"/> class.
        /// </summary>
        /// <param name="optionViewModel">The option view model.</param>
        public OptionsIssuesValueView(IIssueDetailsViewModel optionViewModel)
        {
            InitializeComponent();

            this.DataContext = optionViewModel;

            #region Registration for needed messages in PrefAppMessanger

            PrefAppMessanger.EditOptionIssueMessage.Register(this, OnChangeOptions);
          
            #endregion

        }

        #endregion Constructors

        #region → Event Handlers .

        #region Control Event Handlers

        /// <summary>
        /// Handles the ValueChanged event of the uxScoreRating control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedPropertyChangedEventArgs&lt;System.Nullable&lt;System.Double&gt;&gt;"/> instance containing the event data.</param>
        void uxScoreRating_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
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
            }
        }

        /// <summary>
        /// Handles the MouseLeftButtonDown event of the uxScoreRating control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        private void uxScoreRating_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            IsLastControlIsStarts = true;
        }

        /// <summary>
        /// Handles the GotFocus event of the RadNumericUpDown control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void RadNumericUpDown_GotFocus(object sender, RoutedEventArgs e)
        {
            IsLastTextUpdated = (ScoreValue != (sender as RadNumericUpDown).Value.Value);

            //Save Current Value Before Changing
            ScoreValue = (sender as RadNumericUpDown).Value.Value;
            IsLastTextUpdated = false;

            SetCurrentRow((Guid)(sender as RadNumericUpDown).Tag);
        }

        /// <summary>
        /// Handles the LostFocus event of the RadNumericUpDown control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs" /> instance containing the event data.</param>
        private void RadNumericUpDown_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            IsLastTextUpdated = (ScoreValue != (sender as RadNumericUpDown).Value.Value);
        }

        /// <summary>
        /// Handles the GotFocus event of the TextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SetCurrentRow((Guid)(sender as TextBox).Tag);
        }

        /// <summary>
        /// Handles the LostFocus event of the uxOptionName control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs" /> instance containing the event data.</param>
        private void uxOptionName_LostFocus(object sender, RoutedEventArgs e)
        {
            Dispatcher.BeginInvoke(() =>
            {
                this.ViewModel.IsAllOptionsValuesValid();
            });
        }

        #endregion Control Event Handlers
        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Called when [change options].
        /// </summary>
        /// <param name="optionIssue">The option issue.</param>
        private void OnChangeOptions(OptionIssue optionIssue)
        {
            if (this.ViewModel.CurrentIssue.IssueID == optionIssue.IssueID)
            {
                var item = uxOptionsIssueGridView.Items[uxOptionsIssueGridView.Items.Count - 1];
                uxOptionsIssueGridView.ScrollIntoViewAsync(item, s =>
                {
                    var row = s as GridViewRow;
                    if (row != null)
                    {
                        row.IsCurrent = true;
                        row.Focus();
                        row.Cells[1].Focus();
                        (row.Cells[1].Content as TextBox).SelectAll();
                    }
                });
            }
        }

        /// <summary>
        /// Sets the current row.
        /// </summary>
        /// <param name="OptionIssueID">The option issue ID.</param>
        private void SetCurrentRow(Guid OptionIssueID)
        {
            foreach (var item in uxOptionsIssueGridView.Items)
            {
                if ((item as OptionIssue).OptionIssueID == OptionIssueID)
                {
                    uxOptionsIssueGridView.SelectedItem = item;
                    uxOptionsIssueGridView.CurrentItem = item;
                }
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
