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
using LiveChartsCore.SkiaSharpView.Painting.Effects;
using System.Collections.Generic;

namespace Aksl.Modules.LiveCharts.Axes.ViewModels
{
    public class StyleViewModel : BindableBase, INavigationAware
    {
        #region Members
        private readonly IDialogViewService _dialogViewService;
        private static readonly SKColor s_gray = new(195, 195, 195);
        private static readonly SKColor s_gray1 = new(160, 160, 160);
        private static readonly SKColor s_gray2 = new(90, 90, 90);
        private static readonly SKColor s_dark3 = new(60, 60, 60);
        #endregion

        #region Constructors
        public StyleViewModel()
        {
            _dialogViewService = (PrismApplication.Current as PrismApplicationBase).Container.Resolve<IDialogViewService>();
        }
        #endregion

        #region Properties
        public ISeries[] Series { get; set; } =
        [
             new LineSeries<ObservablePoint>
             {
                 Values = Fetch(),
                 Stroke = new SolidColorPaint(new SKColor(33, 150, 243), 4),
                 Fill = null,
                 GeometrySize = 0
             }
        ];

        public ICartesianAxis[] XAxes { get; set; } =
        [
            new Axis
            {
                Name = "X axis",
                NamePaint = new SolidColorPaint(s_gray1),
                TextSize = 18,
                Padding = new Padding(5, 15, 5, 5),
                LabelsPaint = new SolidColorPaint(s_gray),
                SeparatorsPaint = new SolidColorPaint
                {
                    Color = s_gray,
                    StrokeThickness = 1,
                    PathEffect = new DashEffect([3, 3])
                },
                SubseparatorsPaint = new SolidColorPaint
                {
                    Color = s_gray2,
                    StrokeThickness = 0.5f
                },
                SubseparatorsCount = 9,
                ZeroPaint = new SolidColorPaint
                {
                    Color = s_gray1,
                    StrokeThickness = 2
                },
                TicksPaint = new SolidColorPaint
                {
                    Color = s_gray,
                    StrokeThickness = 1.5f
                },
                SubticksPaint = new SolidColorPaint
                {
                    Color = s_gray,
                    StrokeThickness = 1
                }
            }
         ];

        public ICartesianAxis[] YAxes { get; set; } = 
        [
            new Axis
            {
                Name = "Y axis",
                NamePaint = new SolidColorPaint(s_gray1),
                TextSize = 18,
                Padding = new Padding(5, 0, 15, 0),
                LabelsPaint = new SolidColorPaint(s_gray),
                SeparatorsPaint = new SolidColorPaint
                {
                    Color = s_gray,
                    StrokeThickness = 1,
                    PathEffect = new DashEffect([3, 3])
                },
                SubseparatorsPaint = new SolidColorPaint
                {
                    Color = s_gray2,
                    StrokeThickness = 0.5f
                },
                SubseparatorsCount = 9,
                ZeroPaint = new SolidColorPaint
                {
                    Color = s_gray1,
                    StrokeThickness = 2
                },
                TicksPaint = new SolidColorPaint
                {
                    Color = s_gray,
                    StrokeThickness = 1.5f
                },
                SubticksPaint = new SolidColorPaint
                {
                    Color = s_gray,
                    StrokeThickness = 1
                }
            }
        ];

        public DrawMarginFrame Frame { get; set; } = new()
        {
         Fill = new SolidColorPaint(s_dark3),
         Stroke = new SolidColorPaint
         {
             Color = s_gray,
             StrokeThickness = 1
         }
         };
        #endregion

        #region Fetch Method
        private static List<ObservablePoint> Fetch()
        {
            var list = new List<ObservablePoint>();
            var fx = EasingFunctions.BounceInOut;

            for (var x = 0f; x < 1f; x += 0.001f)
            {
                var y = fx(x);

                list.Add(new()
                {
                    X = x - 0.5,
                    Y = y - 0.5
                });
            }

            return list;
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
