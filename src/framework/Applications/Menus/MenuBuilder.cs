using System;
using System.Collections.Generic;
using System.Linq;

namespace Orion.Framework
{
    public class MenuBuilder
    {
        private List<MenuElement> _menuItems;

        public MenuBuilder()
        {
            _menuItems = new List<MenuElement>();
        }
        public int TotalItem()
        {
            return _menuItems.Count();
        }
        public List<MenuElement> GetItems()
        {
            return _menuItems;
        }
        public MenuBuilder AddItem(MenuElement menuItem)
        {
            _menuItems.Add(menuItem);
            return this;
        }
        public MenuBuilder AddItem(int position, string caption, string link, bool isVisible = true, bool isEnabled = true)
        {
            var menuItem = new MenuElement
            {
                Position = position,
                Caption = caption,
                Link = link,                
                IsSubMenu = false,
                IsVisible = isVisible,
                IsEnabled = isEnabled
            };

            _menuItems.Add(menuItem);

            return this;
        }

        public MenuBuilder AddSubMenu(int position, string caption, MenuBuilder menuItems, bool isVisible = true, bool isEnabled = true)
        {
            var menuItem = new MenuElement();
            menuItem.Position = position;
            menuItem.IsSubMenu = true;
            menuItem.Caption = caption;
            menuItem.MenuItems = menuItems;
            menuItem.IsVisible = isVisible;
            menuItem.IsEnabled = isEnabled;

            _menuItems.Add(menuItem);
            return this;
        }

        public List<MenuElement> Build(Func<MenuElement, int> orderBy)
        {
            var menuItems = _menuItems.OrderBy(orderBy);

            return menuItems.ToList();
        }
    }

    public class MenuElement
    {
        public int Id { get; set; }
        public int Position { get; set; }
        public string Caption { get; set; }
        public string Link { get; set; }
        public string Icon { get; set; }        
        public MenuBuilder MenuItems { get; set; } = new MenuBuilder();
        public bool IsSubMenu { get; set; }
        public bool IsVisible { get; set; }
        public bool IsEnabled { get; set; }
    }
}
