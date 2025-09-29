
using System;

using LiveChartsCore.Drawing;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView.Drawing;

namespace Aksl.Modules.LiveCharts.Bars.ViewModels
{
    public class MyGeometry : BoundedDrawnGeometry, IDrawnElement<SkiaSharpDrawingContext>
    {
        public void Draw(SkiaSharpDrawingContext context)
        {
            var paint = context.ActiveSkiaPaint;
            var canvas = context.Canvas;
            var y = Y;

            while (y < Y + Height)
            {
                canvas.DrawLine(X, y, X + Width, y, paint);
                y += 5;
            }
        }
    }

}
