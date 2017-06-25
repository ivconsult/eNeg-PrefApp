 
#region → Usings   .

using System;
using System.Windows;
using System.Windows.Controls;

#endregion

#region → History  .
/* Date         User            Change
 * 
 * 28.09.10     M.Wahab        creation
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

  #region →  Event Args Size    .
    

    /// <summary>
    /// Custom EventArgs For Size
    /// </summary>
    public class EventArgsSize : EventArgs
    {
        #region → Properties     .

        /// <summary>
        /// Get or Set Width
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// Get or Set Height
        /// </summary>
        public double Height { get; set; }

        #endregion Properties

        #region → Constructor    .

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Width">Width</param>
        /// <param name="Height">Height</param>
        public EventArgsSize(double Width, double Height)
        {
            this.Width = Width;
            this.Height = Height;
        }

        #endregion Constructor
    }

    #endregion  EventArgsSize

    /// <summary>
    /// For handling control resize
    /// </summary>
    public class ViewsAdjustSize
    {

        #region → Fields         .

        private UserControl mUserControl;
        private double mMinWidth = 640;
        private double mMinHeight = 480;
        private double mMarginTopBottom =5;
        private double mMargin = 5;
        private double mMarginLeftRight = 5;
        private double mBannerHeight = 64;

        #endregion Fields

        #region → Properties     .

        /// <summary>
        /// Get or Set The Value of userControl
        /// </summary>
        public UserControl UserControl
        {
            get
            {
                return mUserControl;
            }
            set
            {
                mUserControl = value;
            }
        }

        /// <summary>
        /// Get or Set the Value of Top Bottom Margin
        /// </summary>
        public double MarginTopBottom
        {
            get
            {
                return mMarginTopBottom;
            }
            set
            {
                mMarginTopBottom = value;
            }
        }

        /// <summary>
        /// Get or Set the Value of Left Right Margin
        /// </summary>
        public double MarginLeftRight
        {
            get
            {
                return mMarginLeftRight;
            }
            set
            {
                mMarginLeftRight = value;
            }
        }

        /// <summary>
        /// Get or Set all Margins
        /// </summary>
        public double Margin
        {
            get
            {
                return mMargin;
            }
            set
            {
                mMargin = value;
                MarginLeftRight = value;
                MarginLeftRight = value;
            }
        }

        /// <summary>
        /// Get or Set Banner Height
        /// </summary>
        public double BannerHeight
        {
            get { return mBannerHeight; }
            set { mBannerHeight = value; }
        }

        /// <summary>
        /// Get or Set Min Width Defualt 640
        /// </summary>
        public double MinWidth
        {
            get { return mMinWidth; }
            set { mMinWidth = value; }
        }

        /// <summary>
        /// Get or Set Min Height Defualt 480
        /// </summary>
        public double MinHeight
        {
            get { return mMinHeight; }
            set { mMinHeight = value; }
        }


        #endregion Properties

        #region → Constructor    .

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="userControl">UserControl (View)</param>
        public ViewsAdjustSize(UserControl userControl)
        {
            this.UserControl = userControl;

            this.UserControl.Loaded += new RoutedEventHandler(ViewsBase_Loaded);
            this.UserControl.Unloaded += new RoutedEventHandler(ViewsBase_Unloaded);
            this.Content_Resized(this.UserControl, null);
        }


        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="userControl">UserControl (View)</param>
        /// <param name="MinWidth">Min Control Width</param>
        /// <param name="MinHeight">Min Control Height</param>
        public ViewsAdjustSize(UserControl userControl, double MinWidth, double MinHeight)
        {
            this.UserControl = userControl;
            this.MinWidth = MinWidth;
            this.MinHeight = MinHeight;

            this.UserControl.Loaded += new RoutedEventHandler(ViewsBase_Loaded);
            this.UserControl.Unloaded += new RoutedEventHandler(ViewsBase_Unloaded);
            this.Content_Resized(this.UserControl, null);
        }

        #endregion Constructor

        #region → Events         .
 

        /// <summary>
        /// Event Run after resize the user control and send Height and Width as args
        /// </summary>
        public event EventHandler<EventArgsSize> AfterResize;

        #endregion Events

        #region → Event Handlers .

        #region Logic to dynamically set UserControl Width and Height

        /// <summary>
        /// Event handler executed when User Control Loaded
        /// </summary>
        /// <param name="sender">Value of sender</param>
        /// <param name="e">Value of  EventArgs</param>
        private void ViewsBase_Loaded(object sender, RoutedEventArgs e)
        {
            App.Current.Host.Content.Resized += new EventHandler(Content_Resized);

        }

        /// <summary>
        /// Event handler executed when User Control UnLoaded
        /// </summary>
        /// <param name="sender">Value of sender</param>
        /// <param name="e">Value of  EventArgs</param>
        private void ViewsBase_Unloaded(object sender, RoutedEventArgs e)
        {
            App.Current.Host.Content.Resized -= new EventHandler(Content_Resized);
        }

        /// <summary>
        /// Event handler executed when Content Resized
        /// </summary>
        /// <param name="sender">Value of sender</param>
        /// <param name="e">Value of  EventArgs</param>
        private void Content_Resized(object sender, EventArgs e)
        {
            #region Adjust Width

            // stretch the UserControl to the current browser width if wider than MinimumWidth
            if (App.Current.Host.Content.ActualWidth < MinWidth)
                this.UserControl.Width = MinWidth;
            else
                this.UserControl.Width = App.Current.Host.Content.ActualWidth - this.MarginLeftRight;

            #endregion Adjust Width

            #region Adjust Height

            if (App.Current.Host.Content.ActualHeight < MinHeight)
                this.UserControl.Height = MinHeight - this.BannerHeight - this.MarginTopBottom;
            else
                // stretch the UserControl to the current brower Height - Title Height(42) - Menu Height(22)
                this.UserControl.Height = App.Current.Host.Content.ActualHeight - this.BannerHeight - this.MarginTopBottom;

            #endregion Adjust Height

            //Fire Events after Resize to the Subscribers
            if (AfterResize != null)
                AfterResize(this, new EventArgsSize(this.UserControl.Width, this.UserControl.Height));

        }

        #endregion Logic to dynamically set UserControl Width and Height

        #endregion Event Handlers

        #region → Methods        .

        /// <summary>
        /// For Refresh Size
        /// </summary>
        public void ReSize()
        {
            this.Content_Resized(this.UserControl, null);
        }

        #endregion Methods

    }
}
