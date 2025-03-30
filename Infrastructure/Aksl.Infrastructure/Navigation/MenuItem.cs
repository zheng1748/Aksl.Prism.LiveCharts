﻿using System.Collections.Generic;

namespace Aksl.Infrastructure
{
    public class MenuItem
    {
        #region Constructors
        public MenuItem()
        {
        }
        #endregion

        #region Properties
        public int Id { get; set; }

        public string Name { get; set; }

        public MenuItemType ItemType { get; set; }

        public bool IsSelectedOnInitialize { get; set; } = false;

        public bool IsHome { get; set; } = false;

        public bool IsCacheable { get; set; } = false;

        public string Glyph { get; set; } //<FontIcon Glyph="{Binding FontIcon}"/>

        public int Level { get; set; }

        public string IconPath { get; set; }

        public string IconKind { get; set; }

        public string Title { get; set; }

        public string ModuleName { get; set; }

        public string ViewName { get; set; }

        public MenuItem Parent { get; set; } = null;

        /// <summary>
        /// 子节点
        /// </summary>
        public ICollection<MenuItem> SubMenus { get; } = new List<MenuItem>();

        public string NavigationName { get; set; }

        //权限
        public string RequrePermissons { get; set; }

        public bool CanRun { get; set; }

        public bool IsSeparator => ItemType == MenuItemType.Separator;
        #endregion
    }

    public enum MenuItemType
    {
        Header,
        Item,
        Separator,
        AutoSuggestBox
    }
}
