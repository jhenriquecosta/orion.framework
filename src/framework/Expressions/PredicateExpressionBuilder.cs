using System;
using System.Linq.Expressions;
using Orion.Framework.Helpers;
using Orion.Framework.DataLayer.Queries;

namespace Orion.Framework.Expressions
{
    /// <summary>
    /// 
    /// </summary>
    public class PredicateExpressionBuilder<TEntity> 
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ParameterExpression _parameter;
        /// <summary>
        /// 
        /// </summary>
        private Expression _result;

        /// <summary>
        /// 
        /// </summary>
        public PredicateExpressionBuilder() {
            _parameter = Lambda.CreateParameter<TEntity>();
        }

        /// <summary>
        /// 
        /// </summary>
        public ParameterExpression GetParameter() {
            return _parameter;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <param name="operator"></param>
        /// <param name="value"></param>
        public void Append<TProperty>( Expression<Func<TEntity, TProperty>> property, Operator @operator, object value ) {
            _result = _result.And( _parameter.Property( Lambda.GetMember( property ) ).Operation( @operator, value ) );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <param name="operator"></param>
        /// <param name="value"></param>
        public void Append<TProperty>( Expression<Func<TEntity, TProperty>> property, Operator @operator, Expression value ) {
            _result = _result.And( _parameter.Property( Lambda.GetMember( property ) ).Operation( @operator, value ) );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <param name="operator"></param>
        /// <param name="value"></param>
        public void Append( string property, Operator @operator, object value ) {
            _result = _result.And( _parameter.Property( property ).Operation( @operator, value ) );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <param name="operator"></param>
        /// <param name="value"></param>
        public void Append( string property, Operator @operator, Expression value ) {
            _result = _result.And( _parameter.Property( property ).Operation( @operator, value ) );
        }

        /// <summary>
        /// 
        /// </summary>
        public void Clear() {
            _result = null;
        }

        /// <summary>
        /// 
        /// </summary>
        public Expression<Func<TEntity, bool>> ToLambda() {
            return _result.ToLambda<Func<TEntity, bool>>( _parameter );
        }
    }
}
