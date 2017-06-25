
#region → Usings   .
using System;
using System.ServiceModel.DomainServices.Client;
using citPOINT.PrefApp.Data.Web;
#endregion

#region → History  .

/* Date         User            change
 * 
 * 27.01.11     Yousr Reda     creation
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
    /// Used to Send a Mail.
    /// </summary>
    public class MailHelper
    {

        #region → Fields         .

        private MailContext mMailContext;
        #endregion

        #region → Constructors   .
        /// <summary>
        /// Class Constructor
        /// </summary>
        public MailHelper()
        {
            mMailContext = new MailContext(new Uri(PrefAppConfigurations.MainPlatformInfo.GetApplicationInfo(PrefAppConfigurations.AppName).ApplicationBaseAddress + "citPOINT-prefApp-Data-Web-MailService.svc", UriKind.Absolute));
        }
        #endregion

        #region → Events         .

        /// <summary>
        /// Call Back Event After Sending Mail
        /// </summary>
        public event Action<InvokeOperation> MailSendComplete;

        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Sends an mail message
        /// </summary>
        /// <param name="from">Sender address</param>
        /// <param name="to">Recepient address</param>
        /// <param name="subject">Subject of mail message</param>
        /// <param name="body">Body of mail message</param>
        private void SendMailMessage(string from, string to, string subject, string body)
        {
            mMailContext.SendMailMessage(from, to, null, null, subject, body, MailSendComplete, null);
        }

        #endregion

        #region → Public         .


        /// <summary>
        /// Sends the mail to negotiators.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        public void SendMailToNegotiators(string from, string to, string subject, string body)
        {
            string[] AllReceivers = to.Split(new string[] { "; " }, StringSplitOptions.RemoveEmptyEntries);
           
            foreach (var receiver in AllReceivers)
            {
                body = body.Replace("\r\n", "<br/><br/>");

                string MessageSubject = "[PrefApp] New Issues or Options Notification";

                SendMailMessage(from, receiver, MessageSubject, body);
            }
        }

        #endregion



        #endregion
    }
}
