
#region → Usings   .
using System;
using System.Collections.Generic;
using System.ServiceModel.DomainServices.Client;
using citPOINT.PrefApp.Data.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endregion

#region → History  .

/* Date         User            Change
 * 
 * 22.09.10     M.Wahab     creation
 * 24.10.10    Yousra Reda  Test Operations related to LaterRatedIssue Table
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion

namespace citPOINT.PrefApp.Data.WebTest
{
    /// <summary>
    /// This is a test class for PrefAppService
    /// Insert Update Delete Functionalities
    /// For The following Tables
    /// 
    ///  MainPreferenceSet,
    ///  PreferenceSet,
    ///  Issue ,
    ///  OptionIssue,
    ///  NumericIssue,
    ///  IssueType,
    ///  PreferenceSetNeg,
    ///  NegConversation,
    ///  ConversationMessage
    ///</summary>
    [TestClass()]
    public class PrefAppServiceTest
    {
        #region → Fields         .

        PrefAppContext mContext;

        List<MainPreferenceSet> mMainPreferenceSets;
        List<PreferenceSet> mPreferenceSets;
        List<Issue> mIssues;
        List<NumericIssue> mNumericIssues;
        List<OptionIssue> mOptionIssues;
        List<IssueType> mIssueTypes;
        List<LaterRatedIssue> mLaterRatedIssues;

        List<ConversationMessage> mConversationMessages;
        List<NegConversation> mNegConversations;
        List<PreferenceSetNeg> mPreferenceSetNegs;

        private int CountOfAllEntries = 0;
        
        private const int IssueType_Numeric = 0;
        private const int IssueType_Options = 1;
        private const int IssueType_LaterRated = 2;
        private const int IssueType_NotRated = 3;

        /// <summary>
        /// Issue Index used in entering data
        /// </summary>
        private struct IssueIndex
        {
            public static int Car_Price = 0;
            public static int Car_Color = 1;
            public static int Car_Power = 2;
            public static int Car_Model = 3;

            public static int Apartment_Size = 4;
            public static int Apartment_RoomsCount = 5;
            public static int Apartment_Floor = 6;
            
            public static int Computer_Price = 8;
            public static int Computer_Type = 9;
            public static int Computer_Monitor_Size = 10;

            public static int Door_Price = 11;
            public static int Door_Door_Type = 12;
        }

        private TestContext testContextInstance;

        #endregion Fields

        #region → Properties     .
        
        /// <summary>
        /// Gets the main preference sets count.
        /// </summary>
        /// <value>The main preference sets count.</value>
        public int MainPreferenceSetsCount
        {
            get
            {
                return this.MainPreferenceSets.Count;
            }
        }

        /// <summary>
        /// Gets the preference set count.
        /// </summary>
        /// <value>The preference set count.</value>
        public int PreferenceSetCount
        {
            get
            {
                return this.PreferenceSets.Count;
            }
        }

        /// <summary>
        /// Gets the issues count.
        /// </summary>
        /// <value>The issues count.</value>
        public int IssuesCount
        {
            get
            {
                return this.Issues.Count;
            }
        }

        /// <summary>
        /// Gets the numeric issues count.
        /// </summary>
        /// <value>The numeric issues count.</value>
        public int NumericIssuesCount
        {
            get
            {
                return this.NumericIssues.Count;
            }
        }

        /// <summary>
        /// Gets the option issues count.
        /// </summary>
        /// <value>The option issues count.</value>
        public int OptionIssuesCount
        {
            get
            {
                return this.OptionIssues.Count;
            }
        }

        /// <summary>
        /// Gets the later rated issues count.
        /// </summary>
        /// <value>The later rated issues count.</value>
        public int LaterRatedIssuesCount
        {
            get
            {
                return LaterRatedIssues.Count;
            }
        }

        /// <summary>
        /// Gets the neg conversations count.
        /// </summary>
        /// <value>The neg conversations count.</value>
        public int NegConversationsCount
        {
            get
            {
                return this.NegConversations.Count;
            }
        }

        /// <summary>
        /// Gets the preference set negs count.
        /// </summary>
        /// <value>The preference set negs count.</value>
        public int PreferenceSetNegsCount
        {
            get
            {
                return this.PreferenceSetNegs.Count;
            }
        }

        /// <summary>
        /// Gets the conversation messages count.
        /// </summary>
        /// <value>The conversation messages count.</value>
        public int ConversationMessagesCount
        {
            get
            {
                return this.ConversationMessages.Count;
            }
        }

        /// <summary>
        /// Gets the issue types count.
        /// </summary>
        /// <value>The issue types count.</value>
        public int IssueTypesCount
        {
            get
            {
                return this.IssueTypes.Count;
            }
        }

        /// <summary>
        /// instance of PrefAppContext of PrefApp to can use available services
        /// </summary>
        private PrefAppContext Context
        {
            get
            {
                if (mContext == null)
                {
                    mContext = new PrefAppContext(new Uri("http://localhost:9002/citPOINT-prefApp-Data-Web-PrefAppService.svc", UriKind.Absolute));
                }
                return mContext;
            }

        }

        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        /// </summary>
        /// <value>The test context.</value>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region  Generate All Objects Mock

        #region User
        /// <summary>
        /// Gets the current user ID.
        /// </summary>
        /// <value>The current user ID.</value>
        public Guid CurrentUserID
        {
            get
            {
                return new Guid("0a48aa0d-28d8-49bc-a486-bf3ce8cd85ec");
            }
        }
        #endregion

        #region Main Preference Set
        
        /// <summary>
        /// Get a List Of Main Preference Set
        /// </summary>
        /// <value>The main preference sets.</value>
        public List<MainPreferenceSet> MainPreferenceSets
        {
            get
            {
                if (mMainPreferenceSets == null)
                {
                    mMainPreferenceSets = new List<MainPreferenceSet>()
                    {
                        new MainPreferenceSet ()
                           { 
                                MainPreferenceSetID=Guid.NewGuid(),
                                MainPreferenceSetName="My Sets"
                           },

                        new MainPreferenceSet ()
                           { 
                                MainPreferenceSetID=Guid.NewGuid(),
                                MainPreferenceSetName="Organization Sets"
                           },

                        new MainPreferenceSet ()
                           { 
                                MainPreferenceSetID=Guid.NewGuid(),
                                MainPreferenceSetName="Store Sets"
                           },

                    };
                }
                return mMainPreferenceSets;

            }
        }

        #endregion Main Preference Set

        #region Preference Set
        
        /// <summary>
        /// Get a List Of Preference Set
        /// </summary>
        /// <value>The preference sets.</value>
        public List<PreferenceSet> PreferenceSets
        {
            get
            {
                if (mPreferenceSets == null)
                {
                    mPreferenceSets = new List<PreferenceSet>()
                    {
                       new PreferenceSet ()
                           { 
                                PreferenceSetID=Guid.NewGuid(),
                                PreferenceSetName="Purchase a Car",
                                MainPreferenceSetID=this.MainPreferenceSets[0].MainPreferenceSetID,
                                MainPreferenceSet=this.MainPreferenceSets[0],
                                UserID=CurrentUserID,
                                //Nodes=new string[] { "Issue","Values"},
                                Deleted=false,
                                DeletedBy=CurrentUserID,
                                DeletedOn=DateTime.Now
                           },

                       new PreferenceSet ()
                           { 
                                PreferenceSetID=Guid.NewGuid(),
                                PreferenceSetName="Purchase apartment",
                                MainPreferenceSetID=this.MainPreferenceSets[0].MainPreferenceSetID,
                                MainPreferenceSet=this.MainPreferenceSets[0],
                                UserID=CurrentUserID,
                                //Nodes=new string[] { "Issue","Values"},
                                Deleted=false,
                                DeletedBy=CurrentUserID,
                                DeletedOn=DateTime.Now
                           },

                       new PreferenceSet ()
                           {  
                                PreferenceSetID=Guid.NewGuid(),
                                PreferenceSetName="Purchase Computer",
                                MainPreferenceSetID=this.MainPreferenceSets[0].MainPreferenceSetID,
                                MainPreferenceSet=this.MainPreferenceSets[0],
                                UserID=CurrentUserID,
                                //Nodes=new string[] { "Issue","Values"},
                                Deleted=false,
                                DeletedBy=CurrentUserID,
                                DeletedOn=DateTime.Now
                           },

                       new PreferenceSet ()
                           { 
                                PreferenceSetID=Guid.NewGuid(),
                                PreferenceSetName="Purchase Door",
                                MainPreferenceSetID=this.MainPreferenceSets[0].MainPreferenceSetID,
                                MainPreferenceSet=this.MainPreferenceSets[0],
                                UserID=CurrentUserID,
                                //Nodes=new string[] { "Issue","Values"},
                                Deleted=false,
                                DeletedBy=CurrentUserID,
                                DeletedOn=DateTime.Now
                           },

                    };
                }
                return mPreferenceSets;
            }
        }

        #endregion Preference Set

        #region Isssue Types

        /// <summary>
        /// Gets the issue types.
        /// </summary>
        /// <value>The issue types.</value>
        public List<IssueType> IssueTypes
        {
            get
            {
                if (mIssueTypes == null)
                {
                    mIssueTypes = new List<IssueType>()
                       {
                           new IssueType ()
                            { 
                                IssueTypeID=Guid.NewGuid(),
                                IssueTypeName="Numeric"
                            },

                            new IssueType ()
                            { 
                                IssueTypeID=Guid.NewGuid(),
                                IssueTypeName="Options"
                            },

                            new IssueType ()
                            { 
                                IssueTypeID=Guid.NewGuid(),
                                IssueTypeName="Later Rated"
                            }
                            ,

                            new IssueType ()
                            { 
                                IssueTypeID=Guid.NewGuid(),
                                IssueTypeName="Not Rated"
                            } 

                       };
                }
                return mIssueTypes;
            }
        }

        #endregion Isssue Types

        #region Issues

        /// <summary>
        /// Get a List Of Issue
        /// </summary>
        /// <value>The issues.</value>
        public List<Issue> Issues
        {
            get
            {
                if (mIssues == null)
                {
                    mIssues = new List<Issue>()
                    {

#region Purchase a Car

                       new Issue ()
                           { 
                                PreferenceSetID=this.PreferenceSets[0].PreferenceSetID /*Car*/,
                                PreferenceSet=this.PreferenceSets[0],
                                IssueID=Guid.NewGuid(),
                                IssueName="Price",
                                IssueType=this.IssueTypes[IssueType_Numeric],
                                IssueTypeID=this.IssueTypes[IssueType_Numeric].IssueTypeID,
                                IssueWeight=25,
                                
                                Deleted=false,
                                DeletedBy=CurrentUserID,
                                DeletedOn=DateTime.Now
                           },

                       new Issue ()
                           { 
                                PreferenceSetID=this.PreferenceSets[0].PreferenceSetID /*Car*/,
                                PreferenceSet=this.PreferenceSets[0],
                                IssueID=Guid.NewGuid(),
                                IssueName="Color",
                                IssueType=this.IssueTypes[IssueType_Options],
                                IssueTypeID=this.IssueTypes[IssueType_Options].IssueTypeID,
                                IssueWeight=30,
                                
                                Deleted=false,
                                DeletedBy=CurrentUserID,
                                DeletedOn=DateTime.Now
                           },

                       new Issue ()
                           { 
                                PreferenceSetID=this.PreferenceSets[0].PreferenceSetID /*Car*/,
                                PreferenceSet=this.PreferenceSets[0],
                                IssueID=Guid.NewGuid(),
                                IssueName="Power",
                                IssueType=this.IssueTypes[IssueType_Numeric],
                                IssueTypeID=this.IssueTypes[IssueType_Numeric].IssueTypeID,
                                IssueWeight=5,
                                
                                Deleted=false,
                                DeletedBy=CurrentUserID,
                                DeletedOn=DateTime.Now
                           },

                       new Issue ()
                           { 
                                PreferenceSetID=this.PreferenceSets[0].PreferenceSetID /*Car*/,
                                PreferenceSet=this.PreferenceSets[0],
                                IssueID=Guid.NewGuid(),
                                IssueName="Model",
                                IssueType=this.IssueTypes[IssueType_Options],
                                IssueTypeID=this.IssueTypes[IssueType_Options].IssueTypeID,
                                IssueWeight=40,
                                
                                Deleted=false,
                                DeletedBy=CurrentUserID,
                                DeletedOn=DateTime.Now
                           },

