#region → Usings   .
using citPOINT.PrefApp.Common;
using citPOINT.PrefApp.Data.Web;
using System.Collections.ObjectModel;

#endregion

#region → History  .

/* Date         User              Change
 * 
 * 09.11.10     Yousra.Mohamed       Creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion

namespace citPOINT.PrefApp.Client
{
    /// <summary>
    /// Issue Types Source
    /// </summary>
    public class IssueTypesSource
    {
        #region → Fields         .
        private ObservableCollection<IssueType> mIssueTypes;
        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets the issue types.
        /// </summary>
        /// <value>The issue types.</value>
        public ObservableCollection<IssueType> IssueTypes
        {
            get
            {
                if (mIssueTypes == null)
                {
                    mIssueTypes = new ObservableCollection<IssueType>();

                    mIssueTypes.Add(new IssueType() { IssueTypeID = PrefAppConstant.IssueTypes.SelectType, IssueTypeName = "Select" });
                    mIssueTypes.Add(new IssueType() { IssueTypeID = PrefAppConstant.IssueTypes.Numeric, IssueTypeName = "Numeric" });
                    mIssueTypes.Add(new IssueType() { IssueTypeID = PrefAppConstant.IssueTypes.Options, IssueTypeName = "Options" });
                    mIssueTypes.Add(new IssueType() { IssueTypeID = PrefAppConstant.IssueTypes.LaterRated, IssueTypeName = "Later Rated" });
                    mIssueTypes.Add(new IssueType() { IssueTypeID = PrefAppConstant.IssueTypes.NotRated, IssueTypeName = "Not Rated" });
                }
                return mIssueTypes;
            }
        }
        #endregion
    }
}
