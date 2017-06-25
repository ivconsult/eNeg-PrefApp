#region → Usings   .
using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using citPOINT.eNeg.Apps.Common.Interfaces;
using citPOINT.PrefApp.Common;
using GalaSoft.MvvmLight.Messaging;
using Telerik.Windows.Controls;
using citPOINT.eNeg.Apps.Common.Enums;

#endregion

#region → History  .

/* Date         User          Change
 * 
 * 05.04.12    M.Wahab       Creation
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
    /// Main Preference App View.
    /// </summary>
    [Export]
    public partial class MainPreferenceAppView : UserControl, IObserverApp
    {
        #region → Fields         .
        RadWindow window = new RadWindow();
        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets the name of the app.
        /// </summary>
        /// <value>The name of the app.</value>
        public string AppName
        {
            get
            {
                return PrefAppConfigurations.AppName;
            }
        }

        /// <summary>
        /// Gets or sets the view model repository.
        /// </summary>
        /// <value>The view model repository.</value>
        private ViewModelRepository ViewModelRepository { get; set; }

        /// <summary>
        /// Gets or sets the main page pref app view.
        /// </summary>
        /// <value>The main page pref app view.</value>
        private MainPagePrefApp MainPagePrefAppView { get; set; }

        #endregion

        #region → Constructor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPreferenceAppView"/> class.
        /// </summary>
        [ImportingConstructor]
        public MainPreferenceAppView()
        {
            InitializeComponent();

            PrefAppMessanger.RaiseErrorMessage.Register(this, OnRaiseErrorMessage);
            //PrefAppMessanger.ConfirmMessage.Register(this, OnConfirmMessage);
            PrefAppMessanger.FlippMessage.Register(this, onFlippingByPageName);

            try
            {
                this.ApplyChanges(false);

                PrefAppConfigurations.MainPlatformInfo.TrackChanges.AddObserverApp(this);

                if (Helper.IsWorkALone)
                {
                    this.ApplyChanges(true);
                }
            }
            catch (Exception ex)
            {
                PrefApp.Common.PrefAppConfigurations.MainPlatformInfo.HandleException.HandleException(ex, this.AppName);
            }
        }

        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Raise error message if there is any layer send RaiseErrorMessage
        /// </summary>
        /// <param name="ex">exception to raise</param>
        private void OnRaiseErrorMessage(Exception ex)
        {
            PrefAppConfigurations.MainPlatformInfo.HandleException.HandleException(ex, PrefAppConfigurations.AppName);

            //ExceptionHandlingResult exceptionHandlingResult = ExceptionManager.Instance.HandleException(ex, "Policy1");
            //ClientExceptionHandlerProvider.ShowMessageErrorWindow(exceptionHandlingResult.Message, ex);
        }

      

        /// <summary>
        /// Ons the name of the flipping by page.
        /// </summary>
        /// <param name="PageName">Name of the page.</param>
        private void onFlippingByPageName(string PageName)
        {
            #region Swich on Curent screen Name to change the view according to that
            switch (PageName)
            {
                case PrefAppViewTypes.ExitLoadingView:
                    uxgrdLoading.Visibility = System.Windows.Visibility.Collapsed;
                    break;

                case PrefAppViewTypes.LoadingView:
                    uxgrdLoading.Visibility = System.Windows.Visibility.Visible;
                    break;
            }
            #endregion
        }

        #endregion

        #region → Public         .

        /// <summary>
        /// Applies the changes.
        /// </summary>
        /// <param name="isActive">if set to <c>true</c> [is active].</param>
        public void ApplyChanges(bool isActive)
        {
            if (isActive || Helper.IsActives)
            {
                PrefAppMessanger.FlippMessage.Send(PrefAppViewTypes.ExitLoadingView);

                PrefAppMessanger.FlippMessage.Send(PrefAppViewTypes.LoadingView);

                if (PrefAppConfigurations.MainPlatformInfo.CurrentPlatform == PlatformTypes.MainPlatform)
                {
                    PrefAppConfigurations.ActionTypeParameter = PrefAppConfigurations.ActionTypes.Report.ToString();
                }
                else
                {
                    PrefAppConfigurations.ActionTypeParameter = PrefAppConfigurations.ActionTypes.ExternalReport.ToString();
                }

                #region → Change Negotiation      .

                if (PrefAppConfigurations.MainPlatformInfo.CurrentNegotiation != null)
                {
                    PrefAppConfigurations.NegotiationIDParameter = PrefAppConfigurations.MainPlatformInfo.CurrentNegotiation.NegotiationID;
                }
                else
                {
                    PrefAppConfigurations.NegotiationIDParameter = null;
                }

                #endregion

                #region → Change Conversation     .

                if (PrefAppConfigurations.MainPlatformInfo.CurrentConversation != null)
                {
                    PrefAppConfigurations.ConversationIDParameter = PrefAppConfigurations.MainPlatformInfo.CurrentConversation.ConversationID;
                }
                else
                {
                    PrefAppConfigurations.ConversationIDParameter = null;
                }

                #endregion

                #region → Change User             .

                if (PrefAppConfigurations.CurrentLoginUser != null && PrefAppConfigurations.CurrentLoginUser.UserID != PrefAppConfigurations.MainPlatformInfo.UserInfo.UserID)
                {
                    if (this.ViewModelRepository != null)
                    {
                        this.ViewModelRepository.Cleanup();

                        MainPagePrefAppView.Cleanup();

                        this.ViewModelRepository = null;
                    }
                }

                PrefAppConfigurations.CurrentLoginUser = PrefAppConfigurations.MainPlatformInfo.UserInfo;

                #endregion

                #region → View Model Repository   .

                if (PrefAppConfigurations.MainPlatformInfo.CurrentPlatform == PlatformTypes.MainPlatform)
                {
                    if (ViewModelRepository != null)
                    {
                        ViewModelRepository.DataMatchingViewModel.ApplyChanges();
                    }
                    else
                    {
                        ViewModelRepository = new ViewModelRepository();
                    }

                    if (MainPagePrefAppView == null)
                    {
                        MainPagePrefAppView = new MainPagePrefApp(ViewModelRepository);
                    }
                }
                #endregion

                if (PrefAppConfigurations.MainPlatformInfo.CurrentPlatform == PlatformTypes.MainPlatform)
                {
                    this.uxCntContent.Content = this.MainPagePrefAppView;
                }
                else
                {
                    this.uxCntContent.Content = new MainExternalReportView();
                }
            }
            else
            {
                uxgrdLoading.Visibility = System.Windows.Visibility.Visible;
            }
        }

        #endregion
        
        #endregion


    }
}
