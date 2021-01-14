namespace Orion.Framework.Domains.Repositories
{
    public interface IQueryBase<TEntity> : ICriteria<TEntity> where TEntity : class {
   
        string GetOrder();

        IPager GetPager();
    }
}
