
#region → Usings   .
using System.ComponentModel.Composition;
using System.Windows.Controls;
using citPOINT.PrefApp.Common;
using citPOINT.PrefApp.ViewModel;
using GalaSoft.MvvmLight;
#endregion

#region → History  .

/* Date         User              Change
 * 
 * 20.02.11     M.Wahab       Creation
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
    /// External Report View
    /// </summary>
    public partial class MainExternalReportView : UserControl, ICleanup
    {
        #region → Properties     .

        #region Using MEF to import External Report ViewModel

        /// <summary>
        /// Set View Model By MEF
        /// </summary>
        [Import(PrefAppViewModelTypes.ExternalReportViewModel)]
        public ExternalReportViewModel ViewModel
        {
            get
            {
                return (DataContext as ExternalReportViewModel);
            }
            set
            {
                DataContext = value;
            }
        }

        #endregion Using MEF to import External Report ViewModel

        #endregion

        #region → Constructor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="MainExternalReportView"/> class.
        /// </summary>
        public MainExternalReportView()
        {
            InitializeComponent();

            #region → Use MEF To load the View Model             .

            if (!ViewModelBase.IsInDesignModeStatic)
            {
                PreferenceAppModule.Container.SatisfyImportsOnce(this);
            }

            PrefAppMessanger.FlippMessage.Register(this, OnFilppMessage);

            #endregion Use MEF To load the View Model
        }

        private void OnFilppMessage(string viewName)
        {
            if (viewName == PrefAppViewTypes.ReportView)
            {
                this.uxMainContentView.Content = new ExternalReportView();
            }
            else if (viewName == PrefAppViewTypes.NotificationView)
            {
                this.uxMainContentView.Content = new ReportMessages();
            }
        }

        #endregion

        #region → Methods        .

        /// <summary>
        /// Cleanups this instance.
        /// </summary>
        public void Cleanup()
        {
            this.ViewModel.Cleanup();
        }

        #endregion

    }
}
