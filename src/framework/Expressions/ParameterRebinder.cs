using System.Collections.Generic;
using System.Linq.Expressions;

namespace Orion.Framework.Expressions {
    /// <summary>
    /// 
    /// </summary>
    public class ParameterRebinder : ExpressionVisitor {
        /// <summary>
        /// 
        /// </summary>
        private readonly Dictionary<ParameterExpression, ParameterExpression> _map;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="map"></param>
        public ParameterRebinder( Dictionary<ParameterExpression, ParameterExpression> map ) {
            _map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="map"></param>
        /// <param name="exp"></param>
        public static Expression ReplaceParameters( Dictionary<ParameterExpression, ParameterExpression> map, Expression exp ) {
            return new ParameterRebinder( map ).Visit( exp );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameterExpression"></param>
        protected override Expression VisitParameter( ParameterExpression parameterExpression ) {
            if( _map.TryGetValue( parameterExpression, out var replacement ) )
                parameterExpression = replacement;
            return base.VisitParameter( parameterExpression );
        }
    }
}
