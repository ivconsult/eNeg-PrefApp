

#region → Usings   .

using System.Windows;
using System.Windows.Controls;
using citPOINT.PrefApp.ViewModel;
using citPOINT.PrefApp.Common;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight;


#endregion

#region → History  .

/* Date         User              Change
 * 
 * 16.01.11    Yousra.Mohammed       Creation
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
    /// View appear as a content in PopUp Window when user want to add Negotiation to Preference Set
    /// </summary>
    public partial class AddNegToPrefSet : UserControl, ICleanup
    {

        #region → Constructor    .
        /// <summary>
        /// Initializes a new instance of the <see cref="AddNegToPrefSet"/> class.
        /// </summary>
        public AddNegToPrefSet()
        {
            InitializeComponent();

            //Initialize messages from Messages Resource
            this.uxBILoadingNegs.BusyContent = citPOINT.PrefApp.ViewModel.Resources.LoadingNegs;
            this.uxTbHeader.Text = citPOINT.PrefApp.ViewModel.Resources.AddNegWindowHeader;

            #region → Register needed messages .
            PrefAppMessanger.LoadCompleted.Register(this, OnLoadCompleted);
            #endregion
        }

        #endregion

        #region → Event Handlers .

        /// <summary>
        /// Handles the Click event of the uxcmdAddNegotiation control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void uxcmdAddNegotiation_Click(object sender, RoutedEventArgs e)
        {
            if (uxCboNegotiations.SelectedItem != null)
            {
                this.uxBILoadingNegs.BusyContent = citPOINT.PrefApp.ViewModel.Resources.AddingNegToPreferencesSetLoading;
                ((this.DataContext) as DataMatchingViewModel).AssignPreferenceSetCommand.Execute(uxCboNegotiations.SelectedItem);
                PrefAppMessanger.FlippMessage.Send(PrefAppViewTypes.ClosePopupView);
            }
        }

        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Called when [load completed].
        /// </summary>
        /// <param name="OperationName">Name of the operation.</param>
        private void OnLoadCompleted(string OperationName)
        {
            if (OperationName == PrefAppMessanger.OperationType.AvailableNegotiationsCompleted.ToString())
            {
                if (uxCboNegotiations.Items.Count > 0)
                    uxCboNegotiations.SelectedIndex = 0;
            }
        }

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
