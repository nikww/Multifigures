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
        public Circle(double xx, double yy, Color cc) : base(xx, yy, cc) { }

        public override void Draw(DrawingContext context) 
        {
            Brush brush = new SolidColorBrush(Colors.Black);
            Pen pen = new Pen(new SolidColorBrush(c), 1, lineCap: PenLineCap.Square);
            context.DrawEllipse(brush, pen, new Point(x, y), r, r);
        }
        public override bool IsInside(double curs_x, double curs_y)
        {
            if (Math.Sqrt(Math.Abs(curs_x - x) * Math.Abs(curs_x - x) + Math.Abs(curs_y - y) * Math.Abs(curs_y - y)) <= r)
            {
                return true;
            }
            return false;
        }
    }
}
