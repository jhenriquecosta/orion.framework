using Orion.Framework.Applications.Services.Contracts;
using Orion.Framework.DataLayer.Queries;
using Orion.Framework.Web.Applications.Operations;

namespace Orion.Framework.Web.Applications.Services.Contracts
{

    public interface IDeleteService<TDto, in TQueryParameter> : IQueryService<TDto, TQueryParameter>, IDelete, IDeleteAsync
        where TDto : new()
        where TQueryParameter : IQueryParameter {
    }
}
