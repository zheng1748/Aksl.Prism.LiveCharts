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
            containerRegistry.RegisterForNavigation<ErrorHamburgerMenuHubView>();
            containerRegistry.RegisterForNavigation<EventsHamburgerMenuHubView>();
            containerRegistry.RegisterForNavigation<FinancialHamburgerMenuHubView>();
            containerRegistry.RegisterForNavigation<GeneralHamburgerMenuHubView>();
            containerRegistry.RegisterForNavigation<HeatHamburgerMenuHubView>();
            containerRegistry.RegisterForNavigation<LinesHamburgerMenuHubView>();
            containerRegistry.RegisterForNavigation<MapsHamburgerMenuHubView>();
            containerRegistry.RegisterForNavigation<PiesHamburgerMenuHubView>();
            containerRegistry.RegisterForNavigation<PolarHamburgerMenuHubView>();
            containerRegistry.RegisterForNavigation<ScatterHamburgerMenuHubView>();
            containerRegistry.RegisterForNavigation<StackedAreaHamburgerMenuHubView>();
            containerRegistry.RegisterForNavigation<StackedBarsHamburgerMenuHubView>();
            containerRegistry.RegisterForNavigation<StepLinesHamburgerMenuHubView>();
            containerRegistry.RegisterForNavigation<FinancialHamburgerMenuHubView>();
            containerRegistry.RegisterForNavigation<FinancialHamburgerMenuHubView>();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            ViewModelLocationProvider.Register(typeof(HamburgerMenuHubView).ToString(),
                                               () => this._container.Resolve<HamburgerMenuHubViewModel>()); ;

            ViewModelLocationProvider.Register(typeof(AxesHamburgerMenuHubView).ToString(),
                                               () => this._container.Resolve<AxesHamburgerMenuHubViewModel>());
            ViewModelLocationProvider.Register(typeof(BoxHamburgerMenuHubView).ToString(),
                                               () => this._container.Resolve<BoxHamburgerMenuHubViewModel>());
            ViewModelLocationProvider.Register(typeof(BoxHamburgerMenuHubView).ToString(),
                                               () => this._container.Resolve<BoxHamburgerMenuHubViewModel>());
            ViewModelLocationProvider.Register(typeof(DesignHamburgerMenuHubView).ToString(),
                                               () => this._container.Resolve<DesignHamburgerMenuHubViewModel>());
            ViewModelLocationProvider.Register(typeof(ErrorHamburgerMenuHubView).ToString(),
                                               () => this._container.Resolve<ErrorHamburgerMenuHubViewModel>());
            ViewModelLocationProvider.Register(typeof(EventsHamburgerMenuHubView).ToString(),
                                               () => this._container.Resolve<EventsHamburgerMenuHubViewModel>());
            ViewModelLocationProvider.Register(typeof(FinancialHamburgerMenuHubView).ToString(),
                                               () => this._container.Resolve<FinancialHamburgerMenuHubViewModel>());
            ViewModelLocationProvider.Register(typeof(GeneralHamburgerMenuHubView).ToString(),
                                               () => this._container.Resolve<GeneralHamburgerMenuHubViewModel>());
            ViewModelLocationProvider.Register(typeof(HeatHamburgerMenuHubView).ToString(),
                                               () => this._container.Resolve<HeatHamburgerMenuHubViewModel>());
            ViewModelLocationProvider.Register(typeof(LinesHamburgerMenuHubView).ToString(),
                                               () => this._container.Resolve<LinesHamburgerMenuHubViewModel>());
            ViewModelLocationProvider.Register(typeof(MapsHamburgerMenuHubView).ToString(),
                                               () => this._container.Resolve<MapsHamburgerMenuHubViewModel>());
            ViewModelLocationProvider.Register(typeof(PiesHamburgerMenuHubView).ToString(),
                                               () => this._container.Resolve<PolarHamburgerMenuHubViewModel>());
            ViewModelLocationProvider.Register(typeof(ScatterHamburgerMenuHubView).ToString(),
                                               () => this._container.Resolve<ScatterHamburgerMenuHubViewModel>());
            ViewModelLocationProvider.Register(typeof(StackedAreaHamburgerMenuHubView).ToString(),
                                               () => this._container.Resolve<StackedAreaHamburgerMenuHubViewModel>());
            ViewModelLocationProvider.Register(typeof(StackedBarsHamburgerMenuHubView).ToString(),
                                               () => this._container.Resolve<StackedBarsHamburgerMenuHubViewModel>());
            ViewModelLocationProvider.Register(typeof(StepLinesHamburgerMenuHubView).ToString(),
                                               () => this._container.Resolve<StepLinesHamburgerMenuHubViewModel>());
            ViewModelLocationProvider.Register(typeof(FinancialHamburgerMenuHubView).ToString(),
                                               () => this._container.Resolve<FinancialHamburgerMenuHubViewModel>());
            ViewModelLocationProvider.Register(typeof(FinancialHamburgerMenuHubView).ToString(),
                                               () => this._container.Resolve<FinancialHamburgerMenuHubViewModel>());
            ViewModelLocationProvider.Register(typeof(FinancialHamburgerMenuHubView).ToString(),
                                               () => this._container.Resolve<FinancialHamburgerMenuHubViewModel>());
        }
        #endregion
    }
}
