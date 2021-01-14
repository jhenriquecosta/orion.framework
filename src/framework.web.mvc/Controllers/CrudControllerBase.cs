using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Orion.Framework.DataLayer.Queries;
using Orion.Framework.Web.Mvc.Models;
using Orion.Framework.Web.Mvc.Properties;
using Orion.Framework.Applications.Dtos;
using Orion.Framework.Applications.Services.Contracts;
using Orion.Framework.Web.Applications.Services.Contracts;

namespace Orion.Framework.Web.Mvc.Controllers
{
    /// <summary>
    /// Crud
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TQuery"></typeparam>
    public abstract class CrudControllerBase<TDto, TQuery> : CrudControllerBase<TDto, TDto, TDto, TQuery>
        where TQuery : IQueryParameter
        where TDto : IDto, new()
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="service">Crud</param>
        protected CrudControllerBase(ICrudService<TDto, TQuery> service): base(service)
        {
        }
    }

    /// <summary>
    /// Crud
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TQuery"></typeparam>
    public abstract class CrudControllerBase<TDto, TRequest, TQuery> : CrudControllerBase<TDto, TRequest, TRequest, TQuery>
        where TQuery : IQueryParameter
        where TRequest : IRequest, IKey, new()
        where TDto : IDto, new()
    {
        /// <summary>
        ///Crud
        /// </summary>
        /// <param name="service">Crud</param>
        protected CrudControllerBase(ICrudService<TDto, TRequest, TQuery> service)
            : base(service)
        {
        }
    }

    /// <summary>
    /// Crud
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TCreateRequest"></typeparam>
    /// <typeparam name="TUpdateRequest"></typeparam>
    /// <typeparam name="TQuery"></typeparam>
    public abstract class CrudControllerBase<TDto, TCreateRequest, TUpdateRequest, TQuery> : QueryControllerBase<TDto, TQuery>
        where TQuery : IQueryParameter
        where TCreateRequest : IRequest, new()
        where TUpdateRequest : IRequest, IKey, new()
        where TDto : IDto, new()
    {
        /// <summary>
        /// Crud
        /// </summary>
        private readonly ICrudService<TDto, TUpdateRequest, TCreateRequest, TUpdateRequest, TQuery> _service;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        protected CrudControllerBase(ICrudService<TDto, TUpdateRequest, TCreateRequest, TUpdateRequest, TQuery> service)
            : base(service)
        {
            _service = service;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// 
        /// POST
        /// /api/customer
        /// </remarks>
        /// <param name="request"></param>
        [HttpPost]
        public virtual async Task<IActionResult> CreateAsync([FromBody] TCreateRequest request)
        {
            if (request == null)
                return Fail(WebResource.CreateRequestIsEmpty);
            CreateBefore(request);
            var id = await _service.CreateAsync(request);
            var result = await _service.GetByIdAsync(id);
            return Success(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        protected virtual void CreateBefore(TCreateRequest dto)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <remarks>
        /// 
        /// PUT
        /// /api/customer/1
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="request"></param>
        [HttpPut("{id?}")]
        public virtual async Task<IActionResult> UpdateAsync(string id, [FromBody] TUpdateRequest request)
        {
            if (request == null)
                return Fail(WebResource.UpdateRequestIsEmpty);
            if (id.IsEmpty() && request.Id.IsEmpty())
                return Fail(WebResource.IdIsEmpty);
            if (request.Id.IsEmpty())
                request.Id = id;
            UpdateBefore(request);
            await _service.UpdateAsync(request);
            var result = await _service.GetByIdAsync(request.Id);
            return Success(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        protected virtual void UpdateBefore(TUpdateRequest dto)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// 
        /// DELETE
        /// /api/customer/1
        /// </remarks>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> DeleteAsync(string id)
        {
            await _service.DeleteAsync(id);
            return Success();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// :
        /// POST   
        /// /api/customer/delete
        /// body: "'1,2,3'"
        /// </remarks>
        /// <param name="ids"></param>
        [HttpPost("delete")]
        public virtual async Task<IActionResult> BatchDeleteAsync([FromBody] string ids)
        {
            await _service.DeleteAsync(ids);
            return Success();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        [HttpPost("save")]
        public virtual async Task<IActionResult> SaveAsync([FromBody] SaveModel request)
        {
            if (request == null)
                return Fail(WebResource.RequestIsEmpty);
            var creationList = Orion.Framework.Json.Json.ToObject<List<TDto>>(request.CreationList);
            var updateList = Orion.Framework.Json.Json.ToObject<List<TDto>>(request.UpdateList);
            var deleteList = Orion.Framework.Json.Json.ToObject<List<TDto>>(request.DeleteList);
            await _service.SaveAsync(creationList, updateList, deleteList);
            return Success();
        }
    }

}
