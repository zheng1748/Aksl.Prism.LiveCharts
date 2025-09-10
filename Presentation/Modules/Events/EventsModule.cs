using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Unity;

using Aksl.Modules.LiveCharts.Events.ViewModels;
using Aksl.Modules.LiveCharts.Events.Views;

//install-package LiveChartsCore.SkiaSharpView.WPF -Version 2.0.0-beta.90

namespace Aksl.Modules.LiveCharts.Events
{
    public class EventsModule : IModule
    {
        #region Members
        private readonly IUnityContainer _container;
        #endregion

        #region Constructors
        public EventsModule(IUnityContainer container)
        {
            this._container = container;
        }
        #endregion

        #region IModule 成员
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<AddPointOnClickView>();
            containerRegistry.RegisterForNavigation<CartesianView>();
            containerRegistry.RegisterForNavigation<CartesianView>();
            containerRegistry.RegisterForNavigation<PieView>();
            containerRegistry.RegisterForNavigation<PolarView>();
            containerRegistry.RegisterForNavigation<TutorialView>();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            ViewModelLocationProvider.Register(typeof(AddPointOnClickView).ToString(),
                                        () => this._container.Resolve<AddPointOnClickViewModel>());
            ViewModelLocationProvider.Register(typeof(CartesianView).ToString(),
                                       () => this._container.Resolve<CartesianViewModel>());
            ViewModelLocationProvider.Register(typeof(CartesianView).ToString(),
                                       () => this._container.Resolve<OverrideFindViewModel>());
            ViewModelLocationProvider.Register(typeof(PieView).ToString(),
                                       () => this._container.Resolve<PieViewModel>());
            ViewModelLocationProvider.Register(typeof(PolarView).ToString(),
                                       () => this._container.Resolve<PolarViewModel>());
            ViewModelLocationProvider.Register(typeof(TutorialView).ToString(),
                                       () => this._container.Resolve<TutorialViewModel>());
        }
        #endregion
    }
}
