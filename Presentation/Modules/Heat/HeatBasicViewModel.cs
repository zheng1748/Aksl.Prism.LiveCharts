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
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.SkiaSharpView;
using SkiaSharp;

using Aksl.Toolkit.Services;

namespace Aksl.Modules.LiveCharts.Heat.ViewModels
{
    public class HeatBasicViewModel : BindableBase, INavigationAware
    {
        #region Members
        private readonly IDialogViewService _dialogViewService;
        private readonly Random _random = new();
        #endregion

        #region Constructors
        public HeatBasicViewModel()
        {
            _dialogViewService = (PrismApplication.Current as PrismApplicationBase).Container.Resolve<IDialogViewService>();
        }
        #endregion

        #region Properties
        public ISeries[] Series { get; set; } =
        [
            new HeatSeries<WeightedPoint>
            {
                HeatMap =
                [
                    new SKColor(255, 241, 118).AsLvcColor(), // the first element is the "coldest"
                    SKColors.DarkSlateGray.AsLvcColor(),
                    SKColors.Blue.AsLvcColor() // the last element is the "hottest"
                ],
                Values =
               [
                       // Charles
                   new(0, 0, 150), // Jan
                   new(0, 1, 123), // Feb
                   new(0, 2, 310), // Mar
                   new(0, 3, 225), // Apr
                   new(0, 4, 473), // May
                   new(0, 5, 373), // Jun

                   // Richard
                   new(1, 0, 432), // Jan
                   new(1, 1, 312), // Feb
                   new(1, 2, 135), // Mar
                   new(1, 3, 78), // Apr
                   new(1, 4, 124), // May
                   new(1, 5, 423), // Jun

                   // Ana
                   new(2, 0, 543), // Jan
                   new(2, 1, 134), // Feb
                   new(2, 2, 524), // Mar
                   new(2, 3, 315), // Apr
                   new(2, 4, 145), // May
                   new(2, 5, 80), // Jun

                   // Mari
                   new(3, 0, 90), // Jan
                   new(3, 1, 123), // Feb
                   new(3, 2, 70), // Mar
                   new(3, 3, 123), // Apr
                   new(3, 4, 432), // May
                   new(3, 5, 142), // Jun
               ]
            }
           ];

        public ICartesianAxis[] XAxes { get; set; } =
        [
            new Axis
            {
                Labels = ["Charles", "Richard", "Ana", "Mari"]
            }
         ];

        public ICartesianAxis[] YAxes { get; set; } =
        [
            new Axis
            {
                Labels = ["Jan", "Feb", "Mar", "Apr", "May", "Jun"]
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
