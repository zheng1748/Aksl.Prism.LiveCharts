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


            containerRegistry.RegisterForNavigation<AxesHamburgerMenuHubView>();

            containerRegistry.RegisterForNavigation<BarsHamburgerMenuHubView>();

            containerRegistry.RegisterForNavigation<BarsHamburgerMenuHubView>();

            containerRegistry.RegisterForNavigation<BoxHamburgerMenuHubView>();

            containerRegistry.RegisterForNavigation<DesignHamburgerMenuHubView>();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            ViewModelLocationProvider.Register(typeof(HamburgerMenuHubView).ToString(),
                                               () => this._container.Resolve<HamburgerMenuHubViewModel>());;

            ViewModelLocationProvider.Register(typeof(AxesHamburgerMenuHubView).ToString(),
                                          () => this._container.Resolve<AxesHamburgerMenuHubViewModel>());

            ViewModelLocationProvider.Register(typeof(BoxHamburgerMenuHubView).ToString(),
                                        () => this._container.Resolve<BoxHamburgerMenuHubViewModel>());

            ViewModelLocationProvider.Register(typeof(BoxHamburgerMenuHubView).ToString(),
                                       () => this._container.Resolve<BoxHamburgerMenuHubViewModel>());

            ViewModelLocationProvider.Register(typeof(DesignHamburgerMenuHubView).ToString(),
                                      () => this._container.Resolve< DesignHamburgerMenuHubViewModel >());
        }
        #endregion
    }
}
