
#region → Usings   .
using System;
using System.Collections.Generic;
using System.Linq;
using citPOINT.eNeg.Common;
using citPOINT.PrefApp.Common;
using citPOINT.PrefApp.Data.Web;
using GalaSoft.MvvmLight;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using citPOINT.PrefApp.ViewModel;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;
using System.Windows.Controls;
using citPOINT.PrefApp.Client;
using Telerik.Windows.Controls.Charting;
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

namespace citPOINT.PrefApp.MVVM.UnitTest
{
    /// <summary>
    /// This is a test class for PrefApp PreferenceSetModel and PreferenceSetViewModel
    /// </summary>
    [TestClass]
    public class PreferenceSetsViewModel_Test
    {

        #region → Fields         .

        private PreferenceSetsViewModel mPreferenceSetsViewModel;
        private string ErrorMessage;
        private string LastFlipedMessage;
        private string LastPopMessage;
        #endregion Fields

        #region → Properties     .

        /// <summary>
        /// Gets or sets the preference sets view model.
        /// </summary>
        /// <value>The preference sets view model.</value>
        public PreferenceSetsViewModel TheVM
        {
            get { return this.mPreferenceSetsViewModel; }
            set { this.mPreferenceSetsViewModel = value; }
        }

        #endregion

        #region → Constructors   .

        /// <summary>
        /// Test_s the intialization_ pass.
        /// </summary>
        [TestInitialize]
        public void Test_Intialization_Pass()
        {
            TheVM = new PreferenceSetsViewModel(new MockPreferenceSetsModel());

            #region → Registeration for needed messages in eNegMessenger
            // register for RaiseErrorMessage
            PrefAppMessanger.RaiseErrorMessage.Register(this, OnRaiseErrorMessage);

            // register for PleaseConfirmMessage and always return ok to can complete test seccessfully
            PrefAppMessanger.ConfirmMessage.Register(this, OnConfirmMessage);

            //register for flip message and save the new flipped page name in variable "LastFlipedMessage".
            PrefAppMessanger.FlippMessage.Register(this, OnFlippMessage);

            //register for new pop message and save the new pop typ in variable "LastPopMessage".
            PrefAppMessanger.NewPopUp.Register(this, OnNewPopupMessage);


            eNegMessanger.SendCustomMessage.Register(this, OnCustomMessage);
            #endregion
        }

        #endregion

        #region → Commands       .

        /// <summary>
        /// Adds the preference set command.
        /// </summary>
        [TestMethod]
        public void AddPreferenceSet_ExecuteCommand_AddSuccess()
        {
            int PreferenceSetCountAfterAdd = this.TheVM.PreferenceSetsSource.Count + 1;
            this.TheVM.CurrentPreferenceSet = null;

            this.TheVM.AddNewPreferenceSetCommand.Execute(null);

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(this.TheVM.PreferenceSetsSource.Count == PreferenceSetCountAfterAdd, "Cannot Execute Add Item Comand");
        }

        /// <summary>
        /// Removes the preference set command.
        /// </summary>
        [TestMethod]
        public void DeletePreferenceSet_ExecuteCommand_DeleteSuccess()
        {

            int PreferenceSetCountAfterAdd = this.TheVM.PreferenceSetsSource.Count + 1;
            this.TheVM.CurrentPreferenceSet = null;

            this.TheVM.AddNewPreferenceSetCommand.Execute(null);

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(this.TheVM.PreferenceSetsSource.Count == PreferenceSetCountAfterAdd, "Cannot Execute Add Item Comand");
        }

