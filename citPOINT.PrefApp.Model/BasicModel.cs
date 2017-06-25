
#region → Usings   .
using System;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel.DomainServices.Client;
using System.ComponentModel.Composition;
using System.Linq.Expressions;
using System.Collections.Generic;
using citPOINT.PrefApp.Data.Web;
using citPOINT.eNeg.Common;
using citPOINT.PrefApp.Common;
using citPOINT.PrefApp.Common.Helper;
#endregion

#region → History  .

/* Date         User            Change
 * 
 * 26.03.12     Yousra Reda     creation
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
    /// <summary>
    /// Model Layer for the basic items will be needed in most view models
    /// </summary>
    #region  Using MEF to export to BasicViewModel

    [Export(typeof(IBasicModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    #endregion
    public class BasicModel : IBasicModel
    {
        #region → Fields         .

        private PrefAppContext mPrefAppContext;

        public bool mIsBusy = false;
        private bool mHasChanges = false;

        #endregion Fields

        #region → Properties     .

        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>The context.</value>
        public PrefAppContext Context
        {
            get
            {
                if (mPrefAppContext == null)
                {
                    mPrefAppContext = Repository.Context;
                    mPrefAppContext.PropertyChanged += new PropertyChangedEventHandler(mPrefAppContext_PropertyChanged);
                }
                return mPrefAppContext;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is busy.
        /// </summary>
        /// <value><c>true</c> if this instance is busy; otherwise, <c>false</c>.</value>
        public bool IsBusy
        {
            get
            {
                return mIsBusy;
            }
            private set
            {
                if (mIsBusy != value)
                {
                    mIsBusy = value;
                    OnPropertyChnaged("IsBusy");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has changes.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has changes; otherwise, <c>false</c>.
        /// </value>
        public bool HasChanges
        {
            get
            {
                return mHasChanges;
            }
            private set
            {
                if (mHasChanges != value)
                {
                    mHasChanges = value;
                    OnPropertyChnaged("HasChanges");
                }
            }
        }


        #endregion Properties

        #region → Event Handlers .

        /// <summary>
        /// Handles the PropertyChanged event of the mNegContext control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void mPrefAppContext_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "HasChanges":
                    this.HasChanges = mPrefAppContext.HasChanges;
                    break;
                case "IsBusy":
                    this.IsBusy = mPrefAppContext.IsLoading;
                    break;
            }
        }

        #endregion Event Handlers

        #region → Events         .

        /// <summary>
        /// Occurs when [get current preference set completed].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<PreferenceSet>> GetCurrentPreferenceSetCompleted;

        /// <summary>
        /// Occurs when [get current pref set neg by ID async completed].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<PreferenceSetNeg>> GetCurrentPrefSetNegByIDAsyncCompleted;

        /// <summary>
        /// Occurs when [get current neg conversation by ID async completed].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<NegConversation>> GetCurrentNegConversationByIDAsyncCompleted;

        /// <summary>
        /// Get All Negotiations Conversations CallBack
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<NegConversation>> GetNegotiationConversationsComplete;

        /// <summary>
        /// Occurs when [get conversation messages complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<ConversationMessage>> GetConversationMessagesComplete;

        /// <summary>
        /// Occurs when [get message issues complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<MessageIssue>> GetMessageIssuesComplete;

        /// <summary>
        /// Occurs when [get message option issues complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<MessageOptionIssue>> GetMessageOptionIssuesComplete;

        /// <summary>
        /// Occurs when [get message later rated issues complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<MessageLaterRatedIssue>> GetMessageLaterRatedIssuesComplete;

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Performs the users query.
        /// </summary>
        /// <typeparam name="T">The Entity.</typeparam>
        /// <param name="qry">The qry.</param>
        /// <param name="evt">The evt.</param>
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

        #endregion Private

        #region → Protected      .

        /// <summary>
        /// Called when [property chnaged].
        /// </summary>
        /// <param name="PropertyName">Name of the property.</param>
        protected virtual void OnPropertyChnaged(string PropertyName)
        {
            if (PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
            }
        }

        #endregion Protected

        #region → Public         .

        #region IBasicModel Interface implementation

        /// <summary>
        /// Gets the current preference set async.
        /// </summary>
        /// <param name="negotiationID">The negotiation ID.</param>
        public void GetCurrentPreferenceSetAsync(Guid negotiationID)
        {
            PerformQuery<PreferenceSet>(Context.GetPreferenceSetForNegotiationQuery(negotiationID)
                                                  .Where(s => s.Deleted == false),
                                           GetCurrentPreferenceSetCompleted);
        }

        /// <summary>
        /// Gets the current pref set neg by ID async.
        /// </summary>
        /// <param name="negotiationID">The negotiation ID.</param>
        public void GetCurrentPrefSetNegByIDAsync(Guid negotiationID)
        {
            PerformQuery<PreferenceSetNeg>(Context.GetPreferenceSetNegsQuery()
                                                  .Where(s => s.NegID == negotiationID
                                                         && s.Deleted == false),
                                           GetCurrentPrefSetNegByIDAsyncCompleted);
        }

        /// <summary>
        /// Gets the current neg conversation by ID async.
        /// </summary>
        /// <param name="conversationID">The conversation ID.</param>
        public void GetCurrentNegConversationByIDAsync(Guid conversationID)
        {
            PerformQuery<NegConversation>(Context.GetNegConversationsQuery()
                                                  .Where(s => s.ConversationID == conversationID
                                                         && s.Deleted == false),
                                          GetCurrentNegConversationByIDAsyncCompleted);
        }

        /// <summary>
        /// Gets the negotiation conversations by neg ID async.
        /// </summary>
        /// <param name="prefSetNegID">The pref set neg ID.</param>
        public void GetNegotiationConversationsByNegIDAsync(Guid prefSetNegID)
        {
            PerformQuery<NegConversation>(Context.GetNegConversationsQuery()
                                                 .Where(s => s.PreferenceSetNegID == prefSetNegID
                                                         && s.Deleted == false
                                                         && s.DeletedBy == PrefAppConfigurations.CurrentLoginUser.UserID)
                                                 .OrderBy(s => s.DeletedOn),
                                          GetNegotiationConversationsComplete);
        }

        /// <summary>
        /// Gets the conversation messages by conv ID async.
        /// </summary>
        /// <param name="ConvIDs">The conv I ds.</param>
        public void GetConversationMessagesByConvIDsAsync(Guid[] ConvIDs)
        {
            PerformQuery<ConversationMessage>(Context.GetConvMessagesByConvIDsQuery(ConvIDs),
                                              GetConversationMessagesComplete);
        }

        /// <summary>
        /// Gets the message issues async.
        /// </summary>
        public void GetMessageIssuesAsync(Guid[] msgIDs)
        {
            PerformQuery<MessageIssue>(Context.GetMessageIssuesByNegIDsQuery(msgIDs),
                                       GetMessageIssuesComplete);
        }

        /// <summary>
        /// Gets the message option issues async.
        /// </summary>
        public void GetMessageOptionIssuesAsync(Guid[] msgIssueIDs)
        {
            PerformQuery<MessageOptionIssue>(Context.GetMessageOptionIssuesByNegIDsQuery(msgIssueIDs),
                                             GetMessageOptionIssuesComplete);
        }

        /// <summary>
        /// Gets the message later rated issues async.
        /// </summary>
        public void GetMessageLaterRatedIssuesAsync(Guid[] msgIssueIDs)
        {
            PerformQuery<MessageLaterRatedIssue>(Context.GetMessageLaterRatedIssuesByNegIDsQuery(msgIssueIDs),
                                                 GetMessageLaterRatedIssuesComplete);
        }

        /// <summary>
        /// Cleans up.
        /// </summary>
        public void CleanUp()
        {
            Repository.Context = null;
        }

        #endregion IBasicModel Interface implementation

        #endregion Public

        #endregion Methods
    }
}
