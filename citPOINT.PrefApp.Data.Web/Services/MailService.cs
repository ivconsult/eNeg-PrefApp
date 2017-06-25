#region → Usings   .
using citPOINT.PrefApp.Data.Web.eNegMailService;
using System;
using System.ServiceModel.DomainServices.Hosting;
using System.ServiceModel.DomainServices.Server;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Configuration;
#endregion

#region → History  .

/* Date         User              Change
 * 
 * 27.01.11     Yousra Reda       Creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion

namespace citPOINT.PrefApp.Data.Web
{
    /// <summary>
    /// Service provide me to send mail 
    /// </summary>
    [EnableClientAccess()]
    public class MailService : DomainService
    {

        #region → Fields         .
        MailServiceSoapClient mLoader;
        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets the loader.
        /// </summary>
        /// <value>The loader.</value>
        public MailServiceSoapClient Loader
        {
            get
            {
                if (mLoader == null)
                {
                    mLoader = new MailServiceSoapClient();
                    InjectCredentials();
                }
                return mLoader;
            }
        }

        #endregion Properties

        #region → Methods        .

        #region → Public         .

        /// <summary>
        /// Sends an mail message
        /// </summary>
        /// <param name="from">Sender address</param>
        /// <param name="to">Recepient address</param>
        /// <param name="bcc">Bcc recepient</param>
        /// <param name="cc">Cc recepient</param>
        /// <param name="subject">Subject of mail message</param>
        /// <param name="body">Body of mail message</param>
        public void SendMailMessage(string from, string to, string bcc, string cc, string subject, string body)
        {
            try
            {
                Loader.SendMailMessage(from, to, bcc, cc, subject, body);
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        #region → Private        .

        /// <summary>
        /// Injects the credentials into message header.
        /// </summary>
        private void InjectCredentials()
        {
            OperationContextScope scope = new OperationContextScope((IContextChannel)Loader.InnerChannel);

            MessageHeaders messageHeadersElement = OperationContext.Current.OutgoingMessageHeaders;
            messageHeadersElement.Add(MessageHeader.CreateHeader("username", "http://tempori.org", ConfigurationManager.AppSettings["username"]));
            messageHeadersElement.Add(MessageHeader.CreateHeader("password", "http://tempori.org", ConfigurationManager.AppSettings["password"]));
        }
        #endregion

        #endregion
    }
}
