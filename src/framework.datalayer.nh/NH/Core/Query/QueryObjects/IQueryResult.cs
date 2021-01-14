using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Orion.Framework.DataLayer.NH.QueryObjects
{
    public enum SortType
    {
        Ascending,
        Descending
    }

    public interface IQueryResult<TResult> where TResult : class
    {
        string Description { get; }

        TResult Unique();

        IEnumerable<TResult> All(SortType pSort, params Expression<Func<TResult, object>>[] pOrderBy);

        Page<TResult> Page(short pNumber, short pSize, SortType pSort, params Expression<Func<TResult, object>>[] pOrderBy);
    }
}