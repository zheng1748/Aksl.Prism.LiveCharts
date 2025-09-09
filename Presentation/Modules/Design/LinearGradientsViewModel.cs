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

namespace Aksl.Modules.LiveCharts.Design.ViewModels
{
    public class LinearGradientsViewModel : BindableBase, INavigationAware
    {
        #region Members
        private readonly IDialogViewService _dialogViewService;
        #endregion

        #region Constructors
        public LinearGradientsViewModel()
        {
            _dialogViewService = (PrismApplication.Current as PrismApplicationBase).Container.Resolve<IDialogViewService>();
        }
        #endregion

        #region Properties
        public ISeries[] Series { get; set; } =
        [
            new ColumnSeries<int>
            {
                Name = "John",
                Values = [3, 7, 2, 9, 4],
                Stroke = null,

                // this is an easy way to set a linear gradient:
                // Fill = new LinearGradientPaint(new SKColor(255, 140, 148), new SKColor(220, 237, 194))

                // but you can customize the gradient
                Fill = new LinearGradientPaint(
                // the gradient will use the following colors array
                [new SKColor(255, 140, 148), new SKColor(220, 237, 194)],

                // now with the following points we are specifying the orientation of the gradient
                // by default the gradient is orientated horizontally
                // defined by the points: (0, 0.5) and (1, 0.5)
                // but for this sample we will use a vertical gradient:

                // to build a vertical gradient we must specify 2 points that will draw a imaginary line
                // the gradient will interpolate colors lineally as it moves following this imaginary line
                // the coordinates of these points (X, Y) go from 0 to 1
                // where 0 is the start of the axis and 1 the end. Then to build our vertical gradient

                // we must go from the point:
                // (x0, y0) where x0 could be read as "the middle of the x axis" (0.5) and y0 as
                // "the start of the y axis" (0)
                new SKPoint(0.5f, 0),

                // to the point:
                // (x1, y1) where x1 could be read as "the middle of the x axis" (0.5) and y0 as
                // "the end of the y axis" (1)
                new SKPoint(0.5f, 1))
            },
            new LineSeries<int>
            {
                Name = "Charles",
                Values = [4, 2, 8, 5, 3],
                GeometrySize = 22,
                Stroke = new LinearGradientPaint([new SKColor(45, 64, 89), new SKColor(255, 212, 96)])
                {
                    StrokeThickness = 10
                },
                GeometryStroke = new LinearGradientPaint([new SKColor(45, 64, 89), new SKColor(255, 212, 96)])
                {
                    StrokeThickness = 10
                },
                Fill = null
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
