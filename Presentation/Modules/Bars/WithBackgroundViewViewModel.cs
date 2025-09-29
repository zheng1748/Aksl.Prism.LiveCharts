using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

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
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

using Aksl.Toolkit.Services;
using LiveChartsCore.Kernel.Sketches;

namespace Aksl.Modules.LiveCharts.Bars.ViewModels
{
    public class WithBackgroundViewModel : BindableBase, INavigationAware
    {
        #region Members
        private readonly IDialogViewService _dialogViewService;
        #endregion

        #region Constructors
        public WithBackgroundViewModel()
        {
            _dialogViewService = (PrismApplication.Current as PrismApplicationBase).Container.Resolve<IDialogViewService>();
        }
        #endregion

        #region Properties
        public ISeries[] Series { get; set; } =
        [
          new ColumnSeries<double>
          {
              IsHoverable = false, // disables the series from the tooltips // mark
              Values = [10, 10, 10, 10, 10, 10, 10],
              Stroke = null,
              Fill = new SolidColorPaint(new SKColor(30, 30, 30, 30)),
              IgnoresBarPosition = true
          },
            new ColumnSeries<double>
            {
                Values = [3, 10, 5, 3, 7, 3, 8],
                Stroke = null,
                Fill = new SolidColorPaint(SKColors.CornflowerBlue),
                IgnoresBarPosition = true
            }
        ];

        public ICartesianAxis[] YAxes { get; set; } =
        [
            new Axis { MinLimit = 0, MaxLimit = 10 }
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
