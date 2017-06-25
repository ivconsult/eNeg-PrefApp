#region → Usings   .
using System.Windows.Controls;
using citPOINT.PrefApp.ViewModel;
using Telerik.Windows.Controls;
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
    /// Numeric Text Mode View
    /// </summary>
    public partial class NumericTextView : UserControl
    {

        #region → Constructor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="NumericTextView"/> class.
        /// </summary>
        public NumericTextView()
        {
            InitializeComponent();
        }

        #endregion Constructor

        #region → Event Handlers .

        /// <summary>
        /// Handles the KeyDown event of the UserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs"/> instance containing the event data.</param>
        private void UserControl_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter && (sender as RadNumericUpDown) != null)
            {
                uxtxtUnit.Focus();
                (sender as RadNumericUpDown).Focus();
            }
        }

        #endregion
    }
}