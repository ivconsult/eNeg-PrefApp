#region → Usings   .
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
#endregion

#region → History  .

/* Date         User            Change
 * 
 * 19.10.10     Yousra Reda     creation
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

    public partial class MessageIssue
    {
        /// <summary>
        /// Gets or sets the numeric rate.
        /// </summary>
        /// <value>The numeric rate.</value>
        [DataMember]
        [DefaultValue(0)]
        public decimal Rate { get; set; }

        ///// <summary>
        ///// Gets or sets the numeric rate.
        ///// </summary>
        ///// <value>The numeric rate.</value>
        //[DataMember]
        //[DefaultValue(0)]
        //public decimal NumericRate { get; set; }

    }

    /// <summary>
    /// Partial Class for PreferenceSet MetaData to add new feature not exist in ADO.Net Entity created for PrefApp
    /// </summary>
    public partial class PreferenceSet
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is new preference set.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is new preference set; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        [DefaultValue(false)]
        public bool IsNewPreferenceSet { get; set; }

    }


    public partial class PreferenceSetNeg
    {

        /// <summary>
        /// Gets or sets a value indicating whether this instance is closed.
        /// </summary>
        /// <value><c>true</c> if this instance is closed; otherwise, <c>false</c>.</value>
        [DataMember]
        [DefaultValue(false)]
        public bool IsClosed { get; set; }


        internal void UpdateMaxPercentage() { }

    }





    public partial class Issue
    {

        /// <summary>
        /// Gets or sets a value indicating whether this instance is selected.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is selected; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        [DefaultValue(false)]
        public Boolean IsSelected { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether this instance is new issue.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is new issue; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        [DefaultValue(false)]
        public bool IsNewIssue { get; set; }


    }


    // The MetadataTypeAttribute identifies OptionIssueMetadata as the class
    // that carries additional metadata for the OptionIssue class.
    [MetadataTypeAttribute(typeof(OptionIssue.OptionIssueMetadata))]
    public partial class OptionIssue
    {

        /// <summary>
        /// Option Issue Metadata
        /// </summary>
        internal sealed class OptionIssueMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private OptionIssueMetadata()
            {
            }

            public Nullable<bool> Deleted { get; set; }

            public string DeletedBy { get; set; }

            public Nullable<DateTime> DeletedOn { get; set; }

            public Issue Issue { get; set; }

            public Guid IssueID { get; set; }

            public Guid OptionIssueID { get; set; }

            public string OptionIssueValue { get; set; }

            [Range(0, 100, ErrorMessageResourceName = "ValidationErrorMaxMinRange", ErrorMessageResourceType = typeof(ErrorResources))]
            public decimal OptionIssueWeight { get; set; }
        }




        /// <summary>
        /// Gets or sets a value indicating whether this instance is selected.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is selected; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        [DefaultValue(false)]
        public bool IsSelected { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is new option.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is new option; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        [DefaultValue(true)]
        public bool IsNewOption { get; set; }

    }

    /// <summary>
    /// Numeric Issue
    /// </summary>
    public partial class NumericIssue : INumericIssue
    {
        /// <summary>
        /// Gets or sets a value indicating whether [min operator better].
        /// </summary>
        /// <value><c>true</c> if [min operator better]; otherwise, <c>false</c>.</value>
        [DataMember]
        public bool MinOperatorBetter
        {
            get
            {
                if (this.MinimumOperator == 0)
                    return true;
                return false;

            }
            set
            {
                if (value)
                    this.MinimumOperator = 0;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [min operator equal].
        /// </summary>
        /// <value><c>true</c> if [min operator equal]; otherwise, <c>false</c>.</value>
        [DataMember]
        public bool MinOperatorEqual
        {
            get
            {
                if (this.MinimumOperator == 1)
                    return true;
                return false;

            }
            set
            {
                if (value)
                    this.MinimumOperator = 1;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [min operator worse].
        /// </summary>
        /// <value><c>true</c> if [min operator worse]; otherwise, <c>false</c>.</value>
        [DataMember]
        public bool MinOperatorWorse
        {

            get
            {
                if (this.MinimumOperator == 2)
                    return true;
                return false;

            }
            set
            {
                if (value)
                    this.MinimumOperator = 2;
            }

        }

        /// <summary>
        /// Gets or sets a value indicating whether [max operator better].
        /// </summary>
        /// <value><c>true</c> if [max operator better]; otherwise, <c>false</c>.</value>
        [DataMember]
        public bool MaxOperatorBetter
        {
            get
            {
                if (this.MaximumOperator == 0)
                    return true;
                return false;

            }
            set
            {
                if (value)
                    this.MaximumOperator = 0;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [max operator equal].
        /// </summary>
        /// <value><c>true</c> if [max operator equal]; otherwise, <c>false</c>.</value>
        [DataMember]
        public bool MaxOperatorEqual
        {
            get
            {
                if (this.MaximumOperator == 1)
                    return true;
                return false;

            }
            set
            {
                if (value)
                    this.MaximumOperator = 1;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [max operator worse].
        /// </summary>
        /// <value><c>true</c> if [max operator worse]; otherwise, <c>false</c>.</value>
        [DataMember]
        public bool MaxOperatorWorse
        {
            get
            {
                if (this.MaximumOperator == 2)
                    return true;
                return false;

            }
            set
            {
                if (value)
                    this.MaximumOperator = 2;
            }

        }
    }

    /// <summary>
    /// Class represent Coordinates Points
    /// </summary>
    [Serializable()]
    [DataContractAttribute(IsReference = true)]
    public partial class CoordinatesPoints
    {
        #region → Properties     .

        [Key]
        [DataMemberAttribute()]
        public Guid CoordinatesPointsID { get; set; }

        [DataMemberAttribute()]
        public DateTime XAxisPoint { get; set; }

        [DataMemberAttribute()]
        public decimal YAxisPoint { get; set; }

        #endregion

        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="CoordinatesPoints"/> class.
        /// </summary>
        /// <param name="xpoint">The xpoint.</param>
        /// <param name="ypoint">The ypoint.</param>
        public CoordinatesPoints(DateTime xpoint, decimal ypoint)
        {
            CoordinatesPointsID = Guid.NewGuid();
            XAxisPoint = xpoint;
            YAxisPoint = ypoint;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CoordinatesPoints"/> class.
        /// </summary>
        public CoordinatesPoints()
        { }

        #endregion
    }



    /// <summary> 
    /// Class represent Conversation Period
    /// </summary>
    public partial class ConversationPeriod
    {
        #region → Properties     .

        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        /// <value>The ID.</value>
        [Key]
        [DataMemberAttribute()]
        [DefaultValue(1)]
        public int ID { get; set; }

        #endregion

        #region → Constructors   .

        #endregion
    }

}
