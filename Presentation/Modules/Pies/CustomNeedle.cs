using LiveChartsCore.SkiaSharpView.Drawing;
using LiveChartsCore.SkiaSharpView.Drawing.Geometries;

namespace Aksl.Modules.LiveCharts.Pies.ViewModels
{
    public class CustomNeedle : NeedleGeometry
    {
        public override void Draw(SkiaSharpDrawingContext context)
        {
            var paint = context.ActiveSkiaPaint;

            context.Canvas.DrawRect(X - Width * 0.5f, Y, Width, Radius, paint);
        }
    }

    public class SmallNeedle : NeedleGeometry
    {
        public SmallNeedle()
        {
            ScaleTransform = new(0.6f, 0.6f);
        }
    }
}
