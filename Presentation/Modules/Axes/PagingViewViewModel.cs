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
using System.Collections.Generic;

namespace Aksl.Modules.LiveCharts.Axes.ViewModels
{
    public class PagingViewModel : BindableBase, INavigationAware
    {
        #region Members
        private readonly IDialogViewService _dialogViewService;
        private readonly Random _random = new();
        #endregion

        #region Constructors
        public PagingViewModel()
        {
            _dialogViewService = (PrismApplication.Current as PrismApplicationBase).Container.Resolve<IDialogViewService>();

            var trend = 100;
            var values = new List<int>();

            for (var i = 0; i < 100; i++)
            {
                trend += _random.Next(-30, 50);
                values.Add(trend);
            }

            Series = [new ColumnSeries<int>(values)];
            XAxes = [new Axis()];
        }
        #endregion

        #region Properties
        public ISeries[] Series { get; set; }

        public ICartesianAxis[] XAxes { get; set; }
        #endregion

        #region Methods
        public void GoToPage1()
        {
            var axis = XAxes[0];
            axis.MinLimit = -0.5;
            axis.MaxLimit = 10.5;
        }

        public void GoToPage2()
        {
            var axis = XAxes[0];
            axis.MinLimit = 9.5;
            axis.MaxLimit = 20.5;
        }

        public void GoToPage3()
        {
            var axis = XAxes[0];
            axis.MinLimit = 19.5;
            axis.MaxLimit = 30.5;
        }

        public void SeeAll()
        {
            var axis = XAxes[0];
            axis.MinLimit = null;
            axis.MaxLimit = null;
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
