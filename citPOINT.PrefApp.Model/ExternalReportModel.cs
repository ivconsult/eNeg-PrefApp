#region → Usings   .
using citPOINT.eNeg.Common;
using citPOINT.PrefApp.Common;
using citPOINT.PrefApp.Data.Web;
using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ServiceModel.DomainServices.Client;

#endregion

#region → History  .

/* Date         User              Change
 * 
 * 10.01.11     M.Wahab           • Creation
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

namespace citPOINT.PrefApp.Model
{

    #region Using MEF to export External Report Model

    /// <summary>
    /// Data Matching Model.
    /// </summary>
    [Export(typeof(IExternalReportModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    #endregion
    public class ExternalReportModel : IExternalReportModel
    {

        #region → Fields         .
        private PrefAppContext mPrefAppContext;
        private Boolean mHasChanges = false;
        private Boolean mIsBusy = false;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Context of Service eNegService
        /// </summary>
        public PrefAppContext Context
        {
            get
            {
                if (mPrefAppContext == null)
                {
                    mPrefAppContext = new PrefAppContext(PrefAppConfigurations.MainServiceUri);
                    mPrefAppContext.PropertyChanged += new PropertyChangedEventHandler(mPrefAppContext_PropertyChanged);
                }
                return mPrefAppContext;
            }

        }

        /// <summary>
        /// True if Domain context Has Changes ;otherwise false
        /// </summary>
        public Boolean HasChanges
        {
            get
            {
                return this.mHasChanges;
            }
            private set
            {


                this.mHasChanges = value;
                this.OnPropertyChanged("HasChanges");

            }
        }

        /// <summary>
        /// True if either "IsLoading" or "IsSubmitting" is
        /// in progress; otherwise, false
        /// </summary>
        public Boolean IsBusy
        {
            get
            {
                return this.mIsBusy;
            }

            private set
            {
                this.mIsBusy = value;
                this.OnPropertyChanged("IsBusy");

            }
        }

        #endregion

        #region → Event Handlers .

        /// <summary>
        /// Executed when any property of Domain context is changed
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">PropertyChangedEventArgs</param>
        private void mPrefAppContext_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "HasChanges":
                    this.HasChanges = mPrefAppContext.HasChanges;
                    break;
                case "IsLoading":
                    this.IsBusy = mPrefAppContext.IsLoading;
                    break;
                case "IsSubmitting":
                    this.IsBusy = mPrefAppContext.IsSubmitting;
                    break;
            }
        }
        #endregion

        #region → Events         .


        /// <summary>
        /// PropertyChanged Callback
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Occurs when [get last sent message complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<ConversationMessage>> GetLastSentMessageComplete;

        /// <summary>
        /// Occurs when [get last received message complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<ConversationMessage>> GetLastReceivedMessageComplete;

        /// <summary>
        /// Occurs when [get preference set complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<PreferenceSet>> GetPreferenceSetByIDComplete;

        /// <summary>
        /// Occurs when [get preference set neg complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<PreferenceSetNeg>> GetPreferenceSetNegComplete;


        /// <summary>
        /// Occurs when [get issues by negotiation ID complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<Issue>> GetIssuesComplete;

        /// <summary>
        /// Occurs when [get option issues complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<OptionIssue>> GetOptionIssuesComplete;

        /// <summary>
        /// Occurs when [get numeric issues complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<NumericIssue>> GetNumericIssuesComplete;

        /// <summary>
        /// Occurs when [get later rated issue complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<LaterRatedIssue>> GetLaterRatedIssueComplete;

        /// <summary>
        /// Occurs when [get message issues by message ID complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<MessageIssue>> GetMessageIssuesByMessageIDComplete;

        /// <summary>
        /// Occurs when [get message option issue by message ID complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<MessageOptionIssue>> GetMessageOptionIssueByMessageIDComplete;

        /// <summary>
        /// Occurs when [get message later rated issue by message ID complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<MessageLaterRatedIssue>> GetMessageLaterRatedIssueByMessageIDComplete;



        #endregion

        #region → Methods        .

        #region → Private        .
        
        /// <summary>
        /// Private Method used to perform query on certain entity of PrefApp Entities
        /// </summary>
        /// <typeparam name="T">Value Of T</typeparam>
        /// <param name="qry">Value Of qry</param>
        /// <param name="evt">Value Of evt</param>
        private void PerformQuery<T>(EntityQuery<T> qry, EventHandler<eNegEntityResultArgs<T>> evt) where T : Entity
        {
            Context.Load<T>(qry, LoadBehavior.RefreshCurrent, r =>
            {
                if (evt != null)
                {
                    try
                    {
                        if (r.HasError)
                        {
                            evt(this, new eNegEntityResultArgs<T>(r.Error));
                            r.MarkErrorAsHandled();
                        }
                        else
                        {
                            evt(this, new eNegEntityResultArgs<T>(r.Entities));
                        }
                    }
                    catch (Exception ex)
                    {
                        evt(this, new eNegEntityResultArgs<T>(ex));
                    }
                }
            }, null);
        }
        
        #endregion

        #region → Protected      .
        
        #region "INotifyPropertyChanged Interface implementation"

        /// <summary>
        /// called On Property Changed
        /// </summary>
        /// <param name="propertyName">property name</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion "INotifyPropertyChanged Interface implementation"

        #endregion

        #region → Public         .
        
        #region IExternalReportModel Interface Implementation
        
        /// <summary>
        /// Gets the messate query.
        /// </summary>
        /// <param name="isSent">if set to <c>true</c> [is sent].</param>
        /// <returns></returns>
        private EntityQuery<ConversationMessage> GetMessateQuery(bool isSent)
        {
            EntityQuery<ConversationMessage> messageQry = Context.GetConversationMessagesQuery()
                     .Where(s => s.Deleted == false && //Not Deleted
                                 s.IsSent == isSent &&     //Sent or Received Message
                                 s.RatedDate != null && //Mean that it is rated Message. 
                                 s.NegConversation.PreferenceSetNeg.NegID == PrefAppConfigurations.NegotiationIDParameter.Value);


            // in case the Statastical on Conversation Level
            if (PrefAppConfigurations.ConversationIDParameter.HasValue)
            {
                messageQry = messageQry.Where(s => s.NegConversation.ConversationID == PrefAppConfigurations.ConversationIDParameter.Value);
            }

            return messageQry;

        }
      
        /// <summary>
        /// Gets the last sent message async.
        /// </summary>
        public void GetLastSentMessageAsync()
        {
            PerformQuery<ConversationMessage>
                (
                  GetMessateQuery(true).OrderByDescending(d => d.RatedDate).Take(1),
                  GetLastSentMessageComplete
                );
        }

        /// <summary>
        /// Gets the last Received message async.
        /// </summary>
        public void GetLastReceivedMessageAsync()
        {
            PerformQuery<ConversationMessage>
                (
                  GetMessateQuery(false).OrderByDescending(d => d.RatedDate).Take(1),
                  GetLastReceivedMessageComplete
                );
        }

        /// <summary>
        /// Gets the preference set by preference ID async.
        /// </summary>
        /// <param name="PreferenceID">The preference ID.</param>
        public void GetPreferenceSetByIDAsync(Guid PreferenceID)
        {
            PerformQuery<PreferenceSet>(Context.GetPreferenceSetsByIDQuery(PreferenceID), GetPreferenceSetByIDComplete);
        }
        
        #region → Preference Set Details         .

        /// <summary>
        /// Gets the preference set neg async.
        /// </summary>
        public void GetPreferenceSetNegAsync()
        {
            PerformQuery<PreferenceSetNeg>(
              Context.GetPreferenceSetNegsQuery()
                     .Where(s => s.Deleted == false && //Not Deleted
                            s.NegID == PrefAppConfigurations.NegotiationIDParameter.Value)
                     .OrderByDescending(d => d.DeletedOn),
            GetPreferenceSetNegComplete);

        }
        
        /// <summary>
        /// Gets the issues by preference ID async.
        /// </summary>
        /// <param name="PreferenceID">The preference ID.</param>
        public void GetIssuesByPreferenceIDAsync(Guid PreferenceID)
        {

            PerformQuery<Issue>(
                Context.GetIssuesQuery()
                       .Where(s => s.Deleted == false && //Not Deleted
                              s.PreferenceSetID == PreferenceID)
                       .OrderByDescending(d => d.DeletedOn),
                GetIssuesComplete);
        }
        
        /// <summary>
        /// Gets the option issues by preference ID async.
        /// </summary>
        /// <param name="PreferenceID">The preference ID.</param>
        public void GetOptionIssuesByPreferenceIDAsync(Guid PreferenceID)
        {
            PerformQuery<OptionIssue>(
                Context.GetOptionIssuesQuery()
                       .Where(s => s.Deleted == false && //Not Deleted
                              s.Issue.PreferenceSetID == PreferenceID)
                       .OrderByDescending(d => d.DeletedOn),
                GetOptionIssuesComplete);
        }
        
        /// <summary>
        /// Gets the numeric issues by preference ID async.
        /// </summary>
        /// <param name="PreferenceID">The preference ID.</param>
        public void GetNumericIssuesByPreferenceIDAsync(Guid PreferenceID)
        {
            PerformQuery<NumericIssue>(
                Context.GetNumericIssuesQuery()
                       .Where(s => s.Deleted == false && //Not Deleted
                              s.Issue.PreferenceSetID == PreferenceID)
                       .OrderByDescending(d => d.DeletedOn),
                GetNumericIssuesComplete);
        }
        
        /// <summary>
        /// Gets the later rated by preference ID async.
        /// </summary>
        /// <param name="PreferenceID">The preference ID.</param>
        public void GetLaterRatedByPreferenceIDAsync(Guid PreferenceID)
        {
            PerformQuery<LaterRatedIssue>(
                Context.GetLaterRatedIssuesQuery()
                       .Where(s => s.Deleted == false && //Not Deleted
                              s.Issue.PreferenceSetID == PreferenceID)
                       .OrderByDescending(d => d.DeletedOn),
                GetLaterRatedIssueComplete);
        }
        
        #endregion

        /// <summary>
        /// Gets the message issues by message ID async.
        /// </summary>
        /// <param name="MessageID">The message ID.</param>
        public void GetMessageIssuesByMessageIDAsync(Guid MessageID)
        {
            PerformQuery<MessageIssue>(
                Context.GetMessageIssuesQuery()
                       .Where(s => s.Deleted == false && //Not Deleted
                              s.ConversationMessageID == MessageID)
                       .OrderByDescending(d => d.DeletedOn),
                GetMessageIssuesByMessageIDComplete);
        }

        /// <summary>
        /// Gets the message option issue by message ID async.
        /// </summary>
        /// <param name="MessageID">The message ID.</param>
        public void GetMessageOptionIssueByMessageIDAsync(Guid MessageID)
        {
            PerformQuery<MessageOptionIssue>(
                Context.GetMessageOptionIssuesQuery()
                       .Where(s => s.Deleted == false && //Not Deleted
                              s.MessageIssue.ConversationMessageID == MessageID)
                       .OrderByDescending(d => d.DeletedOn),
                GetMessageOptionIssueByMessageIDComplete);
        }
        
        /// <summary>
        /// Gets the message later rated issue by message ID async.
        /// </summary>
        /// <param name="MessageID">The message ID.</param>
        public void GetMessageLaterRatedIssueByMessageIDAsync(Guid MessageID)
        {
            PerformQuery<MessageLaterRatedIssue>(
                Context.GetMessageLaterRatedIssuesQuery()
                       .Where(s => s.Deleted == false && //Not Deleted
                              s.MessageIssue.ConversationMessageID == MessageID)
                       .OrderByDescending(d => d.DeletedOn),
                 GetMessageLaterRatedIssueByMessageIDComplete);
        }
        
        #endregion IPrefernceSetsModel Interface Implementation

        #endregion

        #endregion

    }
}
