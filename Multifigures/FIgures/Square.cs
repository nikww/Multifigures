using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Media;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Multifigures.Figures
{
    sealed class Square : Shape
    {
        public Square(int xx, int yy, Color cc) : base(xx, yy, cc) {}

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
            throw new NotImplementedException();
        }
    }
}
