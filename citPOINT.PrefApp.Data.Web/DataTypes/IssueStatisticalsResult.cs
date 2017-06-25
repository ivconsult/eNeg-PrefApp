using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace citPOINT.PrefApp.Data.Web
{
    /// <summary>
    /// Class reprsents the result from selecting Issues Statisticals
    /// </summary>
    [MetadataTypeAttribute(typeof(IssueStatisticalsResult.IssueStatisticalsResultMetadata))]
    public partial class IssueStatisticalsResult
                         
    {
          internal sealed class IssueStatisticalsResultMetadata
          {
              // Metadata classes are not meant to be instantiated.
              private IssueStatisticalsResultMetadata()
              {

              }

              #region → Properties     .

              /// <summary>
              /// Gets or sets the rank.
              /// </summary>
              /// <value>The rank.</value>
              [DataMemberAttribute()]
              [Key]
              public int Rank { get; set; }

              /// <summary>
              /// Gets or sets the name of the issue.
              /// </summary>
              /// <value>The name of the issue.</value>
              [DataMemberAttribute()]
              public string IssueName { get; set; }

              /// <summary>
              /// Gets or sets the times used.
              /// </summary>
              /// <value>The times used.</value>
              [DataMemberAttribute()]
              public int TimesUsed { get; set; }

              #endregion
          }
    }
}
