using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Unity;

using Aksl.Modules.LiveCharts.Financial.ViewModels;
using Aksl.Modules.LiveCharts.Financial.Views;

//install-package LiveChartsCore.SkiaSharpView.WPF -Version 2.0.0-beta.90

namespace Aksl.Modules.LiveCharts.Financial
{
    public class FinancialModule : IModule
    {
        #region Members
        private readonly IUnityContainer _container;
        #endregion

        #region Constructors
        public FinancialModule(IUnityContainer container)
        {
            this._container = container;
        }
        #endregion

        #region IModule 成员
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<BasicCandlesticksView>();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            ViewModelLocationProvider.Register(typeof(BasicCandlesticksView).ToString(),
                                        () => this._container.Resolve<BasicCandlesticksViewModel>());
        }
        #endregion
    }
}
