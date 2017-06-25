#region → Usings   .
using citPOINT.PrefApp.Data.Web;
#endregion

#region → History  .

/* Date         User             • Change
 * 
 * 19.01.11     M.Wahab          •→ Creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 */

# endregion


namespace citPOINT.PrefApp.Common
{
    /// <summary>
    /// Class that contain all available Views
    /// </summary>
    public sealed class DataMatchingMessage
    {
        
        #region → Fields         .

        private Issue mCurrentIssue;
        private ConversationMessage mCurrentMessage;
        private string mOptionValue;
        private bool mIsChecked;

        #endregion

        #region → Properties     .
        
        /// <summary>
        /// Gets or sets the current issue.
        /// </summary>
        /// <value>The current issue.</value>
        public Issue CurrentIssue
        {
            get
            {
                return mCurrentIssue;
            }
            set
            {
                mCurrentIssue = value;
            }
        }

        /// <summary>
        /// Gets or sets the current message.
        /// </summary>
        /// <value>The current message.</value>
        public ConversationMessage CurrentMessage
        {
            get
            {
                return mCurrentMessage;
            }
            set
            {
                mCurrentMessage = value;
            }
        }
        
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public string Value
        {
            get
            {
                return mOptionValue;
            }
            set
            {
                mOptionValue = value;
            }
        }
        
        /// <summary>
        /// Gets or sets a value indicating whether this instance is checked.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is checked; otherwise, <c>false</c>.
        /// </value>
        public bool IsChecked
        {
            get
            {
                return mIsChecked;
            }
            set
            {
                mIsChecked = value;
            }
        }

        #endregion
        
    }
}
