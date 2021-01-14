using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;

namespace Orion.Framework.DataLayer.NH.Fluent.Conventions
{
    /// <summary>
    /// ForeignKeyColumnNameConvention class.
    /// </summary>
    public class ForeignKeyColumnNameConvention : IReferenceConvention, IReferenceConventionAcceptance, IComponentConvention, IHasManyConvention
    {
        #region Public Methods

        /// <summary>
        /// Accepts the specified criteria if is component.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        public void Accept ( IAcceptanceCriteria<IManyToOneInspector> criteria )
        {
            criteria.Expect ( c => ! c.EntityType.IsNHibernateComponent () );
        }

        /// <summary>
        /// Applies Foriegn key column name based on type.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void Apply ( IManyToOneInstance instance )
        {
            // name the key field
            var key = instance.Property.Name;

            //if ( typeof( ILookup ).IsAssignableFrom ( instance.Property.PropertyType ) )
            //{
            //    key = key + "_Lkp";
            //}
            key = $"{key.ToPascalCaseWithUndescore()}_Id";
            key = key.ToLower();
           
            instance.Column ( key  );
        }

        /// <summary>
        /// Applies Foriegn key column name based on type.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void Apply ( IComponentInstance instance )
        {
            foreach ( var manyToOneInspector in instance.References )
            {
                var namingStrategy = instance.GetNamingStrategy ();
                var columnName = namingStrategy.GetColumnName ( instance.Property, manyToOneInspector.Property, false );
                columnName = $"{columnName.ToPascalCaseWithUndescore()}_Id";
                columnName = columnName.ToLower();
                manyToOneInspector.Column ( columnName);
            }
        }

        /// <summary>
        /// Applies Key based on type.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void Apply(IOneToManyCollectionInstance instance)
        {
            var entityType = instance.EntityType;

            // name the key field
            var key = entityType.Name;
            key = $"{key.ToPascalCaseWithUndescore()}_Id";
            key = key.ToLower();

            //var type = instance.ChildType;
            //var schema = AppHelper.GetCache<string>(type.FullName);
            //if (!schema.IsEmpty()) instance.Schema(schema);

            instance.Key.Column(key);
            instance.Inverse();
        }

        #endregion
    }
}
