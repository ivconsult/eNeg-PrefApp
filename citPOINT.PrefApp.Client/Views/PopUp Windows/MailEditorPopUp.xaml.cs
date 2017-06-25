#region → Usings   .
using System.Windows.Controls;

#endregion

#region → History  .

/* Date         User              Change
 * 
 * 27.01.11    Yousra.Mohammed       Creation
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
    /// View appear as a content in PopUp Window to enable user to edit in some Pre-defined Email to send to Negotiators
    /// </summary>
    public partial class MailEditorPopUp : UserControl
    {

        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="SendMailPopUp"/> class.
        /// </summary>
        public MailEditorPopUp()
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
                uxcmdSendMail.Focus();
                Dispatcher.BeginInvoke(() => { uxcmdSendMail.Command.Execute(uxcmdSendMail.CommandParameter); });
            }
        }
        #endregion

    }
}
