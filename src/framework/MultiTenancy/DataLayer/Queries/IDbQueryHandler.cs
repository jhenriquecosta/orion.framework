using System.Threading;
using System.Threading.Tasks;

namespace Orion.Framework.DataLayer.Queries
{
    public interface IDbQueryHandler<TQuery, TResult> where TQuery : IDbQuery<TResult>
    {
        string InstitutionCode { get; set; }
        TResult Handle(TQuery theQuery);
    }

    public interface IDbQueryHandlerAsync<TQuery, TResult> where TQuery : IDbQueryAsync<TResult>
    {
        string InstitutionCode { get; set; }
        Task<TResult> Handle(TQuery theQuery, CancellationToken token = default(CancellationToken));
    }
}