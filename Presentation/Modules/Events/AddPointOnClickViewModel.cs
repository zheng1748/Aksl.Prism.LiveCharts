using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

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
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.Kernel.Events;

using Aksl.Toolkit.Services;

namespace Aksl.Modules.LiveCharts.Events.ViewModels
{
    public class AddPointOnClickViewModel : BindableBase, INavigationAware
    {
        #region Members
        private readonly IDialogViewService _dialogViewService;
        private readonly Random _random = new();
        #endregion

        #region Constructors
        public AddPointOnClickViewModel()
        {
            _dialogViewService = (PrismApplication.Current as PrismApplicationBase).Container.Resolve<IDialogViewService>();

            Points =
            [
              new(0, 5),
              new(3, 8),
              new(7, 9)
            ];

            SeriesCollection = [
                new LineSeries<ObservablePoint>
                {
                    Values = Points,
                    Fill = null,
                    DataPadding = new LiveChartsCore.Drawing.LvcPoint(5, 5)
                }
            ];

            CreatePointerDownCommand();
        }
   
        #endregion

        #region Properties
        public ObservableCollection<ObservablePoint> Points { get; set; }

        public ISeries[] SeriesCollection { get; set; }

        #endregion

        #region Pointer Down Command
        public ICommand PointerDownCommand { get; private set; }
     
        private void CreatePointerDownCommand()
        {
            PointerDownCommand = new DelegateCommand<PointerCommandArgs>((args) =>
            {
                 ExecutePointerDownCommand(args);
            },
            (args) =>
            {
                var canExecute = args is not null;
                return canExecute;
            });
        }

        private void ExecutePointerDownCommand(PointerCommandArgs args)
        {
            if (args != null)
            {
                var chart = (ICartesianChartView)args.Chart;

                // scales the UI coordinates to the corresponding data in the chart.
                var scaledPoint = chart.ScalePixelsToData(args.PointerPosition);

                // finally add the new point to the data in our chart.
                Points.Add(new ObservablePoint(scaledPoint.X, scaledPoint.Y));
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
}
