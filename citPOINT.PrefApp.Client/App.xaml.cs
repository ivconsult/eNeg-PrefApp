
#region → Usings   .

using System;
using System.Windows;
using System.Collections.Generic;
using citPOINT.PrefApp.Common;
using System.Windows.Browser;
using System.Threading;
using System.Globalization;
using citPOINT.eNeg.Infrastructure.ExceptionHandling;
using citPOINT.eNeg.Infrastructure.ClientSide.ExceptionHandling;
using citPOINT.PrefApp.Data.Web;
using Telerik.Windows.Controls;

#endregion

#region → History  .

/* Date         User              Change
 * 21.09.10     Yousra.M       Creation
 * 28.09.10     M.Wahab        XML Comments
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion

namespace citPOINT.PrefApp.Client
{
    /// <summary>
    /// Mian Class Or Start Point Class
    /// </summary>
    public partial class App : Application
    {
        #region → Fields         .
        /// <summary>
        /// Opened Sub Windows
        /// </summary>
        public static List<object> OpenedSubWindows = new List<object>();
        #endregion

        #region → Constructor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
            //Initialize used exception handling policies
            ClientExceptionHandlerProvider.RepaireExceptionPolicies();

            // Set the current UI culture.
            Thread.CurrentThread.CurrentCulture = new CultureInfo("En-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("En-US");

            //Register for most used event handlers of App
            this.Startup += this.Application_Startup;
            this.UnhandledException += this.Application_UnhandledException;

            InitializeComponent();
        }

        #endregion Constructor

        #region → Event Handlers .

        /// <summary>
        /// Handler for Application Start Up.
        /// </summary>
        /// <param name="sender">Value of Sender</param>
        /// <param name="e">Value of StartupEventArgs </param>
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            PrefAppConfigurations.IPAddress = e.InitParams["IPAddress"];

            #region → Loading The base Address                                .

            try
            {
                int index = System.Windows.Application.Current.Host.Source.AbsoluteUri.IndexOf("ClientBin", StringComparison.CurrentCultureIgnoreCase);
                eNeg.Common.AppConfigurations.HostBaseAddress = System.Windows.Application.Current.Host.Source.AbsoluteUri.Substring(0, index);
            }
            catch { }

            #endregion "Loding The base Address"

            #region → In case you navigate from eNeg To PrefApp to Make Data Matching for certain just added message.

            if (HtmlPage.Document.QueryString.ContainsKey("ActionType"))
            {
                PrefAppConfigurations.ActionTypeParameter = HtmlPage.Document.QueryString["ActionType"].ToString();

                if (HtmlPage.Document.QueryString.ContainsKey("NegID"))
                {
                    PrefAppConfigurations.NegotiationIDParameter = new Guid(HtmlPage.Document.QueryString["NegID"].ToString());

                    if (HtmlPage.Document.QueryString.ContainsKey("ConvID"))
                    {
                        PrefAppConfigurations.ConversationIDParameter = new Guid(HtmlPage.Document.QueryString["ConvID"].ToString());

                        if (HtmlPage.Document.QueryString.ContainsKey("MsgID"))
                        {
                            PrefAppConfigurations.MessageIDParameter = new Guid(HtmlPage.Document.QueryString["MsgID"].ToString());
                        }
                    }
                }
            }

            #endregion

            #region → In case of laoding External Reports from eNeg           .
            if (PrefAppConfigurations.NegotiationIDParameter.HasValue &&
                 !string.IsNullOrEmpty(PrefAppConfigurations.ActionTypeParameter) &&
                  PrefAppConfigurations.ActionTypeParameter.ToLower() ==
                     PrefAppConfigurations.ActionTypes.ExternalReport.ToString().ToLower())
            {
                this.RootVisual = new ExternalReportView();
            }
            #endregion

            #region → In case of Do Data matching for a certain eNeg message  .
            else if (PrefAppConfigurations.NegotiationIDParameter.HasValue &&
                //PrefAppConfigurations.ConversationIDParameter.HasValue &&
                //PrefAppConfigurations.MessageIDParameter.HasValue &&
                     !string.IsNullOrEmpty(PrefAppConfigurations.ActionTypeParameter))
            {
                this.RootVisual = new MainPagePrefApp(new ViewModelRepository());
                PrefAppMessanger.FlippMessage.Send(PrefAppViewTypes.LoadingView);
            }
            #endregion

            #region → Default Case                                            .
            else
            {
                Helper.Run();

                this.RootVisual =  new MainPreferenceAppView();

            }
            #endregion
        }

        /// <summary>
        /// Handle Application Unhandled Exception
        /// </summary>
        /// <param name="sender">Value of Sender</param>
        /// <param name="e">Value of ApplicationUnhandledExceptionEventArgs </param>
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            try
            {
                e.Handled = true;

                if (!(e.ExceptionObject is System.InvalidOperationException))
                {
                    Deployment.Current.Dispatcher.BeginInvoke(delegate { ReportErrorToDOM(e.ExceptionObject); });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Used to Report Error To DOM (Mean Html Side)
        /// </summary>
        /// <param name="e">Value of ApplicationUnhandledExceptionEventArgs</param>
        private void ReportErrorToDOM(Exception e)
        {
            try
            {
                string errorMsg = e.Message + e.StackTrace;
                errorMsg = errorMsg.Replace('"', '\'').Replace("\r\n", @"\n");

                System.Windows.Browser.HtmlPage.Window.Alert("throw new Error(\"Unhandled Error in Silverlight Application " + errorMsg + "\");");
            }
            catch (Exception)
            {
            }
        }

        #endregion Event Handlers
    }
}
