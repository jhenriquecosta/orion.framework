using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Orion.Framework.Domains;
using NHibernate.Linq;
using Orion.Framework;
using Orion.Framework.DataLayer.NHibernate.Repositories;
using Orion.Framework.Domains.ValueObjects;
using Orion.Framework.DataLayer.NHibernate.Dao;

namespace Orion.Framework.Applications.Services
{
    public class LookupService<TEntity> where TEntity: IEntity<TEntity,int>
    {
        public async Task<List<DataItemCombo>> GetLookUpAsync()            
        {
            var _db = AppHelper.Resolve<DbDataContext>();
            var session = _db.GetSession;
            var getAll = await session.Query<TEntity>().ToListAsync();
            var result = getAll.Select(ToItem);
            return result.OrderBy(f => f.Text).ToList();
        }
        protected DataItemCombo ToItem(TEntity dto)
        {
            var data = new DataItemCombo
            {
                Key = dto.Id,
                Text = dto.ToString(),
                Value = dto
            };
            return data;
            
        }
    }
    public static class LookupService
    {
       
        public static async Task<List<DataItemCombo>> GetLookUpAsync<TModel>()
        {
            var _db = AppHelper.Resolve<DbDataContext>();
            var records = new List<DataItemCombo>();
            var _all = await _db.FindAllAsync<TModel>();
            foreach (var item in _all)
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
            _db.Dispose();
            var returns = records.ToList();
            return returns;
        }
    }
}
