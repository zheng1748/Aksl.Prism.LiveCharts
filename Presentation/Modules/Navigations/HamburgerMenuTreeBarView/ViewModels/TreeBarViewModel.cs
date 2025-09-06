using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using Prism.Events;
using Prism.Mvvm;

using Aksl.Infrastructure;

namespace Aksl.Modules.HamburgerMenuTreeBar.ViewModels
{
    public class TreeBarViewModel : BindableBase
    {
        #region Members
        private readonly IEventAggregator _eventAggregator;
        private readonly IMenuService _menuService;
        private MenuItem _rootMenuItem;
        private MenuItem _parentMenuItem;
        #endregion

        #region Constructors
        public TreeBarViewModel(IEventAggregator eventAggregator, IMenuService menuService, MenuItem rootMenuItem)
        {
            _eventAggregator = eventAggregator;
            _menuService = menuService;

            _rootMenuItem = rootMenuItem;

            TreeBarItems = new();
        }
        #endregion

        #region Properties
        public ObservableCollection<TreeBarItemViewModel> TreeBarItems { get; }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty<bool>(ref _isLoading, value);
        }
        #endregion

        #region Create TreeBarItem ViewModel Method
        internal async Task CreateTreeBarItemViewModelsAsync()
        {
            IsLoading = true;

            var parentMenuItem = await _menuService.GetMenuAsync(_rootMenuItem.NavigationName);

            _parentMenuItem = parentMenuItem;

            RecursiveSubMenuItem(_parentMenuItem);

            foreach (var sm in _parentMenuItem.SubMenus)
            {
                TreeBarItemViewModel treeBarItemViewModel = new(_eventAggregator, sm);

                TreeBarItems.Add(treeBarItemViewModel);
            }

            void RecursiveSubMenuItem(MenuItem currentMenuItem)
            {
                currentMenuItem.WorkspaceRegionName = _rootMenuItem.WorkspaceRegionName;
                currentMenuItem.WorkspaceViewEventName = _rootMenuItem.WorkspaceViewEventName;

                if (HasSubMenu(currentMenuItem))
                {
                    foreach (var smi in currentMenuItem.SubMenus)
                    {
                        RecursiveSubMenuItem(smi);
                    }
                }
            }

            bool HasSubMenu(MenuItem mi) => (mi is not null) && mi.SubMenus.Any();

            IsLoading = false;
        }
        #endregion
    }
}
