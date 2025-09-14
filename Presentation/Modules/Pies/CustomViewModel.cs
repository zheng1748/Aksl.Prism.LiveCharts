using System;
using System.Collections.Generic;
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
using LiveChartsCore.SkiaSharpView.Extensions;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

using Aksl.Toolkit.Services;

namespace Aksl.Modules.LiveCharts.Pies.ViewModels
{
    public class CustomViewModel : BindableBase, INavigationAware
    {
        #region Members
        private readonly IDialogViewService _dialogViewService;
        #endregion

        #region Constructors
        public CustomViewModel()
        {
            _dialogViewService = (PrismApplication.Current as PrismApplicationBase).Container.Resolve<IDialogViewService>();

            var outer = 0;
            var data = new[] { 6, 5, 4, 3 };

            // you can convert any array, list or IEnumerable<T> to a pie series collection:
            Series = data.AsPieSeries((value, series) =>
            {
                // this method is called once per element in the array, so:

                // for the series with the value 6, we set the OuterRadiusOffset to 0
                // for the series with the value 5, the OuterRadiusOffset is 50
                // for the series with the value 4, the OuterRadiusOffset is 100
                // for the series with the value 3, the OuterRadiusOffset is 150

                series.OuterRadiusOffset = outer;
                outer += 50;

                series.DataLabelsPaint = new SolidColorPaint(SKColors.White)
                {
                    SKTypeface = SKTypeface.FromFamilyName("Arial", SKFontStyle.Bold)
                };

                series.ToolTipLabelFormatter =
                point =>
                {
                    var pv = point.Coordinate.PrimaryValue;
                    var sv = point.StackedValue!;

                    var a = $"{pv}/{sv.Total}{Environment.NewLine}{sv.Share:P2}";
                    return a;
                };

                series.DataLabelsFormatter =
                point =>
                {
                    var pv = point.Coordinate.PrimaryValue;
                    var sv = point.StackedValue!;

                    var a = $"{pv}/{sv.Total}{Environment.NewLine}{sv.Share:P2}";
                    return a;
                };
            });
        }
        #endregion

        #region Properties
        public IEnumerable<ISeries> Series { get; set; }
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
