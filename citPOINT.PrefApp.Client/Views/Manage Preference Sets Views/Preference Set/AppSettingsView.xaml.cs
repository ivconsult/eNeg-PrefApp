#region → Usings   .
using citPOINT.PrefApp.Common;
using citPOINT.PrefApp.Data;
using citPOINT.PrefApp.Data.Web;
using citPOINT.PrefApp.ViewModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel.DomainServices.Client;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Telerik.Windows;
using Telerik.Windows.Controls;
using System.Collections.Generic;
using citPOINT.PrefApp.Model;
using citPOINT.PrefApp.Client;

#endregion

#region → History  .

/* Date         User              Change
 * 
 * 26.09.10     M.Wahab       Creation
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
    /// Used for Manage Preference Sets View
    /// </summary>
    public partial class AppSettingsView : UserControl, ICleanup
    {
        #region → Fields         .

        private PreferenceSetSettingsView mAddNewView;

        private RadTreeViewItem mOrignalPreferenceSetNode;

        #endregion Fields

        #region → Properties     .

        #region Using MEF to import Preference Sets ViewModel

        /// <summary>
        /// Set View Model By MEF
        /// </summary>
        public PreferenceSetsViewModel ViewModel
        {
            get
            {
                return (DataContext as PreferenceSetsViewModel);
            }
            set
            {
                DataContext = value;
            }
        }

        #endregion Using MEF to import Preference Sets ViewModel

        #region Views

        /// <summary>
        /// Get Add New Preference Set View
        /// </summary>
        private PreferenceSetSettingsView AddNewView
        {
            get
            {
                if (mAddNewView == null)
                {
                    mAddNewView = new PreferenceSetSettingsView();
                }
                return mAddNewView;
            }
        }

        #endregion  Views

        /// <summary>
        /// Gets or sets the last instance.
        /// used in case of Drag and Drop.
        /// </summary>
        /// <value>The last instance.</value>
        // public static AppSettingsView LastInstance { get; set; }

        #endregion Properties

        #region → Constructors   .

        /// <summary>
        /// Default Constructor
        /// </summary>
        public AppSettingsView(ViewModelRepository viewModelRepository)
        {
            InitializeComponent();

            //Setting the Last Instance to this.used in case of drag and drop.
            //LastInstance = this;

            #region → Use MEF To load the View Model                         .

            if (!GalaSoft.MvvmLight.ViewModelBase.IsInDesignModeStatic)
            {
                this.ViewModel = viewModelRepository.PreferenceSetsViewModel;
            }

            #endregion Use MEF To load the View Model

            #region → Registration for needed messages in PrefAppMessanger   .
            //PrefAppMessanger.EditPreferenceSetMessage.Register(this, OnChangeTreeNode);
            PrefAppMessanger.FlippMessage.Register(this, onFlippingMessage);
            PrefAppMessanger.FlippMessage.Register(this, onFlippingByPageName);
            PrefAppMessanger.NewPopUp.Register(this, OnAddNewPopUp);
            #endregion

            SetGolobalSize();
        }

        #endregion Constructors

        #region → Event Handlers .

        #endregion Event Handlers

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Called when [add new pop up].
        /// </summary>
        /// <param name="DragedValue">The draged value.</param>
        private void OnAddNewPopUp(string DragedValue)
        {
            if (PrefAppMessanger.NewPopUp.PopUpType == PrefAppMessanger.PopUpType.PublishToSetStore.ToString())
            {
                #region Show PopUp window to define a confirm on publishing PrefSet to Set Store
                {
                    PopUpWindow confirmPublishToStore = new PopUpWindow(DragedValue);
                    confirmPublishToStore.DataContext = this.DataContext;
                    confirmPublishToStore.Content = new PublishToSetStorePopUp();
                    confirmPublishToStore.WindowStartupLocation = Telerik.Windows.Controls.WindowStartupLocation.CenterScreen;
                    confirmPublishToStore.ShowDialog();
                }
                #endregion
            }
            else if (PrefAppMessanger.NewPopUp.PopUpType == PrefAppMessanger.PopUpType.PublishToMySets.ToString())
            {
                #region Show PopUp window to define a confirm on copying PrefSet to user sets
                {
                    PopUpWindow confirmPublishToMySets = new PopUpWindow(DragedValue);
                    confirmPublishToMySets.DataContext = this.DataContext;
                    confirmPublishToMySets.Content = new PublishToMySetsPopUp();
                    confirmPublishToMySets.WindowStartupLocation = Telerik.Windows.Controls.WindowStartupLocation.CenterScreen;
                    confirmPublishToMySets.ShowDialog();
                }
                #endregion
            }
            else if (PrefAppMessanger.NewPopUp.PopUpType == PrefAppMessanger.PopUpType.PublishToOrganization.ToString())
            {
                #region Show PopUp window to define a confirm on publish PrefSet to one or more organization
                {
                    PopUpWindow confirmPublishToOrgaSets = new PopUpWindow(DragedValue);
                    confirmPublishToOrgaSets.DataContext = this.DataContext;
                    confirmPublishToOrgaSets.Content = new PublishToOrganizationPopUp();
                    confirmPublishToOrgaSets.WindowStartupLocation = Telerik.Windows.Controls.WindowStartupLocation.CenterScreen;
                    confirmPublishToOrgaSets.ShowDialog();
                }
                #endregion
            }

            else if (PrefAppMessanger.NewPopUp.PopUpType == PrefAppMessanger.PopUpType.PublishToOrganizationAndReplace.ToString())
            {
                #region Show PopUp window to define a confirm on publish PrefSet to one or more organization
                {
                    PopUpWindow confirmPublishToOrgaSets = new PopUpWindow(DragedValue);
                    confirmPublishToOrgaSets.DataContext = this.DataContext;
                    confirmPublishToOrgaSets.Content = new ReplacePublishedSetPopUp();
                    confirmPublishToOrgaSets.WindowStartupLocation = Telerik.Windows.Controls.WindowStartupLocation.CenterScreen;
                    confirmPublishToOrgaSets.ShowDialog();
                }
                #endregion
            }
        }

        /// <summary>
        /// Ons the name of the flipping by page.
        /// </summary>
        /// <param name="PageName">Name of the page.</param>
        private void onFlippingByPageName(string PageName)
        {
            #region Swich on Curent screen Name to change the view according to that
            switch (PageName)
            {
                case PrefAppViewTypes.AddPrefSetView:
                    OpenAddPrefSetView();
                    break;

                case PrefAppViewTypes.ForceAddPrefSetView:
                    OpenForceAddPrefSetView();
                    break;

                case PrefAppViewTypes.IssuesView:
                    OpenIssuesView(false);
                    break;

                case PrefAppViewTypes.ClearSubWindows:
                    CleanUpAllSubWindows();
                    break;

                case PrefAppViewTypes.AddOrRenamePreferenceSetViews:
                    string header = (this.ViewModel.CurrentPreferenceSet.EntityState == EntityState.New ? "Add " : "Rename ") + "Preference set";

                    PopUpWindow confirmPublishToStore = new PopUpWindow(header);
                    confirmPublishToStore.DataContext = this.DataContext;
                    confirmPublishToStore.Content = new AddOrRenamePrefSetPopUp();
                    confirmPublishToStore.WindowStartupLocation = Telerik.Windows.Controls.WindowStartupLocation.CenterScreen;
                    confirmPublishToStore.ShowDialog();
                    break;
            }
            #endregion
        }

        /// <summary>
        /// Ons the flipping message.
        /// </summary>
        /// <param name="entity">The entity.</param>
        private void onFlippingMessage(Entity entity)
        {
            if (entity is Issue)
            {
                PrefAppMessanger.EditPreferenceSetMessage.Send((entity as Issue).PreferenceSet);

                Dispatcher.BeginInvoke(() =>
                {
                    OpenValuesView((entity as Issue));
                });
            }
        }

        /// <summary>
        /// Opens the add pref set view.
        /// </summary>
        private void OpenAddPrefSetView()
        {
            if (!PrefAppConfigurations.IsNewIssuePending)
            {
                this.uxcntMainContent.Content = this.AddNewView;
                //this.ViewModel.CurrentPreferenceSet = null;
            }
        }

        /// <summary>
        /// Opens the force add pref set view.
        /// </summary>
        private void OpenForceAddPrefSetView()
        {
            PrefAppConfigurations.IsNewIssuePending = false;

            this.ViewModel.RejectChanges();

            this.ViewModel.SelectedItem = null;

            this.ViewModel.CurrentPreferenceSet = null;

            this.uxcntMainContent.Content = this.AddNewView;
        }

        /// <summary>
        /// Opens the values view.
        /// </summary>
        /// <param name="issue">The issue.</param>
        private void OpenValuesView(Issue issue)
        {
            PrefAppConfigurations.MailNegotiationName = string.Empty;

            this.ViewModel.RejectChanges();

            this.ViewModel.IssuesVM.RefreshIssuesSource();

            OpenIssuesView(false);

            if (issue != null)
            {
                Dispatcher.BeginInvoke(() =>
                {
                    var issueDetailsVM = this.ViewModel
                                             .IssuesVM
                                             .IssuesNumericOptionsOnlySource
                                             .Where(s => s.CurrentIssue.IssueID == issue.IssueID)
                                             .FirstOrDefault();

                    if (issueDetailsVM != null)
                    {
                        issueDetailsVM.IsExpanded = true;
                    }

                    PrefAppMessanger.FlippMessage.Send(PrefAppViewTypes.InnerValuesView);
                });
            }
        }

        /// <summary>
        /// Opens the issues view.
        /// </summary>
        /// <param name="AddNewIssue">if set to <c>true</c> [add new issue].</param>
        private void OpenIssuesView(bool AddNewIssue)
        {
            this.uxcntMainContent.Content = new EditPreferenceSetView(this.ViewModel.IssuesVM);
        }

        /// <summary>
        /// Sets the size of the golobal.
        /// </summary>
        private void SetGolobalSize()
        {
            //PrefAppConfigurations.AdjustDragDropPoints = new Point(10, 30); 
        }

        /// <summary>
        /// Cleans up all sub windows.
        /// </summary>
        private void CleanUpAllSubWindows()
        {
            #region To Clean Up from sub windows

            foreach (var XPage in App.OpenedSubWindows)
                if ((XPage as ICleanup) != null)
                    (XPage as ICleanup).Cleanup();
            App.OpenedSubWindows.Clear();
            #endregion
        }

        #endregion Private

        #region → Public         .

        /// <summary>
        /// ICleanup interface implementation
        /// </summary>
        public void Cleanup()
        {
            // Cleanup itself
            Messenger.Default.Unregister(this);
        }

        #endregion Public

        #endregion Methods
    }
}
