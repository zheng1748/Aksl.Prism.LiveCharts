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

using Aksl.Toolkit.Services;

namespace Aksl.Modules.LiveCharts.Pies.ViewModels
{
    public class PiesAutoUpdateViewModel: BindableBase, INavigationAware
    {
        #region Members
        private readonly IDialogViewService _dialogViewService;
        private readonly Random _random = new();
        #endregion

        #region Constructors
        public PiesAutoUpdateViewModel()
        {
            _dialogViewService = (PrismApplication.Current as PrismApplicationBase).Container.Resolve<IDialogViewService>();

            // Use ObservableCollections to let the chart listen for changes (or any INotifyCollectionChanged). // mark
            Series =
            [
                // Use the ObservableValue or ObservablePoint types to let the chart listen for property changes // mark
                // or use any INotifyPropertyChanged implementation // mark
                new PieSeries<ObservableValue> { Values = [new ObservableValue(2)] },
                new PieSeries<ObservableValue> { Values = [new ObservableValue(5)] },
                new PieSeries<ObservableValue> { Values = [new ObservableValue(3)] },
                new PieSeries<ObservableValue> { Values = [new ObservableValue(7)] },
                new PieSeries<ObservableValue> { Values = [new ObservableValue(4)] },
                new PieSeries<ObservableValue> { Values = [new ObservableValue(3)] }
            ];
        }
        #endregion

        #region Properties
        public ObservableCollection<ISeries> Series { get; set; }
        #endregion

        #region Methods
       
        public void AddSeries()
        {
            foreach (var series in Series)
            {
                if (series.Values is null)
                {
                    continue;
                }

                foreach (var value in series.Values)
                {
                    var observableValue = (ObservableValue)value;
                    observableValue.Value = _random.Next(1, 10);
                }
            }
        }

        public void RemoveSeries()
        {
            if (Series.Count == 1)
            {
                return;
            }

            Series.RemoveAt(Series.Count - 1);
        }

        public void UpdateAll()
        {
            foreach (var series in Series)
            {
                if (series.Values is null)
                {
                    continue;
                }

                foreach (var value in series.Values)
                {
                    var observableValue = (ObservableValue)value;
                    observableValue.Value = _random.Next(1, 10);
                }
            }
        }

        private bool? _isStreaming = false;
        public async void ConstantChangesClick(object sender, System.Windows.RoutedEventArgs e)
        {
            _isStreaming = _isStreaming is null ? true : !_isStreaming;

            while (_isStreaming.Value)
            {
                RemoveSeries();
                AddSeries();
                await Task.Delay(1000);
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
