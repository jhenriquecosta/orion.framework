using System.Linq;

namespace Framework.DataLayer.NHibernate.Fetching
{
    public interface IFetchRequest<TQueried, TFetch> : IOrderedQueryable<TQueried>
    {
    }
}
