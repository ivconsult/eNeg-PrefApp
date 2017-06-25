#region → Usings   .
using citPOINT.PrefApp.Common;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Controls;
using citPOINT.PrefApp.ViewModel;

#endregion

#region → History  .

/* Date         User              Change
 * 
 * 25.01.11    Yousra.Mohammed       Creation
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
    /// View appear as a content in PopUp Window when user want to add new Issue
    /// </summary>
    public partial class AddIssuePopUp : UserControl, ICleanup
    {

        #region → Properties     .


        #endregion

        #region → Constructor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="AddIssuePopUp"/> class.
        /// </summary>
        public AddIssuePopUp()
        {
            InitializeComponent();
        }


        #endregion

        #region → Event Handlers .


        /// <summary>
        /// For Make Register button as Accept Key
        /// </summary>
        /// <param name="sender">Value of Sender</param>
        /// <param name="e">Value of KeyEventArgs</param>
        private void UserControl_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                uxcmdAddIssue.Focus();
                Dispatcher.BeginInvoke(() => { uxcmdAddIssue.Command.Execute(uxcmdAddIssue.CommandParameter); });
            }
        }

        /// <summary>
        /// Handles the ValueChanged event of the uxScoreRating control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedPropertyChangedEventArgs&lt;System.Nullable&lt;System.Double&gt;&gt;"/> instance containing the event data.</param>
        private void uxScoring_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double?> e)
        {
            //To Fire lost focus from uxUpDownRating to get the value of stars scoring control
            uxcmdAddIssue.Focus();
            uxUpDownRating.Value = (sender as eNegRatingControl).CalValue;
        }

        /// <summary>
        /// Handles the ValueChanged event of the uxUpDownRating control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Windows.Controls.RadRangeBaseValueChangedEventArgs"/> instance containing the event data.</param>
        private void uxUpDownRating_ValueChanged(object sender, Telerik.Windows.Controls.RadRangeBaseValueChangedEventArgs e)
        {
            uxScoring.CalValue = uxUpDownRating.Value.Value;
        }
        #endregion

        #region → Methods        .

        #region → Private        .

        #endregion

        #region → Public         .

        /// <summary>
        /// ICleanup interface implementation
        /// </summary>
        public void Cleanup()
        {
            Messenger.Default.Unregister(this);
        }
        #endregion
                
        #endregion
    }

}
