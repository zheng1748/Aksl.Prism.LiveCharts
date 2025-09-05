using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using Unity;

using Aksl.Modules.HamburgerMenuNavigationBar.Views;
using Aksl.Modules.HamburgerMenuNavigationBar.ViewModels;

namespace Aksl.Modules.HamburgerMenuNavigationBar           
{
    public class HamburgerMenuNavigationBarModule : IModule
    {
        #region Members
        private readonly IUnityContainer _container;
        #endregion

        #region Constructors
        public HamburgerMenuNavigationBarModule(IUnityContainer container)
        {
            this._container = container;
        }
        #endregion

        #region IModule 成员
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<HamburgerMenuNavigationBarHubView>();

            containerRegistry.RegisterForNavigation<IndustryHamburgerMenuNavigationBarHubView>();

            containerRegistry.RegisterForNavigation<CustomerHamburgerMenuNavigationBarHubView>();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            ViewModelLocationProvider.Register(typeof(HamburgerMenuNavigationBarHubView).ToString(),
                                               () => this._container.Resolve<HamburgerMenuNavigationBarHubViewModel>());

            ViewModelLocationProvider.Register(typeof(IndustryHamburgerMenuNavigationBarHubView).ToString(),
                                             () => this._container.Resolve<IndustryHamburgerMenuNavigationBarHubViewModel>());

            ViewModelLocationProvider.Register(typeof(CustomerHamburgerMenuNavigationBarHubView).ToString(),
                                             () => this._container.Resolve<CustomerHamburgerMenuNavigationBarHubViewModel>());
        }
        #endregion
    }
}
