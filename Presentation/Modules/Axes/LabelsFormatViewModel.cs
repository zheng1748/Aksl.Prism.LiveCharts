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
    public class LabelsFormatViewModel : BindableBase, INavigationAware
    {
        #region Members
        private readonly IDialogViewService _dialogViewService;
        private AxisPosition _selectedPosition = AxisPosition.End;
        private int _selectedColor = 0;
        private readonly LvcColor[] _colors = ColorPalletes.FluentDesign;
        #endregion

        #region Constructors
        public LabelsFormatViewModel()
        {
            _dialogViewService = (PrismApplication.Current as PrismApplicationBase).Container.Resolve<IDialogViewService>();
        }
        #endregion

        #region Properties
        public ISeries[] Series { get; set; } =
        [
             new ColumnSeries<double> { Values = [426, 583, 104] },
             new LineSeries<double> { Values = [200, 558, 458], Fill = null },
        ];

        public ICartesianAxis[] XAxes { get; set; } =
        [
            new Axis
            {
                Name = "Salesman/woman",
                // Use the labels property for named or static labels // mark
                Labels = ["Sergio", "Lando", "Lewis"], // mark
                LabelsRotation = 15,
            }
         ];

        public ICartesianAxis[] YAxes { get; set; } = 
        [
            new Axis
            {
                Name = "Salome",
                NamePadding = new LiveChartsCore.Drawing.Padding(0, 15),

                LabelsPaint = new SolidColorPaint
                {
                    Color = SKColors.Blue,
                    FontFamily = "Times New Roman",
                    SKFontStyle = new SKFontStyle(SKFontStyleWeight.ExtraBold, SKFontStyleWidth.Normal, SKFontStyleSlant.Italic)
                },

                // Use the Labeler property to give format to the axis values // mark
                // Now the Y axis labels have a currency format.

                // LiveCharts provides some common formatters
                // in this case we are using the currency formatter.
                Labeler = Labelers.Currency // mark

                // you could also build your own currency formatter
                // for example:
                // Labeler = (value) => value.ToString("C")
                // but the one that LiveCharts provides creates shorter labels when
                // the amount is in millions or trillions
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
