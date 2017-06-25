
#region → Usings   .

using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.ServiceModel.DomainServices.Server;
using citPOINT.PrefApp.Common;

#endregion

#region → History  .

/* Date         User          Change
 * 
 * 26.02.12     M.Wahab       • Creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion

namespace citPOINT.PrefApp.Data.Web.Complete
{
    /// <summary>
    /// Complete Issue in one object.
    /// </summary>
    [DataContract]
    public class CompleteIssue
    {
        #region → Fields         .

        /// <summary>
        /// rank for ordering only
        /// </summary>
        internal static int rank = 0;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets the preference set ID.
        /// </summary>
        /// <value>The preference set ID.</value>
        [DataMemberAttribute]
        public Guid PreferenceSetID { get; set; }

        /// <summary>
        /// Gets or sets the issue ID.
        /// </summary>
        /// <value>The issue ID.</value>
        [DataMemberAttribute()]
        [Key]
        public Guid IssueID { get; set; }

        /// <summary>
        /// Gets or sets the rank.
        /// </summary>
        /// <value>The rank.</value>
        [DataMemberAttribute()]
        public int Rank { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [DataMemberAttribute()]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the score.
        /// </summary>
        /// <value>The score.</value>
        [DataMemberAttribute()]
        public decimal Score { get; set; }

        /// <summary>
        /// Gets or sets the type of the issue.
        /// </summary>
        /// <value>The type of the issue.</value>
        [DataMemberAttribute()]
        public CompleteIssueType IssueType { get; set; }

        /// <summary>
        /// Gets or sets the issue detials.
        /// </summary>
        /// <value>The issue detials.</value>
        [DataMemberAttribute()]
        [Include()]
        [Association("Issue_Options", "IssueID", "IssueID")]
        public List<CompleteOption> Options { get; set; }

        /// <summary>
        /// Gets or sets the numeric.
        /// </summary>
        /// <value>The numeric.</value>
        [DataMemberAttribute()]
        [Include()]
        [Association("Issue_Numeric", "IssueID", "IssueID")]
        public CompleteNumeric Numeric { get; set; }

        #endregion Properties

        #region → Constractor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="CompleteIssue"/> class.
        /// </summary>
        public CompleteIssue()
        {
            this.Options = new List<CompleteOption>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompleteIssue"/> class.
        /// </summary>
        /// <param name="issue">The issue.</param>
        public CompleteIssue(Issue issue)
            : this()
        {
            this.PreferenceSetID = issue.PreferenceSetID;
            this.IssueID = issue.IssueID;
            this.Name = issue.IssueName;
            this.Rank = ++rank;
            this.Score = issue.IssueWeight;

            #region Set Type 

            if (issue.IssueTypeID == PrefAppConstant.IssueTypes.Numeric)
            {
                this.IssueType = CompleteIssueType.Numeric;
            }
            else if (issue.IssueTypeID == PrefAppConstant.IssueTypes.Options)
            {
                this.IssueType = CompleteIssueType.Option;
            }
            else if (issue.IssueTypeID == PrefAppConstant.IssueTypes.LaterRated)
            {
                this.IssueType = CompleteIssueType.LaterRated;
            }
            else if (issue.IssueTypeID == PrefAppConstant.IssueTypes.NotRated)
            {
                this.IssueType = CompleteIssueType.NotRated;
            }
            #endregion

        }

        #endregion
 
    }
}
