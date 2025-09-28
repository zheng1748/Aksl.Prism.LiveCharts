using System;
using System.Collections.ObjectModel;

using Prism;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Unity;
using Unity;

using LiveChartsCore;
using LiveChartsCore.Drawing;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.Themes;
using LiveChartsCore.Defaults;
using LiveChartsCore.Kernel.Sketches;
using SkiaSharp;

using Aksl.Toolkit.Services;
using System.Collections.Generic;

namespace Aksl.Modules.LiveCharts.Axes.ViewModels
{
    public class SharedViewModel : BindableBase, INavigationAware
    {
        #region Members
        private readonly IDialogViewService _dialogViewService;
        #endregion

        #region Constructors
        public SharedViewModel()
        {
            _dialogViewService = (PrismApplication.Current as PrismApplicationBase).Container.Resolve<IDialogViewService>();

            var values1 = Fetch();
            var values2 = Fetch();

            SeriesCollection1 = [new LineSeries<int> { Values = values1 }];
            SeriesCollection2 = [new ColumnSeries<int> { Values = values2 }];

            // ideally, when sharing an axis, you should set the // mark
            // initial limits for all the axes involved. // mark
            var padding = 3;
            var start = 0 - padding;
            var end = values1.Length - 1 + padding;

            X1 = 
            [
                new Axis
               {
               MinLimit = start,
               MaxLimit = end,
               CrosshairLabelsBackground = SKColors.OrangeRed.AsLvcColor(),
               CrosshairLabelsPaint = new SolidColorPaint(SKColors.White),
               CrosshairPaint = new SolidColorPaint(SKColors.OrangeRed.WithAlpha(50), 4),
               CrosshairPadding = new(8),
               Labeler = value => value.ToString("N2")
           }
       ];
            X2 = [
                new Axis
                {
                    MinLimit = start,
                    MaxLimit = end,
                    CrosshairPaint = new SolidColorPaint(SKColors.OrangeRed.WithAlpha(50), 4)
                }
            ];

            SharedAxes.Set(X1[0], X2[0]);

            // Force the chart to use 70px margin on the left, this way we can align both charts Y axes. // mark
            DrawMargin = new Margin(70, Margin.Auto, Margin.Auto, Margin.Auto);

            // Advanced alternative:
            // you can also ask an axis its posible dimensions to determine the margin you need.

            // First you need to get a chart from the UI
            // in this sample we use the in-memory chart provided by the library.

            // var cartesianChart = new SKCartesianChart();
            // var axis = cartesianChart.YAxes.First() as Axis;
            // var size = axis.GetPossibleSize(cartesianChart.Core);

            // finally instead of using the static 70px, we can use the actual width of the axis.

            // DrawMargin = new Margin(size.Width, Margin.Auto, Margin.Auto, Margin.Auto);

            // normally you would need measure all the axes involved, and use the greater width to
            // calculate the required margin.
        }
        #endregion

        #region Properties
        public ISeries[] SeriesCollection1 { get; set; }
        public ISeries[] SeriesCollection2 { get; set; }
        public Axis[] X1 { get; set; }
        public Axis[] X2 { get; set; }
        public Margin DrawMargin { get; set; }

        #endregion

        #region Fetch Method
        private static int[] Fetch()
        {
            var values = new int[50];
            var r = new Random();
            var t = 0;

            for (var i = 0; i < 50; i++)
            {
                t += r.Next(-90, 100);
                values[i] = t;
            }

            return values;
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
