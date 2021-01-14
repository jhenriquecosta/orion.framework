using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Orion.Framework.DataLayer.Queries;
using Orion.Framework.Domains.Repositories;
using Orion.Framework.Domains.ValueObjects;
using Orion.Framework.Web.Mvc.Properties;
using Orion.Framework.Applications.Dtos;
using Orion.Framework.Applications.Services.Contracts;

namespace Orion.Framework.Web.Mvc.Controllers
{

    public abstract class QueryControllerBase<TDto, TQuery> : WebApiControllerBase
        where TQuery : IQueryParameter
        where TDto : IDto, new() {
     
        private readonly IQueryService<TDto, TQuery> _service;

     
        protected QueryControllerBase( IQueryService<TDto, TQuery> service )
        {
            _service = service;
        }

     
        [HttpGet( "{id}" )]
        public virtual async Task<IActionResult> GetAsync( string id ) {
            var result = await _service.GetByIdAsync( id );
            return Success( result );
        }

       
        [HttpGet]
        public virtual async Task<IActionResult> PagerQueryAsync( TQuery query ) {
            PagerQueryBefore( query );
            var result = await _service.PagerQueryAsync( query );
            var data = Success( ToPagerQueryResult( result ) );
            return data;
        }

       
        protected virtual void PagerQueryBefore( TQuery query ) {
        }

    
        protected virtual dynamic ToPagerQueryResult( PagerList<TDto> result ) {
            return result;
        }

       
        [HttpGet( "Query" )]
        public virtual async Task<IActionResult> QueryAsync( TQuery query ) {
            QueryBefore( query );
            var result = await _service.QueryAsync( query );
            return Success( ToQueryResult( result ) );
        }

   
        protected virtual void QueryBefore( TQuery query ) {
        }

        
        protected virtual dynamic ToQueryResult( List<TDto> result ) {
            return result;
        }

        /// <summary>
        /// Obter a lista de itens
        /// </ summary>
        /// <param name = "query"> Parâmetros de consulta </ ​​param>      
        [HttpGet( "Items" )]
        public async Task<IActionResult> GetItemsAsync( TQuery query ) {
            if( query == null )
                return Fail( WebResource.QueryIsEmpty );
            if( query.Order.IsEmpty() )
                query.Order = "CreatedTime Desc";
            var list = await _service.PagerQueryAsync( query );
            var result = list.Data.Select( ToItem );
            return Success( result );
        }

      
        protected virtual DataItem ToItem( TDto dto ) {
            throw new NotImplementedException( ", ToItem " );
        }
    }
}
