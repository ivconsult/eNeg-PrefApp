#region → Usings   .
using System;

#endregion

#region → History  .

/* Date         User              Change
 * 
 * 17.05.12     M.Wahab          Creation
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
    /// Pending Item
    /// </summary>
    public class PendingItem
    {

        /// <summary>
        /// Gets or sets the type of the pending.
        /// </summary>
        /// <value>The type of the pending.</value>
        public PrefAppConfigurations.IssueTypes PendingType { get; set; }

        /// <summary>
        /// Gets or sets the pending ID.
        /// </summary>
        /// <value>The pending ID.</value>
        public Guid PendingID { get; set; }


        /// <summary>
        /// Creates the issue.
        /// </summary>
        /// <param name="issueID">The issue ID.</param>
        /// <returns></returns>
        public static PendingItem CreateIssue(Guid issueID)
        {
            return new PendingItem() { PendingID = issueID, PendingType = PrefAppConfigurations.IssueTypes.Issue };
        }

        /// <summary>
        /// Creates the option issue.
        /// </summary>
        /// <param name="optionIssueID">The option issue ID.</param>
        /// <returns></returns>
        public static PendingItem CreateOptionIssue(Guid optionIssueID)
        {
            return new PendingItem() { PendingID = optionIssueID, PendingType= PrefAppConfigurations.IssueTypes.Option };
        }

        /// <summary>
        /// Creates the later rated issue.
        /// </summary>
        /// <param name="laterRatedIssueID">The later rated issue ID.</param>
        /// <returns></returns>
        public static PendingItem CreateLaterRatedIssue(Guid laterRatedIssueID)
        {
            return new PendingItem() { PendingID =laterRatedIssueID, PendingType =  PrefAppConfigurations.IssueTypes.LaterRated };
        }

    }
}
