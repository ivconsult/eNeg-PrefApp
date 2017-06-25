#region → Usings   .
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;



#endregion

#region → History  .

#endregion

#region → ToDos    .
#endregion

namespace citPOINT.PrefApp.Common
{
    /// <summary>
    /// Data Matching Drag Drop
    /// For Draging Values
    /// </summary>
    public class DragDrop
    {

        #region → Fields         .

        private Canvas mTopCanvas;
        private UIElement mCurrentPage;
        private Panel mContainerPanel;
        private bool isDragging = false;
        private Point Offset;
        private DragDropLabelOnFly mDragOnFlyShape;
        private List<UIElement> mDestinationTargets;
        private List<TextBlock> mSourceTargets;
        
        TextBlock mSourceTextBlock = null;

        #endregion

        #region → Properties     .

        #region → Private        .
        
        /// <summary>
        /// Gets or sets the top canvas.
        /// </summary>
        /// <value>The top canvas.</value>
        private Canvas TopCanvas
        {
            get { return mTopCanvas; }
            set { mTopCanvas = value; }
        }

        /// <summary>
        /// Gets or sets the current page.
        /// </summary>
        /// <value>The current page.</value>
        private UIElement CurrentPage
        {
            get { return mCurrentPage; }
            set { mCurrentPage = value; }
        }

        /// <summary>
        /// Gets or sets the container panel.
        /// </summary>
        /// <value>The container panel.</value>
        private Panel ContainerPanel
        {
            get { return mContainerPanel; }
            set { mContainerPanel = value; }
        }

        /// <summary>
        /// Gets the drag on fly Shape.
        /// </summary>
        /// <value>The drag on fly Shape.</value>
        private DragDropLabelOnFly DragOnFlyShape
        {
            get
            {
                if (mDragOnFlyShape == null)
                {
                    mDragOnFlyShape = new DragDropLabelOnFly(TopCanvas);
                }

                return mDragOnFlyShape;
            }
        }

        #endregion

        #region → Public         .

        /// <summary>
        /// Gets the Source targets.
        /// e.g. the text block which we will copy from.
        /// </summary>
        /// <value>The source targets.</value>
        public List<TextBlock> SourceTargets
        {
            get
            {
                if (mSourceTargets == null)
                {
                    mSourceTargets = new List<TextBlock>();
                }
                return mSourceTargets;
            }
        }

        /// <summary>
        /// Gets the destination targets.
        /// e.g. the text block which we will Past Into.
        /// </summary>
        /// <value>The destination targets.</value>
        public List<UIElement> DestinationTargets
        {
            get
            {
                if (mDestinationTargets == null)
                {
                    mDestinationTargets = new List<UIElement>();
                }
                return mDestinationTargets;
            }
        }

        #endregion

        #endregion

        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="DragDrop"/> class.
        /// </summary>
        /// <param name="CurrentPage">The current page.</param>
        /// <param name="TopCanvas">The top canvas.</param>
        /// <param name="ContainerPanel">The container panel.</param>
        public DragDrop(UIElement CurrentPage, Canvas TopCanvas, Panel ContainerPanel)
        {
            this.CurrentPage = CurrentPage;
            this.TopCanvas = TopCanvas;
            this.ContainerPanel = ContainerPanel;

            #region Intializing Mouse Events

            CurrentPage.MouseLeftButtonDown += new MouseButtonEventHandler(MouseLeftButtonDown);
            CurrentPage.MouseLeftButtonUp += new MouseButtonEventHandler(MouseLeftButtonUp);
            CurrentPage.MouseMove += new MouseEventHandler(MouseMove);

            ContainerPanel.MouseLeftButtonDown += new MouseButtonEventHandler(MouseLeftButtonDown);
            ContainerPanel.MouseLeftButtonUp += new MouseButtonEventHandler(MouseLeftButtonUp);
            ContainerPanel.MouseMove += new MouseEventHandler(MouseMove);

            #endregion
        }

        #endregion

        #region → Events         .
        /// <summary>
        /// Occurs when [drag completed].
        /// Mean you have drag and drop
        /// </summary>
        public event EventHandler<DragEventArgs> DragDropCompleted;
        #endregion Event

        #region → Event Handlers .

        /// <summary>
        /// Mouses the left button down.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        void MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            var FoundedControls = VisualTreeHelper.FindElementsInHostCoordinates(e.GetPosition(this.CurrentPage), this.CurrentPage);

            mSourceTextBlock = null;


            foreach (var xControl in FoundedControls.Where(s => s.GetType().Equals(typeof(TextBlock))).AsQueryable())
            {
                if (xControl.GetType().Equals(typeof(TextBlock)) &&
                    //Check if their is a valid Value.
                    !string.IsNullOrEmpty((xControl as TextBlock).Text) &&
                    !string.IsNullOrWhiteSpace((xControl as TextBlock).Text) &&
                    //to be sure that the Current Control is one of the Sources
                    SourceTargets.Contains((xControl as TextBlock)))
                {
                    mSourceTextBlock = xControl as TextBlock;

                    break;
                }
            }

            if (mSourceTextBlock == null)
            {
                if (e.OriginalSource != null && e.OriginalSource.GetType().Equals(typeof(TextBlock)) &&
                    //Check if their is a valid Value.
                 !string.IsNullOrEmpty((e.OriginalSource as TextBlock).Text) &&
                 !string.IsNullOrWhiteSpace((e.OriginalSource as TextBlock).Text) &&
                    //to be sure that the Current Control is one of the Sources
                 SourceTargets.Contains((e.OriginalSource as TextBlock)))
                {
                    mSourceTextBlock = e.OriginalSource as TextBlock;

                }
            }


