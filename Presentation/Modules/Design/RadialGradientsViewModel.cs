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

namespace Aksl.Modules.LiveCharts.Box.ViewModels
{
    public class RadialGradientsViewModel : BindableBase, INavigationAware
    {
        #region Members
        private readonly IDialogViewService _dialogViewService;
        // radial gradients are based on SkiaSharp circular gradients
        // for more info please see:
        // https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/graphics/skiasharp/effects/shaders/circular-gradients

        private static readonly SKColor[] s_colors = [
            new SKColor(179, 229, 252),
            new SKColor(1, 87, 155)
        // ...

        // you can add as many colors as you require to build the gradient
        // by default all the distance between each color is equal
        // use the colorPos parameter in the constructor of the RadialGradientPaint class
        // to specify the distance between each color
        ];
        #endregion

        #region Constructors
        public RadialGradientsViewModel()
        {
            _dialogViewService = (PrismApplication.Current as PrismApplicationBase).Container.Resolve<IDialogViewService>();
        }
        #endregion

        #region Properties
        public ISeries[] Series { get; set; } =
        [
            new PieSeries<int>
            {
                Name = "Maria",
                Values = [7],
                Stroke = null,
                Fill = new RadialGradientPaint(s_colors),
                Pushout = 10,
                OuterRadiusOffset = 20
            },
            new PieSeries<int>
            {
                Name = "Charles",
                Values = [3],
                Stroke = null,
                Fill = new RadialGradientPaint(new SKColor(255, 205, 210), new SKColor(183, 28, 28))
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
