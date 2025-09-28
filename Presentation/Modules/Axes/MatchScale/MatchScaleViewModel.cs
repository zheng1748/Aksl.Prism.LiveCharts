using System;
using System.Collections.Generic;

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
    public class MatchScaleViewModel : BindableBase, INavigationAware
    {
        #region Members
        private readonly IDialogViewService _dialogViewService;
        private AxisPosition _selectedPosition = AxisPosition.End;
        private int _selectedColor = 0;
        private readonly LvcColor[] _colors = ColorPalletes.FluentDesign;
        #endregion

        #region Constructors
        public MatchScaleViewModel()
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
                SeparatorsPaint = new SolidColorPaint(new SKColor(220, 220, 200)),
                MinStep = 0.1,
                ForceStepToMin = true
            }
         ];

        public ICartesianAxis[] YAxes { get; set; } = 
        [
            new Axis
            {
                Name = "Y axis",
                SeparatorsPaint = new SolidColorPaint(new SKColor(200, 200, 200)),
                MinStep = 0.1,
                ForceStepToMin = true
            }
        ];
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
