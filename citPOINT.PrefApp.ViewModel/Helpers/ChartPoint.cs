

#region → Usings   .
using System;
using Telerik.Windows.Controls.Charting;
#endregion

#region → History  .

/* 
 * Date         User            Change
 * *********************************************
 * 12/07/2012   mwahab         • creation
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
namespace citPOINT.PrefApp.ViewModel.Helpers
{
    /// <summary>
    /// Cutamized Chart Point.
    /// </summary>
    public class ChartPoint : DataPoint
    {
        /// <summary>
        /// Gets or sets the current date.
        /// </summary>
        /// <value>The current date.</value>
        public DateTime? CurrentDate { get; set; }
    }
}
