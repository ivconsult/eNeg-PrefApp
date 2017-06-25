#region → Usings   .
using System.Windows.Controls;
using citPOINT.PrefApp.ViewModel;
using Telerik.Windows.Controls;
using System.Windows;
using System;

#endregion

#region → History  .

/* Date         User              Change
 * 
 * 07.11.10     M.Wahab       Creation
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
    /// Numeric Issue in Graph Mode View.
    /// </summary>
    public partial class NumericIssueGraphView : UserControl
    {
        #region → Fields         .

        double firstWidth = 0;
        double firstMargineLeft = 0;

        #endregion

        #region → Constructor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="NumericIssueGraphView"/> class.
        /// </summary>
        public NumericIssueGraphView( )
        {
             InitializeComponent();

            Dispatcher.BeginInvoke(() =>
            {
                UpdateSliderWidth();
            });
        }

        #endregion Constructor

        #region → Event Handlers .

        /// <summary>
        /// Handles the KeyDown event of the UserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs"/> instance containing the event data.</param>
        private void UserControl_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (!(sender as RadNumericUpDown).ShowButtons)
            {
                Dispatcher.BeginInvoke(() =>
                {
                    uxtxtUnit.Focus();
                });
                return;
            }

            if (e.Key == System.Windows.Input.Key.Enter && (sender as RadNumericUpDown) != null)
            {
                uxtxtUnit.Focus();
                (sender as RadNumericUpDown).Focus();
            }
        }

        /// <summary>
        /// Handles the ValueChanged event of the uxtxtOptimumValueStart control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Windows.Controls.RadRangeBaseValueChangedEventArgs"/> instance containing the event data.</param>
        private void uxtxtOptimumValueStart_ValueChanged(object sender, RadRangeBaseValueChangedEventArgs e)
        {
            Dispatcher.BeginInvoke(() =>
            {
                UpdateSliderWidth();
            });

        }

        #endregion

        #region → Methods        .

        /// <summary>
        /// Updates the width of the slider.
        /// </summary>
        public void UpdateSliderWidth()
        {
            try
            {
                if (uxsldOptimumValuesRanges.IsEnabled && uxsldOptimumValuesRanges.ActualWidth > 0)
                {

                    if (firstWidth <= 0)
                        firstWidth = uxsldOptimumValuesRanges.ActualWidth;

                    if (firstMargineLeft <= 0)
                        firstMargineLeft = uxsldOptimumValuesRanges.Margin.Left;

                    #region → Minimum Operators  .

                    if (uxtxtMinValue.Value == uxtxtOptimumValueStart.Value)
                    {
                        double lastRange = (uxtxtMinValue.Value.Value - radChart.DefaultView.ChartArea.AxisX.ActualMinValue);
                        double pct = 1 - (lastRange / radChart.DefaultView.ChartArea.AxisX.ActualRange);
                        uxsldOptimumValuesRanges.Width = (pct * firstWidth) + 5d;
                        double left = firstMargineLeft + ((1 - pct) * firstWidth) - 5d;
                        uxsldOptimumValuesRanges.Margin = new Thickness(left, 5, 0, 5);
                        uxsldOptimumValuesRanges.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    }
                    #endregion

                    #region → Maximum Operators  .

                    else if (uxtxtMaxValue.Value == uxtxtOptimumValueEnd.Value)
                    {

                        double lastRange = (radChart.DefaultView.ChartArea.AxisX.ActualMaxValue - uxtxtMaxValue.Value.Value);
                        double pct = 1 - (lastRange / radChart.DefaultView.ChartArea.AxisX.ActualRange);
                        uxsldOptimumValuesRanges.Width = (pct * firstWidth) + 5d;
                        double left = firstMargineLeft + (uxtxtMinValue.Value.Value - radChart.DefaultView.ChartArea.AxisX.ActualMinValue);
                        uxsldOptimumValuesRanges.Margin = new Thickness(left, 5, 0, 5);
                        uxsldOptimumValuesRanges.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;

                    }

                    #endregion

                    #region → Optimum Operators  .

                    else
                    {
                        uxsldOptimumValuesRanges.Width = firstWidth;
                        uxsldOptimumValuesRanges.Margin = new Thickness(firstMargineLeft, 5, 0, 5);
                        uxsldOptimumValuesRanges.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    }

                    #endregion

                }
            }
            catch (Exception)
            { }
        }
        
        #endregion
    }
}