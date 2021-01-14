using Orion.Framework.Domains;
using Orion.Framework.Domains.ValueObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Framework.Webs.Applications.A
{
    public class DataCrudService<TEntity> : NhDataOperation<TEntity> where TEntity : class
    {
        public async Task<List<DataItemCombo>> GetLookUpAsync()
        {
           
            var getAll = await base.FindAllAsync();          
            var records = new List<DataItemCombo>();
            foreach (var item in getAll)
            {
                var record = item as IEntity<int>;
                var data = new DataItemCombo
                {
                    Key = record.Id,
                    Text = record.ToString(),
                    Value = record
                };
                records.Add(data);
            }
            return records;
        }
    }
   
}
