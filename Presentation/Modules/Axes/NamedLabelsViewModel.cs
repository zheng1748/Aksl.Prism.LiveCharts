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
    public class NamedLabelsViewModel : BindableBase, INavigationAware
    {
        #region Members
        private readonly IDialogViewService _dialogViewService;
        #endregion

        #region Constructors
        public NamedLabelsViewModel()
        {
            _dialogViewService = (PrismApplication.Current as PrismApplicationBase).Container.Resolve<IDialogViewService>();
        }
        #endregion

        #region Properties
        public ISeries[] Series { get; set; } =
        [
            new ColumnSeries<int>
            {
                Name = "Sales",
                Values = [200, 558, 458, 249],
            },
            new LineSeries<int>
            {
                Name = "Projected",
                Values = [300, 450, 400, 280],
                Fill = null
            }
        ];

        public ICartesianAxis[] XAxes { get; set; } =
        [
            new Axis
            {
                // Use the labels property to define named labels.
                Labels = ["Anne", "Johnny", "Zac", "Rosa"]
            }
         ];

        public ICartesianAxis[] YAxes { get; set; } = 
        [
            new Axis
            {
                // Now the Y axis we will display labels as currency
                // LiveCharts provides some common formatters
                // in this case we are using the currency formatter.
                Labeler = Labelers.Currency

                // you could also build your own currency formatter
                // for example:
                // Labeler = (value) => value.ToString("C")

                // But the one that LiveCharts provides creates shorter labels when
                // the amount is in millions or trillions
            }
        ];
        public SolidColorPaint TooltipTextPaint { get; set; } =
       new SolidColorPaint
       {
           Color = new SKColor(242, 244, 195),
           SKTypeface = SKTypeface.FromFamilyName("Courier New")
       };

        public SolidColorPaint TooltipBackgroundPaint { get; set; } =
            new SolidColorPaint(new SKColor(72, 0, 50));
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
