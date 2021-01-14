using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Orion.Framework.Infrastructurelications.Operations
{
  
    public interface IFirstOrDefault<TDto> where TDto : new() {
     
        TDto  FirstOrDefault(Expression<Func<TDto, bool>> predicate);
    }
}
