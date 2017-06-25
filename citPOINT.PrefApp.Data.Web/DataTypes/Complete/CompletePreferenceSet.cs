
#region → Usings   .

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.ServiceModel.DomainServices.Server;
using System.Xml.Serialization;

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
    /// Complete Preference Set in One Object.
    /// </summary>
    [DataContract]
    public class CompletePreferenceSet
    {

        #region → Properties     .

        /// <summary>
        /// Gets or sets the preference set ID.
        /// </summary>
        /// <value>The preference set ID.</value>
        [DataMemberAttribute]
        [Key]
        public Guid PreferenceSetID { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [DataMemberAttribute]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the max percentage.
        /// </summary>
        /// <value>The max percentage.</value>
        [DataMemberAttribute]
        public decimal MaxPercentage { get; set; }

        /// <summary>
        /// Gets or sets the issues.
        /// </summary>
        /// <value>The issues.</value>
        [DataMember]
        //[XmlIgnoreAttribute()]
        //[SoapIgnoreAttribute()]
        [XmlElement]
        [Include]
        [Association("Issues_PrefSet", "PreferenceSetID", "PreferenceSetID")]
        public List<CompleteIssue> Issues { get; set; }

        #endregion

        #region → Constractor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="CompletePreferenceSet"/> class.
        /// </summary>
        public CompletePreferenceSet()
        {
            this.Issues = new List<CompleteIssue>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompletePreferenceSet"/> class.
        /// </summary>
        /// <param name="preferenceSet">The preference set.</param>
        public CompletePreferenceSet(PreferenceSet preferenceSet)
            : this()
        {
            this.Name = preferenceSet.PreferenceSetName;
            this.MaxPercentage = preferenceSet.MaxPercentage;
            this.PreferenceSetID = preferenceSet.PreferenceSetID;
        }

        #endregion

    }
}
