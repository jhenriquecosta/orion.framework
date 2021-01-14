namespace Orion.Framework.DataLayer.NHibernate.UnitOfWorks.Contracts
{
    public interface IUnitOfWorkFactory
    {
        
        IUnitOfWork New();
        void Remove(string uowName);
        IUnitOfWork Get(string uowName);
    }
}
