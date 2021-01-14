using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orion.Framework.DataLayer.NHibernate.UnitOfWorks.Contracts

{
   
    public class UnitOfWorkManager : IUnitOfWorkManager 
    {
       
        private readonly List<IUnitOfWorkBase> _unitOfWorks;

     
        public UnitOfWorkManager()
        {
            _unitOfWorks = new List<IUnitOfWorkBase>();
        }

        public void BeginTransaction()
        {
            //foreach (var unitOfWork in _unitOfWorks)
            //    unitOfWork.BeginTransaction();
        }

        public void Commit()
        {
            foreach( var unitOfWork in _unitOfWorks )
                unitOfWork.Commit();
        }

       
        public async Task CommitAsync() 
        {
            foreach ( var unitOfWork in _unitOfWorks )
                await unitOfWork.CommitAsync();
        }
        public async Task RollbackAsync()
        {
            foreach (var unitOfWork in _unitOfWorks)
                await unitOfWork.CommitAsync();
        }

        public void Register( IUnitOfWorkBase unitOfWork ) 
        {
            if( unitOfWork == null )
                throw new ArgumentNullException( nameof( unitOfWork ) );
            if( _unitOfWorks.Contains( unitOfWork ) == false )
                _unitOfWorks.Add( unitOfWork );
        }

       
    }
}
