namespace Orion.Framework.Domains 
{
   
    public interface IEntity : IDomainObject
    {
        void Init();
        bool IsTransient();
    }

    public interface IEntity<out TKey> : IKey<TKey>, IEntity
    {

    }

 
    public interface IEntity<in TEntity, out TKey> : ICompareChange<TEntity>, IEntity<TKey> where TEntity : IEntity
    {
    }
}
