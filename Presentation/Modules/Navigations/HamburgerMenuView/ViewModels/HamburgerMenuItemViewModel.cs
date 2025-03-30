﻿using System;
using System.Windows.Input;

using Prism.Commands;
using Prism.Events;
using Prism.Modularity;
using Prism.Mvvm;

using Aksl.Toolkit.Controls;
using Aksl.Infrastructure;
using Aksl.Infrastructure.Events;

namespace Aksl.Modules.HamburgerMenu.ViewModels
{
    public class HamburgerMenuItemViewModel : BindableBase
    {
        #region Members
        protected readonly IEventAggregator _eventAggregator;
        private readonly Infrastructure.MenuItem _menuItem;
        #endregion

        #region Constructors
        public HamburgerMenuItemViewModel(IEventAggregator eventAggregator, int index, MenuItem menuItem)
        {
            _eventAggregator = eventAggregator;
            Index = index;
            _menuItem = menuItem;
        }
        #endregion

        #region Properties
        public int Index { get; }

        public Infrastructure.MenuItem MenuItem => _menuItem;

        public string Title => _menuItem.Title;

        public bool Isleaf => _menuItem.SubMenus.Count <= 0;

        private bool _isSelected = false;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (SetProperty<bool>(ref _isSelected, value))
                {
                    if (Isleaf && _isSelected)
                    {
                        _eventAggregator.GetEvent<OnBuildHamburgerMenuWorkspaceViewEvent>().Publish(new() { CurrentMenuItem = _menuItem });
                    }
                }
            }
        }

        public PackIconKind IconKind 
        {
            get
            {
               PackIconKind kind = PackIconKind.None;

                _ = Enum.TryParse(_menuItem.IconKind, out kind);

                return kind;
            }
        }

        private bool _isPaneOpen = false;
        public bool IsPaneOpen
        {
            get => _isPaneOpen;
            set => SetProperty<bool>(ref _isPaneOpen, value);
        }

        //private void DoCallBack(bool isLoadModule)
        //{
        //    if (!isLoadModule)
        //    {
        //        IsSelected = false;
        //    }
        //}

        protected bool _isEnabled = true;
        public bool IsEnabled
        {
            get => _isEnabled;

            set => SetProperty<bool>(ref _isEnabled, value);
        }
        #endregion

        #region Mouse Left Button Down Event
        public void ExecuteNavigationItemClick(object sender, MouseButtonEventArgs e)
        {
            if (Isleaf)
            {
                IsSelected = true;
            }
        }
        #endregion
    }
}
