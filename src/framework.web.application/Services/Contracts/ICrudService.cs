

using Orion.Framework.Applications.Dtos;
using Orion.Framework.Applications.Services.Contracts;
using Orion.Framework.DataLayer.Queries;
using Orion.Framework.Web.Applications.Operations;

namespace Orion.Framework.Web.Applications.Services.Contracts
{



    public interface ICrudService<TDto> : ICrudService<TDto, IQueryParameter> where TDto : IDto, new()
    { }

    public interface ICrudService<TDto, in TQueryParameter> : ICrudService<TDto, TDto, TQueryParameter>
        where TDto : IDto, new()
        where TQueryParameter : IQueryParameter {
    }


    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TQueryParameter"></typeparam>
    public interface ICrudService<TDto, in TRequest, in TQueryParameter> : ICrudService<TDto, TRequest, TRequest, TRequest, TQueryParameter>
        where TDto : IDto, new()
        where TRequest : IRequest, IKey, new()
        where TQueryParameter : IQueryParameter
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TCreateRequest"></typeparam>
    /// <typeparam name="TUpdateRequest"></typeparam>
    /// <typeparam name="TQueryParameter"></typeparam>
    public interface ICrudService<TDto, in TRequest, in TCreateRequest, in TUpdateRequest, in TQueryParameter> : IQueryService<TDto, TQueryParameter>,
        ICreate<TCreateRequest>, IUpdate<TUpdateRequest>, IDelete,
        ICreateAsync<TCreateRequest>, IUpdateAsync<TUpdateRequest>, IDeleteAsync, ISaveAsync<TRequest>, IBatchSaveAsync<TDto>
        where TDto : IDto, new()
        where TRequest : IRequest, IKey, new()
        where TCreateRequest : IRequest, new()
        where TUpdateRequest : IRequest, new()
        where TQueryParameter : IQueryParameter
    {
    }




}
