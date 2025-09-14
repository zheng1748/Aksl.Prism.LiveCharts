using System;
using System.Collections.Generic;
using System.Linq;

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
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Extensions;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

using Aksl.Toolkit.Services;

namespace Aksl.Modules.LiveCharts.Pies.ViewModels
{
    public class GaugeViewModel : BindableBase, INavigationAware
    {
        #region Members
        private readonly IDialogViewService _dialogViewService;
        #endregion

        #region Constructors
        public GaugeViewModel()
        {
            _dialogViewService = (PrismApplication.Current as PrismApplicationBase).Container.Resolve<IDialogViewService>();

            _maxAngle = 270;
            GaugeTotal = 60;
            _initialRotation = 135;

            Series = GaugeGenerator.BuildSolidGauge
            (
                new GaugeItem(10, SetStyle),
                new GaugeItem(25, SetStyle),
                new GaugeItem(50, SetStyle),
                new GaugeItem(50, SetStyle),
                new GaugeItem(GaugeItem.Background, SetBackgroundStyle)
             );
        }
        #endregion

        #region Properties
        public IEnumerable<PieSeries<ObservableValue>> Series { get; set; }

        public double GaugeTotal { get; set; }

        private double _initialRotation;
        public double InitialRotation
        {
            get => _initialRotation;
            set
            {
                SetProperty<double>(ref _initialRotation, value);
            }
        }

        private double _maxAngle;
        public double MaxAngle
        {
            get => _maxAngle;
            set
            {
                SetProperty<double>(ref _maxAngle, value);
            }
        }

        private double _backgroundInnerRadius = 100;
        public double BackgroundInnerRadius
        {
            get => _backgroundInnerRadius;
            set
            {
                if (SetProperty<double>(ref _backgroundInnerRadius, value))
                {
                    foreach (var item in Series.Where(x => x.IsFillSeries))
                    {
                        item.InnerRadius = value;
                    }
                }
            }
        }

        private double _backgroundOffsetRadius = 20;
        public double BackgroundOffsetRadius
        {
            get => _backgroundOffsetRadius;
            set
            {
                if (SetProperty<double>(ref _backgroundOffsetRadius, value))
                {

                    foreach (var item in Series.Where(x => x.IsFillSeries))
                    {
                        item.RelativeInnerRadius = value;
                        item.RelativeOuterRadius = value;
                    }
                }
            }
        }

        private double _innerRadius = 100;
        public double InnerRadius
        {
            get => _innerRadius;
            set
            {
                if (SetProperty<double>(ref _innerRadius, value))
                {
                    _innerRadius = value;
                    foreach (var item in Series.Where(x => !x.IsFillSeries))
                    {
                        item.InnerRadius = value;
                    }
                }
            }
        }

        private double _offsetRadius = 10;
        public double OffsetRadius
        {
            get => _offsetRadius;
            set
            {
                if (SetProperty<double>(ref _offsetRadius, value))
                {
                    foreach (var item in Series.Where(x => !x.IsFillSeries))
                    {
                        item.RelativeInnerRadius = value;
                        item.RelativeOuterRadius = value;
                    }
                }
            }
        }
        #endregion

        #region Set Style Method
        private void SetStyle(PieSeries<ObservableValue> series)
        {
            series.InnerRadius = InnerRadius;
            series.RelativeInnerRadius = OffsetRadius;
            series.RelativeOuterRadius = OffsetRadius;
            series.DataLabelsFormatter = point => point.Coordinate.PrimaryValue.ToString();
            series.DataLabelsPosition = PolarLabelsPosition.Start;
            series.DataLabelsSize = 30;
        }

        private void SetBackgroundStyle(PieSeries<ObservableValue> series)
        {
            series.Fill = new SolidColorPaint(new SKColor(0, 0, 0, 10));
            series.InnerRadius = BackgroundInnerRadius;
            series.RelativeInnerRadius = BackgroundOffsetRadius;
            series.RelativeOuterRadius = BackgroundOffsetRadius;
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
