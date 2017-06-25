
#region → Usings   .
using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using citPOINT.PrefApp.Common;
using citPOINT.PrefApp.Data.Web;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Telerik.Windows.Controls.Charting;
using System.Collections.Generic;

#endregion

#region → History  .

/* 
 * Date         User            Change
 * *********************************************
 * 7/6/2011 10:42:04 AM      mwahab         • creation
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

namespace citPOINT.PrefApp.ViewModel
{
    /// <summary>
    /// View Model for editing Numeric. 
    /// </summary>
    public class NumericViewModel : ViewModelBase, IIssueDetailsViewModel
    {
        #region → Fields         .

        private Issue mCurrentIssue;
        private NumericIssue mCurrentNumericIssue;
        private DataSeries mChartValues;

        private bool mIsMinimumOperatorsEnabled = false;
        private bool mIsMaximumOperatorsEnabled = false;

        private bool mShowGraphModeView;
        private bool mIsExpanded;

        private RelayCommand mPlotChartCommand;
        private RelayCommand mGoToNumericTextModeCommand;
        private RelayCommand mGoToNumericGraphicModeCommand;

        //Used to unit test PlotChartCmmand to be sure that command executed successfully
        public bool IsPlotChartCommandDone = false;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets a value indicating whether this instance is expanded.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is expanded; otherwise, <c>false</c>.
        /// </value>
        public bool IsExpanded
        {
            get
            {
                return mIsExpanded;
            }
            set
            {
                mIsExpanded = value;

                this.RaisePropertyChanged("IsExpanded");
            }
        }

        /// <summary>
        /// Gets the name of the view.
        /// </summary>
        /// <value>The name of the view.</value>
        public string ViewName
        {
            get { return PrefAppViewTypes.NumericIssue; }
        }

        /// <summary>
        /// Gets the first view model.
        /// </summary>
        /// <value>The first view model.</value>
        public IIssueDetailsViewModel FirstViewModel
        {
            get { return this; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show graph mode view].
        /// </summary>
        /// <value><c>true</c> if [show graph mode view]; otherwise, <c>false</c>.</value>
        public bool ShowGraphModeView
        {
            get
            {
                return mShowGraphModeView;
            }
            set
            {
                mShowGraphModeView = value;
                this.RaisePropertyChanged("ShowGraphModeView");
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is all valid.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is all valid; otherwise, <c>false</c>.
        /// </value>
        public bool IsAllValid
        {
            get { return IsAllNumericValuesValid(); }
        }

        /// <summary>
        /// Gets the view model.
        /// Current Insatnce wrapped in list form for Binding
        /// </summary>
        /// <value>The view model.</value>
        public IEnumerable<IIssueDetailsViewModel> ViewModel
        {
            get
            {
                return new List<IIssueDetailsViewModel> { this }.AsEnumerable();
            }
        }

        /// <summary>
        /// Gets or sets the current numeric issue.
        /// </summary>
        /// <value>The current numeric issue.</value>
        public NumericIssue CurrentNumericIssue
        {
            get
            {
                return mCurrentNumericIssue;
            }
            private set
            {
                if (mCurrentNumericIssue != value)
                {
                    mCurrentNumericIssue = value;

                    this.RaisePropertyChanged("CurrentNumericIssue");

                    this.SetMaxMinEnableState();
                }
            }
        }

        /// <summary>
        /// Gets or sets the current Issue
        /// </summary>
        public Issue CurrentIssue
        {
            get { return mCurrentIssue; }
            private set
            {
                if (mCurrentIssue != value || true)
                {
                    mCurrentIssue = value;

                    this.RaisePropertyChanged("CurrentIssue");

                    if (mCurrentIssue != null)
                    {
                        this.CurrentNumericIssue = value.NumericIssues.FirstOrDefault();

                        this.RaisePropertyChanged("CurrentNumericIssue");
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is minimum operators enabled.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is minimum operators enabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsMinimumOperatorsEnabled
        {
            get
            {
                return mIsMinimumOperatorsEnabled;
            }
            set
            {
                mIsMinimumOperatorsEnabled = value;
                this.RaisePropertyChanged("IsMinimumOperatorsEnabled");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is maximum operators enabled.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is maximum operators enabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsMaximumOperatorsEnabled
        {
            get
            {
                return mIsMaximumOperatorsEnabled;
            }
            set
            {
                mIsMaximumOperatorsEnabled = value;
                this.RaisePropertyChanged("IsMaximumOperatorsEnabled");
            }
        }

        /// <summary>
        /// Gets the chart values.
        /// </summary>
        /// <value>The chart values.</value>
        public DataSeries ChartValues
        {
            get
            {
                if (mChartValues == null)
                    mChartValues = new DataSeries();

                if (this.CurrentNumericIssue != null)
                {
                    if (this.CurrentNumericIssue.MinimumValue <= this.CurrentNumericIssue.MaximumValue &&
                        this.CurrentNumericIssue.OptimumValueStart <= this.CurrentNumericIssue.OptimumValueEnd &&
                        (this.CurrentNumericIssue.OptimumValueStart >= this.CurrentNumericIssue.MinimumValue && this.CurrentNumericIssue.OptimumValueStart <= this.CurrentNumericIssue.MaximumValue))
                    {
                        mChartValues = new DataSeries();
                        mChartValues.LegendLabel = "Issue";

                        mChartValues.Definition = new LineSeriesDefinition();

                        #region → Collapse Labels + Marks                           .
                        ISeriesDefinition seriesDefinition;
                        seriesDefinition = mChartValues.Definition as ISeriesDefinition;
                        seriesDefinition.Appearance.PointMark.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Transparent);
                        seriesDefinition.Appearance.PointMark.Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Transparent);
                        seriesDefinition.ShowItemLabels = false;
                        #endregion

                        #region → In case that it is related to Optimum = Min Value .

                        if (this.CurrentNumericIssue.OptimumValueStart == this.CurrentNumericIssue.MinimumValue)
                        {
                            if (this.CurrentNumericIssue.MinOperatorEqual)
                            {
                                decimal NewPointDiff = (this.CurrentNumericIssue.MaximumValue - this.CurrentNumericIssue.MinimumValue) / (decimal)10.00;

                                #region For Drawing Better Arrows

                                mChartValues.Add(new DataPoint() { YValue = 103, XValue = (double)(this.CurrentNumericIssue.MinimumValue - NewPointDiff + (NewPointDiff / (decimal)10.00)), Label = "↑↑" });
                                mChartValues.Add(new DataPoint() { YValue = 100, XValue = (double)(this.CurrentNumericIssue.MinimumValue - NewPointDiff), Label = "↑↑" });
                                mChartValues.Add(new DataPoint() { YValue = 97, XValue = (double)(this.CurrentNumericIssue.MinimumValue - NewPointDiff + (NewPointDiff / (decimal)10.00)), Label = "↑↑" });

                                #endregion

                                mChartValues.Add(new DataPoint() { YValue = 100, XValue = (double)(this.CurrentNumericIssue.MinimumValue - NewPointDiff), Label = "←" });
                                mChartValues.Add(new DataPoint() { YValue = 100, XValue = (double)(this.CurrentNumericIssue.MinimumValue), Label = "Min" });
                                //in case if the start not equal the End Optimim
                                if (this.CurrentNumericIssue.OptimumValueStart != this.CurrentNumericIssue.OptimumValueEnd)
                                {
                                    mChartValues.Add(new DataPoint() { YValue = 100, XValue = (double)(this.CurrentNumericIssue.OptimumValueEnd), Label = "Opt.End" });
                                }
                                mChartValues.Add(new DataPoint() { YValue = 0, XValue = (double)(this.CurrentNumericIssue.MaximumValue), Label = "Max" });
                            }

                            if (this.CurrentNumericIssue.MinOperatorWorse)
                            {
                                decimal NewPointDiff = (this.CurrentNumericIssue.MaximumValue - this.CurrentNumericIssue.MinimumValue) / (decimal)10.00;
                                mChartValues.Add(new DataPoint() { YValue = 0, XValue = (double)(this.CurrentNumericIssue.MinimumValue - NewPointDiff) });
                                mChartValues.Add(new DataPoint() { YValue = 0, XValue = (double)(this.CurrentNumericIssue.MinimumValue - (decimal)0.01) });
                                mChartValues.Add(new DataPoint() { YValue = 100, XValue = (double)(this.CurrentNumericIssue.MinimumValue), Label = "Min" });

                                //in case if the start not equal the End Optimim
                                if (this.CurrentNumericIssue.OptimumValueStart != this.CurrentNumericIssue.OptimumValueEnd)
                                {
                                    mChartValues.Add(new DataPoint() { YValue = 100, XValue = (double)(this.CurrentNumericIssue.OptimumValueEnd), Label = "Opt.End" });
                                }
                                mChartValues.Add(new DataPoint() { YValue = 0, XValue = (double)(this.CurrentNumericIssue.MaximumValue), Label = "Max" });
                            }


                            if (this.CurrentNumericIssue.MinOperatorBetter)
                            {
                                decimal NewPointDiff = (this.CurrentNumericIssue.MaximumValue - this.CurrentNumericIssue.MinimumValue) / (decimal)10.00;

                                #region For Drawing Better Arrows
                                mChartValues.Add(new DataPoint() { YValue = 111, XValue = (double)(this.CurrentNumericIssue.MinimumValue - NewPointDiff + (NewPointDiff / (decimal)10.00)), Label = "↑↑" });
                                mChartValues.Add(new DataPoint() { YValue = 110, XValue = (double)(this.CurrentNumericIssue.MinimumValue - NewPointDiff), Label = "↑↑" });
                                mChartValues.Add(new DataPoint() { YValue = 107, XValue = (double)(this.CurrentNumericIssue.MinimumValue - NewPointDiff + (NewPointDiff / (decimal)15.00)), Label = "↑↑" });
                                #endregion

                                mChartValues.Add(new DataPoint() { YValue = 110, XValue = (double)(this.CurrentNumericIssue.MinimumValue - NewPointDiff), Label = "↑↑" });
                                mChartValues.Add(new DataPoint() { YValue = 100, XValue = (double)(this.CurrentNumericIssue.MinimumValue), Label = "Min" });

                                //in case if the start not equal the End Optimim
                                if (this.CurrentNumericIssue.OptimumValueStart != this.CurrentNumericIssue.OptimumValueEnd)
                                {
                                    mChartValues.Add(new DataPoint() { YValue = 100, XValue = (double)(this.CurrentNumericIssue.OptimumValueEnd), Label = "Opt.End" });
                                }

                                mChartValues.Add(new DataPoint() { YValue = 0, XValue = (double)(this.CurrentNumericIssue.MaximumValue), Label = "Max" });
                            }


                        }

                        # endregion In case that it is related to Optimum = Min Value

                        #region → In case that it is related to Optimum = Max Value .

                        if (this.CurrentNumericIssue.OptimumValueEnd == this.CurrentNumericIssue.MaximumValue)
                        {
                            if (this.CurrentNumericIssue.MaxOperatorEqual)
                            {
                                decimal NewPointDiff = (this.CurrentNumericIssue.MaximumValue - this.CurrentNumericIssue.MinimumValue) / (decimal)10.00;

                                mChartValues.Add(new DataPoint() { YValue = 0, XValue = (double)(this.CurrentNumericIssue.MinimumValue), Label = "Min" });
                                //in case if the start not equal the End Optimim
                                if (this.CurrentNumericIssue.OptimumValueStart != this.CurrentNumericIssue.OptimumValueEnd)
                                {
                                    mChartValues.Add(new DataPoint() { YValue = 100, XValue = (double)(this.CurrentNumericIssue.OptimumValueStart), Label = "Opt.Start" });
                                }
                                mChartValues.Add(new DataPoint() { YValue = 100, XValue = (double)(this.CurrentNumericIssue.MaximumValue), Label = "Max" });
                                mChartValues.Add(new DataPoint() { YValue = 100, XValue = (double)(this.CurrentNumericIssue.MaximumValue + NewPointDiff), Label = "→" });

                                #region For Drawing Better arrows
                                mChartValues.Add(new DataPoint() { YValue = 103, XValue = (double)(this.CurrentNumericIssue.MaximumValue + NewPointDiff - (NewPointDiff / (decimal)10.00)), Label = "→" });
                                mChartValues.Add(new DataPoint() { YValue = 100, XValue = (double)(this.CurrentNumericIssue.MaximumValue + NewPointDiff), Label = "→" });
                                mChartValues.Add(new DataPoint() { YValue = 97, XValue = (double)(this.CurrentNumericIssue.MaximumValue + NewPointDiff - (NewPointDiff / (decimal)10.00)), Label = "→" });
                                #endregion


                            }

                            if (this.CurrentNumericIssue.MaxOperatorWorse)
                            {
                                decimal NewPointDiff = (this.CurrentNumericIssue.MaximumValue - this.CurrentNumericIssue.MinimumValue) / (decimal)10.00;

                                mChartValues.Add(new DataPoint() { YValue = 0, XValue = (double)(this.CurrentNumericIssue.MinimumValue), Label = "Min" });
                                //in case if the start not equal the End Optimim
                                if (this.CurrentNumericIssue.OptimumValueStart != this.CurrentNumericIssue.OptimumValueEnd)
                                {
                                    mChartValues.Add(new DataPoint() { YValue = 100, XValue = (double)(this.CurrentNumericIssue.OptimumValueStart), Label = "Opt.Start" });
                                }
                                mChartValues.Add(new DataPoint() { YValue = 100, XValue = (double)(this.CurrentNumericIssue.MaximumValue), Label = "Max" });
                                mChartValues.Add(new DataPoint() { YValue = 0, XValue = (double)(this.CurrentNumericIssue.MaximumValue + (decimal)0.01) });
                                mChartValues.Add(new DataPoint() { YValue = 0, XValue = (double)(this.CurrentNumericIssue.MaximumValue + NewPointDiff) });
                            }


                            if (this.CurrentNumericIssue.MaxOperatorBetter)
                            {

                                decimal NewPointDiff = (this.CurrentNumericIssue.MaximumValue - this.CurrentNumericIssue.MinimumValue) / (decimal)10.00;

                                mChartValues.Add(new DataPoint() { YValue = 0, XValue = (double)(this.CurrentNumericIssue.MinimumValue), Label = "Min" });

                                //in case if the start not equal the End Optimim
                                if (this.CurrentNumericIssue.OptimumValueStart != this.CurrentNumericIssue.OptimumValueEnd)
                                {
                                    mChartValues.Add(new DataPoint() { YValue = 100, XValue = (double)(this.CurrentNumericIssue.OptimumValueStart), Label = "Opt.Start" });
                                }
                                mChartValues.Add(new DataPoint() { YValue = 100, XValue = (double)(this.CurrentNumericIssue.MaximumValue), Label = "Max" });
                                mChartValues.Add(new DataPoint() { YValue = 110, XValue = (double)(this.CurrentNumericIssue.MaximumValue + NewPointDiff), Label = "↑↑" });

                                #region For Drawing Better arrows
                                mChartValues.Add(new DataPoint() { YValue = 111, XValue = (double)(this.CurrentNumericIssue.MaximumValue + NewPointDiff - (NewPointDiff / (decimal)10.00)), Label = "↑↑" });
                                mChartValues.Add(new DataPoint() { YValue = 110, XValue = (double)(this.CurrentNumericIssue.MaximumValue + NewPointDiff), Label = "↑↑" });
                                mChartValues.Add(new DataPoint() { YValue = 107, XValue = (double)(this.CurrentNumericIssue.MaximumValue + NewPointDiff - (NewPointDiff / (decimal)15.00)), Label = "↑↑" });
                                #endregion



                            }


                        }

                        # endregion In case that it is related to Optimum = Max Value

                        if (mChartValues.Count == 0)
                        {
                            mChartValues.Add(new DataPoint() { YValue = 0, XValue = (double)(this.CurrentNumericIssue.MinimumValue), Label = "Min", Tooltip = "Minimum value" });
                            mChartValues.Add(new DataPoint() { YValue = 100, XValue = (double)(this.CurrentNumericIssue.OptimumValueStart), Label = "Opt. Start" });
                            mChartValues.Add(new DataPoint() { YValue = 100, XValue = (double)(this.CurrentNumericIssue.OptimumValueEnd), Label = "Opt. End" });
                            mChartValues.Add(new DataPoint() { YValue = 0, XValue = (double)(this.CurrentNumericIssue.MaximumValue), Label = "Max" });
                        }
                    }
                }
                return mChartValues;
            }
        }

        #endregion

        #region → Constractor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="NumericViewModel"/> class.
        /// </summary>
        public NumericViewModel(Issue issue)
        {
            this.CurrentIssue = issue;
        }

        #endregion

        #region → Commands       .

        /// <summary>
        /// Gets the plot chart command.
        /// </summary>
        /// <value>The plot chart command.</value>
        public RelayCommand PlotChartCommand
        {
            get
            {
                if (mPlotChartCommand == null)
                {
                    mPlotChartCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            this.IsMinimumOperatorsEnabled = false;
                            this.IsMaximumOperatorsEnabled = false;

                            if (IsAllNumericValuesValid())
                            {
                                SetMaxMinEnableState();

                                this.RaisePropertyChanged("ChartValues");

                                IsPlotChartCommandDone = true;
                            }
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            PrefAppMessanger.RaiseErrorMessage.Send(ex);
                        }
                    });
                }
                return mPlotChartCommand;
            }
        }

        /// <summary>
        /// Gets the go to numeric text mode command.
        /// </summary>
        /// <value>The go to numeric text mode command.</value>
        public RelayCommand GoToNumericTextModeCommand
        {
            get
            {
                if (mGoToNumericTextModeCommand == null)
                {
                    mGoToNumericTextModeCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            this.ShowGraphModeView = false;
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            PrefAppMessanger.RaiseErrorMessage.Send(ex);
                        }
                    });
                }
                return mGoToNumericTextModeCommand;
            }
        }

        /// <summary>
        /// Gets the go to numeric graphic mode command.
        /// </summary>
        /// <value>The go to numeric graphic mode command.</value>
        public RelayCommand GoToNumericGraphicModeCommand
        {
            get
            {
                if (mGoToNumericGraphicModeCommand == null)
                {
                    mGoToNumericGraphicModeCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            this.ShowGraphModeView = true;
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            PrefAppMessanger.RaiseErrorMessage.Send(ex);
                        }
                    });
                }
                return mGoToNumericGraphicModeCommand;
            }
        }

        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Manipulates the min max.
        /// </summary>
        private void ManipulateMinMax()
        {
            if (this.CurrentNumericIssue != null)
            {
                #region → Maximum Manipulation .

                if (this.CurrentNumericIssue.MaxOperatorBetter &&
                    this.CurrentNumericIssue.MaximumOperator != (byte)Operators.Better)
                {
                    this.CurrentNumericIssue.MaximumOperator = (byte)Operators.Better;
                }

                if (this.CurrentNumericIssue.MaxOperatorEqual &&
                    this.CurrentNumericIssue.MaximumOperator != (byte)Operators.Equal)
                {
                    this.CurrentNumericIssue.MaximumOperator = (byte)Operators.Equal;
                }

                if (this.CurrentNumericIssue.MaxOperatorWorse &&
                    this.CurrentNumericIssue.MaximumOperator != (byte)Operators.Worse)
                {
                    this.CurrentNumericIssue.MaximumOperator = (byte)Operators.Worse;
                }

                #endregion

                #region → Minimum Manipulation .

                if (this.CurrentNumericIssue.MinOperatorBetter &&
                    this.CurrentNumericIssue.MinimumOperator != (byte)Operators.Better)
                {
                    this.CurrentNumericIssue.MinimumOperator = (byte)Operators.Better;
                }

                if (this.CurrentNumericIssue.MinOperatorEqual &&
                    this.CurrentNumericIssue.MinimumOperator != (byte)Operators.Equal)
                {
                    this.CurrentNumericIssue.MinimumOperator = (byte)Operators.Equal;
                }

                if (this.CurrentNumericIssue.MinOperatorWorse &&
                    this.CurrentNumericIssue.MinimumOperator != (byte)Operators.Worse)
                {
                    this.CurrentNumericIssue.MinimumOperator = (byte)Operators.Worse;
                }

                #endregion
            }
        }

        /// <summary>
        /// Determines whether [is all numeric values valid].
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if [is all numeric values valid]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsAllNumericValuesValid()
        {
            if (CurrentNumericIssue != null)
            {
                ManipulateMinMax();

                mCurrentNumericIssue.ValidationErrors.Clear();

                if (mCurrentNumericIssue.MinimumValue > mCurrentNumericIssue.MaximumValue)
                {
                    this.mCurrentNumericIssue.ValidationErrors.Add(new ValidationResult(Resources.MinimumToMaximumValue, new string[] { "MinimumValue" }));
                }

                if (mCurrentNumericIssue.MaximumValue < mCurrentNumericIssue.MinimumValue)
                {
                    this.mCurrentNumericIssue.ValidationErrors.Add(new ValidationResult(Resources.MaximumToMinimumValue, new string[] { "MaximumValue" }));
                }

                if (mCurrentNumericIssue.OptimumValueStart < mCurrentNumericIssue.MinimumValue ||
                    mCurrentNumericIssue.OptimumValueStart > mCurrentNumericIssue.OptimumValueEnd)
                {
                    this.mCurrentNumericIssue.ValidationErrors.Add(new ValidationResult(Resources.MinOptimumToMaxOptimumToMaxValue, new string[] { "OptimumValueStart" }));
                }

                if (mCurrentNumericIssue.OptimumValueEnd > mCurrentNumericIssue.MaximumValue ||
                    mCurrentNumericIssue.OptimumValueEnd < mCurrentNumericIssue.OptimumValueStart)
                {
                    this.mCurrentNumericIssue.ValidationErrors.Add(new ValidationResult(Resources.MaxOptimumToMinOptimumToMinValue, new string[] { "OptimumValueEnd" }));
                }

                if (mCurrentNumericIssue.OptimumValueStart == mCurrentNumericIssue.MinimumValue &&
                    mCurrentNumericIssue.OptimumValueEnd == mCurrentNumericIssue.MaximumValue)
                {
                    this.mCurrentNumericIssue.ValidationErrors.Add(new ValidationResult(Resources.WrongGraphValuesMinMax, new string[] { "OptimumValueStart" }));
                }

                if (!mCurrentNumericIssue.TryValidateObject())
                    return false;


                #region → reset flag of Better in case no better  .

                if (mCurrentNumericIssue.OptimumValueStart > mCurrentNumericIssue.MinimumValue && mCurrentNumericIssue.MinimumOperator != 1)
                {
                    mCurrentNumericIssue.MinimumOperator = (int)Operators.Equal;
                    mCurrentNumericIssue.MinOperatorBetter = false;
                    mCurrentNumericIssue.MinOperatorEqual = true;
                    mCurrentNumericIssue.MinOperatorWorse = false;
                }

                if (mCurrentNumericIssue.OptimumValueEnd < mCurrentNumericIssue.MaximumValue && mCurrentNumericIssue.MaximumOperator != 1)
                {
                    mCurrentNumericIssue.MaximumOperator = (int)Operators.Equal;
                    mCurrentNumericIssue.MaxOperatorBetter = false;
                    mCurrentNumericIssue.MaxOperatorEqual = true;
                    mCurrentNumericIssue.MaxOperatorWorse = false;
                }

                #endregion

                return this.mCurrentNumericIssue.ValidationErrors.Count == 0;
            }
            return true;

        }

        /// <summary>
        /// Sets the state of the max min enable.
        /// </summary>
        private void SetMaxMinEnableState()
        {
            //in case if the Optimum start equal to minimum then open minimum operators
            this.IsMinimumOperatorsEnabled = this.CurrentNumericIssue != null && this.CurrentNumericIssue.MinimumValue == this.CurrentNumericIssue.OptimumValueStart;

            //in case if the Optimum end equal to maximum then open maximum operators
            this.IsMaximumOperatorsEnabled = this.CurrentNumericIssue != null && this.CurrentNumericIssue.MaximumValue == this.CurrentNumericIssue.OptimumValueEnd;
        }

        #endregion

        #endregion Methods

    }
}
