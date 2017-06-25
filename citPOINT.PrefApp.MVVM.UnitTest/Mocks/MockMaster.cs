
#region → Usings   .
using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using citPOINT.PrefApp.ViewModel;
//using System.ComponentModel.Composition;
using citPOINT.PrefApp.Common;
using citPOINT.PrefApp.Data.Web;
using System.Collections.Generic;
#endregion

#region → History  .

/* Date         User              Change
 * 
 * 13.02.11     M.Whab       Creation
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
    /// Mock master inherited from any othr mock need these data
    /// </summary>
    public class MockMaster
    {
        #region → Fields         .

        private static List<PrefAppContext> oldContext = new List<PrefAppContext>();

        private PrefAppContext mContext;

        private List<MainPreferenceSet> mMainPreferenceSets;
        private List<PreferenceSet> mPreferenceSets;
        private List<Issue> mIssues;
        private List<NumericIssue> mNumericIssues;
        private List<OptionIssue> mOptionIssues;
        private List<IssueType> mIssueTypes;
        private List<LaterRatedIssue> mLaterRatedIssues;

        private List<PreferenceSetNeg> mPreferenceSetNegs;
        private List<NegConversation> mNegConversations;
        private List<Negotiation> mNegotiations;
        private List<Conversation> mConversations;
        private List<Message> mMessages;
        private List<ConversationMessage> mConversationMessages;
        private List<MessageIssue> mMessageIssues;
        private List<MessageLaterRatedIssue> mMessageLaterRatedIssues;
        private List<MessageOptionIssue> mMessageOptionIssues;
        private List<Negotiation> mAvailableNegotiations;
        private List<Organization> mOrganizations;
        private List<IssueStatisticalsResult> mIssueStatisticals;

        #region → I n d e x e s    .

        public const int IssueType_Numeric = 0;
        public const int IssueType_Options = 1;
        public const int IssueType_LaterRated = 2;
        public const int IssueType_NotRated = 3;

        /// <summary>
        /// Index for Issues
        /// </summary>
        public struct IssueIndex
        {
            public static int Car_Price = 0;
            public static int Car_Color = 1;
            public static int Car_Power = 2;
            public static int Car_Model = 3;

            public static int Apartment_Size = 4;
            public static int Apartment_RoomsCount = 5;
            public static int Apartment_Floor = 6;
            public static int Apartment_Addresss = 7;

            public static int Computer_Price = 8;
            public static int Computer_Type = 9;
            public static int Computer_Monitor_Size = 10;

            public static int Door_Price = 11;
            public static int Door_Door_Type = 12;
        }

        /// <summary>
        /// General Index
        /// </summary>
        public class GeneralIndex
        {

            public static int Car = 0;
            public static int Apartment = 1;
            public static int Computer = 2;
            public static int Door = 3;


            public static Guid Car_Guid;
            public static Guid Apartment_Guid;
            public static Guid Computer_Guid;
            public static Guid Door_Guid;
        }

        /// <summary>
        /// 
        /// </summary>
        public class PreferenceSetIndex : GeneralIndex
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public class NegotiationIndex : GeneralIndex
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public class PreferenceSetNegIndex : GeneralIndex
        {
        }

        /// <summary>
        /// Index for conversations
        /// </summary>
        public class ConversationIndex
        {
            public static int Car_Conversation_BMW = 0;
            public static int Car_Conversation_Fiat = 1;


            public static Guid Car_Conversation_BMW_Guid;
            public static Guid Car_Conversation_Fiat_Guid;

        }

        /// <summary>
        /// Index for messages
        /// </summary>
        public class MessageIndex
        {
            public static int Car_Msg_BMW_001 = 0;
            public static int Car_Msg_BMW_002 = 1;
            public static int Car_Msg_Fiat_003 = 2;




            public static Guid Car_Msg_BMW_001_Guid;
            public static Guid Car_Msg_BMW_002_Guid;
            public static Guid Car_Msg_Fiat_003_Guid;


        }

        #endregion

        private TestContext testContextInstance;

        #endregion Fields

        #region → Properties     .

        #region → Generate All Objects Mock    .

        #region → User                .

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

        #region → Main Preference Set .

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
                                MainPreferenceSetID=Common.PrefAppConstant.MainPreferenceSets.MySets,
                                MainPreferenceSetName="My Sets"
                           },

                        new MainPreferenceSet ()
                           { 
                                MainPreferenceSetID=Common.PrefAppConstant.MainPreferenceSets.OrganizationSets,
                                MainPreferenceSetName="Organization Sets"
                           },

                        new MainPreferenceSet ()
                           { 
                                MainPreferenceSetID=Common.PrefAppConstant.MainPreferenceSets.SetStore,
                                MainPreferenceSetName="Set Store"
                           }

                    };
                }
                return mMainPreferenceSets;

            }
        }

        #endregion Main Preference Set

        #region → Preference Set      .

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
                                
                                Deleted=false,
                                DeletedBy=CurrentUserID,
                                DeletedOn=DateTime.Now
                           },

                       new PreferenceSet ()
                           { 
                                PreferenceSetID=Guid.NewGuid(),
                                PreferenceSetName="Z_Organization Sets",
                                MainPreferenceSetID=this.MainPreferenceSets[1].MainPreferenceSetID,//Organization Sets
                                MainPreferenceSet=this.MainPreferenceSets[1],//Organization Sets
                                UserID=CurrentUserID,
                                
                                Deleted=false,
                                DeletedBy=CurrentUserID,
                                DeletedOn=DateTime.Now
                           },

                       new PreferenceSet ()
                           { 
                                PreferenceSetID=Guid.NewGuid(),
                                PreferenceSetName="Z_Set Store",
                                MainPreferenceSetID=this.MainPreferenceSets[2].MainPreferenceSetID,//Set Store
                                MainPreferenceSet=this.MainPreferenceSets[2],//Set Store
                                UserID=CurrentUserID,
                                
                                Deleted=false,
                                DeletedBy=CurrentUserID,
                                DeletedOn=DateTime.Now
                           } 

                    };
                }
                return mPreferenceSets;
            }
        }

        #endregion Preference Set

        #region → Issue Types         .

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
                                IssueTypeID=PrefAppConstant.IssueTypes.Numeric,
                                IssueTypeName="Numeric"
                            },

                            new IssueType ()
                            { 
                                IssueTypeID=PrefAppConstant.IssueTypes.Options,
                                IssueTypeName="Options"
                            },

                            new IssueType ()
                            { 
                                IssueTypeID=PrefAppConstant.IssueTypes.LaterRated,
                                IssueTypeName="Later Rated"
                            }
                            ,

                            new IssueType ()
                            { 
                                IssueTypeID=PrefAppConstant.IssueTypes.NotRated,
                                IssueTypeName="Not Rated"
                            } 

                       };
                }
                return mIssueTypes;
            }
        }

        #endregion Isssue Types

        #region → Issues              .

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
                        //0
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
                           //1
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
                           //2
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
                          //3
                       new Issue ()
                           { 
                                PreferenceSetID=this.PreferenceSets[0].PreferenceSetID /*Car*/,
                                PreferenceSet=this.PreferenceSets[0],
                                IssueID=Guid.NewGuid(),
                                IssueName="Model",
                                IssueType=this.IssueTypes[IssueType_LaterRated],
                                IssueTypeID=this.IssueTypes[IssueType_LaterRated].IssueTypeID,
                                IssueWeight=40,
                                
                                Deleted=false,
                                DeletedBy=CurrentUserID,
                                DeletedOn=DateTime.Now
                           },

                        #endregion Purchase a Car

                        #region Purchase Apartment 
                           //4
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
                           //5
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

                           //6
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

                           //7
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
                         //8
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
                           //9
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
                           //10
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
                        //11
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
                           //12
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
                           }  

                        #endregion Purchase a Door

                    };
                }
                return mIssues;
            }
        }

        #endregion Issues

        #region → Numeric Issue       .
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
                               Unit="$",
                               Deleted=false,
                               DeletedBy=CurrentUserID,
                               DeletedOn=DateTime.Now
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
                               Unit="Hourse",
                               Deleted=false,
                               DeletedBy=CurrentUserID,
                               DeletedOn=DateTime.Now
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
                               Unit="Meter",
                               Deleted=false,
                               DeletedBy=CurrentUserID,
                               DeletedOn=DateTime.Now
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
                               Unit="Room",
                               Deleted=false,
                               DeletedBy=CurrentUserID,
                               DeletedOn=DateTime.Now
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
                               Unit="$",
                               Deleted=false,
                               DeletedBy=CurrentUserID,
                               DeletedOn=DateTime.Now
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
                               Unit="Inch",
                               Deleted=false,
                               DeletedBy=CurrentUserID,
                               DeletedOn=DateTime.Now
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
                               Unit="$",
                               Deleted=false,
                               DeletedBy=CurrentUserID,
                               DeletedOn=DateTime.Now
                           }
 
