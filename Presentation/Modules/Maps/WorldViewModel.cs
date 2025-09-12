using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using Prism;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Unity;
using Unity;

using LiveChartsCore.SkiaSharpView.Drawing.Geometries;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.Geo;

using Aksl.Toolkit.Services;

namespace Aksl.Modules.LiveCharts.Maps.ViewModels
{
    public class WorldViewModel : BindableBase, INavigationAware
    {
        #region Members
        private readonly IDialogViewService _dialogViewService;
        private readonly Random _random = new();
        private readonly HeatLand _brazil;
        private bool _isBrazilInChart = true;
        #endregion

        #region Constructors
        public WorldViewModel()
        {
            _dialogViewService = (PrismApplication.Current as PrismApplicationBase).Container.Resolve<IDialogViewService>();

            // every country has a unique identifier
            // check the "shortName" property in the following
            // json file to assign a value to a country in the heat map
            // https://github.com/beto-rodriguez/LiveCharts2/blob/master/docs/_assets/word-map-index.json
            var lands = new HeatLand[]
            {
              new() { Name = "bra", Value = 13 },
              new() { Name = "mex", Value = 10 },
              new() { Name = "usa", Value = 15 },
              new() { Name = "can", Value = 8 },
              new() { Name = "ind", Value = 12 },
              new() { Name = "deu", Value = 13 },
              new() { Name= "jpn", Value = 15 },
              new() { Name = "chn", Value = 14 },
              new() { Name = "rus", Value = 11 },
              new() { Name = "fra", Value = 8 },
              new() { Name = "esp", Value = 7 },
              new() { Name = "kor", Value = 10 },
              new() { Name = "zaf", Value = 12 },
              new() { Name = "are", Value = 13 }
            };

            Series = [new HeatLandSeries { Lands = lands }];

            _brazil = lands.First(x => x.Name == "bra");
            DoRandomChanges();

            CreatePointerDownCommand();
        }
        #endregion

        #region Properties
        public HeatLandSeries[] Series { get; set; }
        #endregion

        #region ToggleBrazil Command
        public ICommand ToggleBrazilCommand { get; private set; }

        private void CreatePointerDownCommand()
        {
            ToggleBrazilCommand = new DelegateCommand( ()=>
            {
                ExecuteToggleBrazilCommand();
            },
            () =>
            {
                return true;
            });
        }

        private void ExecuteToggleBrazilCommand()
        {
            var lands = Series[0].Lands;
            if (lands is null)
            {
                return;
            }

            if (_isBrazilInChart)
            {
                Series[0].Lands = lands.Where(x => x != _brazil).ToArray();
                _isBrazilInChart = false;
                return;
            }

            Series[0].Lands = [.. lands, _brazil];
            _isBrazilInChart = true;
        }
        #endregion

        #region DoRandomChanges Method
        private async void DoRandomChanges()
        {
            await Task.Delay(1000);

            while (true)
            {
                foreach (var shape in Series[0].Lands ?? Enumerable.Empty<IWeigthedMapLand>())
                {
                    shape.Value = _random.Next(0, 20);
                }

                await Task.Delay(500);
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
