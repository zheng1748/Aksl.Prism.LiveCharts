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
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

using Aksl.Toolkit.Services;

namespace Aksl.Modules.LiveCharts.Bars.ViewModels
{
    public class RowsWithLabelsViewModel : BindableBase, INavigationAware
    {
        #region Members
        private readonly IDialogViewService _dialogViewService;
        #endregion

        #region Constructors
        public RowsWithLabelsViewModel()
        {
            _dialogViewService = (PrismApplication.Current as PrismApplicationBase).Container.Resolve<IDialogViewService>();
        }
        #endregion

        #region Properties
        public ISeries[] Series { get; set; } =
        [
         new RowSeries<int>
         {
             Values = [8, -3, 4],
             Stroke = null,
             DataLabelsPaint = new SolidColorPaint(new SKColor(45, 45, 45)),
             DataLabelsSize = 14,
             DataLabelsPosition = DataLabelsPosition.End
         },
            new RowSeries<int>
            {
                Values = [4, -6, 5],
                Stroke = null,
                DataLabelsPaint = new SolidColorPaint(new SKColor(250, 250, 250)),
                DataLabelsSize = 14,
                DataLabelsPosition = DataLabelsPosition.Middle
            },
            new RowSeries<int>
            {
                Values = [6, -9, 3],
                Stroke = null,
                DataLabelsPaint = new SolidColorPaint(new SKColor(45, 45, 45)),
                DataLabelsSize = 14,
                DataLabelsPosition = DataLabelsPosition.Start
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
