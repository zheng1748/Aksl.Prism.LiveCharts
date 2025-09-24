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
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.Kernel.Sketches;
using SkiaSharp;

using Aksl.Toolkit.Services;

namespace Aksl.Modules.LiveCharts.Axes.ViewModels
{
    public class LabelsFormat2ViewModel : BindableBase, INavigationAware
    {
        #region Members
        private readonly IDialogViewService _dialogViewService;
        #endregion

        #region Constructors
        public LabelsFormat2ViewModel()
        {
            _dialogViewService = (PrismApplication.Current as PrismApplicationBase).Container.Resolve<IDialogViewService>();

            // You must register any non-Latin based font //mark
            // you can add this code when the app starts to register Chinese characters: // mark

            LiveChartsCore.LiveCharts.Configure(config =>
                config.HasGlobalSKTypeface(SKFontManager.Default.MatchCharacter('汉')));

            // You can learn more about extra settings at: // mark
            // https://livecharts.dev/docs/{{ platform }}/{{ version }}/Overview.Installation#configure-themes-fonts-or-mappers-optional // mark
        }
        #endregion

        #region Properties
        public ISeries[] Series { get; set; } =
        [
           new ColumnSeries<double> { Values = [426, 583, 104] },
           new LineSeries<double> { Values = [200, 558, 458], Fill = null }
        ];

        public ICartesianAxis[] XAxes { get; set; } =
        [
            new Axis
            {
                Name = "Salesman/woman",
                Labels = ["王", "赵", "张"],
                LabelsPaint = new SolidColorPaint(SKColors.Black)
            }
         ];

        public ICartesianAxis[] YAxes { get; set; } = 
        [
            new Axis
            {
                Name = "Sales amount",
                NamePadding = new LiveChartsCore.Drawing.Padding(0, 15),
                Labeler = Labelers.Currency,
                LabelsPaint = new SolidColorPaint
                {
                    Color = SKColors.Blue,
                    FontFamily = "Times New Roman",
                    SKFontStyle = new SKFontStyle(
                    SKFontStyleWeight.ExtraBold,
                    SKFontStyleWidth.Normal,
                    SKFontStyleSlant.Italic)
                },
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
