
#region → Usings   .
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using citPOINT.eNeg.Common;
using citPOINT.PrefApp.Common;
using citPOINT.PrefApp.Data.Web;
using GalaSoft.MvvmLight;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Silverlight.Testing;
using citPOINT.PrefApp.ViewModel;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;
#endregion

#region → History  .

/* Date         User            Change
 * 
 * 26.09.10     Yousra Reda         • creation
 * 
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion

namespace citPOINT.PrefApp.MVVM.Test
{
    /// <summary>
    /// This is a test class for PrefApp PreferenceSetModel and PreferenceSetViewModel
    /// </summary>
    [TestClass]
    public class PreferenceSetsVMTest : SilverlightTest
    {

        #region → Fields         .

        private PreferenceSetsViewModel mPreferenceSetsViewModel;
        private string ErrorMessage;

        #endregion Fields

        #region → Properties     .

        /// <summary>
        /// Gets or sets the preference sets view model.
        /// </summary>
        /// <value>The preference sets view model.</value>
        [Import(PrefAppViewModelTypes.PreferenceSetsViewModel)]
        public PreferenceSetsViewModel PreferenceSetsViewModel
        {
            get { return this.mPreferenceSetsViewModel; }
            set { this.mPreferenceSetsViewModel = value; }
        }

        #endregion



        #region → Commands       .


        /// <summary>
        /// Adds the preference set command.
        /// </summary>
        [Asynchronous]
        [TestMethod]
        public void AddPreferenceSetCommand()
        {
            int PreferenceSetCountAfterAdd = this.PreferenceSetsViewModel.PreferenceSetsSource.Count + 1;
            this.PreferenceSetsViewModel.CurrentPreferenceSet = null;

            this.PreferenceSetsViewModel.AddNewPreferenceSetCommand.Execute(null);

            EnqueueCallback(() => Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage)));
            EnqueueCallback(() => Assert.IsTrue(this.PreferenceSetsViewModel.PreferenceSetsSource.Count == PreferenceSetCountAfterAdd, "Cannot Execute Add Item Comand"));
            EnqueueTestComplete();
        }

        /// <summary>
        /// Removes the preference set command.
        /// </summary>
        [Asynchronous]
        [TestMethod]
        public void DeletePreferenceSetCommand()
        {
            this.PreferenceSetsViewModel.CurrentPreferenceSet = this.PreferenceSetsViewModel.PreferenceSetsSource[this.PreferenceSetsViewModel.PreferenceSetsSource.Count - 1];

            int PreferenceSetsCountAfterDelete = this.PreferenceSetsViewModel.PreferenceSetsSource.Count - 1;
            this.PreferenceSetsViewModel.DeleteItemCommand.Execute(null);

            int PrefernceSetCount = this.PreferenceSetsViewModel.PreferenceSetsSource.Count();

            EnqueueCallback(() => Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage)));
            EnqueueCallback(() => Assert.IsTrue(PreferenceSetsCountAfterDelete == PrefernceSetCount, "Cannot Execute Delete Item Comand"));
            EnqueueTestComplete();
        }


        /// <summary>
        /// Adds the new issue command.
        /// </summary>
        [Asynchronous]
        [TestMethod]
        public void AddNewIssueCommand()
        {
            //Set The Current Preference set Like Choose Node From Tree.
            this.PreferenceSetsViewModel.CurrentPreferenceSet = this.PreferenceSetsViewModel.PreferenceSetsSource[0];

            //Setting The Expected Issues count After add.
            int IssuesCountAfterAdd = this.PreferenceSetsViewModel.IssuesSource.Count + 1;

            //Execute The Command and wait for Results
            this.PreferenceSetsViewModel.AddNewIssueCommand.Execute(null);

            EnqueueCallback(() => Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage)));

            EnqueueCallback(() => Assert.IsTrue(this.PreferenceSetsViewModel.IssuesSource.Count == IssuesCountAfterAdd, "Cannot Execute Add New Issue Command"));

            EnqueueTestComplete();
        }


        /// <summary>
        /// Adds the new issue command.
        /// </summary>
        [Asynchronous]
        [TestMethod]
        public void DeleteIssueCommand()
        {
            //Set The Current Preference set Like Choose Node From Tree.
            this.PreferenceSetsViewModel.CurrentPreferenceSet = this.PreferenceSetsViewModel.PreferenceSetsSource[0];
            this.PreferenceSetsViewModel.CurrentIssue = this.PreferenceSetsViewModel.CurrentPreferenceSet.Issues.FirstOrDefault();

            this.PreferenceSetsViewModel.CurrentIssue.IsSelected = true;


            //Setting The Expected Issues count After add.
            int IssuesCountAfterDelete = this.PreferenceSetsViewModel.IssuesSource.Count - 1;

            //Execute The Command and wait for Results
            this.PreferenceSetsViewModel.DeleteIssueCommand.Execute(null);

            EnqueueCallback(() => Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage)));

            EnqueueCallback(() => Assert.IsTrue(this.PreferenceSetsViewModel.IssuesSource.Count == IssuesCountAfterDelete, "Cannot Execute Delete an Issue Command"));

            EnqueueTestComplete();
        }


        /// <summary>
        /// Adds the new option command.
        /// </summary>
        [Asynchronous]
        [TestMethod]
        public void AddNewOptionCommand()
        {
            //Set The Current Preference set Like Choose Node From Tree.
            this.PreferenceSetsViewModel.CurrentPreferenceSet = this.PreferenceSetsViewModel.PreferenceSetsSource[0];
            this.PreferenceSetsViewModel.CurrentIssue = this.PreferenceSetsViewModel.CurrentPreferenceSet.Issues.FirstOrDefault(s => s.IssueTypeID == PrefAppConstant.IssueTypes.Options);


            //Setting The Expected Options count After add.
            int OptionsCountAfterAdd = this.PreferenceSetsViewModel.OptionsViewModel.OptionIssueSource.Count + 1;

            //Execute The Command and wait for Results
            this.PreferenceSetsViewModel.OptionsViewModel.AddNewOptionCommand.Execute(null);

            EnqueueCallback(() => Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage)));

            EnqueueCallback(() => Assert.IsTrue(this.PreferenceSetsViewModel.OptionsViewModel.OptionIssueSource.Count == OptionsCountAfterAdd, "Cannot Execute Add New Option Command"));

            EnqueueTestComplete();
        }


        /// <summary>
        /// Deletes the option command.
        /// </summary>
        [Asynchronous]
        [TestMethod]
        public void DeleteOptionCommand()
        {
            //Set The Current Preference set Like Choose Node From Tree.
            this.PreferenceSetsViewModel.CurrentPreferenceSet = this.PreferenceSetsViewModel.PreferenceSetsSource[0];

            //Setting the Current Issue
            this.PreferenceSetsViewModel.CurrentIssue = this.PreferenceSetsViewModel.CurrentPreferenceSet.Issues.Where(s => s.IssueTypeID == PrefAppConstant.IssueTypes.Options && s.OptionIssues.Count() > 0).LastOrDefault();

            //Setting Like Checked in the Grid.
            this.PreferenceSetsViewModel.CurrentIssue.OptionIssues.FirstOrDefault().IsSelected = true;

            //Setting The Expected Options count After add.
            int OptionsCountAfterDelete = this.PreferenceSetsViewModel.OptionsViewModel.OptionIssueSource.Count - 1;

            //Execute The Command and wait for Results
            this.PreferenceSetsViewModel.OptionsViewModel.DeleteOptionCommand.Execute(null);


            EnqueueCallback(() => Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage)));

            EnqueueCallback(() => Assert.IsTrue(this.PreferenceSetsViewModel.OptionsViewModel.OptionIssueSource.Count == OptionsCountAfterDelete, "Cannot Execute Delete Option Command"));

            EnqueueTestComplete();
        }


        /// <summary>
        /// Change issue type command.
        /// </summary>
        [Asynchronous]
        [TestMethod]
        public void ChangeIssueTypeCommand()
        {
            //Set The Current Preference set Like Choose Node From Tree.
            this.PreferenceSetsViewModel.CurrentPreferenceSet = this.PreferenceSetsViewModel.PreferenceSetsSource[1];

            //Setting the Current Issue
            this.PreferenceSetsViewModel.CurrentIssue = this.PreferenceSetsViewModel.CurrentPreferenceSet.Issues.FirstOrDefault(s => s.IssueTypeID == PrefAppConstant.IssueTypes.Options);

            //Setting The Expected Options count After add.
            int OptionsCountAfterDelete = 0;

            this.PreferenceSetsViewModel.CurrentIssue.IssueTypeID = PrefAppConstant.IssueTypes.NotRated;

            //Execute The Command and wait for Results
            this.PreferenceSetsViewModel.ChangeIssueTypeCommand.Execute(null);


            EnqueueCallback(() => Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage)));

            EnqueueCallback(() => Assert.IsTrue(this.PreferenceSetsViewModel.CurrentIssue.OptionIssues.Count == OptionsCountAfterDelete, "Cannot Execute Change Issue Type Command."));

            EnqueueTestComplete();
        }


        #endregion

        #region → Methods        .

        #region → Public         .
        /// <summary>
        /// Builds up.
        /// </summary>
        [TestInitialize]
        public void BuidUp()
        {
            CompositionInitializer.SatisfyImports(this);

            #region " Registeration for needed messages in eNegMessenger "
            // register for RaiseErrorMessage
            PrefAppMessanger.RaiseErrorMessage.Register(this, OnRaiseErrorMessage);
            // register for PleaseConfirmMessage
            PrefAppMessanger.ConfirmMessage.Register(this, OnConfirmMessage);
            #endregion

        }

        /// <summary>
        /// Tests the basics.
        /// </summary>
        [TestMethod]
        public void TestSuccessOfMEF()
        {
            Assert.IsNotNull(this.PreferenceSetsViewModel, "Failed to retrieve the viewmodel via MEF");
        }

        /// <summary>
        /// Gets the main preference set async.
        /// </summary>
        [Asynchronous]
        [TestMethod]
        public void GetMainPreferenceSetAsync()
        {
            EnqueueCallback(() => this.PreferenceSetsViewModel.GetMainPreferenceSetAsync());
            EnqueueCallback(() => Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage)));
            EnqueueCallback(() => Assert.IsTrue(this.PreferenceSetsViewModel.MainPreferenceSets.Count() > 0, "No Main Preference Sets Found"));
            EnqueueTestComplete();
        }


        /// <summary>
        /// Gets the preference set async.
        /// </summary>
        [Asynchronous]
        [TestMethod]
        public void GetPreferenceSetAsync()
        {
            EnqueueCallback(() => this.PreferenceSetsViewModel.GetPreferenceSetAsync());
            EnqueueCallback(() => Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage)));
            EnqueueCallback(() => Assert.IsTrue(this.PreferenceSetsViewModel.PreferenceSets.Count() > 0, "No Preference Sets Found"));
            EnqueueTestComplete();
        }




        /// <summary>
        /// Gets the issue types async.
        /// </summary>
        [Asynchronous]
        [TestMethod]
        public void GetIssueTypesAsync()
        {
            EnqueueCallback(() => this.PreferenceSetsViewModel.GetIssueTypesAsync());
            EnqueueCallback(() => Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage)));
            EnqueueCallback(() => Assert.IsTrue(this.PreferenceSetsViewModel.IssueTypes.Count() > 0, "No IssueTypes Found"));
            EnqueueTestComplete();
        }


        /// <summary>
        /// Gets the issues async.
        /// </summary>
        [Asynchronous]
        [TestMethod]
        public void GetIssuesAsync()
        {
            EnqueueCallback(() => this.PreferenceSetsViewModel.GetIssuesAsync());
            EnqueueCallback(() => Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage)));
            EnqueueCallback(() => Assert.IsTrue(this.PreferenceSetsViewModel.Issues.Count() > 0, "No Issues Found"));

            EnqueueTestComplete();
        }

        /// <summary>
        /// Gets the numeric issues async.
        /// </summary>
        [Asynchronous]
        [TestMethod]
        public void GetNumericIssuesAsync()
        {
            EnqueueCallback(() => this.PreferenceSetsViewModel.GetNumericIssuesAsync());
            EnqueueCallback(() => Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage)));
            EnqueueCallback(() => Assert.IsTrue(this.PreferenceSetsViewModel.NumericIssues.Count() > 0, "No Numeric Issues Found"));
            EnqueueTestComplete();
        }

        /// <summary>
        /// Gets the option issues async.
        /// </summary>
        [Asynchronous]
        [TestMethod]
        public void GetOptionIssuesAsync()
        {
            EnqueueCallback(() => this.PreferenceSetsViewModel.GetOptionIssuesAsync());
            EnqueueCallback(() => Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage)));
            EnqueueCallback(() => Assert.IsTrue(this.PreferenceSetsViewModel.OptionIssues.Count() > 0, "No Option Issues Found"));
            EnqueueTestComplete();
        }

        /// <summary>
        /// Gets the later rated issues async.
        /// </summary>
        [Asynchronous]
        [TestMethod]
        public void GetLaterRatedIssuesAsync()
        {
            EnqueueCallback(() => this.PreferenceSetsViewModel.GetLaterRatedIssuesAsync());
            EnqueueCallback(() => Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage)));
            EnqueueCallback(() => Assert.IsTrue(this.PreferenceSetsViewModel.LaterRatedIssues.Count() > 0, "No Later Rated Issues Found"));
            EnqueueTestComplete();
        }

        /// <summary>
        /// Used To Clean All Resources
        /// </summary>
        [TestCleanup]
        public void CleanUp()
        {
            // call Cleanup on its ViewModel
            this.PreferenceSetsViewModel.Cleanup();

            // Cleanup itself
            Messenger.Default.Unregister(this);

 //           this.PreferenceSetsViewModel.mPrefSetsModel.RejectChanges();
 //this.PreferenceSetsViewModel.mPrefSetsModel = null;
 //           this.PreferenceSetsViewModel = null;

           

        }
        #endregion Public

        #region → Private        .

        #region " RaiseErrorMessage "

        /// <summary>
        /// Raise error message if there is any layer send RaiseErrorMessage
        /// </summary>
        /// <param name="ex">exception to raise</param>
        private void OnRaiseErrorMessage(Exception ex)
        {
            if (ex != null)
            {

                if (ex.InnerException != null)
                {
                    ErrorMessage = ex.Message + "\r\n" + ex.InnerException.Message;

                }
                else
                    ErrorMessage = ex.Message;

                //MessageBox.Show(ErrorMessage, "Error", MessageBoxButton.OK);
            }
        }

        #endregion "RaiseErrorMessage"


        #region " ConfirmMessage "

        /// <summary>
        /// Display Confirmation Message and resent back the result choosen 
        /// </summary>
        /// <param name="dialogMessage">dialogMessage</param>
        private void OnConfirmMessage(DialogMessage dialogMessage)
        {
            if (dialogMessage != null)
            {
                dialogMessage.Callback(MessageBoxResult.OK);
            }
        }
        #endregion



        #endregion Private
        #endregion Methods
    }
}
