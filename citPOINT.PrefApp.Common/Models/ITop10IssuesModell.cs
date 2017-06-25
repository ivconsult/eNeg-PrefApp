#region → Usings   .
using System;
using System.ComponentModel;
using citPOINT.eNeg.Common;
using citPOINT.PrefApp.Data.Web;

#endregion

#region → History  .

/* 
 * Date         User            Change
 * *********************************************
 * 3/29/2012 10:51:21 AM      mwahab         • creation
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
    /// Interface for top 10 issues.
    /// </summary>
    public interface ITop10IssuesModel : INotifyPropertyChanged
    {
        #region → Properties     .

        /// <summary>
        /// True if "IsLoading" 
        /// in progress; otherwise, false
        /// </summary>
        bool IsBusy { get; }

        #endregion Properties
 
        #region → Events         .

        /// <summary>
        /// event for returning the loaded Top ten Issues.
        /// </summary>
        event EventHandler<eNegEntityResultArgs<IssueStatisticalsResult>> GetIssueStatisticalsComplete;

        #endregion

        #region → Methods        .
              
        #region → Public         .

        /// <summary>
        /// Gets the issue statisticals async.
        /// </summary>
        void GetIssueStatisticalsAsync();

        #endregion
        
        #endregion

    }
}
