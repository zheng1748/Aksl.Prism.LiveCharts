using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Unity;

using Aksl.Modules.LiveCharts.Pies.ViewModels;
using Aksl.Modules.LiveCharts.Pies.Views;

//install-package LiveChartsCore.SkiaSharpView.WPF -Version 2.0.0-beta.90

namespace Aksl.Modules.LiveCharts.Pies
{
    public class PiesModule : IModule
    {
        #region Members
        private readonly IUnityContainer _container;
        #endregion

        #region Constructors
        public PiesModule(IUnityContainer container)
        {
            this._container = container;
        }
        #endregion

        #region IModule 成员
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<AngularGaugeView>();
            containerRegistry.RegisterForNavigation<PiesAutoUpdateView>();
            containerRegistry.RegisterForNavigation<PiesBasicView>();
            containerRegistry.RegisterForNavigation<CustomView>();
            containerRegistry.RegisterForNavigation<DoughnutView>();
            containerRegistry.RegisterForNavigation<GaugeView>();
            containerRegistry.RegisterForNavigation<Gauge1View>();
            containerRegistry.RegisterForNavigation<Gauge2View>();
            containerRegistry.RegisterForNavigation<Gauge3View>();
            containerRegistry.RegisterForNavigation<Gauge4View>();
            containerRegistry.RegisterForNavigation<Gauge5View>();
            containerRegistry.RegisterForNavigation<GaugesView>();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            ViewModelLocationProvider.Register(typeof(AngularGaugeView).ToString(),
                                               () => this._container.Resolve<AngularGaugeViewModel>());
            ViewModelLocationProvider.Register(typeof(PiesAutoUpdateView).ToString(),
                                               () => this._container.Resolve<PiesAutoUpdateViewModel>());
            ViewModelLocationProvider.Register(typeof(PiesBasicView).ToString(),
                                               () => this._container.Resolve<PiesBasicViewModel>());
            ViewModelLocationProvider.Register(typeof(CustomView).ToString(),
                                               () => this._container.Resolve<CustomViewModel>());
            ViewModelLocationProvider.Register(typeof(DoughnutView).ToString(),
                                               () => this._container.Resolve<DoughnutViewModel>());
            ViewModelLocationProvider.Register(typeof(GaugeView).ToString(),
                                               () => this._container.Resolve<GaugeViewModel>());
            ViewModelLocationProvider.Register(typeof(Gauge1View).ToString(),
                                               () => this._container.Resolve<Gauge1ViewModel>());
            ViewModelLocationProvider.Register(typeof(Gauge2View).ToString(),
                                               () => this._container.Resolve<Gauge2ViewModel>());
            ViewModelLocationProvider.Register(typeof(Gauge3View).ToString(),
                                               () => this._container.Resolve<Gauge3ViewModel>());
            ViewModelLocationProvider.Register(typeof(Gauge4View).ToString(),
                                               () => this._container.Resolve<Gauge4ViewModel>());
            ViewModelLocationProvider.Register(typeof(Gauge5View).ToString(),
                                               () => this._container.Resolve<Gauge5ViewModel>());
            ViewModelLocationProvider.Register(typeof(GaugesView).ToString(),
                                               () => this._container.Resolve<GaugesViewModel>());
        }
    }
    #endregion
}
