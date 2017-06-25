#region → Usings   .
using System;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using citPOINT.PrefApp.Data.Web;
using System.Windows.Controls;
using System.Collections;
using System.Collections.Generic;
using GalaSoft.MvvmLight;
using citPOINT.PrefApp.ViewModel;
using System.Windows.Media;
using citPOINT.PrefApp.Common;
#endregion

#region → History  .

/* Date         User          Change
 * 
 * 27.03.12    M.Wahab         Creation
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
    /// Represents the converter that converts Boolean values to and from Visibility enumeration values. 
    /// </summary>
    public sealed class IssueTypeToControlConverter : IValueConverter
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
        /// <returns>Either visible or collapsed</returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null && (value as IEnumerable<IIssueDetailsViewModel>) != null)
            {
                IIssueDetailsViewModel vmbase = (value as IEnumerable<IIssueDetailsViewModel>).FirstOrDefault();

                if (vmbase != null)
                {
                    if (vmbase.ViewName == PrefAppViewTypes.NumericIssue)
                    {
                        return new NumericView(vmbase) { HorizontalAlignment= HorizontalAlignment.Stretch, HorizontalContentAlignment= HorizontalAlignment.Stretch };
                    }
                    else if (vmbase.ViewName == PrefAppViewTypes.OptionIssue)
                    {
                        return new OptionsIssuesValueView(vmbase) { HorizontalAlignment = HorizontalAlignment.Stretch, HorizontalContentAlignment = HorizontalAlignment.Stretch };
                    }
                }
            }

            return new TextBlock() { Text = "Failed to render ask the administrator", Foreground = new SolidColorBrush(Colors.Red) };
        }

        /// <summary>
        /// Convert from visibilty type to bool
        /// </summary>
        /// <param name="value">visibilty to convert</param>
        /// <param name="targetType">Value of target type</param>
        /// <param name="parameter">Parameters if found</param>
        /// <param name="culture">value of used Culture</param>
        /// <returns>bool</returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((value is Issue) && (((Visibility)value) == Visibility.Visible));
        }
        #endregion

        #endregion

        #endregion

    }
}
