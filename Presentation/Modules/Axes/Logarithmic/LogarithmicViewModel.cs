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

namespace Aksl.Modules.LiveCharts.Axes.ViewModels
{
    public class LogarithmicViewModel : BindableBase, INavigationAware
    {
        #region Members
        private readonly IDialogViewService _dialogViewService;
        // base 10 log, change the base if you require it.
        // or use any custom scale the logic is the same.
        private static readonly int s_logBase = 10;
        #endregion

        #region Constructors
        public LogarithmicViewModel()
        {
            _dialogViewService = (PrismApplication.Current as PrismApplicationBase).Container.Resolve<IDialogViewService>();
        }
        #endregion

        #region Properties
        public ISeries[] Series { get; set; } =
        [
              new LineSeries<LogarithmicPoint>
              {
                  Values =
                  [
                      new() { X = 1, Y = 1 },
                      new() { X = 2, Y = 10 },
                      new() { X = 3, Y = 100 },
                      new() { X = 4, Y = 1_000 },
                      new() { X = 5, Y = 10_000 },
                      new() { X = 6, Y = 100_000 },
                      new() { X = 7, Y = 1_000_000 },
                      new() { X = 8, Y = 10_000_000 }
                  ],

                  // lets map the values to the logarithmic scale // mark
                  // for the x coordinate, we use the X property // mark
                  // and for the Y coordinate, we will map it to the logarithm of the Y value // mark
                  Mapping = (logPoint, index) => // mark
                                    new(logPoint.X, Math.Log(logPoint.Y, s_logBase)), // mark

                  // for more info about mappers see:
                  // https://livecharts.dev/docs/{{ platform }}/{{ version }}/Overview.Mappers
              }
        ];

        public ICartesianAxis[] YAxes { get; set; } =
        [
            new LogarithmicAxis(s_logBase)
            {
                SeparatorsPaint = new SolidColorPaint
                {
                    Color = SKColors.Black.WithAlpha(100),
                    StrokeThickness = 1,
                },
                SubseparatorsPaint = new SolidColorPaint
                {
                    Color = SKColors.Black.WithAlpha(50),
                    StrokeThickness = 0.5f
                },
                SubseparatorsCount = 9,
            }
        ];
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
