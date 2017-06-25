#region → Usings   .

using System.Windows.Controls;
using citPOINT.PrefApp.ViewModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System.ComponentModel.Composition;
using citPOINT.PrefApp.Common;

#endregion

#region → History  .

/* Date         User              Change
 * 
 * 10.11.10     M.Wahab       Creation
 */

#endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

#endregion

namespace citPOINT.PrefApp.Client
{
    /// <summary>
    /// Numeric View - Container View For Text Mode and Graph Mode.
    /// </summary>
    [Export(PrefAppViewTypes.NumericIssue)]
    public partial class NumericView : UserControl, ICleanup
    {
        #region → Fields         .

        #endregion

        #region → Constructor    .

        /// <summary>
        /// Creates a new <see cref="NumericIssueGraphView"/> instance.
        /// </summary>
        /// <param name="numericViewModel">The numeric view model.</param>
        public NumericView(IIssueDetailsViewModel numericViewModel)
        {
            this.DataContext = numericViewModel;

            InitializeComponent();

            Dispatcher.BeginInvoke(() =>
            {
                uxnumericIssueGraphView.Width = uxnumericTextView.ActualWidth - 20;
                uxnumericIssueGraphView.MinWidth = uxnumericTextView.ActualWidth - 20;
                uxnumericIssueGraphView.MaxWidth = uxnumericTextView.ActualWidth - 20;

                uxnumericIssueGraphView.Height = 350;
                uxnumericIssueGraphView.MinHeight = 350;
                uxnumericIssueGraphView.MaxHeight = 350;

            });

            this.SizeChanged += new System.Windows.SizeChangedEventHandler(NumericView_SizeChanged);
        }

        #endregion

        #region → Event Handlers .
        
        /// <summary>
        /// Handles the SizeChanged event of the NumericView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.SizeChangedEventArgs"/> instance containing the event data.</param>
        private void NumericView_SizeChanged(object sender, System.Windows.SizeChangedEventArgs e)
        {
            Dispatcher.BeginInvoke(() =>
            {
                uxnumericIssueGraphView.Width = uxnumericTextView.ActualWidth - 20;
                uxnumericIssueGraphView.MinWidth = uxnumericTextView.ActualWidth - 20;
                uxnumericIssueGraphView.MaxWidth = uxnumericTextView.ActualWidth - 20;

                uxnumericIssueGraphView.UpdateSliderWidth();
            });
        }

        #endregion

        #region → Methods        .

        #region → Public     .

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
