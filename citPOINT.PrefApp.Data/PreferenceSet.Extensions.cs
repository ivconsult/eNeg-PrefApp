
/* Clone All Process Discribtion
 * ex.we make  a preference set for "Purchase a car"
 *  and my company need a car also and i used the same prefenece set "Purchase a Car"
 * 
 *    →Purchase a Car
 *          →Negotion of My
 *          →Negotiation of My Company
 *          
 *  then i Need to add new issue for my Company Only and not effect "my Car".
 * 
 *  then the solution is to copy the current preference Set to new with the changes then
 *  return the Original one to its original (Mean Reject changes)
 * 
 * Step 1 :Copy Current preference Set and its related Tables.
 *           →PreferenceSet
 *              →Issue
 *                  →NumericIssue
 *                  →OptionIssue
 *                  →LaterRatedIssue
 *                  
 * Step 2:Save Data mathing Exist for Selected Negotiation e.g "Negotiation of My Company"
 *          →MessageIssue
 *              →MessageOptionIssue
 *              →MessageLaterRatedIssue
 *              
 * Step 3:Create Mapper Table for Old Issue ID and New Issue ID
 *          e.g. Old Issue is "Price with ID 6"  and New One is "Price with ID 125"
 * 
 * 
 * Step 4:Reject All changes
 * 
 * Step 5:Add New records for All Cloned Tables
 *           →PreferenceSet     (Name =Orignal Prefernce Name + Select negotiation Name) e.g "Purchase a car for My Company"
 *              →Issue
 *                  →NumericIssue
 *                  →OptionIssue
 *                  →LaterRatedIssue
 * 
 * Step 6:Mapping the Following Tables to New Table of "Issues,NumericIssue,OptionIssue,LaterRatedIssue"
 *          →MessageIssue
 *              →MessageOptionIssue
 *              →MessageLaterRatedIssue
 * 
 * Step 7:Updating The prefence set in Selected Negotiation
 * 
 * Step 8:Thanks
 */

#region → Usings         .
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.ServiceModel.DomainServices.Client;
#endregion

#region → History        .

/* Date         User            Change
 * 
 *22.09.10     M.Wahab     creation
 */

# endregion

#region → ToDos          .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion

namespace citPOINT.PrefApp.Data.Web
{

    /// <summary>
    /// PreferenceSet class client-side extensions
    /// </summary>
    public partial class PreferenceSet
    {
        #region → Fields         .
        private List<MapperTable> mMapperTables;
        #endregion

        #region → Properties     .
  
        /// <summary>
        /// Gets the onging preference set negs.
        /// </summary>
        /// <value>The onging preference set negs.</value>
        public IEnumerable<PreferenceSetNeg> OngingPreferenceSetNegs
        {
            get
            {
                return this.PreferenceSetNegs.Where(s => s.IsClosed == false).AsEnumerable();
            }
        }

