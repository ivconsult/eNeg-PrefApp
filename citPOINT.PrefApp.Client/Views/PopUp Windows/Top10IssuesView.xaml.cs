#region → Usings   .
using System.ComponentModel.Composition;
using System.Windows.Controls;
using citPOINT.PrefApp.Common;
using citPOINT.PrefApp.ViewModel;
using GalaSoft.MvvmLight;
using System.Windows;
#endregion

#region → History  .

/* 
 * Date         User            Change
 * *********************************************
 * 3/29/2012 11:20:00 AM      mwahab         • creation
 * **********************************************
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
    /// Top 1 0Issues View.
    /// </summary>
    public partial class Top10IssuesView : UserControl, ICleanup
    {
        #region → Properties     .

        #region Using MEF to import Top 10 Issues View Model

        /// <summary>
        /// Set View Model By MEF
        /// </summary>
        [Import(PrefAppViewModelTypes.Top10IssuesViewModel)]
        public Top10IssuesViewModel ViewModel
        {
            get
            {
                return (DataContext as Top10IssuesViewModel);
            }

            set
            {
                DataContext = value;
            }
        }

        #endregion
        
        #endregion

        #region → Constructor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="Top10IssuesView"/> class.
        /// </summary>
        public Top10IssuesView(bool isCurrentPrefernceEditable)
        {
            InitializeComponent();

            #region → Use MEF To load the View Model             .

            if (!ViewModelBase.IsInDesignModeStatic)
            {
                PreferenceAppModule.Container.SatisfyImportsOnce(this);
            }

            if (!isCurrentPrefernceEditable)
            {
                uxIssueStatisticalsGridView.Columns["SelectIssue"].IsVisible = false;
                uxcmdDragSelectedIssues.Visibility =  System.Windows.Visibility.Collapsed;
            }
            

            #endregion Use MEF To load the View Model
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