        /// <summary>
        /// Publish a preference set form my sets to organization sets so a new one add to organization sets.
        /// </summary>
        [TestMethod]
        public void Publish_PreferenceSet_Form_MySets_To_Organization_New_One_Add_To_Organization()
        {
            #region → Arrange .

            int mySetsCount = this.TheVM
                                  .MainPreferenceSets
                                  .Where(s => s.MainPreferenceSetID == PrefAppConstant.MainPreferenceSets.MySets)
                                  .First()
                                  .PreferenceSets
                                  .Count();

            int OrganizationSetsCount = this.TheVM
                                            .MainPreferenceSets
                                            .Where(s => s.MainPreferenceSetID == PrefAppConstant.MainPreferenceSets.OrganizationSets)
                                            .First()
                                            .PreferenceSets
                                            .Count();
            int SetStoreCount = this.TheVM
                                    .MainPreferenceSets
                                    .Where(s => s.MainPreferenceSetID == PrefAppConstant.MainPreferenceSets.SetStore)
                                    .First()
                                    .PreferenceSets
                                    .Count();


            this.TheVM.CurrentPreferenceSet = this.TheVM
                                                  .MainPreferenceSets
                                                  .Where(s => s.MainPreferenceSetID == PrefAppConstant.MainPreferenceSets.MySets)
                                                  .First()
                                                  .PreferenceSets
                                                  .First();


            string assertMessage = "Failed to Publish a PreferenceSet Form MySets To Organization sets";

            #endregion

            #region → Act     .

            this.TheVM.PublishPreferenceSetToOrganizationCommand.Execute("START");

            this.TheVM.PublishPreferenceSetToOrganizationCommand.Execute("OK");

            #endregion

            #region → Assert  .

            #region → Getting Actual Counts .


            int actaulMySetsCount = this.TheVM
                                        .MainPreferenceSets
                                        .Where(s => s.MainPreferenceSetID == PrefAppConstant.MainPreferenceSets.MySets)
                                        .First()
                                        .PreferenceSets
                                        .Count();

            int actaulOrganizationSetsCount = this.TheVM
                                                  .MainPreferenceSets
                                                  .Where(s => s.MainPreferenceSetID == PrefAppConstant.MainPreferenceSets.OrganizationSets)
                                                  .First()
                                                  .PreferenceSets
                                                  .Count();
            int actaulSetStoreCount = this.TheVM
                                          .MainPreferenceSets
                                          .Where(s => s.MainPreferenceSetID == PrefAppConstant.MainPreferenceSets.SetStore)
                                          .First()
                                          .PreferenceSets
                                          .Count();

            #endregion

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));

            Assert.IsTrue(mySetsCount == actaulMySetsCount, assertMessage);
            Assert.IsTrue((OrganizationSetsCount + 1) == actaulOrganizationSetsCount, assertMessage);
            Assert.IsTrue(SetStoreCount == actaulSetStoreCount, assertMessage); ;

