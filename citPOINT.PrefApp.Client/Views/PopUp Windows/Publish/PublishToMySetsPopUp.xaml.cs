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
    /// View appear as a content in PopUp Window when user want to copy published PrefSet to his Sets
    /// </summary>
    public partial class PublishToMySetsPopUp : UserControl, ICleanup
    {

        #region → Properties     .


        #endregion

        #region → Constructor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishToMySetsPopUp"/> class.
        /// </summary>
        public PublishToMySetsPopUp()
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
                uxcmdPublish.Focus();
                Dispatcher.BeginInvoke(() => { uxcmdPublish.Command.Execute(uxcmdPublish.CommandParameter); });
            }
        }

        #endregion
        #region → Methods        .

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
