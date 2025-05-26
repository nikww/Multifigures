using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Media;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Multifigures.Figures
{
    public sealed class Square : Shape
    {
        public Square(double xx, double yy) : base(xx, yy) {}

        public override void Draw(DrawingContext context)
        {
            Pen pen = new Pen(new SolidColorBrush(c), 1, lineCap: PenLineCap.Square);
            double a = Math.Sqrt(r * r / 2);

            Point p1 = new Point(x - a, y + a);
            Point p2 = new Point(x + a, y + a);
            Point p3 = new Point(x + a, y - a);
            Point p4 = new Point(x - a, y - a);
            context.DrawLine(pen, p1, p2);
            context.DrawLine(pen, p2, p3);
            context.DrawLine(pen, p3, p4);
            context.DrawLine(pen, p4, p1);
        }
        public override bool IsInside(double curs_x, double curs_y)
        {
            double a = Math.Sqrt(r * r / 2);
            if (Math.Abs(curs_x - x) <= a && Math.Abs(curs_y - y) <= a)
            {
                return true;
            }
            return false;
        }
    }
}
