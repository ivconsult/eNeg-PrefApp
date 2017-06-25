
#region → Usings   .
using System.Collections.Generic;
using GalaSoft.MvvmLight;
using citPOINT.PrefApp.Data.Web;
using citPOINT.PrefApp.Common;
#endregion

#region → History  .

/* 
 * Date         User            Change
 * *********************************************
 * 27.03.2012   mwahab         • creation
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
    /// Interface for Return Instance.
    /// </summary>
    public interface IIssueDetailsViewModel
    {
        /// <summary>
        /// Gets a value indicating whether this instance is all valid.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is all valid; otherwise, <c>false</c>.
        /// </value>
        bool IsAllValid { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is expanded.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is expanded; otherwise, <c>false</c>.
        /// </value>
        bool IsExpanded { get; set; }

        /// <summary>
        /// Gets the current issue.
        /// </summary>
        /// <value>The current issue.</value>
        Issue CurrentIssue { get; }

        /// <summary>
        /// Gets the name of the view.
        /// </summary>
        /// <value>The name of the view.</value>
        string ViewName { get; }

        /// <summary>
        /// Gets the first view model.
        /// </summary>
        /// <value>The first view model.</value>
        IIssueDetailsViewModel FirstViewModel { get; }

        /// <summary>
        /// Gets the view model.
        /// </summary>
        /// <value>The view model.</value>
        IEnumerable<IIssueDetailsViewModel> ViewModel { get; }
    }
}
