using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Orion.Framework.DataLayer.NH.QueryObjects
{
    public class QueryableResult<TResult> : IQueryResult<TResult> where TResult : class
    {
        #region "PRIVATE MEMBERS"
        private IQueryable<TResult> _query;
        private string _description;
        #endregion

        #region "PROPERTIES"
        public string Description
        {
            get { return _description; }
        }
        #endregion

        #region "CONSTRUCTORS"
        public QueryableResult(IQueryable<TResult> pQuery, string pDescription)
        {
            _query = pQuery;
            _description = pDescription;
        }
        #endregion

        #region "PUBLIC METHODS"
        public TResult Unique()
        {
        
            TResult _result = _query.First();
           return _result;
        }

        public IEnumerable<TResult> All(SortType pSort, params Expression<Func<TResult, object>>[] pOrderBy)
        {
        
            if (pOrderBy.GetLength(0) >= 1)
            {
                if (pSort == SortType.Ascending)
                {
                    _query = _query.OrderBy(pOrderBy[pOrderBy.GetLength(0) - 1]);
                }
                else { _query = _query.OrderByDescending(pOrderBy[pOrderBy.GetLength(0) - 1]); }
            }

            if (pOrderBy.GetLength(0) > 1)
            {
                for (short f = (short)(pOrderBy.GetLength(0) - 2); f >= 0; f--)
                {
                    if (pSort == SortType.Ascending)
                    {
                        _query = _query.OrderBy(pOrderBy[f]);
                    }
                    else { _query = _query.OrderByDescending(pOrderBy[f]); }
                }
            }

            IEnumerable<TResult> _result = _query.ToList<TResult>();
            return _result;
        }

        public Page<TResult> Page(short pNumber, short pSize, SortType pSort, params Expression<Func<TResult, object>>[] pOrderBy)
        {
      
            var _count = _query.Count();

            if (pOrderBy.GetLength(0) >= 1)
            {
                if (pSort == SortType.Ascending)
                {
                    _query = _query.OrderBy(pOrderBy[pOrderBy.GetLength(0) - 1]);
                }
                else { _query = _query.OrderByDescending(pOrderBy[pOrderBy.GetLength(0) - 1]); }
            }

            if (pOrderBy.GetLength(0) > 1)
            {
                for (short f = (short)(pOrderBy.GetLength(0) - 2); f >= 0; f--)
                {
                    if (pSort == SortType.Ascending)
                    {
                        _query = _query.OrderBy(pOrderBy[f]);
                    }
                    else { _query = _query.OrderByDescending(pOrderBy[f]); }
                }
            }

            IEnumerable<TResult> _result = _query.Skip(pSize * (pNumber - 1)).Take(pSize).ToList<TResult>();
            return new Page<TResult>(pNumber, pSize, _count, _result);
        }
        #endregion
    }
}