#region → Usings   .
using System;
using System.Windows;
using System.Collections.Generic;
using citPOINT.PrefApp.Data.Web;
using System.ComponentModel;
using citPOINT.eNeg.Apps.Common.Interfaces;
using citPOINT.eNeg.Apps.Common.Enums;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Controls;
using citPOINT.eNeg.Common;
using System.Threading;
#endregion

#region → History  .

/* Date         User              Change
 * 
 * 07.09.10     Yousra Reda       Creation
 * 07.09.10     Yousra Reda       Save current Login User
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
    /// Class that have some configuraton needed in our application
    /// </summary>
    public class PrefAppConfigurations
    {
        #region → Enums          .

        /// <summary>
        /// Issue Types
        /// </summary>
        public enum IssueTypes
        {
            /// <summary>
            /// issue
            /// </summary>
            Issue,
            /// <summary>
            /// optionIssue
            /// </summary>
            Option,
            /// <summary>
            /// Later Rated
            /// </summary>
            LaterRated,
        }

        /// <summary>
        /// Action type
        /// </summary>
        public enum ActionTypes
        {
            /// <summary>
            /// Data Matching
            /// </summary>
            DataMatching,

            /// <summary>
            /// Report
            /// </summary>
            Report,

            /// <summary>
            /// External Report
            /// </summary>
            ExternalReport
        }

        #endregion

        private static bool mCanRejectChanges = true;


        #region → Properties     .

        #region Static

        /// <summary>
        /// Gets or sets the current login user.
        /// </summary>
        /// <value>The current login user.</value>
        public static IUserInfo CurrentLoginUser { get; set; }

        /// <summary>
        /// Adjust Drag Drop Points X,Y
        /// </summary>
        public static Point AdjustDragDropPoints = new Point(0, 0);

        /// <summary>
        /// Gets the name of the app.
        /// </summary>
        /// <value>The name of the app.</value>
        public static string AppName { get { return "Preference App"; } }

        /// <summary>
        /// Gets or sets the name of the current screen.
        /// </summary>
        /// <value>The name of the current screen.</value>
        public static string CurrentScreenName { get; set; }

        /// <summary>
        /// Gets or sets the main drag drop manager.
        /// </summary>
        /// <value>The main drag drop manager.</value>
        public static DragDrop MainDragDropManager { get; set; }

        /// <summary>
        /// Gets or sets the IP address.
        /// </summary>
        /// <value>The IP address.</value>
        public static string IPAddress { get; set; }

        /// <summary>
        ///  New Issue Pending.
        /// </summary>
        private static bool mNewIssuePending = false;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is new issue pending.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is new issue pending; otherwise, <c>false</c>.
        /// </value>
        public static bool IsNewIssuePending
        {
            get
            {
                return mNewIssuePending;
            }
            set
            {
                mNewIssuePending = value;
            }
        }

        /// <summary>
        /// Gets or sets the pending issue GUID.
        /// </summary>
        /// <value>The pending issue GUID.</value>
        public static List<PendingItem> PendingItems { get; set; }

        /// <summary>
        /// Gets or sets the action type parameter.
        /// </summary>
        /// <value>The action type parameter.</value>
        public static string ActionTypeParameter { get; set; }

        /// <summary>
        /// Gets or sets the negotiatio ID parameter.
        /// </summary>
        /// <value>The negotiatio ID parameter.</value>
        public static Guid? NegotiationIDParameter { get; set; }

        /// <summary>
        /// Gets or sets the conversation ID parameter.
        /// </summary>
        /// <value>The conversation ID parameter.</value>
        public static Guid? ConversationIDParameter { get; set; }

        /// <summary>
        /// Gets or sets the message ID parameter.
        /// </summary>
        /// <value>The message ID parameter.</value>
        public static Guid? MessageIDParameter { get; set; }

        /// <summary>
        /// Gets or sets the name of the mail negotiation.
        /// </summary>
        /// <value>The name of the mail negotiation.</value>
        public static string MailNegotiationName { get; set; }

        /// <summary>
        /// Gets or sets the mail preference set neg ID.
        /// </summary>
        /// <value>The mail preference set neg ID.</value>
        public static Guid MailPreferenceSetNegID { get; set; }

        /// <summary>
        /// Gets or sets the main platform info.
        /// </summary>
        /// <value>The main platform info.</value>
        public static IMainPlatformInfo MainPlatformInfo { get; set; }

        /// <summary>
        /// Gets the main service URI.
        /// </summary>
        /// <value>The main service URI.</value>
        public static Uri MainServiceUri
        {
            get
            {
                if (PrefAppConfigurations.MainPlatformInfo != null)
                {

                    var app = PrefAppConfigurations
                                    .MainPlatformInfo
                                    .GetApplicationInfo(PrefAppConfigurations.AppName);

                    if (app != null && !string.IsNullOrEmpty(app.ApplicationMainServicePath))
                    {
                        return new Uri(app.ApplicationMainServicePath, UriKind.Absolute);
                    }
                }

                return new Uri(string.Empty, UriKind.Absolute);
            }
        }

        /// <summary>
        /// Gets the default view.
        /// </summary>
        /// <value>The default view.</value>
        public static string DefaultView
        {
            get
            {
                if (PrefAppConfigurations.MainPlatformInfo != null)
                {

                    var app = PrefAppConfigurations
                                    .MainPlatformInfo
                                    .GetApplicationInfo(PrefAppConfigurations.AppName);

                    if (app != null)
                    {
                        if (app.DefaultView == ViewsTypes.AppSettings)
                        {
                            return PrefAppViewTypes.AppSettingsView;
                        }
                    }
                }

                return PrefAppViewTypes.ReportView;
            }

            set
            {
                if (PrefAppConfigurations.MainPlatformInfo != null)
                {
                    var app = PrefAppConfigurations
                                    .MainPlatformInfo
                                    .GetApplicationInfo(PrefAppConfigurations.AppName);

                    if (app != null)
                    {
                        switch (value)
                        {
                            case PrefAppViewTypes.ReportView:
                                app.DefaultView = ViewsTypes.Report;
                                break;
                            case PrefAppViewTypes.AppSettingsView:
                                app.DefaultView = ViewsTypes.AppSettings;
                                break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is negotiation attached preference set.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is negotiation attached preference set; otherwise, <c>false</c>.
        /// </value>
        public static bool IsNegotiationAttachedPreferenceSet { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [check for cancel preference set changes].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [check for cancel preference set changes]; otherwise, <c>false</c>.
        /// </value>
        public static bool CheckForCancelPreferenceSetChanges { get; set; }

        /// <summary>
        /// Determines whether this instance [can continue process] the specified callback.
        /// </summary>
        /// <param name="callback">The callback.</param>
        public static void CanContinueProcess(Action<MessageBoxResult> callback)
        {
            if (CheckForCancelPreferenceSetChanges)
            {
                //Firstly ask user to confirm editing that item
                DialogMessage dialogMessage = new DialogMessage(
                    new Button(),
                    "Cancel editing current item, Are you sure?",
                    callback)
                {
                    Button = MessageBoxButton.OKCancel,
                    Caption = "Confirm Cancelling"
                };

                eNegMessanger.ConfirmMessage.Send(dialogMessage);
            }
            else
            {
                callback(MessageBoxResult.OK);
            }
        }

        /// <summary>
        /// Gets the application ID.
        /// </summary>
        /// <value>The application ID.</value>
        public static Guid ApplicationID
        {
            get
            {
                if (PrefAppConfigurations.MainPlatformInfo != null)
                {

                    var app = PrefAppConfigurations
                                    .MainPlatformInfo
                                    .GetApplicationInfo(PrefAppConfigurations.AppName);

                    if (app != null)
                    {
                        return app.ApplicationID;
                    }
                }

                return Guid.Empty;
            }
        }

        #endregion

        #endregion

    }
}
