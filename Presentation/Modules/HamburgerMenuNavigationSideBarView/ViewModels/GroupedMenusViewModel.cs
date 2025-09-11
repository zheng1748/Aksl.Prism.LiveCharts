﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using Prism.Events;
using Prism.Mvvm;

using Aksl.Infrastructure;

namespace Aksl.Modules.HamburgerMenuNavigationSideBar.ViewModels
{
    public class GroupedMenusViewModel : BindableBase
    {
        #region Members
        private readonly IEventAggregator _eventAggregator;
        private readonly IMenuService _menuService;
        private int _currentGroupeIndex = -1;
        #endregion

        #region Constructors
        public GroupedMenusViewModel(IEventAggregator eventAggregator, IMenuService menuService)
        {
            _eventAggregator = eventAggregator;
            _menuService = menuService;

            GroupedMenus = new();
        }
        #endregion

        #region Properties
        public ObservableCollection<GroupedMenuViewModel> GroupedMenus { get; }

        internal MenuItemViewModel _previewSelectedMenuItem;
        internal MenuItemViewModel PreviewSelectedMenuItem => _previewSelectedMenuItem;

        private MenuItemViewModel _selectedMenuItemItem;
        public MenuItemViewModel SelectedMenuItem
        {
            get => _selectedMenuItemItem;
            set
            {
                if (SetProperty(ref _selectedMenuItemItem, value))
                {
                    foreach (var gm in GroupedMenus)
                    {
                        if (_currentGroupeIndex == gm.GroupIndex)
                        {
                            gm.MenuContent.SelectedMenuItem= _selectedMenuItemItem;
                        }
                    }
                }
            }
        }

        private bool _isPaneOpen = false;
        public bool IsPaneOpen
        {
            get => _isPaneOpen;
            set
            {
                if (SetProperty<bool>(ref _isPaneOpen, value))
                {
                    foreach (var gmvm in GroupedMenus)
                    {
                        gmvm.IsPaneOpen = value;
                    }
                }
            }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty<bool>(ref _isLoading, value);
        }
        #endregion

        #region Reset/Clear Selected MenuItem Method
        internal void ClearSelectedMenuItem()
        {
            if (_selectedMenuItemItem is not null)
            {
                var groupedMenu = GroupedMenus.FirstOrDefault(gm => gm.MenuContent.MenuItems.Any(mi => IsEqualsNameOrTitle(mi.MenuItem.Title, _selectedMenuItemItem.MenuItem.Title) || IsEqualsNameOrTitle(mi.MenuItem.Name, _selectedMenuItemItem.MenuItem.Name)));
               
                if (groupedMenu is not null)
                {
                    _selectedMenuItemItem = null;
                    _previewSelectedMenuItem = null;

                    groupedMenu.MenuContent.ClearSelectedMenuItem();
                    _currentGroupeIndex = -1;
                }
            }

            //foreach (var gm in GroupedMenus)
            //{
            //    if (_currentGroupeIndex == gm.GroupIndex)
            //    {
            //        if (_selectedMenuItemItem is not null)
            //        {
            //            _selectedMenuItemItem = null;
            //            _previewSelectedMenuItem = null;
            //        }

            //        gm.MenuContent.ClearSelectedMenuItem();
            //        _currentGroupeIndex = -1;

            //        break;
            //    }
            //}
        }

        internal void ResetSelectedMenuItem(MenuItemViewModel selectedMenuItemItem)
        {
            if (selectedMenuItemItem is not null)
            {
                //var previewgGoupedMenu = GroupedMenus.FirstOrDefault(gm => gm.MenuContent.MenuItems.Any(mi => IsEqualsNameOrTitle(mi.MenuItem.Title, _selectedMenuItemItem.MenuItem.Title) || IsEqualsNameOrTitle(mi.MenuItem.Name, _selectedMenuItemItem.MenuItem.Name)));

                //if (previewgGoupedMenu is not null)
                //{
                //    previewgGoupedMenu.MenuContent.ClearSelectedMenuItem();
                //}

                var groupedMenu = GroupedMenus.FirstOrDefault(gm => gm.MenuContent.MenuItems.Any(mi => IsEqualsNameOrTitle(mi.MenuItem.Title, selectedMenuItemItem.MenuItem.Title) || IsEqualsNameOrTitle(mi.MenuItem.Name, selectedMenuItemItem.MenuItem.Name)));
               
                if (groupedMenu is not null)
                {
                    groupedMenu.MenuContent.SelectedMenuItem=selectedMenuItemItem;
                    //groupedMenu.MenuContent.ResetSelectedMenuItem(selectedMenuItemItem);
                    //_currentGroupeIndex = groupedMenu.MenuContent.GroupIndex;
                    //_selectedMenuItemItem = selectedMenuItemItem;
                }
                //foreach (var gm in GroupedMenus)
                //{
                //    if (_currentGroupeIndex == gm.GroupIndex)
                //    {
                //        gm.MenuContent.ResetSelectedMenuItem(selectedMenuItemItem);

                //        break;
                //    }
                //}
            }
        }
        #endregion

