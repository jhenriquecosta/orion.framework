using System;
using System.Collections.Generic;

namespace Orion.Framework.DataLayer.Sql.Builders.Core {
    /// <summary>
    /// 
    /// </summary>
    public class EntityAliasRegister : IEntityAliasRegister {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public EntityAliasRegister( IDictionary<Type, string> data = null ) {
            Data = data ?? new Dictionary<Type, string>();
        }

        /// <summary>
        /// 
        /// </summary>
        public Type FromType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IDictionary<Type, string> Data { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="alias"></param>
        public void Register( Type entity, string alias ) {
            Data[entity] = alias;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public bool Contains( Type entity ) {
            if ( entity == null )
                return false;
            return Data.ContainsKey( entity );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public string GetAlias( Type entity ) {
            if ( entity == null )
                return null;
            if ( Data.ContainsKey( entity ) )
                return Data[entity];
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        public IEntityAliasRegister Clone() {
            return new EntityAliasRegister( new Dictionary<Type, string>( Data ) );
        }
    }
}
