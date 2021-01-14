using System.Collections.Generic;

namespace Orion.Framework.Infrastructurelications.Operations
{
   
    public interface IGetById<TDto> where TDto : new() 
    {
     
        TDto GetById( object id );
      
        List<TDto> GetByIds( string ids );


    }
}