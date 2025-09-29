using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Unity;

using Aksl.Modules.LiveCharts.Bars.ViewModels;
using Aksl.Modules.LiveCharts.Bars.Views;
using System;

//install-package LiveChartsCore.SkiaSharpView.WPF -Version 2.0.0-beta.90

namespace Aksl.Modules.LiveCharts.Bars
{
    public class BarsModule : IModule
    {
        #region Members
        private readonly IUnityContainer _container;
        #endregion

        #region Constructors
        public BarsModule(IUnityContainer container)
        {
            this._container = container;
        }
        #endregion

        #region IModule 成员
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<AutoUpdateView>();
            containerRegistry.RegisterForNavigation<BasicView>();
            containerRegistry.RegisterForNavigation<CustomView>();
            containerRegistry.RegisterForNavigation<DelayedAnimationView>();
            containerRegistry.RegisterForNavigation<LayeredView>();
            containerRegistry.RegisterForNavigation<RaceView>();
            containerRegistry.RegisterForNavigation<RowsWithLabelsView>();
            containerRegistry.RegisterForNavigation<SpacingView>();
            containerRegistry.RegisterForNavigation<WithBackgroundView>();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            ViewModelLocationProvider.Register(typeof(AutoUpdateView).ToString(),
                                         () => this._container.Resolve<AutoUpdateViewModel>());
            ViewModelLocationProvider.Register(typeof(BasicView).ToString(),
                                        () => this._container.Resolve<BasicViewModel>());
            ViewModelLocationProvider.Register(typeof(CustomView).ToString(),
                                       () => this._container.Resolve<CustomViewModel>());
            ViewModelLocationProvider.Register(typeof(DelayedAnimationView).ToString(),
                                       () => this._container.Resolve<DelayedAnimationViewModel>());
            ViewModelLocationProvider.Register(typeof(LayeredView).ToString(),
                                       () => this._container.Resolve<LayeredViewModel>());
            ViewModelLocationProvider.Register(typeof(RaceView).ToString(),
                                       () => this._container.Resolve<RaceViewModel>());
            ViewModelLocationProvider.Register(typeof(RowsWithLabelsView).ToString(),
                                       () => this._container.Resolve<RowsWithLabelsViewModel>());
            ViewModelLocationProvider.Register(typeof(SpacingView).ToString(),
                                     () => this._container.Resolve<SpacingViewModel>());
            ViewModelLocationProvider.Register(typeof(WithBackgroundView).ToString(),
                                     () => this._container.Resolve<WithBackgroundViewModel>());
        }
        #endregion
    }
}
