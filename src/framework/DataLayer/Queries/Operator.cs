using System.ComponentModel;

namespace Orion.Framework.DataLayer.Queries {
    /// <summary>
    /// 
    /// </summary>
    public enum Operator {
        /// <summary>
        /// 
        /// </summary>
        [Description( "" )]
        Equal,
        /// <summary>
        /// 
        /// </summary>
        [Description( "" )]
        NotEqual,
        /// <summary>
        /// 
        /// </summary>
        [Description( "" )]
        Greater,
        /// <summary>
        /// 大于等于
        /// </summary>
        [Description( "" )]
        GreaterEqual,
        /// <summary>
        /// 
        /// </summary>
        [Description( "" )]
        Less,
        /// <summary>
        /// 
        /// </summary>
        [Description( "" )]
        LessEqual,
        /// <summary>
        /// 
        /// </summary>
        [Description( "" )]
        Starts,
        /// <summary>
        /// 
        /// </summary>
        [Description( "" )]
        Ends,
        /// <summary>
        /// 
        /// </summary>
        [Description( "" )]
        Contains,
        /// <summary>
        /// In
        /// </summary>
        [Description( "In" )]
        In,
        /// <summary>
        /// Not In
        /// </summary>
        [Description( "Not In" )]
        NotIn,
    }
}
