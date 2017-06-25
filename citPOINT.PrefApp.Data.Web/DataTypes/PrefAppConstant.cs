
#region → Usings   .
using System;
#endregion

#region → History  .

/* Date         User              Change
 * 
 * 26.09.10     Yousra Reda       Creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion


namespace citPOINT.PrefApp.Data.Web
{ 

    #region → Pref App Constant .

    /// <summary>
    /// Constant for All Tables (Lockup Tables)
    /// </summary>
    public static class PrefAppConstant
    {
        #region → MainPrefernce Sets  .

        /// <summary>
        /// For Main Prefernce Sets
        /// </summary>
        public class MainPreferenceSets
        {
            #region → Fields         .
            private static Guid mMySets;
            #endregion  Fields

            #region → Properties     .
            /// <summary>
            /// for MySets MainPreferenceSets
            /// </summary>
            public static Guid MySets
            {
                get
                {
                    if (mMySets.ToString() == "00000000-0000-0000-0000-000000000000")
                        mMySets = new Guid("72f5566e-3bf5-46e6-9406-b13e80f83bcc");

                    return mMySets;
                }

            }
            #endregion  Properties
        }

        #endregion MainPrefernceSets

        #region → IssueTypes Sets     .

        /// <summary>
        /// Issue Types
        /// </summary>
        public class IssueTypes
        {

            #region → Fields         .
            private static Guid mNumeric;
            private static Guid mOptions;
            private static Guid mLaterRated;
            private static Guid mNotRated;

            #endregion  Fields

            #region → Properties     .

            /// <summary>
            /// Gets the type of the select.
            /// </summary>
            /// <value>The type of the select.</value>
            public static Guid SelectType
            {
                get
                {
                    return new Guid();
                }

            }

            /// <summary>
            /// Gets the numeric.
            /// </summary>
            /// <value>The numeric.</value>
            public static Guid Numeric
            {
                get
                {
                    if (mNumeric.ToString() == "00000000-0000-0000-0000-000000000000")
                        mNumeric = new Guid("6025fbd2-c4eb-474d-834f-4818bde8e4eb");

                    return mNumeric;
                }

            }

            /// <summary>
            /// Gets the options.
            /// </summary>
            /// <value>The options.</value>
            public static Guid Options
            {
                get
                {
                    if (mOptions.ToString() == "00000000-0000-0000-0000-000000000000")
                        mOptions = new Guid("6125fbd2-c4eb-474d-834f-4818bde8e4eb");

                    return mOptions;
                }

            }

            /// <summary>
            /// Gets the later rated.
            /// </summary>
            /// <value>The later rated.</value>
            public static Guid LaterRated
            {
                get
                {
                    if (mLaterRated.ToString() == "00000000-0000-0000-0000-000000000000")
                        mLaterRated = new Guid("6225fbd2-c4eb-474d-834f-4818bde8e4eb");

                    return mLaterRated;
                }

            }

            /// <summary>
            /// Gets the not rated.
            /// </summary>
            /// <value>The not rated.</value>
            public static Guid NotRated
            {
                get
                {
                    if (mNotRated.ToString() == "00000000-0000-0000-0000-000000000000")
                        mNotRated = new Guid("6325fbd2-c4eb-474d-834f-4818bde8e4eb");

                    return mNotRated;
                }

            }
            #endregion  Properties

        }
        #endregion

    }

    #endregion

}
