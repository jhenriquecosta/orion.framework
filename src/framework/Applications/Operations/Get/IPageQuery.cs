using System.Collections.Generic;
using Orion.Framework.DataLayer.Queries;
using Orion.Framework.Domains.Repositories;

namespace Orion.Framework.Infrastructurelications.Operations
{

    public interface IPageQuery<TDto, in TQueryParameter>
        where TDto : new()
        where TQueryParameter : IQueryParameter {
    
        List<TDto> Query( TQueryParameter parameter );
       
        PagerList<TDto> PagerQuery( TQueryParameter parameter );
    }
}