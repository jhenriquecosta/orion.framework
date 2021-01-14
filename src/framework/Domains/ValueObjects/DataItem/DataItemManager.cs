using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Orion.Framework.Domains.ValueObjects
{
    public class DataItemManager
    {
        private List<DataItem> _items = new List<DataItem>();
        public DataItemManager(List<DataItem> dataItems)
        {
            _items = dataItems;
        }
        public DataItemManager( )
        {
            _items = new List<DataItem>();
        }
            
        public DataItemManager Add(DataItem item)
        {
            if (_items.IsNull()) _items = new List<DataItem>();
            if (_items.Any(f => f.Text.Equals(item.Text)))
            {
                return this;
            }
            _items.Add(item);
            return this;
        }
        public DataItemManager Add(string name,object value)
        {
            if (_items.IsNull()) _items = new List<DataItem>();
            var item = new DataItem();
            item.Text = name;
            item.Value = value;
            Add(item);
            return this;
        }
        public List<DataItem> Get()
        {
            return _items;
        }
    }
}
