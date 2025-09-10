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

namespace Aksl.Modules.LiveCharts.Events.ViewModels
{
    public class CartesianViewModel : BindableBase, INavigationAware
    {
        #region Members
        private readonly IDialogViewService _dialogViewService;
        private readonly Random _random = new();
        #endregion

        #region Constructors
        public CartesianViewModel()
        {
            _dialogViewService = (PrismApplication.Current as PrismApplicationBase).Container.Resolve<IDialogViewService>();
        }
        #endregion

        #region Properties
        public ISeries[] Series { get; set; } =
        [
            new BoxSeries<BoxValue>
            {
                Name = "Year 2023",
                Values = [
                // max, upper quartile, median, lower quartile, min
                new(100, 80, 60, 20, 70),
                    new(90, 70, 50, 30, 60),
                    new(80, 60, 40, 10, 50)
            ]
            },
            new BoxSeries<BoxValue>
            {
                Name = "Year 2024",
                Values = [
                new(90, 70, 50, 30, 60),
                    new(80, 60, 40, 10, 50),
                    new(70, 50, 30, 20, 40)
            ]
            },
            new BoxSeries<BoxValue>
            {
                Name = "Year 2025",
                Values = [
                new(80, 60, 40, 10, 50),
                    new(70, 50, 30, 20, 40),
                    new(60, 40, 20, 10, 30)
            ]
            }

        ];

        public Axis[] XAxes { get; set; } =
        [
            new Axis
            {
                Labels = ["Apperitizers", "Mains", "Desserts"],
                LabelsRotation = 0,
                SeparatorsPaint = new SolidColorPaint(new SKColor(200, 200, 200)),
                SeparatorsAtCenter = false,
                TicksPaint = new SolidColorPaint(new SKColor(35, 35, 35)),
                TicksAtCenter = true
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
