
#region → Usings   .

using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows;
using citPOINT.PrefApp.Common;
using citPOINT.PrefApp.ViewModel;

#endregion

#region → History  .

/* Date         User              Change
 * 
 * 26.01.11    Yousra.Mohammed       Creation
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
    /// View appear as a content in PopUp Window when to ask user whether he wants to send mail to negotiators or not
    /// </summary>
    public partial class SendMailPopUp : UserControl
    {

        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="SendMailPopUp"/> class.
        /// </summary>
        public SendMailPopUp()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SendMailPopUp"/> class.
        /// </summary>
        /// <param name="IssueName">Name of the issue.</param>
        public SendMailPopUp(string IssueName)
        {
            InitializeComponent();

            IssueName = IssueName.Replace("→",", ");
            Run runContent = new Run();
            runContent.Text = "'" + IssueName + "'";
            runContent.FontWeight = FontWeights.Bold;

            uxTxtNotifyMessage.Text = citPOINT.PrefApp.ViewModel.Resources.SendingMailRequest;
            uxTxtNotifyMessage.Inlines.Add(runContent);
            uxTxtNotifyMessage.Inlines.Add(" from them too?");
             
        }

        #endregion
    }
}
