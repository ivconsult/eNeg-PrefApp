#region → Usings   .
using System;
using System.ComponentModel;
using System.Linq;
using System.Collections.Generic;
using citPOINT.PrefApp.Data.Web;

#endregion

#region → History  .

/* 
 * Date         User            Change
 * *********************************************
 * 06/06/2011   mwahab         • creation
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
    /// Class for Interface for Numeric Boundary 
    /// </summary>
    public interface INumericBoundary
    {
         #region → Properties     .

        /// <summary>
        /// Gets the top minimum value.
        /// </summary>
        /// <value>The top minimum value.</value>
        decimal TopMinimumValue { get; }

        /// <summary>
        /// Gets the top maximum value.
        /// </summary>
        /// <value>The top maximum value.</value>
        decimal TopMaximumValue { get; }

        /// <summary>
        /// Gets or sets the current numeric.
        /// </summary>
        /// <value>The current numeric.</value>
        INumericIssue CurrentNumeric { get; set; }

        /// <summary>
        /// Gets or sets the preference set neg ID.
        /// </summary>
        /// <value>The preference set neg ID.</value>
        Guid PreferenceSetNegID { get; set; }

        #endregion Properties
    }
}
