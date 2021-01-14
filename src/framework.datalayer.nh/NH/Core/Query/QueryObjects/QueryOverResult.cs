using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Orion.Framework.DataLayer.NH.QueryObjects
{
    public class QueryOverResult<TResult> : IQueryResult<TResult> where TResult : class
    {
        #region "PRIVATE MEMBERS"
        private IQueryOver<TResult, TResult> _query;
        private string _description;
        #endregion

        #region "PROPERTIES"
        public string Description
        {
            get { return _description; }
        }
        #endregion

        #region "CONSTRUCTORS"
        public QueryOverResult(IQueryOver<TResult, TResult> pQuery, string pDescription)
        {
            _query = pQuery;
            _description = pDescription;
        }
        #endregion

        #region "PUBLIC METHODS"
        public TResult Unique()
        {
           TResult _result = _query.SingleOrDefault<TResult>();
            return _result;
        }

        public IEnumerable<TResult> All(SortType pSort, params Expression<Func<TResult, object>>[] pOrderBy)
        {
             for (short f = 0; f < pOrderBy.GetLength(0); f++)
            {
                if (pSort == SortType.Ascending)
                {
                    _query = _query.OrderBy(pOrderBy[f]).Asc();
                }
                else
                {
                    _query = _query.OrderBy(pOrderBy[f]).Desc();
                }
            }

            IEnumerable<TResult> _result = _query.List<TResult>();
            return _result;
        }

        public Page<TResult> Page(short pNumber, short pSize, SortType pSort, params Expression<Func<TResult, object>>[] pOrderBy)
        {
            var _count = ((IQueryOver<TResult, TResult>)_query.Clone()).Select(Projections.RowCount()).FutureValue<Int32>();

            for (short f = 0; f < pOrderBy.GetLength(0); f++)
            {
                if (pSort == SortType.Ascending)
                {
                    _query = _query.OrderBy(pOrderBy[f]).Asc();
                }
                else
                {
                    _query = _query.OrderBy(pOrderBy[f]).Desc();
                }
            }

            IEnumerable<TResult> _result = _query.Skip(pSize * (pNumber - 1)).Take(pSize).List<TResult>();
            return new Page<TResult>(pNumber, pSize, _count.Value, _result);
        }
        #endregion
    }
}