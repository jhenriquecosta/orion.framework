
using Orion.Framework.Domains.Repositories;

namespace Orion.Framework.DataLayer.Queries {
  
    public interface IQueryParameter : IPager {
       
        string Keyword { get; set; }
    }
}
