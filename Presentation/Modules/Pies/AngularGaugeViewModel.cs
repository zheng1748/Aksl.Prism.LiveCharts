using System;
using System.Windows.Input;

using Prism;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Unity;
using Unity;

using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.VisualElements;
using LiveChartsCore.VisualElements;
using System.Collections.Generic;
using LiveChartsCore.SkiaSharpView.Extensions;

using Aksl.Toolkit.Services;

namespace Aksl.Modules.LiveCharts.Pies.ViewModels
{
    public class AngularGaugeViewModel : BindableBase, INavigationAware
    {
        #region Members
        private readonly IDialogViewService _dialogViewService;
        private readonly Random _random = new();
        #endregion

        #region Constructors
        public AngularGaugeViewModel()
        {
            _dialogViewService = (PrismApplication.Current as PrismApplicationBase).Container.Resolve<IDialogViewService>();

            var sectionsOuter = 130;
            var sectionsWidth = 20;

            Needle = new NeedleVisual
            {
                Value = 45
            };

            Series = GaugeGenerator.BuildAngularGaugeSections
                    (
                      new GaugeItem(60, s => SetStyle(sectionsOuter, sectionsWidth, s)),
                      new GaugeItem(30, s => SetStyle(sectionsOuter, sectionsWidth, s)),
                      new GaugeItem(10, s => SetStyle(sectionsOuter, sectionsWidth, s))
                    );

            VisualElements =
            [
                new AngularTicksVisual
                {
                    Labeler = value => value.ToString("N1"),
                    LabelsSize = 16,
                    LabelsOuterOffset = 15,
                    OuterOffset = 65,
                    TicksLength = 20
                },
                Needle
            ];

            CreateDoRandomChangeCommand();
        }
        #endregion

        #region Properties
        public IEnumerable<ISeries> Series { get; set; }
        public IEnumerable<VisualElement> VisualElements { get; set; }
        public NeedleVisual Needle { get; set; }
        #endregion

        #region DoRandomChange Command
        public ICommand DoRandomChangeCommand { get; private set; }

        private void CreateDoRandomChangeCommand()
        {
            DoRandomChangeCommand = new DelegateCommand(() =>
            {
                ExecuteDoRandomChangeCommand();
            },
            () =>
            {
                return true;
            });
        }

        private void ExecuteDoRandomChangeCommand()
        {
            // modifying the Value property updates and animates the chart automatically
            Needle.Value = _random.Next(0, 100);
        }
        #endregion

        #region  Set Style Method
        private static void SetStyle(double sectionsOuter, double sectionsWidth, PieSeries<ObservableValue> series)
        {
            series.OuterRadiusOffset = sectionsOuter;
            series.MaxRadialColumnWidth = sectionsWidth;
            series.CornerRadius = 0;
        }
        #endregion

        #region INavigationAware
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
        #endregion
    }
}
