#region → Usings   .
using citPOINT.eNeg.Common;
using citPOINT.PrefApp.Common;
using citPOINT.PrefApp.ViewModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Telerik.Windows.Controls;
using citPOINT.eNeg.Infrastructure.ExceptionHandling;
using citPOINT.eNeg.Infrastructure.ClientSide.ExceptionHandling;
using citPOINT.PrefApp.Data;
using System.Collections.Generic;
#endregion

#region → History  .

/* Date         User              Change
 * 
 * 13.09.10     Yousra Reda       Creation
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
    /// For Main Page PrefApp (View)
    /// </summary>
    [Export]
    public partial class MainPagePrefApp : UserControl, ICleanup
    {
        #region → Fields         .
        private AppSettingsView mAppSettingsView;
        private NewMainDataMatchingView mNewMainDataMatchingView;
        private ReportView mReportView;

        private List<string> MenusList = new List<string>() { PrefAppViewTypes.AppSettingsView, PrefAppViewTypes.DataMatchingView, PrefAppViewTypes.ReportView };

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets the last instance.
        /// </summary>
        /// <value>The last instance.</value>
        public static MainPagePrefApp LastInstance { get; set; }

        #region Using MEF to import MainPageViewModel
        /// <summary>
        /// Set Main Page ViewModel
        /// </summary>
        [Import(PrefAppViewModelTypes.MainPageViewModel)]
        public object ViewModel
        {
            set
            {
                DataContext = value;
            }
            get
            {
                return DataContext;
            }
        }

        /// <summary>
        /// Gets or sets the view model repository.
        /// </summary>
        /// <value>The view model repository.</value>
        public ViewModelRepository ViewModelRepository { get; set; }

        #endregion


        /// <summary>
        /// Gets the app settings view.
        /// </summary>
        /// <value>The app settings view.</value>
        private AppSettingsView AppSettingsView
        {
            get
            {
                if (mAppSettingsView == null)
                {
                    mAppSettingsView = new AppSettingsView(ViewModelRepository);
                }

                return mAppSettingsView;
            }
        }

        /// <summary>
        /// Gets the new main data matching view.
        /// </summary>
        /// <value>The new main data matching view.</value>
        private NewMainDataMatchingView NewMainDataMatchingView
        {
            get
            {
                if (mNewMainDataMatchingView == null)
                {
                    mNewMainDataMatchingView = new NewMainDataMatchingView(ViewModelRepository.DataMatchingViewModel);
                }

                return mNewMainDataMatchingView;
            }
        }

        /// <summary>
        /// Gets the report view.
        /// </summary>
        /// <value>The report view.</value>
        private ReportView ReportView
        {
            get
            {
                if (mReportView == null)
                {
                    mReportView = new ReportView(ViewModelRepository.DataMatchingViewModel.ReportVM);
                }

                ViewModelRepository.ReportViewModel.RefereshSources();

                return mReportView;
            }
        }
        #endregion

        #region → Constructors   .
        /// <summary>
        /// Default Constuctor
        /// </summary>
        public MainPagePrefApp(ViewModelRepository viewModelRepository)
        {
            LastInstance = this;

            this.ViewModelRepository = viewModelRepository;

            InitializeComponent();

            //Keep the registeration of these messages before MEF Import to can chnage screen when completed
            #region Registeration for needed messages in eNegMessenger

            PrefAppMessanger.ChangeScreenMessage.Register(this, OnChangeScreenMessage);
            PrefAppMessanger.NewPopUp.Register(this, OnNewPopUpPopUp);

            #endregion

            #region Use MEF To load the View Model
            if (!GalaSoft.MvvmLight.ViewModelBase.IsInDesignModeStatic)
            {
                PreferenceAppModule.Container.SatisfyImportsOnce(this);
            }

            #endregion

            PrefAppConfigurations.AdjustDragDropPoints =
                new Point(PrefAppConfigurations.MainPlatformInfo.HostRegionSizeDetails.Left,
                          PrefAppConfigurations.MainPlatformInfo.HostRegionSizeDetails.Top);
        }

        #endregion

        #region → Event Handlers .

        /// <summary>
        /// Handles the MouseMove event of the uxcmdAppSettings control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseEventArgs"/> instance containing the event data.</param>
        private void uxcmdAppSettings_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            HyperlinkButton link = (sender as HyperlinkButton);

            if (link != null && link.IsHitTestVisible)
            {
                Border border = (link.Parent as Border);

                if (border != null)
                {
                    border.BorderThickness = new Thickness(1);

                    border.Background = new SolidColorBrush(Color.FromArgb(120, 183, 215, 225));
                }
            }
        }

        /// <summary>
        /// Handles the MouseLeave event of the uxcmdAppSettings control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseEventArgs"/> instance containing the event data.</param>
        private void uxcmdAppSettings_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            HyperlinkButton link = (sender as HyperlinkButton);

            if (link != null && link.IsHitTestVisible)
            {
                Border border = (link.Parent as Border);

                if (border != null)
                {
                    border.BorderThickness = new Thickness(0);

                    border.Background = new SolidColorBrush(Color.FromArgb(0, 183, 215, 225));
                }
            }
        }

        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Heighlights the menus.
        /// </summary>
        /// <param name="menuName">Name of the menu.</param>
        private void HeighlightMenus(string menuName)
        {
            foreach (var controlItem in uxMenu.Children)
            {
                Border bordr = (controlItem as Border);

                if (bordr != null && (bordr.Child is HyperlinkButton) && MenusList.Contains(menuName))
                {
                    HyperlinkButton link = (bordr.Child as HyperlinkButton);

                    if (link.CommandParameter.ToString() == menuName)
                    {
                        link.IsHitTestVisible = false;

                        bordr.BorderThickness = new Thickness(1);

                        bordr.Background = new SolidColorBrush(Color.FromArgb(255, 183, 215, 225));
                    }
                    else
                    {
                        link.IsHitTestVisible = true;

                        bordr.Background = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));

                        bordr.BorderThickness = new Thickness(0);
                    }
                }
            }
        }

        /// <summary>
        /// change scrren according to the sent parameter
        /// </summary>
        /// <param name="changeScreen">Name of Screen</param>
        private void OnChangeScreenMessage(string changeScreen)
        {
            #region call Cleanup() on the current screen before switching

            ICleanup currentScreen = this.uxContentMainPage.Content as ICleanup;

            //if (currentScreen != null)
            //    currentScreen.Cleanup();

            #endregion

            #region Swich on Curent screen Name to change the view according to that

            HeighlightMenus(changeScreen);

            switch (changeScreen)
            {
                case PrefAppViewTypes.AppSettingsView:
                    {
                        this.uxContentMainPage.Content = this.AppSettingsView;
                        PrefAppMessanger.FlippMessage.Send(PrefAppViewTypes.AddPrefSetView);
                        PrefAppConfigurations.CurrentScreenName = PrefAppViewTypes.AppSettingsView;
                        break;
                    }

                case PrefAppViewTypes.DataMatchingView:
                    {
                        if (PrefAppConfigurations.IsNegotiationAttachedPreferenceSet)
                        {
                            this.uxContentMainPage.Content = this.NewMainDataMatchingView;
                            PrefAppConfigurations.CurrentScreenName = PrefAppViewTypes.DataMatchingView;
                        }
                        else
                        {
                            PrefAppMessanger.ChangeScreenMessage.Send(PrefAppViewTypes.AssignPrefSetToNegotiationView);
                        }

                        break;
                    }
                case PrefAppViewTypes.ReportView:
                    {
                        if (PrefAppConfigurations.IsNegotiationAttachedPreferenceSet)
                        {
                            this.uxContentMainPage.Content = this.ReportView;
                            PrefAppConfigurations.CurrentScreenName = PrefAppViewTypes.ReportView;
                        }
                        else
                        {
                            PrefAppMessanger.ChangeScreenMessage.Send(PrefAppViewTypes.AssignPrefSetToNegotiationView);
                        }
                        break;
                    }
                case PrefAppViewTypes.AssignPrefSetToNegotiationView:
                    {
                        this.uxContentMainPage.Content = new AssignPrefSetToNegotiationView();
                        (this.uxContentMainPage.Content as AssignPrefSetToNegotiationView).DataContext = ViewModelRepository.DataMatchingViewModel;
                        PrefAppConfigurations.CurrentScreenName = PrefAppViewTypes.AssignPrefSetToNegotiationView;
                        break;
                    }
            }
            #endregion
        }

        /// <summary>
        /// Called when [new pop up pop up].
        /// </summary>
        /// <param name="DragedValue">The draged value.</param>
        private void OnNewPopUpPopUp(string DragedValue)
        {
            if (PrefAppMessanger.NewPopUp.PopUpType == PrefAppMessanger.PopUpType.SendMail.ToString())
            {
                #region Show PopUp window to decide whether to send mail to Negotiators or not
                PopUpWindow SendMailWindow = new PopUpWindow("Send E-mail");
                SendMailWindow.DataContext = this.ViewModelRepository.PreferenceSetsViewModel;
                SendMailWindow.Content = new SendMailPopUp(DragedValue);
                SendMailWindow.ShowDialog();
                #endregion
            }
            else if (PrefAppMessanger.NewPopUp.PopUpType == PrefAppMessanger.PopUpType.MailEditor.ToString())
            {
                #region Show PopUp window to can edit in the mail Content and Receiver
                PopUpWindow SendMailWindow = new PopUpWindow("Send E-mail");
                SendMailWindow.DataContext = this.ViewModelRepository.PreferenceSetsViewModel;

                SendMailWindow.Content = new MailEditorPopUp();
                SendMailWindow.ShowDialog();

                PrefAppConfigurations.MailNegotiationName = string.Empty;
                PrefAppConfigurations.MailPreferenceSetNegID = Guid.Empty;

                #endregion
            }
            else if (PrefAppMessanger.NewPopUp.PopUpType == PrefAppMessanger.PopUpType.ClonePreferenceSet.ToString())
            {
                #region Show PopUp window to define a Clone Preference Set
                {
                    PopUpWindow selectNegotiationsWindow = new PopUpWindow(DragedValue);
                    selectNegotiationsWindow.DataContext = this.ViewModelRepository.PreferenceSetsViewModel.IssuesVM;
                    selectNegotiationsWindow.Content = new SelectNegotiationsPopUp();
                    selectNegotiationsWindow.ShowDialog();
                }
                #endregion
            }

        }

        /// <summary>
        /// Cleanups the specified obj.
        /// </summary>
        /// <param name="obj">The obj.</param>
        private void Cleanup(object obj)
        {
            if (obj != null && (obj as ICleanup) != null)
            {
                (obj as ICleanup).Cleanup();
            }
        }

        #endregion

        #region → Public         .

        /// <summary>
        /// ICleanup interface implementation
        /// </summary>
        public void Cleanup()
        {
            Cleanup(mAppSettingsView);

            Cleanup(mNewMainDataMatchingView);

            Cleanup(mReportView);

            // Cleanup itself
            Messenger.Default.Unregister(this);
        }

        #endregion


        #endregion

    }
}
