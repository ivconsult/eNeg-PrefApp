#region → Usings   .
using System;
using System.ComponentModel;
using System.ServiceModel.DomainServices.Client;
using citPOINT.eNeg.Common;
using citPOINT.PrefApp.Common;
using citPOINT.PrefApp.Data.Web;
using System.ComponentModel.Composition;
#endregion

#region → History  .

/* 
 * Date         User            Change
 * *********************************************
 * 3/29/2012 10:55:03 AM      mwahab         • creation
 * **********************************************
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion

namespace citPOINT.PrefApp.Model
{
    /// <summary>
    /// Class for Top10IssuesModel 
    /// </summary>
    #region " Using MEF to export Top10IssuesModel  "
    [Export(typeof(ITop10IssuesModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    #endregion
    public class Top10IssuesModel : ITop10IssuesModel
    {
        #region → Fields         .
        private PrefAppContext mPrefAppContext;
        private bool mIsBusy;
        #endregion

        #region → Properties     .

        /// <summary>
        /// True if "IsLoading"
        /// in progress; otherwise, false
        /// </summary>
        /// <value></value>
        public bool IsBusy
        {
            get
            {
                return mIsBusy;
            }
            private set
            {
                mIsBusy = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs("IsBusy"));
                }
            }
        }

        /// <summary>
        /// Context of Service eNegService
        /// </summary>
        private PrefAppContext Context
        {
            get
            {
                if (mPrefAppContext == null)
                {
                    mPrefAppContext = new PrefAppContext(PrefAppConfigurations.MainServiceUri);

                    mPrefAppContext.PropertyChanged += new PropertyChangedEventHandler(mPrefAppContext_PropertyChanged);
                }

                return mPrefAppContext;
            }
        }

        #endregion Properties

        #region → Event Handlers .

        /// <summary>
        /// Executed when any property of Domain context is changed
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">PropertyChangedEventArgs</param>
        private void mPrefAppContext_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "IsLoading":
                    this.IsBusy = mPrefAppContext.IsLoading;
                    break;
            }
        }

        #endregion

        #region → Events         .

        /// <summary>
        /// event for returning the loaded Top ten Issues.
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<IssueStatisticalsResult>> GetIssueStatisticalsComplete;

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Private Method used to perform query on certain entity of PrefApp Entities
        /// </summary>
        /// <typeparam name="T">Value Of T</typeparam>
        /// <param name="qry">Value Of qry</param>
        /// <param name="evt">Value Of evt</param>
        private void PerformQuery<T>(EntityQuery<T> qry, EventHandler<eNegEntityResultArgs<T>> evt) where T : Entity
        {

            Context.Load<T>(qry, LoadBehavior.RefreshCurrent, r =>
            {
                if (evt != null)
                {
                    try
                    {
                        if (r.HasError)
                        {
                            evt(this, new eNegEntityResultArgs<T>(r.Error));
                            r.MarkErrorAsHandled();
                        }
                        else
                        {
                            evt(this, new eNegEntityResultArgs<T>(r.Entities));
                        }
                    }
                    catch (Exception ex)
                    {
                        evt(this, new eNegEntityResultArgs<T>(ex));
                    }
                }
            }, null);
        }

        #endregion

        #region → Public         .

        /// <summary>
        /// Gets the issue statisticals async.
        /// </summary>
        public void GetIssueStatisticalsAsync()
        {
            PerformQuery<IssueStatisticalsResult>(Context.GetIssueStatisticalsQuery(PrefAppConfigurations.CurrentLoginUser.UserID), GetIssueStatisticalsComplete);
        }

        #endregion
        #endregion
    }
}
