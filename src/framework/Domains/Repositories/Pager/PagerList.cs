using System;
using System.Collections.Generic;
using System.Linq;

namespace Orion.Framework.Domains.Repositories
{
    
    [Serializable]
    public class PagerList<T> : IPagerBase {
     
        public PagerList() : this( 0 ) {
        }

     
        public PagerList( IEnumerable<T> data = null )
            : this( 0, data ) {
        }

       
        public PagerList( int totalCount, IEnumerable<T> data = null )
            : this( 1, 20, totalCount, data ) {
        }

      
        public PagerList( int page, int pageSize, int totalCount, IEnumerable<T> data = null )
            : this( page, pageSize, totalCount, "", data ) {
        }

       
        public PagerList( int page, int pageSize, int totalCount, string order, IEnumerable<T> data = null ) {
            Data = data?.ToList() ?? new List<T>();
            var pager = new Pager( page, pageSize, totalCount );
            TotalCount = pager.TotalCount;
            PageCount = pager.GetPageCount();
            Page = pager.Page;
            PageSize = pager.PageSize;
            Order = order;
        }

       
        public PagerList( IPager pager, IEnumerable<T> data = null )
            : this( pager.Page, pager.PageSize, pager.TotalCount, pager.Order, data ) {
        }

      
        public int Page { get; set; }

       
        public int PageSize { get; set; }

     
        public int TotalCount { get; set; }

      
        public int PageCount { get; set; }

      
        public string Order { get; set; }

        
        public List<T> Data { get; }

       
        public T this[int index] {
            get => Data[index];
            set => Data[index] = value;
        }

    
        public void Add( T item ) {
            Data.Add( item );
        }

        public void AddRange( IEnumerable<T> collection ) {
            Data.AddRange( collection );
        }

       
        public void Clear() {
            Data.Clear();
        }

      
        public PagerList<TResult> Convert<TResult>( Func<T, TResult> converter ) {
            return Convert( this.Data.Select( converter ) );
        }

       
        public PagerList<TResult> Convert<TResult>( IEnumerable<TResult> data ) {
            return new PagerList<TResult>( Page, PageSize, TotalCount, Order, data );
        }
    }
}
