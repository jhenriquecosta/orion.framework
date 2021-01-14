using Orion.Framework.Domains.Attributes;
using Orion.Framework.Domains.Repositories;

namespace Orion.Framework.DataLayer.Queries
{
    /// <summary>
    /// 
    /// </summary>
    [Model( "queryParam" )]
    public class QueryParameter : Pager, IQueryParameter {
        /// <summary>
        /// 
        /// </summary>
        public string Keyword { get; set; }
    }
}
