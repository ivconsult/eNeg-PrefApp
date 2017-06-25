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
    /// View appear as a content in PopUp Window when user want to Replace published PrefSet.
    /// </summary>
    public partial class ReplacePublishedSetPopUp : UserControl, ICleanup
    {

        #region → Properties     .


        #endregion

        #region → Constructor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="ReplacePublishedSetPopUp"/> class.
        /// </summary>
        public ReplacePublishedSetPopUp()
        {
            InitializeComponent();
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
