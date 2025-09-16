using System;
using System.Collections.Generic;
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
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Extensions;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

using Aksl.Toolkit.Services;

namespace Aksl.Modules.LiveCharts.Pies.ViewModels
{
    public class Gauge5ViewModel : BindableBase, INavigationAware
    {
        #region Members
        private readonly IDialogViewService _dialogViewService;
        private readonly Random _random = new();
        #endregion

        #region Constructors
        public Gauge5ViewModel()
        {
            _dialogViewService = (PrismApplication.Current as PrismApplicationBase).Container.Resolve<IDialogViewService>();

            ObservableValue1 = new ObservableValue { Value = 50 };
            ObservableValue2 = new ObservableValue { Value = 80 };

            Series = GaugeGenerator.BuildSolidGauge
            (
                new GaugeItem(ObservableValue1, series =>
                {
                    series.Name = "North";
                    series.DataLabelsPosition = PolarLabelsPosition.Start;
                }),
                new GaugeItem(ObservableValue2, series =>
                {
                    series.Name = "South";
                    series.DataLabelsPosition = PolarLabelsPosition.Start;
                })
             );

            CreateDoRandomChangeCommand();
        }
        #endregion

        #region Properties
        public ObservableValue ObservableValue1 { get; set; }
        public ObservableValue ObservableValue2 { get; set; }
        public IEnumerable<ISeries> Series { get; set; }
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
            ObservableValue1.Value = _random.Next(0, 100);
            ObservableValue2.Value = _random.Next(0, 100);
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
