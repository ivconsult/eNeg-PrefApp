#region → Usings   .

using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using citPOINT.eNeg.Apps.Common.Interfaces;
using citPOINT.PrefApp.Common;
using citPOINT.PrefApp.Data.Web;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using System.Windows;

#endregion

#region → History  .

/* Date         User              Change
 * 
 * 21.03.12     M.Wahab       Creation
 */

# endregion History

#region → ToDos    .
/*
 * Date         set by User     Description
 * 
 * 
*/
# endregion ToDos

namespace citPOINT.PrefApp.Client
{
    /// <summary>
    /// Preference App Module.
    /// </summary>
    [ModuleExport(typeof(PreferenceAppModule))]
    public class PreferenceAppModule : IModule
    {
        #region → Fields         .

        private readonly IRegionManager regionManager;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets the container.
        /// </summary>
        /// <value>The container.</value>
        public static CompositionContainer Container { get; set; }

        #endregion

        #region → Constructor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="PreferenceAppModule"/> class.
        /// </summary>
        /// <param name="regionManager">The region manager.</param>
        /// <param name="MainPlatformInfo">The main platform info.</param>
        [ImportingConstructor()]
        public PreferenceAppModule(IRegionManager regionManager, IMainPlatformInfo MainPlatformInfo)
        {
            this.regionManager = regionManager;
                        
            PrefAppConfigurations.MainPlatformInfo = MainPlatformInfo;

            PrefAppConfigurations.ActionTypeParameter = PrefAppConfigurations.ActionTypes.Report.ToString();

            this.IntializeContainer();
        }

        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Intializes the container.
        /// </summary>
        private void IntializeContainer()
        {
            //An aggregate catalog that combines multiple catalogs
            var catalog = new AggregateCatalog();

            //Adds all the parts found in the same assembly as the Program class
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(App).Assembly));

            catalog.Catalogs.Add(new AssemblyCatalog(typeof(PrefAppConfigurations).Assembly));

            catalog.Catalogs.Add(new AssemblyCatalog(typeof(ViewModel.PreferenceSetsViewModel).Assembly));

            catalog.Catalogs.Add(new AssemblyCatalog(typeof(Model.PreferenceSetsModel).Assembly));

            catalog.Catalogs.Add(new AssemblyCatalog(typeof(Data.MapperTable).Assembly));

            catalog.Catalogs.Add(new AssemblyCatalog(typeof(PreferenceSetNeg).Assembly));

            //Create the CompositionContainer with the parts in the catalog
            Container = new CompositionContainer(catalog);
        }

        #endregion

        #region → Public         .

        /// <summary>
        /// Notifies the module that it has be initialized.
        /// </summary>
        public void Initialize()
        {
            try
            {
                regionManager.RegisterViewWithRegion
                    (PrefAppConfigurations.AppName.Replace(" ", "") + "Region",
                     typeof(MainPreferenceAppView));
            }
            catch (System.Exception ex)
            {
                PrefAppConfigurations.MainPlatformInfo.HandleException.HandleException(ex, PrefAppConfigurations.AppName);
            }
        }

        #endregion

        #endregion
    
    }
}
