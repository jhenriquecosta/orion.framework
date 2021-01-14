namespace Orion.Framework.DataLayer.NH.QueryObjects
{
    public interface IQuery<TResult, IDBContext>  where TResult : class  where IDBContext : class
    {
        string Description { get; }

        IQueryResult<TResult> Execute(IDBContext pContext);
    }
}