#region → Usings   .
using citPOINT.PrefApp.Data.Web;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

#endregion

#region → History  .
/* Date         User            Change
 * 
 * 18.01.11     M.Wahab           → Creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion

namespace citPOINT.PrefApp.Common
{
    /// <summary>
    /// Issues Datamatching Accordion Control
    /// </summary>
    public partial class IssuesDatamatchingAccordion : StackPanel
    {

        #region → Fields         .


        /// <summary>
        ///Using a DependencyProperty as the backing store for ItemsSource. 
        ///This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable<Issue>), typeof(IssuesDatamatchingAccordion), new PropertyMetadata(new PropertyChangedCallback(OnChanging)));

        /// <summary>
        /// sing a DependencyProperty as the backing store for CurrentMessageSource. 
        /// </summary>
        public static readonly DependencyProperty CurrentMessageSourceProperty =
         DependencyProperty.Register("CurrentMessageSource", typeof(ConversationMessage), typeof(IssuesDatamatchingAccordion), new PropertyMetadata(new PropertyChangedCallback(OnChangingMessage)));


        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets the items source.
        /// </summary>
        /// <value>The items source.</value>
        public IEnumerable<Issue> ItemsSource
        {
            get { return (IEnumerable<Issue>)GetValue(ItemsSourceProperty); }
            set
            {
                SetValue(ItemsSourceProperty, value);
            }
        }



        /// <summary>
        /// Gets or sets the current message source.
        /// </summary>
        /// <value>The current message source.</value>
        public ConversationMessage CurrentMessageSource
        {
            get { return (ConversationMessage)GetValue(CurrentMessageSourceProperty); }
            set
            {
                SetValue(CurrentMessageSourceProperty, value);
            }
        }

        #endregion

        #region → events         .

        #endregion

        #region → Event Handlers .
        /// <summary>
        /// Called when [changing].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnChanging(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            BuildControls((sender as IssuesDatamatchingAccordion));
        }



        private static void OnChangingMessage(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            BuildControls((sender as IssuesDatamatchingAccordion));
        }



        #endregion

        #region → Methods        .

        #region → Public         .

        /// <summary>
        /// Updates the drag drop area.
        /// </summary>
        public void UpdateDragDropArea()
        {
            BuildControls(this);
        }

        /// <summary>
        /// Builds the controls.
        /// </summary>
        /// <param name="IssuesAccordion">The pref app accordion.</param>
        public static void BuildControls(IssuesDatamatchingAccordion IssuesAccordion)
        {
            //(IssuesAccordion as IssuesDatamatchingAccordion).Background = new SolidColorBrush(Colors.White);
            IssuesAccordion.Children.Clear();

            //clear all drop targets
            while (PrefAppConfigurations.MainDragDropManager != null && PrefAppConfigurations.MainDragDropManager.DestinationTargets.Where(s => s.GetType().Equals(typeof(IssuesDatamatchingItem))).Count() > 0)
            {
                var destinationTarget = PrefAppConfigurations.MainDragDropManager.DestinationTargets.Where(s => s.GetType().Equals(typeof(IssuesDatamatchingItem))).FirstOrDefault();
                PrefAppConfigurations.MainDragDropManager.DestinationTargets.Remove(destinationTarget);
            }

            int counter = 0;

            if (IssuesAccordion.ItemsSource != null && IssuesAccordion.CurrentMessageSource != null)
            {
                foreach (var item in IssuesAccordion.ItemsSource)
                {
                    IssuesDatamatchingItem issuesDatamatchingItem = new IssuesDatamatchingItem();
                    issuesDatamatchingItem.MessageValue = IssuesAccordion.CurrentMessageSource;
                    issuesDatamatchingItem.IssueValue = (item as Issue);
                    issuesDatamatchingItem.ShowHeader = counter == 0;
                    counter += 1;
                    IssuesAccordion.Children.Add(issuesDatamatchingItem);
                }
            }
        }
        #endregion

        #endregion
    }
}
