﻿
#region → Usings   .
using System;
using citPOINT.PrefApp.Data.Web;
#endregion

#region → History  .
/* Date           User            Change
 * 
 * 18.07.11      M.Wahab        → creation
 * Generated By Eno Generator - Mohamedenew@hotmail.com
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion

namespace citPOINT.PrefApp.Common.Test.Mocks
{
    /// <summary>
    /// Class for Numeric Boundary (Max-Min) Mock.
    /// </summary>
   public class NumericBoundaryMock : INumericBoundary
    {
        #region → Fields        .
        
        private decimal mTopMinimumValue;
        private decimal mTopMaximumValue;
        private Guid mPreferenceSetNegID;
        private INumericIssue mCurrentNumeric;

        #endregion

        #region → Properties    .

        /// <summary>
        /// Gets the top minimum value.
        /// </summary>
        /// <value>The top minimum value.</value>
        public decimal TopMinimumValue
        {
            get { return mTopMinimumValue; }
        }

        /// <summary>
        /// Gets the top maximum value.
        /// </summary>
        /// <value>The top maximum value.</value>
        public decimal TopMaximumValue
        {
            get { return mTopMaximumValue; }
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

        #endregion
        
        #region → Constructor   .

        /// <summary>
        /// Initializes a new instance of the <see cref="NumericBoundaryMock"/> class.
        /// </summary>
        /// <param name="currentNumeric">The current numeric.</param>
        /// <param name="topMinimumValue">The top minimum value.</param>
        /// <param name="topMaximumValue">The top maximum value.</param>
        public NumericBoundaryMock(INumericIssue currentNumeric, decimal topMinimumValue, decimal topMaximumValue)
        {
            this.CurrentNumeric = currentNumeric;
            this.mTopMaximumValue = topMaximumValue;
            this.mTopMinimumValue = topMinimumValue;
        }
        
        #endregion

    }
}