using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Unity;

using Aksl.Modules.LiveCharts.Design.ViewModels;
using Aksl.Modules.LiveCharts.Design.Views;
using Aksl.Modules.LiveCharts.Box.ViewModels;

//install-package LiveChartsCore.SkiaSharpView.WPF -Version 2.0.0-beta.90

namespace Aksl.Modules.LiveCharts.Design
{
    public class DesignModule : IModule
    {
        #region Members
        private readonly IUnityContainer _container;
        #endregion

        #region Constructors
        public DesignModule(IUnityContainer container)
        {
            this._container = container;
        }
        #endregion

        #region IModule 成员
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<LinearGradientsView>();
            containerRegistry.RegisterForNavigation<RadialGradientsView>();
            containerRegistry.RegisterForNavigation<StrokeDashArrayView>();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            ViewModelLocationProvider.Register(typeof(LinearGradientsView).ToString(),
                                        () => this._container.Resolve<LinearGradientsViewModel>());
            ViewModelLocationProvider.Register(typeof(RadialGradientsView).ToString(),
                                      () => this._container.Resolve<RadialGradientsViewModel>());
            ViewModelLocationProvider.Register(typeof(StrokeDashArrayView).ToString(),
                                      () => this._container.Resolve<StrokeDashArrayViewModel>());
        }
        #endregion
    }
}
