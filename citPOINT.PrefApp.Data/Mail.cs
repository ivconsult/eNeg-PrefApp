
#region → Usings   .
using System.ComponentModel;
using System.ServiceModel.DomainServices.Client;
#endregion

#region → History  .

/* Date         User              Change
 * 
 * 03.02.11     Yousra Reda         • creation
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
    /// Class encapsulate mail params that will be sent to Negotiators
    /// </summary>
    public class Mail : Entity, INotifyPropertyChanged
    {
        #region → Fields         .
        private string mSender;
        private string mReceiver;
        private string mSubject;
        private string mBody;
        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets the sender.
        /// </summary>
        /// <value>The sender.</value>
        public string Sender
        {
            get
            {
                return mSender;
            }
            set
            {
                mSender = value;
                RaisePropertyChanged("Sender");
            }
        }

        /// <summary>
        /// Gets or sets the receiver.
        /// </summary>
        /// <value>The receiver.</value>
        public string Receiver
        {
            get { return mReceiver; }
            set
            {
                mReceiver = value;
                RaisePropertyChanged("Reciever");
            }
        }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <value>The subject.</value>
        public string Subject
        {
            get { return mSubject; }
            set
            {
                mSubject = value;
                RaisePropertyChanged("Subject");
            }
        }

        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        /// <value>The body.</value>
        public string Body
        {
            get { return mBody; }
            set
            {
                mBody = value;
                RaisePropertyChanged("Body");
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
