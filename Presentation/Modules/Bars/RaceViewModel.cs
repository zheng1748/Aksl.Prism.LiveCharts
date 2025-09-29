using System;
using System.Linq;
using System.Threading.Tasks;

using Prism;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Unity;
using Unity;

using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.Kernel.Events;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.Themes;
using SkiaSharp;

using Aksl.Toolkit.Services;

namespace Aksl.Modules.LiveCharts.Bars.ViewModels
{
    public class RaceViewModel : BindableBase, INavigationAware
    {
        #region Members
        private readonly IDialogViewService _dialogViewService;
        private readonly Random _random = new();
        private readonly PilotInfo[] _data;
        #endregion

        #region Constructors
        public RaceViewModel()
        {
            _dialogViewService = (PrismApplication.Current as PrismApplicationBase).Container.Resolve<IDialogViewService>();

            var paints = Enumerable.Range(0, 7)
            .Select(i => new SolidColorPaint(ColorPalletes.MaterialDesign500[i].AsSKColor()))
            .ToArray();

            // generate some data for each pilot:
            _data =
            [
                new("Tsunoda", 500, paints[0]),
                new("Sainz", 450, paints[1]),
                new("Riccardo", 520, paints[2]),
                new("Bottas", 550, paints[3]),
                new("Perez", 660, paints[4]),
                new("Verstapen", 920, paints[5]),
                new("Hamilton", 1000, paints[6])
            ];

            var rowSeries = new RowSeries<PilotInfo>
            {
                Values = SortData(),
                DataLabelsPaint = new SolidColorPaint(new SKColor(245, 245, 245)),
                DataLabelsPosition = DataLabelsPosition.End,
                DataLabelsTranslate = new(-1, 0),
                DataLabelsFormatter = point => $"{point.Model!.Name} {point.Coordinate.PrimaryValue}",
                MaxBarWidth = 50,
                Padding = 10,
            }
             .OnPointMeasured(point =>
             {
                 // assign a different color to each point
                 if (point.Visual is null) return;
                 point.Visual.Fill = point.Model!.Paint;
             });

            _series = [rowSeries];

            _ = StartRace();
        }
        #endregion

        #region Properties
        private ISeries[] _series;
        public ISeries[] Series 
        {
            get => _series;
            set => SetProperty<ISeries[]>(ref _series, value);
        }

        private Axis[] _xAxes=[new Axis { SeparatorsPaint = new SolidColorPaint(new SKColor(220, 220, 220)) }];
        public Axis[] XAxes
        {
            get => _xAxes;
            set => SetProperty(ref _xAxes, value);
        }

        private Axis[] _yAxes = [new Axis { IsVisible = false }];
        public Axis[] YAxes
        {
            get => _yAxes;
            set => SetProperty(ref _yAxes, value);
        }
        private PilotInfo[] SortData() => [.. _data.OrderBy(x => x.Value)];

        public bool IsReading { get; set; } = true;
        #endregion

        #region Methods
        public async Task StartRace()
        {
            await Task.Delay(1000);

            // to keep this sample simple, we run the next infinite loop
            // in a real application you should stop the loop/task when the view is disposed

            while (IsReading)
            {
                // do a random change to the data
                foreach (var item in _data)
                    item.Value += _random.Next(0, 100);

                Series[0].Values = SortData();

                await Task.Delay(100);
            }
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

    public class PilotInfo : ObservableValue
    {
        public PilotInfo(string name, int value, SolidColorPaint paint)
        {
            Name = name;
            Paint = paint;

            // the ObservableValue.Value property is used by the chart
            Value = value;
        }

        public string Name { get; set; }
        public SolidColorPaint Paint { get; set; }
    }
}
