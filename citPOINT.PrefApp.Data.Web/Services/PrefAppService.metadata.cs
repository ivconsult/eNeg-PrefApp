
namespace citPOINT.PrefApp.Data.Web
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Objects.DataClasses;



    // The MetadataTypeAttribute identifies PreferenceSetOrganizationMetadata as the class
    // that carries additional metadata for the PreferenceSetOrganization class.
    [MetadataTypeAttribute(typeof(PreferenceSetOrganization.PreferenceSetOrganizationMetadata))]
    public partial class PreferenceSetOrganization
    {

        // This class allows you to attach custom attributes to properties
        // of the PreferenceSetOrganization class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class PreferenceSetOrganizationMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private PreferenceSetOrganizationMetadata()
            {
            }

            public bool Deleted { get; set; }

            public Nullable<Guid> DeletedBy { get; set; }

            public Nullable<DateTime> DeletedOn { get; set; }

            public Guid OrganizationID { get; set; }

            public PreferenceSet PreferenceSet { get; set; }

            public Guid PreferenceSetID { get; set; }

            public Guid PreferenceSetOrganizationID { get; set; }
        }
    }


    // The MetadataTypeAttribute identifies ActionTypeMetadata as the class
    // that carries additional metadata for the ActionType class.
    [MetadataTypeAttribute(typeof(ActionType.ActionTypeMetadata))]
    public partial class ActionType
    {

        // This class allows you to attach custom attributes to properties
        // of the ActionType class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ActionTypeMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private ActionTypeMetadata()
            {
            }

            public string ActionName { get; set; }

            public Guid ActionTypeID { get; set; }

            public EntityCollection<History> Histories { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies C__RefactorLogMetadata as the class
    // that carries additional metadata for the C__RefactorLog class.
    [MetadataTypeAttribute(typeof(C__RefactorLog.C__RefactorLogMetadata))]
    public partial class C__RefactorLog
    {

        // This class allows you to attach custom attributes to properties
        // of the C__RefactorLog class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class C__RefactorLogMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private C__RefactorLogMetadata()
            {
            }

            public Guid OperationKey { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies ConversationMessageMetadata as the class
    // that carries additional metadata for the ConversationMessage class.
    [MetadataTypeAttribute(typeof(ConversationMessage.ConversationMessageMetadata))]
    public partial class ConversationMessage
    {

        // This class allows you to attach custom attributes to properties
        // of the ConversationMessage class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ConversationMessageMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private ConversationMessageMetadata()
            {
            }

            public Guid ConversationMessageID { get; set; }

            public Nullable<bool> Deleted { get; set; }

            public Nullable<Guid> DeletedBy { get; set; }

            public Nullable<DateTime> DeletedOn { get; set; }

            public Nullable<Guid> MessageID { get; set; }

            public EntityCollection<MessageIssue> MessageIssues { get; set; }

            public NegConversation NegConversation { get; set; }

            public Guid NegConversationID { get; set; }

            public Nullable<decimal> Percentage { get; set; }

            public Nullable<DateTime> RatedDate { get; set; }

            public Nullable<bool> IsSent { get; set; }

            public Nullable<bool> IsExceedVariation { get; set; }
            
        }
    }

    // The MetadataTypeAttribute identifies HistoryMetadata as the class
    // that carries additional metadata for the History class.
    [MetadataTypeAttribute(typeof(History.HistoryMetadata))]
    public partial class History
    {

        // This class allows you to attach custom attributes to properties
        // of the History class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class HistoryMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private HistoryMetadata()
            {
            }

            public DateTime ActionDate { get; set; }

            public ActionType ActionType { get; set; }

            public Guid ActionTypeID { get; set; }

            public Guid DoneBy { get; set; }

            public string NewValue { get; set; }

            public string OldValue { get; set; }

            public Guid SN { get; set; }

            public string TableName { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies IssueMetadata as the class
    // that carries additional metadata for the Issue class.
    [MetadataTypeAttribute(typeof(Issue.IssueMetadata))]
    public partial class Issue
    {

        // This class allows you to attach custom attributes to properties
        // of the Issue class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class IssueMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private IssueMetadata()
            {
            }

            public Nullable<bool> Deleted { get; set; }

            public Nullable<Guid> DeletedBy { get; set; }

            public Nullable<DateTime> DeletedOn { get; set; }

            public Guid IssueID { get; set; }
            
            public string IssueName { get; set; }

            public IssueType IssueType { get; set; }

            public Guid IssueTypeID { get; set; }

            public decimal IssueWeight { get; set; }

            public EntityCollection<LaterRatedIssue> LaterRatedIssues { get; set; }

            public EntityCollection<MessageIssue> MessageIssues { get; set; }

            public EntityCollection<NumericIssue> NumericIssues { get; set; }

            public EntityCollection<OptionIssue> OptionIssues { get; set; }

            public PreferenceSet PreferenceSet { get; set; }

            public Guid PreferenceSetID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies IssueTypeMetadata as the class
    // that carries additional metadata for the IssueType class.
    [MetadataTypeAttribute(typeof(IssueType.IssueTypeMetadata))]
    public partial class IssueType
    {

        // This class allows you to attach custom attributes to properties
        // of the IssueType class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class IssueTypeMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private IssueTypeMetadata()
            {
            }

            public EntityCollection<Issue> Issues { get; set; }

            public Guid IssueTypeID { get; set; }

            public string IssueTypeName { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies LaterRatedIssueMetadata as the class
    // that carries additional metadata for the LaterRatedIssue class.
    [MetadataTypeAttribute(typeof(LaterRatedIssue.LaterRatedIssueMetadata))]
    public partial class LaterRatedIssue
    {

        // This class allows you to attach custom attributes to properties
        // of the LaterRatedIssue class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class LaterRatedIssueMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private LaterRatedIssueMetadata()
            {
            }

            public Nullable<bool> Deleted { get; set; }

            public Nullable<Guid> DeletedBy { get; set; }

            public Nullable<DateTime> DeletedOn { get; set; }

            public Issue Issue { get; set; }

            public Guid IssueID { get; set; }

            public Guid LaterRatedIssueID { get; set; }

            public string LaterRatedIssueValue { get; set; }

            public decimal LaterRatedIssueWeight { get; set; }

            public EntityCollection<MessageLaterRatedIssue> MessageLaterRatedIssues { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies MainPreferenceSetMetadata as the class
    // that carries additional metadata for the MainPreferenceSet class.
    [MetadataTypeAttribute(typeof(MainPreferenceSet.MainPreferenceSetMetadata))]
    public partial class MainPreferenceSet
    {

        // This class allows you to attach custom attributes to properties
        // of the MainPreferenceSet class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class MainPreferenceSetMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private MainPreferenceSetMetadata()
            {
            }

            public Guid MainPreferenceSetID { get; set; }

            public string MainPreferenceSetName { get; set; }

            public EntityCollection<PreferenceSet> PreferenceSets { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies MessageIssueMetadata as the class
    // that carries additional metadata for the MessageIssue class.
    [MetadataTypeAttribute(typeof(MessageIssue.MessageIssueMetadata))]
    public partial class MessageIssue
    {

        // This class allows you to attach custom attributes to properties
        // of the MessageIssue class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class MessageIssueMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private MessageIssueMetadata()
            {
            }

            public ConversationMessage ConversationMessage { get; set; }

            public Guid ConversationMessageID { get; set; }

            public Nullable<bool> Deleted { get; set; }

            public Nullable<Guid> DeletedBy { get; set; }

            public Nullable<DateTime> DeletedOn { get; set; }

            public Issue Issue { get; set; }

            public Guid IssueID { get; set; }

            public Guid MessageIssueID { get; set; }

            public EntityCollection<MessageLaterRatedIssue> MessageLaterRatedIssues { get; set; }

            public EntityCollection<MessageOptionIssue> MessageOptionIssues { get; set; }

            public Nullable<decimal> Score { get; set; }

            public string Value { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies MessageLaterRatedIssueMetadata as the class
    // that carries additional metadata for the MessageLaterRatedIssue class.
    [MetadataTypeAttribute(typeof(MessageLaterRatedIssue.MessageLaterRatedIssueMetadata))]
    public partial class MessageLaterRatedIssue
    {

        // This class allows you to attach custom attributes to properties
        // of the MessageLaterRatedIssue class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class MessageLaterRatedIssueMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private MessageLaterRatedIssueMetadata()
            {
            }

            public Nullable<bool> Deleted { get; set; }

            public Nullable<Guid> DeletedBy { get; set; }

            public Nullable<DateTime> DeletedOn { get; set; }

            public LaterRatedIssue LaterRatedIssue { get; set; }

            public MessageIssue MessageIssue { get; set; }

            public Guid MessageIssueID { get; set; }

            public Guid MessageLaterRatedIssueID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies MessageOptionIssueMetadata as the class
    // that carries additional metadata for the MessageOptionIssue class.
    [MetadataTypeAttribute(typeof(MessageOptionIssue.MessageOptionIssueMetadata))]
    public partial class MessageOptionIssue
    {

        // This class allows you to attach custom attributes to properties
        // of the MessageOptionIssue class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class MessageOptionIssueMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private MessageOptionIssueMetadata()
            {
            }

            public Nullable<bool> Deleted { get; set; }

            public Nullable<Guid> DeletedBy { get; set; }

            public Nullable<DateTime> DeletedOn { get; set; }

            public MessageIssue MessageIssue { get; set; }

            public Guid MessageIssueID { get; set; }

            public Guid MessageOptionIssueID { get; set; }

            public OptionIssue OptionIssue { get; set; }

            public Guid OptionIssueID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies NegConversationMetadata as the class
    // that carries additional metadata for the NegConversation class.
    [MetadataTypeAttribute(typeof(NegConversation.NegConversationMetadata))]
    public partial class NegConversation
    {

        // This class allows you to attach custom attributes to properties
        // of the NegConversation class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class NegConversationMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private NegConversationMetadata()
            {
            }

            public Nullable<Guid> ConversationID { get; set; }

            public EntityCollection<ConversationMessage> ConversationMessages { get; set; }

            public Nullable<bool> Deleted { get; set; }

            public Nullable<Guid> DeletedBy { get; set; }

            public Nullable<DateTime> DeletedOn { get; set; }

            public Guid NegConversationID { get; set; }

            public decimal Percentage { get; set; }

            public PreferenceSetNeg PreferenceSetNeg { get; set; }

            public Guid PreferenceSetNegID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies NumericIssueMetadata as the class
    // that carries additional metadata for the NumericIssue class.
    [MetadataTypeAttribute(typeof(NumericIssue.NumericIssueMetadata))]
    public partial class NumericIssue
    {

        // This class allows you to attach custom attributes to properties
        // of the NumericIssue class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class NumericIssueMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private NumericIssueMetadata()
            {
            }

            public Nullable<bool> Deleted { get; set; }

            public Nullable<Guid> DeletedBy { get; set; }

            public Nullable<DateTime> DeletedOn { get; set; }

            public Issue Issue { get; set; }

            public Guid IssueID { get; set; }

            public byte MaximumOperator { get; set; }

            public decimal MaximumValue { get; set; }

            public byte MinimumOperator { get; set; }

            public decimal MinimumValue { get; set; }

            public Guid NumericIssueID { get; set; }

            public decimal OptimumValueEnd { get; set; }

            public decimal OptimumValueStart { get; set; }

            public string Unit { get; set; }
        }
    }


    // The MetadataTypeAttribute identifies PreferenceSetMetadata as the class
    // that carries additional metadata for the PreferenceSet class.
    [MetadataTypeAttribute(typeof(PreferenceSet.PreferenceSetMetadata))]
    public partial class PreferenceSet
    {

        // This class allows you to attach custom attributes to properties
        // of the PreferenceSet class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class PreferenceSetMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private PreferenceSetMetadata()
            {
            }

            public Nullable<bool> Deleted { get; set; }

            public Nullable<Guid> DeletedBy { get; set; }

            public Nullable<DateTime> DeletedOn { get; set; }

            public EntityCollection<Issue> Issues { get; set; }

            public MainPreferenceSet MainPreferenceSet { get; set; }

            public Guid MainPreferenceSetID { get; set; }

            public Guid PreferenceSetID { get; set; }

            public string PreferenceSetName { get; set; }

            public decimal MaxPercentage { get; set; }

            public EntityCollection<PreferenceSetNeg> PreferenceSetNegs { get; set; }

            public Guid UserID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies PreferenceSetNegMetadata as the class
    // that carries additional metadata for the PreferenceSetNeg class.
    [MetadataTypeAttribute(typeof(PreferenceSetNeg.PreferenceSetNegMetadata))]
    public partial class PreferenceSetNeg
    {

        // This class allows you to attach custom attributes to properties
        // of the PreferenceSetNeg class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class PreferenceSetNegMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private PreferenceSetNegMetadata()
            {
            }

            public Nullable<bool> Deleted { get; set; }

            public Nullable<Guid> DeletedBy { get; set; }

            public Nullable<DateTime> DeletedOn { get; set; }

            public EntityCollection<NegConversation> NegConversations { get; set; }

            public Guid NegID { get; set; }

            public string NegotiationName { get; set; }

            public Guid StatusID { get; set; }

            public decimal Percentage { get; set; }

            public PreferenceSet PreferenceSet { get; set; }

            public Guid PreferenceSetID { get; set; }

            public Guid PreferenceSetNegID { get; set; }
        }
    }
}
