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

namespace Aksl.Modules.LiveCharts.Axes.ViewModels
{
    public class LabelsRotationViewModel : BindableBase, INavigationAware
    {
        #region Members
        private readonly IDialogViewService _dialogViewService;
        private AxisPosition _selectedPosition = AxisPosition.End;
        private int _selectedColor = 0;
        private readonly LvcColor[] _colors = ColorPalletes.FluentDesign;
        #endregion

        #region Constructors
        public LabelsRotationViewModel()
        {
            _dialogViewService = (PrismApplication.Current as PrismApplicationBase).Container.Resolve<IDialogViewService>();
        }
        #endregion

        #region Properties
        public ISeries[] Series { get; set; } =
        [
              new LineSeries<double>
              {
                  Values = [200, 558, 458, 249, 457, 339, 587],
                  XToolTipLabelFormatter = (point) =>
                      $"This is {Environment.NewLine}" +
                      $"A multi-line label {Environment.NewLine}" +
                      $"With a value of {Environment.NewLine}" + point.Coordinate.PrimaryValue,
              }
        ];

        public ICartesianAxis[] XAxes { get; set; } =
        [
            new Axis
            {
                // Use the Label property to indicate the format of the labels in the axis
                // The Labeler takes the value of the label as parameter and must return it as string
                Labeler = (value) =>
                    $"This is {Environment.NewLine}" +
                    $"A multi-line label {Environment.NewLine}" +
                    $"With a value of {Environment.NewLine}" + value * 100,

                // The MinStep property lets you define the minimum separation (in chart values scale)
                // between every axis separator, in this case we don't want decimals,
                // so lets force it to be greater or equals than 1
                MinStep = 1,

                // labels rotations is in degrees (0 - 360)
                LabelsRotation = 45,

                SeparatorsPaint = new SolidColorPaint(SKColors.LightGray, 2)
            }
         ];

        public ICartesianAxis[] YAxes { get; set; } = 
        [
            new Axis
            {
                LabelsRotation = 15,

                // Now the Y axis we will display it as currency
                // LiveCharts provides some common formatters
                // in this case we are using the currency formatter.
                Labeler = Labelers.Currency,

                // you could also build your own currency formatter
                // for example:
                // Labeler = (value) => value.ToString("C")

                // But the one that LiveCharts provides creates shorter labels when
                // the amount is in millions or trillions

                SeparatorsPaint = new SolidColorPaint(SKColors.LightGray, 2)
            }
        ];

        private double _sliderValue = 15;
        public double SliderValue
        {
            get => _sliderValue;
            set
            {
                if (SetProperty(ref _sliderValue, value))
                {
                    YAxes[0].LabelsRotation = value;
                }
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
