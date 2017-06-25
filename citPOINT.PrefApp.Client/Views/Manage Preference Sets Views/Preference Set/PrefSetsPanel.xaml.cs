
#region → Usings   .

using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using citPOINT.PrefApp.Common;
using GalaSoft.MvvmLight.Messaging;
using citPOINT.PrefApp.Data.Web;

#endregion

#region → History  .

/* Date         User              Change
 * 
 * 11.04.12     Yousra Reda       Creation
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
    /// View for the left side area contains the pref sets 
    /// </summary>
    public partial class PrefSetsPanel : UserControl
    {
        #region → Fields         .

        private RadTreeViewItem mOrignalPreferenceSetNode;

        bool mForceSelectTreeNode;

        #endregion Fields

        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="PrefSetsPanel"/> class.
        /// </summary>
        public PrefSetsPanel()
        {
            InitializeComponent();

            #region → Registration for needed messages in PrefAppMessanger   .
            PrefAppMessanger.EditPreferenceSetMessage.Register(this, OnChangeTreeNode);
            #endregion
        }

        #endregion

        #region → Event Handlers .

        #region → Control Events .

        /// <summary>
        /// Event handler fired when the context menu opened
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">RoutedEventArgs</param>
        private void contextMenu_Opened(object sender, System.Windows.RoutedEventArgs e)
        {
            var contextMenu = (sender as RadContextMenu);
            var treeViewItem = contextMenu.GetClickedElement<RadTreeViewItem>();

            if (treeViewItem == null)
            {
                contextMenu.IsOpen = false;
                return;
            }

            PrefAppConfigurations.CanContinueProcess(result =>
            {
                if (result == MessageBoxResult.Cancel)
                {
                    contextMenu.IsOpen = false;
                    return;
                }
                
                PrefAppMessanger.CancelChangesMessage.Send();

                switch (contextMenu.Name)
                {
                    case "uxContextMySets":
                        uxTreeMySets.SelectedItem = treeViewItem.Header;
                        break;
                    case "uxContextOrganizationSets":
                        uxTreeOrganizationSets.SelectedItem = treeViewItem.Header;
                        break;
                    case "uxContextSetStore":
                        uxTreeSetStore.SelectedItem = treeViewItem.Header;
                        break;
                }
            });

        }

        /// <summary>
        /// Handles the Selected event of the RadPanelBarItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Windows.RadRoutedEventArgs"/> instance containing the event data.</param>
        private void RadPanelBarItem_Selected(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            var barItem = (RadPanelBarItem)sender;

            if (barItem != null && barItem.IsMouseOver) //mean clicked by the user.
            {
                if (PrefAppConfigurations.CheckForCancelPreferenceSetChanges)
                {
                    e.Handled = true;
                }

                PrefAppConfigurations.CanContinueProcess(result =>
                {
                    if (result == MessageBoxResult.Cancel)
                    {
                        uxpnlMySets.IsSelected = true;
                        uxpnlMySets.IsExpanded = true;
                    }
                    else
                    {
                        PrefAppMessanger.CancelChangesMessage.Send();
                        PrefAppMessanger.FlippMessage.Send(PrefAppViewTypes.ForceAddPrefSetView);
                    }
                });
            }
        }

        /// <summary>
        /// Handles the PreviewSelectionChanged event of the uxTreeMySets control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Windows.Controls.SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void uxTreeMySets_PreviewSelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                var selectedNode = e.AddedItems[0];

                if (PrefAppConfigurations.CheckForCancelPreferenceSetChanges && !mForceSelectTreeNode)
                {
                    e.Handled = true;

                    PrefAppConfigurations.CanContinueProcess(result =>
                    {
                        if (result != MessageBoxResult.Cancel)
                        {
                            PrefAppMessanger.CancelChangesMessage.Send();

                            RadTreeViewItem radTreeNode = GetTreeNodeOfPrefSet(selectedNode as PreferenceSet);

                            radTreeNode.IsSelected = true;

                            radTreeNode.Focus();
                        }
                    });
                }
            }

        }

        #endregion

        #endregion Event Handlers

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Open Tree node in edit moe if we choose new Preference Set
        /// </summary>
        /// <param name="preferenceSetNode">Preference Set Node</param>
        private void OnChangeTreeNode(PreferenceSet preferenceSetNode)
        {
            if (preferenceSetNode != null)
            {
                Dispatcher.BeginInvoke(() =>
                {
                    mOrignalPreferenceSetNode = GetTreeNodeOfPrefSet(preferenceSetNode);
                    if (mOrignalPreferenceSetNode != null)
                    {
                        mForceSelectTreeNode = true;

                        mOrignalPreferenceSetNode.IsSelected = true;
                        mOrignalPreferenceSetNode.IsExpanded = true;
                        mOrignalPreferenceSetNode.Focus();
                        mForceSelectTreeNode = false;
                    }
                });
            }
        }



        /// <summary>
        /// Gets the tree node of pref set.
        /// </summary>
        /// <param name="preferenceSetNode">The preference set node.</param>
        /// <returns></returns>
        private RadTreeViewItem GetTreeNodeOfPrefSet(PreferenceSet preferenceSetNode)
        {
            var nodePath = string.Format("MainPreferenceSet : {0}\\PreferenceSet : {1}",
                                          preferenceSetNode.MainPreferenceSetID,
                                          preferenceSetNode.PreferenceSetID);

            if (preferenceSetNode.MainPreferenceSetID == PrefAppConstant.MainPreferenceSets.MySets)
            {
                ((RadPanelBarItem)uxpbAllTrees.Items[0]).IsSelected = true;
                ((RadPanelBarItem)uxpbAllTrees.Items[0]).IsExpanded = true;

                return uxTreeMySets.GetItemByPath(nodePath);
            }
            else if (preferenceSetNode.MainPreferenceSetID == PrefAppConstant.MainPreferenceSets.OrganizationSets)
            {
                ((RadPanelBarItem)uxpbAllTrees.Items[1]).IsSelected = true;
                ((RadPanelBarItem)uxpbAllTrees.Items[1]).IsExpanded = true;

                return uxTreeOrganizationSets.GetItemByPath(nodePath);
            }
            else
            {
                ((RadPanelBarItem)uxpbAllTrees.Items[2]).IsSelected = true;
                ((RadPanelBarItem)uxpbAllTrees.Items[2]).IsExpanded = true;

                return uxTreeSetStore.GetItemByPath(nodePath);
            }
        }

        #endregion Private

        #region → Public         .

        /// <summary>
        /// ICleanup interface implementation
        /// </summary>
        public void Cleanup()
        {
            // call Cleanup on its ViewModel
            //this.ViewModel.Cleanup();

            // Cleanup itself
            Messenger.Default.Unregister(this);
        }

        #endregion Public

        #endregion Methods


    }
}
