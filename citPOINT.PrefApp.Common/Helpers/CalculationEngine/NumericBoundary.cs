


#region → Usings   .
using System;
using System.ComponentModel;
using System.Linq;
using System.Collections.Generic;
using citPOINT.PrefApp.Data.Web;
using citPOINT.PrefApp.Common;
using System.Collections.Generic;

#endregion

#region → History  .

/* 
 * Date         User            Change
 * *********************************************
 * 5/23/2011 11:56:18 AM      mwahab         • creation
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
    /// Class for Numeric Boundary (Max-Min)
    /// </summary>
    public class NumericBoundary : INumericBoundary
    {
        #region → Fields         .

        private Guid mPreferenceSetNegID;
        private INumericIssue mCurrentNumeric;

        #endregion

        #region → Properties     .


        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        /// <value>The context.</value>
        private IEngineProvider Provider { get; set; }

        /// <summary>
        /// Gets the top minimum value.
        /// </summary>
        /// <value>The top minimum value.</value>
        public decimal TopMinimumValue
        {
            get
            {

                //intialize by minimum
                decimal MinValue = CurrentNumeric.MinimumValue;

                //getting minimum
                decimal? messageMinValue = this.Provider.GetMinNumericValue(PreferenceSetNegID, CurrentNumeric.IssueID);

                if (messageMinValue.HasValue && messageMinValue < MinValue)
                {
                    MinValue = messageMinValue.Value;
                }

                return MinValue;// Minimum;
            }
        }

        /// <summary>
        /// Gets the top maximum value.
        /// </summary>
        /// <value>The top maximum value.</value>
        public decimal TopMaximumValue
        {
            get
            {

                //intialize to maximum of chart.
                decimal MaxValue = CurrentNumeric.MaximumValue;

                decimal? messageMaxValue = this.Provider.GetMaxNumericValue(PreferenceSetNegID, CurrentNumeric.IssueID);

                if (messageMaxValue.HasValue && messageMaxValue > MaxValue)
                {
                    MaxValue = messageMaxValue.Value;
                }


                return MaxValue;        // Maximum; }
            }
        }



        /// <summary>
        /// Gets or sets the preference set neg ID.
        /// </summary>
        /// <value>The preference set neg ID.</value>
        public Guid PreferenceSetNegID
        {
            get
            {
                return mPreferenceSetNegID;
            }
            set
            {
                mPreferenceSetNegID = value;
            }
        }


        /// <summary>
        /// Gets or sets the current numeric.
        /// </summary>
        /// <value>The current numeric.</value>
        public INumericIssue CurrentNumeric
        {
            get
            {
                return mCurrentNumeric;
            }
            set
            {
                mCurrentNumeric = value;
            }
        }



        #endregion Properties

        #region → Constractor    .

        /// <summary>
        /// Numerics the boundary.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="currentNumeric">The current numeric.</param>
        /// <param name="preferenceSetNegID">The preference set neg ID.</param>
        public NumericBoundary(IEngineProvider provider, INumericIssue currentNumeric, Guid preferenceSetNegID)
        {
            this.Provider = provider;
            this.CurrentNumeric = currentNumeric;
            this.PreferenceSetNegID = preferenceSetNegID;
        }

        #endregion

        #region → Event Handlers .
        #endregion

        #region → Events         .

        #endregion

        #region → Methods        .

        #region → Private        .


        #endregion

        #region → Protected      .
        #endregion

        #region → Public         .



        #endregion  Public


        #endregion Methods


    }
}