#endregion Purchase a Car

#region Purchase Apartment 
                       new Issue ()
                           { 
                                PreferenceSetID=this.PreferenceSets[1].PreferenceSetID /*Apartment*/,
                                PreferenceSet=this.PreferenceSets[1],
                                IssueID=Guid.NewGuid(),
                                IssueName="Size",
                                IssueType=this.IssueTypes[IssueType_Numeric],
                                IssueTypeID=this.IssueTypes[IssueType_Numeric].IssueTypeID,
                                IssueWeight=50,
                                
                                Deleted=false,
                                DeletedBy=CurrentUserID,
                                DeletedOn=DateTime.Now
                           },

                       new Issue ()
                           { 
                                PreferenceSetID=this.PreferenceSets[1].PreferenceSetID /*Apartment*/,
                                PreferenceSet=this.PreferenceSets[1],
                                IssueID=Guid.NewGuid(),
                                IssueName="Rooms Count",
                                IssueType=this.IssueTypes[IssueType_Numeric],
                                IssueTypeID=this.IssueTypes[IssueType_Numeric].IssueTypeID,
                                IssueWeight=20,
                                
                                Deleted=false,
                                DeletedBy=CurrentUserID,
                                DeletedOn=DateTime.Now
                           },

                           
                       new Issue ()
                           { 
                                PreferenceSetID=this.PreferenceSets[1].PreferenceSetID /*Apartment*/,
                                PreferenceSet=this.PreferenceSets[1],
                                IssueID=Guid.NewGuid(),
                                IssueName="Floor",
                                IssueType=this.IssueTypes[IssueType_Options],
                                IssueTypeID=this.IssueTypes[IssueType_Options].IssueTypeID,
                                IssueWeight=30,
                                
                                Deleted=false,
                                DeletedBy=CurrentUserID,
                                DeletedOn=DateTime.Now
                           },

                           
                       new Issue ()
                           { 
                                PreferenceSetID=this.PreferenceSets[1].PreferenceSetID /*Apartment*/,
                                PreferenceSet=this.PreferenceSets[1],
                                IssueID=Guid.NewGuid(),
                                IssueName="Address",
                                IssueType=this.IssueTypes[IssueType_NotRated],
                                IssueTypeID=this.IssueTypes[IssueType_NotRated].IssueTypeID,
                                IssueWeight=0,
                                
                                Deleted=false,
                                DeletedBy=CurrentUserID,
                                DeletedOn=DateTime.Now
                           },

