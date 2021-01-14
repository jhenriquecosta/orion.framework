using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Criterion;

namespace Orion.Framework.DataLayer.NH.QueryObjects
{
    public class CriteriaResult<TResult> : IQueryResult<TResult> where TResult : class
    {
        #region "PRIVATE MEMBERS"
        private ICriteria _query;
        private string _description;
        #endregion

        #region "PROPERTIES"
        public string Description
        {
            get { return _description; }
        }
        #endregion

        #region "CONSTRUCTORS"
        public CriteriaResult(ICriteria pQuery, string pDescription)
        {
            _query = pQuery;
            _description = pDescription;
        }
        #endregion

        #region "PUBLIC METHODS"
        public TResult Unique()
        {
            TResult _result = _query.UniqueResult<TResult>();
            return _result;
        }

        public IEnumerable<TResult> All(SortType pSort, params Expression<Func<TResult, object>>[] pOrderBy)
        {
          
            for (short f = 0; f < pOrderBy.GetLength(0); f++)
            {
                if (pSort == SortType.Ascending)
                {
                    string _name = (pOrderBy[f].Body as UnaryExpression) != null ? (((pOrderBy[f].Body as UnaryExpression).Operand as System.Linq.Expressions.MemberExpression).Member.Name) : (pOrderBy[f].Body as MemberExpression).Member.Name;
                    _query = _query.AddOrder(new Order(_name, true));
                }
                else
                {
                    string _name = (pOrderBy[f].Body as UnaryExpression) != null ? (((pOrderBy[f].Body as UnaryExpression).Operand as System.Linq.Expressions.MemberExpression).Member.Name) : (pOrderBy[f].Body as MemberExpression).Member.Name;
                    _query = _query.AddOrder(new Order(_name, false));
                }
            }

            IEnumerable<TResult> _result = _query.List<TResult>();
            return _result;
        }

        public Page<TResult> Page(short pNumber, short pSize, SortType pSort, params Expression<Func<TResult, object>>[] pOrderBy)
        {
         
            var _count = ((ICriteria)_query.Clone()).SetProjection(Projections.Count(Projections.Id())).FutureValue<Int32>();

            for (short f = 0; f < pOrderBy.GetLength(0); f++)
            {
                if (pSort == SortType.Ascending)
                {
                    string _name = (pOrderBy[f].Body as UnaryExpression) != null ? (((pOrderBy[f].Body as UnaryExpression).Operand as System.Linq.Expressions.MemberExpression).Member.Name) : (pOrderBy[f].Body as MemberExpression).Member.Name;
                    _query = _query.AddOrder(new Order(_name, true));
                }
                else
                {
                    string _name = (pOrderBy[f].Body as UnaryExpression) != null ? (((pOrderBy[f].Body as UnaryExpression).Operand as System.Linq.Expressions.MemberExpression).Member.Name) : (pOrderBy[f].Body as MemberExpression).Member.Name;
                    _query = _query.AddOrder(new Order(_name, false));
                }
            }
            IEnumerable<TResult> _result = _query.SetMaxResults(pSize).SetFirstResult((pNumber - 1) * pSize).List<TResult>();

            return new Page<TResult>(pNumber, pSize, _count.Value, _result);
        }
        #endregion

    }
}