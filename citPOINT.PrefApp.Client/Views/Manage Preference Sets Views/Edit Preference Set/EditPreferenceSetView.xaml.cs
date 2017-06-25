#region → Usings   .

using System.Windows.Controls;
using citPOINT.PrefApp.Common;
using citPOINT.PrefApp.ViewModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;

#endregion

#region → History  .

/* Date         User              Change
 * 
 * 28.03.12     M.Wahab       Creation
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
    /// Edit Preference Set View.
    /// </summary>
    public partial class EditPreferenceSetView : UserControl, ICleanup
    {
        #region → Fields         .

        private Top10IssuesView top10IssuesView;

        private static EditPreferenceSetView LastInstance;

        #endregion
        #region → Properties     .

        /// <summary>
        /// Gets the view model.
        /// </summary>
        /// <value>The view model.</value>
        private IssuesViewModel ViewModel
        {
            get
            {
                return (this.DataContext as IssuesViewModel);
            }
        }
        #endregion Properties
        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="EditPreferenceSetView"/> class.
        /// </summary>
        /// <param name="issuesViewModel">The issues view model.</param>
        public EditPreferenceSetView(IssuesViewModel issuesViewModel)
        {
            if (LastInstance != null)
            {
                LastInstance.Cleanup();
            }

            LastInstance = this;

            this.DataContext = issuesViewModel;

            InitializeComponent();

            PrefAppMessanger.FlippMessage.Register(this, OnFlippMessage);
        }

        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Called when [flipp message].
        /// </summary>
        /// <param name="viewName">Name of the view.</param>
        private void OnFlippMessage(string viewName)
        {
            if (viewName == PrefAppViewTypes.InnerIssuesView)
            {
                this.uxtabIssueValues.SelectedIndex = 0;
            }
            else if (viewName == PrefAppViewTypes.InnerValuesView)
            {
                this.uxtabIssueValues.SelectedIndex = 1;
            }
            else if (viewName == PrefAppViewTypes.Top10IssuesViews)
            {
                if (this.top10IssuesView != null)
                {
                    this.top10IssuesView.Cleanup();
                }

                this.top10IssuesView = new Top10IssuesView(this.ViewModel.CurrentPreferenceSet.IsEditable);

                #region Show PopUp window to choose identify an Issue and add it
                PopUpWindow SendMailWindow = new PopUpWindow("Issue statistics");
                SendMailWindow.Content = this.top10IssuesView;
                SendMailWindow.ShowDialog();
                #endregion
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
