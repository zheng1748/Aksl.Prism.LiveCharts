using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

using Prism;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Unity;
using Unity;

using Aksl.Toolkit.Services;

using Aksl.Infrastructure;
using Aksl.Infrastructure.Events;

namespace Aksl.Modules.HamburgerMenuNavigationBar.ViewModels
{
    public class HamburgerMenuNavigationBarHubViewModel : BindableBase, INavigationAware
    {
        #region Members 
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;
        private readonly IEventAggregator _eventAggregator;
        private readonly IDialogViewService _dialogViewService;
        private readonly IMenuService _menuService;
        private object _currentView;
        private string _workspaceViewEventName;
        #endregion

        #region Constructors
        public HamburgerMenuNavigationBarHubViewModel()
        {
            _regionManager = (PrismApplication.Current as PrismApplicationBase).Container.Resolve<IRegionManager>();
            _container = (PrismApplication.Current as PrismApplicationBase).Container.Resolve<IUnityContainer>();
            _eventAggregator = (PrismApplication.Current as PrismApplicationBase).Container.Resolve<IEventAggregator>();
            _dialogViewService = (PrismApplication.Current as PrismApplicationBase).Container.Resolve<IDialogViewService>();

            _menuService = _container.Resolve<IMenuService>();

            SelectedDisplayMode = SplitViewDisplayMode.Inline;
            IsPaneOpen = true;
            SelectedPlacement = SplitViewPanePlacement.Left;
        }
        #endregion

        #region Properties
        private string _workspaceRegionName;
        public string WorkspaceRegionName
        {
            get => _workspaceRegionName;
            set => SetProperty<string>(ref _workspaceRegionName, value);
        }

        public GroupedMenusViewModel NavigationBar { get; private set; }

        private Brush _paneBackground = new SolidColorBrush(Colors.White);
        public Brush PaneBackground
        {
            get => _paneBackground;
            set => SetProperty<Brush>(ref _paneBackground, value);
        }

        public GridLength OpenPaneGridLength
        {
            get { return new GridLength(OpenPaneLength); }
        }

        private double _openPaneLength = 320d;
        public double OpenPaneLength
        {
            get => _openPaneLength;
            set => SetProperty<double>(ref _openPaneLength, value);
        }

        public GridLength CompactPaneGridLength
        {
            get { return new GridLength(CompactPaneLength); }
        }

        private double _compactPaneLength = 48d;
        public double CompactPaneLength
        {
            get => _compactPaneLength;
            set => SetProperty<double>(ref _compactPaneLength, value);
        }

        private bool _isPaneOpen = false;
        public bool IsPaneOpen
        {
            get => _isPaneOpen;
            set
            {
                if (SetProperty<bool>(ref _isPaneOpen, value))
                {
                    VisualState = GetVisualState();
                }
            }
        }

        public List<SplitViewDisplayMode> DisplayModeList
        {
            get => Enum.GetValues(typeof(SplitViewDisplayMode)).Cast<SplitViewDisplayMode>().ToList();
        }

        private SplitViewDisplayMode _selectedDisplayMode = SplitViewDisplayMode.Overlay;
        public SplitViewDisplayMode SelectedDisplayMode
        {
            get => _selectedDisplayMode;
            set
            {
                if (SetProperty<SplitViewDisplayMode>(ref _selectedDisplayMode, value))
                {
                    VisualState = GetVisualState();
                }
            }
        }

        public List<SplitViewPanePlacement> PanePlacementList
        {
            get => Enum.GetValues(typeof(SplitViewPanePlacement)).Cast<SplitViewPanePlacement>().ToList();
        }

        private SplitViewPanePlacement _selectedPanePlacement = SplitViewPanePlacement.Left;
        public SplitViewPanePlacement SelectedPlacement
        {
            get => _selectedPanePlacement;
            set
            {
                if (SetProperty<SplitViewPanePlacement>(ref _selectedPanePlacement, value))
                {
                    VisualState = GetVisualState();
                }
            }
        }

