using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using citPOINT.PrefApp.ViewModel.Helpers;
using System.ServiceModel.DomainServices.EntityFramework;

namespace citPOINT.PrefApp.Data.Web.Helpers
{
    class NumericBoundary : INumericBoundary
    {

        #region → Fields         .

        private PrefAppEntities mContext;
        public Guid mPreferenceSetNegID;
        private NumericIssue mCurrentNumeric;

        #endregion

        #region → Properties     .


        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        /// <value>The context.</value>
        private PrefAppEntities Context
        {
            get
            {
                return mContext;
            }

            set
            {
                mContext = value;
            }
        }

        /// <summary>
        /// Gets the top minimum value.
        /// </summary>
        /// <value>The top minimum value.</value>
        public decimal TopMinimumValue
        {
            get
            {
                //intialize by minimum
                decimal MinValue = CurrentNumeric.MinimumValue;


                MessageIssue messageIssue = this.Messages
                                          .OrderBy(ss => decimal.Parse(ss.Value)).Take(1).FirstOrDefault();

                if (messageIssue != null)
                {
                    if (MinValue > decimal.Parse(messageIssue.Value))
                    {
                        MinValue = decimal.Parse(messageIssue.Value);
                    }
                }


                return MinValue;// Minimum;
            }
        }

        /// <summary>
        /// Gets the top maximum value.
        /// </summary>
        /// <value>The top maximum value.</value>
        public decimal TopMaximumValue
        {
            get
            {
                //intialize to maximum of chart.
                decimal MaxValue = CurrentNumeric.MaximumValue;


                MessageIssue messageIssue = this.Messages
                                          .OrderByDescending(ss => decimal.Parse(ss.Value)).Take(1).FirstOrDefault();

                if (messageIssue != null)
                {
                    if (MaxValue < decimal.Parse(messageIssue.Value))
                    {
                        MaxValue = decimal.Parse(messageIssue.Value);
                    }
                }




                return MaxValue;        // Maximum; }
            }
        }

        private IQueryable<MessageIssue> Messages
        {
            get
            {
                return this.Context.MessageIssues
                           .Where(s => s.ConversationMessage != null //has a message
                                    && s.ConversationMessage.NegConversation != null //Has a negotiation
                                    && s.ConversationMessage.NegConversation.PreferenceSetNegID == this.PreferenceSetNegID //In same Negotiation
                                    && s.Issue != null //this issue still exist and not deleted
                                    && s.Issue.IssueTypeID == PrefAppConstant.IssueTypes.Numeric //Numeric Type
                                    && s.Deleted == false  //Not Deleted
                                    && s.IssueID == CurrentNumeric.IssueID);
            }
        }

        /// <summary>
        /// Gets or sets the preference set neg ID.
        /// </summary>
        /// <value>The preference set neg ID.</value>
        public Guid PreferenceSetNegID
        {
            get
            {
                return mPreferenceSetNegID;
            }
            set
            {
                mPreferenceSetNegID = value;
            }
        }

        /// <summary>
        /// Gets or sets the current numeric.
        /// </summary>
        /// <value>The current numeric.</value>
        public Data.Web.NumericIssue CurrentNumeric
        {
            get
            {
                return mCurrentNumeric;
            }
            set
            {
                mCurrentNumeric = value;
            }
        }

        #endregion Properties

        #region → Constractor    .

        /// <summary>
        /// Numerics the boundary.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="currentNumeric">The current numeric.</param>
        /// <param name="preferenceSetNegID">The preference set neg ID.</param>
        public NumericBoundary(PrefAppEntities context, NumericIssue currentNumeric, Guid preferenceSetNegID)
        {
            this.Context = context;
            this.CurrentNumeric = currentNumeric;
            this.PreferenceSetNegID = preferenceSetNegID;
        }

        #endregion

        #region → Event Handlers .
        #endregion

        #region → Events         .

        #endregion

        #region → Methods        .

        #region → Private        .


        #endregion

        #region → Protected      .
        #endregion

        #region → Public         .



        #endregion  Public


        #endregion Methods

    }
}
