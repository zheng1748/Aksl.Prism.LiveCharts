using Prism;
using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Unity;
using Unity;

using Aksl.Infrastructure;
using Aksl.Modules.Account.Views;
using Aksl.Modules.HamburgerMenuSideBar.Views;
using Aksl.Modules.HamburgerMenuNavigationSideBar.Views;
using Aksl.Toolkit.Services;

namespace Aksl.Modules.Shell
{
    public class ShellModule : IModule
    {
        #region Members
        private readonly IUnityContainer _container;
        private readonly IRegionManager _regionManager;
        #endregion

        #region Constructors
        public ShellModule()
        {
            _container = (PrismApplication.Current as PrismApplicationBase).Container.Resolve<IUnityContainer>();
            _regionManager = (PrismApplication.Current as PrismApplicationBase).Container.Resolve<IRegionManager>();
        }
        #endregion

        #region IModule
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RequestNavigate(RegionNames.ShellContentRegion, nameof(Aksl.Modules.HamburgerMenuSideBar.Views.HamburgerMenuSideBarHubView));
            //_regionManager.RequestNavigate(RegionNames.ShellContentRegion, nameof(HamburgerMenuNavigationSideBarHubView));
            // _regionManager.RequestNavigate(RegionNames.ShellContentRegion, nameof(Aksl.Modules.HamburgerMenuTreeSideBar.Views.HamburgerMenuTreeSideBarHubView));

            _regionManager.RequestNavigate(RegionNames.ShellLoginRegion, nameof(LoginStatusView));
        }
        #endregion
    }
}
