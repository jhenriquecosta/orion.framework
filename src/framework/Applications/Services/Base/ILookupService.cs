using System.Collections.Generic;
using System.Threading.Tasks;
using Orion.Framework.DataLayer.NH.Contracts;
using Orion.Framework.Domains.ValueObjects;

namespace Orion.Framework.Applications.Services.Contracts
{
    /// <summary>
    /// 
    /// </summary>
    public interface ILookUpService : ILookUp 
    {
        Task<List<DataItem>> GetLookUpAsync();
    }
}
