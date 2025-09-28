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
    public class TimeSpanScaledViewModel : BindableBase, INavigationAware
    {
        #region Members
        private readonly IDialogViewService _dialogViewService;
        #endregion

        #region Constructors
        public TimeSpanScaledViewModel()
        {
            _dialogViewService = (PrismApplication.Current as PrismApplicationBase).Container.Resolve<IDialogViewService>();
        }
        #endregion

        #region Properties
        public ISeries[] Series { get; set; } =
        [
             new ColumnSeries<TimeSpanPoint>
             {
                 Values = [
                new() { TimeSpan = TimeSpan.FromMilliseconds(1), Value = 10 },
                     new() { TimeSpan = TimeSpan.FromMilliseconds(2), Value = 6 },
                     new() { TimeSpan = TimeSpan.FromMilliseconds(3), Value = 3 },
                     new() { TimeSpan = TimeSpan.FromMilliseconds(4), Value = 12 },
                     new() { TimeSpan = TimeSpan.FromMilliseconds(5), Value = 8 }
            ],
             }
        ];

        // You can use the TimeSpanAxis class to define a time span based axis // mark

        // The first parameter is the time between each point, in this case 1 day // mark
        // you can also use 1 year, 1 month, 1 hour, 1 minute, 1 second, 1 millisecond, etc // mark

        // The second parameter is a function that receives the value and returns the label // mark
        public ICartesianAxis[] XAxes { get; set; } = [
            new TimeSpanAxis(TimeSpan.FromMilliseconds(1), date => date.ToString("fff") + "ms")
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
