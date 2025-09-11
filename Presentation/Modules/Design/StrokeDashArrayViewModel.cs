using System;
using System.Collections.ObjectModel;
using System.Windows.Media.Effects;
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
using LiveChartsCore.SkiaSharpView.Painting.Effects;
using SkiaSharp;

using Aksl.Toolkit.Services;

namespace Aksl.Modules.LiveCharts.Design.ViewModels
{
    public class StrokeDashArrayViewModel : BindableBase, INavigationAware
    {
        #region Members
        private readonly IDialogViewService _dialogViewService;
        #endregion

        #region Constructors
        public StrokeDashArrayViewModel()
        {
            _dialogViewService = (PrismApplication.Current as PrismApplicationBase).Container.Resolve<IDialogViewService>();

            var strokeThickness = 10;
            var strokeDashArray = new float[] { 3 * strokeThickness, 2 * strokeThickness };
            var effect = new DashEffect(strokeDashArray);

            Series =
            [
                new LineSeries<int>
                {
                    Values = [4, 2, 8, 5, 3],
                    LineSmoothness = 1,
                    GeometrySize = 22,
                    Stroke = new SolidColorPaint
                    {
                        Color = SKColors.CornflowerBlue,
                        StrokeCap = SKStrokeCap.Round,
                        StrokeThickness = strokeThickness,
                        PathEffect = effect
                    },
                    Fill = null
                }
            ];
        }
        #endregion

        #region Properties
        public ISeries[] Series { get; set; }
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
