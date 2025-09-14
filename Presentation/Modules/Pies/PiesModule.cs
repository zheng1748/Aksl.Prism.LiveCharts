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
        }
        #endregion
    }
}
