
#region → Usings   .
using System;
using System.ComponentModel;
using System.Linq;
using System.Collections.Generic;
using citPOINT.PrefApp.Data.Web;
using GalaSoft.MvvmLight;

#endregion

#region → History  .

/* 
 * Date         User            Change
 * *********************************************
 * 27.03.12    Yousra Reda      • creation
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

namespace citPOINT.PrefApp.Common.Helper
{

    /// <summary>
    /// Class for Repository 
    /// </summary>
    public static class Repository 
    {

        #region → Fields         .

        private static PrefAppContext mPrefAppContext;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        /// <value>The context.</value>
        public static PrefAppContext Context
        {
            get
            {
                if (mPrefAppContext == null)
                {
                    mPrefAppContext = new PrefAppContext(PrefAppConfigurations.MainServiceUri);
                }

                return mPrefAppContext;
            }
            set
            {
                mPrefAppContext = value;

            }
        }

        #endregion Properties

        #region → Methods        .

        /// <summary>
        /// Cleanups this instance.
        /// </summary>
        public static void Cleanup()
        {
            mPrefAppContext = null;
        }

        #endregion

    }
}