        #region Create GroupedMenu ViewModels Method
        internal async Task CreateGroupedMenuViewModelsAsync()
        {
            IsLoading = true;

            var rootMenuItem = await _menuService.GetMenuAsync("All");

            var subMenuItems = rootMenuItem.SubMenus;
            int index = 0;
            foreach (var smi in subMenuItems)
            {
                IEnumerable<MenuItem> leafMenuItems = GetAllLeafMenuItems(smi);

                GroupedMenuViewModel groupedMenuViewModel = new(_eventAggregator, index++, smi, leafMenuItems);
                AddPropertyChanged();

                GroupedMenus.Add(groupedMenuViewModel);

                void AddPropertyChanged()
                {
                    groupedMenuViewModel.PropertyChanged += (sender, e) =>
                    {
                        if (sender is GroupedMenuViewModel gmvm)
                        {
                            if (e.PropertyName == nameof(GroupedMenuViewModel.IsLoading))
                            {
                                //最后一个
                                if (gmvm.GroupIndex == GroupedMenus.Count() && !gmvm.IsLoading)
                                {
                                    IsLoading = false;
                                }
                            }

                            if (e.PropertyName == nameof(GroupedMenuViewModel.MenuContent))
                            {
                                if (_currentGroupeIndex == gmvm.GroupIndex)
                                {
                                    //SelectedMenuItem = gmvm.MenuContent.SelectedMenuItem;
                                }
                                else
                                {
                                    foreach (var gm in GroupedMenus)
                                    {
                                        if (_currentGroupeIndex == gm.GroupIndex)
                                        {
                                            _previewSelectedMenuItem = gm.MenuContent.SelectedMenuItem;
                                            gm.MenuContent.ClearSelectedMenuItem();

                                            break;
                                        }
                                    }

                                    _currentGroupeIndex = gmvm.GroupIndex;
                                    _selectedMenuItemItem = gmvm.MenuContent.SelectedMenuItem;
                                }
                            }
                        }
                    };
                }
            }

            IsLoading = false;
        }

        private IEnumerable<MenuItem> GetAllLeafMenuItems(MenuItem menuItem)
        {
            List<MenuItem> leafMenuItems = new();

            RecursiveSubMenuItem(menuItem);

            void RecursiveSubMenuItem(MenuItem currentMenuItem)
            {
                if (!AnyEqualsMenuItem(leafMenuItems, currentMenuItem) && Isleaf(currentMenuItem) && HasTitle(currentMenuItem))
                {
                    leafMenuItems.Add(currentMenuItem);
                }

                if (HasSubMenu(currentMenuItem))
                {
                    foreach (var smi in currentMenuItem.SubMenus)
                    {
                        RecursiveSubMenuItem(smi);
                    }
                }
            }

            bool HasSubMenu(MenuItem mi) => (mi is not null) && mi.SubMenus.Any();

            bool Isleaf(MenuItem mi) => (mi is not null) && mi.SubMenus.Count <= 0;

            bool HasTitle(MenuItem mi) => (mi is not null) && !string.IsNullOrEmpty(mi.Title);

            return leafMenuItems;
        }

        internal void GetLeafMenuItems(MenuItem currentMenuItem, IList<MenuItem> leafMenuItems)
        {
            if (Isleaf(currentMenuItem) && HasTitle(currentMenuItem))
            {
                leafMenuItems.Add(currentMenuItem);
            }

            if (currentMenuItem.SubMenus.Any())
            {
                RecursiveSubMenuItem(currentMenuItem);
            }

            void RecursiveSubMenuItem(MenuItem parentMenuItem)
            {
                foreach (var smi in parentMenuItem.SubMenus)
                {
                    if (!leafMenuItems.Contains(smi) && Isleaf(smi) && HasTitle(smi))
                    {
                        leafMenuItems.Add(smi);
                    }
                    RecursiveSubMenuItem(smi);
                }
            }

            bool Isleaf(MenuItem mi) => mi.SubMenus.Count <= 0;

            bool HasTitle(MenuItem mi) => !string.IsNullOrEmpty(mi.Title);
        }
        #endregion

        #region Contain Methods
        private bool AnyEqualsMenuItem(IEnumerable<MenuItem> menuItems, MenuItem menuItem)
        {
            var isEquals = menuItems.Any(mi => IsEqualsNameOrTitle(mi.Title, menuItem.Title) || IsEqualsNameOrTitle(mi.Name, menuItem.Name));

            return isEquals;
        }

        private bool IsEqualsNameOrTitle(string nameOrTitle, string otherNameOrTitle)
        {
            if (string.IsNullOrEmpty(nameOrTitle) || string.IsNullOrEmpty(otherNameOrTitle))
            {
                return false;
            }

            var isEquals = (!string.IsNullOrEmpty(nameOrTitle) && otherNameOrTitle.Equals(nameOrTitle, StringComparison.InvariantCultureIgnoreCase)) ||
                           (!string.IsNullOrEmpty(nameOrTitle) && otherNameOrTitle.Equals(nameOrTitle, StringComparison.InvariantCultureIgnoreCase));

            return isEquals;
        }
        #endregion
    }
}
