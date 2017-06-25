

#region → Usings   .
using System;
using System.ComponentModel;
using System.Windows;
using citPOINT.PrefApp.Data.Web;
using Telerik.Windows.Controls;

#endregion

#region → History  .

/* 
 * Date                      User            Change
 * *********************************************
 * 7/5/2011 12:55:51 PM      mwahab         • creation
 * **********************************************
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
    /// Class for Pref App Rad Slider
    /// </summary>
    public class PrefAppNumericSlider : RadSlider
    {

        #region → Fields         .

        /// <summary>
        ///Using a Dependency Property as the backing store for Current Numeric Issue. 
        /// </summary>
        public static readonly DependencyProperty CurrentNumericIssueProperty =
            DependencyProperty.Register("CurrentNumericIssue", typeof(NumericIssue), typeof(PrefAppNumericSlider), new PropertyMetadata(new PropertyChangedCallback(OnChanging)));

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets the current numeric issue.
        /// </summary>
        /// <value>The current numeric issue.</value>
        public NumericIssue CurrentNumericIssue
        {
            get { return (NumericIssue)GetValue(CurrentNumericIssueProperty); }
            set
            {
                SetValue(CurrentNumericIssueProperty, value);

                if (this.CurrentNumericIssue != null)
                {
                    UpdateSlider();
                    this.CurrentNumericIssue.PropertyChanged += new PropertyChangedEventHandler(CurrentNumericIssue_PropertyChanged);
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance can change slider values.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance can change slider values; otherwise, <c>false</c>.
        /// </value>
        private bool CanChangeSliderValues
        {
            get
            {

                return this.CurrentNumericIssue != null &&
                       this.CurrentNumericIssue.MinimumValue <= this.CurrentNumericIssue.OptimumValueStart &&
                       this.CurrentNumericIssue.OptimumValueStart <= this.CurrentNumericIssue.OptimumValueEnd &&
                       this.CurrentNumericIssue.OptimumValueEnd <= this.CurrentNumericIssue.MaximumValue;
            }
        }


        /// <summary>
        /// Gets a value indicating whether this instance is for editing.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is for editing; otherwise, <c>false</c>.
        /// </value>
        private bool IsForEditing
        {
            get
            {

                return this.CanChangeSliderValues &&
                       this.CurrentNumericIssue.Issue != null &&
                       this.CurrentNumericIssue.Issue.PreferenceSet != null &&
                       this.CurrentNumericIssue.Issue.PreferenceSet.IsEditable;
            }
        }


        #endregion Properties

        #region → Constractor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="PrefAppNumericSlider"/> class.
        /// </summary>
        public PrefAppNumericSlider()
            : base()
        {
            this.SelectionRangeChanged += new RoutedPropertyChangedEventHandler<SelectionRangeChangedEventArgs>(Slider_SelectionRangeChanged);
        }

        #endregion

        #region → Event Handlers .

        /// <summary>
        /// Called when [changing].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnChanging(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            (sender as PrefAppNumericSlider).CurrentNumericIssue = (NumericIssue)e.NewValue;
        }

        /// <summary>
        /// Handles the PropertyChanged event of the CurrentNumericIssue control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void CurrentNumericIssue_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "MaximumValue" ||
                e.PropertyName == "MinimumValue" ||
                e.PropertyName == "OptimumValueStart" ||
                e.PropertyName == "OptimumValueEnd")
            {
                UpdateSlider();
            }
        }

        /// <summary>
        /// Handles the SelectionRangeChanged event of the Slider control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedPropertyChangedEventArgs&lt;Telerik.Windows.Controls.SelectionRangeChangedEventArgs&gt;"/> instance containing the event data.</param>
        private void Slider_SelectionRangeChanged(object sender, RoutedPropertyChangedEventArgs<SelectionRangeChangedEventArgs> e)
        {
            if (this.IsFocused) // to be sure that the changes done by the user not code.
                UpdateSource();
        }

        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Updates the source.
        /// For Updating from Slider to numeric target
        /// </summary>
        private void UpdateSource()
        {
            if (this.CurrentNumericIssue != null)
            {
                this.CurrentNumericIssue.MaximumValue = Math.Round((decimal)this.Maximum, 2);
                this.CurrentNumericIssue.MinimumValue = Math.Round((decimal)this.Minimum, 2);
                this.CurrentNumericIssue.OptimumValueStart = Math.Round((decimal)this.SelectionStart, 2);
                this.CurrentNumericIssue.OptimumValueEnd = Math.Round((decimal)this.SelectionEnd, 2);
            }
        }

        /// <summary>
        /// Update from numeric source to slider
        /// </summary>
        private void UpdateSlider()
        {
            this.IsEnabled = CanChangeSliderValues;


            if (this.IsEnabled) // Must be all values Right
            {
                this.Maximum = (double)this.CurrentNumericIssue.MaximumValue;
                this.Minimum = (double)this.CurrentNumericIssue.MinimumValue;
                this.SelectionStart = (double)this.CurrentNumericIssue.OptimumValueStart;
                this.SelectionEnd = (double)this.CurrentNumericIssue.OptimumValueEnd;
            }

            this.IsHitTestVisible = this.IsForEditing;
        }



        #endregion

        #endregion Methods

    }
}
