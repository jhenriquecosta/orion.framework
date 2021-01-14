namespace Orion.Framework.Domains.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class Pager : IPager {
        /// <summary>
        /// 
        /// </summary>
        public Pager()
            : this( 1 ) {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize">,</param> 
        /// <param name="order"></param>
        public Pager( int page, int pageSize, string order )
            : this( page, pageSize, 0, order ) {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize">,</param> 
        /// <param name="totalCount"></param>
        /// <param name="order"></param>
        public Pager( int page, int pageSize = 20, int totalCount = 0, string order = "" ) {
            Page = page;
            PageSize = pageSize;
            TotalCount = totalCount;
            Order = order;
        }

        private int _pageIndex;
        /// <summary>
        /// 
        /// </summary>
        public int Page {
            get {
                if( _pageIndex <= 0 )
                    _pageIndex = 1;
                return _pageIndex;
            }
            set => _pageIndex = value;
        }

        /// <summary>
        /// 
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int GetPageCount() {
            if ( ( TotalCount % PageSize ) == 0 )
                return TotalCount / PageSize;
            return ( TotalCount / PageSize ) + 1;
        }

        /// <summary>
        /// 
        /// </summary>
        public int GetSkipCount() {
            if ( Page > GetPageCount() )
                Page = GetPageCount();
            return PageSize * ( Page - 1 );
        }

        /// <summary>
        /// 
        /// </summary>
        public string Order { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int GetStartNumber() {
            return ( Page - 1 ) * PageSize + 1;
        }
        /// <summary>
        /// 
        /// </summary>
        public int GetEndNumber() {
            return Page * PageSize;
        }
    }
}
