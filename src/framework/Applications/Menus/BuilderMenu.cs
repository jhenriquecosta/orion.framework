using System;
using System.Collections.Generic;
using System.Linq;
using Orion.Framework;
using Orion.Framework.Domains.ValueObjects;

namespace Orion.Framework.Infrastructurelications.Menus
{
   
    public class BuilderMenu
    {
        private List<DataMenuItem> _menuItems;
        private DataMenuItem _lastItem;
        private DataMenuItem _lastMenu;
        private DataMenuItem _lastRoot;
        private DataMenuItem _lastSubMenu;



        public BuilderMenu()
        {
            _menuItems = new List<DataMenuItem>();
        }
        public BuilderMenu AddItem(string text, string home, string component, string parameter = "", string icon = "")
        {
            var id = _lastItem.Ancestral;
            if (_lastItem.HasChild) id = _lastItem.Id;
            var item = Create(id, text, home, component, parameter, icon);
            return this;
        }
        public BuilderMenu AddMenu(string text, string icon = "")
        {
            _lastMenu = Create(_lastRoot.Id, text, icon);
            return this;
        }
        public BuilderMenu AddRootMenu(string text, string home = "",string component="",string parameters="",string icon="")
        {
            _lastRoot = Create(null, text, home, component, parameters, icon);
            return this;
        }
        public BuilderMenu AddSubMenu(string text, string icon = "")
        {
            if (_lastItem == _lastSubMenu)
            {
                _lastMenu = _lastItem;
            }
            _lastSubMenu = Create(_lastMenu.Id, text, icon);
            return this;
        }
        private DataMenuItem Create(int? ancestral, string text, string icon)
        {
            return Create(ancestral, text, icon, "", "", "", "");
        }
        private DataMenuItem Create(int? ancestral, string text, string home = "", string component = "", string parameter = "", string description = "", string icon = "")
        {
            var url = string.Empty;
            if (!home.IsNullOrWhiteSpace())
            {
                url = $"{home}/{component}";
            }
            var id = _menuItems.Any() ? _menuItems.Max(f => f.Id) + 1 : 1;
            var count = _menuItems.Count(f => f.Ancestral == ancestral);
            var path = "";
            var caption = text;
            if (count > 0)
            {
                path = count.ToString();
                caption = $"{path}-{text}";
            }
            if (ancestral != null)
            {
                if (url.IsNullOrWhiteSpace())
                {
                    count = _menuItems.Count(f => f.Ancestral == ancestral && f.HasChild == true);
                }
                var parent = _menuItems.FirstOrDefault(f => f.Id == ancestral);
                count++;
                path = $"{parent.Path}.{count}";
                caption = $"{path}-{text}";
            }

            var record = new DataMenuItem
            {
                Id = id,
                Ancestral = ancestral,
                Text = text,
                Url = url,
                Home = home,
                Component = component,
                Parameter = parameter,
                Description = description,
                Path = path.ToString(),
                Caption = caption,
                HasChild = url.IsNullOrEmpty()
            };
            _menuItems.Add(record);
            _lastItem = record;
            return record;
        }

        public List<DataMenuItem> Build(Func<DataMenuItem, int> orderBy)
        {
            var menuItems = _menuItems.OrderBy(orderBy);

            return menuItems.ToList();
        }
    }
}
