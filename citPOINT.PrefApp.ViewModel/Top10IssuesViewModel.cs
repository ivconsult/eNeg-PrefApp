#region → Usings   .
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using citPOINT.PrefApp.Common;
using citPOINT.PrefApp.Data.Web;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Command;
using System.ComponentModel;
#endregion

#region → History  .

/* 
 * Date         User            Change
 * *********************************************
 * 3/29/2012 10:58:03 AM      mwahab         • creation
 * **********************************************
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
    #region  Using MEF to export Top10IssuesViewModel
    /// <summary>
    /// Class to get Top 10 Issues.
    /// </summary>
    [Export(PrefAppViewModelTypes.Top10IssuesViewModel)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    #endregion
    public class Top10IssuesViewModel : ViewModelBase
    {
        #region → Fields         .

        private ITop10IssuesModel top10IssuesModel;

        private bool mIsBusy;

        IEnumerable<IssueStatisticalsResult> mTop10IssuesSource;
        private RelayCommand mDragSelectedIssuesCommand;
        private RelayCommand mCancelCommandCommand;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets a value indicating whether this instance is busy.
        /// </summary>
        /// <value><c>true</c> if this instance is busy; otherwise, <c>false</c>.</value>
        public bool IsBusy
        {
            get
            {
                return mIsBusy;
            }
            set
            {
                mIsBusy = value;
                this.RaisePropertyChanged("IsBusy");
            }
        }

        /// <summary>
        /// Gets or sets the top10 issues source.
        /// </summary>
        /// <value>The top10 issues source.</value>
        public IEnumerable<IssueStatisticalsResult> Top10IssuesSource
        {
            get
            {
                return mTop10IssuesSource;
            }
            set
            {
                mTop10IssuesSource = value;
                this.RaisePropertyChanged("Top10IssuesSource");
            }
        }

        #endregion Properties

        #region → Constractor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="Top10IssuesViewModel"/> class.
        /// </summary>
        /// <param name="top10IssuesModel">The top10 issues model.</param>
        [ImportingConstructor]
        public Top10IssuesViewModel(ITop10IssuesModel top10IssuesModel)
        {
            #region → Initialization Variables .

            this.Top10IssuesSource = new List<IssueStatisticalsResult>();

            this.top10IssuesModel = top10IssuesModel;

            #endregion

            #region → Set up event Handling    .

            this.top10IssuesModel.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(top10IssuesModel_PropertyChanged);

            this.top10IssuesModel.GetIssueStatisticalsComplete += new EventHandler<eNeg.Common.eNegEntityResultArgs<Data.Web.IssueStatisticalsResult>>(top10IssuesModel_GetIssueStatisticalsComplete);

            #endregion

            #region → Loading Related Tables   .

            this.GetIssueStatisticalsAsync();

            #endregion
        }

        #endregion

        #region → Event Handlers .

        /// <summary>
        /// Call back of Get issue statisticals.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void top10IssuesModel_GetIssueStatisticalsComplete(object sender, eNeg.Common.eNegEntityResultArgs<Data.Web.IssueStatisticalsResult> e)
        {
            if (!e.HasError)
            {
                this.Top10IssuesSource = e.Results;

                //Adding observe for selection
                foreach (var top10Issue in this.Top10IssuesSource)
                {
                    top10Issue.PropertyChanged += new PropertyChangedEventHandler(top10Issue_PropertyChanged);
                }
            }
            else
            {
                PrefAppMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Handles the PropertyChanged event of the top10Issue control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void top10Issue_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("IsSelected", StringComparison.InvariantCultureIgnoreCase))
            {
                DragSelectedIssuesCommand.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Handles the PropertyChanged event of the top10IssuesModel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void top10IssuesModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("IsBusy"))
            {
                this.IsBusy = top10IssuesModel.IsBusy;
            }
        }

        #endregion

        #region → Command        .

        /// <summary>
        /// Gets the drag selected issues command.
        /// </summary>
        /// <value>The drag selected issues command.</value>
        public RelayCommand DragSelectedIssuesCommand
        {
            get
            {
                if (mDragSelectedIssuesCommand == null)
                {

                    mDragSelectedIssuesCommand = new RelayCommand(() =>
                    {
                        if (!this.IsBusy)
                        {
                            IEnumerable<string> selectedList = this.Top10IssuesSource
                                                                   .Where(s => s.IsSelected)
                                                                   .Select(s => s.IssueName);

                            PrefAppMessanger.DragIssueMessage.Send(selectedList);

                            PrefAppMessanger.FlippMessage.Send(PrefAppViewTypes.ClosePopupView);
                        }
                    }
                 , () => !this.IsBusy &&
                          this.Top10IssuesSource.Where(s => s.IsSelected).Count() > 0);
                }
                return mDragSelectedIssuesCommand;
            }
        }

        /// <summary>
        /// Gets the cancel command.
        /// </summary>
        /// <value>The cancel command.</value>
        public RelayCommand CancelCommand
        {
            get
            {
                if (mCancelCommandCommand == null)
                {
                    mCancelCommandCommand = new RelayCommand(() =>
                    {
                        PrefAppMessanger.FlippMessage.Send(PrefAppViewTypes.ClosePopupView);
                    }
                 , () => true);
                }
                return mCancelCommandCommand;
            }
        }

        #endregion

        #region → Methods        .

        #region → Public         .

        /// <summary>
        /// Gets the issue statisticals async.
        /// </summary>
        public void GetIssueStatisticalsAsync()
        {
            this.top10IssuesModel.GetIssueStatisticalsAsync();
        }

        /// <summary>
        /// Cleanups this instance.
        /// </summary>
        public override void Cleanup()
        {
            base.Cleanup();

            this.top10IssuesModel.PropertyChanged -= new System.ComponentModel.PropertyChangedEventHandler(top10IssuesModel_PropertyChanged);
            this.top10IssuesModel.GetIssueStatisticalsComplete -= new EventHandler<eNeg.Common.eNegEntityResultArgs<Data.Web.IssueStatisticalsResult>>(top10IssuesModel_GetIssueStatisticalsComplete);

            Messenger.Default.Unregister(this);
        }

        #endregion

        #endregion

    }
}
