#region → Usings   .
using System.ComponentModel;
using System.ServiceModel.DomainServices.Client;
#endregion

#region → History  .

/* Date         User              Change
 * 
 * 17.02.11     Yousra Reda         • creation
 * 
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion
namespace citPOINT.PrefApp.Data
{
    /// <summary>
    /// Class encapsulate data binded to report grid and called as Filtered Issue
    /// </summary>
    public class FilteredIssue : Entity, INotifyPropertyChanged
    {
        #region → Fields         .

        private string mIssueName;       
        private string mSentValue;        
        private string mReceivedValue;
        private decimal mSentValueScore;        
        private decimal mReceivedValueScore;        
                
        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets the name of the issue.
        /// </summary>
        /// <value>The name of the issue.</value>
        public string IssueName
        {
            get { return mIssueName; }
            set 
            {
                mIssueName = value;
                RaisePropertyChanged("IssueName");
            }
        }

        /// <summary>
        /// Gets or sets the sent value.
        /// </summary>
        /// <value>The sent value.</value>
        public string SentValue
        {
            get { return mSentValue; }
            set 
            { 
                mSentValue = value;
                RaisePropertyChanged("SentValue");
            }
        }

        /// <summary>
        /// Gets or sets the received value.
        /// </summary>
        /// <value>The received value.</value>
        public string ReceivedValue
        {
            get { return mReceivedValue; }
            set
            { 
                mReceivedValue = value;
                RaisePropertyChanged("ReceivedValue");
            }
        }

        /// <summary>
        /// Gets or sets the sent value score.
        /// </summary>
        /// <value>The sent value score.</value>
        public decimal SentValueScore
        {
            get { return mSentValueScore; }
            set 
            { 
                mSentValueScore = value;
                RaisePropertyChanged("SentValueScore");
            }
        }

        /// <summary>
        /// Gets or sets the received value score.
        /// </summary>
        /// <value>The received value score.</value>
        public decimal ReceivedValueScore
        {
            get { return mReceivedValueScore; }
            set 
            { 
                mReceivedValueScore = value;
                RaisePropertyChanged("ReceivedValueScore");
            }
        }

 
        #endregion

        #region → Events         .
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region → Methods        .

        #region → Private        .
        /// <summary>
        /// Raises the property changed.
        /// </summary>
        /// <param name="PropertyName">Name of the property.</param>
        private void RaisePropertyChanged(string PropertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }
        #endregion

        #endregion
    }
}
