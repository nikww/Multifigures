using Avalonia.Controls;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;

namespace Multifigures
{
    public class CustomControl : UserControl
    {
        public override void Render(DrawingContext context)
        {
            Brush brush = new SolidColorBrush(Colors.Black);
            Pen pen = new Pen(Brushes.Aqua, 1, lineCap: PenLineCap.Square);

            context.DrawEllipse(brush, pen, new Point(100, 100), 10, 10);
        }
    }
}
