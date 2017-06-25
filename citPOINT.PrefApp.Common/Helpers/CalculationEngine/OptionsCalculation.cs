

#region → Usings   .
using System;
using System.Collections.Generic;
using System.Linq;
using citPOINT.PrefApp.Data.Web;

#endregion

#region → History  .

/* 
 * Date         User            Change
 * *********************************************
 * 06/06/2011   mwahab         • creation
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
    /// Class for Options Calculation 
    /// Options or Later Rated
    /// </summary>
    public class OptionsCalculation
    {

        #region → Fields         .

        private string mValue = string.Empty;
        private decimal mRate;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets the value.
        /// the collections of choices
        /// </summary>
        /// <value>The value.</value>
        public string Value
        {
            get { return mValue; }
            set { mValue = value; }
        }

        /// <summary>
        /// Gets or sets the rate.
        /// </summary>
        /// <value>The rate.</value>
        public decimal Rate
        {
            get
            {
                return mRate;
            }
            set
            {
                mRate = value;
            }
        }
        #endregion
        
        #region → Methods        .

        #region → Public         .

        /// <summary>
        /// Calc Options Score
        /// </summary>
        /// <param name="issueWeight">The issue weight.</param>
        /// <param name="optionIssues">the Options from Options which is Under Preference Set.</param>
        /// <param name="messageOptionIssues">The message option issues.</param>
        /// <returns>
        /// decimal value represent the score of item
        /// </returns>
        public decimal CalcOptionScore(decimal issueWeight, IEnumerable<OptionIssue> optionIssues, IEnumerable<MessageOptionIssue> messageOptionIssues)
        {

            decimal itemScore = 0;

            //Collectiong all values in one line for later usage
            this.Value = string.Empty;

            foreach (var msgOption in messageOptionIssues)
            {
                //Select from Option under Prefernce Set.
                OptionIssue optIssue = optionIssues.Where(s => s.OptionIssueID == msgOption.OptionIssueID).FirstOrDefault();

                if (optIssue != null)
                {
                    if (itemScore < optIssue.OptionIssueWeight)
                    {
                        itemScore = optIssue.OptionIssueWeight;
                    }

                    this.Value += "," + optIssue.OptionIssueValue;
                }
            }

            if (this.Value.Length > 0)
            {
                this.Value = this.Value.Substring(1);
            }

            this.Rate = itemScore;

            itemScore = Math.Round((itemScore * issueWeight / (decimal)100.00), 2);

            return itemScore;

        }

        /// <summary>
        /// Calc Later Rated Score.
        /// </summary>
        /// <param name="issueWeight">The issue weight.</param>
        /// <param name="laterRatedIssues">the Options from Options which is Under Preference Set.</param>
        /// <param name="messageLaterRatedIssues">The message later rated issues.</param>
        /// <returns>
        /// decimal value represent the score of item
        /// </returns>
        public decimal CalcLaterRatedScore(decimal issueWeight, IEnumerable<LaterRatedIssue> laterRatedIssues, IEnumerable<MessageLaterRatedIssue> messageLaterRatedIssues)
        {

            decimal itemScore = 0;

            //Collectiong all values in one line for later usage
            this.Value = string.Empty;

            foreach (var msgLaterRated in messageLaterRatedIssues)
            {
                //Select from LaterRated under Prefernce Set.
                LaterRatedIssue laterRatedIssue = laterRatedIssues.Where(s => s.LaterRatedIssueID == msgLaterRated.LaterRatedIssueID).FirstOrDefault();

                if (laterRatedIssue != null)
                {
                    if (itemScore < laterRatedIssue.LaterRatedIssueWeight)
                    {
                        itemScore = laterRatedIssue.LaterRatedIssueWeight;
                    }

                    this.Value += "," + laterRatedIssue.LaterRatedIssueValue;
                }
            }

            if (this.Value.Length > 0)
            {
                this.Value = this.Value.Substring(1);
            }

            this.Rate = itemScore;

            itemScore = Math.Round((itemScore * issueWeight / (decimal)100.00), 2);

            return itemScore;

        }
        
        #endregion
        
        #endregion

    }
}
