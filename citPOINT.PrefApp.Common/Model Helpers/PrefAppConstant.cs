
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


namespace citPOINT.PrefApp.Common
{

    #region → Operators Enum    .

    /// <summary>
    ///  Minimum Maximum Operators
    /// </summary>
    public enum Operators
    {

        /// <summary>
        /// Better like Greater than Operator
        /// </summary>
        Better = 0,

        /// <summary>
        /// Equal Like = Operator all 100%
        /// </summary>
        Equal = 1,

        /// <summary>
        /// Worse Like Zero rate for what Next the Maximum or Minimum Value.
        /// </summary>
        Worse = 2

    }



    /// <summary>
    /// Message Filter (ComboBox in Data matching)
    /// </summary>
    public enum MessageFilter
    {
        /// <summary>
        /// Select All
        /// </summary>
        All=0,
        /// <summary>
        /// Received Messages Only.
        /// </summary>
        ReceivedDataOnly=1,

        /// <summary>
        /// Send Messages Only.
        /// </summary>
        SendDataOnly=2

    }

    #endregion


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
            private static Guid mSetStore;
            private static Guid mOrganizationSets;
            #endregion  Fields

            #region → Properties     .
            /// <summary>
            /// for MySets MainPreferenceSets
            /// </summary>
            public static Guid MySets
            {
                get
                {
                    if (mMySets== Guid.Empty)
                        mMySets = new Guid("72f5566e-3bf5-46e6-9406-b13e80f83bcc");
                    return mMySets;
                }
            }

            /// <summary>
            /// Gets the set store.
            /// </summary>
            /// <value>The set store.</value>
            public static Guid SetStore
            {
                get
                {
                    if (mSetStore== Guid.Empty)
                        mSetStore = new Guid("dc0981bd-0164-4042-a313-5d79cff5211c");
                    return mSetStore;
                }
            }

            /// <summary>
            /// Gets the organization sets.
            /// </summary>
            /// <value>The organization sets.</value>
            public static Guid OrganizationSets
            {
                get
                {
                    if (mOrganizationSets== Guid.Empty)
                        mOrganizationSets = new Guid("78ac5cf7-a5ab-4377-b9f9-d105f462c26e");
                    return mOrganizationSets;
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
                    return Guid.Empty;
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
                    if (mNumeric== Guid.Empty)
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
                    if (mOptions== Guid.Empty)
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
                    if (mLaterRated== Guid.Empty)
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
                    if (mNotRated== Guid.Empty)
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
