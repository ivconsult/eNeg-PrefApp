
namespace citPOINT.PrefApp.Data.Web
{
    using System.Data;
    using System.Linq;
    using System.ServiceModel.DomainServices.EntityFramework;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;
    using System.ServiceModel;
    using System;


    // Implements application logic using the PrefAppEntities context.
    // Also consider adding roles to restrict access as appropriate.
    // [RequiresAuthentication]
    [EnableClientAccess()]
    public partial class PrefAppService : LinqToEntitiesDomainService<PrefAppEntities>
    {

        
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'ActionTypes' query.
        [Query(IsDefault = true)]
        public IQueryable<ActionType> GetActionTypes()
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.ActionTypes;
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        public void InsertActionType(ActionType actionType)
        {
            if ((actionType.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(actionType, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ActionTypes.AddObject(actionType);
            }
        }

        public void UpdateActionType(ActionType currentActionType)
        {
            this.ObjectContext.ActionTypes.AttachAsModified(currentActionType, this.ChangeSet.GetOriginal(currentActionType));
        }

        public void DeleteActionType(ActionType actionType)
        {
            if ((actionType.EntityState == EntityState.Detached))
            {
                this.ObjectContext.ActionTypes.Attach(actionType);
            }
            this.ObjectContext.ActionTypes.DeleteObject(actionType);
        }

        
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'ConversationMessages' query.
        [Query(IsDefault = true)]
        public IQueryable<ConversationMessage> GetConversationMessages()
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.ConversationMessages;
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        public void InsertConversationMessage(ConversationMessage conversationMessage)
        {
            if ((conversationMessage.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(conversationMessage, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ConversationMessages.AddObject(conversationMessage);
            }
        }

        public void UpdateConversationMessage(ConversationMessage currentConversationMessage)
        {
            this.ObjectContext.ConversationMessages.AttachAsModified(currentConversationMessage, this.ChangeSet.GetOriginal(currentConversationMessage));
        }

        public void DeleteConversationMessage(ConversationMessage conversationMessage)
        {
            if ((conversationMessage.EntityState == EntityState.Detached))
            {
                this.ObjectContext.ConversationMessages.Attach(conversationMessage);
            }
            this.ObjectContext.ConversationMessages.DeleteObject(conversationMessage);
        }

        
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Histories' query.
        [Query(IsDefault = true)]
        public IQueryable<History> GetHistories()
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.Histories;
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        public void InsertHistory(History history)
        {
            if ((history.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(history, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Histories.AddObject(history);
            }
        }

        public void UpdateHistory(History currentHistory)
        {
            this.ObjectContext.Histories.AttachAsModified(currentHistory, this.ChangeSet.GetOriginal(currentHistory));
        }

        public void DeleteHistory(History history)
        {
            if ((history.EntityState == EntityState.Detached))
            {
                this.ObjectContext.Histories.Attach(history);
            }
            this.ObjectContext.Histories.DeleteObject(history);
        }

        
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Issues' query.
        [Query(IsDefault = true)]
        public IQueryable<Issue> GetIssues()
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.Issues;
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        public void InsertIssue(Issue issue)
        {
            if ((issue.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(issue, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Issues.AddObject(issue);
            }
        }

        public void UpdateIssue(Issue currentIssue)
        {
            this.ObjectContext.Issues.AttachAsModified(currentIssue, this.ChangeSet.GetOriginal(currentIssue));
        }

        public void DeleteIssue(Issue issue)
        {
            if ((issue.EntityState == EntityState.Detached))
            {
                this.ObjectContext.Issues.Attach(issue);
            }
            this.ObjectContext.Issues.DeleteObject(issue);
        }

        
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'IssueTypes' query.
        [Query(IsDefault = true)]
        public IQueryable<IssueType> GetIssueTypes()
        {
            if (ServiceAuthentication.IsValid())
            {

                return this.ObjectContext.IssueTypes;
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        public void InsertIssueType(IssueType issueType)
        {
            if ((issueType.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(issueType, EntityState.Added);
            }
            else
            {
                this.ObjectContext.IssueTypes.AddObject(issueType);
            }
        }

        public void UpdateIssueType(IssueType currentIssueType)
        {
            this.ObjectContext.IssueTypes.AttachAsModified(currentIssueType, this.ChangeSet.GetOriginal(currentIssueType));
        }

        public void DeleteIssueType(IssueType issueType)
        {
            if ((issueType.EntityState == EntityState.Detached))
            {
                this.ObjectContext.IssueTypes.Attach(issueType);
            }
            this.ObjectContext.IssueTypes.DeleteObject(issueType);
        }

        
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'LaterRatedIssues' query.
        [Query(IsDefault = true)]
        public IQueryable<LaterRatedIssue> GetLaterRatedIssues()
        {
            if (ServiceAuthentication.IsValid())
            {

                return this.ObjectContext.LaterRatedIssues;
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        public void InsertLaterRatedIssue(LaterRatedIssue laterRatedIssue)
        {
            if ((laterRatedIssue.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(laterRatedIssue, EntityState.Added);
            }
            else
            {
                this.ObjectContext.LaterRatedIssues.AddObject(laterRatedIssue);
            }
        }

        public void UpdateLaterRatedIssue(LaterRatedIssue currentLaterRatedIssue)
        {
            this.ObjectContext.LaterRatedIssues.AttachAsModified(currentLaterRatedIssue, this.ChangeSet.GetOriginal(currentLaterRatedIssue));
        }

        public void DeleteLaterRatedIssue(LaterRatedIssue laterRatedIssue)
        {
            if ((laterRatedIssue.EntityState == EntityState.Detached))
            {
                this.ObjectContext.LaterRatedIssues.Attach(laterRatedIssue);
            }
            this.ObjectContext.LaterRatedIssues.DeleteObject(laterRatedIssue);
        }

        
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'MainPreferenceSets' query.
        [Query(IsDefault = true)]
        public IQueryable<MainPreferenceSet> GetMainPreferenceSets()
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.MainPreferenceSets;
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        public void InsertMainPreferenceSet(MainPreferenceSet mainPreferenceSet)
        {
            if ((mainPreferenceSet.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(mainPreferenceSet, EntityState.Added);
            }
            else
            {
                this.ObjectContext.MainPreferenceSets.AddObject(mainPreferenceSet);
            }
        }

        public void UpdateMainPreferenceSet(MainPreferenceSet currentMainPreferenceSet)
        {
            this.ObjectContext.MainPreferenceSets.AttachAsModified(currentMainPreferenceSet, this.ChangeSet.GetOriginal(currentMainPreferenceSet));
        }

        public void DeleteMainPreferenceSet(MainPreferenceSet mainPreferenceSet)
        {
            if ((mainPreferenceSet.EntityState == EntityState.Detached))
            {
                this.ObjectContext.MainPreferenceSets.Attach(mainPreferenceSet);
            }
            this.ObjectContext.MainPreferenceSets.DeleteObject(mainPreferenceSet);
        }

        
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'MessageIssues' query.
        [Query(IsDefault = true)]
        public IQueryable<MessageIssue> GetMessageIssues()
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.MessageIssues;
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        public void InsertMessageIssue(MessageIssue messageIssue)
        {
            if ((messageIssue.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(messageIssue, EntityState.Added);
            }
            else
            {
                this.ObjectContext.MessageIssues.AddObject(messageIssue);
            }
        }

        public void UpdateMessageIssue(MessageIssue currentMessageIssue)
        {
            this.ObjectContext.MessageIssues.AttachAsModified(currentMessageIssue, this.ChangeSet.GetOriginal(currentMessageIssue));
        }

        public void DeleteMessageIssue(MessageIssue messageIssue)
        {
            if ((messageIssue.EntityState == EntityState.Detached))
            {
                this.ObjectContext.MessageIssues.Attach(messageIssue);
            }
            this.ObjectContext.MessageIssues.DeleteObject(messageIssue);
        }

        
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'MessageLaterRatedIssues' query.
        [Query(IsDefault = true)]
        public IQueryable<MessageLaterRatedIssue> GetMessageLaterRatedIssues()
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.MessageLaterRatedIssues;
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        public void InsertMessageLaterRatedIssue(MessageLaterRatedIssue messageLaterRatedIssue)
        {
            if ((messageLaterRatedIssue.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(messageLaterRatedIssue, EntityState.Added);
            }
            else
            {
                this.ObjectContext.MessageLaterRatedIssues.AddObject(messageLaterRatedIssue);
            }
        }

        public void UpdateMessageLaterRatedIssue(MessageLaterRatedIssue currentMessageLaterRatedIssue)
        {
            this.ObjectContext.MessageLaterRatedIssues.AttachAsModified(currentMessageLaterRatedIssue, this.ChangeSet.GetOriginal(currentMessageLaterRatedIssue));
        }

        public void DeleteMessageLaterRatedIssue(MessageLaterRatedIssue messageLaterRatedIssue)
        {
            if ((messageLaterRatedIssue.EntityState == EntityState.Detached))
            {
                this.ObjectContext.MessageLaterRatedIssues.Attach(messageLaterRatedIssue);
            }
            this.ObjectContext.MessageLaterRatedIssues.DeleteObject(messageLaterRatedIssue);
        }

        
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'MessageOptionIssues' query.
        [Query(IsDefault = true)]
        public IQueryable<MessageOptionIssue> GetMessageOptionIssues()
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.MessageOptionIssues;
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        public void InsertMessageOptionIssue(MessageOptionIssue messageOptionIssue)
        {
            if ((messageOptionIssue.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(messageOptionIssue, EntityState.Added);
            }
            else
            {
                this.ObjectContext.MessageOptionIssues.AddObject(messageOptionIssue);
            }
        }

        public void UpdateMessageOptionIssue(MessageOptionIssue currentMessageOptionIssue)
        {
            this.ObjectContext.MessageOptionIssues.AttachAsModified(currentMessageOptionIssue, this.ChangeSet.GetOriginal(currentMessageOptionIssue));
        }

        public void DeleteMessageOptionIssue(MessageOptionIssue messageOptionIssue)
        {
            if ((messageOptionIssue.EntityState == EntityState.Detached))
            {
                this.ObjectContext.MessageOptionIssues.Attach(messageOptionIssue);
            }
            this.ObjectContext.MessageOptionIssues.DeleteObject(messageOptionIssue);
        }

        
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'NegConversations' query.
        [Query(IsDefault = true)]
        public IQueryable<NegConversation> GetNegConversations()
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.NegConversations;
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        public void InsertNegConversation(NegConversation negConversation)
        {
            if ((negConversation.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(negConversation, EntityState.Added);
            }
            else
            {
                this.ObjectContext.NegConversations.AddObject(negConversation);
            }
        }

        public void UpdateNegConversation(NegConversation currentNegConversation)
        {
            this.ObjectContext.NegConversations.AttachAsModified(currentNegConversation, this.ChangeSet.GetOriginal(currentNegConversation));
        }

        public void DeleteNegConversation(NegConversation negConversation)
        {
            if ((negConversation.EntityState == EntityState.Detached))
            {
                this.ObjectContext.NegConversations.Attach(negConversation);
            }
            this.ObjectContext.NegConversations.DeleteObject(negConversation);
        }

        
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'NumericIssues' query.
        [Query(IsDefault = true)]
        public IQueryable<NumericIssue> GetNumericIssues()
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.NumericIssues;
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        public void InsertNumericIssue(NumericIssue numericIssue)
        {
            if ((numericIssue.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(numericIssue, EntityState.Added);
            }
            else
            {
                this.ObjectContext.NumericIssues.AddObject(numericIssue);
            }
        }

        public void UpdateNumericIssue(NumericIssue currentNumericIssue)
        {
            this.ObjectContext.NumericIssues.AttachAsModified(currentNumericIssue, this.ChangeSet.GetOriginal(currentNumericIssue));
        }

        public void DeleteNumericIssue(NumericIssue numericIssue)
        {
            if ((numericIssue.EntityState == EntityState.Detached))
            {
                this.ObjectContext.NumericIssues.Attach(numericIssue);
            }
            this.ObjectContext.NumericIssues.DeleteObject(numericIssue);
        }

        
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'OptionIssues' query.
        [Query(IsDefault = true)]
        public IQueryable<OptionIssue> GetOptionIssues()
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.OptionIssues;
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        public void InsertOptionIssue(OptionIssue optionIssue)
        {
            if ((optionIssue.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(optionIssue, EntityState.Added);
            }
            else
            {
                this.ObjectContext.OptionIssues.AddObject(optionIssue);
            }
        }

        public void UpdateOptionIssue(OptionIssue currentOptionIssue)
        {
            this.ObjectContext.OptionIssues.AttachAsModified(currentOptionIssue, this.ChangeSet.GetOriginal(currentOptionIssue));
        }

        public void DeleteOptionIssue(OptionIssue optionIssue)
        {
            if ((optionIssue.EntityState == EntityState.Detached))
            {
                this.ObjectContext.OptionIssues.Attach(optionIssue);
            }
            this.ObjectContext.OptionIssues.DeleteObject(optionIssue);
        }

      
        public void InsertPreferenceSet(PreferenceSet preferenceSet)
        {
            if ((preferenceSet.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(preferenceSet, EntityState.Added);
            }
            else
            {
                this.ObjectContext.PreferenceSets.AddObject(preferenceSet);
            }
        }

        public void UpdatePreferenceSet(PreferenceSet currentPreferenceSet)
        {
            this.ObjectContext.PreferenceSets.AttachAsModified(currentPreferenceSet, this.ChangeSet.GetOriginal(currentPreferenceSet));
        }

        public void DeletePreferenceSet(PreferenceSet preferenceSet)
        {
            if ((preferenceSet.EntityState == EntityState.Detached))
            {
                this.ObjectContext.PreferenceSets.Attach(preferenceSet);
            }
            this.ObjectContext.PreferenceSets.DeleteObject(preferenceSet);
        }

        
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'PreferenceSetNegs' query.
        [Query(IsDefault = true)]
        public IQueryable<PreferenceSetNeg> GetPreferenceSetNegs()
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.PreferenceSetNegs;
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        public void InsertPreferenceSetNeg(PreferenceSetNeg preferenceSetNeg)
        {
            if ((preferenceSetNeg.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(preferenceSetNeg, EntityState.Added);
            }
            else
            {
                this.ObjectContext.PreferenceSetNegs.AddObject(preferenceSetNeg);
            }
        }

        public void UpdatePreferenceSetNeg(PreferenceSetNeg currentPreferenceSetNeg)
        {
            this.ObjectContext.PreferenceSetNegs.AttachAsModified(currentPreferenceSetNeg, this.ChangeSet.GetOriginal(currentPreferenceSetNeg));
        }

        public void DeletePreferenceSetNeg(PreferenceSetNeg preferenceSetNeg)
        {
            if ((preferenceSetNeg.EntityState == EntityState.Detached))
            {
                this.ObjectContext.PreferenceSetNegs.Attach(preferenceSetNeg);
            }
            this.ObjectContext.PreferenceSetNegs.DeleteObject(preferenceSetNeg);
        }

        [Query(IsDefault = true)]
        public IQueryable<PreferenceSetOrganization> GetPreferenceSetOrganizations()
        {

            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.PreferenceSetOrganizations;
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        public void InsertPreferenceSetOrganization(PreferenceSetOrganization preferenceSetOrganization)
        {
            if ((preferenceSetOrganization.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(preferenceSetOrganization, EntityState.Added);
            }
            else
            {
                this.ObjectContext.PreferenceSetOrganizations.AddObject(preferenceSetOrganization);
            }
        }

        public void UpdatePreferenceSetOrganization(PreferenceSetOrganization currentPreferenceSetOrganization)
        {
            this.ObjectContext.PreferenceSetOrganizations.AttachAsModified(currentPreferenceSetOrganization, this.ChangeSet.GetOriginal(currentPreferenceSetOrganization));
        }

        public void DeletePreferenceSetOrganization(PreferenceSetOrganization preferenceSetOrganization)
        {
            if ((preferenceSetOrganization.EntityState == EntityState.Detached))
            {
                this.ObjectContext.PreferenceSetOrganizations.Attach(preferenceSetOrganization);
            }
            this.ObjectContext.PreferenceSetOrganizations.DeleteObject(preferenceSetOrganization);
        }

    }
}


