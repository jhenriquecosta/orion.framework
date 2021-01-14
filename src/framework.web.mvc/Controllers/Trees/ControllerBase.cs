using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Orion.Framework.Infrastructurelications.Trees;
using Orion.Framework.DataLayer.Queries.Trees;
using Orion.Framework.Applications.Dtos;
using Orion.Framework.Web.Applications.Services.Trees;

namespace Orion.Framework.Web.Mvc.Controllers.Trees
{


    public abstract class ControllerBase<TDto,TQuery> : ControllerBase<TDto,TQuery,Guid?>
        where TDto : TreeDto<TDto>, new()
        where TQuery : class, ITreeQueryParameter, new()
    {
       
        protected ControllerBase(ITreeService<TDto, TQuery, Guid?> service) : base(service)
        {
        }
    }

    public abstract class ControllerBase<TDto, TQuery, TParentId> : WebApiControllerBase
        where TDto : class, ITreeNode, new()
        where TQuery : class, ITreeQueryParameter<TParentId> {
      
        private readonly ITreeService<TDto, TQuery, TParentId> _service;

        protected ControllerBase( ITreeService<TDto, TQuery, TParentId> service ) {
            _service = service;
        }

       
        protected virtual LoadMode GetLoadMode() {
            return LoadMode.Sync;
        }

       
        [HttpGet( "{id}" )]
        public virtual async Task<IActionResult> GetAsync( string id ) {
            var result = await _service.GetByIdAsync( id );
            return Success( result );
        }

       
        [HttpDelete( "{id}" )]
        public virtual async Task<IActionResult> DeleteAsync( string id ) {
            await _service.DeleteAsync( id );
            return Success();
        }

       
        [HttpPost( "delete" )]
        public virtual async Task<IActionResult> BatchDeleteAsync( [FromBody] string ids ) {
            await _service.DeleteAsync( ids );
            return Success();
        }

       
        [HttpPost( "enable" )]
        public virtual async Task<IActionResult> Enable( [FromBody] string ids ) {
            await _service.EnableAsync( ids );
            var result = await _service.FindByIdsAsync( ids );
            return Success( result );
        }

       
        [HttpPost( "disable" )]
        public virtual async Task<IActionResult> Disable( [FromBody] string ids ) {
            await _service.DisableAsync( ids );
            var result = await _service.FindByIdsAsync( ids );
            return Success( result );
        }

       
        [HttpPost( "SwapSort" )]
        public virtual async Task<IActionResult> SwapSortAsync( [FromBody] string ids ) {
            var idList = ids.ToIntList();
            if( idList.Count < 2 )
                return Fail( "" );
            await _service.SwapSortAsync( idList[0], idList[1] );
            return Success();
        }

      
        [HttpPost( "fix" )]
        public virtual async Task<IActionResult> FixAsync( [FromBody] TQuery parameter ) {
            if ( parameter == null )
                return Fail( "" );
            await _service.FixSortIdAsync( parameter );
            return Success();
        }
    }
}
