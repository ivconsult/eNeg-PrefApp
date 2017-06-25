
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
    /// Complete Numeric in One Object.
    /// </summary>
    [DataContract]
    public class CompleteNumeric
    {

        #region → Fields         .
        /// <summary>
        /// Auto ID.
        /// </summary>
        internal static int nextID = 0;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        /// <value>The ID.</value>
        [DataMemberAttribute]
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the issue ID.
        /// </summary>
        /// <value>The issue ID.</value>
        [DataMemberAttribute()]
        public Guid IssueID { get; set; }

        /// <summary>
        /// Gets or sets the unit.
        /// </summary>
        /// <value>The unit.</value>
        [DataMemberAttribute()]
        public string Unit { get; set; }

        /// <summary>
        /// Gets or sets the min.
        /// </summary>
        /// <value>The min.</value>
        [DataMemberAttribute()]
        public decimal MinimumValue { get; set; }

        /// <summary>
        /// Gets or sets the max.
        /// </summary>
        /// <value>The max.</value>
        [DataMemberAttribute()]
        public decimal MaximumValue { get; set; }

        /// <summary>
        /// Gets or sets the top min.
        /// </summary>
        /// <value>The top min.</value>
        [DataMemberAttribute()]
        public decimal TopMin { get; set; }

        /// <summary>
        /// Gets or sets the top max.
        /// </summary>
        /// <value>The top max.</value>
        [DataMemberAttribute()]
        public decimal TopMax { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [min operator better].
        /// </summary>
        /// <value><c>true</c> if [min operator better]; otherwise, <c>false</c>.</value>
        [DataMemberAttribute()]
        public bool MinOperatorBetter { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [min operator equal].
        /// </summary>
        /// <value><c>true</c> if [min operator equal]; otherwise, <c>false</c>.</value>
        [DataMemberAttribute()]
        public bool MinOperatorEqual { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [min operator worse].
        /// </summary>
        /// <value><c>true</c> if [min operator worse]; otherwise, <c>false</c>.</value>
        [DataMemberAttribute()]
        public bool MinOperatorWorse { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [max operator better].
        /// </summary>
        /// <value><c>true</c> if [max operator better]; otherwise, <c>false</c>.</value>
        [DataMemberAttribute()]
        public bool MaxOperatorBetter { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [max operator equal].
        /// </summary>
        /// <value><c>true</c> if [max operator equal]; otherwise, <c>false</c>.</value>
        [DataMemberAttribute()]
        public bool MaxOperatorEqual { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [max operator worse].
        /// </summary>
        /// <value><c>true</c> if [max operator worse]; otherwise, <c>false</c>.</value>
        [DataMemberAttribute()]
        public bool MaxOperatorWorse { get; set; }

        /// <summary>
        /// Gets or sets the optimum start.
        /// </summary>
        /// <value>The optimum start.</value>
        [DataMemberAttribute()]
        public decimal OptimumValueStart { get; set; }

        /// <summary>
        /// Gets or sets the optimum end.
        /// </summary>
        /// <value>The optimum end.</value>
        [DataMemberAttribute()]
        public decimal OptimumValueEnd { get; set; }

        #endregion

        #region → Constractor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="CompleteNumeric"/> class.
        /// </summary>
        public CompleteNumeric()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompleteNumeric"/> class.
        /// </summary>
        /// <param name="numericIssue">The numeric issue.</param>
        public CompleteNumeric(NumericIssue numericIssue)
            : this()
        {
            this.ID = ++nextID;

            this.IssueID = numericIssue.IssueID;
            this.MaximumValue = numericIssue.MaximumValue;
            this.MinimumValue = numericIssue.MinimumValue;
            this.MaxOperatorBetter = numericIssue.MaxOperatorBetter;
            this.MaxOperatorEqual = numericIssue.MaxOperatorEqual;
            this.MaxOperatorWorse = numericIssue.MaxOperatorWorse;


            this.MinOperatorBetter = numericIssue.MinOperatorBetter;
            this.MinOperatorEqual = numericIssue.MinOperatorEqual;
            this.MinOperatorWorse = numericIssue.MinOperatorWorse;


            this.OptimumValueStart = numericIssue.OptimumValueEnd;
            this.OptimumValueEnd = numericIssue.OptimumValueEnd;

            this.Unit = numericIssue.Unit;

            this.TopMax = this.MaximumValue;
            this.TopMin = this.MinimumValue;
        }

        #endregion
    }
}

