using Orion.Framework.DataLayer.Sql.Builders.Core;

namespace Orion.Framework.DataLayer.Sql.Builders {
    /// <summary>
    /// Sql过滤器
    /// </summary>
    public interface ISqlFilter {
        /// <summary>
        /// 过滤
        /// </summary>
        /// <param name="context">Sql执行上下文</param>
        void Filter( SqlContext context );
    }
}
