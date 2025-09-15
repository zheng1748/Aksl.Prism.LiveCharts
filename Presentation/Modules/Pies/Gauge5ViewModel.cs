using System;
using System.Collections.Generic;
using System.Linq;

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
        #endregion

        #region Constructors
        public Gauge5ViewModel()
        {
            _dialogViewService = (PrismApplication.Current as PrismApplicationBase).Container.Resolve<IDialogViewService>();

            Series = GaugeGenerator.BuildSolidGauge
            (
                new GaugeItem(30, series =>
                {
                    series.Fill = new SolidColorPaint(SKColors.YellowGreen);
                    series.DataLabelsSize = 50;
                    series.DataLabelsPaint = new SolidColorPaint(SKColors.Red);
                    series.DataLabelsPosition = PolarLabelsPosition.ChartCenter;
                    series.InnerRadius = 75;
                }),
                new GaugeItem(GaugeItem.Background, series =>
                {
                    series.Fill = new SolidColorPaint(new SKColor(100, 181, 246, 90));
                    series.InnerRadius = 75;
                 })
             );
        }
        #endregion

        #region Properties
        public IEnumerable<PieSeries<ObservableValue>> Series { get; set; }
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
