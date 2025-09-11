using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using Unity;

using Aksl.Modules.HamburgerMenu.Views;
using Aksl.Modules.HamburgerMenu.ViewModels;

namespace Aksl.Modules.HamburgerMenu
{
    public class HamburgerMenuModule : IModule
    {
        #region Members
        private readonly IUnityContainer _container;
        #endregion

        #region Constructors
        public HamburgerMenuModule(IUnityContainer container)
        {
            this._container = container;
        }
        #endregion

        #region IModule 成员
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<HamburgerMenuHubView>();

            containerRegistry.RegisterForNavigation<IndustryHamburgerMenuHubView>();

            containerRegistry.RegisterForNavigation<CustomerHamburgerMenuHubView>();

            //containerRegistry.RegisterForNavigation<AxesHamburgerMenuHubView>();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            ViewModelLocationProvider.Register(typeof(HamburgerMenuHubView).ToString(),
                                               () => this._container.Resolve<HamburgerMenuHubViewModel>());

            ViewModelLocationProvider.Register(typeof(IndustryHamburgerMenuHubView).ToString(),
                                             () => this._container.Resolve<AxesHamburgerMenuHubViewModel>());

            ViewModelLocationProvider.Register(typeof(CustomerHamburgerMenuHubView).ToString(),
                                           () => this._container.Resolve<CustomerHamburgerMenuHubViewModel>());

            //ViewModelLocationProvider.Register(typeof(AxesHamburgerMenuHubView).ToString(),
            //                              () => this._container.Resolve<AxesHamburgerMenuHubViewModel>());
        }
        #endregion
    }
}
