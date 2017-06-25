
#region → Usings   .

using System.Windows.Media;
using Telerik.Windows.Controls;
using citPOINT.PrefApp.Common;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using citPOINT.PrefApp.ViewModel;
using System;
#endregion

#region → History  .

/* Date         User              Change
 * 
 * 16.01.11    Yousra.Mohammed       Creation
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
    /// Generic PopUp Window can get different Content and header for it
    /// , but in all cases it has similar properties like Resizing, Modal feature,...etc 
    /// </summary>
    public partial class PopUpWindow : RadWindow, ICleanup
    {

        #region → Fields         .
        private bool mIsExit = true;
        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets the view model.
        /// </summary>
        /// <value>The view model.</value>
        public PreferenceSetsViewModel ViewModel
        {
            get
            {
                return (DataContext as PreferenceSetsViewModel);
            }
        }


        /// <summary>
        /// Gets the issues view model.
        /// </summary>
        /// <value>The issues view model.</value>
        public IssuesViewModel IssuesViewModel
        {
            get
            {
                return (DataContext as IssuesViewModel);
            }
        }


        #endregion

        #region → Constructor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="PopUpWindow"/> class.
        /// </summary>
        public PopUpWindow(string header)
        {
            InitializeComponent();

            CustomInitionlizeRadWindow(header);

            PrefAppMessanger.FlippMessage.Register(this, onFlippingByPageName);
        }
        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Raises the <see cref="E:Closed"/> event.
        /// </summary>
        /// <param name="args">The <see cref="Telerik.Windows.Controls.WindowClosedEventArgs"/> instance containing the event data.</param>
        protected override void OnClosed(WindowClosedEventArgs args)
        {
            base.OnClosed(args);

            if (ViewModel != null || this.IssuesViewModel != null)
            {
                if (mIsExit)
                {
                    PrefAppConfigurations.MailPreferenceSetNegID = Guid.Empty;

                    if (ViewModel != null)
                    {
                        this.ViewModel.RejectChanges();
                    }
                    else
                    {
                        this.IssuesViewModel.RejectChanges();
                    }
                }
            }
        }

        /// <summary>
        /// Raises the <see cref="E:KeyDown"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs"/> instance containing the event data.</param>
        protected override void OnKeyDown(System.Windows.Input.KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.Key == System.Windows.Input.Key.Escape)
            {
                mIsExit = true;
                this.Close();
            }
        }

        /// <summary>
        /// Ons the name of the flipping by page.
        /// </summary>
        /// <param name="PageName">Name of the page.</param>
        private void onFlippingByPageName(string PageName)
        {
            if (PageName == PrefAppViewTypes.ClosePopupView)
            {
                mIsExit = false;
                this.Close();
            }
            //Special case for closing replace popup as 2 pop at same time
            else if (PageName == PrefAppViewTypes.CloseReplacePopupView && (this.Content as ReplacePublishedSetPopUp) != null)
            {
                mIsExit = false;
                this.Close();
            }
        }
        #endregion

        #region → Public         .
        /// <summary>
        /// Customs the initionlize RAD window.
        /// </summary>
        public void CustomInitionlizeRadWindow(string header)
        {
            this.ResizeMode = ResizeMode.NoResize;
            this.Header = header;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.ModalBackground = new SolidColorBrush(Colors.LightGray);
            this.ModalBackground.Opacity = 0.5;
        }

        /// <summary>
        /// Cleanups this instance.
        /// </summary>
        public void Cleanup()
        {
            Messenger.Default.Unregister(this);
        }

        #endregion

        #endregion


    }
}
