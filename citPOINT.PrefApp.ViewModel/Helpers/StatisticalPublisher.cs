
#region → Usings   .
using System;
using System.ComponentModel;
using System.Linq;
using System.Collections.Generic;
using citPOINT.PrefApp.Data.Web;
using citPOINT.PrefApp.Common;
using System.ServiceModel.DomainServices.Client;

#endregion

#region → History  .

/* 
 * Date         User            Change
 * *********************************************
 * 4/12/2011 4:02:43 PM      mwahab         • creation
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

namespace citPOINT.PrefApp.ViewModel
{

    /// <summary>
    /// Class for Statistical Publisher 
    /// (send messages to eNeg using RESET)
    /// </summary>
    public class StatisticalPublisher
    {

        #region → Fields         .

        private PrefAppContext mContext;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        /// <value>The context.</value>
        private PrefAppContext Context
        {
            get
            {
                return mContext;
            }

            set
            {
                mContext = value;
            }
        }

        /// <summary>
        /// Gets or sets the negotiation conversations.
        /// </summary>
        /// <value>The negotiation conversations.</value>
        private List<NegConversation> NegotiationConversations
        {
            get
            {
                return this.Context.NegConversations.ToList<NegConversation>();
            }
        }

        #endregion Properties

        #region → Constractor    .

        /// <summary>
        /// Statisticals the publisher.
        /// </summary>
        /// <param name="context">The context.</param>
        public StatisticalPublisher(PrefAppContext context)
        {
            this.Context = context;
        }

        #endregion

        #region → Event Handlers .

        /// <summary>
        /// Result_s the completed.
        /// </summary>
        /// <param name="e">The e.</param>
        private void result_Completed(InvokeOperation<bool> e)
        {
            if (e.HasError)
            {
                e.MarkErrorAsHandled();
                PrefAppMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        #endregion

        #region → Events         .

        #endregion

        #region → Methods        .

        #region → Private        .


        /// <summary>
        /// Sends the apps statisticals messages.
        /// </summary>
        /// <param name="appName">Name of the app.</param>
        /// <param name="userID">The user ID.</param>
        /// <param name="conversationID">The conversation ID.</param>
        /// <param name="messageContent">Content of the message.</param>
        /// <param name="messageSubject">The message subject.</param>
        /// <param name="messageSender">The message sender.</param>
        /// <param name="messageReceiver">The message receiver.</param>
        /// <returns>True Mean Sucess</returns>
        private bool SendAppsStatisticalsMessages(string appName, Guid userID, Guid conversationID, string messageContent, string messageSubject, string messageSender, string messageReceiver)
        {
            InvokeOperation<bool> result = this.Context.SendAppsStatisticalsMessages(
                                                    PrefAppConfigurations.AppName,
                                                    PrefAppConfigurations.CurrentLoginUser.UserID,
                                                    conversationID,
                                                    messageContent,
                                                    PrefAppConfigurations.AppName + " Feedback",
                                                    "Preference App",
                                                    "eNeg System",
                                                    new Action<InvokeOperation<bool>>(result_Completed),
                                                    null);

            return true;
        }


        /// <summary>
        /// Sends a Message to eNeg contains all Statisticals.
        /// </summary>
        /// <returns>true if it succes();o send</returns>
        public bool Send()
        {
            while (this.NegotiationConversations.Where(s => s.HasDataMacthingChanges).Count() > 0)
            {

                DataReport dataReport = new DataReport();

                NegConversation conversation = this.NegotiationConversations.Where(s => s.HasDataMacthingChanges).FirstOrDefault();

                string messageContent = dataReport.GenerateFinaleNegReport(conversation.DMLastSentMessage, conversation.DMLastReceivedMessage);

                if (!this.SendAppsStatisticalsMessages(PrefAppConfigurations.AppName, PrefAppConfigurations.CurrentLoginUser.UserID, conversation.ConversationID.Value, messageContent, "Preference App Feedback", PrefAppConfigurations.CurrentLoginUser.EmailAddress, PrefAppConfigurations.CurrentLoginUser.EmailAddress))
                {
                    return false;
                }

                conversation.HasDataMacthingChanges = false;
            }

            return true;
        }


        /// <summary>
        /// Resets Has Data Macthing Changes for every onversations.
        /// </summary>
        public void Reset()
        {
            while (this.NegotiationConversations.Where(s => s.HasDataMacthingChanges).Count() > 0)
            {
                NegConversation conversation = this.NegotiationConversations.Where(s => s.HasDataMacthingChanges).FirstOrDefault();
                conversation.HasDataMacthingChanges = false;
            }
        }


        #endregion  Public


        #endregion Methods

    }
}
