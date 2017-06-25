
#region → Usings   .

using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

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
    /// Complete Option in One Object.
    /// </summary>
    [DataContract]
    public class CompleteOption
    {
        #region → Fields         .
        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        /// <value>The ID.</value>
        [DataMemberAttribute]
        [Key]
        public Guid OptionID { get; set; }


        /// <summary>
        /// Gets or sets the issue ID.
        /// </summary>
        /// <value>The issue ID.</value>
        [DataMemberAttribute()]
        public Guid IssueID { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [DataMemberAttribute]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the rate.
        /// </summary>
        /// <value>The rate.</value>
        [DataMemberAttribute]
        public decimal Rate { get; set; }

        #endregion

        #region → Constractor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="CompleteOption"/> class.
        /// </summary>
        public CompleteOption()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompleteOption"/> class.
        /// </summary>
        /// <param name="optionIssue">The option issue.</param>
        public CompleteOption(OptionIssue optionIssue)
            : this()
        {
            this.OptionID = optionIssue.OptionIssueID;
            this.Name = optionIssue.OptionIssueValue;
            this.Rate = optionIssue.OptionIssueWeight;
            this.IssueID = optionIssue.IssueID;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompleteOption"/> class.
        /// </summary>
        /// <param name="laterRatedIssue">The later rated issue.</param>
        public CompleteOption(LaterRatedIssue laterRatedIssue)
            : this()
        {
            this.OptionID = laterRatedIssue.LaterRatedIssueID;
            this.Name = laterRatedIssue.LaterRatedIssueValue;
            this.Rate = laterRatedIssue.LaterRatedIssueWeight;
            this.IssueID = laterRatedIssue.IssueID;
        }

        #endregion
     
    }
}
