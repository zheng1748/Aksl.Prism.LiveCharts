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
using LiveChartsCore.Drawing;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Drawing.Geometries;

using Aksl.Toolkit.Services;

namespace Aksl.Modules.LiveCharts.Bars.ViewModels
{
    public class BarsCustomViewModel : BindableBase, INavigationAware
    {
        #region Members
        private readonly IDialogViewService _dialogViewService;
        #endregion

        #region Constructors
        public BarsCustomViewModel()
        {
            _dialogViewService = (PrismApplication.Current as PrismApplicationBase).Container.Resolve<IDialogViewService>();
        }
        #endregion

        #region Properties
        public ISeries[] Series { get; set; } =
        [
           new ColumnSeries<double>([2, 1, 4]),

            // use the second generic parameter to define the geometry to draw
            // there are many predefined geometries in the LiveChartsCore.Drawing namespace
            // for example, the StarGeometry, CrossGeometry, RectangleGeometry and DiamondGeometry
            new ColumnSeries<double, DiamondGeometry>([4, 3, 6]),

            // You can also use SVG paths to draw the geometry
            // the VariableSVGPathGeometry can change the drawn path at runtime
            new ColumnSeries<double, VariableSVGPathGeometry>([-2, 2, 1])
            {
                GeometrySvg = SVGPoints.Star
            },

            // finally you can also use SkiaSharp to draw your own geometry
            new ColumnSeries<double, MyGeometry>([4, 5, 2])
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
