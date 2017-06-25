#region → Usings   .
using System;
using System.Windows.Data;
#endregion

#region → History  .

/* Date         User              Change
 * 
 * 13.09.10     Yousra Reda       Creation
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
    /// Two way IValueConverter that lets you bind the inverse of a boolean property
    /// to a dependency property
    /// </summary>
    public sealed class NotOperatorValueConverter : IValueConverter
    {

        #region → Methods        .

        #region Puplic

        #region Implment Interface IValueConverter

        /// <summary>
        /// Reverse the bool value
        /// </summary>
        /// <param name="value">bool value to convert</param>
        /// <param name="targetType">Value of target type</param>
        /// <param name="parameter">Parameters if found</param>
        /// <param name="culture">value of used Culture</param>
        /// <returns>if value is true return false and vice versa</returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return !((bool)value);
        }

        /// <summary>
        /// Reverse the bool value
        /// </summary>
        /// <param name="value">bool value to convert</param>
        /// <param name="targetType">Value of target type</param>
        /// <param name="parameter">Parameters if found</param>
        /// <param name="culture">value of used Culture</param>
        /// <returns>if value is true return false and vice versa</returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return !((bool)value);
        }

        #endregion

        #endregion

        #endregion
    }
}
