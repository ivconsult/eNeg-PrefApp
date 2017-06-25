#region → Usings   .
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

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
    /// For Manage Preference Sets Values
    /// </summary>
    public partial class ValuesView :  ICleanup
    {
        #region → Constructor    .

        /// <summary>
        /// Default Constructor.
        /// </summary>
        public ValuesView()
        {
            InitializeComponent();
        }

        #endregion Constructors

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
