using Orion.Framework.DataLayer.Queries;
using Orion.Framework.Applications.Operations;

namespace Orion.Framework.Applications.Services.Contracts
{

    public interface IDeleteService<TDto, in TQueryParameter> : IQueryService<TDto, TQueryParameter>, IDelete, IDeleteAsync
        where TDto : new()
        where TQueryParameter : IQueryParameter {
    }
}
