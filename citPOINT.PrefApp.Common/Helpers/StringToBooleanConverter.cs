

#region → Usings   .
using System;
using System.Windows;
using System.Windows.Data;
#endregion

#region → History  .

/* Date         User              Change
 * 
 * 04.06.11     M.Wahab           * Creation
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
    /// Represents the converter that converts string Valuesto and from boolean enumeration values. 
    /// </summary>
    public sealed class StringToBooleanConverter : IValueConverter
    {
        #region → Methods        .

        #region Puplic

        #region Implment Interface IValueConverter
        /// <summary>
        /// Convert from bool to visibilty type
        /// </summary>
        /// <param name="value">bool value to convert</param>
        /// <param name="targetType">Value of target type</param>
        /// <param name="parameter">Parameters if found</param>
        /// <param name="culture">value of used Culture</param>
        /// <returns>Either true or false</returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool flag = false;

            if (value != null && value is string)
            {
                if (value.ToString().ToLower() == "true")
                {
                    flag = true;
                }

            }
            return flag;
        }

        /// <summary>
        /// Convert from visibilty type to bool
        /// </summary>
        /// <param name="value">boolean to convert</param>
        /// <param name="targetType">Value of target type</param>
        /// <param name="parameter">Parameters if found</param>
        /// <param name="culture">value of used Culture</param>
        /// <returns>string</returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (bool)value ? "true" : "false";
        }
        #endregion

        #endregion

        #endregion

    }
}