            if (mSourceTextBlock != null)
            {
                // Mark that we're doing a drag
                isDragging = true;

                // Ensure that the mouse can't leave the dragon
                this.TopCanvas.CaptureMouse();


                // Determine where the mouse 'grabbed' 
                // to use during MouseMove
                //Offset = e.GetPosition(mSourceTextBlock);

                Offset = PrefAppConfigurations.AdjustDragDropPoints;

                Offset = new Point(-10, 10);


                //Setting the New label (fly Label) with the drag text
                DragOnFlyShape.uxDragOnFlyLabel.Text = mSourceTextBlock.Text;

                DragOnFlyShape.MainControl.SetValue(Canvas.LeftProperty, e.GetPosition(this.CurrentPage).X - Offset.X);
                DragOnFlyShape.MainControl.SetValue(Canvas.TopProperty, e.GetPosition(this.CurrentPage).Y - Offset.Y);

                //Change The Visibilty of Main Control (Shape)
                DragOnFlyShape.MainControl.Visibility = System.Windows.Visibility.Visible;

            }
        }

        /// <summary>
        /// Mouses the move.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseEventArgs"/> instance containing the event data.</param>
        void MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                //Clear the Mask OF the Last Control Has a amask
                ClearMask();

                // Where is the mouse now?
                Point newPosition = e.GetPosition(this.CurrentPage);

                DragOnFlyShape.MainControl.SetValue(Canvas.LeftProperty, newPosition.X - Offset.X);
                DragOnFlyShape.MainControl.SetValue(Canvas.TopProperty, newPosition.Y - Offset.Y);

                #region Visible the Best Icon Up On Can Drag Or Not

                UIElement uxCtrl = GetDragedTarget(e.GetPosition(this.CurrentPage));
                if (uxCtrl != null && DestinationTargets.Contains(uxCtrl))
                {
                    this.DragOnFlyShape.CanDrag = true;

                    //Adding Effects to Current Control
                    AddMask(uxCtrl);
                }
                else
                {
                    this.DragOnFlyShape.CanDrag = false;
                }

                #endregion

            }
        }
         
        /// <summary>
        /// Mouses the left button up.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        void MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            //Clear the Mask OF the Last Control Has a amask
            ClearMask();

            if (isDragging)
            {
                // Turn off Drag and Drop
                isDragging = false;

                // Free the Mouse
                TopCanvas.ReleaseMouseCapture();


                DragOnFlyShape.MainControl.Visibility = System.Windows.Visibility.Collapsed;

                UIElement uxCtrl = GetDragedTarget(e.GetPosition(this.CurrentPage));


                if (uxCtrl != null && DestinationTargets.Contains(uxCtrl) && DragDropCompleted != null)
                    DragDropCompleted(this, new DragEventArgs(uxCtrl, DragOnFlyShape.uxDragOnFlyLabel.Text));

                if (mSourceTextBlock != null)
                {
                    mSourceTextBlock.Visibility = Visibility.Collapsed;
                    mSourceTextBlock = null;
                }

            }
        }
        #endregion

        #region → Methods        .
        #region → Private        .
         
        /// <summary>
        /// Gets the draged target.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns> Target UIElement </returns>
        private UIElement GetDragedTarget(Point point)
        {
            point.Y += PrefAppConfigurations.AdjustDragDropPoints.Y + 10; /* 20 top menu height */

            point.X += PrefAppConfigurations.AdjustDragDropPoints.X - 10 /*10 is the margin of data matching*/;

            var FoundedControls = VisualTreeHelper.FindElementsInHostCoordinates(point, this.CurrentPage);

            UIElement uxCtrl = null;

            foreach (var xControl in FoundedControls)
            {

                if (DestinationTargets.Contains(xControl))
                {
                    uxCtrl = xControl;
                    break;
                }
            }

            return uxCtrl;
        }
         
        /// <summary>
        /// Clears the mask.
        /// </summary>
        private void ClearMask()
        {
            foreach (var ctrl in DestinationTargets)
            {
                Border uxDragBorder = null;

                if (ctrl.GetType().Equals(typeof(IssuesDatamatchingItem)))
                {
                    uxDragBorder = (ctrl as IssuesDatamatchingItem).uxbrdDetails;
                }
                else if (ctrl.GetType().Equals(typeof(Border)))
                {
                    uxDragBorder = (ctrl as Border);
                }

                if (uxDragBorder != null && uxDragBorder.Tag!=null)
                {
                    uxDragBorder.BorderBrush = (Brush)uxDragBorder.Tag;
                    uxDragBorder.BorderThickness = new Thickness(1);
                    uxDragBorder.Opacity = 1;
                }
            }
        }
         
        /// <summary>
        /// Adds the mask.
        /// </summary>
        /// <param name="control">The control.</param>
        private void AddMask(UIElement control)
        {

            Border uxDragBorder = null;

            if (control.GetType().Equals(typeof(IssuesDatamatchingItem)))
            {
                uxDragBorder = (control as IssuesDatamatchingItem).uxbrdDetails;
            }
            else if (control.GetType().Equals(typeof(Border)))
            {
                uxDragBorder = (control as Border);
            }

            if (uxDragBorder != null)
            {
                uxDragBorder.Tag = uxDragBorder.BorderBrush;
                uxDragBorder.BorderBrush = new SolidColorBrush(Colors.Red);
                uxDragBorder.BorderThickness = new Thickness(2);
                uxDragBorder.Opacity = 0.8;
            }
        }

        #endregion Private

        #endregion Methods

    }
}
