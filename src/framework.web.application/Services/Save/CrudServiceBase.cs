
using Orion.Framework.DataLayer.Queries;
using Orion.Framework.DataLayer.UnitOfWorks.Contracts;
using Orion.Framework.Web.Applications.Aspects;
using Orion.Framework.Web.Applications.Services.Contracts;
using Orion.Framework.Domains;
using Orion.Framework.Domains.Repositories;
using Orion.Framework.Maps;
using Orion.Framework.Applications.Dtos;

namespace Orion.Framework.Web.Applications.Services
{

    /// <summary>
    /// CRUD BASE
    /// </summary>
    /// <typeparam name="TEntity">Entity</typeparam>
    /// <typeparam name="TDto">Dto</typeparam>
    /// <typeparam name="TQueryParameter">Query Parameters</typeparam>
    public abstract class CrudServiceBase<TEntity, TDto, TQueryParameter> : 
        CrudServiceBase<TEntity, TDto, TDto, TDto, TDto, TQueryParameter, int>, ICrudService<TDto, TQueryParameter>
        where TEntity : class, IAggregateRoot<TEntity, int>, new()
        where TDto : IDto, new()
        where TQueryParameter : IQueryParameter
        {
          
        protected CrudServiceBase(IRepository<TEntity, int> repository ) : base(repository )
        {
            
        }
        //protected CrudServiceBase(IRepository<TEntity, int> repository) : this((IUnitOfWork)repository.UnitOfWork, repository)
        //{

        //}

    }

    /// <summary>
    /// CRUD BASE
    /// </summary>
    /// <typeparam name="TEntity">Entity</typeparam>
    /// <typeparam name="TDto">Dto</typeparam>
    /// <typeparam name="TQueryParameter">Query Parameters</typeparam>
    /// <typeparam name="TKey">Tipo da Primary Key</typeparam>
    
    public abstract class CrudServiceBase<TEntity, TDto, TQueryParameter, TKey> :
        CrudServiceBase<TEntity, TDto, TDto, TQueryParameter, TKey>, 
        ICrudService<TDto, TQueryParameter>
        where TEntity : class, IAggregateRoot<TEntity, TKey>, new()
        where TDto : IDto, new()
        where TQueryParameter : IQueryParameter {
       
        protected CrudServiceBase( IRepository<TEntity, TKey> repository ) : base(  repository ) 
        {
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TQueryParameter"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class CrudServiceBase<TEntity, TDto, TRequest, TQueryParameter, TKey> :
        CrudServiceBase<TEntity, TDto, TRequest, TRequest, TRequest, TQueryParameter, TKey>,
        ICrudService<TDto, TRequest, TQueryParameter>
        where TEntity : class, IAggregateRoot<TEntity, TKey>, new()
        where TDto : IDto, new()
        where TRequest : IRequest, IKey, new()
        where TQueryParameter : IQueryParameter {
       
        protected CrudServiceBase(  IRepository<TEntity, TKey> repository ) : base(  repository )
        {
        }
    }


    /// <summary>
    /// CRUD/DELETE SERVICE
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TCreateRequest"></typeparam>
    /// <typeparam name="TUpdateRequest"></typeparam>
    /// <typeparam name="TQueryParameter"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract partial class CrudServiceBase<TEntity, TDto, TRequest, TCreateRequest, TUpdateRequest, TQueryParameter, TKey> :
        DeleteServiceBase<TEntity, TDto, TQueryParameter, TKey>,
        ICrudService<TDto, TRequest, TCreateRequest, TUpdateRequest, TQueryParameter>, ICommitAfter
        where TEntity : class, IAggregateRoot<TEntity, TKey>, new()
        where TDto : IDto, new()
        where TRequest : IRequest, IKey, new()
        where TCreateRequest : IRequest, new()
        where TUpdateRequest : IRequest, new()
        where TQueryParameter : IQueryParameter
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<TEntity,TKey> _repository;
    
        protected CrudServiceBase( IRepository<TEntity, TKey> repository ) : base( repository ) 
        {            
            _unitOfWork = (IUnitOfWork)repository.UnitOfWork;
            _repository = repository;
        }
        //protected CrudServiceBase(IRepository<TEntity, TKey> repository) : this((IUnitOfWork)repository.UnitOfWork, repository)
        //{
          
        //}

        protected virtual TEntity ToEntity( TRequest request ) {
            return request.MapTo<TEntity>();
        }

        
        protected virtual TEntity ToEntityFromCreateRequest( TCreateRequest request ) {
            if( typeof( TCreateRequest ) == typeof( TRequest ) )
                return ToEntity( Orion.Framework.Helpers.TypeConvert.To<TRequest>( request ) );
            return request.MapTo<TEntity>();
        }

        protected virtual TEntity ToEntityFromUpdateRequest( TUpdateRequest request ) {
            if( typeof( TUpdateRequest ) == typeof( TRequest ) )
                return ToEntity( Orion.Framework.Helpers.TypeConvert.To<TRequest>( request ) );
            return request.MapTo<TEntity>();
        }
        protected virtual TEntity ToEntityFromDto(TDto request)
        {
            if (typeof(TDto) == typeof(TRequest))
                return ToEntity(Orion.Framework.Helpers.TypeConvert.To<TRequest>(request));
            return request.MapTo<TEntity>();
        }
    }
}
