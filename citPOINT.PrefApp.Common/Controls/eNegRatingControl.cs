
#region → Usings   .
using System.ComponentModel;
using System.Windows;
using Telerik.Windows.Controls;

#endregion

#region → History  .
/* Date         User            Change
 * 
 * 08.11.10     Y.Mohamed       → Creation
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
    /// ☼ ☼ ☼ ☼ ☼ ☼ ☼ ☼ ☼ ☼ ☼ ☼
    /// ☼ eNeg Rating Control ☼
    /// ☼ ☼ ☼ ☼ ☼ ☼ ☼ ☼ ☼ ☼ ☼ ☼
    /// </summary>
    public class eNegRatingControl : RadRating, INotifyPropertyChanged
    {

        #region → Fields         .

        /// <summary>
        /// Using a DependencyProperty as the backing store for CalValue.
        /// This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty CalValueProperty =
            DependencyProperty.Register("CalValue", typeof(double), typeof(eNegRatingControl), new PropertyMetadata(new PropertyChangedCallback(OnChanging)));
        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets the cal value.
        /// </summary>
        /// <value>The cal value.</value>
        public double CalValue
        {
            get
            {
                if (Value != null)
                    return (double)this.Value * 100 / this.NumberOfItemsToGenerate;
                return 0.0;
            }
            set
            {
                this.SetValue(CalValueProperty, value);
                this.Value = value * this.NumberOfItemsToGenerate / 100;
                OnPropertyChanged("CalValue");
            }
        }
        #endregion

        #region → events         .
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region → Event Handlers .
        /// <summary>
        /// Called when [changing].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnChanging(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            eNegRatingControl eNegRatingControl = (sender as eNegRatingControl);
            eNegRatingControl.CalValue = (double)e.NewValue;
        }

        #endregion

        #region → Methods        .

        #region → Public         .

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="PropertyName">Name of the property.</param>
        public virtual void OnPropertyChanged(string PropertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
            }
        }

        #endregion

        #endregion

    }
}
