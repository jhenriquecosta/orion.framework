using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Orion.Framework.Domains;

namespace Orion.Framework.MultiTenancy.DataLayer.CoreDAO
{

    public partial interface ICoreReadsDAO<T> : ICoreReadsDAO<T, int> where T : IEntity<int>, IKey<int>
    {

    }
    public partial interface ICoreReadsDAO<T, idT> : ICoreGeneralDAO where T : IEntity<idT>, IKey<idT> 
    {
        T Load(idT id);
        T FirstOrDefault(idT id);
        IQueryable<T> AsQueryable(Expression<Func<T, bool>> filter = null);
        IList<T> FindAll(Expression<Func<T, bool>> filter = null);
        T FirstOrDefault(Expression<Func<T, bool>> filter);
        bool Any(Expression<Func<T, bool>> filter);
        int Count(Expression<Func<T, bool>> filter = null);
    }

   
}
