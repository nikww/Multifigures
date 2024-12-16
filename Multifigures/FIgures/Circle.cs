using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;

namespace Multifigures.Figures
{
    public sealed class Circle : Shape
    { 
        public Circle(int xx, int yy, Color cc) : base(xx, yy, cc) { }

        public override void Draw(DrawingContext context)
        {
            Brush brush = new SolidColorBrush(Colors.Black);
            Pen pen = new Pen(new SolidColorBrush(c), 1, lineCap: PenLineCap.Square);
            context.DrawEllipse(brush, pen, new Point(x, y), r, r);
        }
    }
}
