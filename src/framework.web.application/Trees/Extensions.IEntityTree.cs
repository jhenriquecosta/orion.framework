using Orion.Framework.DataLayer.NH.Dao;
using Orion.Framework.Domains;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Orion.Framework
{
    public static class IEntityTreeExtensions
    {
        public static string ApplyFormat(int level, int value, string path, string format)
        {
            if (level == 0)
            {
                path = format;
            }
            var mask = path.Split('.');
            for (int i = 0; i < mask.Length; i++)
            {
                if (i == level)
                {
                    mask[i] = value.ToString(mask[i]);
                }

            }
            return string.Join(".", mask);
        }
        public static async Task<TModel> AddItemAsync<TModel>(this IEntityTree entity, int? parent, Expression<Func<TModel, bool>> filter, string format = null) where TModel : class, IEntityTree, new()
        {
            var _ordem = 0;

            var dataService = AppHelper.Resolve<DbDataContext>();
            var _path = "";
            var _level = 0;
            var _count = await dataService.CountAsync<TModel>(filter);
            _count++;
            var nivel = _count;
            _ordem = nivel * 1000;
            _path = $"{_count}";
            if (parent != null)
            {

                var _parent = await dataService.LoadAsync<TModel>(parent);
                _ordem = (_parent.Ordem + _count) + 1;
                _level = _parent.Level;
                _path = _parent.Path;
                _path = $"{_path}.{_count}";
            }

            if (format != null) _path = ApplyFormat(_level, _count, _path, format);
            var record = new TModel();
            _level++;
            record.InitPath(_path, _level);
            record.Ancestral = parent;
            record.Ordem = _ordem;
            return record;
        }
       
    }
}
