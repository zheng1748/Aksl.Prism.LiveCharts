using Aksl.Modules.LiveCharts.Axes.ViewModels;
using Aksl.Modules.LiveCharts.Axes.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;

using Unity;

//install-package LiveChartsCore.SkiaSharpView.WPF -Version 2.0.0-beta.90

namespace Aksl.Modules.LiveCharts.Axes
{
    public class AxesModule : IModule
    {
        #region Members
        private readonly IUnityContainer _container;
        #endregion

        #region Constructors
        public AxesModule(IUnityContainer container)
        {
            this._container = container;
        }
        #endregion

        #region IModule 成员
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ColorsAndPositionView>();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            ViewModelLocationProvider.Register(typeof(ColorsAndPositionView).ToString(),
                                         () => this._container.Resolve<ColorsAndPositionViewModel>());
        }
        #endregion
    }
}
