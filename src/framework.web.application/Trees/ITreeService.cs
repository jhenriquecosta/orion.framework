using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orion.Framework.Infrastructurelications.Trees;
using Orion.Framework.DataLayer.Queries.Trees;
using Orion.Framework.Web.Applications.Services.Contracts;

namespace Orion.Framework.Web.Applications.Services.Trees
{

    public interface ITreeService<TDto, in TQueryParameter> : ITreeService<TDto, TQueryParameter, Guid?>
        where TDto : class, ITreeNode, new()
        where TQueryParameter : class, ITreeQueryParameter {
    }

   
    public interface ITreeService<TDto, in TQueryParameter, TParentId> : IDeleteService<TDto, TQueryParameter>
        where TDto : class, ITreeNode, new()
        where TQueryParameter : class, ITreeQueryParameter<TParentId> {
     
        Task<List<TDto>> FindByIdsAsync( string ids );
      
        Task EnableAsync( string ids );
       
        Task DisableAsync( string ids );
      
        Task SwapSortAsync( int id, int swapId );
       
        Task FixSortIdAsync( TQueryParameter parameter );
    }
}