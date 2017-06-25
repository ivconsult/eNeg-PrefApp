
#region → Usings   .

using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using citPOINT.PrefApp.Common;
using citPOINT.PrefApp.Data.Web;
using citPOINT.PrefApp.ViewModel;

#endregion

#region → History  .

/* Date         User              Change
 * 
 * 06.04.11    M.Wahab       Creation
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
    /// window Contain a list of check boxes for select 
    /// the negotiations that will be effected by the changes occurred to Preference set.
    /// </summary>
    public partial class SelectNegotiationsPopUp : UserControl
    {
        #region → Constractor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectNegotiationsPopUp"/> class.
        /// </summary>
        public SelectNegotiationsPopUp()
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
                uxcmdOK.Focus();
                Dispatcher.BeginInvoke(() => { uxcmdOK.Command.Execute(uxcmdOK.CommandParameter); });
            }
        }
        #endregion

    }
}
