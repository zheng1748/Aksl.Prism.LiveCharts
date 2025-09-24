using Aksl.Modules.LiveCharts.Axes.ViewModels;
using Aksl.Modules.LiveCharts.Axes.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;

using Unity;

//install-package LiveChartsCore.SkiaSharpView.WPF -Version 2.0.0-beta.90
//install-package LiveCharts.Core -Version 2.0.0-beta.90

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
            containerRegistry.RegisterForNavigation<CrosshairsView>();
            containerRegistry.RegisterForNavigation<CustomSeparatorsIntervalView>();
            containerRegistry.RegisterForNavigation<DateTimeScaledView>();
            containerRegistry.RegisterForNavigation<LabelsFormatView>();
            containerRegistry.RegisterForNavigation<LabelsFormat2View>();
            containerRegistry.RegisterForNavigation<LabelsRotationView>();
            containerRegistry.RegisterForNavigation<LogicGateAndView>();
            containerRegistry.RegisterForNavigation<MatchScaleView>();
            containerRegistry.RegisterForNavigation<MultipleView>();
            containerRegistry.RegisterForNavigation<NamedLabelsView>();
            containerRegistry.RegisterForNavigation<PagingView>();
            containerRegistry.RegisterForNavigation<SharedView>();
            containerRegistry.RegisterForNavigation<StyleView>();
            containerRegistry.RegisterForNavigation<TimeSpanScaledView>();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            ViewModelLocationProvider.Register(typeof(ColorsAndPositionView).ToString(),
                                         () => this._container.Resolve<ColorsAndPositionViewModel>());
            ViewModelLocationProvider.Register(typeof(CrosshairsView).ToString(),
                                     () => this._container.Resolve<CrosshairsViewModel>());
            ViewModelLocationProvider.Register(typeof(CustomSeparatorsIntervalView).ToString(),
                                 () => this._container.Resolve<CustomSeparatorsIntervalViewModel>());
            ViewModelLocationProvider.Register(typeof(DateTimeScaledView).ToString(),
                                () => this._container.Resolve<DateTimeScaledViewModel>());
            ViewModelLocationProvider.Register(typeof(LabelsFormatView).ToString(),
                              () => this._container.Resolve<LabelsFormatViewModel>());
            ViewModelLocationProvider.Register(typeof(LabelsFormat2View).ToString(),
                              () => this._container.Resolve<LabelsFormat2ViewModel>());
            ViewModelLocationProvider.Register(typeof(LabelsRotationView).ToString(),
                              () => this._container.Resolve<LabelsRotationViewModel>());
            ViewModelLocationProvider.Register(typeof(LogicGateAndView).ToString(),
                              () => this._container.Resolve<LogicGateAndViewModel>());
            ViewModelLocationProvider.Register(typeof(MatchScaleView).ToString(),
                              () => this._container.Resolve<MatchScaleViewModel>());
            ViewModelLocationProvider.Register(typeof(MultipleView).ToString(),
                              () => this._container.Resolve<MultipleViewModel>());
            ViewModelLocationProvider.Register(typeof(NamedLabelsView).ToString(),
                              () => this._container.Resolve<NamedLabelsViewModel>());
            ViewModelLocationProvider.Register(typeof(PagingView).ToString(),
                              () => this._container.Resolve<PagingViewModel>());
            ViewModelLocationProvider.Register(typeof(SharedView).ToString(),
                              () => this._container.Resolve<SharedViewModel>());
            ViewModelLocationProvider.Register(typeof(StyleView).ToString(),
                              () => this._container.Resolve<StyleViewModel>());
            ViewModelLocationProvider.Register(typeof(TimeSpanScaledView).ToString(),
                              () => this._container.Resolve<TimeSpanScaledViewModel>());
        }
        #endregion
    }
}
