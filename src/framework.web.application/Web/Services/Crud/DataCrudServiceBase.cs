namespace Framework.Webs.Applications
{
    public abstract class DataCrudServiceBase<TEntity> :   DataCrudService<TEntity>, IDataCrudService<TEntity> where TEntity : class
    {
       
    }
   
}