#endregion Purchase Apartment 

#region Purchase a Computer

                       new Issue ()
                           { 
                                PreferenceSetID=this.PreferenceSets[2].PreferenceSetID /*Car*/,
                                PreferenceSet=this.PreferenceSets[2],
                                IssueID=Guid.NewGuid(),
                                IssueName="Price",
                                IssueType=this.IssueTypes[IssueType_Numeric],
                                IssueTypeID=this.IssueTypes[IssueType_Numeric].IssueTypeID,
                                IssueWeight=55,
                                
                                Deleted=false,
                                DeletedBy=CurrentUserID,
                                DeletedOn=DateTime.Now
                           },

                       new Issue ()
                           { 
                                PreferenceSetID=this.PreferenceSets[2].PreferenceSetID /*Car*/,
                                PreferenceSet=this.PreferenceSets[2],
                                IssueID=Guid.NewGuid(),
                                IssueName="Type",
                                IssueType=this.IssueTypes[IssueType_Options],
                                IssueTypeID=this.IssueTypes[IssueType_Options].IssueTypeID,
                                IssueWeight=35,
                                
                                Deleted=false,
                                DeletedBy=CurrentUserID,
                                DeletedOn=DateTime.Now
                           },

                       new Issue ()
                           { 
                                PreferenceSetID=this.PreferenceSets[0].PreferenceSetID /*Car*/,
                                PreferenceSet=this.PreferenceSets[0],
                                IssueID=Guid.NewGuid(),
                                IssueName="Monitor Size",
                                IssueType=this.IssueTypes[IssueType_Numeric],
                                IssueTypeID=this.IssueTypes[IssueType_Numeric].IssueTypeID,
                                IssueWeight=10,
                                
                                Deleted=false,
                                DeletedBy=CurrentUserID,
                                DeletedOn=DateTime.Now
                           },

#endregion Purchase a Computer

