using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orion.Framework.DataLayer.NH.Fluent.Conventions
{
    public class CustomManyToManyTableNameConvention : ManyToManyTableNameConvention
    {
        protected override string GetBiDirectionalTableName(IManyToManyCollectionInspector collection, IManyToManyCollectionInspector otherSide)
        {
            return collection.EntityType.Name + "_" + otherSide.EntityType.Name;
        }

        protected override string GetUniDirectionalTableName(IManyToManyCollectionInspector collection)
        {
            return collection.EntityType.Name + "_" + collection.ChildType.Name;
        }
    }
    public class HasManyToManyConvention
    {

    }
    public class ManyToManyConvention : IHasManyToManyConvention
    {
        public void Apply(IManyToManyCollectionInstance instance)
        {

            instance.Key.ForeignKey(string.Format("{0}{1}{2}{3}",
                   "FK_", instance.TableName,
                   "_",
                  instance.EntityType.Name));
            instance.Key.Column(instance.EntityType.Name + "ID");
            instance.Relationship.Column(instance.OtherSide.EntityType.Name + "ID");
        }
    } 
}