#endregion Purchase a Door

                      };
                }

                return mNumericIssues;
            }

        }

        #endregion Numeric Issue

        #region → Option Issues       .

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

        #region → Later Rated Issues  .

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

                      #region Model-BMW-Fiat-Mercedes

                        
                         //BMW 100 %
                         new LaterRatedIssue()
                           {
                               LaterRatedIssueID=Guid.NewGuid(),
                               Issue=this.Issues[IssueIndex.Car_Model],
                               IssueID=this.Issues[IssueIndex.Car_Model].IssueID,
                               LaterRatedIssueValue="BMW",
                               LaterRatedIssueWeight=100,
                               
                               Deleted=false,
                               DeletedBy=CurrentUserID,
                               DeletedOn=DateTime.Now
                                
                           },

                         //Fiat 10 %
                         new LaterRatedIssue()
                           {
                               LaterRatedIssueID=Guid.NewGuid(),
                               Issue=this.Issues[IssueIndex.Car_Model],
                               IssueID=this.Issues[IssueIndex.Car_Model].IssueID,
                               LaterRatedIssueValue="Fiat",
                               LaterRatedIssueWeight=10,
                               
                               Deleted=false,
                               DeletedBy=CurrentUserID,
                               DeletedOn=DateTime.Now
                                
                           },
                         
                         //Model 80 %
                         new LaterRatedIssue()
                           {
                               LaterRatedIssueID=Guid.NewGuid(),
                               Issue=this.Issues[IssueIndex.Car_Model],
                               IssueID=this.Issues[IssueIndex.Car_Model].IssueID,
                               LaterRatedIssueValue="Mercedes",
                               LaterRatedIssueWeight=80,
                               
                               Deleted=false,
                               DeletedBy=CurrentUserID,
                               DeletedOn=DateTime.Now
                                
                           },

                      #endregion Model-BMW-Fiat-Mercedes

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

        #region → eNeg..Negotiations  .


        /// <summary>
        /// Gets the negotiations.
        /// </summary>
        /// <value>The negotiations.</value>
        public List<Negotiation> Negotiations
        {
            get
            {
                if (mNegotiations == null)
                {


                    mNegotiations = new List<Negotiation>()
                    {
                     
                    #region → Purchase a Car For My Family    .
                         
 
                         //Purchase a Car For My Family
                         new Negotiation()
                           { 
                               NegotiationID=Guid.NewGuid(),
                               NegotiationName="Purchase a Car For My Family",
                               IsClosed=false
                                  
                           } ,
                    #endregion Purchase a Car For Me.
                           
                    #region → Purchase Apartment 150 M        .
 
                         //Purchase a Apartment 150 M
                         new Negotiation()
                           { 
                               NegotiationID=Guid.NewGuid(),
                               NegotiationName="Purchase Apartment 150 M",
                               IsClosed=false
                                  
                           } ,

                    #endregion  Purchase a Apartment .
                         
                    #region → Purchase a Laptop For Graphics  .
                          
 
                         //"Purchase a Laptop For Graphics"
                         new Negotiation()
                           { 
                               NegotiationID=Guid.NewGuid(),
                               NegotiationName="Purchase a Laptop For Graphics",
                               IsClosed=false
                                  
                           } ,


                       
                    #endregion Purchase a Computer  .
                         
                    #region → Purchase a Door 70x200 Cm       .
  
                         //"Purchase a Door 70x200 Cm"
                          new Negotiation()
                           { 
                               NegotiationID=Guid.NewGuid(),
                               NegotiationName="Purchase a Door 70x200 Cm",
                               IsClosed=false
                                  
                           }  


                    #endregion Purchase a Computer  .
                         
                    };
                }
                return mNegotiations;
            }
        }

        #endregion

        #region → eNeg..Conversation  .

        /// <summary>
        /// Gets the conversations.
        /// </summary>
        /// <value>The conversations.</value>
        public List<Conversation> Conversations
        {
            get
            {
                if (mConversations == null)
                {
                    mConversations = new List<Conversation>()
                    {
                     
                    #region → Purchase a Car For My Family    .
                         
 
                         //BMW
                         new Conversation()
                           { 
                              ConversationID=Guid.NewGuid(),
                              ConversationName="Conversation BMW",
                              NegotiationID=this.Negotiations[NegotiationIndex.Car].NegotiationID
                           } ,

                        //Fiat
                         new Conversation()
                           { 
                              ConversationID=Guid.NewGuid(),
                              ConversationName="Conversation Fiat",
                              NegotiationID=this.Negotiations[NegotiationIndex.Car].NegotiationID
                           }  

                    #endregion Purchase a Car For Me.
                           
                    #region → Purchase Apartment 150 M        .
  

                    #endregion  Purchase a Apartment .
                         
                    #region → Purchase a Laptop For Graphics  .
                          
  


                       
                    #endregion Purchase a Computer  .
                         
                    #region → Purchase a Door 70x200 Cm       .
  
                        


                    #endregion Purchase a Computer  .
                         
                    };
                }
                return mConversations;
            }
        }

        #endregion

        #region → eNeg..Messages      .

        /// <summary>
        /// Gets the messages.
        /// </summary>
        /// <value>The messages.</value>
        public List<Message> Messages
        {
            get
            {
                if (mMessages == null)
                {
                    mMessages = new List<Message>()
                    {
                     
                    #region → Purchase a Car For My Family    .
                         
 
                         //BMW 001
                         new Message()
                           { 
                              
                              MessageContent="We Have a car its price is 10,000 EUR and its Colour is blackk",
                              ConversationID=this.Conversations[ConversationIndex.Car_Conversation_BMW].ConversationID,
                              IsSent=false,
                              MessageDate=DateTime.Now,
                              MessageID=Guid.NewGuid(),
                              MessageName="New Message"                               ,
                              MessageReceiver="<Point@point.com>",
                              MessageSender="Model@Model.com",
                              MessageSubject="New Message",
                           } ,


                        //BMW 002
                         new Message()
                           { 
                              
                              MessageContent="I need a car that may be Black and price under 15,000 EUR. thanks",
                              ConversationID=this.Conversations[ConversationIndex.Car_Conversation_BMW].ConversationID,
                              IsSent=true,
                              MessageDate=DateTime.Now,
                              MessageID=Guid.NewGuid(),
                              MessageName="Top manager"                               ,
                              MessageReceiver="<Point@point.com>",
                              MessageSender="Model@Model.com",
                              MessageSubject="Need a car",
                           } ,

 
                        //Fiat 001
                         new Message()
                           { 
                              
                              MessageContent="I need a car that may be Yellow and price under 5,000 EUR.Model 1978 thanks",
                              ConversationID=this.Conversations[ConversationIndex.Car_Conversation_Fiat].ConversationID,
                              IsSent=true,
                              MessageDate=DateTime.Now,
                              MessageID=Guid.NewGuid(),
                              MessageName="Top manager"                               ,
                              MessageReceiver="<Point@point.com>",
                              MessageSender="Model@Model.com",
                              MessageSubject="Need a car",
                           }  


                    #endregion Purchase a Car For Me.
                           
                    #region → Purchase Apartment 150 M        .
  

                    #endregion  Purchase a Apartment .
                         
                    #region → Purchase a Laptop For Graphics  .
                           
                       
                    #endregion Purchase a Computer  .
                         
                    #region → Purchase a Door 70x200 Cm       .
  
                        


                    #endregion Purchase a Computer  .
                         
                    };
                }
                return mMessages;
            }
        }

        #endregion

        #region → Preference Set Neg  .

        /// <summary>
        /// Gets the preference set negs.
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
                     
                    #region → Purchase a Car For My Family    .
                         
 
                         //Purchase a Car For My Family
                         new PreferenceSetNeg()
                           {
                               
                               PreferenceSetNegID=Guid.NewGuid(),
                               
                               

                               Percentage=0,
                               PreferenceSet=this.PreferenceSets[PreferenceSetIndex.Car],
                               PreferenceSetID=this.PreferenceSets[PreferenceSetIndex.Car].PreferenceSetID,
                               
                               #region Form eNeg Negotiation.

                                       NegID=this.Negotiations[NegotiationIndex.Car].NegotiationID,
                                       IsClosed=this.Negotiations[NegotiationIndex.Car].IsClosed,
                                       //"Purchase a Car For My Family"
                                       Name=this.Negotiations[NegotiationIndex.Car].NegotiationName,
                               
                               #endregion

                               Deleted=false,
                               DeletedBy=CurrentUserID,
                               DeletedOn=DateTime.Now
                           } ,
                    #endregion Purchase a Car For Me.
                           
                    #region → Purchase Apartment 150 M        .
                         
 
                         //"Purchase a Apartment 150 M"
                         new PreferenceSetNeg()
                           {
                               PreferenceSetNegID=Guid.NewGuid(),
                               
                               Percentage=0,
                               PreferenceSet=this.PreferenceSets[PreferenceSetIndex.Apartment],
                               PreferenceSetID=this.PreferenceSets[PreferenceSetIndex.Apartment].PreferenceSetID,
                              

                               #region Form eNeg Negotiation.

                                       NegID=this.Negotiations[NegotiationIndex.Apartment].NegotiationID,
                                       IsClosed=this.Negotiations[NegotiationIndex.Apartment].IsClosed,
                                       //Purchase Apartment 150 M
                                       Name=this.Negotiations[NegotiationIndex.Apartment].NegotiationName,
                               
                               #endregion


                               Deleted=false,
                               DeletedBy=CurrentUserID,
                               DeletedOn=DateTime.Now
                           } ,
                    #endregion  Purchase a Apartment .
                         
                    #region → Purchase a Laptop For Graphics  .
                          
 
                         //"Purchase a Laptop For Graphics"
                         new PreferenceSetNeg()
                           {
                               PreferenceSetNegID=Guid.NewGuid(),
                               
                               Percentage=0,
                               PreferenceSet=this.PreferenceSets[PreferenceSetIndex.Computer],
                               PreferenceSetID=this.PreferenceSets[PreferenceSetIndex.Computer].PreferenceSetID,
                               
                               #region Form eNeg Negotiation.

                                       NegID=this.Negotiations[NegotiationIndex.Computer].NegotiationID,
                                       IsClosed=this.Negotiations[NegotiationIndex.Computer].IsClosed,
                                       //"Purchase a Laptop For Graphics
                                       Name=this.Negotiations[NegotiationIndex.Computer].NegotiationName,
                               
                               #endregion

                               Deleted=false,
                               DeletedBy=CurrentUserID,
                               DeletedOn=DateTime.Now
                           } ,
                    #endregion Purchase a Computer  .
                         
                    #region → Purchase a Door 70x200 Cm       .
                          
 
                         //"Purchase a Door 70x200 Cm"
                         new PreferenceSetNeg()
                           {
                               PreferenceSetNegID=Guid.NewGuid(),
                              
                               Percentage=0,
                               PreferenceSet=this.PreferenceSets[PreferenceSetIndex.Door],
                               PreferenceSetID=this.PreferenceSets[PreferenceSetIndex.Door].PreferenceSetID,
                               

                               #region Form eNeg Negotiation.

                                       NegID=this.Negotiations[NegotiationIndex.Computer].NegotiationID,
                                       IsClosed=this.Negotiations[NegotiationIndex.Computer].IsClosed,
                                       //Purchase a Door 70x200 Cm
                                       Name=this.Negotiations[NegotiationIndex.Computer].NegotiationName,
                               
                               #endregion

                               Deleted=false,
                               DeletedBy=CurrentUserID,
                               DeletedOn=DateTime.Now
                           } 
                    #endregion Purchase a Computer  .
                         
                    };
                }
                return mPreferenceSetNegs;
            }
        }
        #endregion

        #region → Neg. Conversations  .
        /// <summary>
        /// Gets the preference set negs.
        /// </summary>
        /// <value>The preference set negs.</value>
        public List<NegConversation> NegConversations
        {
            get
            {
                if (mNegConversations == null)
                {
                    mNegConversations = new List<NegConversation>()
                    {
                     
                    #region → Purchase a Car For My Family    .
                         
 
                         //Conversation BMW
                         new NegConversation()
                           {
                                
                               NegConversationID=Guid.NewGuid(),
                               
                               Percentage=0,
                               PreferenceSetNeg=this.PreferenceSetNegs[PreferenceSetIndex.Car],
                               PreferenceSetNegID=this.PreferenceSetNegs[PreferenceSetIndex.Car].PreferenceSetNegID,
                               ConversationID=this.Conversations[ConversationIndex.Car_Conversation_BMW].ConversationID,
                               Name=this.Conversations[ConversationIndex.Car_Conversation_BMW].ConversationName,

                               Deleted=false,
                               DeletedBy=CurrentUserID,
                               DeletedOn=DateTime.Now
                           } ,

                         // Car_Conversation_Fiat
                         new NegConversation()
                           {
                                
                               NegConversationID=Guid.NewGuid(),
                               
                               Percentage=0,
                               PreferenceSetNeg=this.PreferenceSetNegs[PreferenceSetIndex.Car],
                               PreferenceSetNegID=this.PreferenceSetNegs[PreferenceSetIndex.Car].PreferenceSetNegID,
                               ConversationID=this.Conversations[ConversationIndex.Car_Conversation_Fiat].ConversationID,
                               Name=this.Conversations[ConversationIndex.Car_Conversation_Fiat].ConversationName,

                               Deleted=false,
                               DeletedBy=CurrentUserID,
                               DeletedOn=DateTime.Now
                           }  
                    #endregion Purchase a Car For Me.
                           
                    #region → Purchase Apartment 150 M        .
                         
  
                    #endregion  Purchase a Apartment .
                         
                    #region → Purchase a Laptop For Graphics  .
                          
  
                    #endregion Purchase a Computer  .
                         
                    #region → Purchase a Door X 70x200 Cm     .
                          
  
                    #endregion Purchase a Computer  .
                         
                    };
                }
                return mNegConversations;
            }
        }

        #endregion

        #region → Conversation Message.

        /// <summary>
        /// Gets the conversation messages.
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
                     
                    #region → Purchase a Car For My Family    .
                         
 
                         //Car_Msg_BMW_001
                         new ConversationMessage()
                           {
                               ConversationMessageID=Guid.NewGuid(),
                               NegConversationID = this.NegConversations[ConversationIndex.Car_Conversation_BMW].NegConversationID ,
                               NegConversation= this.NegConversations[ConversationIndex.Car_Conversation_BMW] ,
                               MessageID= this.Messages[MessageIndex.Car_Msg_BMW_001].MessageID ,
                                
                               IsSent=true,
                               Deleted=false,
                               DeletedBy=CurrentUserID,
                               DeletedOn=new DateTime(2011,5,30)
                           } ,

 
                         //Car_Msg_BMW_002
                         new ConversationMessage()
                           {
                               ConversationMessageID=Guid.NewGuid(),
                               NegConversationID = this.NegConversations[ConversationIndex.Car_Conversation_BMW].NegConversationID ,
                               NegConversation= this.NegConversations[ConversationIndex.Car_Conversation_BMW] ,
                               MessageID= this.Messages[MessageIndex.Car_Msg_BMW_002].MessageID ,
                                
                               IsSent=false,
                               Deleted=false,
                               DeletedBy=CurrentUserID,
                               DeletedOn=new DateTime(2011,8,20)
                           } ,

                            
                         //Car_Msg_Fiat_003
                         new ConversationMessage()
                           {
                               ConversationMessageID=Guid.NewGuid(),
                               NegConversationID = this.NegConversations[ConversationIndex.Car_Conversation_Fiat].NegConversationID ,
                               NegConversation= this.NegConversations[ConversationIndex.Car_Conversation_Fiat] ,
                               MessageID= this.Messages[MessageIndex.Car_Msg_Fiat_003].MessageID ,
                                
                               IsSent=true,
                               Deleted=false,
                               DeletedBy=CurrentUserID,
                               DeletedOn=new DateTime(2012,11,11)
                           }  

                           
                    #endregion Purchase a Car For Me.
                           
                    #region → Purchase Apartment 150 M        .
                         
  
                    #endregion  Purchase a Apartment .
                         
                    #region → Purchase a Laptop For Graphics  .
                          
  
                    #endregion Purchase a Computer  .
                         
                    #region → Purchase a Door X 70x200 Cm     .
                          
  
                    #endregion Purchase a Computer  .
                         
                    };
                }
                return mConversationMessages;
            }
        }

        #endregion

        #region → Message Issues      .


        /// <summary>
        /// Gets the message issues.
        /// </summary>
        /// <value>The message issues.</value>
        public List<MessageIssue> MessageIssues
        {
            get
            {
                if (mMessageIssues == null)
                {
                    mMessageIssues = new List<MessageIssue>()
                    {

                        #region → Purchase a Car For My Family    .



                        ////Car_Msg_BMW_001 Issue One Price 15,000
                        //new MessageIssue()
                        //  {
                        //      MessageIssueID=Guid.NewGuid(),
                        //      ConversationMessageID=this.ConversationMessages[MessageIndex.Car_Msg_BMW_001].ConversationMessageID,
                        //      ConversationMessage=this.ConversationMessages[MessageIndex.Car_Msg_BMW_001],
                        //      IssueID=this.Issues[IssueIndex.Car_Price].IssueID,
                        //      Issue=this.Issues[IssueIndex.Car_Price],
                        //      Value="15000.00",
                        //      Score=0,
                        //      Deleted=false,
                        //      DeletedBy=CurrentUserID,
                        //      DeletedOn=DateTime.Now
                        //  } ,


                        //  //Car_Msg_BMW_001 Issue One Colors Blues
                        //new MessageIssue()
                        //  {
                        //      MessageIssueID=Guid.NewGuid(),
                        //      ConversationMessageID=this.ConversationMessages[MessageIndex.Car_Msg_BMW_001].ConversationMessageID,
                        //      ConversationMessage=this.ConversationMessages[MessageIndex.Car_Msg_BMW_001],
                        //      IssueID=this.Issues[IssueIndex.Car_Color].IssueID,
                        //      Issue=this.Issues[IssueIndex.Car_Color],
                        //      Value=null,
                        //      Score=0,
                        //      Deleted=false,
                        //      DeletedBy=CurrentUserID,
                        //      DeletedOn=DateTime.Now
                        //  } ,

                        //  //Car_Msg_BMW_001 Issue One Colors Blues
                        //new MessageIssue()
                        //  {
                        //      MessageIssueID=Guid.NewGuid(),
                        //      ConversationMessageID=this.ConversationMessages[MessageIndex.Car_Msg_BMW_001].ConversationMessageID,
                        //      ConversationMessage=this.ConversationMessages[MessageIndex.Car_Msg_BMW_001],
                        //      IssueID=this.Issues[IssueIndex.Car_Power].IssueID,
                        //      Issue=this.Issues[IssueIndex.Car_Power],
                        //      Value="350",
                        //      Score=0,
                        //      Deleted=false,
                        //      DeletedBy=CurrentUserID,
                        //      DeletedOn=DateTime.Now
                        //  }  

                        #endregion Purchase a Car For Me.

                            #region → Purchase Apartment 150 M        .


                            #endregion  Purchase a Apartment .

                            #region → Purchase a Laptop For Graphics  .


                            #endregion Purchase a Computer  .

                            #region → Purchase a Door X 70x200 Cm     .


                            #endregion Purchase a Computer  .

                    };
                }
                return mMessageIssues;
            }
        }


        public List<MessageLaterRatedIssue> MessageLaterRatedIssues
        {
            get
            {
                if (mMessageLaterRatedIssues == null)
                {
                    mMessageLaterRatedIssues = new List<MessageLaterRatedIssue>()
                    {

                            #region → Purchase a Car For My Family    .



                            #endregion Purchase a Car For Me.

                            #region → Purchase Apartment 150 M        .


                            #endregion  Purchase a Apartment .

                            #region → Purchase a Laptop For Graphics  .


                            #endregion Purchase a Computer  .

                            #region → Purchase a Door X 70x200 Cm     .


                            #endregion Purchase a Computer  .

                    };
                }
                return mMessageLaterRatedIssues;
            }
        }



        public List<MessageOptionIssue> MessageOptionIssues
        {
            get
            {
                if (mMessageOptionIssues == null)
                {
                    mMessageOptionIssues = new List<MessageOptionIssue>()
                    {

                            #region → Purchase a Car For My Family    .

                            #endregion Purchase a Car For Me.

                            #region → Purchase Apartment 150 M        .


                            #endregion  Purchase a Apartment .

                            #region → Purchase a Laptop For Graphics  .


                            #endregion Purchase a Computer  .

                            #region → Purchase a Door X 70x200 Cm     .


                            #endregion Purchase a Computer  .

                    };
                }
                return mMessageOptionIssues;
            }
        }





        #endregion

        #region → eNeg..Available Neg .

        /// <summary>
        /// Gets the available negotiations.
        /// </summary>
        /// <value>The available negotiations.</value>
        public List<Negotiation> AvailableNegotiations
        {
            get
            {
                if (mAvailableNegotiations == null)
                {
                    mAvailableNegotiations = new List<Negotiation>()
                    {
                     
                    #region → Purchase a Car For My Family    .
                         
 
                         //Purchase a New Car BMW
                         new Negotiation()
                           { 
                               NegotiationID=Guid.NewGuid(),
                               NegotiationName="Purchase a New Car BMW",
                               IsClosed=false
                                  
                           } 

                    #endregion Purchase a Car For Me.
                           
                    #region → Purchase Apartment 150 M        .
 
                    #endregion  Purchase a Apartment .
                         
                    #region → Purchase a Laptop For Graphics  .
                          
                       
                    #endregion Purchase a Computer  .
                         
                    #region → Purchase a Door 70x200 Cm       .
 

                    #endregion Purchase a Computer  .
                         
                    };
                }
                return mAvailableNegotiations;
            }
        }

        #endregion

        #region → User organizations  .

        /// <summary>
        /// Gets the organizations.
        /// </summary>
        /// <value>The organizations.</value>
        public List<Organization> Organizations
        {
            get
            {
                if (mOrganizations == null)
                {
                    mOrganizations = new List<Organization>();

                    mOrganizations.Add(new Organization()
                    {
                        OrganizationID = Guid.NewGuid(),
                        OrganizationName = "CitPoint",
                        IsSelected = true
                    });


                    mOrganizations.Add(new Organization()
                    {
                        OrganizationID = Guid.NewGuid(),
                        OrganizationName = "Google",
                        IsSelected = true
                    });

                }

                return mOrganizations;
            }
        }
        #endregion

        #region → Issue Statisticals  .

        /// <summary>
        /// Gets the issue statisticals.
        /// </summary>
        /// <value>The issue statisticals.</value>
        public List<IssueStatisticalsResult> IssueStatisticals
        {
            get
            {
                if (mIssueStatisticals == null)
                {
                    mIssueStatisticals = new List<IssueStatisticalsResult>();
                    mIssueStatisticals.Add(new IssueStatisticalsResult()
                    {
                        TimesUsed = 100,
                        IssueName = "Price",
                        Rank = 1
                    });

                    mIssueStatisticals.Add(new IssueStatisticalsResult()
                    {
                        TimesUsed = 80,
                        IssueName = "Color",
                        Rank = 2
                    });

                    mIssueStatisticals.Add(new IssueStatisticalsResult()
                    {
                        TimesUsed = 6,
                        IssueName = "Type",
                        Rank = 3
                    });
                }
                return mIssueStatisticals;
            }

        }

        #endregion

        #endregion Generate All Objects Mock

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

        /// <summary>
        /// Gets the base context.
        /// </summary>
        /// <value>The base context.</value>
        public PrefAppContext BaseContext
        {
            get
            {
                if (mContext == null)
                {
                    while (oldContext.Count() > 0)
                    {
                        Clear(oldContext.FirstOrDefault());
                        oldContext.Remove(oldContext.FirstOrDefault());
                    }

                    if (mContext == null)
                    {
                        mContext = new PrefAppContext(new Uri("http://localhost:9002/citPOINT-prefApp-Data-Web-PrefAppService.svc", UriKind.Absolute));
                    }

                    oldContext.Add(mContext);

                }


                return mContext;
            }

        }



        void Clear(PrefAppContext prefContext)
        {
            prefContext.RejectChanges();

            while (prefContext.MessageLaterRatedIssues.Count() > 0)
            {
                prefContext.RejectChanges();
                prefContext.MessageLaterRatedIssues.Remove(prefContext.MessageLaterRatedIssues.FirstOrDefault());
            }


            while (prefContext.MessageOptionIssues.Count() > 0)
            {
                prefContext.MessageOptionIssues.Remove(prefContext.MessageOptionIssues.FirstOrDefault());
            }

            while (prefContext.MessageIssues.Count() > 0)
            {
                prefContext.MessageIssues.Remove(prefContext.MessageIssues.FirstOrDefault());
            }




            while (prefContext.ConversationMessages.Count() > 0)
            {
                prefContext.ConversationMessages.Remove(prefContext.ConversationMessages.FirstOrDefault());
            }


            while (prefContext.NegConversations.Count() > 0)
            {
                prefContext.NegConversations.Remove(prefContext.NegConversations.FirstOrDefault());
            }

            while (prefContext.PreferenceSetNegs.Count() > 0)
            {
                prefContext.PreferenceSetNegs.Remove(prefContext.PreferenceSetNegs.FirstOrDefault());
            }


            while (prefContext.NumericIssues.Count() > 0)
            {
                prefContext.NumericIssues.Remove(prefContext.NumericIssues.FirstOrDefault());
            }

            while (prefContext.OptionIssues.Count() > 0)
            {
                prefContext.OptionIssues.Remove(prefContext.OptionIssues.FirstOrDefault());
            }

            while (prefContext.LaterRatedIssues.Count() > 0)
            {
                prefContext.LaterRatedIssues.Remove(prefContext.LaterRatedIssues.FirstOrDefault());
            }

            while (prefContext.Issues.Count() > 0)
            {
                prefContext.Issues.Remove(prefContext.Issues.FirstOrDefault());
            }

            while (prefContext.PreferenceSetNegs.Count() > 0)
            {
                prefContext.PreferenceSetNegs.Remove(prefContext.PreferenceSetNegs.FirstOrDefault());
            }

            while (prefContext.PreferenceSets.Count() > 0)
            {
                prefContext.PreferenceSets.Remove(prefContext.PreferenceSets.FirstOrDefault());
            }

            while (prefContext.MainPreferenceSets.Count() > 0)
            {
                prefContext.MainPreferenceSets.Remove(prefContext.MainPreferenceSets.FirstOrDefault());
            }

            prefContext.SubmitChanges();
        }

        #endregion Properties

        #region → Constructor    .

        private bool ReturnLastAvailableContext = false;
        /// <summary>
        /// Initializes a new instance of the <see cref="MockMaster"/> class.
        /// </summary>
        public MockMaster(bool ReturnLastAvailableContext = false)
        {
            if (!ReturnLastAvailableContext)
            {
                this.ReturnLastAvailableContext = ReturnLastAvailableContext;

                PrefAppConfigurations.CurrentLoginUser = new LoginUser();
                PrefAppConfigurations.CurrentLoginUser.UserID = new Guid("0a48aa0d-28d8-49bc-a486-bf3ce8cd85ec");

                this.ManageTablesRelations();

                #region Adding To Context

                foreach (var item in this.MainPreferenceSets)
                {
                    if (!this.BaseContext.MainPreferenceSets.Contains(item))
                    {
                        this.BaseContext.MainPreferenceSets.Add(item);
                    }
                }

                foreach (var item in this.PreferenceSets)
                {
                    if (!this.BaseContext.PreferenceSets.Contains(item))
                    {
                        this.BaseContext.PreferenceSets.Add(item);
                    }
                }

                foreach (var item in this.Issues)
                {
                    if (!this.BaseContext.Issues.Contains(item))
                    {
                        this.BaseContext.Issues.Add(item);
                    }
                }

                foreach (var item in this.NumericIssues)
                {
                    if (!this.BaseContext.NumericIssues.Contains(item))
                    {
                        this.BaseContext.NumericIssues.Add(item);
                    }
                }

                foreach (var item in this.OptionIssues)
                {
                    if (!this.BaseContext.OptionIssues.Contains(item))
                    {
                        this.BaseContext.OptionIssues.Add(item);
                    }
                }

                foreach (var item in this.LaterRatedIssues)
                {
                    if (!this.BaseContext.LaterRatedIssues.Contains(item))
                    {
                        this.BaseContext.LaterRatedIssues.Add(item);
                    }
                }



                foreach (var item in this.PreferenceSetNegs)
                {
                    if (!this.BaseContext.PreferenceSetNegs.Contains(item))
                    {
                        this.BaseContext.PreferenceSetNegs.Add(item);
                    }
                }

                foreach (var item in this.NegConversations)
                {
                    if (!this.BaseContext.NegConversations.Contains(item))
                    {
                        this.BaseContext.NegConversations.Add(item);
                    }
                }

                foreach (var item in this.ConversationMessages)
                {
                    if (!this.BaseContext.ConversationMessages.Contains(item))
                    {
                        this.BaseContext.ConversationMessages.Add(item);
                    }

                }


                foreach (var item in this.MessageIssues)
                {

                    if (!this.BaseContext.MessageIssues.Contains(item))
                    {
                        this.BaseContext.MessageIssues.Add(item);
                    }

                }

                foreach (var item in this.MessageOptionIssues)
                {
                    if (!this.BaseContext.MessageOptionIssues.Contains(item))
                    {
                        this.BaseContext.MessageOptionIssues.Add(item);
                    }

                }

                foreach (var item in this.MessageLaterRatedIssues)
                {

                    if (!this.BaseContext.MessageLaterRatedIssues.Contains(item))
                    {
                        this.BaseContext.MessageLaterRatedIssues.Add(item);
                    }

                }
                #endregion


                GeneralIndex.Car_Guid = this.PreferenceSetNegs[GeneralIndex.Car].PreferenceSetNegID;
                GeneralIndex.Apartment_Guid = this.PreferenceSetNegs[GeneralIndex.Apartment].PreferenceSetNegID;
                GeneralIndex.Computer_Guid = this.PreferenceSetNegs[GeneralIndex.Computer].PreferenceSetNegID;
                GeneralIndex.Door_Guid = this.PreferenceSetNegs[GeneralIndex.Door].PreferenceSetNegID;



                MessageIndex.Car_Msg_BMW_001_Guid = this.ConversationMessages[MessageIndex.Car_Msg_BMW_001].ConversationMessageID;
                MessageIndex.Car_Msg_BMW_002_Guid = this.ConversationMessages[MessageIndex.Car_Msg_BMW_002].ConversationMessageID;
                MessageIndex.Car_Msg_Fiat_003_Guid = this.ConversationMessages[MessageIndex.Car_Msg_Fiat_003].ConversationMessageID;




                ConversationIndex.Car_Conversation_BMW_Guid = this.NegConversations[ConversationIndex.Car_Conversation_BMW].NegConversationID;
                ConversationIndex.Car_Conversation_Fiat_Guid = this.NegConversations[ConversationIndex.Car_Conversation_Fiat].NegConversationID;





            }





        }

        #endregion

        #region → Methods        .

        #region → Private       .

        /// <summary>
        /// Manages the tables relations.
        /// </summary>
        private void ManageTablesRelations()
        {

            #region → IssueType                                         .

            foreach (var IssueTypeItem in this.IssueTypes)
            {
                //Issue
                foreach (var IssueItem in this.Issues.Where(ss => ss.IssueTypeID == IssueTypeItem.IssueTypeID))
                {
                    IssueTypeItem.Issues.Add(IssueItem);
                    IssueItem.IssueType = IssueTypeItem;
                }



            }

            #endregion

            #region → MainPreferenceSet                                 .

            foreach (var MainPreferenceSetItem in this.MainPreferenceSets)
            {
                //PreferenceSet
                foreach (var PreferenceSetItem in this.PreferenceSets.Where(ss => ss.MainPreferenceSetID == MainPreferenceSetItem.MainPreferenceSetID))
                {
                    MainPreferenceSetItem.PreferenceSets.Add(PreferenceSetItem);
                    PreferenceSetItem.MainPreferenceSet = MainPreferenceSetItem;
                }



            }

            #endregion

            #region → PreferenceSet                                     .

            foreach (var PreferenceSetItem in this.PreferenceSets)
            {
                //Issue
                foreach (var IssueItem in this.Issues.Where(ss => ss.PreferenceSetID == PreferenceSetItem.PreferenceSetID))
                {
                    PreferenceSetItem.Issues.Add(IssueItem);
                    IssueItem.PreferenceSet = PreferenceSetItem;
                }



                //PreferenceSetNeg
                foreach (var PreferenceSetNegItem in this.PreferenceSetNegs.Where(ss => ss.PreferenceSetID == PreferenceSetItem.PreferenceSetID))
                {
                    PreferenceSetItem.PreferenceSetNegs.Add(PreferenceSetNegItem);
                    PreferenceSetNegItem.PreferenceSet = PreferenceSetItem;
                }



                //*->1 ==> MainPreferenceSet
            }

            #endregion

            #region → Issue                                             .

            foreach (var IssueItem in this.Issues)
            {
                //LaterRatedIssue
                foreach (var LaterRatedIssueItem in this.LaterRatedIssues.Where(ss => ss.IssueID == IssueItem.IssueID))
                {
                    IssueItem.LaterRatedIssues.Add(LaterRatedIssueItem);
                    LaterRatedIssueItem.Issue = IssueItem;
                }



                //MessageIssue
                foreach (var MessageIssueItem in this.MessageIssues.Where(ss => ss.IssueID == IssueItem.IssueID))
                {
                    IssueItem.MessageIssues.Add(MessageIssueItem);
                    MessageIssueItem.Issue = IssueItem;
                }



                //NumericIssue
                foreach (var NumericIssueItem in this.NumericIssues.Where(ss => ss.IssueID == IssueItem.IssueID))
                {
                    IssueItem.NumericIssues.Add(NumericIssueItem);
                    NumericIssueItem.Issue = IssueItem;
                }



                //OptionIssue
                foreach (var OptionIssueItem in this.OptionIssues.Where(ss => ss.IssueID == IssueItem.IssueID))
                {
                    IssueItem.OptionIssues.Add(OptionIssueItem);
                    OptionIssueItem.Issue = IssueItem;
                }



                //*->1 ==> IssueType
                //*->1 ==> PreferenceSet
            }

            #endregion

            #region → PreferenceSetNeg                                  .

            foreach (var PreferenceSetNegItem in this.PreferenceSetNegs)
            {
                //NegConversation
                foreach (var NegConversationItem in this.NegConversations.Where(ss => ss.PreferenceSetNegID == PreferenceSetNegItem.PreferenceSetNegID))
                {
                    PreferenceSetNegItem.NegConversations.Add(NegConversationItem);
                    NegConversationItem.PreferenceSetNeg = PreferenceSetNegItem;
                }



                //*->1 ==> PreferenceSet
            }

            #endregion

            #region → LaterRatedIssue                                   .

            foreach (var LaterRatedIssueItem in this.LaterRatedIssues)
            {
                //MessageLaterRatedIssue
                foreach (var MessageLaterRatedIssueItem in this.MessageLaterRatedIssues.Where(ss => ss.LaterRatedIssueID == LaterRatedIssueItem.LaterRatedIssueID))
                {
                    LaterRatedIssueItem.MessageLaterRatedIssues.Add(MessageLaterRatedIssueItem);
                    MessageLaterRatedIssueItem.LaterRatedIssue = LaterRatedIssueItem;
                }



                //*->1 ==> Issue
            }

            #endregion

            #region → NegConversation                                   .

            foreach (var NegConversationItem in this.NegConversations)
            {
                //ConversationMessage
                foreach (var ConversationMessageItem in this.ConversationMessages.Where(ss => ss.NegConversationID == NegConversationItem.NegConversationID))
                {
                    NegConversationItem.ConversationMessages.Add(ConversationMessageItem);
                    ConversationMessageItem.NegConversation = NegConversationItem;
                }



                //*->1 ==> PreferenceSetNeg
            }

            #endregion

            #region → NumericIssue                                      .

            foreach (var NumericIssueItem in this.NumericIssues)
            {
                //*->1 ==> Issue
            }

            #endregion

            #region → OptionIssue                                       .

            foreach (var OptionIssueItem in this.OptionIssues)
            {
                //MessageOptionIssue
                foreach (var MessageOptionIssueItem in this.MessageOptionIssues.Where(ss => ss.OptionIssueID == OptionIssueItem.OptionIssueID))
                {
                    OptionIssueItem.MessageOptionIssues.Add(MessageOptionIssueItem);
                    MessageOptionIssueItem.OptionIssue = OptionIssueItem;
                }



                //*->1 ==> Issue
            }

            #endregion

            #region → ConversationMessage                               .

            foreach (var ConversationMessageItem in this.ConversationMessages)
            {
                //MessageIssue
                foreach (var MessageIssueItem in this.MessageIssues.Where(ss => ss.ConversationMessageID == ConversationMessageItem.ConversationMessageID))
                {
                    ConversationMessageItem.MessageIssues.Add(MessageIssueItem);
                    MessageIssueItem.ConversationMessage = ConversationMessageItem;
                }



                //*->1 ==> NegConversation
            }

            #endregion

            #region → MessageIssue                                      .

            foreach (var MessageIssueItem in this.MessageIssues)
            {
                //MessageLaterRatedIssue
                foreach (var MessageLaterRatedIssueItem in this.MessageLaterRatedIssues.Where(ss => ss.MessageIssueID == MessageIssueItem.MessageIssueID))
                {
                    MessageIssueItem.MessageLaterRatedIssues.Add(MessageLaterRatedIssueItem);
                    MessageLaterRatedIssueItem.MessageIssue = MessageIssueItem;
                }



                //MessageOptionIssue
                foreach (var MessageOptionIssueItem in this.MessageOptionIssues.Where(ss => ss.MessageIssueID == MessageIssueItem.MessageIssueID))
                {
                    MessageIssueItem.MessageOptionIssues.Add(MessageOptionIssueItem);
                    MessageOptionIssueItem.MessageIssue = MessageIssueItem;
                }



                //*->1 ==> ConversationMessage
                //*->1 ==> Issue
            }

            #endregion

            #region → MessageLaterRatedIssue                            .

            foreach (var MessageLaterRatedIssueItem in this.MessageLaterRatedIssues)
            {
                //*->1 ==> LaterRatedIssue
                //*->1 ==> MessageIssue
            }

            #endregion

            #region → MessageOptionIssue                                .

            foreach (var MessageOptionIssueItem in this.MessageOptionIssues)
            {
                //*->1 ==> MessageIssue
                //*->1 ==> OptionIssue
            }

            #endregion

        }

        #endregion

        #endregion
    }
}

