using System;
using System.Linq.Expressions;

namespace Orion.Framework.DataLayer.Sql.Builders {
    /// <summary>
    /// 
    /// </summary>
    public interface IEntityResolver {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        string GetTable( Type entity );
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        string GetSchema( Type entity );
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="propertyAsAlias"></param>
        string GetColumns<TEntity>( bool propertyAsAlias );
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="columns"></param>
        /// <param name="propertyAsAlias"></param>
        string GetColumns<TEntity>( Expression<Func<TEntity, object[]>> columns, bool propertyAsAlias );
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="column"></param>
        string GetColumn<TEntity>( Expression<Func<TEntity, object>> column );
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="entity"></param>
        /// <param name="right"></param>
        string GetColumn( Expression expression, Type entity, bool right = false );
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="right"></param>
        Type GetType( Expression expression, bool right = false );
    }
}
