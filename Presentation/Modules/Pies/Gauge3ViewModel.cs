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
    public class Gauge3ViewModel : BindableBase, INavigationAware
    {
        #region Members
        private readonly IDialogViewService _dialogViewService;
        #endregion

        #region Constructors
        public Gauge3ViewModel()
        {
            _dialogViewService = (PrismApplication.Current as PrismApplicationBase).Container.Resolve<IDialogViewService>();

            Series = GaugeGenerator.BuildSolidGauge
            (
               new GaugeItem(30, series => SetStyle("Vanessa", series)),
               new GaugeItem(50, series => SetStyle("Charles", series)),
               new GaugeItem(70, series => SetStyle("Ana", series)),
               new GaugeItem(GaugeItem.Background, series =>
               {
                   series.InnerRadius = 20;
               }
             )
          );
        }
        #endregion

        #region Properties
        public IEnumerable<PieSeries<ObservableValue>> Series { get; set; }
        #endregion

        #region Set Style Method
        public static void SetStyle(string name, PieSeries<ObservableValue> series)
        {
            series.Name = name;
            series.DataLabelsPosition = PolarLabelsPosition.Start;
            series.DataLabelsFormatter =
                    point => $"{point.Coordinate.PrimaryValue} {point.Context.Series.Name}";
            series.InnerRadius = 20;
            series.RelativeOuterRadius = 8;
            series.RelativeInnerRadius = 8;
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
