
#region → Usings   .
using System.ComponentModel.Composition;
using citPOINT.PrefApp.Common;
using citPOINT.PrefApp.ViewModel;
using GalaSoft.MvvmLight;
using citPOINT.PrefApp.Common.Helper;
#endregion

#region → History  .

/* Date         User          Change
 * 
 * 05.04.12    M.Wahab         Creation
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
    /// View Model Repository.
    /// Shared View Models forcing that all view models are intialized.
    /// </summary>
    public class ViewModelRepository:ICleanup
    {
        #region → Properties     .

        /// <summary>
        /// Gets or sets the preference sets view model.
        /// </summary>
        /// <value>The preference sets view model.</value>
        [Import(PrefAppViewModelTypes.PreferenceSetsViewModel)]
        public PreferenceSetsViewModel PreferenceSetsViewModel { get; set; }

        /// <summary>
        /// Gets or sets the data matching view model.
        /// </summary>
        /// <value>The data matching view model.</value>
        [Import(PrefAppViewModelTypes.DataMatchingViewModel)]
        public DataMatchingViewModel DataMatchingViewModel { get; set; }

        /// <summary>
        /// Gets or sets the report view model.
        /// </summary>
        /// <value>The report view model.</value>
        public ReportViewModel ReportViewModel { get; set; }

        /// <summary>
        /// Gets or sets the last instance.
        /// </summary>
        /// <value>The last instance.</value>
        public static ViewModelRepository LastInstance { get; set; }
        #endregion

        #region → Constructor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelRepository"/> class.
        /// </summary>
        [ImportingConstructor]
        public ViewModelRepository()
        {
            if (!GalaSoft.MvvmLight.ViewModelBase.IsInDesignModeStatic)
            {
                PreferenceAppModule.Container.SatisfyImportsOnce(this);

                this.ReportViewModel = this.DataMatchingViewModel.ReportVM;

                this.DataMatchingViewModel.PreferenceSetsVM = this.PreferenceSetsViewModel;

                LastInstance = this;
            }
        }

        #endregion

        #region → Methods        .

        /// <summary>
        /// Cleanups this instance.
        /// </summary>
        public void Cleanup()
        {
            this.ReportViewModel.Cleanup();

            this.DataMatchingViewModel.Cleanup();
            
            this.PreferenceSetsViewModel.Cleanup();
            
            Repository.Cleanup();
        }

        #endregion
    }
}
