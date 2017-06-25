#region → Usings   .
using System;
using System.IO;
using System.Windows.Controls;
using citPOINT.PrefApp.Common;
using citPOINT.PrefApp.ViewModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Telerik.Windows.Media.Imaging;
using System.ComponentModel.Composition;
#endregion

#region → History  .

/* Date         User              Change
 * 
 * 12.01.11     Yousra Reda       Creation
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
    /// Used as container to all sub-view used in Data Matching Module 
    /// </summary>
    public partial class ReportView : UserControl, ICleanup
    {
        #region → Properties     .

        #region Using MEF to import Report ViewModel

        /// <summary>
        /// Gets the view model.
        /// </summary>
        /// <value>The view model.</value>
        public ReportViewModel ViewModel
        {
            get
            {
                return (DataContext as ReportViewModel);
            }
        }
        #endregion

        #endregion

        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="MainDataMatchingView"/> class.
        /// </summary>
        public ReportView(ReportViewModel viewModel)
        {
            InitializeComponent();

            #region → Use MEF To load the View Model             .

            if (!ViewModelBase.IsInDesignModeStatic)
            {
                this.DataContext = viewModel;
            }

            this.Loaded += new System.Windows.RoutedEventHandler(ReportView_Loaded);

            #endregion Use MEF To load the View Model

            #region → Registing Gala Soft Messages               .
            PrefAppMessanger.FlippMessage.Register(this, OnFlippingMessage);
            PrefAppMessanger.ExportReport.Register(this, OnExportPNG);
            #endregion
        }

        #endregion

        #region → Event Handlers .

        /// <summary>
        /// Handles the Loaded event of the ReportView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ReportView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Dispatcher.BeginInvoke(() =>
            {
                PrefAppMessanger.FlippMessage.Send(PrefAppViewTypes.ReportGraphView);
            });
        }

        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Ons the flipping message.
        /// </summary>
        /// <param name="PageName">Name of the page.</param>
        private void OnFlippingMessage(string PageName)
        {
            if (PageName == PrefAppViewTypes.ReportGraphView)
            {
                Dispatcher.BeginInvoke(() =>
                {
                    if (ViewModel != null)
                    {
                        uxReportGraph.DefaultView.ChartArea.DataSeries.Clear();
                        var list = ViewModel.ChartValues;

                        if (list != null)
                        {
                            foreach (var Series in list)
                            {
                                if (Series != null)
                                {
                                    uxReportGraph.DefaultView.ChartArea.DataSeries.Add(Series);
                                }
                            }
                            uxReportGraph.DefaultView.ChartArea.AxisX.DefaultLabelFormat = ViewModel.IsShowChartTime ? "dd/MM-HH:mm" : "dd-MMM";
                        }
                    }
                });
            }
        }

        /// <summary>
        /// Called when [export PNG].
        /// </summary>
        /// <param name="fileStream">The file stream.</param>
        private void OnExportPNG(Stream fileStream)
        {
            if (fileStream != null)
            {
                try
                {
                    uxReportGraph.ExportToImage(fileStream, new PngBitmapEncoder());
                    fileStream.Close();
                }
                catch (Exception ex)
                {
                    PrefAppMessanger.RaiseErrorMessage.Send(ex);
                }
            }
        }
        #endregion

        #region → Public         .

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
