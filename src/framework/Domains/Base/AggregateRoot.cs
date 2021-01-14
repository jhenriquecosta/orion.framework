using Orion.Framework.Domains.Attributes;

namespace Orion.Framework.Domains
{

   

    
    public abstract class AggregateRoot<TEntity> : AggregateRoot<TEntity, int> where TEntity : IAggregateRoot
    {
        
    }

    public abstract class AggregateRoot<TEntity, TKey> : EntityBase<TEntity, TKey>, IAggregateRoot<TEntity, TKey> where TEntity : IAggregateRoot
    {

        [ModelField(Ignore = true)]
        public virtual int Version
        {
            get; set;
        }

    }
}


