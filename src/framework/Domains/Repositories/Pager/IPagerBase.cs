namespace Orion.Framework.Domains.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPagerBase {
        /// <summary>
        /// 
        /// </summary>
        int Page { get; set; }
        /// <summary>
        ///
        /// </summary>
        int PageSize { get; set; }
        /// <summary>
        /// 
        /// </summary>
        int TotalCount { get; set; }
    }
}
