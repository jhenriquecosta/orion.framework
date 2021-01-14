using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Orion.Framework.Infrastructurelications.Operations
{

    public interface IFirstOrDefaultAsync<TDto> where TDto : new()
    {
        Task<TDto> FirstOrDefaultAsync(Expression<Func<TDto, bool>> predicate);
    }
}