            #endregion

        }

        /// <summary>
        /// Publish a preference set form my sets to set store so a new one add to set store.
        /// </summary>
        [TestMethod]
        public void Publish_PreferenceSet_Form_MySets_To_Set_Store_New_One_Add_To_Set_store()
        {
            #region → Arrange .

            int mySetsCount = this.TheVM
                                  .MainPreferenceSets
                                  .Where(s => s.MainPreferenceSetID == PrefAppConstant.MainPreferenceSets.MySets)
                                  .First()
                                  .PreferenceSets
                                  .Count();

            int OrganizationSetsCount = this.TheVM
                                            .MainPreferenceSets
                                            .Where(s => s.MainPreferenceSetID == PrefAppConstant.MainPreferenceSets.OrganizationSets)
                                            .First()
                                            .PreferenceSets
                                            .Count();
            int SetStoreCount = this.TheVM
                                    .MainPreferenceSets
                                    .Where(s => s.MainPreferenceSetID == PrefAppConstant.MainPreferenceSets.SetStore)
                                    .First()
                                    .PreferenceSets
                                    .Count();


            this.TheVM.CurrentPreferenceSet = this.TheVM
                                                  .MainPreferenceSets
                                                  .Where(s => s.MainPreferenceSetID == PrefAppConstant.MainPreferenceSets.MySets)
                                                  .First()
                                                  .PreferenceSets
                                                  .First();


            string assertMessage = "Failed to Publish a PreferenceSet Form MySets To Set Store";

            #endregion

            #region → Act     .

            this.TheVM.PublishPreferenceSetToSetStoreCommand.Execute("START");

            this.TheVM.PublishPreferenceSetToSetStoreCommand.Execute("OK");

            #endregion

            #region → Assert  .

            #region → Getting Actual Counts .


            int actaulMySetsCount = this.TheVM
                                        .MainPreferenceSets
                                        .Where(s => s.MainPreferenceSetID == PrefAppConstant.MainPreferenceSets.MySets)
                                        .First()
                                        .PreferenceSets
                                        .Count();

            int actaulOrganizationSetsCount = this.TheVM
                                                  .MainPreferenceSets
                                                  .Where(s => s.MainPreferenceSetID == PrefAppConstant.MainPreferenceSets.OrganizationSets)
                                                  .First()
                                                  .PreferenceSets
                                                  .Count();
            int actaulSetStoreCount = this.TheVM
                                          .MainPreferenceSets
                                          .Where(s => s.MainPreferenceSetID == PrefAppConstant.MainPreferenceSets.SetStore)
                                          .First()
                                          .PreferenceSets
                                          .Count();

            #endregion

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));

            Assert.IsTrue(mySetsCount == actaulMySetsCount, assertMessage);
            Assert.IsTrue(OrganizationSetsCount == actaulOrganizationSetsCount, assertMessage);
            Assert.IsTrue((SetStoreCount + 1) == actaulSetStoreCount, assertMessage); ;

            #endregion

        }

        /// <summary>
        /// Publish Preference Set Form Set Store To MySets so a New One Add To My Sets
        /// </summary>
        [TestMethod]
        public void Publish_PreferenceSet_Form_SetStore_To_MySets_New_One_Add_To_My_Sets()
        {
            #region → Arrange .

            int mySetsCount = this.TheVM
                                  .MainPreferenceSets
                                  .Where(s => s.MainPreferenceSetID == PrefAppConstant.MainPreferenceSets.MySets)
                                  .First()
                                  .PreferenceSets
                                  .Count();

            int OrganizationSetsCount = this.TheVM
                                            .MainPreferenceSets
                                            .Where(s => s.MainPreferenceSetID == PrefAppConstant.MainPreferenceSets.OrganizationSets)
                                            .First()
                                            .PreferenceSets
                                            .Count();
            int SetStoreCount = this.TheVM
                                    .MainPreferenceSets
                                    .Where(s => s.MainPreferenceSetID == PrefAppConstant.MainPreferenceSets.SetStore)
                                    .First()
                                    .PreferenceSets
                                    .Count();


            this.TheVM.CurrentPreferenceSet = this.TheVM
                                                  .MainPreferenceSets
                                                  .Where(s => s.MainPreferenceSetID == PrefAppConstant.MainPreferenceSets.SetStore)
                                                  .First()
                                                  .PreferenceSets
                                                  .First();


            string assertMessage = "Failed to Publish a PreferenceSet Form Set Store To MySets";

            #endregion

            #region → Act     .

            this.TheVM.PublishPreferenceSetToMySetsCommand.Execute("START");

            this.TheVM.PublishPreferenceSetToMySetsCommand.Execute("OK");

            #endregion

            #region → Assert  .

            #region → Getting Actual Counts .


            int actaulMySetsCount = this.TheVM
                                        .MainPreferenceSets
                                        .Where(s => s.MainPreferenceSetID == PrefAppConstant.MainPreferenceSets.MySets)
                                        .First()
                                        .PreferenceSets
                                        .Count();

            int actaulOrganizationSetsCount = this.TheVM
                                                  .MainPreferenceSets
                                                  .Where(s => s.MainPreferenceSetID == PrefAppConstant.MainPreferenceSets.OrganizationSets)
                                                  .First()
                                                  .PreferenceSets
                                                  .Count();

            int actaulSetStoreCount = this.TheVM
                                          .MainPreferenceSets
                                          .Where(s => s.MainPreferenceSetID == PrefAppConstant.MainPreferenceSets.SetStore)
                                          .First()
                                          .PreferenceSets
                                          .Count();

            #endregion

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));

            Assert.IsTrue((mySetsCount + 1) == actaulMySetsCount, assertMessage);
            Assert.IsTrue(OrganizationSetsCount == actaulOrganizationSetsCount, assertMessage);
            Assert.IsTrue(SetStoreCount == actaulSetStoreCount, assertMessage); ;

            #endregion

        }


        /// <summary>
        /// Publishs the preference set From my sets to Organization while the user has not organization so raise messages.
        /// </summary>
        [TestMethod]
        public void Publish_PreferenceSet_From_MySets_To_organization_User_No_Organization_Raise_Messages()
        {
            #region → Arrange .

            this.TheVM.OrganizationSource = new List<Organization>();

            int mySetsCount = this.TheVM
                                  .MainPreferenceSets
                                  .Where(s => s.MainPreferenceSetID == PrefAppConstant.MainPreferenceSets.MySets)
                                  .First()
                                  .PreferenceSets
                                  .Count();

            int OrganizationSetsCount = this.TheVM
                                            .MainPreferenceSets
                                            .Where(s => s.MainPreferenceSetID == PrefAppConstant.MainPreferenceSets.OrganizationSets)
                                            .First()
                                            .PreferenceSets
                                            .Count();
            int SetStoreCount = this.TheVM
                                    .MainPreferenceSets
                                    .Where(s => s.MainPreferenceSetID == PrefAppConstant.MainPreferenceSets.SetStore)
                                    .First()
                                    .PreferenceSets
                                    .Count();


            this.TheVM.CurrentPreferenceSet = this.TheVM
                                                  .MainPreferenceSets
                                                  .Where(s => s.MainPreferenceSetID == PrefAppConstant.MainPreferenceSets.MySets)
                                                  .First()
                                                  .PreferenceSets
                                                  .First();


            string assertMessage = "Failed to Publish a PreferenceSet Form My Sets To Organization sets";

            #endregion

            #region → Act     .

            this.TheVM.PublishPreferenceSetToOrganizationCommand.Execute("START");

            //this.TheVM.PublishPreferenceSetToOrganizationCommand.Execute("OK");

            #endregion

            #region → Assert  .

            #region → Getting Actual Counts .

            

            int actaulMySetsCount = this.TheVM
                                        .MainPreferenceSets
                                        .Where(s => s.MainPreferenceSetID == PrefAppConstant.MainPreferenceSets.MySets)
                                        .First()
                                        .PreferenceSets
                                        .Count();

            int actaulOrganizationSetsCount = this.TheVM
                                                  .MainPreferenceSets
                                                  .Where(s => s.MainPreferenceSetID == PrefAppConstant.MainPreferenceSets.OrganizationSets)
                                                  .First()
                                                  .PreferenceSets
                                                  .Count();

            int actaulSetStoreCount = this.TheVM
                                          .MainPreferenceSets
                                          .Where(s => s.MainPreferenceSetID == PrefAppConstant.MainPreferenceSets.SetStore)
                                          .First()
                                          .PreferenceSets
                                          .Count();

            #endregion

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));

            Assert.IsTrue(mySetsCount  == actaulMySetsCount, assertMessage);
            Assert.IsTrue(OrganizationSetsCount == actaulOrganizationSetsCount, assertMessage);
            Assert.IsTrue(SetStoreCount == actaulSetStoreCount, assertMessage); ;

            Assert.IsTrue(!string.IsNullOrEmpty(this.LastPopMessage)&& this.LastPopMessage==PrefApp.ViewModel.Resources.UserHaveNotOrganization , assertMessage); ;



            #endregion

        }

        /// <summary>
        /// Adds the new issue command.
        /// </summary>
        [TestMethod]
        public void AddNewIssue_ExecuteCommand_AddScuccess()
        {
            //Set The Current Preference set Like Choose Node From Tree.
            this.TheVM.CurrentPreferenceSet = this.TheVM.PreferenceSetsSource[0];

            //Setting The Expected Issues count After add.
            int IssuesCountAfterAdd = this.TheVM.IssuesSource.Count + 1;

            //Execute The Command and wait for Results
            this.TheVM.AddNewIssueCommand.Execute(null);

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(this.TheVM.IssuesSource.Count == IssuesCountAfterAdd, "Cannot Execute Add New Issue Command");
        }

        /// <summary>
        /// Adds the new issue command.
        /// </summary>
        [TestMethod]
        public void DeleteIssue_ExecuteCommand_DeleteScuccess()
        {
            //Set The Current Preference set Like Choose Node From Tree.
            this.TheVM.CurrentPreferenceSet = this.TheVM.PreferenceSetsSource[0];
            this.TheVM.CurrentIssue = this.TheVM.CurrentPreferenceSet.Issues.FirstOrDefault();

            this.TheVM.CurrentIssue.IsSelected = true;

            //Setting The Expected Issues count After add.
            int IssuesCountAfterDelete = this.TheVM.IssuesSource.Count - 1;

            //Execute The Command and wait for Results
            this.TheVM.DeleteIssueCommand.Execute(null);

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(this.TheVM.IssuesSource.Count == IssuesCountAfterDelete, "Cannot Execute Delete an Issue Command");
        }

        /// <summary>
        /// Adds the new option command.
        /// </summary>
        [TestMethod]
        public void AddNewOption_ExecuteCommand_AddScuccess()
        {
            //Set The Current Preference set Like Choose Node From Tree.
            this.TheVM.CurrentPreferenceSet = this.TheVM.PreferenceSetsSource[0];
            this.TheVM.CurrentIssue = this.TheVM.CurrentPreferenceSet.Issues.FirstOrDefault(s => s.IssueTypeID == PrefAppConstant.IssueTypes.Options);

            //Setting The Expected Options count After add.
            int OptionsCountAfterAdd = this.TheVM.OptionsViewModel.OptionIssueSource.Count + 1;

            //Execute The Command and wait for Results
            this.TheVM.OptionsViewModel.AddNewOptionCommand.Execute(null);
            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(this.TheVM.OptionsViewModel.OptionIssueSource.Count == OptionsCountAfterAdd, "Cannot Execute Add New Option Command");
        }

        /// <summary>
        /// Deletes the option command.
        /// </summary>
        [TestMethod]
        public void DeleteOption_ExecuteCommand_DeleteSuccess()
        {
            //Set The Current Preference set Like Choose Node From Tree.
            this.TheVM.CurrentPreferenceSet = this.TheVM.PreferenceSetsSource[0];

            //Setting the Current Issue
            this.TheVM.CurrentIssue = this.TheVM.CurrentPreferenceSet.Issues.Where(s => s.IssueTypeID == PrefAppConstant.IssueTypes.Options && s.OptionIssues.Count() > 0).LastOrDefault();

            //Setting Like Checked in the Grid.
            this.TheVM.CurrentIssue.OptionIssues.FirstOrDefault().IsSelected = true;

            //Setting The Expected Options count After add.
            int OptionsCountAfterDelete = this.TheVM.OptionsViewModel.OptionIssueSource.Count - 1;

            //Execute The Command and wait for Results
            this.TheVM.OptionsViewModel.DeleteOptionCommand.Execute(null);

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(this.TheVM.OptionsViewModel.OptionIssueSource.Count == OptionsCountAfterDelete, "Cannot Execute Delete Option Command");
        }

        /// <summary>
        /// Change issue type command.
        /// </summary>
        [TestMethod]
        public void ChangeIssueType_ExecuteCommand_TypeChanged()
        {
            //Set The Current Preference set Like Choose Node From Tree.
            this.TheVM.CurrentPreferenceSet = this.TheVM.PreferenceSetsSource[1];

            //Setting the Current Issue
            this.TheVM.CurrentIssue = this.TheVM.CurrentPreferenceSet.Issues.FirstOrDefault(s => s.IssueTypeID == PrefAppConstant.IssueTypes.Options);

            //Setting The Expected Options count After add.
            int OptionsCountAfterDelete = 0;

            this.TheVM.CurrentIssue.IssueTypeID = PrefAppConstant.IssueTypes.NotRated;

            //Execute The Command and wait for Results
            this.TheVM.ChangeIssueTypeCommand.Execute(null);
            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(this.TheVM.CurrentIssue.OptionIssues.Count == OptionsCountAfterDelete, "Cannot Execute Change Issue Type Command.");
        }

        /// <summary>
        /// Expands the issue item_ pass numeric issue_ expanded.
        /// </summary>
        [TestMethod]
        public void ExpandIssueItem_PassNumericIssue_Expanded()
        {
            //Set The Current Preference set Like Choose Node From Tree.
            this.TheVM.CurrentPreferenceSet = this.TheVM.PreferenceSetsSource[0];

            //Setting the Current Issue as Numeric Issue
            this.TheVM.CurrentIssue = this.TheVM.CurrentPreferenceSet.Issues.Where(s => s.IssueTypeID == PrefAppConstant.IssueTypes.Numeric).FirstOrDefault();

            PrefAppAccordionEventArgs args = new PrefAppAccordionEventArgs(TheVM.CurrentIssue, null);

            this.LastFlipedMessage = string.Empty;
            TheVM.ExpandIssueItem.Execute(args);

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(LastFlipedMessage == PrefAppViewTypes.NumericIssue, "Cannot Execute expand issue item Command for numeric issue.");
        }

        /// <summary>
        /// Expands the issue item_ pass option issue_ expanded.
        /// </summary>
        [TestMethod]
        public void ExpandIssueItem_PassOptionIssue_Expanded()
        {
            //Set The Current Preference set Like Choose Node From Tree.
            this.TheVM.CurrentPreferenceSet = this.TheVM.PreferenceSetsSource[0];

            //Setting the Current Issue as option Issue
            this.TheVM.CurrentIssue = this.TheVM.CurrentPreferenceSet.Issues.Where(s => s.IssueTypeID == PrefAppConstant.IssueTypes.Options).FirstOrDefault();

            PrefAppAccordionEventArgs args = new PrefAppAccordionEventArgs(TheVM.CurrentIssue, null);

            this.LastFlipedMessage = string.Empty;
            TheVM.ExpandIssueItem.Execute(args);

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(LastFlipedMessage == PrefAppViewTypes.OptionIssue, "Cannot Execute expand issue item Command for option issue.");
        }

        /// <summary>
        /// Navigates to issues_ execute command_ success.
        /// </summary>
        [TestMethod]
        public void NavigateToIssues_ExecuteCommand_Success()
        {
            this.LastFlipedMessage = string.Empty;
            TheVM.NavigateToIssues.Execute(null);

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(LastFlipedMessage == PrefAppViewTypes.IssuesView, "Cannot Navigate to issues view.");
        }

        /// <summary>
        /// Edits the sent message command_ no response_ success.
        /// </summary>
        [TestMethod]
        public void EditSentMessageCommand_NoResponse_Success()
        {
            this.LastFlipedMessage = string.Empty;
            TheVM.EditSentMessageCommand.Execute(MessageBoxResult.No.ToString());

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(LastFlipedMessage == PrefAppViewTypes.ClosePopupView, "Cannot execute \"no\" response of user to not send mail to negotiators.");
        }

        /// <summary>
        /// Edits the sent message command_ yes response_ success.
        /// </summary>
        [TestMethod]
        public void EditSentMessageCommand_YesResponse_Success()
        {
            this.LastFlipedMessage = string.Empty;
            this.LastPopMessage = string.Empty;
            TheVM.EditSentMessageCommand.Execute(MessageBoxResult.Yes.ToString());

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(LastFlipedMessage == PrefAppViewTypes.ClosePopupView &&
                PrefAppMessanger.PopUpType.MailEditor == PrefAppMessanger.PopUpType.MailEditor, "Cannot execute \"yes\" response of user to send mail to negotiators.");
        }

        /// <summary>
        /// Closes the add new issue pop up window command_ add issue entity_ success.
        /// </summary>
        [TestMethod]
        public void CloseAddNewIssuePopUpWindowCommand_AddIssueEntity_Success()
        {
            this.LastFlipedMessage = string.Empty;
            //Set The Current Preference set Like Choose Node From Tree.
            this.TheVM.CurrentPreferenceSet = this.TheVM.PreferenceSetsSource[0];

            this.TheVM.AddNewIssueCommand.Execute(null);
            TheVM.CloseAddNewIssuePopUpWindowCommand.Execute(TheVM.CurrentIssue);

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(PrefAppConfigurations.PendingIssueType == PrefAppConfigurations.IssueTypes.Issue &&
                LastFlipedMessage == PrefAppViewTypes.ClosePopupView, "Cannot add new undefined issue");
        }

        /// <summary>
        /// Closes the add new issue pop up window command_ add option entity_ success.
        /// </summary>
        [TestMethod]
        public void CloseAddNewIssuePopUpWindowCommand_AddOptionEntity_Success()
        {
            this.LastFlipedMessage = string.Empty;
            //Set The Current Preference set Like Choose Node From Tree.
            this.TheVM.CurrentPreferenceSet = this.TheVM.PreferenceSetsSource[0];
            this.TheVM.CurrentIssue = this.TheVM.CurrentPreferenceSet.Issues.FirstOrDefault(s => s.IssueTypeID == PrefAppConstant.IssueTypes.Options);

            this.TheVM.OptionsViewModel.AddNewOptionCommand.Execute(null);
            TheVM.CloseAddNewIssuePopUpWindowCommand.Execute(TheVM.CurrentOption);

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(PrefAppConfigurations.PendingIssueType == PrefAppConfigurations.IssueTypes.Option &&
                LastFlipedMessage == PrefAppViewTypes.ClosePopupView, "Cannot add new undefined option issue");
        }

        /// <summary>
        /// Closes the add new issue pop up window command_ add later rated entity_ success.
        /// </summary>
        [TestMethod]
        public void CloseAddNewIssuePopUpWindowCommand_AddLaterRatedEntity_Success()
        {
            this.LastFlipedMessage = string.Empty;
            //Set The Current Preference set Like Choose Node From Tree.
            this.TheVM.CurrentPreferenceSet = this.TheVM.PreferenceSetsSource[MockMaster.GeneralIndex.Door];
            this.TheVM.CurrentIssue = this.TheVM.CurrentPreferenceSet.Issues.FirstOrDefault(s => s.IssueTypeID == PrefAppConstant.IssueTypes.LaterRated);

            this.TheVM.AddLaterRatedIssue(true, TheVM.CurrentIssue);
            TheVM.CloseAddNewIssuePopUpWindowCommand.Execute(TheVM.CurrentLaterRated);

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(PrefAppConfigurations.PendingIssueType == PrefAppConfigurations.IssueTypes.LaterRated &&
                LastFlipedMessage == PrefAppViewTypes.ClosePopupView, "Cannot add new undefined later rated issue");
        }

        /// <summary>
        /// Closes the add new issue pop up window command_ add other entity_ fail.
        /// </summary>
        [TestMethod]
        public void CloseAddNewIssuePopUpWindowCommand_AddOtherEntity_Fail()
        {
            this.LastFlipedMessage = string.Empty;
            //Set The Current Preference set Like Choose Node From Tree.
            this.TheVM.CurrentPreferenceSet = this.TheVM.PreferenceSetsSource[0];
            this.TheVM.CurrentIssue = this.TheVM.CurrentPreferenceSet.Issues.FirstOrDefault(s => s.IssueTypeID == PrefAppConstant.IssueTypes.Numeric);

            this.TheVM.AddNumericIssue(true, TheVM.CurrentIssue);
            TheVM.CloseAddNewIssuePopUpWindowCommand.Execute(TheVM.NumericViewModel.CurrentNumericIssue);

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(string.IsNullOrEmpty(LastFlipedMessage), "Add new undefined numeric issue by mistake");
        }

        /// <summary>
        /// Goes to numeric text mode command_ execute command_ success.
        /// </summary>
        [TestMethod]
        public void GoToNumericTextModeCommand_ExecuteCommand_Success()
        {
            this.LastFlipedMessage = string.Empty;
            TheVM.NumericViewModel.GoToNumericTextModeCommand.Execute(null);

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(LastFlipedMessage == PrefAppViewTypes.NumericTextModeView, "Cannot Navigate to numeric text mode view.");
        }

        /// <summary>
        /// Goes to numeric graphic mode command_ execute command_ success.
        /// </summary>
        [TestMethod]
        public void GoToNumericGraphicModeCommand_ExecuteCommand_Success()
        {
            this.LastFlipedMessage = string.Empty;
            TheVM.NumericViewModel.GoToNumericGraphicModeCommand.Execute(null);

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(LastFlipedMessage == PrefAppViewTypes.NumericGraphModeView, "Cannot Navigate to numeric graph mode view.");
        }

        [TestMethod]
        public void PlotChartCommand_ExecuteCommand_Success()
        {
            this.LastFlipedMessage = string.Empty;
            //Set The Current Preference set Like Choose Node From Tree.
            this.TheVM.CurrentPreferenceSet = this.TheVM.PreferenceSetsSource[0];
            this.TheVM.CurrentIssue = this.TheVM.CurrentPreferenceSet.Issues.FirstOrDefault(s => s.IssueTypeID == PrefAppConstant.IssueTypes.Numeric);

            TheVM.NumericViewModel.PlotChartCommand.Execute(null);

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(TheVM.NumericViewModel.IsPlotChartCommandDone, "Cannot Plot points on graph.");
        }
        #endregion

        #region → Methods        .

        #region → Private        .

        #region → Raise Error Message  .

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
                {
                    ErrorMessage = ex.Message;
                }
            }
        }

        #endregion

        #region → Confirm Message      .

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

        #region → Flip Message         .

        /// <summary>
        /// Called when [flipp message].
        /// </summary>
        /// <param name="pageName">Name of the page.</param>
        private void OnFlippMessage(string pageName)
        {
            this.LastFlipedMessage = pageName;
        }

        #endregion

        #region → PopUp Message        .
        /// <summary>
        /// Called when [new popup message].
        /// </summary>
        /// <param name="PopType">Type of the pop.</param>
        private void OnNewPopupMessage(string PopType)
        {
            this.LastPopMessage = PopType;
        }
        #endregion

        #region → Custom Message        .

        /// <summary>
        /// Called when [custom message new popup message].
        /// </summary>
        /// <param name="message">The message.</param>
        private void OnCustomMessage(eNegMessage message)
        {
            this.LastPopMessage = message.Message;
        }
        #endregion

        #endregion

        #region → Public         .

        /// <summary>
        /// Gets the main preference set async.
        /// </summary>
        [TestMethod]
        public void GetMainPreferenceSet_NoCondition_ReturnCollection()
        {
            this.TheVM.GetMainPreferenceSetAsync();
            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(this.TheVM.MainPreferenceSets.Count() > 0, "No Main Preference Sets Found");
        }


        /// <summary>
        /// Gets the organizations for user without a condition so return collection.
        /// </summary>
        [TestMethod]
        public void GetOrganizationsForUser_NoCondition_ReturnCollection()
        {
            this.TheVM.GetOrganizationsForUserAsync();
            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(this.TheVM.OrganizationSource.Count() > 0, "No Organization Source Found");
        }

        /// <summary>
        /// Gets the issue statisticals without a condition so return collection.
        /// </summary>
        [TestMethod]
        public void GetIssueStatisticals_NoCondition_ReturnCollection()
        {
            this.TheVM.GetIssueStatisticalsAsync();
            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(this.TheVM.IssueStatisticals.Count() > 0, "No Issue Statisticals Source Found");
        }

        /// <summary>
        /// Gets the preference set without condition so return collection.
        /// </summary>
        [TestMethod]
        public void GetPreferenceSet_NoCondition_ReturnCollection()
        {
            this.TheVM.GetPreferenceSetAsync();
            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(this.TheVM.PreferenceSets.Count() > 0, "No Preference Sets Found");
        }

        /// <summary>
        /// Gets the issue types async.
        /// </summary>
        [TestMethod]
        public void GetIssueTypes_NoCondition_ReturnCollection()
        {
            this.TheVM.GetIssueTypesAsync();
            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(this.TheVM.IssueTypes.Count() > 0, "No IssueTypes Found");
        }

        /// <summary>
        /// Gets the issues async.
        /// </summary>
        [TestMethod]
        public void GetIssues_NoCondition_ReturnCollection()
        {
            this.TheVM.GetIssuesAsync();
            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(this.TheVM.Issues.Count() > 0, "No Issues Found");
        }

        /// <summary>
        /// Gets the numeric issues async.
        /// </summary>
        [TestMethod]
        public void GetNumericIssues_NoCondition_ReturnCollection()
        {
            this.TheVM.GetNumericIssuesAsync();
            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(this.TheVM.NumericIssues.Count() > 0, "No Numeric Issues Found");
        }

        /// <summary>
        /// Gets the option issues async.
        /// </summary>
        [TestMethod]
        public void GetOptionIssues_NoCondition_ReturnCollection()
        {
            this.TheVM.GetOptionIssuesAsync();
            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(this.TheVM.OptionIssues.Count() > 0, "No Option Issues Found");
        }

        /// <summary>
        /// Gets the later rated issues async.
        /// </summary>
        [TestMethod]
        public void GetLaterRatedIssues_NoCondition_ReturnCollection()
        {
            this.TheVM.GetLaterRatedIssuesAsync();
            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(this.TheVM.LaterRatedIssues.Count() > 0, "No Later Rated Issues Found");
        }

        /// <summary>
        /// Used To Clean All Resources
        /// </summary>
        [TestCleanup]
        public void CleanUp()
        {
            // call Cleanup on its ViewModel
            this.TheVM.Cleanup();

            // Cleanup itself
            Messenger.Default.Unregister(this);

            //           this.PreferenceSetsViewModel.mPrefSetsModel.RejectChanges();
            //this.PreferenceSetsViewModel.mPrefSetsModel = null;
            //           this.PreferenceSetsViewModel = null;



        }

        #endregion Public

        #endregion Methods
    }
}
