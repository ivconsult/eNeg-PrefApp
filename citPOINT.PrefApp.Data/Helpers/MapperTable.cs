 

#region → Usings   .
using System;


#endregion

#region → History  .

/* 
 * Date                    User            Change
 * *********************************************
 * 4/4/2011 5:17:47 PM      mwahab         • creation
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

namespace citPOINT.PrefApp.Data 
{


    /// <summary>
    /// mapp old guid to new Guid
    /// </summary>
    public class MapperTable 
    {
 
        #region → Properties     .

        /// <summary>
        /// Gets or sets the old GUID.
        /// </summary>
        /// <value>The old GUID.</value>
        public Guid OldGuid { get; set; }

        /// <summary>
        /// Gets or sets the new GUID.
        /// </summary>
        /// <value>The new GUID.</value>
        public Guid NewGuid { get; set; }

        /// <summary>
        /// Gets or sets the name of the table.
        /// </summary>
        /// <value>The name of the table.</value>
        public TableNames TableName { get; set; }

        #endregion Properties

    }
}
