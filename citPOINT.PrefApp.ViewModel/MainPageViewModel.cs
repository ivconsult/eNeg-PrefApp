#region → Usings   .

using citPOINT.PrefApp.Common;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.ComponentModel.Composition;
using citPOINT.eNeg.Apps.Common.Interfaces;
using System.Windows;

#endregion

#region → History  .

/* Date         User              Change
 * 
 * 07.09.10     Yousra Reda       Creation
 * 07.09.10     Yousra Reda       MEF Export
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

    #region  Using MEF to export MainPageViewModel
    /// <summary>
    /// Class For Login operation (Login and logout).
    /// </summary>
    [Export(PrefAppViewModelTypes.MainPageViewModel)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    #endregion
    public class MainPageViewModel : ViewModelBase
    {

        #region → Fields         .

        RelayCommand<string> mChangeScreenCommand;

        string mWelcomeText;

        string mCurrentScreenText;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Property with setter and getter to contain the current User after login process
        /// </summary>
        public IUserInfo CurrentUser
        {
            get
            {
                return PrefAppConfigurations.CurrentLoginUser;
            }
            set
            {
                if (value != PrefAppConfigurations.CurrentLoginUser)
                {
                    PrefAppConfigurations.CurrentLoginUser = value;

                    RaisePropertyChanged("CurrentUser");
                }
            }
        }

        /// <summary>
        /// carry the welcome text the will then be concatenated with Current LoggedIn UserName
        /// </summary>
        public string WelcomeText
        {
            get { return mWelcomeText; }
            private set
            {
                if (value != mWelcomeText)
                {
                    mWelcomeText = value;
                    RaisePropertyChanged("WelcomeText");
                }
            }
        }

        /// <summary>
        /// String carry the current screen we currently navigate
        /// </summary>
        public string CurrentScreenText
        {
            get { return mCurrentScreenText; }
            set
            {
                if (value != mCurrentScreenText)
                {
                    mCurrentScreenText = value;
                    RaisePropertyChanged("CurrentScreenText");
                }
            }
        }

        #endregion

        #region → Constructors   .

        /// <summary>
        /// constructor that take a parameter IloginModel and give its values to mLoginModel and register required events
        /// </summary>
        [ImportingConstructor]
        public MainPageViewModel()
        {
            #region Intilization

            // Clear the user name and password
            CurrentUser = PrefAppConfigurations.MainPlatformInfo.UserInfo;

            #endregion

            Deployment.Current.Dispatcher.BeginInvoke(Intialize);

        }

        #endregion

        #region → Event Handlers .

        /// <summary>
        /// Call back for login
        /// </summary>
        public void Intialize()
        {
            WelcomeText = string.Format("Welcome {0}", CurrentUser.FullName);

            CurrentScreenText = PrefAppViewTypes.ReportView;

            PrefAppMessanger.ChangeScreenMessage.Send(PrefAppViewTypes.ReportView);
        }

        #endregion

        #region → Commands       .

        /// <summary>
        /// Relay Command for Changing screen according to navigation
        /// </summary>
        public RelayCommand<string> ChangeScreenCommand
        {
            get
            {
                if (mChangeScreenCommand == null)
                {
                    mChangeScreenCommand = new RelayCommand<string>(g =>
                    {
                        try
                        {
                            PrefAppConfigurations.CanContinueProcess(result =>
                            {
                                if (result == MessageBoxResult.OK)
                                {
                                    CurrentScreenText = "";

                                    PrefAppMessanger.CancelChangesMessage.Send();

                                    PrefAppMessanger.ChangeScreenMessage.Send(g);

                                    CurrentScreenText = g;
                                }
                            });
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            PrefAppMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }, g => g != null);
                }
                return mChangeScreenCommand;
            }
        }

        #endregion Commands

        #region → Methods        .

        #region → Public         .

        #endregion

        #region → Private        .

        #endregion

        #endregion
    }
}
