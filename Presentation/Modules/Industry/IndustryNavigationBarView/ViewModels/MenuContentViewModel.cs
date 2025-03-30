﻿using System.Collections.Generic;
using System.Collections.ObjectModel;

using Prism.Events;
using Prism.Mvvm;

using Aksl.Infrastructure;

namespace Aksl.Modules.IndustryNavigationBar.ViewModels
{
    public class MenuContentViewModel : BindableBase
    {
        #region Members
        protected readonly IEventAggregator _eventAggregator;
        private IEnumerable<MenuItem> _leafMenuItems;
        #endregion

        #region Constructors
        public MenuContentViewModel(IEventAggregator eventAggregator, int groupIndex, IEnumerable<MenuItem> leafMenuItems)
        {
            _eventAggregator = eventAggregator;
            GroupIndex = groupIndex;
            _leafMenuItems = leafMenuItems;

            MenuItems = new();
        }
        #endregion

        #region Properties
        public int GroupIndex { get; }

        public ObservableCollection<MenuItemViewModel> MenuItems { get; private set; }

        private MenuItemViewModel _selectedMenuItem;
        public MenuItemViewModel SelectedMenuItem
        {
            get => _selectedMenuItem;
            set
            {
                var previewSelectedMenuItem = _selectedMenuItem;

                if (SetProperty(ref _selectedMenuItem, value))
                {
                    if (previewSelectedMenuItem is not null)
                    {
                        previewSelectedMenuItem.IsSelected = false;
                    }

                    if (_selectedMenuItem is not null)
                    {
                        _selectedMenuItem.IsSelected = true;
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

        internal void ClearSelectedMenuItem()
        {
            SelectedMenuItem.IsSelected = false;
            SelectedMenuItem = null;
        }

        #region Create MenuItemViewModel Method
        internal void CreateMenuItemViewModels()
        {
            int index = 0;

            IsLoading = true;

            foreach (var menuItem in _leafMenuItems)
            {
                MenuItemViewModel menuItemViewModel = new(_eventAggregator, GroupIndex, index++, menuItem);
                //AddPropertyChanged(menuItemViewModel);

                //void AddPropertyChanged(MenuItemViewModel menuItemvm)
                //{
                //    menuItemvm.PropertyChanged += (sender, e) =>
                //    {
                //        if (sender is MenuItemViewModel mivm)
                //        {
                //            if (e.PropertyName == nameof(MenuItemViewModel.IsSelected))
                //            {
                //                SelectedMenuItem = mivm;
                //            }
                //        }
                //    };
                //}

                MenuItems.Add(menuItemViewModel);
            }

            IsLoading = false;
        }
        #endregion
    }
}