        /// <summary>
        /// Gets the mapper tables.
        /// <B>Table Carry the Mapper Table for All tables for
        /// New Value  And Old Value Was?
        /// </B>
        /// ex.Old Issue Name Is "Price" ID=125 New Issue Name ="Price" and ID=520
        /// </summary>
        /// <value>The mapper tables.</value>
        public List<MapperTable> MapperTables
        {
            get
            {
                if (mMapperTables == null)
                {
                    mMapperTables = new List<MapperTable>();
                }
                return mMapperTables;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is editable.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is editable; otherwise, <c>false</c>.
        /// </value>
        public bool IsEditable
        {
            get
            {
                return this.MainPreferenceSetID.Equals(Guid.Parse("72f5566e-3bf5-46e6-9406-b13e80f83bcc")/* My Sets */);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this entity is currently in a read-only state.
        /// </summary>
        /// <value></value>
        public  bool IsReadOnly
        {
            get
            {
                return !(this.MainPreferenceSetID == new Guid("72f5566e-3bf5-46e6-9406-b13e80f83bcc"))/* Set store & organization sets*/;
            }
        }


        #endregion

        #region → Methods        .

        /// <summary>
        /// Updates the max percentage.
        /// Maximum preference set Thresould (xx of XX%)
        /// </summary>
        public void UpdateMaxPercentage()
        {

            this.MaxPercentage = 0;

            foreach (var issue in this.Issues)
            {
                // In case of Numeric
                if (issue.NumericIssues.Count > 0)
                {
                    this.MaxPercentage += issue.IssueWeight;
                }
                // In case of Option Issues 
                else if (issue.OptionIssues.Count > 0)
                {
                    this.MaxPercentage += Math.Round(issue.OptionIssues.OrderByDescending(s => s.OptionIssueWeight).FirstOrDefault().OptionIssueWeight * issue.IssueWeight / 100, 2);
                }
                // In case of later rated Type
                else if (issue.LaterRatedIssues.Count > 0)
                {
                    this.MaxPercentage += Math.Round(issue.LaterRatedIssues.OrderByDescending(s => s.LaterRatedIssueWeight).FirstOrDefault().LaterRatedIssueWeight * issue.IssueWeight / 100, 2);
                }
            }

            this.RaisePropertyChanged("MaxPercentage");
        }

        /// <summary>
        /// Try validate for the PreferenceSet class
        /// </summary>
        /// <returns>True Or False </returns>
        public bool TryValidateObject()
        {
            ValidationContext context = new ValidationContext(this, null, null);
            var validationResults = new Collection<ValidationResult>();

            if (Validator.TryValidateObject(this, context, validationResults, false) == false)
            {
                foreach (ValidationResult error in validationResults)
                {
                    this.ValidationErrors.Add(error);
                }
                return false;
            }


            return true;
        }

        /// <summary>    
        /// Try Try Validate by Property name  
        /// </summary> 
        /// <returns>True Or False </returns> 
        public bool TryValidateProperty(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentNullException("propertyName");
            }
            if (propertyName == "PreferenceSetID"
             || propertyName == "PreferenceSetName"
             || propertyName == "UserID"
             || propertyName == "MainPreferenceSetID"
             || propertyName == "Deleted"
             || propertyName == "DeletedBy"
             || propertyName == "DeletedOn"
            )
            {

                ValidationContext context = new ValidationContext(this, null, null) { MemberName = propertyName };
                var validationResults = new Collection<ValidationResult>();
                if (propertyName == "PreferenceSetID")
                    return Validator.TryValidateProperty(this.PreferenceSetID, context, validationResults);
                if (propertyName == "PreferenceSetName")
                    return Validator.TryValidateProperty(this.PreferenceSetName, context, validationResults);
                if (propertyName == "UserID")
                    return Validator.TryValidateProperty(this.UserID, context, validationResults);
                if (propertyName == "MainPreferenceSetID")
                    return Validator.TryValidateProperty(this.MainPreferenceSetID, context, validationResults);
                if (propertyName == "Deleted")
                    return Validator.TryValidateProperty(this.Deleted, context, validationResults);
                if (propertyName == "DeletedBy")
                    return Validator.TryValidateProperty(this.DeletedBy, context, validationResults);
                if (propertyName == "DeletedOn")
                    return Validator.TryValidateProperty(this.DeletedOn, context, validationResults);
            }
            return false;
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>return new Instance of PreferenceSet</returns>
        public PreferenceSet Clone()
        {
            PreferenceSet mPreferenceSet = new PreferenceSet
            {
                PreferenceSetID = this.PreferenceSetID,
                PreferenceSetName = this.PreferenceSetName,
                UserID = this.UserID,
                MainPreferenceSetID = this.MainPreferenceSetID,
                MaxPercentage = this.MaxPercentage,
                Checkvariation = this.Checkvariation,
                VariationValue = this.VariationValue,
                Deleted = this.Deleted,
                DeletedBy = this.DeletedBy,
                DeletedOn = this.DeletedOn
            };

            return mPreferenceSet;
        }

        /// <summary>
        ///Clones this and its related tables.
        /// </summary>
        /// <param name="PreferenceSetNegIDs">The preference set neg Ids.</param>
        /// <returns>return new Instance of PreferenceSet</returns>
        public List<Entity> CloneAll(List<Guid> PreferenceSetNegIDs)
        {
            List<Entity> allEntities = new List<Entity>();

            #region → Clone and Alter Preference Set         .

            PreferenceSet newPreferenceSet = this.Clone();
            newPreferenceSet.PreferenceSetID = Guid.NewGuid();
            allEntities.Add(newPreferenceSet);

            #endregion

            #region → Clone and Alter Issues                 .

            foreach (var issue in this.Issues)
            {
                #region → Clone And Alter Issues             .

                Issue newIssue = issue.Clone();

                newIssue.PreferenceSetID = newPreferenceSet.PreferenceSetID;

                newIssue.IssueID = Guid.NewGuid();

                allEntities.Add(newIssue);



                // mapper for Issues (For Future use)
                this.MapperTables.Add(new MapperTable()
                                                    {
                                                        OldGuid = issue.IssueID,
                                                        NewGuid = newIssue.IssueID,
                                                        TableName = TableNames.Issue
                                                    });



                #endregion

                #region → Clone and Alter Numeric Issues     .

                foreach (var numeric in issue.NumericIssues)
                {
                    NumericIssue newNumericIssue = numeric.Clone();

                    newNumericIssue.NumericIssueID = Guid.NewGuid();

                    newNumericIssue.IssueID = newIssue.IssueID;

                    allEntities.Add(newNumericIssue);
                }

                #endregion

                #region → Clone and Alter Option Issues      .

                foreach (var optionIssue in issue.OptionIssues)
                {
                    OptionIssue newOptionIssue = optionIssue.Clone();

                    newOptionIssue.OptionIssueID = Guid.NewGuid();

                    newOptionIssue.IssueID = newIssue.IssueID;

                    allEntities.Add(newOptionIssue);


                    // mapper for option Issue (For Future use)
                    this.MapperTables.Add(new MapperTable()
                                                        {
                                                            OldGuid = optionIssue.OptionIssueID,
                                                            NewGuid = newOptionIssue.OptionIssueID,
                                                            TableName = TableNames.OptionIssue
                                                        });

                }

                #endregion

                #region → Clone and Alter Later rated Issues .

                foreach (var laterRatedIssues in issue.LaterRatedIssues)
                {

                    LaterRatedIssue newLaterRatedIssue = laterRatedIssues.Clone();

                    newLaterRatedIssue.LaterRatedIssueID = Guid.NewGuid();

                    newLaterRatedIssue.IssueID = newIssue.IssueID;

                    allEntities.Add(newLaterRatedIssue);


                    // mapper for later Issue (For Future use)
                    this.MapperTables.Add(new MapperTable()
                                                    {
                                                        OldGuid = laterRatedIssues.LaterRatedIssueID,
                                                        NewGuid = newLaterRatedIssue.LaterRatedIssueID,
                                                        TableName = TableNames.LaterRatedIssue
                                                    }
                                   );





                }

                #endregion

                #region → Save Data mathing Exist            .
                if (PreferenceSetNegIDs != null)
                {

                    List<MessageIssue> mMessageIssues = issue.MessageIssues.Where(ss => PreferenceSetNegIDs.Contains(ss.ConversationMessage.NegConversation.PreferenceSetNegID)).ToList();

                    foreach (var messageIssue in mMessageIssues)
                    {
                        // mapper for Message Issue (For Future use)
                        this.MapperTables.Add(new MapperTable()
                                                        {
                                                            OldGuid = messageIssue.MessageIssueID,
                                                            NewGuid = messageIssue.MessageIssueID,
                                                            TableName = TableNames.MessageIssue
                                                        });

                        foreach (var messageOptionIssue in messageIssue.MessageOptionIssues)
                        {
                            // mapper for Message Issue (For Future use)
                            this.MapperTables.Add(new MapperTable()
                                                            {
                                                                OldGuid = messageOptionIssue.MessageOptionIssueID,
                                                                NewGuid = messageOptionIssue.MessageOptionIssueID,
                                                                TableName = TableNames.MessageOptionIssue
                                                            });
                        }


                        foreach (var messageLaterRatedIssue in messageIssue.MessageLaterRatedIssues)
                        {
                            // mapper for Message Issue (For Future use)
                            this.MapperTables.Add(new MapperTable()
                            {
                                OldGuid = messageLaterRatedIssue.MessageLaterRatedIssueID,
                                NewGuid = messageLaterRatedIssue.MessageLaterRatedIssueID,
                                TableName = TableNames.MessageLaterRatedIssue
                            });
                        }
                    }
                }
                #endregion

            }
            #endregion

            return allEntities;
        }


        #endregion Methods

    }
}
