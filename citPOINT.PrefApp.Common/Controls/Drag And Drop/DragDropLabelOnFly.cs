/*

     |▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬|
     |         Main Control (StackPanel)             |
     |     |-----------------------------------|     |
     |     |  uxMasterBorderOnFly(StackPanel)  |     |
     |     |          uxBorderOnFly            |     |
     |     |         uxDragOnFlyLabel          |     |
     |     |___________________________________|     |
     |                                               |
     |           ► CancelImage                       |
     |           ► PastImage                         |
     |_______________________________________________|

*/


#region → Usings   .
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
#endregion

#region → History  .

#endregion

#region → ToDos    .
#endregion

namespace citPOINT.PrefApp.Common
{
    /// <summary>
    /// Drag and Drop Shape.
    /// </summary>
    public class DragDropLabelOnFly
    {

        #region → Fields         .

        private Border muxBorderOnFly;
        private TextBlock muxDragOnFlyLabel;
        private Image mCancelImage;
        private Image mPastImage;
        private Border muxMasterBorderOnFly;
        private LinearGradientBrush mMyBruch;
        private StackPanel mMainControl;

        #endregion

        #region → Properties     .


        /// <summary>
        /// Gets my bruch.
        /// Gradient Bruch
        /// </summary>
        /// <value>My bruch.</value>
        private LinearGradientBrush MyBruch
        {

            get
            {
                if (mMyBruch == null)
                {

                    GradientStopCollection gradientStopCollection = new GradientStopCollection();
                    gradientStopCollection.Add(new GradientStop() { Color = Color.FromArgb(255, 255, 220, 180), Offset = .0 });
                    gradientStopCollection.Add(new GradientStop() { Color = Color.FromArgb(255, 255, 195, 100), Offset = .5 });
                    gradientStopCollection.Add(new GradientStop() { Color = Color.FromArgb(255, 255, 226, 145), Offset = 1 });

                    mMyBruch = new LinearGradientBrush(gradientStopCollection, 90);

                }
                return mMyBruch;

            }
        }

        /// <summary>
        /// Gets the main control.
        /// Stack Panel that contains All Controls
        /// </summary>
        /// <value>The main control.</value>
        public StackPanel MainControl
        {
            get
            {
                if (mMainControl == null)
                {
                    mMainControl = new StackPanel()
                    {
                        Visibility = Visibility.Collapsed
                    };
                }
                return mMainControl;
            }
        }

        /// <summary>
        /// Gets the Main Border Control.
        /// </summary>
        /// <value>The ux borer.</value>
        private Border uxMasterBorderOnFly
        {
            get
            {
                if (muxMasterBorderOnFly == null)
                {
                    muxMasterBorderOnFly = new Border()
                    {
                        BorderThickness = new Thickness(1),
                        BorderBrush = MyBruch,
                        CornerRadius = new CornerRadius(5),
                        Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.White)

                    };
                }
                return muxMasterBorderOnFly;
            }
        }

        /// <summary>
        /// Gets the Border Control.
        /// </summary>
        /// <value>The ux borer.</value>
        private Border uxBorderOnFly
        {
            get
            {
                if (muxBorderOnFly == null)
                {

                    muxBorderOnFly = new Border()
                    {
                        BorderThickness = new Thickness(2),
                        BorderBrush = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.White),
                        CornerRadius = new CornerRadius(5),
                        Background = MyBruch
                    };
                }
                return muxBorderOnFly;
            }
        }

        /// <summary>
        /// Gets the drag on fly label.
        /// </summary>
        /// <value>The ux drag on fly label.</value>
        public TextBlock uxDragOnFlyLabel
        {
            get
            {
                if (muxDragOnFlyLabel == null)
                {
                    muxDragOnFlyLabel = new TextBlock()
                    {
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Margin = new Thickness(5),
                        Text = "Dragged Text ....",
                        FontSize = 11.0,
                        MaxWidth = 300,
                        TextWrapping = TextWrapping.Wrap

                    };
                }
                return muxDragOnFlyLabel;
            }
        }

        /// <summary>
        /// Gets the cancel image.
        /// </summary>
        /// <value>The cancel image.</value>
        private Image CancelImage
        {
            get
            {
                if (mCancelImage == null)
                {
                    BitmapImage bi3 = new BitmapImage();
                    bi3.UriSource = new Uri("/citPOINT.PrefApp.Common;component/Images/Cancel-icon.png", UriKind.Relative);

                    mCancelImage = new Image()
                    {
                        Stretch = Stretch.Fill,
                        Source = bi3,
                        Width = 20,
                        Height = 20,
                        Margin = new Thickness(1),
                        Visibility = Visibility.Collapsed,
                        HorizontalAlignment = HorizontalAlignment.Left

                    };


                }
                return mCancelImage;

            }
        }

        /// <summary>
        /// Gets the cancel image.
        /// </summary>
        /// <value>The cancel image.</value>
        private Image PastImage
        {
            get
            {
                if (mPastImage == null)
                {

                    BitmapImage bi3 = new BitmapImage();
                    bi3.CreateOptions = BitmapCreateOptions.IgnoreImageCache;

                    bi3.UriSource = new Uri("/citPOINT.PrefApp.Common;component/Images/CanDrag.png", UriKind.Relative);


                    mPastImage = new Image()
                    {
                        Stretch = Stretch.Fill,
                        Source = bi3,
                        Width = 24,
                        Height = 24,
                        Margin = new Thickness(1),
                        Visibility = Visibility.Collapsed,
                        HorizontalAlignment = HorizontalAlignment.Left

                    };
                }

                return mPastImage;

            }
        }


        /// <summary>
        /// Sets a value indicating whether this instance can drag.
        /// </summary>
        /// <value><c>true</c> if this instance can drag; otherwise, <c>false</c>.</value>
        public bool CanDrag
        {
            set
            {
                PastImage.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
                CancelImage.Visibility = !value ? Visibility.Visible : Visibility.Collapsed;
            }
        }


        #endregion

        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="DragDropLabelOnFly"/> class.
        /// </summary>
        /// <param name="TopCanvas">The top canvas.</param>
        public DragDropLabelOnFly(Canvas TopCanvas)
        {
            #region Intialize The Required controls

            //Set Border into Border
            this.uxMasterBorderOnFly.Child = this.uxBorderOnFly;

            //Set TextBlock (Label) with Border
            this.uxBorderOnFly.Child = this.uxDragOnFlyLabel;
            //Adding Boder to main Control (Actual Control)
            this.MainControl.Children.Add(this.uxMasterBorderOnFly);
            //Adding Cancel Image + Past Image And Then Control with to Visible.
            this.MainControl.Children.Add(this.CancelImage);

            this.MainControl.Children.Add(this.PastImage);

            TopCanvas.Children.Add(this.MainControl);

            #endregion

        }

        #endregion

    }
}
