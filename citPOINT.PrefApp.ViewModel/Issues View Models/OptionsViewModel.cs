
#region → Usings   .

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;
using citPOINT.PrefApp.Common;
using citPOINT.PrefApp.Data.Web;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using citPOINT.eNeg.Common;

#endregion

#region → History  .

/* 
 * Date                       User            Change
 * *********************************************************
 * 07/06/2011 12:03:04 PM      mwahab         • creation
 * *********************************************************
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

    /// <summary>
    /// Class for Preference Set Options View Model 
    /// </summary>
    public class OptionsViewModel : ViewModelBase, IIssueDetailsViewModel
    {
        #region → Fields         .

        private bool mHasChanges;
        private bool mIsBusy;
        private bool mIsExpanded;

        private IssuesViewModel mIssuesViewModel;

        private Issue mCurrentIssue;

        private List<Guid> mSelectedIDs;

        private RelayCommand mAddNewOptionCommand = null;
        private RelayCommand mDeleteOptionCommand = null;

        private IPreferenceSetsModel preferenceSetsModel;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets a value indicating whether this instance is expanded.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is expanded; otherwise, <c>false</c>.
        /// </value>
        public bool IsExpanded
        {
            get
            {
                return mIsExpanded;
            }
            set
            {
                mIsExpanded = value;

                this.RaisePropertyChanged("IsExpanded");
            }
        }

        /// </summary>
        /// <value>The name of the view.</value>
        public string ViewName
        {
            get { return PrefAppViewTypes.OptionIssue; }
        }

        /// <summary>
        /// Gets the first view model.
        /// </summary>
        /// <value>The first view model.</value>
        public IIssueDetailsViewModel FirstViewModel
        {
            get { return this; }
        }

        /// <summary>
        /// Gets the view model.
        /// Current Insatnce wrapped in list form for Binding
        /// </summary>
        /// <value>The view model.</value>
        public IEnumerable<IIssueDetailsViewModel> ViewModel
        {
            get
            {
                return new List<IIssueDetailsViewModel> { this }.AsEnumerable();
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has changes.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has changes; otherwise, <c>false</c>.
        /// </value>
        public bool HasChanges
        {
            get
            {
                return mHasChanges;
            }
            set
            {
                mHasChanges = value;
                RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is busy.
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

                RaisePropertyChanged("IsBusy");
                RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is all valid.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is all valid; otherwise, <c>false</c>.
        /// </value>
        public bool IsAllValid
        {
            get { return IsAllOptionsValuesValid(); }
        }

        /// <summary>
        /// Gets or sets the current Issue
        /// </summary>
        public Issue CurrentIssue
        {
            get { return mCurrentIssue; }
            set
            {
                if (mCurrentIssue != value || true)
                {
                    mCurrentIssue = value;
                    this.RaisePropertyChanged("CurrentIssue");

                    if (mCurrentIssue != null)
                    {
                        OptionIssueSource = new ObservableCollection<OptionIssue>(mCurrentIssue.OptionIssues.OrderBy(s => s.DeletedOn));
                        this.RaisePropertyChanged("OptionIssueSource");

                        foreach (var optionItem in this.OptionIssueSource)
                        {
                            optionItem.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(optionItem_PropertyChanged);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the option issue source.
        /// </summary>
        /// <value>The option issue source.</value>
        public ObservableCollection<OptionIssue> OptionIssueSource { get; set; }

        #endregion

        #region → Constractor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="OptionsViewModel"/> class.
        /// </summary>
        /// <param name="issue">The issue.</param>
        /// <param name="preferenceSetsModel">The preference sets model.</param>
        public OptionsViewModel(Issue issue, IPreferenceSetsModel preferenceSetsModel)
        {
            this.CurrentIssue = issue;
            this.preferenceSetsModel = preferenceSetsModel;
        }

        #endregion

        #region → Event handlers .

        /// <summary>
        /// Handles the PropertyChanged event of the optionItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void optionItem_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsSelected")
            {
                this.RaiseCanExecuteChanged();
            }
        }

        #endregion

        #region → Commands       .

        /// <summary>
        /// Gets the add new option command.
        /// </summary>
        /// <value>The add new option command.</value>
        public RelayCommand AddNewOptionCommand
        {
            get
            {
                if (mAddNewOptionCommand == null)
                {
                    mAddNewOptionCommand = new RelayCommand(() =>
                    {

                        if (!this.IsBusy && this.CurrentIssue != null && IsAllOptionsValuesValid())
                        {
                            OptionIssue mCurrentOption = this.AddOptionIssue(this.CurrentIssue);

                            this.CurrentIssue.OptionIssues.Add(mCurrentOption);

                            OptionIssueSource = new ObservableCollection<OptionIssue>(mCurrentIssue.OptionIssues.OrderBy(s => s.DeletedOn));
                            this.RaisePropertyChanged("OptionIssueSource");

                            PrefAppMessanger.EditOptionIssueMessage.Send(mCurrentOption);

                            mCurrentOption.IsNewOption = false;

                            //Check if The current Preference Set has Negs so every add for New option will lead to 
                            //Asking the user whether he wants to send mail to Negotiators with this new issue option or not
                            if (CurrentIssue.PreferenceSet.PreferenceSetNegs.Where(s => s.Deleted == false).Count() > 0)
                            {
                                PrefAppConfigurations.IsNewIssuePending = true;

                                if (PrefAppConfigurations.PendingItems == null)
                                {
                                    PrefAppConfigurations.PendingItems = new List<PendingItem>();
                                }
                                PrefAppConfigurations.PendingItems.Add(PendingItem.CreateOptionIssue(mCurrentOption.OptionIssueID));
                            }
                        }
                    }, () => this.IsBusy == false);
                }

                return mAddNewOptionCommand;
            }
        }

        /// <summary>
        /// Gets the delete issue command.
        /// </summary>
        /// <value>The delete issue command.</value>
        public RelayCommand DeleteOptionCommand
        {
            get
            {
                if (mDeleteOptionCommand == null)
                {
                    mDeleteOptionCommand = new RelayCommand(() =>
                    {

                        if (!this.IsBusy &&
                            this.CurrentIssue != null &&
                            this.CurrentIssue.OptionIssues.Where(a => a.IsSelected == true).Count() > 0)
                        {
                            Action<MessageBoxResult> callBackResult = null;

                            #region Confirmation Message
                        
                            if (!this.IsBusy)
                            {
                                //Firstly ask user to confirm editing that item
                                DialogMessage dialogMessage = new DialogMessage(this,
                                    Resources.DeleteCurrentItemMessageBoxText, result => callBackResult(result))
                                {
                                    Button = MessageBoxButton.OKCancel,
                                    Caption = Resources.ConfirmMessageBoxCaption
                                };

                                eNegMessanger.ConfirmMessage.Send(dialogMessage);
                            }
                            #endregion Confirmation Message

                            callBackResult = (result) =>
                               {
                                   if (result == MessageBoxResult.Cancel)
                                       return;

                                   #region Delete Process

                                   while (OptionIssueSource.Where(s => s.IsSelected).Count() > 0)
                                   {
                                       OptionIssue optionIssue = OptionIssueSource.Where(s => s.IsSelected).FirstOrDefault();

                                       #region → Remove from pending Issues .

                                       if (PrefAppConfigurations.PendingItems != null)
                                       {
                                           PendingItem pendingItem = PrefAppConfigurations.PendingItems.Where(s => s.PendingID == optionIssue.OptionIssueID).FirstOrDefault();

                                           if (pendingItem != null)
                                           {
                                               PrefAppConfigurations.PendingItems.Remove(pendingItem);

                                               if (OptionIssueSource.Where(s => s.IsSelected).Count() == 1)
                                               {
                                                   PrefAppConfigurations.IsNewIssuePending = false;
                                               }
                                           }
                                       }
                                       #endregion

                                       this.preferenceSetsModel.RemoveOptionIssue(optionIssue);

                                       OptionIssueSource.Remove(optionIssue);
                                   }

                                   #endregion
                               };
                        }
                    },
                    () => !this.IsBusy &&
                        this.CurrentIssue != null &&
                        this.CurrentIssue.OptionIssues.Where(a => a.IsSelected == true).Count() > 0);
                }

                return mDeleteOptionCommand;
            }
        }

        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Raises the can execute changed.
        /// </summary>
        private void RaiseCanExecuteChanged()
        {
            AddNewOptionCommand.RaiseCanExecuteChanged();
            DeleteOptionCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Adds the option issue.
        /// </summary>
        /// <param name="p">if set to <c>true</c> [p].</param>
        /// <param name="issue">The issue.</param>
        /// <returns></returns>
        private OptionIssue AddOptionIssue(Issue issue)
        {
            return this.preferenceSetsModel.AddOptionIssue(true, issue);
        }

        #endregion

        #region → Public         .

        /// <summary>
        /// Determines whether [is all numeric values valid].
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if [is all numeric values valid]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsAllOptionsValuesValid()
        {
            bool IsAllvalid = true;

            if (OptionIssueSource != null && OptionIssueSource.Count > 0)
            {
                foreach (var item in OptionIssueSource)
                {
                    item.ValidationErrors.Clear();

                    if (string.IsNullOrEmpty(item.OptionIssueValue))
                    {
                        item.OptionIssueValue = string.Empty;
                        IsAllvalid = false;
                    }
                    else if (item.OptionIssueValue.Length > 100)
                    {
                        item.ValidationErrors.Add(new ValidationResult(Resources.OptionLongName, new string[] { "OptionIssueValue" }));
                        IsAllvalid = false;
                    }
                    else
                    {
                        if (OptionIssueSource.Count(s => (s.OptionIssueValue.ToLower() == item.OptionIssueValue.ToLower())
                                                && (s.OptionIssueID != item.OptionIssueID)) > 0)
                        {
                            (item as OptionIssue).ValidationErrors.Add(new ValidationResult(Resources.RepeatedOptions, new string[] { "OptionIssueValue" }));
                            IsAllvalid = false;
                        }
                    }

                    if (!(item.TryValidateObject() &&
                        item.TryValidateProperty("OptionIssueValue") &&
                        item.TryValidateProperty("OptionIssueWeight")))
                    {
                        IsAllvalid = false;
                    }
                }
            }

            return IsAllvalid;
        }

        #endregion

        #endregion

    }
}
