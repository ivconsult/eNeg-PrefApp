
#region → Usings   .
using System;
using System.Windows;

#endregion

#region → History  .

#endregion

#region → ToDos    .
#endregion

namespace citPOINT.PrefApp.Common
{
    /// <summary>
    /// Drag Event Args
    /// </summary>
    public class DragEventArgs : EventArgs
    {
        #region → Fields         .

        private UIElement mTarget;
        private string mValue;

        #endregion Fields

        #region → Properties     .

        /// <summary>
        /// Gets or sets the target control.
        /// </summary>
        /// <value>The target control.</value>
        public UIElement TargetControl
        {
            get
            {
                return mTarget;
            }

            set
            {
                mTarget = value;
            }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public string Value
        {
            get
            {
                return mValue;
            }

            set
            {
                mValue = value;
            }
        }
        
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public double NumericValue
        {
            get
            {
                if (Value == null)
                    return 0;
             
                double x=0;

                if (double.TryParse(Value, out x))
                    return x;

                double? dbl = WordsToNumberConverter.Text2Number(Value);
                if (dbl.HasValue)
                    return dbl.Value;

                return 0;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is numeric.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is numeric; otherwise, <c>false</c>.
        /// </value>
        public bool IsNumeric
        {
            get
            {

                if (Value == null)
                    return false;
                
                double x = 0;
                if (double.TryParse(Value, out x))
                    return true;

                double? dbl = WordsToNumberConverter.Text2Number(Value);
                if (dbl.HasValue)
                    return true;

                return false;
            }
        }

        #endregion Properties

        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="DragEventArgs"/> class.
        /// </summary>
        /// <param name="TargetControl">The target control.</param>
        /// <param name="Value">The value.</param>
        public DragEventArgs(UIElement TargetControl, string Value)
        {
            this.TargetControl = TargetControl;
            this.Value = Value;
        }

        #endregion Constructor

    }
}
