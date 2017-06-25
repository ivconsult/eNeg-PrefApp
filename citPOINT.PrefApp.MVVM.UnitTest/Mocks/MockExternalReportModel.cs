#region → Usings   .
using System;
using System.ComponentModel;
using System.ServiceModel.DomainServices.Client;
using citPOINT.PrefApp.Common;
using citPOINT.PrefApp.Data.Web;
using System.Windows;
using System.Collections.ObjectModel;
using System.Linq;
using citPOINT.eNeg.Common;
using System.Collections.Generic;
#endregion

#region → History  .

/* Date         User              Change
 * 
 * 18.07.11     Yousra Reda       Creation
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
    /// Mock External Report ViewModel
    /// </summary>
    public class MockExternalReportModel : MockMaster, IExternalReportModel
    {
        #region → Fields         .

        private PrefAppContext mContext;

        #region 1111111111111
        //private List<ConversationMessage> mConversationMessages;
        //private List<PreferenceSetNeg> mPreferenceSetNegs;
        //private List<NegConversation> mNegConversations;
        //private List<PreferenceSet> mPreferenceSets;
        //private List<MainPreferenceSet> mMainPreferenceSets;
        //private List<Negotiation> mNegotiations;
        //private List<Message> mMessages;
        //private List<Conversation> mConversations;

        ///// <summary>
        ///// General Index
        ///// </summary>
        //public class GeneralIndex
        //{
        //    public static int Car = 0;
        //    public static int Apartment = 1;
        //    public static int Computer = 2;
        //    public static int Door = 3;
        //}

        ///// <summary>
        ///// PreferenceSet Index
        ///// </summary>
        //public class PreferenceSetIndex : GeneralIndex
        //{ }

        ///// <summary>
        ///// Negotiation Index
        ///// </summary>
        //public class NegotiationIndex : GeneralIndex
        //{ }

        ///// <summary>
        ///// Index for conversations
        ///// </summary>
        //public class ConversationIndex
        //{
        //    public static int Car_Conversation_BMW = 0;
        //    public static int Car_Conversation_Fiat = 1;
        //}

        ///// <summary>
        ///// Index for messages
        ///// </summary>
        //public class MessageIndex
        //{
        //    public static int Car_Msg_BMW_001 = 0;
        //    public static int Car_Msg_BMW_002 = 1;
        //    public static int Car_Msg_Fiat_003 = 2;
        //}
        #endregion

        #endregion

        #region → Properties     .

        #region → Generate all Mocks Objects   .

        //#region → User                .

        ///// <summary>
        ///// Gets the current user ID.
        ///// </summary>
        ///// <value>The current user ID.</value>
        //public Guid CurrentUserID
        //{
        //    get
        //    {

        //        return new Guid("0a48aa0d-28d8-49bc-a486-bf3ce8cd85ec");
        //    }
        //}

        //#endregion

        //#region → Main Preference Set .

        ///// <summary>
        ///// Get a List Of Main Preference Set
        ///// </summary>
        ///// <value>The main preference sets.</value>
        //public List<MainPreferenceSet> MainPreferenceSets
        //{
        //    get
        //    {
        //        if (mMainPreferenceSets == null)
        //        {


        //            mMainPreferenceSets = new List<MainPreferenceSet>()
        //            {
        //                new MainPreferenceSet ()
        //                   { 
        //                        MainPreferenceSetID=Common.PrefAppConstant.MainPreferenceSets.MySets,
        //                        MainPreferenceSetName="My Sets"
        //                   },

        //                new MainPreferenceSet ()
        //                   { 
        //                        MainPreferenceSetID=Guid.NewGuid(),
        //                        MainPreferenceSetName="Organization Sets"
        //                   },

        //                new MainPreferenceSet ()
        //                   { 
        //                        MainPreferenceSetID=Guid.NewGuid(),
        //                        MainPreferenceSetName="Set Store"
        //                   }
        //            };
        //        }
        //        return mMainPreferenceSets;
        //    }
        //}

        //#endregion Main Preference Set

        //#region → Preference Set      .

        ///// <summary>
        ///// Get a List Of Preference Set
        ///// </summary>
        ///// <value>The preference sets.</value>
        //public List<PreferenceSet> PreferenceSets
        //{
        //    get
        //    {
        //        if (mPreferenceSets == null)
        //        {

        //            mPreferenceSets = new List<PreferenceSet>()
        //            {
        //               new PreferenceSet ()
        //                   { 
        //                        PreferenceSetID=Guid.NewGuid(),
        //                        PreferenceSetName="Purchase a Car",
        //                        MainPreferenceSetID=this.MainPreferenceSets[0].MainPreferenceSetID,
        //                        MainPreferenceSet=this.MainPreferenceSets[0],
        //                        UserID=CurrentUserID,

        //                        Deleted=false,
        //                        DeletedBy=CurrentUserID,
        //                        DeletedOn=DateTime.Now
        //                   },

        //               new PreferenceSet ()
        //                   { 
        //                        PreferenceSetID=Guid.NewGuid(),
        //                        PreferenceSetName="Purchase apartment",
        //                        MainPreferenceSetID=this.MainPreferenceSets[0].MainPreferenceSetID,
        //                        MainPreferenceSet=this.MainPreferenceSets[0],
        //                        UserID=CurrentUserID,

        //                        Deleted=false,
        //                        DeletedBy=CurrentUserID,
        //                        DeletedOn=DateTime.Now
        //                   },

        //               new PreferenceSet ()
        //                   {  
        //                        PreferenceSetID=Guid.NewGuid(),
        //                        PreferenceSetName="Purchase Computer",
        //                        MainPreferenceSetID=this.MainPreferenceSets[0].MainPreferenceSetID,
        //                        MainPreferenceSet=this.MainPreferenceSets[0],
        //                        UserID=CurrentUserID,

        //                        Deleted=false,
        //                        DeletedBy=CurrentUserID,
        //                        DeletedOn=DateTime.Now
        //                   },

        //               new PreferenceSet ()
        //                   { 
        //                        PreferenceSetID=Guid.NewGuid(),
        //                        PreferenceSetName="Purchase Door",
        //                        MainPreferenceSetID=this.MainPreferenceSets[0].MainPreferenceSetID,
        //                        MainPreferenceSet=this.MainPreferenceSets[0],
        //                        UserID=CurrentUserID,

        //                        Deleted=false,
        //                        DeletedBy=CurrentUserID,
        //                        DeletedOn=DateTime.Now
        //                   } 
        //            };
        //        }
        //        return mPreferenceSets;
        //    }
        //}

        //#endregion Preference Set

        //#region → eNeg..Negotiations  .

        ///// <summary>
        ///// Gets the negotiations.
        ///// </summary>
        ///// <value>The negotiations.</value>
        //public List<Negotiation> Negotiations
        //{
        //    get
        //    {
        //        if (mNegotiations == null)
        //        {
        //            mNegotiations = new List<Negotiation>()
        //            {

        //            #region → Purchase a Car For My Family    .                        

        //                 //Purchase a Car For My Family
        //                 new Negotiation()
        //                   { 
        //                       NegotiationID=Guid.NewGuid(),
        //                       NegotiationName="Purchase a Car For My Family",
        //                       IsClosed=false

        //                   } ,
        //            #endregion Purchase a Car For Me.

        //            #region → Purchase Apartment 150 M        .

        //                 //Purchase a Apartment 150 M
        //                 new Negotiation()
        //                   { 
        //                       NegotiationID=Guid.NewGuid(),
        //                       NegotiationName="Purchase Apartment 150 M",
        //                       IsClosed=false                                  
        //                   } ,

        //            #endregion  Purchase a Apartment .

        //            #region → Purchase a Laptop For Graphics  .                         

        //                 //"Purchase a Laptop For Graphics"
        //                 new Negotiation()
        //                   { 
        //                       NegotiationID=Guid.NewGuid(),
        //                       NegotiationName="Purchase a Laptop For Graphics",
        //                       IsClosed=false
        //                   } ,                       
        //            #endregion Purchase a Computer  .

        //            #region → Purchase a Door 70x200 Cm       .

        //                 //"Purchase a Door 70x200 Cm"
        //                  new Negotiation()
        //                   { 
        //                       NegotiationID=Guid.NewGuid(),
        //                       NegotiationName="Purchase a Door 70x200 Cm",
        //                       IsClosed=false                                  
        //                   }  
        //            #endregion Purchase a Computer  .

        //            };
        //        }
        //        return mNegotiations;
        //    }
        //}

        //#endregion

        //#region → eNeg..Conversation  .

        ///// <summary>
        ///// Gets the conversations.
        ///// </summary>
        ///// <value>The conversations.</value>
        //public List<Conversation> Conversations
        //{
        //    get
        //    {
        //        if (mConversations == null)
        //        {
        //            mConversations = new List<Conversation>()
        //            {

        //            #region → Purchase a Car For My Family    .                         

        //                 //BMW
        //                 new Conversation()
        //                   { 
        //                      ConversationID=Guid.NewGuid(),
        //                      ConversationName="Conversation BMW",
        //                      NegotiationID=this.Negotiations[NegotiationIndex.Car].NegotiationID
        //                   } ,

        //                //Fiat
        //                 new Conversation()
        //                   { 
        //                      ConversationID=Guid.NewGuid(),
        //                      ConversationName="Conversation Fiat",
        //                      NegotiationID=this.Negotiations[NegotiationIndex.Car].NegotiationID
        //                   }  

        //            #endregion Purchase a Car For Me.                         
        //            };
        //        }
        //        return mConversations;
        //    }
        //}

        //#endregion

        //#region → eNeg..Messages      .

        ///// <summary>
        ///// Gets the messages.
        ///// </summary>
        ///// <value>The messages.</value>
        //public List<Message> Messages
        //{
        //    get
        //    {
        //        if (mMessages == null)
        //        {
        //            mMessages = new List<Message>()
        //            {

        //            #region → Purchase a Car For My Family    .


        //                 //BMW 001
        //                 new Message()
        //                   { 

        //                      MessageContent="We Have a car its price is 10,000 EUR and its Colour is blackk",
        //                      ConversationID=this.Conversations[ConversationIndex.Car_Conversation_BMW].ConversationID,
        //                      IsSent=false,
        //                      MessageDate=DateTime.Now,
        //                      MessageID=Guid.NewGuid(),
        //                      MessageName="New Message"                               ,
        //                      MessageReceiver="<Point@point.com>",
        //                      MessageSender="Model@Model.com",
        //                      MessageSubject="New Message",
        //                   } ,


        //                //BMW 002
        //                 new Message()
        //                   { 

        //                      MessageContent="I need a car that may be Black and price under 15,000 EUR. thanks",
        //                      ConversationID=this.Conversations[ConversationIndex.Car_Conversation_BMW].ConversationID,
        //                      IsSent=true,
        //                      MessageDate=DateTime.Now,
        //                      MessageID=Guid.NewGuid(),
        //                      MessageName="Top manager"                               ,
        //                      MessageReceiver="<Point@point.com>",
        //                      MessageSender="Model@Model.com",
        //                      MessageSubject="Need a car",
        //                   } ,


        //                //Fiat 001
        //                 new Message()
        //                   { 

        //                      MessageContent="I need a car that may be Yellow and price under 5,000 EUR.Model 1978 thanks",
        //                      ConversationID=this.Conversations[ConversationIndex.Car_Conversation_Fiat].ConversationID,
        //                      IsSent=true,
        //                      MessageDate=DateTime.Now,
        //                      MessageID=Guid.NewGuid(),
        //                      MessageName="Top manager"                               ,
        //                      MessageReceiver="<Point@point.com>",
        //                      MessageSender="Model@Model.com",
        //                      MessageSubject="Need a car",
        //                   } 


        //            #endregion Purchase a Car For Me.        
        //            };
        //        }
        //        return mMessages;
        //    }
        //}

        //#endregion

        //#region → Preference Set Neg  .

        ///// <summary>
        ///// Gets the preference set negs.
        ///// </summary>
        ///// <value>The preference set negs.</value>
        //public List<PreferenceSetNeg> PreferenceSetNegs
        //{
        //    get
        //    {
        //        if (mPreferenceSetNegs == null)
        //        {
        //            mPreferenceSetNegs = new List<PreferenceSetNeg>()
        //            {

        //            #region → Purchase a Car For My Family    .

        //                  //Purchase a Car For My Family
        //                 new PreferenceSetNeg()
        //                   {                               
        //                       PreferenceSetNegID=Guid.NewGuid(),                                                             

        //                       Percentage=0,
        //                       PreferenceSet=this.PreferenceSets[PreferenceSetIndex.Car],
        //                       PreferenceSetID=this.PreferenceSets[PreferenceSetIndex.Car].PreferenceSetID,

        //                       #region Form eNeg Negotiation.

        //                               NegID=this.Negotiations[NegotiationIndex.Car].NegotiationID,
        //                               IsClosed=this.Negotiations[NegotiationIndex.Car].IsClosed,
        //                               //"Purchase a Car For My Family"
        //                               Name=this.Negotiations[NegotiationIndex.Car].NegotiationName,

        //                       #endregion

        //                       Deleted=false,
        //                       DeletedBy=CurrentUserID,
        //                       DeletedOn=DateTime.Now
        //                   } ,
        //            #endregion Purchase a Car For Me.

        //            #region → Purchase Apartment 150 M        .                         

        //                 //"Purchase a Apartment 150 M"
        //                 new PreferenceSetNeg()
        //                   {
        //                       PreferenceSetNegID=Guid.NewGuid(), 

        //                       Percentage=0,
        //                       PreferenceSet=this.PreferenceSets[PreferenceSetIndex.Apartment],
        //                       PreferenceSetID=this.PreferenceSets[PreferenceSetIndex.Apartment].PreferenceSetID,

        //                       #region Form eNeg Negotiation.

        //                               NegID=this.Negotiations[NegotiationIndex.Apartment].NegotiationID,
        //                               IsClosed=this.Negotiations[NegotiationIndex.Apartment].IsClosed,
        //                               //Purchase Apartment 150 M
        //                               Name=this.Negotiations[NegotiationIndex.Apartment].NegotiationName,

        //                       #endregion

        //                       Deleted=false,
        //                       DeletedBy=CurrentUserID,
        //                       DeletedOn=DateTime.Now
        //                   } ,
        //            #endregion  Purchase a Apartment .

        //            #region → Purchase a Laptop For Graphics  .

        //                 //"Purchase a Laptop For Graphics"
        //                 new PreferenceSetNeg()
        //                   {
        //                       PreferenceSetNegID=Guid.NewGuid(),

        //                       Percentage=0,
        //                       PreferenceSet=this.PreferenceSets[PreferenceSetIndex.Computer],
        //                       PreferenceSetID=this.PreferenceSets[PreferenceSetIndex.Computer].PreferenceSetID,

        //                       #region Form eNeg Negotiation.

        //                               NegID=this.Negotiations[NegotiationIndex.Computer].NegotiationID,
        //                               IsClosed=this.Negotiations[NegotiationIndex.Computer].IsClosed,
        //                               //"Purchase a Laptop For Graphics
        //                               Name=this.Negotiations[NegotiationIndex.Computer].NegotiationName,

        //                       #endregion

        //                       Deleted=false,
        //                       DeletedBy=CurrentUserID,
        //                       DeletedOn=DateTime.Now
        //                   } ,
        //            #endregion Purchase a Computer  .

        //            #region → Purchase a Door 70x200 Cm       .

        //                 //"Purchase a Door 70x200 Cm"
        //                 new PreferenceSetNeg()
        //                   {
        //                       PreferenceSetNegID=Guid.NewGuid(),

        //                       Percentage=0,
        //                       PreferenceSet=this.PreferenceSets[PreferenceSetIndex.Door],
        //                       PreferenceSetID=this.PreferenceSets[PreferenceSetIndex.Door].PreferenceSetID,

        //                       #region Form eNeg Negotiation.

        //                               NegID=this.Negotiations[NegotiationIndex.Computer].NegotiationID,
        //                               IsClosed=this.Negotiations[NegotiationIndex.Computer].IsClosed,
        //                               //Purchase a Door 70x200 Cm
        //                               Name=this.Negotiations[NegotiationIndex.Computer].NegotiationName,

        //                       #endregion

        //                       Deleted=false,
        //                       DeletedBy=CurrentUserID,
        //                       DeletedOn=DateTime.Now
        //                   } 
        //            #endregion Purchase a Computer  .

        //            };
        //        }
        //        return mPreferenceSetNegs;
        //    }
        //}
        //#endregion

        //#region → Neg. Conversations  .
        ///// <summary>
        ///// Gets the preference set negs.
        ///// </summary>
        ///// <value>The preference set negs.</value>
        //public List<NegConversation> NegConversations
        //{
        //    get
        //    {
        //        if (mNegConversations == null)
        //        {
        //            mNegConversations = new List<NegConversation>()
        //            {

        //            #region → Purchase a Car For My Family    .

        //                 //Conversation BMW
        //                 new NegConversation()
        //                   {

        //                       NegConversationID=Guid.NewGuid(),

        //                       Percentage=0,
        //                       PreferenceSetNeg=this.PreferenceSetNegs[PreferenceSetIndex.Car],
        //                       PreferenceSetNegID=this.PreferenceSetNegs[PreferenceSetIndex.Car].PreferenceSetNegID,
        //                       ConversationID=this.Conversations[ConversationIndex.Car_Conversation_BMW].ConversationID,
        //                       Name=this.Conversations[ConversationIndex.Car_Conversation_BMW].ConversationName,

        //                       Deleted=false,
        //                       DeletedBy=CurrentUserID,
        //                       DeletedOn=DateTime.Now
        //                   } ,

        //                 // Car_Conversation_Fiat
        //                 new NegConversation()
        //                   {

        //                       NegConversationID=Guid.NewGuid(),

        //                       Percentage=0,
        //                       PreferenceSetNeg=this.PreferenceSetNegs[PreferenceSetIndex.Car],
        //                       PreferenceSetNegID=this.PreferenceSetNegs[PreferenceSetIndex.Car].PreferenceSetNegID,
        //                       ConversationID=this.Conversations[ConversationIndex.Car_Conversation_Fiat].ConversationID,
        //                       Name=this.Conversations[ConversationIndex.Car_Conversation_Fiat].ConversationName,

        //                       Deleted=false,
        //                       DeletedBy=CurrentUserID,
        //                       DeletedOn=DateTime.Now
        //                   }  
        //            #endregion Purchase a Car For Me.

        //            };
        //        }
        //        return mNegConversations;
        //    }
        //}

        //#endregion

        //#region → Conversation Message.

        ///// <summary>
        ///// Gets the conversation messages.
        ///// </summary>
        ///// <value>The conversation messages.</value>
        //public List<ConversationMessage> ConversationMessages
        //{
        //    get
        //    {
        //        if (mConversationMessages == null)
        //        {
        //            mConversationMessages = new List<ConversationMessage>()
        //            {

        //            #region → Purchase a Car For My Family    .


        //                 //Car_Msg_BMW_001
        //                 new ConversationMessage()
        //                   {
        //                       ConversationMessageID=Guid.NewGuid(),
        //                       NegConversationID = this.NegConversations[ConversationIndex.Car_Conversation_BMW].NegConversationID ,
        //                       NegConversation= this.NegConversations[ConversationIndex.Car_Conversation_BMW] ,
        //                       MessageID= this.Messages[MessageIndex.Car_Msg_BMW_001].MessageID ,


        //                       Deleted=false,
        //                       DeletedBy=CurrentUserID,
        //                       DeletedOn=DateTime.Now
        //                   } ,


        //                 //Car_Msg_BMW_002
        //                 new ConversationMessage()
        //                   {
        //                       ConversationMessageID=Guid.NewGuid(),
        //                       NegConversationID = this.NegConversations[ConversationIndex.Car_Conversation_BMW].NegConversationID ,
        //                       NegConversation= this.NegConversations[ConversationIndex.Car_Conversation_BMW] ,
        //                       MessageID= this.Messages[MessageIndex.Car_Msg_BMW_002].MessageID ,


        //                       Deleted=false,
        //                       DeletedBy=CurrentUserID,
        //                       DeletedOn=DateTime.Now
        //                   } ,


        //                 //Car_Msg_Fiat_003
        //                 new ConversationMessage()
        //                   {
        //                       ConversationMessageID=Guid.NewGuid(),
        //                       NegConversationID = this.NegConversations[ConversationIndex.Car_Conversation_BMW].NegConversationID ,
        //                       NegConversation= this.NegConversations[ConversationIndex.Car_Conversation_BMW] ,
        //                       MessageID= this.Messages[MessageIndex.Car_Msg_Fiat_003].MessageID ,


        //                       Deleted=false,
        //                       DeletedBy=CurrentUserID,
        //                       DeletedOn=DateTime.Now
        //                   }  

        //            #endregion Purchase a Car For Me.

        //            #region → Purchase Apartment 150 M        .


        //            #endregion  Purchase a Apartment .

        //            #region → Purchase a Laptop For Graphics  .


        //            #endregion Purchase a Computer  .

        //            #region → Purchase a Door X 70x200 Cm     .


        //            #endregion Purchase a Computer  .

        //            };
        //        }
        //        return mConversationMessages;
        //    }
        //}

        //#endregion
        #endregion

        /// <summary>
        /// True if Domain context Has Changes ;otherwise false
        /// </summary>
        /// <value></value>
        public bool HasChanges
        {
            get { return false; }
        }

        /// <summary>
        /// True if either "IsLoading" or "IsSubmitting" is
        /// in progress; otherwise, false
        /// </summary>
        /// <value></value>
        public bool IsBusy
        {
            get { return false; }
        }

        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>The context.</value>
        public PrefAppContext Context
        {
            get
            {
                if (mContext == null)
                {
                    mContext = new PrefAppContext(new Uri("http://localhost:9002/Services/citPOINT-PrefApp-Data-Web-PrefAppService.svc", UriKind.Absolute));
                }
                return mContext;
            }
        }
        #endregion

        #region → Events         .

        /// <summary>
        /// PropertyChanged Callback
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Occurs when [get last sent message complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<ConversationMessage>> GetLastSentMessageComplete;

        /// <summary>
        /// Occurs when [get last received message complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<ConversationMessage>> GetLastReceivedMessageComplete;

        /// <summary>
        /// Occurs when [get preference set neg complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<PreferenceSetNeg>> GetPreferenceSetNegComplete;

        /// <summary>
        /// Occurs when [get issues complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<Issue>> GetIssuesComplete;

        /// <summary>
        /// Occurs when [get option issues complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<OptionIssue>> GetOptionIssuesComplete;

        /// <summary>
        /// Occurs when [get numeric issues complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<NumericIssue>> GetNumericIssuesComplete;

        /// <summary>
        /// Occurs when [get later rated issue complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<LaterRatedIssue>> GetLaterRatedIssueComplete;

        /// <summary>
        /// Occurs when [get message issues by message ID complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<MessageIssue>> GetMessageIssuesByMessageIDComplete;

        /// <summary>
        /// Occurs when [get message option issue by message ID complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<MessageOptionIssue>> GetMessageOptionIssueByMessageIDComplete;

        /// <summary>
        /// Occurs when [get message later rated issue by message ID complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<MessageLaterRatedIssue>> GetMessageLaterRatedIssueByMessageIDComplete;
        #endregion

        #region → Methods        .

        #region → Public         .

        /// <summary>
        /// Gets the last sent message async.
        /// </summary>
        public void GetLastSentMessageAsync()
        {
            if (GetLastSentMessageComplete != null)
            {
                GetLastSentMessageComplete(this, new eNegEntityResultArgs<ConversationMessage>(ConversationMessages));
            }
        }

        /// <summary>
        /// Gets the last received message async.
        /// </summary>
        public void GetLastReceivedMessageAsync()
        {
            if (GetLastReceivedMessageComplete != null)
            {
                GetLastReceivedMessageComplete(this, new eNegEntityResultArgs<ConversationMessage>(ConversationMessages));
            }
        }

        /// <summary>
        /// Gets the preference set neg async.
        /// </summary>
        public void GetPreferenceSetNegAsync()
        {
            if (GetPreferenceSetNegComplete != null)
            {
                GetPreferenceSetNegComplete(this, new eNegEntityResultArgs<PreferenceSetNeg>(PreferenceSetNegs));
            }
        }

        /// <summary>
        /// Gets the issues by preference ID async.
        /// </summary>
        /// <param name="PreferenceID">The preference ID.</param>
        public void GetIssuesByPreferenceIDAsync(Guid PreferenceID)
        {
            if (GetIssuesComplete != null)
            {
                GetIssuesComplete(this, new eNegEntityResultArgs<Issue>(Issues));
            }
        }

        /// <summary>
        /// Gets the option issues by preference ID async.
        /// </summary>
        /// <param name="PreferenceID">The preference ID.</param>
        public void GetOptionIssuesByPreferenceIDAsync(Guid PreferenceID)
        {
            if (GetOptionIssuesComplete != null)
            {
                GetOptionIssuesComplete(this, new eNegEntityResultArgs<OptionIssue>(OptionIssues));
            }
        }

        /// <summary>
        /// Gets the numeric issues by preference ID async.
        /// </summary>
        /// <param name="PreferenceID">The preference ID.</param>
        public void GetNumericIssuesByPreferenceIDAsync(Guid PreferenceID)
        {
            if (GetNumericIssuesComplete != null)
            {
                GetNumericIssuesComplete(this, new eNegEntityResultArgs<NumericIssue>(NumericIssues));
            }
        }

        /// <summary>
        /// Gets the later rated by preference ID async.
        /// </summary>
        /// <param name="PreferenceID">The preference ID.</param>
        public void GetLaterRatedByPreferenceIDAsync(Guid PreferenceID)
        {
            if (GetLaterRatedIssueComplete != null)
            {
                GetLaterRatedIssueComplete(this, new eNegEntityResultArgs<LaterRatedIssue>(LaterRatedIssues));
            }
        }

        /// <summary>
        /// Gets the message issues by message ID async.
        /// </summary>
        /// <param name="MessageID">The message ID.</param>
        public void GetMessageIssuesByMessageIDAsync(Guid MessageID)
        {
            if (GetMessageIssuesByMessageIDComplete != null)
            {
                GetMessageIssuesByMessageIDComplete(this, new eNegEntityResultArgs<MessageIssue>(MessageIssues));
            }
        }

        /// <summary>
        /// Gets the message option issue by message ID async.
        /// </summary>
        /// <param name="MessageID">The message ID.</param>
        public void GetMessageOptionIssueByMessageIDAsync(Guid MessageID)
        {
            if (GetMessageOptionIssueByMessageIDComplete != null)
            {
                GetMessageOptionIssueByMessageIDComplete(this, new eNegEntityResultArgs<MessageOptionIssue>(MessageOptionIssues));
            }
        }

        /// <summary>
        /// Gets the message later rated issue by message ID async.
        /// </summary>
        /// <param name="MessageID">The message ID.</param>
        public void GetMessageLaterRatedIssueByMessageIDAsync(Guid MessageID)
        {
            if (GetMessageLaterRatedIssueByMessageIDComplete != null)
            {
                GetMessageLaterRatedIssueByMessageIDComplete(this, new eNegEntityResultArgs<MessageLaterRatedIssue>(MessageLaterRatedIssues));
            }
        }

        #endregion

        #endregion
    }
}
