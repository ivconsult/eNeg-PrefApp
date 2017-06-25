#region → Usings   .
using citPOINT.PrefApp.Common;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Controls;
using citPOINT.eNeg.Common;

#endregion

#region → History  .

/* Date         User              Change
 * 
 * 26.09.10     M.Wahab       Creation
 */

# endregion History

#region → ToDos    .
/*
 * Date         set by User     Description
 * 
 * 
*/
# endregion ToDos

namespace citPOINT.PrefApp.Client
{
    /// <summary>
    /// Used for Just View Preference Set Settings View
    /// </summary>
    public partial class PreferenceSetSettingsView : UserControl, ICleanup
    {
        #region → Constructors   .

        /// <summary>
        /// Default Constuctor
        /// </summary>
        public PreferenceSetSettingsView()
        {
            InitializeComponent();

            this.uxlblPreferenceSetName.DataContext = ViewModelRepository.LastInstance.DataMatchingViewModel;
            this.uxcmdUnAssignSets.DataContext = ViewModelRepository.LastInstance.DataMatchingViewModel;

            eNegMessanger.SendCustomMessage.Register(this, OnSendCustomMessage);
        }

        #endregion Constructors

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Called when [send custom message].
        /// </summary>
        /// <param name="message">The message.</param>
        private void OnSendCustomMessage(eNegMessage message)
        {
            if (message.ReceiverApplicationID == PrefAppConfigurations.ApplicationID)
            {
                Dispatcher.BeginInvoke(() =>
                {
                    uxtbHint.MessageText = message.Message;
                    uxtbHint.Completed = message.ShowMessageCompleted;
                    uxtbHint.Show();
                });
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

        #endregion Public

        #endregion Methods


    }
}
