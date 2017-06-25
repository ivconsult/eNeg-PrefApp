

#region → Usings   .
using Telerik.Windows.Controls;
using System.Windows;
using citPOINT.PrefApp.Data.Web;
#endregion

#region → History  .

/* Date         User            change
 * 
 * 27.09.10     M.Wahab         * creation
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
    /// A Custom Selector that select the proper edit template for Preference Set tree node 
    /// </summary>
    public class PreferenceSetCustomSelector : DataTemplateSelector
    {
        private DataTemplate mPreferenceSetTemplate;

        /// <summary>
        /// Select the suitable template according to the selected node type
        /// </summary>
        /// <param name="item">item</param>
        /// <param name="container">container</param>
        /// <returns>DataTemplate</returns>
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is PreferenceSet)
                return this.mPreferenceSetTemplate;
            return null;
        }


        /// <summary>
        ///  Gets or sets the PreferenceSetTemplate
        /// </summary>
        public DataTemplate PreferenceSetTemplate
        {
            get
            {
                return mPreferenceSetTemplate;
            }
            set
            {
                mPreferenceSetTemplate = value;
            }
        }

      
    }
}
