using System.Collections.Generic;


namespace Orion.Framework.Infrastructurelications.Operations
{
  
    public interface IGetAll<TDto> where TDto : new()
    {
        List<TDto> GetAll();
    }
}
