#region → Usings   .
using System;
using System.ServiceModel.DomainServices.Client;
using citPOINT.PrefApp.Data.Web;
using GalaSoft.MvvmLight.Messaging;
using System.IO;
using System.Collections.Generic;
#endregion

#region → History  .

/* Date         User              Change
 * 
 * 13.09.10     Yousra Reda       Creation
 * 13.09.10     Yousra Reda       Create classes for needed messages
 * 27.09.10     M.Wahab           Adding Some XML Comments + EditPreferenceSetMessage
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
    /// Class Used to send and register messaging
    /// </summary>
    public class PrefAppMessanger
    {
        #region → Enums                    .

        /// <summary>
        /// Enumerator represent the set of messages that can be sent
        /// </summary>
        enum MessageTypes
        {
            ChangeScreen,
            SubmitChanges,
            SubmitChangesSendAndMail,
            CancelChanges,
            RaiseError,
            Confirm,
            StatusUpdate,
            EditPreferenceSet,
            EditIssue,
            EditOptionIssue,
            EditNumericIssue,
            EditPreferenceSetNegMessage,
            EditConversationMessage,
            EditNegConversationMessage,
            LoadComplete,
            DataMatchingMessage,
            AddNewPopUpMessage,
            ExportPNG,
            CanExecuteChanged,
            PlotChart,
            RejectChanges,
            DragIssue
        }

        /// <summary>
        /// Enumerator represent the set of PopUp types that exist in that system
        /// </summary>
        public enum PopUpType
        {
            /// <summary>
            /// New Issue
            /// </summary>
            NewIssue,

            /// <summary>
            /// Send Mail
            /// </summary>
            SendMail,

            /// <summary>
            /// Mail Editor
            /// </summary>
            MailEditor,

            /// <summary>
            /// New Option
            /// </summary>
            NewOption,

            /// <summary>
            /// New Later Rated
            /// </summary>
            NewLaterRated,

            /// <summary>
            /// Clone Preference Set
            /// in case if prefence set issues changed 
            /// and you need to effect on some Negotiations only.
            /// </summary>
            ClonePreferenceSet,

            /// <summary>
            /// Publish Preference set to set store.
            /// </summary>
            PublishToSetStore,

            /// <summary>
            /// Publish Preference set to Organization sets.
            /// </summary>
            PublishToOrganization,

            /// <summary>
            /// Publish Preference set to Organization sets.
            /// </summary>
            PublishToOrganizationAndReplace,

            /// <summary>
            /// Publish Preference set to My Sets.
            /// </summary>
            PublishToMySets
        }

        /// <summary>
        /// Define thetypes of operation that can be accomplished
        /// </summary>
        public enum OperationType
        {
            /// <summary>
            /// For indicating That Negotiations which can be added to certain Pref set has been Loaded Completed.
            /// </summary>
            AvailableNegotiationsCompleted
        }

        #endregion Enums Of MessageTypes

        #region → Preference Set           .
        /// <summary>
        /// Class used to set PreferenceSet in edit mode
        /// </summary>
        public static class EditPreferenceSetMessage
        {
            /// <summary>
            /// Send message with a PreferenceSet
            /// </summary>
            /// <param name="CurrentPreferenceSet">Current PreferenceSet</param>
            public static void Send(PreferenceSet CurrentPreferenceSet)
            {
                Messenger.Default.Send<PreferenceSet>(CurrentPreferenceSet, MessageTypes.EditPreferenceSet);
            }

            /// <summary>
            /// Register to receive that type of message
            /// </summary>
            /// <param name="recipient">recipient</param>
            /// <param name="action">method that will be called when message is received</param>
            public static void Register(object recipient, Action<PreferenceSet> action)
            {
                Messenger.Default.Register<PreferenceSet>(recipient, MessageTypes.EditPreferenceSet, action);
            }
        }

        #endregion  Current -(Selected)-Preference Set

        #region → Issues                   .

        /// <summary>
        /// Edit Issue Message
        /// </summary>
        public static class EditIssueMessage
        {
            /// <summary>
            /// Sends the specified issue.
            /// </summary>
            /// <param name="issue">The issue.</param>
            public static void Send(Issue issue)
            {
                Messenger.Default.Send<Issue>(issue, MessageTypes.EditIssue);
            }

            /// <summary>
            /// Registers the specified recipient.
            /// </summary>
            /// <param name="recipient">The recipient.</param>
            /// <param name="action">The action.</param>
            public static void Register(object recipient, Action<Issue> action)
            {
                Messenger.Default.Register<Issue>(recipient, MessageTypes.EditIssue, action);
            }
        }
        
        /// <summary>
        /// Drag Issue Message.
        /// </summary>
        public static class DragIssueMessage
        {
            /// <summary>
            /// Sends the specified issue.
            /// </summary>
            /// <param name="issuesNames">The issues names.</param>
            public static void Send(IEnumerable<string> issuesNames)
            {
                Messenger.Default.Send<IEnumerable<string>>(issuesNames, MessageTypes.DragIssue);
            }

            /// <summary>
            /// Registers the specified recipient.
            /// </summary>
            /// <param name="recipient">The recipient.</param>
            /// <param name="action">The action.</param>
            public static void Register(object recipient, Action<IEnumerable<string>> action)
            {
                Messenger.Default.Register<IEnumerable<string>>(recipient, MessageTypes.DragIssue, action);
            }
        }

        /// <summary>
        /// Add New Issue PopUp
        /// </summary>
        public static class NewPopUp
        {
            #region → Properties  .

            /// <summary>
            /// Gets or sets the type of the pop up.
            /// </summary>
            /// <value>The type of the pop up.</value>
            public static string PopUpType { get; set; }

            /// <summary>
            /// Gets or sets the draged value.
            /// </summary>
            /// <value>The draged value.</value>
            public static string DragedValue { get; set; }
            #endregion

            #region → In Case of Adding Issue
            /// <summary>
            /// Sends the specified issue.
            /// </summary>
            /// <param name="value">The issue.</param>
            /// <param name="type">The type.</param>
            public static void Send(string value, PopUpType type)
            {
                PopUpType = type.ToString();
                Messenger.Default.Send<string>(value, MessageTypes.AddNewPopUpMessage);
            }

            /// <summary>
            /// Registers the specified recipient.
            /// </summary>
            /// <param name="recipient">The recipient.</param>
            /// <param name="action">The action.</param>
            public static void Register(object recipient, Action<string> action)
            {
                Messenger.Default.Register<string>(recipient, MessageTypes.AddNewPopUpMessage, action);
            }
            #endregion

            #region → In Case of Adding Option or Later Rated
            /// <summary>
            /// Sends the specified issue.
            /// </summary>
            /// <param name="issue">The issue.</param>
            /// <param name="value">The value.</param>
            /// <param name="type">The type.</param>
            public static void Send(Issue issue, string value, PopUpType type)
            {
                PopUpType = type.ToString();
                DragedValue = value;
                Messenger.Default.Send<Issue>(issue, MessageTypes.AddNewPopUpMessage);
            }

            /// <summary>
            /// Registers the specified recipient.
            /// </summary>
            /// <param name="recipient">The recipient.</param>
            /// <param name="action">The action.</param>
            public static void Register(object recipient, Action<Issue> action)
            {
                Messenger.Default.Register<Issue>(recipient, MessageTypes.AddNewPopUpMessage, action);
            }
            #endregion
        }

        #endregion

        #region → Option Issue             .

        /// <summary>
        /// Edit Option Issue Message
        /// </summary>
        public static class EditOptionIssueMessage
        {

            //send
            /// <summary>
            /// Sends the specified issue.
            /// </summary>
            /// <param name="optionIssue">The option issue.</param>
            public static void Send(OptionIssue optionIssue)
            {
                Messenger.Default.Send<OptionIssue>(optionIssue, MessageTypes.EditOptionIssue);
            }

            //register
            /// <summary>
            /// Registers the specified recipient.
            /// </summary>
            /// <param name="recipient">The recipient.</param>
            /// <param name="action">The action.</param>
            public static void Register(object recipient, Action<OptionIssue> action)
            {
                Messenger.Default.Register<OptionIssue>(recipient, MessageTypes.EditOptionIssue, action);
            }

        }

        #endregion

        #region → Numeric Issue            .

        /// <summary>
        /// Edit Numeric Issue Message
        /// </summary>
        public static class EditNumericIssueMessage
        {

            //send
            /// <summary>
            /// Sends the specified issue.
            /// </summary>
            /// <param name="numericIssue">The option issue.</param>
            public static void Send(NumericIssue numericIssue)
            {
                Messenger.Default.Send<NumericIssue>(numericIssue, MessageTypes.EditNumericIssue);
            }

            //register
            /// <summary>
            /// Registers the specified recipient.
            /// </summary>
            /// <param name="recipient">The recipient.</param>
            /// <param name="action">The action.</param>
            public static void Register(object recipient, Action<NumericIssue> action)
            {
                Messenger.Default.Register<NumericIssue>(recipient, MessageTypes.EditNumericIssue, action);
            }

        }

        #endregion

        #region → Preference Set Neg       .

        /// <summary>
        /// Edit Preference Set Neg Message
        /// </summary>
        public static class EditPreferenceSetNegMessage
        {

            //send
            /// <summary>
            /// Sends the specified issue.
            /// </summary>
            /// <param name="PreferenceSetNegotiation">The preference set negotiation.</param>
            public static void Send(PreferenceSetNeg PreferenceSetNegotiation)
            {
                Messenger.Default.Send<PreferenceSetNeg>(PreferenceSetNegotiation, MessageTypes.EditPreferenceSetNegMessage);
            }

            //register
            /// <summary>
            /// Registers the specified recipient.
            /// </summary>
            /// <param name="recipient">The recipient.</param>
            /// <param name="action">The action.</param>
            public static void Register(object recipient, Action<PreferenceSetNeg> action)
            {
                Messenger.Default.Register<PreferenceSetNeg>(recipient, MessageTypes.EditPreferenceSetNegMessage, action);
            }

        }

        #endregion

        #region → Conversation Message     .

        /// <summary>
        /// Edit Neg Conversation Message
        /// </summary>
        public static class EditNegConversationMessage
        {
            /// <summary>
            /// Sends the specified entity.
            /// </summary>
            /// <param name="entity">The entity.</param>
            public static void Send(Entity entity)
            {
                Messenger.Default.Send<Entity>(entity, MessageTypes.EditNegConversationMessage);
            }

            /// <summary>
            /// Registers the specified recipient.
            /// </summary>
            /// <param name="recipient">The recipient.</param>
            /// <param name="action">The action.</param>
            public static void Register(object recipient, Action<Entity> action)
            {
                Messenger.Default.Register<Entity>(recipient, MessageTypes.EditNegConversationMessage, action);
            }

        }

        #endregion

        #region → Conversation Message     .

        /// <summary>
        /// Edit Conversation Message
        /// </summary>
        public static class EditConversationMessage
        {

            //send
            /// <summary>
            /// Sends the specified issue.
            /// </summary>
            /// <param name="ConversationMessage">The option issue.</param>
            public static void Send(ConversationMessage ConversationMessage)
            {
                Messenger.Default.Send<ConversationMessage>(ConversationMessage, MessageTypes.EditConversationMessage);
            }

            //register
            /// <summary>
            /// Registers the specified recipient.
            /// </summary>
            /// <param name="recipient">The recipient.</param>
            /// <param name="action">The action.</param>
            public static void Register(object recipient, Action<ConversationMessage> action)
            {
                Messenger.Default.Register<ConversationMessage>(recipient, MessageTypes.EditConversationMessage, action);
            }

        }

        #endregion

        #region → Data matching Message    .

        /// <summary>
        /// Class to Send message of type Datat matching for updating the related Data
        /// when one change the data mathcing for a message.
        /// </summary>
        public static class DataMatchMessage
        {
            /// <summary>
            /// Send this type of message to any recipient who want to register that type of messages
            /// </summary>
            public static void Send(DataMatchingMessage dataMatchingMessage)
            {
                Messenger.Default.Send<DataMatchingMessage>(dataMatchingMessage, MessageTypes.DataMatchingMessage);
            }

            /// <summary>
            /// Register to receive that type of message
            /// </summary>
            /// <param name="recipient">The recipient that has register for this type of message</param>
            /// <param name="action">Method that will be called when message is received</param>
            public static void Register(object recipient, Action<DataMatchingMessage> action)
            {
                Messenger.Default.Register<DataMatchingMessage>(recipient, MessageTypes.DataMatchingMessage, action);
            }
        }

        #endregion

        #region → Change Screen Message    .

        /// <summary>
        /// Class to changes the current screen loaded
        /// </summary>
        public static class ChangeScreenMessage
        {
            /// <summary>
            /// Send this type of message to any recipient who want to register that type of messages
            /// </summary>
            public static void Send(string screenName)
            {
                Messenger.Default.Send<string>(screenName, MessageTypes.ChangeScreen);
            }

            /// <summary>
            /// Register to receive that type of message
            /// </summary>
            /// <param name="recipient">The recipient that has register for this type of message</param>
            /// <param name="action">Method that will be called when message is received</param>
            public static void Register(object recipient, Action<string> action)
            {
                Messenger.Default.Register<string>(recipient, MessageTypes.ChangeScreen, action);
            }
        }

        #endregion

        #region → Flip Message             .

        /// <summary>
        /// Class to flip any flip control
        /// </summary>
        public static class FlippMessage
        {
            /// <summary>
            /// Send this type of message to any recipient who want to register that type of messages
            /// </summary>
            public static void Send()
            {
                Messenger.Default.Send<bool>(true);
            }

            /// <summary>
            /// Send this type of message to any recipient who want to register that type of messages
            /// </summary>
            public static void Send(bool isFlip)
            {
                Messenger.Default.Send<bool>(isFlip);
            }

            /// <summary>
            /// Send this type of message to any recipient who want to register that type of messages
            /// </summary>
            public static void Send(string ViewName)
            {

                Messenger.Default.Send<string>(ViewName);
            }

            /// <summary>
            /// Sends the specified entity.
            /// </summary>
            /// <param name="entity">The entity.</param>
            public static void Send(Entity entity)
            {
                Messenger.Default.Send<Entity>(entity);
            }

            /// <summary>
            /// Register to receive that type of message
            /// </summary>
            /// <param name="recipient">The recipient that has register for this type of message</param>
            /// <param name="action">Method that will be called when message is received</param>
            public static void Register(object recipient, Action<bool> action)
            {
                Messenger.Default.Register<bool>(recipient, action);
            }

            /// <summary>
            /// Register to recieve that type of message
            /// </summary>
            /// <param name="recipient">The recipient that has register for this type of message</param>
            /// <param name="action">Method that will be called when message is recieved</param>
            public static void Register(object recipient, Action<string> action)
            {
                Messenger.Default.Register<string>(recipient, action);
            }

            /// <summary>
            /// Registers the specified recipient.
            /// </summary>
            /// <param name="recipient">The recipient.</param>
            /// <param name="action">The action.</param>
            public static void Register(object recipient, Action<Entity> action)
            {
                Messenger.Default.Register<Entity>(recipient, action);
            }
        }

        #endregion

        #region → Status Update Message    .

        /// <summary>
        /// Class to update status
        /// </summary>
        public static class StatusUpdateMessage
        {
            /// <summary>
            /// Send this type of message to any recipient who want to register that type of messages
            /// </summary>
            public static void Send(DialogMessage dialogMessage)
            {
                Messenger.Default.Send<DialogMessage>(dialogMessage, MessageTypes.StatusUpdate);
            }

            /// <summary>
            /// Register to receive that type of message
            /// </summary>
            /// <param name="recipient">The recipient that has register for this type of message</param>
            /// <param name="action">Method that will be handle the exception send and appear friendly message</param>
            public static void Register(object recipient, Action<DialogMessage> action)
            {
                Messenger.Default.Register<DialogMessage>(recipient, MessageTypes.StatusUpdate, action);
            }
        }
        #endregion

        #region → Submit Changes Message   .

        /// <summary>
        /// Class to submit any pending changes
        /// </summary>
        public static class SubmitChangesMessage
        {
            /// <summary>
            /// Send this type of message to any recipient who want to register that type of messages
            /// </summary>
            /// <param name="value">if set to <c>true</c> [value].</param>
            public static void Send(bool value = false)
            {
                Messenger.Default.Send<Boolean>(value, MessageTypes.SubmitChanges);
            }

            /// <summary>
            /// Register to receive that type of message
            /// </summary>
            /// <param name="recipient">The recipient that has register for this type of message</param>
            /// <param name="action">Method that will be called when message is received</param>
            public static void Register(object recipient, Action<Boolean> action)
            {
                Messenger.Default.Register<Boolean>(recipient, MessageTypes.SubmitChanges, action);
            }

            /// <summary>
            /// Send this type of message to any recipient who want to register that type of messages
            /// </summary>
            /// <param name="value">if set to <c>true</c> [value].</param>
            public static void SendSubmitAndMail(PreferenceSet value)
            {
                Messenger.Default.Send<PreferenceSet>(value, MessageTypes.SubmitChangesSendAndMail);
            }

            /// <summary>
            /// Register to receive that type of message
            /// </summary>
            /// <param name="recipient">The recipient that has register for this type of message</param>
            /// <param name="action">Method that will be called when message is received</param>
            public static void RegisterSubmitAndMail(object recipient, Action<PreferenceSet> action)
            {
                Messenger.Default.Register<PreferenceSet>(recipient, MessageTypes.SubmitChangesSendAndMail, action);
            }

        }
        #endregion

        #region → Cancel Changes Message   .


        /// <summary>
        /// Class to reject any pending changes
        /// </summary>
        public static class CancelChangesMessage
        {
            /// <summary>
            /// Send this type of message to any recipient who want to register that type of messages
            /// </summary>
            public static void Send()
            {
                Messenger.Default.Send<Boolean>(true, MessageTypes.CancelChanges);
            }

            /// <summary>
            /// Register to receive that type of message
            /// </summary>
            /// <param name="recipient">The recipient that has register for this type of message</param>
            /// <param name="action">Method that will be called when message is recieved</param>
            public static void Register(object recipient, Action<Boolean> action)
            {
                Messenger.Default.Register<Boolean>(recipient, MessageTypes.CancelChanges, action);
            }
        }
        #endregion

        #region → Raise Error Message      .
        /// <summary>
        /// Class to handle any raised exception
        /// </summary>
        public static class RaiseErrorMessage
        {
            /// <summary>
            /// Send this type of message to any recipient who want to register that type of messages
            /// </summary>
            public static void Send(Exception ex)
            {
                Messenger.Default.Send<Exception>(ex, MessageTypes.RaiseError);
            }

            /// <summary>
            /// Register to receive that type of message
            /// </summary>
            /// <param name="recipient">The recipient that has register for this type of message</param>
            /// <param name="action">Method that will be handle the excption send and appear friendly message</param>
            public static void Register(object recipient, Action<Exception> action)
            {
                Messenger.Default.Register<Exception>(recipient, MessageTypes.RaiseError, action);
            }
        }

        #endregion

        #region → LoadOperationComplete    .


        /// <summary>
        /// Class to send recieve messages when certain operation complete
        /// </summary>
        public static class LoadCompleted
        {
            /// <summary>
            /// Send this type of message to any recipient who want to register that type of messages
            /// </summary>
            /// <param name="OperationName">Name of the operation.</param>
            public static void Send(string OperationName)
            {
                Messenger.Default.Send<string>(OperationName, MessageTypes.LoadComplete);
            }

            /// <summary>
            /// Register to recieve that type of message
            /// </summary>
            /// <param name="recipient">The recipient that has register for this type of message</param>
            /// <param name="action">Method that will be called when message is recieved</param>
            public static void Register(object recipient, Action<string> action)
            {
                Messenger.Default.Register<string>(recipient, MessageTypes.LoadComplete, action);
            }
        }
        #endregion

        #region → RefreshSource            .

        /// <summary>
        /// Class to send recieve messages when need to refresh certain Source
        /// </summary>
        public static class RefreshSource
        {


            /// <summary>
            /// Gets the issues source.
            /// </summary>
            /// <value>The issues source.</value>
            public static string IssuesSource { get { return "IssuesSource"; } }

            /// <summary>
            /// Gets the preference set deleted.
            /// </summary>
            /// <value>The preference set deleted.</value>
            public static string PreferenceSetDeleted { get { return "PreferenceSetDeleted"; } }

            /// <summary>
            /// Gets the preference set changed.
            /// In case if changes happent to current assigned preference set and it has 2 or more negotiation assigned
            /// </summary>
            /// <value>The preference set changed.</value>
            public static string PreferenceSetChanged { get { return "PreferenceSetChanged"; } }

            /// <summary>
            /// Send this type of message to any recipient who want to register that type of messages
            /// </summary>
            /// <param name="sourceName">Name of the Source.</param>
            public static void Send(string sourceName)
            {
                Messenger.Default.Send<string>(sourceName, MessageTypes.LoadComplete);
            }

            /// <summary>
            /// Register to recieve that type of message
            /// </summary>
            /// <param name="recipient">The recipient that has register for this type of message</param>
            /// <param name="action">Method that will be called when message is recieved</param>
            public static void Register(object recipient, Action<string> action)
            {
                Messenger.Default.Register<string>(recipient, MessageTypes.LoadComplete, action);
            }
        }
        #endregion

        #region → ExportReport             .

        /// <summary>
        /// Class to send recieve messages when need to refresh certain Source
        /// </summary>
        public static class ExportReport
        {
            /// <summary>
            /// Sends the specified stream.
            /// </summary>
            /// <param name="stream">The stream.</param>
            public static void Send(Stream stream)
            {
                Messenger.Default.Send<Stream>(stream, MessageTypes.ExportPNG);
            }

            /// <summary>
            /// Registers the specified recipient.
            /// </summary>
            /// <param name="recipient">The recipient.</param>
            /// <param name="action">The action.</param>
            public static void Register(object recipient, Action<Stream> action)
            {
                Messenger.Default.Register<Stream>(recipient, MessageTypes.ExportPNG, action);
            }
        }
        #endregion

        #region → RefreshSource            .

        /// <summary>
        /// Class to send recieve messages when need to refresh certain Source
        /// </summary>
        public static class PlotChartRefreshSource
        {
            /// <summary>
            /// Send this type of message to any recipient who want to register that type of messages
            /// </summary>
            /// <param name="SourceName">Name of the Source.</param>
            public static void Send(string SourceName)
            {
                Messenger.Default.Send<string>(SourceName, MessageTypes.LoadComplete);
            }

            /// <summary>
            /// Register to recieve that type of message
            /// </summary>
            /// <param name="recipient">The recipient that has register for this type of message</param>
            /// <param name="action">Method that will be called when message is recieved</param>
            public static void Register(object recipient, Action<string> action)
            {
                Messenger.Default.Register<string>(recipient, MessageTypes.LoadComplete, action);
            }
        }
        #endregion

    }
}
