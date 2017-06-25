#region → Usings   .
using System.Windows.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;


#endregion

#region → History  .

/* Date         User        Change
 * 
 * 11.04.12    M.Wahab      • Creation
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
    /// View appear as a content in PopUp Window when user want to Add Or Rename Pref Set .
    /// </summary>
    public partial class AddOrRenamePrefSetPopUp : UserControl, ICleanup
    {

        #region → Properties     .


        #endregion

        #region → Constructor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="AddOrRenamePrefSetPopUp"/> class.
        /// </summary>
        public AddOrRenamePrefSetPopUp()
        {
            InitializeComponent();
        }

        #endregion

        #region → Event Handlers .

        /// <summary>
        /// Handles the KeyDown event of the UserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs"/> instance containing the event data.</param>
        private void UserControl_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                uxcmdSave.Focus();
                Dispatcher.BeginInvoke(() => { uxcmdSave.Command.Execute(uxcmdSave.CommandParameter); });
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
