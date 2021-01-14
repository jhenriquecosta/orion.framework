using System.Collections.Generic;
using System.Threading.Tasks;
using Orion.Framework.DataLayer.Queries;

using Orion.Framework.Domains.Repositories;

namespace Orion.Framework.Infrastructurelications.Operations
{

    public interface IPageQueryAsync<TDto, in TQueryParameter>
        where TDto : new()
        where TQueryParameter : IQueryParameter {

     
        Task<List<TDto>> QueryAsync( TQueryParameter parameter );
       
        Task<PagerList<TDto>> PagerQueryAsync( TQueryParameter parameter );
    }
}