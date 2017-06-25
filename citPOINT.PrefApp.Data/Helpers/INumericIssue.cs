#region → Usings   .
using System;
#endregion

#region → History  .

/* 
 * Date         User            Change
 * *********************************************
 * 27/02/2012   mwahab         • creation
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


namespace citPOINT.PrefApp.Data.Web
{
    /// <summary>
    /// I Numeric Issue
    /// </summary>
    public interface INumericIssue
    {
        #region → Properties     .

        /// <summary>
        /// Gets or sets the issue ID.
        /// </summary>
        /// <value>The issue ID.</value>
        Guid IssueID { get; set; }

        /// <summary>
        /// Gets or sets the maximum value.
        /// </summary>
        /// <value>The maximum value.</value>
        decimal MaximumValue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [max operator better].
        /// </summary>
        /// <value><c>true</c> if [max operator better]; otherwise, <c>false</c>.</value>
        bool MaxOperatorBetter { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [max operator equal].
        /// </summary>
        /// <value><c>true</c> if [max operator equal]; otherwise, <c>false</c>.</value>
        bool MaxOperatorEqual { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [max operator worse].
        /// </summary>
        /// <value><c>true</c> if [max operator worse]; otherwise, <c>false</c>.</value>
        bool MaxOperatorWorse { get; set; }

        /// <summary>
        /// Gets or sets the minimum value.
        /// </summary>
        /// <value>The minimum value.</value>
        decimal MinimumValue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [min operator better].
        /// </summary>
        /// <value><c>true</c> if [min operator better]; otherwise, <c>false</c>.</value>
        bool MinOperatorBetter { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [min operator equal].
        /// </summary>
        /// <value><c>true</c> if [min operator equal]; otherwise, <c>false</c>.</value>
        bool MinOperatorEqual { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [min operator worse].
        /// </summary>
        /// <value><c>true</c> if [min operator worse]; otherwise, <c>false</c>.</value>
        bool MinOperatorWorse { get; set; }

        /// <summary>
        /// Gets or sets the optimum value end.
        /// </summary>
        /// <value>The optimum value end.</value>
        decimal OptimumValueEnd { get; set; }

        /// <summary>
        /// Gets or sets the optimum value start.
        /// </summary>
        /// <value>The optimum value start.</value>
        decimal OptimumValueStart { get; set; }

        /// <summary>
        /// Gets or sets the unit.
        /// </summary>
        /// <value>The unit.</value>
        string Unit { get; set; }

        #endregion
    }


}
