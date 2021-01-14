

namespace Orion.Framework.DataLayer.NHibernate.UnitOfWorks.Contracts
{ 
    public interface IUnitOfWorkRepository
    {        
        void Use(IUnitOfWork unitOfWork);       
    }
}
