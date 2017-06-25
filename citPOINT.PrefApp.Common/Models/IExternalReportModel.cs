#region → Usings   .
using citPOINT.eNeg.Common;
using citPOINT.PrefApp.Data.Web;
using System;
using System.ComponentModel;
#endregion

#region → History  .

/* Date         User            Change
 * 
 * 10.01.11     M.Wahab         • creation
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

namespace citPOINT.PrefApp.Common
{
    /// <summary>
    /// Interface for  Data Matching Model
    /// </summary>
    public interface IExternalReportModel : INotifyPropertyChanged
    {
        #region → Properties     .

        /// <summary>
        /// True if Domain context Has Changes ;otherwise false
        /// </summary>
        bool HasChanges { get; }

        /// <summary>
        /// True if either "IsLoading" or "IsSubmitting" is
        /// in progress; otherwise, false
        /// </summary>
        bool IsBusy { get; }


        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>The context.</value>
        PrefAppContext Context { get; }
        #endregion Properties

        #region → Events         .

        /// <summary>
        /// PropertyChanged Callback
        /// </summary>
        event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Occurs when [get last sent message complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<ConversationMessage>> GetLastSentMessageComplete;

        /// <summary>
        /// Occurs when [get last received message complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<ConversationMessage>> GetLastReceivedMessageComplete;

        /// <summary>
        /// Occurs when [get preference set complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<PreferenceSet>> GetPreferenceSetByIDComplete;

        /// <summary>
        /// Occurs when [get preference set neg complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<PreferenceSetNeg>> GetPreferenceSetNegComplete;

        /// <summary>
        /// Occurs when [get issues complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<Issue>> GetIssuesComplete;
        /// <summary>
        /// Occurs when [get option issues complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<OptionIssue>> GetOptionIssuesComplete;
        /// <summary>
        /// Occurs when [get numeric issues complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<NumericIssue>> GetNumericIssuesComplete;
        /// <summary>
        /// Occurs when [get later rated issue complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<LaterRatedIssue>> GetLaterRatedIssueComplete;

        /// <summary>
        /// Occurs when [get message issues by message ID complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<MessageIssue>> GetMessageIssuesByMessageIDComplete;
        /// <summary>
        /// Occurs when [get message option issue by message ID complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<MessageOptionIssue>> GetMessageOptionIssueByMessageIDComplete;
        /// <summary>
        /// Occurs when [get message later rated issue by message ID complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<MessageLaterRatedIssue>> GetMessageLaterRatedIssueByMessageIDComplete;

        #endregion Events

        #region → Methods        .

        /// <summary>
        /// Gets the last sent message async.
        /// </summary>
        void GetLastSentMessageAsync();

        /// <summary>
        /// Gets the last received message async.
        /// </summary>
        void GetLastReceivedMessageAsync();

        /// <summary>
        /// Gets the preference set by preference ID async.
        /// </summary>
        /// <param name="PreferenceID">The preference ID.</param>
        void GetPreferenceSetByIDAsync(Guid PreferenceID);

        /// <summary>
        /// Gets the preference set neg async.
        /// </summary>
        void GetPreferenceSetNegAsync();

        /// <summary>
        /// Gets the issues by preference ID async.
        /// </summary>
        /// <param name="PreferenceID">The preference ID.</param>
        void GetIssuesByPreferenceIDAsync(Guid PreferenceID);

        /// <summary>
        /// Gets the option issues by preference ID async.
        /// </summary>
        /// <param name="PreferenceID">The preference ID.</param>
        void GetOptionIssuesByPreferenceIDAsync(Guid PreferenceID);

        /// <summary>
        /// Gets the numeric issues by preference ID async.
        /// </summary>
        /// <param name="PreferenceID">The preference ID.</param>
        void GetNumericIssuesByPreferenceIDAsync(Guid PreferenceID);

        /// <summary>
        /// Gets the later rated by preference ID async.
        /// </summary>
        /// <param name="PreferenceID">The preference ID.</param>
        void GetLaterRatedByPreferenceIDAsync(Guid PreferenceID);

        /// <summary>
        /// Gets the message issues by message ID async.
        /// </summary>
        /// <param name="MessageID">The message ID.</param>
        void GetMessageIssuesByMessageIDAsync(Guid MessageID);

        /// <summary>
        /// Gets the message option issue by message ID async.
        /// </summary>
        /// <param name="MessageID">The message ID.</param>
        void GetMessageOptionIssueByMessageIDAsync(Guid MessageID);

        /// <summary>
        /// Gets the message later rated issue by message ID async.
        /// </summary>
        /// <param name="MessageID">The message ID.</param>
        void GetMessageLaterRatedIssueByMessageIDAsync(Guid MessageID);
        #endregion Methods


    }
}