        private string _visualState;
        public string VisualState
        {
            get => _visualState;
            set => SetProperty<string>(ref _visualState, value);
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty<bool>(ref _isLoading, value);
        }
        #endregion

        #region Get State Method
        private bool IsCompact
        {
            get
            {
                return SelectedDisplayMode switch
                {
                    SplitViewDisplayMode.CompactInline or SplitViewDisplayMode.CompactOverlay => true,
                    _ => false,
                };
            }
        }

        private bool IsInline
        {
            get
            {
                return SelectedDisplayMode switch
                {
                    SplitViewDisplayMode.CompactInline or SplitViewDisplayMode.Inline => true,
                    _ => false
                };
            }
        }

        protected virtual string GetVisualState()
        {
            string state;

            if (IsPaneOpen)
            {
                state = "Open";
                state += IsInline ? "Inline" : SelectedDisplayMode.ToString();
            }
            else
            {
                state = "Closed";
                if (IsCompact)
                {
                    state += "Compact";
                }
                //else
                //{
                //    return state;
                //}
            }

            state += SelectedPlacement.ToString();

            return state;
        }
        #endregion

        #region RegisterEvent Method
        private void RegisterBuildWorkspaceViewEvents()
        {
            var buildHWorkspaceViewEvent = _eventAggregator.GetEvent(_workspaceViewEventName) as OnBuildWorkspaceViewEventbase;
            Debug.Assert(buildHWorkspaceViewEvent is not null);

            //_eventAggregator.GetEvent<OnBuildHamburgerMenuNavigationBarWorkspaceViewEvent>().Subscribe(async (bhmnbwve) =>
            buildHWorkspaceViewEvent.Subscribe(async (bmve) =>
            {
                var currentMenuItem = bmve.CurrentMenuItem;

                try
                {
                    if (!currentMenuItem.WorkspaceRegionName.Equals(WorkspaceRegionName) && currentMenuItem.WorkspaceViewEventName.Equals(_workspaceViewEventName))
                    {
                        return;
                    };

                    #region Method
                    //string viewTypeAssemblyQualifiedName = currentMenuItem.ViewName;
                    //Type viewType = Type.GetType(viewTypeAssemblyQualifiedName);
                    //if (viewType is not null)
                    //{
                    //    IRegion region = _regionManager.Regions[RegionNames.HamburgerNavigationBarWorkspaceRegion];
                    //    var viewName = viewType.Name;

                    //    //_currentView = region.GetView(viewTypeAssemblyQualifiedName);
                    //    _currentView = region.Views.FirstOrDefault(v => v.GetType() == viewType);
                    //    if (_currentView is null)
                    //    {
                    //        _currentView = region.GetView(viewType.FullName);
                    //    }

                    //    if (_currentView is not null)
                    //    {
                    //        if (bhmnbwve.CurrentMenuItem.IsCacheable)
                    //        {
                    //            region.Activate(_currentView);
                    //        }
                    //        else
                    //        {
                    //            region.Remove(_currentView);

                    //            AddView();
                    //        }
                    //    }
                    //    else
                    //    {
                    //        AddView();
                    //    }

                    //    void AddView()
                    //    {
                    //        if (CanAddView())
                    //        {
                    //            NavigationParameters navigationParameters = new()
                    //            {
                    //                { "CurrentMenuItem", bhmnbwve.CurrentMenuItem }
                    //            };

                    //            _regionManager.RequestNavigate(RegionNames.HamburgerNavigationBarWorkspaceRegion, viewName, navigationParameters);
                    //        }
                    //    }

                    //    bool CanAddView() => !string.IsNullOrEmpty(bhmnbwve.CurrentMenuItem.ModuleName) && bhmnbwve.CurrentMenuItem.SubMenus.Count == 0;
                    //}
                    //else
                    //{
                    //    await _dialogViewService.AlertAsync(message: $"Unable to find \"{viewTypeAssemblyQualifiedName}\".", title: $"Error:Missing Type");
                    //}
                    #endregion

                    await LoadViewAsync();

                    #region LoadView Method
                    async Task LoadViewAsync()
                    {
                        string viewTypeAssemblyQualifiedName = currentMenuItem.ViewName;
                        Type viewType = Type.GetType(viewTypeAssemblyQualifiedName);
                        if (viewType is not null)
                        {
                            // IRegion region = _regionManager.Regions[RegionNames.HamburgerMenuWorkspaceRegion];
                            IRegion region = _regionManager.Regions[WorkspaceRegionName];
                            var viewName = viewType.Name;

                            //_currentView = region.GetView(viewTypeAssemblyQualifiedName);
                            _currentView = region.Views.FirstOrDefault(v => v.GetType() == viewType);
                            if (_currentView is null)
                            {
                                _currentView = region.GetView(viewType.FullName);
                            }

                            if (_currentView is not null)
                            {
                                if (currentMenuItem.IsCacheable)
                                {
                                    region.Activate(_currentView);
                                }
                                else
                                {
                                    region.Remove(_currentView);

                                    AddView();
                                }
                            }
                            else
                            {
                                AddView();
                            }

                            void AddView()
                            {
                                if (CanAddView())
                                {
                                    NavigationParameters navigationParameters = new()
                                {
                                    { "CurrentMenuItem", currentMenuItem }
                                };

                                    // _regionManager.RequestNavigate(RegionNames.HamburgerMenuWorkspaceRegion, viewName, navigationParameters);
                                    _regionManager.RequestNavigate(WorkspaceRegionName, viewName, navigationParameters);
                                }
                            }

                            bool CanAddView() => !string.IsNullOrEmpty(currentMenuItem.ModuleName) && currentMenuItem.SubMenus.Count == 0;
                        }
                        else
                        {
                            await _dialogViewService.AlertAsync(message: $"Unable to find \"{viewTypeAssemblyQualifiedName}\".", title: $"Error:Missing Type");
                        }
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    await _dialogViewService.AlertAsync(message: $"Unable to loading \"{currentMenuItem.ModuleName}\" module.: \"{ex.Message}\"", title: "Error: Load Module");
                }
            }, ThreadOption.UIThread, true);
        }
        #endregion

        #region Create GroupedMenus ViewModel Method
        private async Task CreateGroupedMenusViewModelAsync(MenuItem currentMenuItem)
        {
            IsLoading = true;

            try
            {
              //  var rootMenuItem = await _menuService.GetMenuAsync(currentMenuItem.NavigationName);

                NavigationBar = new(_eventAggregator, _menuService, currentMenuItem);
                AddPropertyChanged();

                void AddPropertyChanged()
                {
                    NavigationBar.PropertyChanged += (sender, e) =>
                    {
                        if (sender is GroupedMenusViewModel gmvm)
                        {
                            if (e.PropertyName == nameof(GroupedMenusViewModel.IsLoading) && !gmvm.IsLoading)
                            {
                                IsLoading = false;
                            }
                        }
                    };
                }

                await NavigationBar.CreateGroupedMenuViewModelsAsync();
                RaisePropertyChanged(nameof(NavigationBar));
            }
            catch (Exception ex)
            {
                await _dialogViewService.AlertAsync(message: $"Unable to create grouped menu : \"{ex.Message}\"", title: "Error: Create GroupedMenu");
            }
            finally
            {
                if (IsLoading)
                {
                    IsLoading = false;
                }
            }
        }
        #endregion

        #region INavigationAware
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var parameters = navigationContext.Parameters;
            if (parameters.TryGetValue("CurrentMenuItem", out MenuItem currentMenuItem))
            {
                WorkspaceRegionName = currentMenuItem.WorkspaceRegionName;
                _workspaceViewEventName = currentMenuItem.WorkspaceViewEventName;

                RegisterBuildWorkspaceViewEvents();

                CreateGroupedMenusViewModelAsync(currentMenuItem).GetAwaiter().GetResult();
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
        #endregion
    }
}
