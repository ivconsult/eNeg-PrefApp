﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace citPOINT.PrefApp.ViewModel {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("citPOINT.PrefApp.ViewModel.Resources.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Sorry, your account is disabled.
        ///Contact you administrator to enable your account..
        /// </summary>
        public static string AccountDisabled {
            get {
                return ResourceManager.GetString("AccountDisabled", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Sorry, Your account is Locked.
        /// </summary>
        public static string AccountLocked {
            get {
                return ResourceManager.GetString("AccountLocked", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Please wait while adding negotiation to Preference set.
        /// </summary>
        public static string AddingNegToPreferencesSetLoading {
            get {
                return ResourceManager.GetString("AddingNegToPreferencesSetLoading", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Please select the Ongoing Negotiation that you want to add:.
        /// </summary>
        public static string AddNegWindowHeader {
            get {
                return ResourceManager.GetString("AddNegWindowHeader", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cancel editing current item, Are you sure?.
        /// </summary>
        public static string CancelEditingItem {
            get {
                return ResourceManager.GetString("CancelEditingItem", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Delete Confirmation.
        /// </summary>
        public static string ConfirmMessageBoxCaption {
            get {
                return ResourceManager.GetString("ConfirmMessageBoxCaption", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot connect to eNeg remote server.
        /// </summary>
        public static string ConnectionFailed {
            get {
                return ResourceManager.GetString("ConnectionFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Do you really want to delete?.
        /// </summary>
        public static string DeleteCurrentItemMessageBoxText {
            get {
                return ResourceManager.GetString("DeleteCurrentItemMessageBoxText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Issue name must not exceed 300 character.
        /// </summary>
        public static string IssueLongName {
            get {
                return ResourceManager.GetString("IssueLongName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Loading Negotiations.
        /// </summary>
        public static string LoadingNegs {
            get {
                return ResourceManager.GetString("LoadingNegs", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Maximum must be Greater than or Equal to Minimum value..
        /// </summary>
        public static string MaximumToMinimumValue {
            get {
                return ResourceManager.GetString("MaximumToMinimumValue", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Optimum End must be Between (or Equal) Optimum Start and Maximum Values..
        /// </summary>
        public static string MaxOptimumToMinOptimumToMinValue {
            get {
                return ResourceManager.GetString("MaxOptimumToMinOptimumToMinValue", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Minimum must be less than or Equal to Maximum value..
        /// </summary>
        public static string MinimumToMaximumValue {
            get {
                return ResourceManager.GetString("MinimumToMaximumValue", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Optimum start must be Between (or Equal) Minumum and Optimum End Values..
        /// </summary>
        public static string MinOptimumToMaxOptimumToMaxValue {
            get {
                return ResourceManager.GetString("MinOptimumToMaxOptimumToMaxValue", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to App needs to be configured first. 
        ///
        ///Please use the button &quot;Settings&quot;..
        /// </summary>
        public static string MsgConfigureNeeded {
            get {
                return ResourceManager.GetString("MsgConfigureNeeded", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No available information for this conversation.
        ///
        ///   1- Go to main platform.
        ///
        ///   2- Add data matching for the messages.
        ///
        ///   3- Open report again..
        /// </summary>
        public static string MsgNoAvailableConversationInfo {
            get {
                return ResourceManager.GetString("MsgNoAvailableConversationInfo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No available information for this negotiation.
        ///
        ///   1- Go to main platform.
        ///
        ///   2- Add data matching for the messages.
        ///
        ///   3- Open report again..
        /// </summary>
        public static string MsgNoAvailableNegotiationInfo {
            get {
                return ResourceManager.GetString("MsgNoAvailableNegotiationInfo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Sorry, The Negotiation you want doesn&apos;t exist in any Preference Set.
        /// </summary>
        public static string NegotiationNotExist {
            get {
                return ResourceManager.GetString("NegotiationNotExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Issue name must not exceed 100 character.
        /// </summary>
        public static string OptionLongName {
            get {
                return ResourceManager.GetString("OptionLongName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Please select the Ongoing Negotiation that you want to remove:.
        /// </summary>
        public static string RemoveNegWindowHeader {
            get {
                return ResourceManager.GetString("RemoveNegWindowHeader", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Their is another issue with the same name..
        /// </summary>
        public static string RepeatedIssue {
            get {
                return ResourceManager.GetString("RepeatedIssue", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Their is another option with the same name..
        /// </summary>
        public static string RepeatedOptions {
            get {
                return ResourceManager.GetString("RepeatedOptions", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Their is another preference set with the same name..
        /// </summary>
        public static string RepeatedPrefSet {
            get {
                return ResourceManager.GetString("RepeatedPrefSet", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Do you want to send an E-Mail to all other Negotiators to request .
        /// </summary>
        public static string SendingMailRequest {
            get {
                return ResourceManager.GetString("SendingMailRequest", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unassign Confirmation.
        /// </summary>
        public static string UnassignedConfirmMessageBoxCaption {
            get {
                return ResourceManager.GetString("UnassignedConfirmMessageBoxCaption", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Do you really want to unassign?.
        /// </summary>
        public static string UnAssignNegotiationConfirm {
            get {
                return ResourceManager.GetString("UnAssignNegotiationConfirm", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select valid issue type..
        /// </summary>
        public static string UndefiendType {
            get {
                return ResourceManager.GetString("UndefiendType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You should be a member in any organization to can publish to it..
        /// </summary>
        public static string UserHaveNotOrganization {
            get {
                return ResourceManager.GetString("UserHaveNotOrganization", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The E-mail or password is incorrect. 
        ///Please try again..
        /// </summary>
        public static string WrongCredentials {
            get {
                return ResourceManager.GetString("WrongCredentials", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to you can not set Opt.Start=Minimum and also Opt.End=Maximum..
        /// </summary>
        public static string WrongGraphValuesMinMax {
            get {
                return ResourceManager.GetString("WrongGraphValuesMinMax", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The password is incorrect.
        ///Please try again..
        /// </summary>
        public static string WrongPassword {
            get {
                return ResourceManager.GetString("WrongPassword", resourceCulture);
            }
        }
    }
}
