

#region → Usings   .
using System;
using System.Windows.Data;
using System.Collections.Generic;
using citPOINT.PrefApp.Data.Web;
using System.Linq;
#endregion

#region → History  .

/* Date         User              Change
 * 
 * 10.11.10     Yousra Reda       Creation
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
    /// Represents the converter that converts IssueTypeID to and from boolean values to use it to enable option in xaml.
    /// </summary>
    public class OrderPreferenceSetConverter : IValueConverter
    {
        #region → Methods        .

        #region Puplic

        #region Implment Interface IValueConverter

        /// <summary>
        /// Modifies the source data before passing it to the target for display in the UI.
        /// </summary>
        /// <param name="value">The source data being passed to the target.</param>
        /// <param name="targetType">The <see cref="T:System.Type"/> of data expected by the target dependency property.</param>
        /// <param name="parameter">An optional parameter to be used in the converter logic.</param>
        /// <param name="culture">The culture of the conversion.</param>
        /// <returns>
        /// The value to be passed to the target dependency property.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (value as IEnumerable<PreferenceSet>).OrderBy(s => s.DeletedOn);
        }

        /// <summary>
        /// Modifies the target data before passing it to the source object.  This method is called only in <see cref="F:System.Windows.Data.BindingMode.TwoWay"/> bindings.
        /// </summary>
        /// <param name="value">The target data being passed to the source.</param>
        /// <param name="targetType">The <see cref="T:System.Type"/> of data expected by the source object.</param>
        /// <param name="parameter">An optional parameter to be used in the converter logic.</param>
        /// <param name="culture">The culture of the conversion.</param>
        /// <returns>
        /// The value to be passed to the source object.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }

        #endregion

        #endregion

        #endregion
    }
}
