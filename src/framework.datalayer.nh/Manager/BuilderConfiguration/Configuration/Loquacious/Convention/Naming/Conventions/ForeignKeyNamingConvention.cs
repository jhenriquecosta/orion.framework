using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DataLayer.NHibernate.Loquacious.Convention
{
    /// <summary>
    /// Defines the different naming conventions used when creating the FK names
    /// </summary>
    public enum ForeignKeyNamingConvention
    {
        /// <summary>
        /// The default NH MCC convention
        /// </summary>
        Default,

        /// <summary>
        /// Custom user defined convention
        /// </summary>
        Custom,

        /// <summary>
        /// FK + FKTable + PKTable
        /// </summary>
        FK_FKTable_PKTable,

        /// <summary>
        /// FK + FKTable + PKTable + FKColumn
        /// </summary>
        FK_FKTable_PKTable_FKColumn,

        /// <summary>
        /// FKTable + PKTable + FKColumn + FK
        /// </summary>
        FKTable_PKTable_FKColumn_FK,

        /// <summary>
        /// FKTable + PKTable + FK,
        /// </summary>
        FKTable_PKTable_FK,
    }
}