#region Purchase a Door

                       new Issue ()
                           { 
                                PreferenceSetID=this.PreferenceSets[3].PreferenceSetID /*Door*/,
                                PreferenceSet=this.PreferenceSets[3],
                                IssueID=Guid.NewGuid(),
                                IssueName="Price",
                                IssueType=this.IssueTypes[IssueType_Numeric],
                                IssueTypeID=this.IssueTypes[IssueType_Numeric].IssueTypeID,
                                IssueWeight=80,
                                
                                Deleted=false,
                                DeletedBy=CurrentUserID,
                                DeletedOn=DateTime.Now
                           } ,

                         new Issue ()
                           { 
                                PreferenceSetID=this.PreferenceSets[3].PreferenceSetID /*Door*/,
                                PreferenceSet=this.PreferenceSets[3],
                                IssueID=Guid.NewGuid(),
                                IssueName="Wood Type",
                                IssueType=this.IssueTypes[IssueType_LaterRated],
                                IssueTypeID=this.IssueTypes[IssueType_LaterRated].IssueTypeID,
                                IssueWeight=20,
                                
                                Deleted=false,
                                DeletedBy=CurrentUserID,
                                DeletedOn=DateTime.Now
                           } ,


#endregion Purchase a Door

                    };
                }
                return mIssues;
            }
        }

        #endregion Issues

        #region Numeric Issue
        /// <summary>
        /// Get List of Numeric Issues
        /// </summary>
        /// <value>The numeric issues.</value>
        public List<NumericIssue> NumericIssues
        {
            get
            {
                if (mNumericIssues == null)
                {
                    mNumericIssues = new List<NumericIssue>()
                     {
                            
                           #region Purchase a Car
                         //Price
                         new NumericIssue()
                           {
                               NumericIssueID=Guid.NewGuid(),
                               Issue=this.Issues[IssueIndex.Car_Price],
                               IssueID=this.Issues[IssueIndex.Car_Price].IssueID,
                               MaximumOperator=1,
                               MinimumOperator=1,
                               MinimumValue=10000,
                               MaximumValue=20000,
                               OptimumValueStart=12000,
                               OptimumValueEnd=15000,
                               Unit="$"
                           },

                         //Power
                         new NumericIssue()
                           {
                               NumericIssueID=Guid.NewGuid(),
                               Issue=this.Issues[IssueIndex.Car_Power],
                               IssueID=this.Issues[IssueIndex.Car_Power].IssueID,
                               MaximumOperator=1,
                               MinimumOperator=1,
                               MinimumValue=100,
                               MaximumValue=400,
                               OptimumValueStart=100,
                               OptimumValueEnd=400,
                               Unit="Hourse"
                           },
#endregion Purchase a Car

                           #region Purchase Apartment
                         //Size
                         new NumericIssue()
                           {
                               NumericIssueID=Guid.NewGuid(),
                               Issue=this.Issues[IssueIndex.Apartment_Size],
                               IssueID=this.Issues[IssueIndex.Apartment_Size].IssueID,
                               MaximumOperator=1,
                               MinimumOperator=1,
                               MinimumValue=80,
                               MaximumValue=180,
                               OptimumValueStart=150,
                               OptimumValueEnd=180,
                               Unit="Meter"
                           },

                         //Room Count
                         new NumericIssue()
                           {
                               NumericIssueID=Guid.NewGuid(),
                               Issue=this.Issues[IssueIndex.Apartment_RoomsCount],
                               IssueID=this.Issues[IssueIndex.Apartment_RoomsCount].IssueID,
                               MaximumOperator=1,
                               MinimumOperator=1,
                               MinimumValue=3,
                               MaximumValue=5,
                               OptimumValueStart=4,
                               OptimumValueEnd=4,
                               Unit="Room"
                           },
#endregion Purchase a Apartment

                           #region Purchase a Computer
                         //Price
                         new NumericIssue()
                           {
                               NumericIssueID=Guid.NewGuid(),
                               Issue=this.Issues[IssueIndex.Computer_Price],
                               IssueID=this.Issues[IssueIndex.Computer_Price].IssueID,
                               MaximumOperator=1,
                               MinimumOperator=1,
                               MinimumValue=3000,
                               MaximumValue=7000,
                               OptimumValueStart=3000,
                               OptimumValueEnd=7000,
                               Unit="$"
                           },

                         //Monitor
                         new NumericIssue()
                           {
                               NumericIssueID=Guid.NewGuid(),
                               Issue=this.Issues[IssueIndex.Computer_Monitor_Size],
                               IssueID=this.Issues[IssueIndex.Computer_Monitor_Size].IssueID,
                               MaximumOperator=1,
                               MinimumOperator=1,
                               MinimumValue=17,
                               MaximumValue=19,
                               OptimumValueStart=17,
                               OptimumValueEnd=19,
                               Unit="Inch"
                           },
#endregion Purchase a Computer

                           #region Purchase a Door
                         //Price
                         new NumericIssue()
                           {
                               NumericIssueID=Guid.NewGuid(),
                               Issue=this.Issues[IssueIndex.Door_Price],
                               IssueID=this.Issues[IssueIndex.Door_Price].IssueID,
                               MaximumOperator=1,
                               MinimumOperator=1,
                               MinimumValue=700,
                               MaximumValue=1200,
                               OptimumValueStart=800,
                               OptimumValueEnd=1000,
                               Unit="$"
                           }
 
#endregion Purchase a Door

                      };
                }

                return mNumericIssues;
            }

        }

        #endregion Numeric Issue

        #region Option Issues

        /// <summary>
        /// Get List of Numeric Issues
        /// </summary>
        /// <value>The option issues.</value>
        public List<OptionIssue> OptionIssues
        {
            get
            {
                if (mOptionIssues == null)
                {
                    mOptionIssues = new List<OptionIssue>()
                     {
                            
                      #region Purchase a Car
                         
                          #region Colors-Black-Silver-White
                        
                         //Color 100 %
                         new OptionIssue()
                           {
                               OptionIssueID=Guid.NewGuid(),
                               Issue=this.Issues[IssueIndex.Car_Color],
                               IssueID=this.Issues[IssueIndex.Car_Color].IssueID,
                               OptionIssueValue="Black",
                               OptionIssueWeight=100,
                               
                               Deleted=false,
                               DeletedBy=CurrentUserID,
                               DeletedOn=DateTime.Now
                                
                           },

                         //Color 40 %
                         new OptionIssue()
                           {
                               OptionIssueID=Guid.NewGuid(),
                               Issue=this.Issues[IssueIndex.Car_Color],
                               IssueID=this.Issues[IssueIndex.Car_Color].IssueID,
                               OptionIssueValue="Silver",
                               OptionIssueWeight=40,
                               
                               Deleted=false,
                               DeletedBy=CurrentUserID,
                               DeletedOn=DateTime.Now
                                
                           },
                         
                         //White 10 %
                         new OptionIssue()
                           {
                               OptionIssueID=Guid.NewGuid(),
                               Issue=this.Issues[IssueIndex.Car_Color],
                               IssueID=this.Issues[IssueIndex.Car_Color].IssueID,
                               OptionIssueValue="White",
                               OptionIssueWeight=10,
                               
                               Deleted=false,
                               DeletedBy=CurrentUserID,
                               DeletedOn=DateTime.Now
                                
                           },

                           #endregion Colors-Black-Silver-White

                          #region Model-BMW-Fiat-Mercedes

                        
                         //BMW 100 %
                         new OptionIssue()
                           {
                               OptionIssueID=Guid.NewGuid(),
                               Issue=this.Issues[IssueIndex.Car_Model],
                               IssueID=this.Issues[IssueIndex.Car_Model].IssueID,
                               OptionIssueValue="BMW",
                               OptionIssueWeight=100,
                               
                               Deleted=false,
                               DeletedBy=CurrentUserID,
                               DeletedOn=DateTime.Now
                                
                           },

                         //Fiat 10 %
                         new OptionIssue()
                           {
                               OptionIssueID=Guid.NewGuid(),
                               Issue=this.Issues[IssueIndex.Car_Model],
                               IssueID=this.Issues[IssueIndex.Car_Model].IssueID,
                               OptionIssueValue="Fiat",
                               OptionIssueWeight=10,
                               
                               Deleted=false,
                               DeletedBy=CurrentUserID,
                               DeletedOn=DateTime.Now
                                
                           },
                         
                         //Model 80 %
                         new OptionIssue()
                           {
                               OptionIssueID=Guid.NewGuid(),
                               Issue=this.Issues[IssueIndex.Car_Model],
                               IssueID=this.Issues[IssueIndex.Car_Model].IssueID,
                               OptionIssueValue="Mercedes",
                               OptionIssueWeight=80,
                               
                               Deleted=false,
                               DeletedBy=CurrentUserID,
                               DeletedOn=DateTime.Now
                                
                           },

                           #endregion Model-BMW-Fiat-Mercedes

                      #endregion Purchase a Car

                      #region Purchase Apartment
                           
                           #region Floor-Ceramic-Court-Without
                        
                         //Ceramic 80 %
                         new OptionIssue()
                           {
                               OptionIssueID=Guid.NewGuid(),
                               Issue=this.Issues[IssueIndex.Apartment_Floor],
                               IssueID=this.Issues[IssueIndex.Apartment_Floor].IssueID,
                               OptionIssueValue="Ceramic",
                               OptionIssueWeight=80,
                               
                               Deleted=false,
                               DeletedBy=CurrentUserID,
                               DeletedOn=DateTime.Now
                                
                           },

                         //Court 100 %
                         new OptionIssue()
                           {
                               OptionIssueID=Guid.NewGuid(),
                               Issue=this.Issues[IssueIndex.Apartment_Floor],
                               IssueID=this.Issues[IssueIndex.Apartment_Floor].IssueID,
                               OptionIssueValue="Parquet",
                               OptionIssueWeight=100,
                               
                               Deleted=false,
                               DeletedBy=CurrentUserID,
                               DeletedOn=DateTime.Now
                                
                           },  
                        
                           //Without 5 %
                         new OptionIssue()
                           {
                               OptionIssueID=Guid.NewGuid(),
                               Issue=this.Issues[IssueIndex.Apartment_Floor],
                               IssueID=this.Issues[IssueIndex.Apartment_Floor].IssueID,
                               OptionIssueValue="Without",
                               OptionIssueWeight=5,
                               
                               Deleted=false,
                               DeletedBy=CurrentUserID,
                               DeletedOn=DateTime.Now
                                
                           },  

                           #endregion Floor-Ceramic-Court-Without

                      #endregion Purchase a Apartment

                      #region Purchase a Computer
                       
                           #region Type-Dell-Accer-Toshiba
                        
                         //Dell 100 %
                         new OptionIssue()
                           {
                               OptionIssueID=Guid.NewGuid(),
                               Issue=this.Issues[IssueIndex.Computer_Type],
                               IssueID=this.Issues[IssueIndex.Computer_Type].IssueID,
                               OptionIssueValue="Dell",
                               OptionIssueWeight=100,
                               
                               Deleted=false,
                               DeletedBy=CurrentUserID,
                               DeletedOn=DateTime.Now
                                
                           },

                         //Accer 25 %
                         new OptionIssue()
                           {
                               OptionIssueID=Guid.NewGuid(),
                               Issue=this.Issues[IssueIndex.Computer_Type],
                               IssueID=this.Issues[IssueIndex.Computer_Type].IssueID,
                               OptionIssueValue="Acer",
                               OptionIssueWeight=25,
                               
                               Deleted=false,
                               DeletedBy=CurrentUserID,
                               DeletedOn=DateTime.Now
                                
                           },  
                        
                           //Toshiba 90 %
                         new OptionIssue()
                           {
                               OptionIssueID=Guid.NewGuid(),
                               Issue=this.Issues[IssueIndex.Computer_Type],
                               IssueID=this.Issues[IssueIndex.Computer_Type].IssueID,
                               OptionIssueValue="Toshiba",
                               OptionIssueWeight=90,
                               
                               Deleted=false,
                               DeletedBy=CurrentUserID,
                               DeletedOn=DateTime.Now
                                
                           }   

                           #endregion Type-Dell-Accer-Toshiba

#endregion Purchase a Computer

                      #region Purchase a Door
                         
 
#endregion Purchase a Door

                      };
                }

                return mOptionIssues;
            }

        }

        #endregion Option Issues
        
        #region Later Rated Issues

        /// <summary>
        /// Gets the later rated issues.
        /// </summary>
        /// <value>The later rated issues.</value>
        public List<LaterRatedIssue> LaterRatedIssues
        {
            get
            {
                if (mLaterRatedIssues == null)
                {
                    mLaterRatedIssues = new List<LaterRatedIssue>()
                    {
                     

                      #region Purchase a Door
                         
 
                         //maple 100 %
                         new LaterRatedIssue()
                           {
                               LaterRatedIssueID=Guid.NewGuid(),
                               Issue=this.Issues[IssueIndex.Door_Door_Type],
                               IssueID=this.Issues[IssueIndex.Door_Door_Type].IssueID,
                               LaterRatedIssueValue="Maple",
                               LaterRatedIssueWeight=100,
                               
                               Deleted=false,
                               DeletedBy=CurrentUserID,
                               DeletedOn=DateTime.Now
                                
                           },
                           //Oak  70 %
                         new LaterRatedIssue()
                           {
                               LaterRatedIssueID=Guid.NewGuid(),
                               Issue=this.Issues[IssueIndex.Door_Door_Type],
                               IssueID=this.Issues[IssueIndex.Door_Door_Type].IssueID,
                               LaterRatedIssueValue="Oak",
                               LaterRatedIssueWeight=70,
                               
                               Deleted=false,
                               DeletedBy=CurrentUserID,
                               DeletedOn=DateTime.Now
                                
                           }  
                        
#endregion Purchase a Door
                    };
                }
                return mLaterRatedIssues;
            }
        }

        #endregion

        #region Preference Set Neg

        /// <summary>
        /// Get a List Of Preference Set Negs
        /// </summary>
        /// <value>The preference set negs.</value>
        public List<PreferenceSetNeg> PreferenceSetNegs
        {
            get
            {
                if (mPreferenceSetNegs == null)
                {
                    mPreferenceSetNegs = new List<PreferenceSetNeg>()
                    {
                        new PreferenceSetNeg ()
                          {
                               NegID=Guid.NewGuid(),
                               Percentage=20,
                               PreferenceSet=this.PreferenceSets[0],
                               PreferenceSetID=this.PreferenceSets[0].PreferenceSetID,
                               
                               Deleted=false,
                               DeletedBy=CurrentUserID,
                               DeletedOn=DateTime.Now
                               
                          },

                          new PreferenceSetNeg ()
                          {
                               NegID=Guid.NewGuid(),
                               Percentage=50,
                               PreferenceSet=this.PreferenceSets[1],
                               PreferenceSetID=this.PreferenceSets[1].PreferenceSetID,
                               
                               Deleted=false,
                               DeletedBy=CurrentUserID,
                               DeletedOn=DateTime.Now
                               
                          },


                           new PreferenceSetNeg ()
                          {
                               NegID=Guid.NewGuid(),
                               Percentage=20,
                               PreferenceSet=this.PreferenceSets[0],
                               PreferenceSetID=this.PreferenceSets[0].PreferenceSetID,
                               
                               Deleted=false,
                               DeletedBy=CurrentUserID,
                               DeletedOn=DateTime.Now
                               
                          },

                          
                           new PreferenceSetNeg ()
                          {
                               NegID=Guid.NewGuid(),
                               Percentage=80,
                               PreferenceSet=this.PreferenceSets[1],
                               PreferenceSetID=this.PreferenceSets[1].PreferenceSetID,
                               
                               Deleted=false,
                               DeletedBy=CurrentUserID,
                               DeletedOn=DateTime.Now
                               
                          },


                    };
                }
                return mPreferenceSetNegs;

            }
        }

        #endregion Preference Set Neg
                
        #region Neg Conversation

        /// <summary>
        /// Get a List Of Neg Conversation
        /// </summary>
        /// <value>The neg conversations.</value>
        public List<NegConversation> NegConversations
        {
            get
            {
                if (mNegConversations == null)
                {
                    mNegConversations = new List<NegConversation>()
                    {
                        new NegConversation ()
                          {
                              NegConversationID=Guid.NewGuid(),
                              ConversationID=Guid.NewGuid(),
                               PreferenceSetNegID=this.PreferenceSetNegs[0].NegID,
                               PreferenceSetNeg=this.PreferenceSetNegs[0],
                               Percentage=20
                          },

                        new NegConversation ()
                          {
                              NegConversationID=Guid.NewGuid(),
                              ConversationID=Guid.NewGuid(),
                               PreferenceSetNegID=this.PreferenceSetNegs[1].NegID,
                               PreferenceSetNeg=this.PreferenceSetNegs[1],

                               
                               Percentage=80
                          },

                        new NegConversation ()
                          {
                               NegConversationID=Guid.NewGuid(),
                              ConversationID=Guid.NewGuid(),
                               PreferenceSetNegID=this.PreferenceSetNegs[1].NegID,
                               PreferenceSetNeg=this.PreferenceSetNegs[1],
                               Percentage=20
                          }


                    };
                }
                return mNegConversations;

            }
        }

        #endregion  Neg Conversation
        
        #region Conversation Message

        /// <summary>
        /// Get a List Of Conversation Messages
        /// </summary>
        /// <value>The conversation messages.</value>
        public List<ConversationMessage> ConversationMessages
        {
            get
            {
                if (mConversationMessages == null)
                {
                    mConversationMessages = new List<ConversationMessage>()
                    {
                        new ConversationMessage ()
                          { 
                              ConversationMessageID=Guid.NewGuid(),
                              MessageID=Guid.NewGuid(),
                              NegConversation=this.NegConversations[0],
                              NegConversationID=this.NegConversations[0].ConversationID.Value,
                              Percentage=20
                          },

                        new ConversationMessage ()
                          {   
                              ConversationMessageID=Guid.NewGuid(),
                              MessageID=Guid.NewGuid(),
                              NegConversation=this.NegConversations[0],
                              NegConversationID=this.NegConversations[0].ConversationID.Value,
                              Percentage=50
                          },


                          new ConversationMessage ()
                          {   
                              ConversationMessageID=Guid.NewGuid(),
                              MessageID=Guid.NewGuid(),
                              NegConversation=this.NegConversations[0],
                              NegConversationID=this.NegConversations[0].ConversationID.Value,
                              Percentage=12
                          },


                          new ConversationMessage ()
                          {  
                             ConversationMessageID=Guid.NewGuid(),
                              MessageID=Guid.NewGuid(),
                              NegConversation=this.NegConversations[1],
                              NegConversationID=this.NegConversations[1].ConversationID.Value,
                              Percentage=33
                          },


                          new ConversationMessage ()
                          { 
                              ConversationMessageID=Guid.NewGuid(),
                              MessageID=Guid.NewGuid(),
                              NegConversation=this.NegConversations[1],
                              NegConversationID=this.NegConversations[1].ConversationID.Value,
                              Percentage=55
                          },
 

                    };
                }
                return mConversationMessages;

            }
        }

        #endregion Conversation Message

        #endregion Generate All Objects Mock

        #endregion Properties

        #region → Constructor    .
        /// <summary>
        /// Default Constructor
        /// </summary>
        public PrefAppServiceTest()
        {
            CountOfAllEntries =
                                this.MainPreferenceSetsCount +
                                this.PreferenceSetCount +
                                this.IssuesCount +
                                this.IssueTypesCount +
                                this.OptionIssuesCount +
                                this.LaterRatedIssuesCount +
                                this.NumericIssuesCount +

                                this.NegConversationsCount +
                                this.PreferenceSetNegsCount +
                                this.ConversationMessagesCount;
        }

        #endregion Constructor

        #region → Event Handlers .

        /// <summary>
        /// Inserts all entries complete.
        /// </summary>
        /// <param name="subOp">The sub op.</param>
        void InsertingAllEntriesComplete(SubmitOperation subOp)
        {
            if (!subOp.HasError)
            {
                if (subOp.ChangeSet.AddedEntities.Count != this.CountOfAllEntries)
                {
                    eNegMessageBox.ShowMessageBox(false, "InsertAllEntriesComplete", "Number of Records Inserted is not right.");
                }
                else
                {
                    TestUpdatingAllEntries();
                }
            }
            else
            {
                eNegMessageBox.ShowMessageBox(false, "InsertAllEntriesComplete", subOp.Error);
            }
        }

        /// <summary>
        /// Updatings all entries complete.
        /// </summary>
        /// <param name="subOp">The sub op.</param>
        void UpdatingAllEntriesComplete(SubmitOperation subOp)
        {
            if (!subOp.HasError)
            {

                if (subOp.ChangeSet.AddedEntities.Count == 0 &&
                    subOp.ChangeSet.RemovedEntities.Count == 0 &&
                    subOp.ChangeSet.ModifiedEntities.Count != this.CountOfAllEntries)
                {
                    eNegMessageBox.ShowMessageBox(false, "UpdateAllEntriesComplete", "Number of Records Inserted is not right.");
                }
                else
                {
                    TestDeletingAllEntries();
                }
            }
            else
            {
                eNegMessageBox.ShowMessageBox(false, "UpdateAllEntriesComplete", subOp.Error);
            }
        }

        /// <summary>
        /// Event Complete of  Delete All Entries
        /// </summary>
        /// <param name="subOp">Value of subOp</param>
        void DeletingAllEntriesComplete(SubmitOperation subOp)
        {
            if (!subOp.HasError)
            {

                if (subOp.ChangeSet.AddedEntities.Count == 0 &&
                    subOp.ChangeSet.ModifiedEntities.Count == 0 &&
                    subOp.ChangeSet.RemovedEntities.Count != this.CountOfAllEntries - this.MainPreferenceSetsCount - this.IssueTypesCount)
                {
                    eNegMessageBox.ShowMessageBox(false, "DeleteAllEntriesComplete", "Number of Records Inserted is not right.");
                }
                else
                {
                    eNegMessageBox.ShowMessageBox(true, "Inset - Update - Delete All Entries ", "");
                }
            }
            else
            {
                eNegMessageBox.ShowMessageBox(false, "DeleteAllEntriesComplete", subOp.Error);
            }
        }

        #endregion

        #region → Methods        .

        #region → Public         .

        #region Test Insert All Entries

        /// <summary>
        /// Tests the inserting all entries like (PrefernceSets, Issues, Option Issues, LaterRated Issues, PreferenceSet Negs, ...etc).
        /// </summary>
        [TestMethod()]
        public void TestInsertingAllEntries()
        {
            try
            {
                foreach (var item in this.MainPreferenceSets)
                {
                    this.Context.MainPreferenceSets.Add(item);
                }

                foreach (var item in this.PreferenceSets)
                {
                    this.Context.PreferenceSets.Add(item);
                }

                foreach (var item in this.Issues)
                {
                    this.Context.Issues.Add(item);
                }

                foreach (var item in this.IssueTypes)
                {
                    this.Context.IssueTypes.Add(item);
                }

                foreach (var item in this.NumericIssues)
                {
                    this.Context.NumericIssues.Add(item);
                }

                foreach (var item in this.OptionIssues)
                {
                    this.Context.OptionIssues.Add(item);
                }

                foreach (var item in this.LaterRatedIssues)
                {
                    this.Context.LaterRatedIssues.Add(item);
                }

                foreach (var item in this.PreferenceSetNegs)
                {
                    this.Context.PreferenceSetNegs.Add(item);
                }

                foreach (var item in this.NegConversations)
                {
                    this.Context.NegConversations.Add(item);
                }

                foreach (var item in this.ConversationMessages)
                {
                    this.Context.ConversationMessages.Add(item);
                }

                this.Context.SubmitChanges(new Action<SubmitOperation>(InsertingAllEntriesComplete), null);
            }
            catch (Exception ex)
            {
                eNegMessageBox.ShowMessageBox(false, "InsertAllEntries", ex);
            }
        }

        #endregion Test Insert All Entries

        #region Test Update All Entries

        /// <summary>
        /// Tests the updating all entries.
        /// </summary>
        public void TestUpdatingAllEntries()
        {
            try
            {
                this.Context.RejectChanges();

                foreach (var item in this.Context.MainPreferenceSets)
                {
                    item.MainPreferenceSetName = item.MainPreferenceSetName + "_Update";
                }

                foreach (var item in this.Context.PreferenceSets)
                {
                    item.PreferenceSetName = item.PreferenceSetName + "_Update";
                }


                foreach (var item in this.Context.Issues)
                {
                    item.IssueName = item.IssueName + "_Update";
                }


                foreach (var item in this.Context.IssueTypes)
                {
                    item.IssueTypeName = item.IssueTypeName + "_Update";
                }

                foreach (var item in this.Context.NumericIssues)
                {
                    item.Unit = item.Unit + "_Update";
                }


                foreach (var item in this.Context.OptionIssues)
                {
                    item.OptionIssueValue = item.OptionIssueValue + "_Update";
                }


                foreach (var item in this.Context.LaterRatedIssues)
                {
                    item.LaterRatedIssueValue = item.LaterRatedIssueValue + "_Update";
                }

                foreach (var item in this.Context.PreferenceSetNegs)
                {
                    item.Percentage += 11000;
                }


                foreach (var item in this.Context.NegConversations)
                {
                    item.Percentage += 22000;
                }

                foreach (var item in this.Context.ConversationMessages)
                {
                    item.Percentage += 33000;
                }


                this.Context.SubmitChanges(new Action<SubmitOperation>(UpdatingAllEntriesComplete), null);
            }
            catch (Exception ex)
            {
                eNegMessageBox.ShowMessageBox(false, "UpdateAllEntries", ex);
            }
        }
              
        #endregion Test Update All Entries

        #region Test Delete All Entries
        
        /// <summary>
        /// Tests the deleting all entries.
        /// </summary>
        public void TestDeletingAllEntries()
        {
            try
            {
                this.Context.RejectChanges();

                while (this.LaterRatedIssues.Count > 0)
                {
                    this.Context.LaterRatedIssues.Remove(this.LaterRatedIssues[0]);
                    this.LaterRatedIssues.RemoveAt(0);
                }

                while (this.OptionIssues.Count > 0)
                {
                    this.Context.OptionIssues.Remove(this.OptionIssues[0]);
                    this.OptionIssues.RemoveAt(0);
                }

                while (this.NumericIssues.Count > 0)
                {
                    this.Context.NumericIssues.Remove(this.NumericIssues[0]);
                    this.NumericIssues.RemoveAt(0);
                }

                while (this.Issues.Count > 0)
                {
                    this.Context.Issues.Remove(this.Issues[0]);
                    this.Issues.RemoveAt(0);
                }


                while (this.ConversationMessages.Count > 0)
                {
                    this.Context.ConversationMessages.Remove(this.ConversationMessages[0]);
                    this.ConversationMessages.RemoveAt(0);
                }


                while (this.NegConversations.Count > 0)
                {
                    this.Context.NegConversations.Remove(this.NegConversations[0]);
                    this.NegConversations.RemoveAt(0);
                }


                while (this.PreferenceSetNegs.Count > 0)
                {
                    this.Context.PreferenceSetNegs.Remove(this.PreferenceSetNegs[0]);
                    this.PreferenceSetNegs.RemoveAt(0);
                }


                while (this.PreferenceSets.Count > 0)
                {
                    this.Context.PreferenceSets.Remove(this.PreferenceSets[0]);
                    this.PreferenceSets.RemoveAt(0);
                }

                //while (this.MainPreferenceSets.Count > 0)
                //{
                //    this.Context.MainPreferenceSets.Remove(this.MainPreferenceSets[0]);
                //    this.MainPreferenceSets.RemoveAt(0);
                //}

                //while (this.IssueTypes.Count > 0)
                //{
                //    this.Context.IssueTypes.Remove(this.IssueTypes[0]);
                //    this.IssueTypes.RemoveAt(0);
                //}

                this.Context.SubmitChanges(new Action<SubmitOperation>(DeletingAllEntriesComplete), null);
            }
            catch (Exception ex)
            {
                eNegMessageBox.ShowMessageBox(false, "DeleteAllEntries", ex);
            }
        }            

        #endregion Test Delete All Entries

        #endregion → Public

        #endregion Methods

    }
}
