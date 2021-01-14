using Orion.Framework.Infrastructurelications.Operations;
using Orion.Framework.DataLayer.Queries;

namespace Orion.Framework.Applications.Services.Contracts
{

    public interface IQueryService<TDto, in TQueryParameter> : IService,
        IGetById<TDto>, IGetByIdAsync<TDto>,       
        IGetAll<TDto>, IGetAllAsync<TDto>,
        IPageQuery<TDto, TQueryParameter>, IPageQueryAsync<TDto, TQueryParameter>
        where TDto : new()
        where TQueryParameter : IQueryParameter 
    {
    }
   

}